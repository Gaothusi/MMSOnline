
Imports MySql.Data.MySqlClient
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.Data
Imports MMSOnline.Functions
Imports System.IO

Public Class frmPartsOrder

    Public persistant As PreFetch
    Public PartsOrderID As Integer
    Public Store As String
    Public CustomerNumber As Integer
    Public WONumber As Integer
    Public IssueNumber As Integer
    Private funcs As New Functions

    Private Const mydatacount As Integer = 18
    Private mydata(mydatacount) As element
    Private Customerdata As New CustData
    Private thisPOdata As New PartsOrderData
    Private Callsdatatable As New DataTable
    Private Callsnewdata As New DataTable
    Private Callsbindingsource As New BindingSource
    Private Callsbinded As Boolean
    Private PartsDataTable As New DataTable
    Private PartsNewData As New DataTable
    Private PartsBindingSource As New BindingSource
    Private PartsBinded As Boolean
    Private Paymentdatatable As New DataTable
    Private Paymentnewdata As New DataTable
    Private Paymentbindingsource As New BindingSource
    Private Paymentbinded As Boolean
    'Private OrderedBy As String
    Private TotalOwed As Double = 0
    Private TotalPaid As Double = 0

    Private Sub FrmPartsOrder_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        save()
    End Sub
    Private Sub FrmPartsOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setuparray()
        'buildlists()
        'OrderedBy = persistant.getvalue(persistant.tbl_users, "name", "user = '" + persistant.myuserID + "'", 0)

        If PartsOrderID > 0 Then
            'LOAD EXISTING
            downloadPOdata()
            downloadcustdata()
            'GetNewPartsData()
            'GetNewCallData()
            'btnCustData.Visible = False
            'btnAddPart.Visible = True
        ElseIf WONumber > 0 Then
            'see if there is an incomplete order for this work order
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            Dim poID As Integer = 0

            'Dim adapter As New MySqlDataAdapter

            Dim SQL As String = "Select po.PartsOrderID From ServicePartsOrders as po, ServiceParts as p Where po.PartsOrderID = p.PartsOrderID and " & _
                " po.WorkOrderNum = '" & WONumber.ToString & "' and p.Status = 'To Be Ordered' "

            conn.ConnectionString = persistant.myconnstring
            Try
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = SQL
                poID = cmd.ExecuteScalar
                If poID > 0 Then
                    cmd.CommandText = "Select CustomerProfile From ServicePartsOrders " & _
                       "Where PartsOrderID = '" & poID.ToString & "' "
                    CustomerNumber = Val(cmd.ExecuteScalar.ToString)
                End If
                conn.Close()

            Catch ex As Exception
                MessageBox.Show("Error Connecting to Database: " & ex.Message)
            End Try
            conn.Dispose()
            If poID > 0 Then
                PartsOrderID = poID
                downloadPOdata()
                downloadcustdata()
                'GetNewPartsData()
                'GetNewCallData()
                'btnCustData.Visible = False
                'btnAddPart.Visible = True
            End If
            txtStore.Text = Store
            'cmbstore.SelectedItem = Store
            If CustomerNumber > 0 Then downloadcustdata()
        Else

            'NEW PO
            txtStore.Text = Store
            'cmbstore.SelectedItem = Store
            If CustomerNumber > 0 Then downloadcustdata()
        End If
        WritetoScreen()
        permisions()

        'If WONumber = 0 Then btnLogACall.Visible = False
    End Sub
    Private Sub permisions()
        If persistant.myuserLEVEL > 4 Then
            btnVoid.Visible = True
        End If
        'If persistant.myuserLEVEL < 1 Then
        '    Techs()
        '    cmbPriority.Enabled = False
        '    Button2.Visible = False
        '    BtnAddPart.Visible = False
        '    btnCompleted.Visible = False
        '    btnreadytogo.Visible = False
        '    btnPrintOrder.Visible = False
        '    CmbEnviro.Enabled = False
        'Else
        '    If persistant.myuserLEVEL = 1 Or persistant.myuserLEVEL > 7 Then
        '        Admin or Service Writer
        '    Else
        '        CmbEnviro.Enabled = False
        '        btnPrintOrder.Visible = False
        '        BtnDropoff.Visible = False
        '        If cmbStatus.SelectedItem <> "Requested Rig" Then
        '            TabControl1.Enabled = False
        '            BtnAddPart.Visible = False
        '            Panel1.Enabled = False
        '            SplitContainer1.Panel1.Enabled = False
        '        End If
        '    End If
        '    If persistant.myuserLEVEL > 1 And persistant.myuserLEVEL < 8 Then
        '        cmbPriority.Enabled = False
        '        btnviewbos.Visible = False
        '        Button2.Visible = False
        '    End If
        'End If

        'If persistant.myuserLEVEL < 9 Then btnVoid.Visible = False

    End Sub

    'Private Sub buildlists()
    '    For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
    '        If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "ADM" Then cmbstore.Items.Add(persistant.getvalue(persistant.tbl_location, "Code", "", x))
    '    Next

    'End Sub
    Private Sub setuparray()
        Dim i As Integer

        For x As Integer = 0 To (mydata.Length - 1)
            mydata(x) = New element

        Next

        i = 0
        mydata(i).txtbox = txtName
        mydata(i).dbfeild = "Name"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 1
        mydata(i).txtbox = txtAddress
        mydata(i).dbfeild = "Address"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 2
        mydata(i).txtbox = TxtCity
        mydata(i).dbfeild = "City"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 3
        mydata(i).txtbox = txtProv
        mydata(i).dbfeild = "Prov"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 4
        mydata(i).txtbox = txtpostal
        mydata(i).dbfeild = "Postal"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 5
        mydata(i).txtbox = txtphone1
        mydata(i).dbfeild = "Phone1"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 6
        mydata(i).txtbox = Txtphone2
        mydata(i).dbfeild = "Phone2"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 7
        mydata(i).txtbox = Txtemail
        mydata(i).dbfeild = "Email"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 8
        mydata(i).txtbox = TxtBoatMake
        mydata(i).dbfeild = "Bmake"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 9
        mydata(i).txtbox = txtboatmodel
        mydata(i).dbfeild = "Bmodel"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 10
        mydata(i).txtbox = txtBoatSerial
        mydata(i).dbfeild = "Bserial"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 11
        mydata(i).txtbox = txtmotormake
        mydata(i).dbfeild = "Mmake"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 12
        mydata(i).txtbox = txtmotormodel
        mydata(i).dbfeild = "Mmodel"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 13
        mydata(i).txtbox = txtmotorserial
        mydata(i).dbfeild = "Mserial"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 14
        mydata(i).txtbox = txttrailermake
        mydata(i).dbfeild = "Tmake"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 15
        mydata(i).txtbox = txttrailermodel
        mydata(i).dbfeild = "Tmodel"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 16
        mydata(i).txtbox = txttrailerserial
        mydata(i).dbfeild = "Tserial"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 17
        mydata(i).txtbox = txtcolor
        mydata(i).dbfeild = "Color"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 18
        mydata(i).txtbox = txtyear
        mydata(i).dbfeild = "Year"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        'i = 17
        'mydata(i).txtbox = txtNotes
        'mydata(i).dbfeild = "Notes"
        'mydata(i).dbtable = "ServicePartsOrders"
        'mydata(i).changed = False
        'mydata(i).value = ""

        'i = 18
        'mydata(i).txtbox = cmbStatus
        'mydata(i).dbfeild = "Status"
        'mydata(i).dbtable = "WO"
        'mydata(i).changed = False
        'mydata(i).value = ""

        'i = 19
        'mydata(i).txtbox = cmbPriority
        'mydata(i).dbfeild = "Priority"
        'mydata(i).dbtable = "WO"
        'mydata(i).changed = False
        'mydata(i).value = ""

        'i = 20
        'mydata(i).txtbox = DateToBeDropped
        'mydata(i).dbfeild = "DateSchDropOff"
        'mydata(i).dbtable = "WO"
        'mydata(i).changed = False
        'mydata(i).value = ""

        'i = 21
        'mydata(i).txtbox = DateDropOff
        'mydata(i).dbfeild = "DateActDropOff"
        'mydata(i).dbtable = "WO"
        'mydata(i).changed = False
        'mydata(i).value = ""

        'i = 22
        'mydata(i).txtbox = DateReqComp
        'mydata(i).dbfeild = "DateReqComp"
        'mydata(i).dbtable = "WO"
        'mydata(i).changed = False
        'mydata(i).value = ""

        'i = 23
        'mydata(i).txtbox = DateDone
        'mydata(i).dbfeild = "DateActComp"
        'mydata(i).dbtable = "WO"
        'mydata(i).changed = False
        'mydata(i).value = ""


        'i = 24
        'mydata(i).txtbox = txtbill
        'mydata(i).dbfeild = "TotalBilled"
        'mydata(i).dbtable = "WO"
        'mydata(i).changed = False
        'mydata(i).value = ""


        'i = 25
        'mydata(i).txtbox = txtpaid
        'mydata(i).dbfeild = "TotalPaid"
        'mydata(i).dbtable = "WO"
        'mydata(i).changed = False
        'mydata(i).value = ""



    End Sub
    Public Sub downloadPOdata()
        thisPOdata.PartsOrderID = PartsOrderID
        thisPOdata.POstore = Store
        thisPOdata.myconnstring = persistant.myconnstring
        thisPOdata.Download()

        txtOrderID.Text = PartsOrderID.ToString
        txtStore.Text = Store.ToString
        'cmbstore.SelectedItem = Store.ToString
        WONumber = thisPOdata.getvalue("WorkOrderNum")
        CustomerNumber = thisPOdata.getvalue("CustomerProfile")
        txtOrderedBy.Text = thisPOdata.getvalue("OrderedBy")
        txtNotes.Text = thisPOdata.getvalue("Notes")
        txtOrderedDate.Text = thisPOdata.getvalue("OrderedDate")
        'cboPaymentType.SelectedItem = thisPOdata.getvalue("PaymentType")
        'For x As Integer = 0 To mydatacount
        '    If mydata(x).dbtable = "WO" Then mydata(x).value = thisPOdata.getvalue(mydata(x).dbfeild)
        'Next
        'CustomerNumber = thisPOdata.getvalue("CustomerProfile")

    End Sub
    Public Sub downloadcustdata()
        Customerdata.CustomerNumber = CustomerNumber
        Customerdata.myconnstring = persistant.myconnstring
        Customerdata.Download()

        For x As Integer = 0 To mydatacount
            If mydata(x).dbtable = "Customer" Then mydata(x).value = Customerdata.getvalue(mydata(x).dbfeild)
        Next
    End Sub
    'Private Sub txtNotes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNotes.TextChanged
    '    stringchanged(sender)
    'End Sub
    'Private Sub stringchanged(ByVal sender As Object)
    '    Dim data As String
    '    For i As Integer = 0 To (mydata.Length - 1)
    '        If sender.Equals(mydata(i).txtbox) = True Then
    '            data = sender.text
    '            mydata(i).value = data
    '            mydata(i).changed = True
    '        End If
    '    Next
    'End Sub
    Private Sub WritetoScreen()
        txtStore.Text = Store
        'cmbstore.SelectedItem = Store
        For x As Integer = 0 To 18
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
        Next

        'Sync The Calender Up
        'CalDateReqComp.SetDate(CDate(mydata(22).value))
        'Show/hide Items taht depend on a customer number
        If CustomerNumber > 0 Then
            'btnLogCall.Visible = True
            btnCustData.Visible = False
            btnAddPart.Visible = True
        Else
            'btnLogCall.Visible = False
            btnCustData.Visible = True
            btnAddPart.Visible = False
        End If
        If WONumber = 0 Then
            pnlPayment2.Visible = True
            If PartsOrderID > 0 Then pnlPayment.Enabled = True
        Else
            txtWONum.Text = WONumber
            If PartsOrderID > 0 Then lblWOPaymentNote.Visible = True
        End If

        ''show hide the mark as done btn
        'Dim anynotdone As Boolean = False
        'For x As Integer = 0 To DVparts.Rows.Count - 1
        '    If PartsDataTable.Rows(x).Item("Status") <> "Picked Up" Then
        '        anynotdone = True
        '    End If
        'Next
        'If anynotdone = False And (cmbStatus.SelectedItem = "Active" Or cmbStatus.SelectedItem = "Approved Rig") Then
        '    If persistant.myuserLEVEL = 1 Or persistant.myuserLEVEL > 7 Then
        '        btnreadytogo.Visible = True
        '    End If
        'Else
        '    btnreadytogo.Visible = False
        'End If
        If PartsOrderID = 0 Then
            txtOrderID.Text = "CREATING NEW"
        Else
            txtOrderID.Text = PartsOrderID.ToString
            GetNewCallData()
            GetNewPartsData()
            GetNewPaymentData()
            txtBalance.Text = Format((TotalOwed - TotalPaid).ToString, "Currency")
        End If

    End Sub
    Private Sub save()

        Me.txtName.Select()

        If PartsOrderID > 0 Then
            'UPDATE PO notes only after a part has been ordered.
            Dim cmdstring As String = "Update ServicePartsOrders Set Notes = '" & txtNotes.Text & "',OrderedDate = '" & txtOrderedDate.Text & _
                                    "' Where PartsOrderID = " & PartsOrderID & " AND Store = '" & Me.Store & "'"
            '"',PaymentType = '" & cboPaymentType.SelectedItem & _

            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring

            cmd.Connection = conn
            cmd.CommandText = cmdstring
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()
        End If
        'For x As Integer = 0 To mydatacount
        '    mydata(x).value = Replace(mydata(x).value, "'", "\'")
        '    mydata(x).value = Replace(mydata(x).value, ";", "\;")
        'Next

        'Dim conn As New MySqlConnection
        'Dim cmd As New MySqlCommand
        'conn.ConnectionString = persistant.myconnstring

        'cmd.Connection = conn

        'If PartsOrderID > 0 Then
        '    'UPDATE PO
        '    Dim cmdstring As String = "Update Service Set "

        '    For x As Integer = 0 To mydatacount
        '        If mydata(x).dbtable = "WO" And mydata(x).changed = True Then
        '            cmdstring = cmdstring + mydata(x).dbfeild + " = '" + mydata(x).value + "', "
        '        End If
        '    Next

        '    cmdstring = cmdstring + "CustomerProfile = '" + CustomerNumber.ToString + "', "
        '    cmdstring = cmdstring + " Where OrderID = " & OrderID & " AND Store = '" & Me.Store & "'"
        '    cmd.CommandText = cmdstring
        '    Try
        '        conn.Open()
        '        cmd.ExecuteNonQuery()
        '        conn.Close()
        '    Catch ex As MySqlException
        '        MessageBox.Show(ex.Message)
        '    End Try
        'Else
        '    'INSERT NEW WO
        '    If CustomerNumber > 0 Then
        '        Dim WOStart As Integer = persistant.getvalue(persistant.tbl_location, "StartWONum", "code = '" + Me.Store + "'", 0)
        '        Try
        '            conn.Open()
        '            cmd.CommandText = "Select CASE WHEN ISNULL(Max(Number)) then " & WOStart.ToString & " else Max(Number) + 1 END from ServiceWO where store = '" + Me.Store + "' and Number >= " & WOStart
        '            'cmd.CommandText = "Select MAX(Number) + 1 as NewWONum from ServiceWO where Store = '" + Me.Store + "'"
        '            OrderID = CInt(cmd.ExecuteScalar)
        '            conn.Close()
        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '            Exit Sub
        '        End Try
        '        Dim cmdstring As String = "Insert INTO ServiceWO Set "

        '        For x As Integer = 0 To mydatacount
        '            If mydata(x).dbtable = "WO" And mydata(x).changed = True Then
        '                cmdstring = cmdstring + mydata(x).dbfeild + " = '" + mydata(x).value + "', "
        '            End If
        '        Next

        '        cmdstring = cmdstring + "CustomerProfile = '" + CustomerNumber.ToString + "', "
        '        cmdstring = cmdstring + "Number = '" + OrderID.ToString + "', "
        '        cmdstring = cmdstring + "Store = '" + Me.Store + "'"
        '        cmd.CommandText = cmdstring
        '        Debug.WriteLine(cmdstring)
        '        Try
        '            conn.Open()
        '            cmd.ExecuteNonQuery()
        '            WritetoScreen()
        '            conn.Close()
        '        Catch ex As MySqlException
        '            MessageBox.Show(ex.Message)
        '        End Try
        '    Else
        '        'MessageBox.Show("You must select a customer before this WO will be saved")
        '    End If
        'End If

        'For x As Integer = 0 To mydatacount
        '    mydata(x).value = Replace(mydata(x).value, "\'", "'")
        '    mydata(x).value = Replace(mydata(x).value, "\;", ";")
        '    mydata(x).changed = False
        'Next

        DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub BtnCustData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustData.Click
        Dim PickCust As New FrmCustFinder
        PickCust.persistant = persistant
        PickCust.Selectmode = True
        PickCust.ShowDialog()
        If PickCust.DialogResult = Windows.Forms.DialogResult.OK Then
            Me.CustomerNumber = PickCust.SelectedProfile
        End If
        downloadcustdata()
        WritetoScreen()
    End Sub

    Private Sub RefreshPartsDataView(ByVal data As DataTable)
        PartsDataTable.Clear()
        PartsDataTable.Merge(data)
        PartsBindingSource.ResetBindings(False)

        If PartsBinded = False Then
            PartsBindingSource.DataSource = PartsDataTable
            DVparts.DataSource = PartsBindingSource
            PartsBinded = True
        End If
        ' Me.DVIssue.Columns("Note").Width = 220
        ' Me.DVIssue.Columns("Who").Width = 40

        If DVparts.RowCount > 0 Then
            If persistant.myuserLEVEL > 8 Then
                Me.DVparts.Columns("RP").Visible = True
            Else
                Me.DVparts.Columns("RP").Visible = False
            End If
            Me.DVparts.Columns("View2").Visible = True
            Me.DVparts.ColumnHeadersVisible = True
            DVparts.Columns("Number").Visible = False
            DVparts.Columns("PartsOrderID").Visible = False
        Else
            Me.DVparts.Columns("RP").Visible = False
            Me.DVparts.Columns("View2").Visible = False
            Me.DVparts.ColumnHeadersVisible = True
        End If
        'Me.DVparts.Columns("Issue").Visible = False
        Me.DVparts.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells) ' .DisplayedCellsExceptHeader)

    End Sub

    Private Sub GetNewPartsData()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        Dim SQL As String = BuildPartsSQL()

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            PartsNewData.Clear()
            adapter.Fill(PartsNewData)
            RefreshPartsDataView(PartsNewData)
            'UpdatePartsState()
            'PartsNewData.Clear()
            'adapter.Fill(PartsNewData)
            'RefreshPartsDataView(PartsNewData)
            Dim Tax As Double = (1 + GSTRate)
            If persistant.getvalue(persistant.tbl_location, "Prov", "Store = '" + Store + "'", 0) = "BC" Then Tax = (1 + GSTRate + PSTRate)
            cmd.CommandText = "Select SUM(Quantity * Price ) from ServiceParts where PartsOrderID = " + PartsOrderID.ToString
            TotalOwed = (cmd.ExecuteScalar() * Tax).ToString
            txtTotal.Text = Format(TotalOwed.ToString, "Currency")
            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub
    Private Function BuildPartsSQL() As String
        BuildPartsSQL = "SELECT Number, PartsOrderID, Status, Quantity, " & _
                            "PartNumber, Part, Price, Supplier, ConfirmationNum, IssueNumber, PaymentType  " & _
                            "From ServiceParts " & _
                            "WHERE PartsOrderID = " & PartsOrderID.ToString & _
                            " ORDER BY PartsOrderID ASC"

    End Function

    Private Sub DVparts_CellContentClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DVparts.CellContentClick
        Dim xRow As Integer
        Dim XNum As Integer
        'If e.ColumnIndex = 0 Then
        '    If MsgBox("Are you sure you want to delete this part?", MsgBoxStyle.YesNo, "Delete Confirmation") = MsgBoxResult.Yes Then
        '        'remove the issue
        '        xRow = e.RowIndex
        '        Dim partNum As Integer = PartsDataTable.Rows(xRow).Item("Number")
        '        'Dim sii As Integer = Issuedatatable.Rows(xRow).Item("Issue")
        '        Dim conn As New MySqlConnection
        '        Dim cmd As New MySqlCommand
        '        conn.ConnectionString = persistant.myconnstring
        '        cmd.Connection = conn
        '        'Dim cmdstring As String = "DELETE from ServicePics where PicID = " & pid
        '        cmd.CommandText = "DELETE from ServiceParts where Number = " & partNum
        '        'Debug.WriteLine(cmdstring)
        '        Try
        '            conn.Open()
        '            cmd.ExecuteNonQuery()
        '            'cmd.CommandText = "DELETE from ServiceIssue where Issue = " & sii
        '            'cmd.ExecuteNonQuery()
        '            conn.Close()
        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '        End Try
        '        conn.Dispose()
        '        'refresh the picture list
        '        'If WONumber <> 0 Then
        '        '    GetNewIssueData()
        '        'End If
        '        'WritetoScreen()
        '        GetNewPartsData()
        '    End If

        'Else
        '    'If DVCalls.Columns(e.ColumnIndex).Name = "View" Then
        '    xRow = e.RowIndex
        '    XNum = PartsDataTable.Rows(xRow).Item("Number").ToString

        '    Dim ViewPart As New frmAddNewPart
        '    ViewPart.persistant = persistant
        '    ViewPart.PartID = XNum
        '    ViewPart.ShowDialog()

        'End If

        If DVparts.Columns(e.ColumnIndex).Name = "View2" Then
            'If DVCalls.Columns(e.ColumnIndex).Name = "View" Then
            xRow = e.RowIndex
            XNum = PartsDataTable.Rows(xRow).Item("Number").ToString

            Dim ViewPart As New frmAddNewPart
            ViewPart.persistant = persistant
            ViewPart.PartID = XNum
            ViewPart.ShowDialog()
        ElseIf DVparts.Columns(e.ColumnIndex).Name = "RP" Then
            If MsgBox("Are you sure you want to delete this part?", MsgBoxStyle.YesNo, "Delete Confirmation") = MsgBoxResult.Yes Then
                'remove the issue
                xRow = e.RowIndex
                Dim partNum As Integer = PartsDataTable.Rows(xRow).Item("Number")
                'Dim sii As Integer = Issuedatatable.Rows(xRow).Item("Issue")
                Dim conn As New MySqlConnection
                Dim cmd As New MySqlCommand
                conn.ConnectionString = persistant.myconnstring
                cmd.Connection = conn
                'Dim cmdstring As String = "DELETE from ServicePics where PicID = " & pid
                cmd.CommandText = "DELETE from ServiceParts where Number = " & partNum
                'Debug.WriteLine(cmdstring)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    'cmd.CommandText = "DELETE from ServiceIssue where Issue = " & sii
                    'cmd.ExecuteNonQuery()
                    conn.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                conn.Dispose()
                'refresh the picture list
                'If WONumber <> 0 Then
                '    GetNewIssueData()
                'End If
                'WritetoScreen()
                'GetNewPartsData()
            End If
        End If



        WritetoScreen()
    End Sub

    Private Sub DVCalls_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DVCalls.CellContentClick
        Dim xRow As Integer
        Dim XNum As Integer
        If DVCalls.Columns(e.ColumnIndex).Name = "View" Then
            xRow = e.RowIndex
            XNum = Callsdatatable.Rows(xRow).Item("Number").ToString
            Dim Viewcall As New frmCallLog
            Viewcall.persistant = persistant
            Viewcall.NoteNumber = XNum
            Viewcall.Viewnote = True
            Viewcall.ShowDialog()
        End If
        WritetoScreen()
    End Sub

    Private Sub FrmPartsOrder_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        save()
    End Sub

    Private Sub btnPrintOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintOrder.Click

 
        'Dim issuedatax As New IssueData
        'Dim posx,
        Dim posy, align As Integer
        'Dim text, txtboxname As String
        Dim size As Integer
        Dim inputfile, savelocation As String
        'Dim imagex As Image
        'Dim mybosstorecode As String = persistant.getvalue(persistant.tbl_location, "Code", "store = '" + ListBox2.SelectedItem + "'", 0)
        Dim addr As String = persistant.installdir & "\resources\images\" & Store & "Addr.png"
        inputfile = persistant.installdir & "\resources\contracts\PartsOrder.pdf"

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

        If Not Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\PO") Then Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\PO")
        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\PO\" + Store + "-" + PartsOrderID.ToString + ".pdf"

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
        cb.ShowTextAligned(align, mydata(0).txtbox.text, 100, 686, 0)

        'Ordered By
        cb.ShowTextAligned(align, txtOrderedBy.Text, 375, 686, 0)

        'customer home phone
        cb.ShowTextAligned(align, mydata(5).txtbox.text, 100, 671, 0)

        'customer work phone
        cb.ShowTextAligned(align, mydata(6).txtbox.text, 375, 671, 0)

        'boat make model
        cb.ShowTextAligned(align, mydata(8).txtbox.text & " / " & mydata(9).txtbox.text, 100, 656, 0)

        'boat serial
        cb.ShowTextAligned(align, mydata(10).txtbox.text, 375, 656, 0)

        'motor make model
        cb.ShowTextAligned(align, mydata(11).txtbox.text & " / " & mydata(12).txtbox.text, 100, 642, 0)

        'motor serial
        cb.ShowTextAligned(align, mydata(13).txtbox.text, 375, 642, 0)

        'trailer make model
        cb.ShowTextAligned(align, mydata(14).txtbox.text & " / " & mydata(15).txtbox.text, 100, 627, 0)

        'trailer serial
        cb.ShowTextAligned(align, mydata(16).txtbox.text, 375, 627, 0)

        posy = 560
        Dim startY As Double = 560
        Dim totalParts As Double = 0
        Dim q As Integer
        Dim p As Double
        For i As Integer = 0 To DVparts.RowCount - 1
            'in stock
            'If Not (PartsDataTable.Rows(i).Item("Status").ToString() = "To Be Ordered" Or
            '    PartsDataTable.Rows(i).Item("Status").ToString() = "Ordered") Then
            '    cb.ShowTextAligned(align, "X", 46, posy, 0)
            'End If
            'Order #
            cb.ShowTextAligned(1, PartsOrderID.ToString, 46, posy, 0)
            'Quantity
            q = PartsDataTable.Rows(i).Item("Quantity")
            cb.ShowTextAligned(2, FormatNumber(q, 0), 115, posy, 0)
            'Part #
            cb.ShowTextAligned(1, PartsDataTable.Rows(i).Item("PartNumber").ToString, 175, posy, 0)
            'Description
            cb.ShowTextAligned(0, PartsDataTable.Rows(i).Item("Part").ToString, 228, posy, 0)
            'Unit Price
            p = IIf(PartsDataTable.Rows(i).Item("Price") Is DBNull.Value, 0, PartsDataTable.Rows(i).Item("Price"))
            cb.ShowTextAligned(2, FormatNumber(p, 2), 500, posy, 0)
            'Extended Price
            cb.ShowTextAligned(2, FormatNumber(q * p, 2), 575, posy, 0)
            totalParts += (q * p)
            posy = startY - (((startY - 110) / 23) * (i + 1))
            'posy = Int(startY - ((i + 1) * 18.6))
        Next
        'sub total
        cb.ShowTextAligned(2, "$" & FormatNumber(totalParts, 2), 575, 90, 0)
        'gst
        Dim tax As Double = 0
        If persistant.getvalue(persistant.tbl_location, "Prov", "Code = '" + Store + "'", 0) = "AB" Then
            tax = totalParts * GSTRate
        Else
            tax = totalParts * (GSTRate + PSTRate)
        End If
        cb.ShowTextAligned(2, "$" & FormatNumber(tax, 2), 575, 70, 0)

        'total
        cb.ShowTextAligned(2, "$" & FormatNumber(totalParts + tax, 2), 575, 50, 0)

        'notice
        cb.ShowTextAligned(0, "There is a 25% re stocking fee plus any applicable shipping charges for any custom ordered parts that are returned.", 30, 25, 0)

        cb.EndText()

        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    Private Sub RefreshCallsDataView(ByVal data As DataTable)
        Callsdatatable.Clear()
        Callsdatatable.Merge(data)
        Callsbindingsource.ResetBindings(False)

        If Callsbinded = False Then
            Callsbindingsource.DataSource = Callsdatatable
            DVCalls.DataSource = Callsbindingsource
            Callsbinded = True
        End If
        ' Me.DVCalls.Columns("Note").Width = 220
        ' Me.DVCalls.Columns("Who").Width = 40
        If DVCalls.RowCount > 0 Then
            Me.DVCalls.Columns("View").Visible = True
            Me.DVCalls.ColumnHeadersVisible = True

        Else
            Me.DVCalls.Columns("View").Visible = False
            Me.DVCalls.ColumnHeadersVisible = True

        End If
        Me.DVCalls.Columns("Number").Visible = False

        Me.DVCalls.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells) ' .DisplayedCellsExceptHeader)

    End Sub

    Private Sub GetNewCallData()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        'Dim SQL As String = "SELECT Number, Who, time, Note FROM ServiceCallLog where CustProfile = '" + CustomerNumber.ToString + "' ORDER BY time DESC"
        Dim SQL As String = "SELECT Number, Who, time, Note FROM ServiceCallLog where CallType = 'Work Order' and ParentID = '" & WONumber.ToString & "' and CustProfile = '" & CustomerNumber.ToString & "' ORDER BY time DESC"
        If WONumber = 0 Then
            SQL = "SELECT Number, Who, time, Note FROM ServiceCallLog where CallType = 'Parts Order' and ParentID = '" & PartsOrderID.ToString & "' and CustProfile = '" & CustomerNumber.ToString & "' ORDER BY time DESC"
        End If

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            Callsnewdata.Clear()
            adapter.Fill(Callsnewdata)
            conn.Close()
            RefreshCallsDataView(Callsnewdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub


    Private Sub btnLogACall_Click(sender As System.Object, e As System.EventArgs) Handles btnLogACall.Click
        Dim logcall As New frmCallLog
        logcall.persistant = persistant
        logcall.CustNumber = CustomerNumber
        If WONumber = 0 Then
            logcall.CallType = "Parts Order"
            logcall.ParentID = PartsOrderID
        End If
        'logcall.isPOCall = True
        logcall.ShowDialog()
        WritetoScreen()
    End Sub

    Private Sub btnViewByCust_Click(sender As System.Object, e As System.EventArgs) Handles btnViewByCust.Click
        Dim CustCalls As New frmOldCallLogByCustomer
        CustCalls.CustomerNumber = CustomerNumber
        CustCalls.persistant = persistant
        CustCalls.ShowDialog()
    End Sub

    Private Sub BtnAddPart_Click(sender As System.Object, e As System.EventArgs) Handles btnAddPart.Click
        ''if the partorderID is 0 then create a new order ID and send it to the new part form.
        'If PartsOrderID = 0 Then
        '    Dim conn As New MySqlConnection
        '    Dim cmd As New MySqlCommand
        '    conn.ConnectionString = persistant.myconnstring
        '    cmd.Connection = conn
        '    Dim cmdstring As String = ""

        '    cmdstring = "Insert INTO ServicePartsOrders Set Store = '" & Store.ToString & "', " & _
        '        "WorkOrderNum = '" & WONumber.ToString & "', CustomerProfile = '" & CustomerNumber.ToString & "'"

        '    cmd.CommandText = cmdstring
        '    Debug.WriteLine(cmdstring)
        '    Try
        '        conn.Open()
        '        cmd.ExecuteNonQuery()
        '        'get the created partsOrderID
        '        cmd.CommandText = "Select MAX(PartsOrderID) from ServicePartsOrders"
        '        PartsOrderID = CInt(cmd.ExecuteScalar)
        '        conn.Close()
        '    Catch ex As MySqlException
        '        MessageBox.Show(ex.Message)
        '    End Try

        'End If
        Dim addPart As New frmAddNewPart
        addPart.persistant = persistant
        addPart.PartsOrderID = PartsOrderID
        addPart.PartID = 0
        addPart.Store = Store
        addPart.WONum = WONumber
        addPart.CustNum = CustomerNumber
        addPart.IssueNum = IssueNumber
        addPart.OrderMe = True
        If addPart.ShowDialog() = DialogResult.OK Then
            PartsOrderID = addPart.PartsOrderID
            txtOrderID.Text = PartsOrderID.ToString
            txtOrderedBy.Text = persistant.getvalue(persistant.tbl_users, "name", "user = '" + persistant.myuserID + "'", 0)
            txtOrderedDate.Text = MonthName(Now.Month) & " " & Now.Day & ", " & Now.Year
            GetNewPartsData()
        End If

        'WritetoScreen()
    End Sub

    Private Sub btnReceived_Click(sender As System.Object, e As System.EventArgs) Handles btnReceived.Click
        Dim conn As New MySqlConnection
        conn.ConnectionString = persistant.myconnstring
        Dim cmd As New MySqlCommand
        For i As Integer = 0 To DVparts.RowCount - 1
            If (PartsDataTable.Rows(i).Item("Status").ToString() = "To Be Ordered" Or
                PartsDataTable.Rows(i).Item("Status").ToString() = "Back Ordered" Or
                 PartsDataTable.Rows(i).Item("Status").ToString() = "Ordered") Then
                Dim cmdstring As String = "Update ServiceParts Set Status = 'Received' Where Number = '" & PartsDataTable.Rows(i).Item("Number").ToString & "'"

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
        GetNewPartsData()

    End Sub

    Private Sub btnVoid_Click(sender As System.Object, e As System.EventArgs) Handles btnVoid.Click
        Dim conn As New MySqlConnection
        conn.ConnectionString = persistant.myconnstring
        Dim cmd As New MySqlCommand
        For i As Integer = 0 To DVparts.RowCount - 1
            Dim cmdstring As String = "Update ServiceParts Set Status = 'Void', Price = 0 Where Number = '" & PartsDataTable.Rows(i).Item("Number").ToString & "'"

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

        Next
        GetNewPartsData()
    End Sub

    Private Sub btnAddPayment_Click(sender As System.Object, e As System.EventArgs) Handles btnAddPayment.Click
        Dim pay As New frmlogpayment
        pay.WO = 0
        pay.PO = PartsOrderID
        pay.Store = Store
        pay.persistant = persistant
        pay.ShowDialog()
        WritetoScreen()

    End Sub

    Private Sub RefreshPaymentDataView(ByVal data As DataTable)
        Paymentdatatable.Clear()
        Paymentdatatable.Merge(data)
        Paymentbindingsource.ResetBindings(False)

        If Paymentbinded = False Then
            Paymentbindingsource.DataSource = Paymentdatatable
            DVPayments.DataSource = Paymentbindingsource
            Paymentbinded = True
        End If
        ' Me.DVpayment.Columns("Note").Width = 220
        ' Me.DVpayment.Columns("Who").Width = 40
        If DVPayments.RowCount > 0 Then
            Me.DVPayments.ColumnHeadersVisible = True
        Else
            Me.DVPayments.ColumnHeadersVisible = False
        End If
        Me.DVPayments.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

    End Sub

    Private Sub GetNewPaymentData()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        Dim SQL As String = "SELECT Concat('$',Amount) as 'Amount', CounterSlip, date from ServicePayments where PO = " + PartsOrderID.ToString + " AND Store = '" + Store + "'"

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            Paymentnewdata.Clear()
            adapter.Fill(Paymentnewdata)

            cmd.CommandText = "Select SUM(Amount) from ServicePayments where PO = " + PartsOrderID.ToString + " AND Store = '" + Store + "'"
            Dim retVal As Object = cmd.ExecuteScalar
            If retVal IsNot DBNull.Value Then
                TotalPaid = retVal
            End If
            txtPaid.Text = Format(TotalPaid.ToString, "Currency")

            conn.Close()
            RefreshPaymentDataView(Paymentnewdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub

 
    Private Sub btnViewEdit_Click(sender As System.Object, e As System.EventArgs) Handles btnViewEdit.Click
        Dim editcust As New frmEditCustomer
        editcust.persistant = persistant
        editcust.CustomerNumber = CustomerNumber
        editcust.ShowDialog()
        downloadcustdata()
        WritetoScreen()

    End Sub

 
End Class