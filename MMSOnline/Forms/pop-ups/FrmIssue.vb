'Imports MySql.Data.MySqlClient
'Imports MMSOnline.Functions
'Imports System.IO
'Imports iTextSharp.text.pdf
'Imports iTextSharp.text

Imports MySql.Data.MySqlClient
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.Data
Imports MMSOnline.Functions
Imports System.IO

Public Class FrmIssue
    Private funcs As New Functions
    Public persistant As PreFetch
    Public IssueNumber, CustomerNumber As Integer
    Public WONumber As Integer
    Public Store As String

    Private Const mydatacount As Integer = 32
    Private dontsave As Boolean
    Private mydata(mydatacount) As element
    Private thisIssuedata As New IssueData
    'Dataview stuff
    Private Partsdatatable As New DataTable
    Private Partsnewdata As New DataTable
    Private Partsbindingsource As New BindingSource
    Private Partsbinded As Boolean

    Private Workdatatable As New DataTable
    Private Worknewdata As New DataTable
    Private Workbindingsource As New BindingSource
    Private Workbinded As Boolean

    Private Pkgsdatatable As New DataTable
    Private Pkgsnewdata As New DataTable
    Private Pkgsbindingsource As New BindingSource
    Private Pkgsbinded As Boolean

    Private Paymentdatatable As New DataTable
    Private Paymentnewdata As New DataTable
    Private Paymentbindingsource As New BindingSource
    Private Paymentbinded As Boolean

    Private PicsDataTable As New DataTable
    Private PicsNewData As New DataTable
    Private PicsBindingSource As New BindingSource
    Private PicsBinded As Boolean

    'Private WOnum As Integer
    Private bosnum As Integer
    'Private WOstore As String
    Private Sub FrmIssue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TEMP remove pics tab
        'TabControl1.TabPages.Remove(TabControl1.TabPages.Item("TabPage4"))
        dontsave = False
        If persistant.myuserLEVEL < 2 Or persistant.myuserLEVEL > 7 Then
            txtbillablehrs.Enabled = True
        Else
            txtbillablehrs.Enabled = False
        End If
        Cmbpaymenttype.Enabled = True
        CmbApprovalStatus.Enabled = True
        If persistant.myuserLEVEL > 0 Then
            CmbTypeOfWork.Enabled = True
            cmbStatus.Enabled = True
        End If
        CmbPkgType.SelectedItem = "Detail"

        setuparray()
        If IssueNumber > 0 Then
            'LOAD EXISTING
            downloadIssuedata()
            'Dim conn As New MySqlConnection
            'Dim cmd As New MySqlCommand
            'conn.ConnectionString = persistant.myconnstring
            'cmd.Connection = conn
            'Try
            '    conn.Open()
            '    cmd.CommandText = "Select WONumber from ServiceWOtoIssue where issue = " + IssueNumber.ToString + ""
            '    WOnum = CInt(cmd.ExecuteScalar)
            '    cmd.CommandText = "Select WOStore from ServiceWOtoIssue where issue = " + IssueNumber.ToString + ""
            '    WOstore = cmd.ExecuteScalar
            '    conn.Close()

            'Catch ex As Exception
            '    MessageBox.Show(ex.Message)
            'End Try

            'WritetoScreen()
        Else
            'NEW ISSUE
            btnAssign.Visible = False
            'set tech name in the assigned to box if the tech created the issue.
            If persistant.myuserLEVEL < 1 Then txtAssignedTo.Text = persistant.myusername
            stringchanged(txtAssignedTo)

            Dim NewIssueApproval, NewIssuePayment, NewIssueType As String
            Dim newissue As New frmNewIssue
            newissue.ShowDialog()
            If newissue.DialogResult = Windows.Forms.DialogResult.OK Then
                NewIssueApproval = newissue.Approval
                NewIssuePayment = newissue.Payment
                NewIssueType = newissue.Type
                Cmbpaymenttype.SelectedItem = NewIssuePayment
                cmbStatus.SelectedItem = "Reported"
                CmbTypeOfWork.SelectedItem = NewIssueType
                cmbchanged(Cmbpaymenttype)
                cmbchanged(cmbStatus)
                cmbchanged(CmbTypeOfWork)
                txtQuoteRate.Text = "$128"
                moneychanged(txtQuoteRate)
                If Cmbpaymenttype.SelectedItem = "WARRANTY" Then
                    CmbBillCustomer.SelectedItem = "NO"
                    txtbillablehrs.Text = "0"
                    stringchanged(txtbillablehrs)
                    'reload approval list
                    'This list is also reloaded in the 'downloadIssuedata' sub routine
                    CmbApprovalStatus.Items.Clear()
                    CmbApprovalStatus.Items.Add("To Be Processed")
                    CmbApprovalStatus.Items.Add("Waiting for Approval")
                    CmbApprovalStatus.Items.Add("Pre Authorized")
                    CmbApprovalStatus.Items.Add("Approved")
                    CmbApprovalStatus.Items.Add("Waiting for Parts")
                    CmbApprovalStatus.Items.Add("Waiting on Customer")
                    CmbApprovalStatus.Items.Add("Returning Boat to Factory")
                    CmbApprovalStatus.Items.Add("Paid Not Complete")
                    CmbApprovalStatus.Items.Add("Complete Unpaid")
                    CmbApprovalStatus.Items.Add("Complete Paid")
                    CmbApprovalStatus.Items.Add("Rejected")
                Else
                    If NewIssueType = "Rig - Sterndrive" Or NewIssueType = "Rig - Outboard" Then

                        CmbBillCustomer.SelectedItem = "NO"
                    Else
                        CmbBillCustomer.SelectedItem = "YES"
                    End If
                End If
                CmbApprovalStatus.SelectedItem = NewIssueApproval
                cmbchanged(CmbApprovalStatus)
                cmbchanged(CmbBillCustomer)
                save()
            Else
                dontsave = True
                Me.Close()
            End If
        End If

        WritetoScreen()
        showhide()
    End Sub

    Private Sub refreshpgsdataview(ByVal data As DataTable)
        Pkgsdatatable.Clear()
        Pkgsdatatable.Merge(data)
        Pkgsbindingsource.ResetBindings(False)

        If Pkgsbinded = False Then
            Pkgsbindingsource.DataSource = Pkgsdatatable
            DVServicePkg.DataSource = Pkgsbindingsource
            Pkgsbinded = True
        End If
        Me.DVServicePkg.Columns("Package").Width = 360
        Me.DVServicePkg.Columns("Price").Width = 50
        DVServicePkg.Columns("BillableHrs").Visible = False

    End Sub
    Private Sub Getnewpkgsdata()
        'Just fill newdata and pass it along to refreshdata
        If CmbTypeOfWork.SelectedItem = "Standard Service" Then
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            Dim adapter As New MySqlDataAdapter

            Dim SQL As String
            SQL = buildpkssql()

            conn.ConnectionString = persistant.myconnstring
            Try
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = SQL
                adapter.SelectCommand = cmd
                Pkgsnewdata.Clear()
                adapter.Fill(Pkgsnewdata)
                conn.Close()
                refreshpgsdataview(Pkgsnewdata)

            Catch ex As Exception
                MessageBox.Show("Error Connecting to Database: " & ex.Message)
            End Try
            If conn.State <> ConnectionState.Closed Then conn.Close()
            conn.Dispose()
        End If

    End Sub
    Private Function buildpkssql() As String
        buildpkssql = "Select Package,  concat('$', Price) as Price, BillableHrs from ServicePkgs where Type = '" + CmbPkgType.SelectedItem.ToString + "' Order by Num ASC"
    End Function

    Private Sub refreshpartsdataview(ByVal data As DataTable)

        Partsdatatable.Clear()
        Partsdatatable.Merge(data)
        Partsbindingsource.ResetBindings(False)

        If Partsbinded = False Then
            Partsbindingsource.DataSource = Partsdatatable
            DVParts.DataSource = Partsbindingsource
            Partsbinded = True
        End If

        Me.DVParts.Columns("Number").Visible = False
        If Partsdatatable.Rows.Count > 0 Then
            Me.DVParts.Columns(0).Visible = True
            Me.DVParts.ColumnHeadersVisible = True

        Else
            Me.DVParts.Columns(0).Visible = False
            Me.DVParts.ColumnHeadersVisible = False
        End If

    End Sub
    Private Sub Getnewpartsdata()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        Dim SQL As String
        SQL = buildpartssql()
        Me.DVParts.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            Partsnewdata.Clear()
            adapter.Fill(Partsnewdata)
            conn.Close()
            refreshpartsdataview(Partsnewdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
        Me.DVParts.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)


    End Sub
    Private Function buildpartssql() As String
        buildpartssql = "Select Number, Status, PartNumber, Supplier, Part, Quantity, Concat('$',Price) as 'Price',PaymentType from ServiceParts where IssueNumber = '" + Me.IssueNumber.ToString + "' Order by Number ASC"
    End Function

    Private Sub RefreshPicsDataView(ByVal data As DataTable)

        PicsDataTable.Clear()
        PicsDataTable.Merge(data)
        PicsBindingSource.ResetBindings(False)

        If PicsBinded = False Then
            PicsBindingSource.DataSource = PicsDataTable
            DVPictures.DataSource = PicsBindingSource
            PicsBinded = True
        End If

        Me.DVPictures.Columns("PicID").Visible = False
        If PicsDataTable.Rows.Count > 0 Then
            Me.DVPictures.Columns(0).Visible = True
            Me.DVPictures.ColumnHeadersVisible = True

        Else
            Me.DVPictures.Columns(0).Visible = False
            Me.DVPictures.ColumnHeadersVisible = False
        End If

    End Sub
    Private Sub GetNewPicsData()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        Dim SQL As String
        SQL = "Select PicID, PicName from ServicePics Where IssueNumber = '" + Me.IssueNumber.ToString + "' Order by PicName ASC"

        Me.DVPictures.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            PicsNewData.Clear()
            adapter.Fill(PicsNewData)
            conn.Close()
            RefreshPicsDataView(PicsNewData)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
        Me.DVPictures.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)


    End Sub

    Private Sub refreshworklogdataview(ByVal data As DataTable)
        Workdatatable.Clear()
        Workdatatable.Merge(data)
        Workbindingsource.ResetBindings(False)

        If Workbinded = False Then
            Workbindingsource.DataSource = Workdatatable
            DVWorkDone.DataSource = Workbindingsource
            Workbinded = True
        End If

        Me.DVWorkDone.Columns("Number").Visible = False
        If Workdatatable.Rows.Count > 0 Then
            Me.DVWorkDone.Columns(0).Visible = True
            Me.DVWorkDone.ColumnHeadersVisible = True

        Else
            Me.DVWorkDone.Columns(0).Visible = False
            Me.DVWorkDone.ColumnHeadersVisible = False
        End If

        Me.DVWorkDone.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader)
        DVWorkDone.Columns("Notes").Width = 125
    End Sub
    Private Sub Getnewworklogdata()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        Dim SQL As String
        SQL = buildWorklogsql()

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            Worknewdata.Clear()
            adapter.Fill(Worknewdata)
            conn.Close()
            refreshworklogdataview(Worknewdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub
    Private Function buildWorklogsql() As String
        buildWorklogsql = "Select Number, Tech, Notes, TimeWorked, timestart, timeend from ServiceWorkLog where Issue = '" + IssueNumber.ToString + "' Order by Number ASC"
    End Function

    Private Sub stringchanged(ByVal sender As Object)
        Dim data As String
        For i As Integer = 0 To (mydata.Length - 1)
            If sender.Equals(mydata(i).txtbox) = True Then
                data = sender.text
                mydata(i).value = data
                mydata(i).changed = True
            End If
        Next
    End Sub
    Private Sub cmbchanged(ByVal sender As Object)
        Try
            Dim data As String
            For i As Integer = 0 To (mydata.Length - 1)
                If sender.Equals(mydata(i).txtbox) = True Then
                    data = sender.selecteditem
                    mydata(i).value = data
                    mydata(i).changed = True
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub
    Private Sub decimalchanged(ByRef sender As Object)
        Dim y As Decimal

        Try
            y = Val(sender.text)

        Catch ex As Exception
            y = 0
        End Try

        If y <> 0 Then
            For i As Integer = 0 To (mydata.Length - 1)
                If sender.Equals(mydata(i).txtbox) = True Then
                    If Val(mydata(i).value) <> y Then
                        mydata(i).value = y.ToString
                        mydata(i).changed = True
                    End If
                    sender.text = y.ToString
                End If
            Next
        End If

        If y = 0 Then
            For i As Integer = 0 To (mydata.Length - 1)
                If sender.Equals(mydata(i).txtbox) = True Then
                    If Val(mydata(i).value) <> y Then
                        mydata(i).value = y.ToString
                        mydata(i).changed = True
                    End If
                    sender.text = "0.0"
                End If
            Next
        End If

    End Sub
    Private Sub moneychanged(ByRef sender As Object)

        Dim x As String
        Dim y As Decimal
        x = Replace(sender.text, "$", "")
        x = Replace(x, ",", "")
        x = Replace(x, "(", "-")
        x = Replace(x, ")", "")
        y = Val(x)

        If y <> 0 Then
            For i As Integer = 0 To (mydata.Length - 1)
                If sender.Equals(mydata(i).txtbox) = True Then
                    If Val(mydata(i).value) <> y Then
                        mydata(i).value = y
                        mydata(i).changed = True
                    End If
                    If i > 41 And i < 49 Then
                        mydata(i - 12).value = "1"
                        mydata(i - 12).txtbox.checked = True
                    End If
                    If i > 63 And i < 69 Then
                        mydata(i - 27).value = "1"
                        mydata(i - 27).txtbox.checked = True
                    End If
                    sender.text = Format(x, "currency")
                End If
            Next
        End If

        If y = 0 Then
            For i As Integer = 0 To (mydata.Length - 1)
                If sender.Equals(mydata(i).txtbox) = True Then
                    If Val(mydata(i).value) <> y Then
                        mydata(i).value = y.ToString
                        mydata(i).changed = True
                    End If
                    If i > 41 And i < 49 Then
                        mydata(i - 12).value = "0"
                        mydata(i - 12).txtbox.checked = False
                    End If
                    If i > 63 And i < 69 Then
                        mydata(i - 27).value = "0"
                        mydata(i - 27).txtbox.checked = False
                    End If
                    sender.text = ""
                End If
            Next
        End If

    End Sub
    Public Sub downloadIssuedata()
        thisIssuedata.IssueNumber = IssueNumber
        thisIssuedata.myconnstring = persistant.myconnstring
        thisIssuedata.Download()

        For x As Integer = 0 To mydatacount
            If mydata(x).dbtable = "Issue" Then mydata(x).value = thisIssuedata.getvalue(mydata(x).dbfeild)
            'reload the approval list for warranty items.
            If x = 2 And mydata(x).value = "WARRANTY" Then
                'reload approval list
                CmbApprovalStatus.Items.Clear()
                CmbApprovalStatus.Items.Add("To Be Processed")
                CmbApprovalStatus.Items.Add("Waiting for Approval")
                CmbApprovalStatus.Items.Add("Pre Authorized")
                CmbApprovalStatus.Items.Add("Approved")
                CmbApprovalStatus.Items.Add("Waiting for Parts")
                CmbApprovalStatus.Items.Add("Waiting on Customer")
                CmbApprovalStatus.Items.Add("Returning Boat to Factory")
                CmbApprovalStatus.Items.Add("Paid Not Complete")
                CmbApprovalStatus.Items.Add("Complete Unpaid")
                CmbApprovalStatus.Items.Add("Complete Paid")
                CmbApprovalStatus.Items.Add("Rejected")
            End If
        Next
        CustomerNumber = thisIssuedata.getvalue("Customer")
    End Sub
    Private Sub setuparray()
        Dim i As Integer

        For x As Integer = 0 To (mydata.Length - 1)
            mydata(x) = New element

        Next

        i = 0
        mydata(i).txtbox = Me.cmbStatus
        mydata(i).dbfeild = "Status"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 1
        mydata(i).txtbox = Me.CmbTypeOfWork
        mydata(i).dbfeild = "Type"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 2
        mydata(i).txtbox = Me.Cmbpaymenttype
        mydata(i).dbfeild = "Payment"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 3
        mydata(i).txtbox = Me.CmbApprovalStatus
        mydata(i).dbfeild = "Approval"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 4
        mydata(i).txtbox = Me.txtDesc
        mydata(i).dbfeild = "Description"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 5
        mydata(i).txtbox = Me.txtAssignedTo
        mydata(i).dbfeild = "Assigned"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""


        i = 6
        mydata(i).txtbox = Me.txtQuoteRate
        mydata(i).dbfeild = "QuoteRate"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 7
        mydata(i).txtbox = Me.txtQuoteParts
        mydata(i).dbfeild = "QuoteParts"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 8
        mydata(i).txtbox = Me.txtQuoteSublet
        mydata(i).dbfeild = "QuoteSublet"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 9
        mydata(i).txtbox = Me.txtsubletcost
        mydata(i).dbfeild = "SubletCost"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 10
        mydata(i).txtbox = Me.txtbill
        mydata(i).dbfeild = "Bill"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 11
        mydata(i).txtbox = Me.txtbillablehrs
        mydata(i).dbfeild = "BillableHrs"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 12
        mydata(i).txtbox = Me.txtbilledhrs
        mydata(i).dbfeild = "BilledHrs"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 13
        mydata(i).txtbox = Me.txtsubletmarkup
        mydata(i).dbfeild = "SubletMarkup"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 14
        mydata(i).txtbox = Me.txtQuoteHours
        mydata(i).dbfeild = "QuoteHrs"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""


        i = 15
        mydata(i).txtbox = Me.txtwarrantynotes
        mydata(i).dbfeild = "warrantynotes"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""


        i = 16
        mydata(i).txtbox = Me.txtservicepackage
        mydata(i).dbfeild = "servicepkg"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 17
        mydata(i).txtbox = Me.txtservicepackageprice
        mydata(i).dbfeild = "servicepkgprice"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 18
        mydata(i).txtbox = Me.txtworkdone
        mydata(i).dbfeild = "Workdone"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 19
        mydata(i).txtbox = Me.CmbBillCustomer
        mydata(i).dbfeild = "billcustomer"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 20
        mydata(i).txtbox = Me.txtRequestedAmt
        mydata(i).dbfeild = "RequestedAmt"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 21
        mydata(i).txtbox = Me.txtRequestedHrs
        mydata(i).dbfeild = "RequestedHrs"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 22
        mydata(i).txtbox = Me.txtApprovedAmt
        mydata(i).dbfeild = "ApprovedAmt"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 23
        mydata(i).txtbox = Me.txtApprovedHrs
        mydata(i).dbfeild = "ApprovedHrs"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 24
        mydata(i).txtbox = Me.txtCreditNum
        mydata(i).dbfeild = "CreditNum"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 25
        mydata(i).txtbox = Me.cboWarrantyType
        mydata(i).dbfeild = "WarrantyType"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 26
        mydata(i).txtbox = Me.txtWSubletName
        mydata(i).dbfeild = "WarrantySubletName"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 27
        mydata(i).txtbox = Me.txtWSubletEst
        mydata(i).dbfeild = "WarrantySubletAmount"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 28
        mydata(i).txtbox = Me.txtWSubletAppr
        mydata(i).dbfeild = "WarrantySubletApproved"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 29
        mydata(i).txtbox = Me.txtWSubletInv
        mydata(i).dbfeild = "WarrantySubletInvoiceNum"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 30
        mydata(i).txtbox = Me.txtClaimNum
        mydata(i).dbfeild = "WarrantyClaimNum"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 31
        mydata(i).txtbox = Me.txtQuoteShipping
        mydata(i).dbfeild = "QuoteShipping"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 32
        mydata(i).txtbox = Me.txtQuoteDetailing
        mydata(i).dbfeild = "QuoteDetailing"
        mydata(i).dbtable = "Issue"
        mydata(i).changed = False
        mydata(i).value = ""


    End Sub
    Private Sub WritetoScreen()
        'Write the loaded info to the screen
        For x As Integer = 0 To 3
            If mydata(x).value <> "" Then mydata(x).txtbox.SelectedItem = mydata(x).value
        Next
        For x As Integer = 4 To 5
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
        Next
        If mydata(15).value <> "" Then mydata(15).txtbox.text = mydata(15).value
        If mydata(16).value <> "" Then mydata(16).txtbox.text = mydata(16).value
        For x As Integer = 5 To 10
            If mydata(x).value <> "" Then mydata(x).txtbox.text = Format((mydata(x).value), "Currency")
        Next
        If mydata(17).value <> "" Then
            mydata(17).txtbox.text = Format((mydata(17).value), "Currency")
        End If

        For x As Integer = 11 To 14
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
        Next
        mydata(18).txtbox.text = mydata(18).value
        If mydata(19).value <> "" Then mydata(19).txtbox.SelectedItem = mydata(19).value
        For x As Integer = 20 To 24
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
        Next
        If mydata(25).value <> "" Then mydata(25).txtbox.SelectedItem = mydata(25).value
        For x As Integer = 26 To 30
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
        Next
        For x As Integer = 31 To 32
            If mydata(x).value <> "" Then mydata(x).txtbox.text = Format((mydata(x).value), "Currency")
        Next

        showhide2()
        Getnewpkgsdata()
        Getnewpartsdata()
        Getnewworklogdata()
        GetNewPicsData()
        domath()
    End Sub
    Private Sub showhide()
        Try
            'Get rid of pictures tab for now
            ' TabControl1.TabPages.Remove(TabControl1.TabPages.Item("tbPics"))

        Catch ex As Exception

        End Try

        'If persistant.myuserLEVEL < 0 Then
        '    txtDesc.ReadOnly = True
        '    txtworkdone.ReadOnly = True

        'End If

        Cmbpaymenttype.Enabled = True
        'allow these to change if a shop supervisor or above.
        If persistant.myuserLEVEL > 1 Then
            CmbTypeOfWork.Enabled = True
            cmbStatus.Enabled = True
            CmbApprovalStatus.Enabled = True
        End If

        'Show the Quote Screen
        If Cmbpaymenttype.SelectedItem = "Quote Requested" And cmbStatus.SelectedItem <> "Complete" Then
            'TabControl1.TabPages.Item(0).Show()
            If CmbApprovalStatus.SelectedItem = "Pending" Then
                btnquoteapproved.Visible = True
                BtnquoteDeclined.Visible = True
            Else
                quotepanel.Enabled = False
                btnquoteapproved.Visible = False
                BtnquoteDeclined.Visible = False
            End If
        Else
            ' TabControl1.TabPages.Item(0).Hide()
            Try
                TabControl1.TabPages.Remove(TabControl1.TabPages.Item("tbQuote"))

            Catch ex As Exception

            End Try

        End If

        'Hide the sublet Screen
        If CmbTypeOfWork.SelectedItem <> "SUBLET" And CmbTypeOfWork.SelectedItem <> "Gelcoat" And Cmbpaymenttype.SelectedItem <> "WARRANTY" Then
            Try
                TabControl1.TabPages.Remove(TabControl1.TabPages.Item("tbSublet"))
            Catch ex As Exception
            End Try
        End If

        'Show the Warranty Screen
        If Cmbpaymenttype.SelectedItem = "WARRANTY" Then
            ' TabControl1.TabPages.Item(1).Show()
            pnlWarrantyHours.Visible = True
            pnlWarantySublet.Visible = True
            If persistant.myuserLEVEL > 0 Then
                txtRequestedHrs.Enabled = True
                txtRequestedAmt.Enabled = True
                txtApprovedHrs.Enabled = True
                txtApprovedAmt.Enabled = True
            End If
            If CmbApprovalStatus.SelectedItem = "Pending" Then
                BtnWarrantyApproved.Visible = True
                BtnWarrantyDeclined.Visible = True
            Else
                BtnWarrantyApproved.Visible = False
                BtnWarrantyDeclined.Visible = False
            End If
        Else
            Try
                TabControl1.TabPages.Remove(TabControl1.TabPages.Item("tbWarranty"))
            Catch ex As Exception
            End Try
            Try
                TabControl1.TabPages.Remove(TabControl1.TabPages.Item("tbPics"))
            Catch ex As Exception
            End Try
        End If


        'Show the Std Service Screen
        If CmbTypeOfWork.SelectedItem = "Standard Service" Then
            txtbillablehrs.Enabled = False
            Try
                TabControl1.TabPages.Remove(TabControl1.TabPages.Item("tbSublet"))
            Catch ex As Exception

            End Try
            txtQuoteRate.Visible = False
            Label25.Visible = False
            Label8.Visible = False
            txtsublettotal.Visible = False
            Label41.Visible = False
        Else
            Try
                TabControl1.TabPages.Remove(TabControl1.TabPages.Item("tbService"))
            Catch ex As Exception
            End Try
        End If


        If persistant.myuserLEVEL = 1 Or persistant.myuserLEVEL > 7 Then
            txtbillablehrs.Enabled = True
            txtQuoteRate.Enabled = True
            BtnSubmitted.Visible = True
            CmbBillCustomer.Enabled = True

        Else
            BtnSubmitted.Visible = False
            btnAssign.Enabled = False
            BtnWarrantyApproved.Enabled = False
            BtnWarrantyDeclined.Enabled = False
            CmbBillCustomer.Enabled = False

        End If

        'If its a new Issue don't allow call logging or work perfomed until its added
        If IssueNumber = 0 Then
            Try
                btnAssign.Visible = False
                btncompleted.Visible = False
                Button2.Visible = False

                TabControl1.TabPages.Remove(TabControl1.TabPages.Item("tbWork"))
                btnLogCall.Visible = False
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub showhide2()
        If Cmbpaymenttype.SelectedItem = "WARRANTY" And CmbApprovalStatus.SelectedItem = "Pending" Then
            BtnSubmitted.Enabled = True
        Else
            BtnSubmitted.Enabled = False
        End If
        'Show the assign button or who should do the job
        If Me.txtAssignedTo.Text = "" Then
            txtAssignedTo.Visible = False
            Label5.Visible = False
        Else
            txtAssignedTo.Visible = True
            Label5.Visible = True
        End If
        'Warning
        If CmbApprovalStatus.SelectedItem = "Denied" Then

            labelMessage.Visible = True
            Panel1.BackColor = System.Drawing.Color.Red

        End If
        If cmbStatus.SelectedItem = "Completed" Then
            btncompleted.Visible = False
        End If

    End Sub
    Private Sub domath()
        'Fill in the calculated values
        Dim quotelab, quotetotal As Decimal

        Try
            quotetotal = 0
            quotelab = 0
            If mydata(6).value <> "" And mydata(14).value <> "" Then
                quotelab = CDec(mydata(6).value) * CDec(mydata(14).value)
                quotetotal = CDec(mydata(6).value) * CDec(mydata(14).value) * (1 + shopSuppliesRate)
            End If
            'Quote Labour
            txtQuoteLabour.Text = Format(quotelab.ToString, "Currency")
            txtshopsupplies.Text = Format((quotelab * shopSuppliesRate).ToString, "Currency")

            'Total Quote
            If mydata(7).value <> "" Then quotetotal = quotetotal + CDec(mydata(7).value)
            If mydata(8).value <> "" Then quotetotal = quotetotal + CDec(mydata(8).value)
            If mydata(31).value <> "" Then quotetotal = quotetotal + CDec(mydata(31).value)
            If mydata(32).value <> "" Then quotetotal = quotetotal + CDec(mydata(32).value)
            txtQuoteTotal.Text = Format(quotetotal.ToString, "Currency")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        'Try
        '    'Quote Supplies
        '    quotelab = 0
        '    If mydata(6).value <> "" And mydata(14).value <> "" Then quotelab = CDec(mydata(6).value) * CDec(mydata(14).value)
        '    txtshopsupplies.Text = Format((quotelab * 0.07).ToString, "Currency")

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        'Try
        '    'If mydata(6).value <> "" And mydata(14).value <> "" Then quotetotal = CDec(mydata(6).value) * CDec(mydata(14).value) * 1.07

        '    '  quotetotal = quotetotal * 1.05

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        Dim subletbill As Decimal = 0
        Try
            'Sublet Total
            If mydata(9).value <> "" And mydata(13).value <> "" Then
                subletbill = CDec(mydata(9).value) * CDec(mydata(13).value)
                txtsubletbilled.Text = Format(subletbill.ToString, "Currency")
            Else
                txtsubletbilled.Text = ""
            End If
            txtsublettotal.Text = txtsubletbilled.Text

        Catch ex As Exception

        End Try
        'Parts Total
        Dim partstotal As Decimal = 0
        Dim partpricestr As String
        Dim partprice As Decimal
        Dim partquantity As Decimal
        If Me.DVParts.RowCount > 0 Then
            Try
                For x As Integer = 0 To DVParts.RowCount - 1
                    partpricestr = Partsdatatable.Rows(x).Item("Price").ToString
                    partpricestr = Replace(partpricestr, "$", "")
                    partpricestr = Replace(partpricestr, ",", "")
                    partpricestr = Replace(partpricestr, "(", "-")
                    partpricestr = Replace(partpricestr, ")", "")
                    partprice = Val(partpricestr)
                    partquantity = CDec(Partsdatatable.Rows(x).Item("Quantity").ToString)
                    partstotal = partstotal + partprice * partquantity
                Next
                txtpartstotal.Text = Format(partstotal.ToString, "Currency")

            Catch ex As Exception
            End Try


        End If
        If txtpartstotal.Text = "" Then
            txtpartstotal.Text = "$0.00"
        End If

        txtpartsclone.Text = txtpartstotal.Text
        'Labour Total
        Dim labour As Decimal = 0
        Try
            If Cmbpaymenttype.SelectedItem = "WARRANTY" And CmbBillCustomer.SelectedItem = "NO" Then
                If mydata(6).value <> "" And mydata(23).value <> "" Then
                    labour = CDec(mydata(6).value) * CDec(mydata(23).value)
                    Me.txtlabourtotal.Text = Format(labour.ToString, "Currency")
                Else
                    Me.txtlabourtotal.Text = ""
                End If
            Else
                If mydata(6).value <> "" And mydata(11).value <> "" Then
                    labour = CDec(mydata(6).value) * CDec(mydata(11).value)
                    Me.txtlabourtotal.Text = Format(labour.ToString, "Currency")
                Else
                    Me.txtlabourtotal.Text = ""
                End If
            End If
        Catch ex As Exception
            Me.txtlabourtotal.Text = ""
        End Try
        'Total billed
        Dim total As Decimal
        total = subletbill + partstotal + labour
        'txtbillingshopsupplies.Text = Format((labour * 0.07).ToString, "Currency")
        'txtbill.Text = Format((total + (labour * 0.07)).ToString, "Currency")
        txtbillingshopsupplies.Text = Format((labour * shopSuppliesRate).ToString, "Currency")
        txtbill.Text = Format((total + (labour * shopSuppliesRate)).ToString, "Currency")

        Dim envirofee As Decimal
        If txtbillablehrs.Text = "" Then
            txtbillablehrs.Text = 0
        End If

        envirofee = 0

        'Std Service Bill
        Try
            'Std service Supplies
            If mydata(17).value <> "" Then
                txtstdservicesupplies.Text = Format(((CDec(mydata(17).value)) + envirofee).ToString, "Currency")
            Else
                txtstdservicesupplies.Text = ""
            End If
        Catch ex As Exception
        End Try
        'Std service parts
        txtstdServiceParts.Text = Format(partstotal.ToString, "Currency")
        'Total std service
        Try
            If mydata(17).value <> "" Then
                txtsrdservicetotal.Text = Format((((CDec(mydata(17).value)) + envirofee) + partstotal).ToString, "Currency")
            Else
                txtsrdservicetotal.Text = ""
            End If
        Catch ex As Exception
        End Try

        'Add up real time
        Dim timeworked As Decimal = 0
        If Me.DVWorkDone.RowCount > 0 Then
            Try
                For x As Integer = 0 To DVWorkDone.RowCount - 1
                    timeworked = timeworked + CDec(Workdatatable.Rows(x).Item("TimeWorked").ToString)
                Next
                txtbilledhrs.Text = timeworked.ToString
                mydata(12).value = timeworked.ToString
                mydata(12).changed = True
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        If timeworked > 0 Then
            If cmbStatus.SelectedItem = "Assigned" Then
                cmbStatus.SelectedItem = "In Progress"
                cmbchanged(cmbStatus)
            End If
        End If

        'Use std service pricing model if applicable
        If Cmbpaymenttype.SelectedItem = "Standard Service" Then
            txtbill.Text = txtsrdservicetotal.Text
            txtlabourtotal.Text = txtservicepackageprice.Text
            txtbillingshopsupplies.Text = txtstdservicesupplies.Text
        End If

        'Pre-paid and rigs
        If (Cmbpaymenttype.SelectedItem = "WARRANTY" And CmbBillCustomer.SelectedItem = "NO") Or Cmbpaymenttype.SelectedItem = "Standard Service (PREPAID)" Or Cmbpaymenttype.SelectedItem = "Rig - Sterndrive" Or Cmbpaymenttype.SelectedItem = "Rig - Outboard" Or Cmbpaymenttype.SelectedItem = "Rig - Jet" Or Cmbpaymenttype.SelectedItem = "Rig - V-Drive" Then
            txtbill.Text = "$0.00"
        End If
        moneychanged(txtbill)

    End Sub
    Private Sub save()
        'Me.txtAssignedTo.Focus()
        Me.txtDesc.Focus()

        ' Me.txtAssignedTo.Select()
        'Me.txtDesc.Select()

        For x As Integer = 0 To mydatacount
            mydata(x).value = Replace(mydata(x).value, "'", "\'")
            mydata(x).value = Replace(mydata(x).value, ";", "\;")
        Next

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring

        cmd.Connection = conn

        If IssueNumber > 0 Then
            'UPDATE Issue
            Dim cmdstring As String = "Update ServiceIssue Set "

            For x As Integer = 0 To mydatacount
                If mydata(x).dbtable = "Issue" And mydata(x).changed = True Then
                    cmdstring = cmdstring + mydata(x).dbfeild + " = '" + mydata(x).value + "', "
                End If
            Next

            cmdstring = cmdstring + "Customer = '" + CustomerNumber.ToString + "'"
            cmdstring = cmdstring + " Where Issue = '" + IssueNumber.ToString + "'"
            cmd.CommandText = cmdstring
            Debug.WriteLine(cmdstring)
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()
        Else
            'INSERT NEW 
            If dontsave = False Then
                Dim cmdstring As String = "Insert INTO ServiceIssue Set "

                For x As Integer = 0 To mydatacount
                    If mydata(x).dbtable = "Issue" And mydata(x).changed = True Then
                        cmdstring = cmdstring + mydata(x).dbfeild + " = '" + mydata(x).value + "', "
                    End If
                Next

                cmdstring = cmdstring + "Customer = '" + CustomerNumber.ToString + "'"
                cmd.CommandText = cmdstring
                Debug.WriteLine(cmdstring)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    cmd.CommandText = "Select MAX(Issue) from ServiceIssue"
                    IssueNumber = CInt(cmd.ExecuteScalar)
                    conn.Close()
                    WritetoScreen()
                Catch ex As MySqlException
                    MessageBox.Show(ex.Message)
                End Try
                conn.Dispose()
            End If

        End If

        For x As Integer = 0 To mydatacount
            mydata(x).value = Replace(mydata(x).value, "\'", "'")
            mydata(x).value = Replace(mydata(x).value, "\;", ";")
            mydata(x).changed = False

        Next
        If cmbStatus.SelectedItem <> "Not Completed" And cmbStatus.SelectedItem <> "Completed" Then
            funcs.updateWOPrioritylist(WONumber.ToString, Store, persistant.myconnstring)
        End If

        ' DialogResult = Windows.Forms.DialogResult.OK

    End Sub
    Private Sub decimalboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsubletmarkup.Leave, txtbillablehrs.Leave, txtQuoteHours.Leave
        decimalchanged(sender)
        WritetoScreen()
    End Sub
    Private Sub moneyboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsubletcost.Leave, txtsubletbilled.Leave, TabControl1.Leave, txtQuoteRate.Leave, txtbill.Leave, txtQuoteParts.Leave, txtQuoteSublet.Leave, txtRequestedAmt.Leave, txtApprovedAmt.Leave, txtWSubletEst.Leave, txtQuoteDetailing.Leave, txtQuoteShipping.Leave
        moneychanged(sender)
        WritetoScreen()
    End Sub
    Private Sub moneyboxleave2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtservicepackageprice.TextChanged, txtbill.TextChanged
        moneychanged(sender)
    End Sub
    Private Sub CmbApprovalStatus_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbTypeOfWork.SelectionChangeCommitted, cmbStatus.SelectionChangeCommitted, Cmbpaymenttype.SelectionChangeCommitted, CmbApprovalStatus.SelectionChangeCommitted, CmbBillCustomer.SelectionChangeCommitted, cboWarrantyType.SelectionChangeCommitted
        cmbchanged(sender)
    End Sub

    Private Sub stringboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtworkdone.Leave, txtservicepackage.TextChanged, txtDesc.Leave, txtwarrantynotes.Leave, txtWSubletName.Leave, txtWSubletAppr.Leave, txtWSubletInv.Leave, txtRequestedHrs.Leave, txtApprovedHrs.Leave, txtCreditNum.Leave, txtClaimNum.Leave
        stringchanged(sender)
    End Sub

    Private Sub FrmIssue_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosing
        'set the focus to two controls
        'this will force the update of all field changed values so that the last change get updated as well.
        'txtDesc.Focus()
        'txtworkdone.Focus()
        save()
        DialogResult = Windows.Forms.DialogResult.OK

    End Sub
    'Private Sub btndone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    save()
    '    Me.Close()

    'End Sub

    Private Sub btnLogCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogCall.Click
        Dim logcall As New frmCallLog
        logcall.persistant = persistant
        logcall.CustNumber = CustomerNumber
        logcall.ShowDialog()
        WritetoScreen()
    End Sub

    Private Sub btnaddpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddpart.Click
        Dim newP As New frmAddNewPart
        newP.persistant = persistant
        newP.IssueNum = IssueNumber
        newP.WONum = WONumber
        newP.CustNum = CustomerNumber
        newP.Store = Store
        newP.OrderMe = False
        newP.PartID = 0
        newP.ShowDialog()
        WritetoScreen()

        'Dim addpart As New frmaddpart
        'addpart.persistant = persistant
        'addpart.IssueNumber = IssueNumber
        'addpart.Number = 0
        'addpart.ShowDialog()
        'WritetoScreen()
    End Sub

    Private Sub btnOrderPart_Click(sender As System.Object, e As System.EventArgs) Handles btnOrderPart.Click
        Dim newPartOrder As New frmPartsOrder
        newPartOrder.persistant = persistant
        newPartOrder.CustomerNumber = CustomerNumber
        newPartOrder.IssueNumber = IssueNumber
        newPartOrder.WONumber = WONumber
        newPartOrder.Store = Store
        newPartOrder.ShowDialog()
        WritetoScreen()
    End Sub

    Private Sub btnquoteapproved_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnquoteapproved.Click
        If Not cmbStatus.SelectedItem = "Complete" Then
            If Me.txtAssignedTo.Text = "" Then
                cmbStatus.SelectedItem = "To Be Assigned"
            Else
                cmbStatus.SelectedItem = "Assigned"
            End If
            cmbchanged(cmbStatus)

            txtbillablehrs.Text = txtQuoteHours.Text
            decimalchanged(txtbillablehrs)

        End If

        CmbApprovalStatus.SelectedItem = "Approved"
        cmbchanged(CmbApprovalStatus)
        Cmbpaymenttype.SelectedItem = "Quote Approved"
        cmbchanged(Cmbpaymenttype)
        'update any parts set as quote = To be Ordered
        Dim conn As New MySqlConnection
        conn.ConnectionString = persistant.myconnstring
        Dim cmd As New MySqlCommand
        For i As Integer = 0 To DVParts.RowCount - 1
            If (Partsdatatable.Rows(i).Item("Status").ToString() = "Quote") Then
                Dim cmdstring As String = "Update ServiceParts Set Status = 'To Be Ordered' Where Number = '" & Partsdatatable.Rows(i).Item("Number").ToString & "'"

                'conn.Open()
                cmd.Connection = conn
                cmd.CommandText = cmdstring
                Debug.WriteLine(cmdstring)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()
                Catch ex As MySqlException
                    MessageBox.Show(ex.Message)
                End Try
                conn.Dispose()

            End If
        Next
        Getnewpartsdata()

        WritetoScreen()
    End Sub

    Private Sub BtnquoteDeclined_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnquoteDeclined.Click
        cmbStatus.SelectedItem = "Completed"
        CmbApprovalStatus.SelectedItem = "Denied"
        cmbchanged(cmbStatus)
        cmbchanged(CmbApprovalStatus)
        txtbillablehrs.Text = txtbilledhrs.Text
        decimalchanged(txtbillablehrs)

        WritetoScreen()
    End Sub
    Private Sub BtnWarrantyApproved_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnWarrantyApproved.Click
        If Me.txtAssignedTo.Text = "" Then
            cmbStatus.SelectedItem = "To Be Assigned"
        Else
            cmbStatus.SelectedItem = "Assigned"
        End If
        CmbApprovalStatus.SelectedItem = "Approved"
        cmbchanged(cmbStatus)
        cmbchanged(CmbApprovalStatus)
        WritetoScreen()
    End Sub

    Private Sub BtnWarrantyDeclined_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnWarrantyDeclined.Click
        cmbStatus.SelectedItem = "Completed"
        CmbApprovalStatus.SelectedItem = "Denied"
        cmbchanged(cmbStatus)
        cmbchanged(CmbApprovalStatus)
        txtbillablehrs.Text = txtbilledhrs.Text
        decimalchanged(txtbillablehrs)
        WritetoScreen()
    End Sub

    Private Sub btnAssign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssign.Click
        Dim pick As New frmSalesman
        pick.persistant = persistant
        pick.filter = "usergroup < 0" 'AND store = '" + wostorelong + "'"
        pick.ShowDialog()
        'Dim wostorelong As String = persistant.getvalue(persistant.tbl_location, "Store", "CODE = '" + Store + "'", 0)
        'If wostorelong = "Shipwreck Edmonton" Or wostorelong = "Atlantis" Then
        '    pick.filter = "usergroup < 0 AND ( store = 'Atlantis' OR store = 'Shipwreck Edmonton' )"
        '    pick.ShowDialog()
        'Else
        '    pick.filter = "usergroup < 0 AND store = '" + wostorelong + "'"
        '    pick.ShowDialog()
        'End If
        If persistant.temppass = "None" Then
            txtAssignedTo.Text = ""
            cmbStatus.SelectedItem = "To Be Assigned"
        Else
            txtAssignedTo.Text = persistant.getvalue(persistant.tbl_users, "user", "name = '" + persistant.temppass + "'", 0)
            If cmbStatus.SelectedItem = "To Be Assigned" Then
                If Cmbpaymenttype.SelectedItem = "Quote Requested" Then
                    'Yep quote it
                    cmbStatus.SelectedItem = "To Be Quoted"
                Else
                    'No, is it warranty?
                    If Cmbpaymenttype.SelectedItem = "WARRANTY" Then
                        'yes it is, approved?
                        If CmbApprovalStatus.SelectedItem = "Approved" Then
                            cmbStatus.SelectedItem = "Assigned"
                        Else
                            'warranty but not approved
                            cmbStatus.SelectedItem = "Warranty Pending"
                        End If
                    Else
                        'No quote needed and no warranty and assigned so get to work
                        cmbStatus.SelectedItem = "Assigned"
                    End If
                End If
            End If
            stringchanged(txtAssignedTo)
            cmbchanged(cmbStatus)

            WritetoScreen()
        End If
    End Sub

    Private Sub LogWork(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim logwork As New frmLogWork
        logwork.persistant = persistant
        logwork.Customer = CustomerNumber
        logwork.Issue = IssueNumber
        logwork.tech = txtAssignedTo.Text
        logwork.LogNumber = 0
        logwork.ShowDialog()
        WritetoScreen()
        Label1.Select()
    End Sub

    Private Sub cboWarrantyType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWarrantyType.SelectedIndexChanged
        If Store = "EDM" Then
            Select Case cboWarrantyType.SelectedItem
                'Case "Mercury", "Sea Ray"
                '    txtQuoteRate.Text = "$120"
                Case "Midwest"
                    txtQuoteRate.Text = "$70"
                Case "Karavan"
                    txtQuoteRate.Text = "$90"
                Case "Bayliner"
                    txtQuoteRate.Text = "$102"
                Case Else
                    txtQuoteRate.Text = "$128"
            End Select
            moneychanged(txtQuoteRate)

        End If

    End Sub

    Private Sub btncompleted_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncompleted.Click
        cmbStatus.SelectedItem = "Completed"
        cmbchanged(cmbStatus)
        WritetoScreen()
    End Sub

    Private Sub DVServicePkg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DVServicePkg.CellContentClick
        Dim xRow As Integer
        xRow = e.RowIndex
        If e.ColumnIndex = 0 Then
            txtservicepackage.Text = Pkgsdatatable.Rows(xRow).Item("Package").ToString
            txtservicepackageprice.Text = Pkgsdatatable.Rows(xRow).Item("Price").ToString
            'If txtDesc.Text = "" Then
            txtDesc.Text = txtservicepackage.Text
            'End If
            txtbillablehrs.Text = Pkgsdatatable.Rows(xRow).Item("BillableHrs").ToString
        End If

        moneychanged(txtservicepackageprice)
        stringchanged(txtbillablehrs)
        stringchanged(txtDesc)
        WritetoScreen()
    End Sub
    Private Sub DVParts_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DVParts.CellContentClick
        Dim xRow As Integer
        Dim xnum As String
        ''***************************
        'If the part is an ordered part you may not want to delete it if it hasn't been Picked Up or In Stock
        xRow = e.RowIndex
        If e.ColumnIndex = 0 Then
            xnum = Partsdatatable.Rows(xRow).Item("Number").ToString
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            cmd.Connection = conn
            'Dim cmdstring As String = "DELETE from ServiceParts where Number = '" + xnum + "'"
            Dim cmdstring As String = "UPDATE ServiceParts Set IssueNumber = 0 where Number = '" + xnum + "'"
            cmd.CommandText = cmdstring
            Debug.WriteLine(cmdstring)
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()
            WritetoScreen()
        End If
        If e.ColumnIndex = 1 Then
            xnum = Partsdatatable.Rows(xRow).Item("Number").ToString

            Dim editpart As New frmAddNewPart
            editpart.persistant = persistant
            editpart.PartID = xnum
            editpart.IssueNum = IssueNumber
            editpart.ShowDialog()
            WritetoScreen()
        End If
    End Sub
    Private Sub DVPictures_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DVPictures.CellContentClick
        Dim xRow As Integer
        xRow = e.RowIndex
        If DVPictures.Columns(e.ColumnIndex).Name = "RP" Then 'remove pic
            Dim pid As Integer = PicsDataTable.Rows(xRow).Item("PicID")
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            cmd.Connection = conn
            'Dim cmdstring As String = "DELETE from ServicePics where PicID = " & pid
            cmd.CommandText = "DELETE from ServicePics where PicID = " & pid
            'Debug.WriteLine(cmdstring)
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()
            'refresh the picture list
            GetNewPicsData()
        End If
        If DVPictures.Columns(e.ColumnIndex).Name = "VP" Then 'view pic
            Dim viewPic As New frmViewPic
            viewPic.persistant = persistant
            viewPic.picID = PicsDataTable.Rows(xRow).Item("PicID")
            viewPic.picName = PicsDataTable.Rows(xRow).Item("PicName").ToString
            viewPic.ShowDialog()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim newWO As New FrmWO
        newWO.persistant = Me.persistant
        newWO.Store = Store
        newWO.WONumber = WONumber
        newWO.ShowDialog()
    End Sub

    Private Sub BtnSubmitted_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSubmitted.Click
        CmbApprovalStatus.SelectedItem = "Submitted"
        cmbchanged(CmbApprovalStatus)
        WritetoScreen()
    End Sub


    Private Sub DVWorkDone_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DVWorkDone.CellContentClick
        Dim xRow As Integer
        Dim XNum As Integer
        If DVWorkDone.Columns(e.ColumnIndex).Name = "View" Then
            xRow = e.RowIndex
            XNum = Workdatatable.Rows(xRow).Item("Number").ToString
            Dim logwork As New frmLogWork
            logwork.LogNumber = XNum
            logwork.persistant = persistant
            logwork.ShowDialog()
            WritetoScreen()
            Label1.Select()
        End If
    End Sub

    Private Sub CmbPkgType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbPkgType.SelectedIndexChanged
        Getnewpkgsdata()
    End Sub

    Private Sub CopyActualtoBilled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyActualtoBilled.Click
        txtbillablehrs.Text = txtbilledhrs.Text
        stringchanged(txtbillablehrs)
    End Sub

    Private Sub Btnreactivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnreactivate.Click
        If txtAssignedTo.Text = "" Then
            cmbStatus.SelectedItem = "To Be Assigned"
        Else
            If Cmbpaymenttype.SelectedItem = "Quote Requested" Then
                'Yep quote it
                If CmbApprovalStatus.SelectedItem = "Pending" Then
                    cmbStatus.SelectedItem = "To Be Quoted"
                Else
                    cmbStatus.SelectedItem = "Assigned"
                End If
            Else
                'No, is it warranty?
                If Cmbpaymenttype.SelectedItem = "WARRANTY" Then
                    'yes it is, approved?
                    If CmbApprovalStatus.SelectedItem = "Approved" Then
                        cmbStatus.SelectedItem = "Assigned"
                    Else
                        'warranty but not approved
                        cmbStatus.SelectedItem = "Warranty Pending"
                    End If
                Else
                    'No quote needed and no warranty and assigned so get to work
                    cmbStatus.SelectedItem = "Assigned"
                End If
            End If
        End If
        cmbchanged(cmbStatus)
        WritetoScreen()
        save()

    End Sub

    Private Sub btnUploadNewPics_Click(sender As System.Object, e As System.EventArgs) Handles btnUploadNewPics.Click
        'prompt for file name
        OpenFileDialog1.Filter = "Png files (*.png)|*.png|Jpeg files (*.jpg)|*.jpg"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fp As String = OpenFileDialog1.FileName
            Dim fiTemp As FileInfo = New FileInfo(fp)
            Dim fn As String = fiTemp.Name

            ' Open the Image file for Read
            Dim imgStream As FileStream = fiTemp.OpenRead

            ' Create a Byte array the size of the image file for the Read methods buffer() byte array
            Dim imgData(imgStream.Length) As Byte
            imgStream.Read(imgData, 0, fiTemp.Length)
            imgStream.Dispose()

            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            cmd.Connection = conn

            Dim img As MySqlParameter = New MySqlParameter("?Image", MySqlDbType.LongBlob)
            img.Direction = ParameterDirection.InputOutput
            img.Value = imgData
            cmd.Parameters.Add(img)

            cmd.CommandText = "Insert Into ServicePics (IssueNumber,Pic,PicName) " & _
                                    "Values(" & IssueNumber & ",?Image,'" & fn & "')"


            ' Debug.WriteLine(cmdstring)
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()

            'refresh the picture list
            GetNewPicsData()
        End If

    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrintQuote_Click(sender As System.Object, e As System.EventArgs) Handles btnPrintQuote.Click

        'cust data


        Dim Customerdata As New CustData
        Customerdata.CustomerNumber = CustomerNumber
        Customerdata.myconnstring = persistant.myconnstring
        Customerdata.Download()
        Dim custName As String = Customerdata.getvalue("Name")
        Dim custHomePhone As String = Customerdata.getvalue("Phone1")
        Dim custWorkPhone As String = Customerdata.getvalue("Phone2")
        Dim boatMakeModel As String = Customerdata.getvalue("BMake") & " / " & Customerdata.getvalue("BModel")
        Dim boatSerial As String = Customerdata.getvalue("BSerial")
        Dim motorMakeModel As String = Customerdata.getvalue("MMake") & " / " & Customerdata.getvalue("MModel")
        Dim motorSerial As String = Customerdata.getvalue("MSerial")
        Dim trailerMakeModel As String = Customerdata.getvalue("TMake") & " / " & Customerdata.getvalue("TModel")
        Dim trailerSerial As String = Customerdata.getvalue("TSerial")


        'Dim posx,
        Dim posy, align As Integer
        'Dim text, txtboxname As String
        Dim size As Integer
        Dim inputfile, savelocation As String
        'Dim imagex As Image
        'Dim mybosstorecode As String = persistant.getvalue(persistant.tbl_location, "Code", "store = '" + ListBox2.SelectedItem + "'", 0)
        Dim addr As String = persistant.installdir & "\resources\images\" & Store & "Addr.png"
        inputfile = persistant.installdir & "\resources\contracts\BlankQuote.pdf"

        'Select Case Store
        '    Case "ATL"
        '        imagex = Image.GetInstance(persistant.installdir + "\resources\images\ATLlogo.PNG")
        '    Case "BTH"
        '        imagex = Image.GetInstance(persistant.installdir + "\resources\images\BTHlogo.PNG")
        '    Case "GAL"
        '        imagex = Image.GetInstance(persistant.installdir + "\resources\images\GALlogo.PNG")
        '    Case "OGO"
        '        imagex = Image.GetInstance(persistant.installdir + "\resources\images\OGOlogo.PNG")
        '    Case "REN"
        '        imagex = Image.GetInstance(persistant.installdir + "\resources\images\RENlogo.PNG")
        '    Case "EDM"
        '        imagex = Image.GetInstance(persistant.installdir + "\resources\images\EDMlogo.PNG")
        '    Case "SYL"
        '        imagex = Image.GetInstance(persistant.installdir + "\resources\images\SYLlogo.PNG")
        'End Select

        If Not Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO") Then Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\PO")
        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO\" + Store + "-Quote-" + WONumber.ToString + ".pdf"

        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        '   Try
        Dim reader As PdfReader = New PdfReader(inputfile)
        Dim doc As Document = New Document(reader.GetPageSize(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
        doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent
        doc.NewPage()
        Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)
        cb.AddTemplate(Import, 0, 0)
        'imagex.ScalePercent(65)
        'doc.Add(imagex)
        Dim im As Image
        im = Image.GetInstance(addr)
        im.ScalePercent(50)
        im.SetAbsolutePosition(0, 710)
        doc.Add(im)


        align = 0
        size = 15
        'WONumber
        If WONumber > 0 Then
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(2, WONumber.ToString, 582, 730, 0)
            cb.EndText()
        End If

        size = 12
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        'date
        'cb.ShowTextAligned(align, Format(DateTime.Now, "MMMM dd, yyyy"), 100, 700, 0)

        'Personal Data
        'Customer name
        cb.ShowTextAligned(align, custName, 100, 686, 0)

        'Ordered By
        'cb.ShowTextAligned(align, txtOrderedBy.Text, 375, 686, 0)

        'customer home phone
        cb.ShowTextAligned(align, custHomePhone, 100, 671, 0)

        'customer work phone
        cb.ShowTextAligned(align, custWorkPhone, 375, 671, 0)

        'boat make model
        cb.ShowTextAligned(align, boatMakeModel, 100, 656, 0)

        'boat serial
        cb.ShowTextAligned(align, boatSerial, 375, 656, 0)

        'motor make model
        cb.ShowTextAligned(align, motorMakeModel, 100, 642, 0)

        'motor serial
        cb.ShowTextAligned(align, motorSerial, 375, 642, 0)

        'trailer make model
        cb.ShowTextAligned(align, trailerMakeModel, 100, 627, 0)

        'trailer serial
        cb.ShowTextAligned(align, trailerSerial, 375, 627, 0)

        posy = 532
        Dim startY As Double = 532
        Dim totalParts As Double = 0
        Dim q As Integer
        Dim p As Double

        For i As Integer = 0 To DVParts.RowCount - 1
            'in stock
            'If Not (PartsDataTable.Rows(i).Item("Status").ToString() = "To Be Ordered" Or
            '    PartsDataTable.Rows(i).Item("Status").ToString() = "Ordered") Then
            '    cb.ShowTextAligned(align, "X", 46, posy, 0)
            'End If
            'Order #
            '           cb.ShowTextAligned(1, PartsOrderID.ToString, 46, posy, 0)
            'Quantity
            q = Partsdatatable.Rows(i).Item("Quantity")
            cb.ShowTextAligned(2, FormatNumber(q, 0), 115, posy, 0)
            'Part #
            cb.ShowTextAligned(1, Partsdatatable.Rows(i).Item("PartNumber").ToString, 175, posy, 0)
            'Description
            cb.ShowTextAligned(0, Partsdatatable.Rows(i).Item("Part").ToString, 228, posy, 0)
            'Unit Price
            p = IIf(Partsdatatable.Rows(i).Item("Price") Is DBNull.Value, 0, Partsdatatable.Rows(i).Item("Price"))
            cb.ShowTextAligned(2, FormatNumber(p, 2), 500, posy, 0)
            'Extended Price
            cb.ShowTextAligned(2, FormatNumber(q * p, 2), 575, posy, 0)
            totalParts += (q * p)
            posy = startY - (((startY - 100) / 22) * (i + 1))
            'posy = Int(startY - ((i + 1) * 18.6))
        Next
        'parts total
        'cb.ShowTextAligned(2, "$" & FormatNumber(totalParts, 2), 575, 315, 0)
        cb.ShowTextAligned(2, "$" & FormatNumber(totalParts, 2), 575, 215, 0)

        Dim tempVal As Double = 0
        'estimated hours
        If IsNumeric(txtQuoteLabour.Text) Then
            tempVal = CDbl(txtQuoteLabour.Text)
        Else
            tempVal = 0
        End If
        'tempVal = IIf(IsNumeric(txtQuoteLabour.Text), CDbl(txtQuoteLabour.Text), 0)
        'cb.ShowTextAligned(2, FormatNumber(tempVal, 2), 575, 201, 0)
        cb.ShowTextAligned(2, FormatNumber(tempVal, 2), 575, 156, 0)
        'estimated parts
        If IsNumeric(txtQuoteParts.Text) Then
            tempVal = CDbl(txtQuoteParts.Text)
        Else
            tempVal = 0
        End If
        cb.ShowTextAligned(2, FormatNumber(tempVal, 2), 575, 142, 0)
        'estimate sublet
        If IsNumeric(txtQuoteSublet.Text) Then
            tempVal = CDbl(txtQuoteSublet.Text)
        Else
            tempVal = 0
        End If
        'tempVal = IIf(IsNumeric(txtQuoteSublet.Text), CDbl(txtQuoteSublet.Text), 0)
        cb.ShowTextAligned(2, FormatNumber(tempVal, 2), 575, 128, 0)
        'shipping
        If IsNumeric(txtQuoteShipping.Text) Then
            tempVal = CDbl(txtQuoteShipping.Text)
        Else
            tempVal = 0
        End If
        'tempVal = IIf(IsNumeric(txtQuoteShipping.Text), CDbl(txtQuoteShipping.Text), 0)
        cb.ShowTextAligned(2, FormatNumber(tempVal, 2), 575, 113, 0)
        'detailing
        If IsNumeric(txtQuoteDetailing.Text) Then
            tempVal = CDbl(txtQuoteDetailing.Text)
        Else
            tempVal = 0
        End If
        'tempVal = IIf(IsNumeric(txtQuoteDetailing.Text), CDbl(txtQuoteDetailing.Text), 0)
        cb.ShowTextAligned(2, FormatNumber(tempVal, 2), 575, 98, 0)
        'shop suplies
        If IsNumeric(txtshopsupplies.Text) Then
            tempVal = CDbl(txtshopsupplies.Text)
        Else
            tempVal = 0
        End If
        'tempVal = IIf(IsNumeric(txtshopsupplies.Text), CDbl(txtshopsupplies.Text), 0)
        cb.ShowTextAligned(2, FormatNumber(tempVal, 2), 575, 84, 0)
        'quote total
        If IsNumeric(txtQuoteTotal.Text) Then
            tempVal = CDbl(txtQuoteTotal.Text)
        Else
            tempVal = 0
        End If
        'tempVal = IIf(IsNumeric(txtQuoteTotal.Text), CDbl(txtQuoteTotal.Text), 0)
        cb.ShowTextAligned(2, FormatNumber(tempVal, 2), 575, 70, 0)

        'gst
        'Dim tax As Double = 0
        'If persistant.getvalue(persistant.tbl_location, "Prov", "Code = '" + Store + "'", 0) = "AB" Then
        '    tax = totalParts * GSTRate
        'Else
        '    tax = totalParts * (GSTRate + PSTRate)
        'End If
        'cb.ShowTextAligned(2, "$" & FormatNumber(tax, 2), 575, 70, 0)

        ''total
        'cb.ShowTextAligned(2, "$" & FormatNumber(totalParts + tax, 2), 575, 50, 0)

        cb.EndText()

        doc.Close()
        System.Diagnostics.Process.Start(savelocation)

    End Sub

    Private Sub btnFillFromParts_Click(sender As System.Object, e As System.EventArgs) Handles btnFillFromParts.Click
        txtQuoteParts.Focus()
        txtQuoteParts.Text = txtpartsclone.Text
        txtQuoteSublet.Focus()
    End Sub

    Private Sub cboQuantity_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboQuantity.SelectedIndexChanged

    End Sub

End Class