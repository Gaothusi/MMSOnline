Imports MySql.Data.MySqlClient
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.Data
Imports MMSOnline.Functions
Imports System.IO

Public Class FrmWO
    Public persistant As PreFetch
    Public WONumber As Integer
    Public Store As String
    Public newWOschDate As DateTime
    Public BOSnumber As Integer
    Public CustomerNumber As Integer
    Public SkipSave As Boolean = False
    Private funcs As New Functions

    Private Const mydatacount As Integer = 29
    Private mydata(mydatacount) As element
    Private Customerdata As New CustData
    Private thisWOdata As New WOData
    Private CheckInNote As Integer
    Private Callsdatatable As New DataTable
    Private Callsnewdata As New DataTable
    Private Callsbindingsource As New BindingSource
    Private Callsbinded As Boolean
    Private Issuedatatable As New DataTable
    Private Issuenewdata As New DataTable
    Private Issuebindingsource As New BindingSource
    Private Issuebinded As Boolean
    Private paymentdatatable As New DataTable
    Private paymentnewdata As New DataTable
    Private paymentbindingsource As New BindingSource
    Private paymentbinded As Boolean
    Private adjustmentdatatable As New DataTable
    Private adjustmentnewdata As New DataTable
    Private adjustmentbindingsource As New BindingSource
    Private adjustmentbinded As Boolean
    Private bill, paid As Decimal


    'Private Sub FrmWO_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave
    '    save()
    '    DialogResult = Windows.Forms.DialogResult.OK
    '    Me.Close()
    'End Sub
    Private Sub FrmWO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setuparray()
        buildlists()


        If WONumber > 0 Then
            'LOAD EXISTING
            downloadWOdata()
            downloadcustdata()
            BtnCustData.Visible = False
        Else
            'NEW WO
            cmbstore.SelectedItem = Store
            cmbPriority.SelectedItem = "Normal"
            cmbStatus.SelectedItem = "Scheduled"
            DateToBeDropped.Value = DateTime.Now
            DateReqComp.Value = Date.Now
            cmbchanged(cmbPriority)
            cmbchanged(cmbStatus)
            datechanged(DateToBeDropped)
            datechanged(DateReqComp)
            If BOSnumber > 0 Then
                BtnDropoff.Visible = False
                cmbStatus.SelectedItem = "Requested Rig"
                cmbchanged(cmbStatus)
                downloadcustdata()
            Else
                CustomerNumber = 0
            End If
            CmbEnviro.SelectedItem = "NO"
            cmbchanged(CmbEnviro)
        End If
        WritetoScreen()
        permisions()
        'Hide/Show PST
        'If persistant.myusername.ToLower() = "dank" Or persistant.myusername.ToLower() = "rob" Then
        'show PST and GST exempt option for Dan Koziak or Rob Pelletier
        txtrednopst.Visible = True
        chkPST.Visible = True
        chkPST.Enabled = True
        'Else
        'hide the option to exempt PST and GST for all other users
        'txtrednopst.Visible = False
        'chkPST.Enabled = False
        'End If
    End Sub
    Private Sub permisions()
        If persistant.myuserLEVEL < 1 Then
            'Techs
            cmbPriority.Enabled = False
            Button2.Visible = False
            'BtnAddIssue.Visible = False
            btnCompleted.Visible = False
            btnreadytogo.Visible = False
            Button3.Visible = False
            CmbEnviro.Enabled = False
        Else
            If persistant.myuserLEVEL = 1 Or persistant.myuserLEVEL > 3 Then
                'Admin or Service Writer
                cmbStatus.Enabled = True
                cmbPriority.Enabled = True
                DateToBeDropped.Enabled = True
                DateDropOff.Enabled = True
            Else
                CmbEnviro.Enabled = False
                Button3.Visible = False
                BtnDropoff.Visible = False
                If cmbStatus.SelectedItem <> "Requested Rig" Then
                    TabControl1.Enabled = False
                    BtnAddIssue.Visible = False
                    Panel1.Enabled = False
                    SplitContainer1.Panel1.Enabled = False
                End If
            End If
            If persistant.myuserLEVEL > 1 And persistant.myuserLEVEL < 4 Then
                cmbPriority.Enabled = False
                btnviewbos.Visible = False
                Button2.Visible = False
            End If
        End If
        'remove void option if it isn't currently selected and the user is not admin
        If cmbStatus.SelectedItem <> "Void" And persistant.myuserLEVEL < 9 Then  cmbStatus.Items.Remove("Void")
        If persistant.myuserLEVEL < 9 Then btnVoid.Visible = False

    End Sub

    Private Sub buildlists()
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
            If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "ADM" Then cmbstore.Items.Add(persistant.getvalue(persistant.tbl_location, "Code", "", x))
        Next

    End Sub
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
        mydata(i).txtbox = txtNotes
        mydata(i).dbfeild = "Notes"
        mydata(i).dbtable = "WO"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 18
        mydata(i).txtbox = cmbStatus
        mydata(i).dbfeild = "Status"
        mydata(i).dbtable = "WO"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 19
        mydata(i).txtbox = cmbPriority
        mydata(i).dbfeild = "Priority"
        mydata(i).dbtable = "WO"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 20
        mydata(i).txtbox = DateToBeDropped
        mydata(i).dbfeild = "DateSchDropOff"
        mydata(i).dbtable = "WO"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 21
        mydata(i).txtbox = DateDropOff
        mydata(i).dbfeild = "DateActDropOff"
        mydata(i).dbtable = "WO"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 22
        mydata(i).txtbox = DateReqComp
        mydata(i).dbfeild = "DateReqComp"
        mydata(i).dbtable = "WO"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 23
        mydata(i).txtbox = DateDone
        mydata(i).dbfeild = "DateActComp"
        mydata(i).dbtable = "WO"
        mydata(i).changed = False
        mydata(i).value = ""


        i = 24
        mydata(i).txtbox = txtbill
        mydata(i).dbfeild = "TotalBilled"
        mydata(i).dbtable = "WO"
        mydata(i).changed = False
        mydata(i).value = ""


        i = 25
        mydata(i).txtbox = txtpaid
        mydata(i).dbfeild = "TotalPaid"
        mydata(i).dbtable = "WO"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 26
        mydata(i).txtbox = txtcolor
        mydata(i).dbfeild = "Color"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 27
        mydata(i).txtbox = txtyear
        mydata(i).dbfeild = "Year"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 28
        mydata(i).txtbox = CmbEnviro
        mydata(i).dbfeild = "EnviroFee"
        mydata(i).dbtable = "WO"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 29
        mydata(i).txtbox = txtWarnings
        mydata(i).dbfeild = "Warnings"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

    End Sub
    Public Sub downloadWOdata()
        thisWOdata.WONumber = WONumber
        thisWOdata.WOstore = Store
        thisWOdata.myconnstring = persistant.myconnstring
        thisWOdata.Download()

        For x As Integer = 0 To mydatacount
            If mydata(x).dbtable = "WO" Then mydata(x).value = thisWOdata.getvalue(mydata(x).dbfeild)
        Next
        CustomerNumber = thisWOdata.getvalue("CustomerProfile")
        BOSnumber = thisWOdata.getvalue("BOS")
        CheckInNote = thisWOdata.getvalue("CheckInNote")

    End Sub
    Public Sub downloadcustdata()
        Customerdata.CustomerNumber = CustomerNumber
        Customerdata.myconnstring = persistant.myconnstring
        Customerdata.Download()

        For x As Integer = 0 To mydatacount
            If mydata(x).dbtable = "Customer" Then mydata(x).value = Customerdata.getvalue(mydata(x).dbfeild)
        Next

        'if this is a new work order then check to see if the serial number has an open issue.
        If WONumber = 0 Then
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            cmd.Connection = conn

            Dim intWONum As Integer = 0
            cmd.CommandText = "SELECT ServiceWOtoIssue.WOnumber " & _
                                "FROM ServiceIssue Inner Join ServiceWOtoIssue ON " & _
                                    "ServiceIssue.Issue = ServiceWOtoIssue.Issue WHERE ServiceIssue.Customer ='" & CustomerNumber.ToString & "' AND " & _
                                    "ServiceIssue.Status <> 'Completed'"
            Try
                conn.Open()
                intWONum = CInt(cmd.ExecuteScalar)
                conn.Close()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()

            If intWONum > 0 Then
                If MsgBox("There is another open issue for this customer in the Work Order # " & intWONum.ToString & "." & Environment.NewLine & _
                          "Do you want to continue to create another Work Order for this Customer?", MsgBoxStyle.YesNo, "Existing Issue") = MsgBoxResult.No Then
                    SkipSave = True
                    Me.Close()
                End If
            End If
        End If
    End Sub
    Private Sub WritetoScreen()
        Me.txtWOnum.Text = WONumber.ToString
     
        Me.cmbstore.SelectedItem = Store
        For x As Integer = 0 To 17
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
        Next
        For x As Integer = 18 To 19
            If mydata(x).value <> "" Then mydata(x).txtbox.SelectedItem = mydata(x).value
        Next
        For x As Integer = 20 To 23
            If mydata(x).value <> "" Then mydata(x).txtbox.value = CDate(mydata(x).value)
        Next
        For x As Integer = 24 To 25
            If mydata(x).value <> "" Then mydata(x).txtbox.text = Format(mydata(x).value, "Currency")
        Next
        For x As Integer = 26 To 27
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
        Next
        If mydata(28).value <> "" Then mydata(28).txtbox.SelectedItem = mydata(28).value
        If mydata(29).value <> "" Then mydata(29).txtbox.text = mydata(29).value

        'Sync The Calender Up
        If mydata(22).value <> "" Then
            CalDateReqComp.SetDate(CDate(mydata(22).value))
        End If
        'Show/hide Items taht depend on a customer number
        If CustomerNumber > 0 Then
            btnLogCall.Visible = True
            Me.btnEditCust.Visible = True
        Else
            btnLogCall.Visible = False
            Me.btnEditCust.Visible = False
        End If

        'Show/hide Button to view check in note
        If CheckInNote > 0 Then
            btnCheckInNotes.Visible = True
            BtnRePrintCheckin.Visible = True
        Else
            btnCheckInNotes.Visible = False
            BtnRePrintCheckin.Visible = True
        End If

        'Show/hide Actual Check In Date 
        If cmbStatus.SelectedItem = "Scheduled" Or cmbStatus.SelectedItem = "Requested Rig" Or cmbStatus.SelectedItem = "Approved Rig" Then
            Label27.Visible = False
            DateDropOff.Visible = False
            DateToBeDropped.Enabled = True

        Else
            Label27.Visible = True
            DateDropOff.Visible = True
            DateToBeDropped.Enabled = False

        End If
        'Show/hide Completed Date
        If cmbStatus.SelectedItem = "Waiting For Pickup" Or cmbStatus.SelectedItem = "Closed" Then
            Label29.Visible = True
            DateDone.Visible = True
            btnReactivate.Visible = True

        Else
            Label29.Visible = False
            DateDone.Visible = False
            btnReactivate.Visible = False

        End If

        If cmbStatus.SelectedItem = "Closed" Then
            BtnAddIssue.Visible = False
            Button3.Visible = False
        End If

        'Show/hide Button to complete
        If cmbStatus.SelectedItem = "Approved Rig" Or cmbStatus.SelectedItem = "Active" Or cmbStatus.SelectedItem = "Waiting For Pickup" Then
            If persistant.myuserLEVEL = 1 Or persistant.myuserLEVEL > 7 Then
                btnCompleted.Visible = True
            End If
        Else
            btnCompleted.Visible = False
            TabControl1.TabPages(2).Hide()
        End If

        'show hide the mark as done btn
        Dim anynotdone As Boolean = False
        For x As Integer = 0 To DVissues.Rows.Count - 1
            If Issuedatatable.Rows(x).Item("Status") <> "Completed" Then
                anynotdone = True
            End If
        Next
        If anynotdone = False And (cmbStatus.SelectedItem = "Active" Or cmbStatus.SelectedItem = "Approved Rig") Then
            If persistant.myuserLEVEL = 1 Or persistant.myuserLEVEL > 7 Then
                btnreadytogo.Visible = True
            End If
        Else
            btnreadytogo.Visible = False
        End If
        If WONumber = 0 Then
            Me.txtWOnum.Text = "CREATING NEW"
        Else
            Getnewcalldata()
            GetNewIssueData()
            GetNewPaymentData()
            Getnewadjustdata()
        End If
        'Show/hide Button to check in
        If cmbStatus.SelectedItem = "Scheduled" Then
            If Issuedatatable.Rows.Count <> 0 Then
                BtnDropoff.Visible = True
                BtnDropoff.Text = "Check In"
            End If
        Else
            If cmbStatus.SelectedItem = "Requested Rig" Then
                BtnDropoff.Visible = True
                BtnDropoff.Text = "Approve Rig"
            Else
                BtnDropoff.Visible = False
            End If
        End If

        'Show/hide Items that are Rig Only
        If BOSnumber > 0 Then
            btnviewbos.Visible = True
            btnCheckInNotes.Visible = False
            btnreadytogo.Visible = False
        Else
            btnviewbos.Visible = False
            btnCheckInNotes.Visible = True
        End If
        domath()
    End Sub
    Private Sub save()
        If SkipSave Then Exit Sub
        Me.txtName.Select()

        For x As Integer = 0 To mydatacount
            mydata(x).value = Replace(mydata(x).value, "'", "\'")
            mydata(x).value = Replace(mydata(x).value, ";", "\;")
        Next

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring

        cmd.Connection = conn

        If WONumber > 0 Then
            'UPDATE WO
            Dim cmdstring As String = "Update ServiceWO Set "

            For x As Integer = 0 To mydatacount
                If mydata(x).dbtable = "WO" And mydata(x).changed = True Then
                    cmdstring = cmdstring + mydata(x).dbfeild + " = '" + mydata(x).value + "', "
                End If
            Next

            cmdstring = cmdstring + "CustomerProfile = '" + CustomerNumber.ToString + "', "
            cmdstring = cmdstring + "BOS = '" + BOSnumber.ToString + "', "
            cmdstring = cmdstring + "CheckInNote = '" + CheckInNote.ToString + "'"
            cmdstring = cmdstring + " Where Number = '" + WONumber.ToString + "' AND Store = '" + Me.Store + "'"
            cmd.CommandText = cmdstring
            '  Debug.WriteLine(cmdstring)
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()
        Else
            'INSERT NEW WO
            If CustomerNumber > 0 Then
                Dim WOStart As Integer = persistant.getvalue(persistant.tbl_location, "StartWONum", "code = '" + Me.Store + "'", 0)
                Try
                    conn.Open()
                    cmd.CommandText = "Select CASE WHEN ISNULL(Max(Number)) then " & WOStart.ToString & " else Max(Number) + 1 END from ServiceWO where store = '" + Me.Store + "' and Number >= " & WOStart
                    'cmd.CommandText = "Select MAX(Number) + 1 as NewWONum from ServiceWO where Store = '" + Me.Store + "'"
                    WONumber = CInt(cmd.ExecuteScalar)
                    conn.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try
                conn.Dispose()
                Dim cmdstring As String = "Insert INTO ServiceWO Set "

                For x As Integer = 0 To mydatacount
                    If mydata(x).dbtable = "WO" And mydata(x).changed = True Then
                        cmdstring = cmdstring + mydata(x).dbfeild + " = '" + mydata(x).value + "', "
                    End If
                Next

                cmdstring = cmdstring + "CustomerProfile = '" + CustomerNumber.ToString + "', "
                cmdstring = cmdstring + "BOS = '" + BOSnumber.ToString + "', "
                cmdstring = cmdstring + "CheckInNote = '" + CheckInNote.ToString + "', "
                cmdstring = cmdstring + "Number = '" + WONumber.ToString + "', "
                cmdstring = cmdstring + "Store = '" + Me.Store + "'"
                cmd.CommandText = cmdstring
                Debug.WriteLine(cmdstring)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    'cmd.CommandText = "Select MAX(Number) + 1 as NewWONum from ServiceWO where Store = '" + Me.Store + "'"
                    'WONumber = CInt(cmd.ExecuteScalar)
                    WritetoScreen()
                    conn.Close()
                Catch ex As MySqlException
                    MessageBox.Show(ex.Message)
                End Try
                conn.Dispose()
            Else
                'MessageBox.Show("You must select a customer before this WO will be saved")
            End If
        End If

        For x As Integer = 0 To mydatacount
            mydata(x).value = Replace(mydata(x).value, "\'", "'")
            mydata(x).value = Replace(mydata(x).value, "\;", ";")
            mydata(x).changed = False
        Next


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
                    sender.text = Format(x, "currency")
                End If
            Next
        End If
    End Sub

    Private Sub domath()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter
        conn.ConnectionString = persistant.myconnstring
        Dim tempstring As String
        Dim tempbill As Decimal
        Dim adjustments As Decimal

        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "Select SUM(Amount) from ServicePayments where WO = " + WONumber.ToString + " AND Store = '" + Store + "'"
            mydata(25).value = cmd.ExecuteScalar().ToString

            txtpaid.Text = Format(mydata(25).value, "Currency")
            If DVadjustments.Rows.Count > 0 Then
                cmd.CommandText = "Select SUM(Amount) from ServiceWOadjustment where WO = " + WONumber.ToString + " AND Store = '" + Store + "'"
                adjustments = CDec(cmd.ExecuteScalar().ToString)
            Else
                adjustments = 0
            End If
            txttotaladjustments.Text = Format(adjustments, "Currency")
            txttotaladjustment2.Text = txttotaladjustments.Text
            cmd.CommandText = "SELECT Sum(ServiceIssue.Bill) FROM ServiceWO Inner Join ServiceWOtoIssue ON ServiceWO.Number = ServiceWOtoIssue.WOnumber AND " & _
                                    "ServiceWO.Store = ServiceWOtoIssue.WOstore Inner Join ServiceIssue ON ServiceWOtoIssue.Issue = ServiceIssue.Issue " & _
                                "WHERE ServiceWOtoIssue.WOnumber =  '" & WONumber.ToString & "' AND ServiceWOtoIssue.WOstore =  '" & Store & "' AND ServiceIssue.billcustomer =  'YES'"
            tempstring = cmd.ExecuteScalar().ToString
            If tempstring = "" Then
                tempbill = 0
            Else
                tempbill = CDec(tempstring)
            End If
            If CmbEnviro.SelectedItem = "YES" Then
                txtEnvirofee.Text = Format(11, "Currency")
                tempbill = tempbill + 11
            Else
                txtEnvirofee.Text = Format(0, "Currency")
            End If
            txtBillpretax.Text = Format(tempbill, "Currency")
            tempbill = tempbill + adjustments
            If Store = "EDM" Or Store = "ATL" Or Store = "REN" Or Store = "SYL" Then
                txtbillgst.Text = Format(tempbill * GSTRate, "Currency")
                txtbillpst.Text = Format(tempbill * 0, "Currency")
                txtbill.Text = Format(tempbill * (1 + GSTRate), "Currency")
            Else
                txtbillgst.Text = Format(tempbill * GSTRate, "Currency")
                txtbillpst.Text = Format(tempbill * PSTRate, "Currency")
                txtbill.Text = Format(tempbill * (1 + GSTRate + PSTRate), "Currency")
            End If
            moneychanged(txtbill)
            moneychanged(txtpaid)
            txtbalance.Text = Format((CDec(mydata(24).value) - CDec(mydata(25).value)), "Currency")
            conn.Close()
        Catch ex As Exception

        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub

    Private Sub refreshcallsdataview(ByVal data As DataTable)
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

        Me.DVCalls.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader)

    End Sub
    Private Sub Getnewcalldata()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        'Dim SQL As String = "SELECT Number, Who, time, Note FROM ServiceCallLog where CustProfile = '" + CustomerNumber.ToString + "' ORDER BY time DESC"
        Dim SQL As String = "SELECT Number, Who, time, Note FROM ServiceCallLog " & _
            "Where CallType = 'Work Order' and ParentID = '" & WONumber.ToString & "' and CustProfile = '" & CustomerNumber.ToString & "' ORDER BY time DESC"

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            Callsnewdata.Clear()
            adapter.Fill(Callsnewdata)
            conn.Close()
            refreshcallsdataview(Callsnewdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub

    Private Sub refreshpaymentdataview(ByVal data As DataTable)
        paymentdatatable.Clear()
        paymentdatatable.Merge(data)
        paymentbindingsource.ResetBindings(False)

        If paymentbinded = False Then
            paymentbindingsource.DataSource = paymentdatatable
            DVpayments.DataSource = paymentbindingsource
            paymentbinded = True
        End If
        ' Me.DVpayment.Columns("Note").Width = 220
        ' Me.DVpayment.Columns("Who").Width = 40
        If DVpayments.RowCount > 0 Then
            Me.DVpayments.ColumnHeadersVisible = True
        Else
            Me.DVpayments.ColumnHeadersVisible = False
        End If
        Me.DVpayments.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

    End Sub
    Private Sub GetNewPaymentData()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        Dim SQL As String
        SQL = buildpaymentsql()

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            paymentnewdata.Clear()
            adapter.Fill(paymentnewdata)
            conn.Close()
            refreshpaymentdataview(paymentnewdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub
    Private Function buildpaymentsql() As String
        buildpaymentsql = "SELECT Concat('$',Amount) as 'Amount', CounterSlip, date from ServicePayments where WO = " + WONumber.ToString + " AND Store = '" + Store + "'"
    End Function
    Private Sub RefreshIssueDataView(ByVal data As DataTable)
        Issuedatatable.Clear()
        Issuedatatable.Merge(data)
        Issuebindingsource.ResetBindings(False)

        If Issuebinded = False Then
            Issuebindingsource.DataSource = Issuedatatable
            DVissues.DataSource = Issuebindingsource
            Issuebinded = True
        End If
        ' Me.DVIssue.Columns("Note").Width = 220
        ' Me.DVIssue.Columns("Who").Width = 40
        If DVissues.RowCount > 0 Then
            If persistant.myuserLEVEL > 8 Then
                Me.DVissues.Columns("RI").Visible = True
            Else
                Me.DVissues.Columns("RI").Visible = False
            End If
            Me.DVissues.Columns("View2").Visible = True
            Me.DVissues.ColumnHeadersVisible = True
        Else
            Me.DVissues.Columns("RI").Visible = False
            Me.DVissues.Columns("View2").Visible = False
            Me.DVissues.ColumnHeadersVisible = True
        End If
        Me.DVissues.Columns("Issue").Visible = False
        Me.DVissues.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader)


    End Sub
    Private Sub UpdateIssueState()
        Dim tempxx As String = Me.cmbStatus.SelectedItem
        If Me.cmbStatus.SelectedItem = "Approved Rig" Or Me.cmbStatus.SelectedItem = "Active" Then
            'switch all issues reported to be assigned
            Dim tempissuenumber As String
            For x As Integer = 0 To Issuedatatable.Rows.Count - 1
                If Issuedatatable.Rows(x).Item("Status") = "Reported" Then
                    tempissuenumber = Issuedatatable.Rows(x).Item("Issue").ToString
                    If Issuedatatable.Rows(x).Item("Assigned") = "" Then
                        'move to to be assigned
                        moveissuestatus(tempissuenumber, "To Be Assigned")
                        If Issuedatatable.Rows(x).Item("Type") = "SUBLET" Then
                            moveissuestatus(tempissuenumber, "In Progress")
                        End If
                    Else
                        'Check if quote is needed
                        If getpaymenttype(tempissuenumber) = "Quote Requested" Then
                            'Yep quote it
                            moveissuestatus(tempissuenumber, "To Be Quoted")
                        Else
                            'No, is it warranty?
                            If getpaymenttype(tempissuenumber) = "WARRANTY" Then
                                'yes it is, approved?
                                If getwarrantystatus(tempissuenumber) = "Approved" Then
                                    moveissuestatus(tempissuenumber, "Assigned")
                                Else
                                    'warranty but not approved
                                    moveissuestatus(tempissuenumber, "Warranty Pending")
                                End If
                            Else
                                'No quote needed and no warranty and assigned so get to work
                                moveissuestatus(tempissuenumber, "Assigned")
                            End If
                        End If
                    End If
                Else
                    If IsDBNull(Issuedatatable.Rows(x).Item("Status")) Or IsDBNull(Issuedatatable.Rows(x).Item("Assigned")) Then
                    Else
                        If Issuedatatable.Rows(x).Item("Status") = "To Be Assigned" And Issuedatatable.Rows(x).Item("Assigned") <> "" Then
                            tempissuenumber = Issuedatatable.Rows(x).Item("Issue").ToString
                            moveissuestatus(tempissuenumber, "Assigned")
                        End If
                    End If
                End If

            Next
        End If
    End Sub

    Private Sub GetNewIssueData()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        Dim SQL As String
        SQL = buildissuesql()

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            Issuenewdata.Clear()
            adapter.Fill(Issuenewdata)
            RefreshIssueDataView(Issuenewdata)
            UpdateIssueState()
            Issuenewdata.Clear()
            adapter.Fill(Issuenewdata)
            RefreshIssueDataView(Issuenewdata)

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub
    Private Function buildissuesql() As String
        buildissuesql = "SELECT M.Issue, S.Type, S.Status, S.Approval, S.Assigned, S.Description, M.Num FROM ServiceWOtoIssue AS M Inner Join ServiceIssue AS S ON M.Issue = S.Issue WHERE M.WOnumber =  '" + WONumber.ToString + "' AND M.WOstore =  '" + Store + "' ORDER BY M.Num ASC"
        'SELECT `M`.`Issue`, `S`.`Type`, `S`.`Status`, `S`.`Desc` FROM `ServiceWOtoIssue` AS `M` Inner Join  'ServiceIssue` AS `S` ON `M`.`Issue` = `S`.`Issue` where WOnum = '" + WONumber.ToString + "' AND WOstore = '" + Store + "'"
    End Function

    Private Sub txtleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNotes.Leave
        stringchanged(sender)
    End Sub
    Private Sub cmbleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPriority.SelectionChangeCommitted, CmbEnviro.SelectionChangeCommitted, cmbStatus.SelectionChangeCommitted
        cmbchanged(sender)
        domath()
        WritetoScreen()
    End Sub
    Private Sub datepickerleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateDone.Leave, DateDropOff.Leave, DateToBeDropped.Leave, DateReqComp.ValueChanged
        datechanged(sender)
    End Sub
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
    Private Sub datechanged(ByVal sender As Object)
        Dim data As String
        For i As Integer = 0 To (mydata.Length - 1)
            If sender.Equals(mydata(i).txtbox) = True Then
                data = Microsoft.VisualBasic.Strings.Format(sender.Value, "yyyy-MM-dd H:mm:ss")
                mydata(i).value = data
                mydata(i).changed = True
                If i = 22 Then
                    CalDateReqComp.SetDate(CDate(mydata(22).value))
                End If
            End If
        Next
    End Sub
    Private Sub moveissuestatus(ByVal issue As String, ByVal targetstatus As String)
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter
        Dim SQL As String
        SQL = buildissuesql()
        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "Update ServiceIssue Set Status = '" + targetstatus + "' where Issue = '" + issue + "'"
            cmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub
    Private Function getpaymenttype(ByVal issue As String) As String
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter
        Dim SQL As String
        SQL = buildissuesql()
        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "Select Payment from ServiceIssue where Issue = '" + issue + "'"
            getpaymenttype = cmd.ExecuteScalar()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Function
    Private Function getwarrantystatus(ByVal issue As String) As String
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter
        Dim SQL As String
        SQL = buildissuesql()
        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "Select Approval from ServiceIssue where Issue = '" + issue + "'"
            getwarrantystatus = cmd.ExecuteScalar()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Function

    Private Sub BtnCustData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCustData.Click
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
    Private Sub btnEditCust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditCust.Click
        Dim editcust As New frmEditCustomer
        editcust.persistant = persistant
        editcust.CustomerNumber = CustomerNumber
        editcust.ShowDialog()
        downloadcustdata()
        WritetoScreen()
    End Sub
    Private Sub btnviewbos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnviewbos.Click
        Dim bos As New frmBOS
        bos.persistant = persistant
        bos.mybosstorecode = Store
        bos.mybosnumber = BOSnumber
        bos.downloaddata()
        bos.Show()
    End Sub
    Private Sub btnLogCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogCall.Click
        Dim logcall As New frmCallLog
        logcall.persistant = persistant
        logcall.CustNumber = CustomerNumber
        logcall.ParentID = WONumber
        logcall.CallType = "Work Order"
        logcall.ShowDialog()
        WritetoScreen()
    End Sub
    Private Sub BtnDropoff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDropoff.Click
        If cmbStatus.SelectedItem = "Scheduled" Then
            Dim CheckinNow As New frmCheckIn
            CheckinNow.persistant = persistant
            CheckinNow.Viewnote = False
            CheckinNow.ShowDialog()
            If CheckinNow.DialogResult = Windows.Forms.DialogResult.OK Then
                cmbStatus.SelectedItem = "Active"
                DateDropOff.Value = Date.Now
                cmbchanged(cmbStatus)
                datechanged(DateDropOff)
                CheckInNote = CheckinNow.NoteNumber
                printCheckin(CheckinNow.Note)
            End If
        End If
        If cmbStatus.SelectedItem = "Requested Rig" Then
            cmbStatus.SelectedItem = "Approved Rig"
            cmbchanged(cmbStatus)
            DateDropOff.Value = Date.Now
            datechanged(DateDropOff)
        End If
        funcs.updateWOPrioritylist(WONumber.ToString, Store, persistant.myconnstring)

        WritetoScreen()
    End Sub
    Private Sub btnCheckInNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckInNotes.Click
        Dim CheckinNoteViewer As New frmCheckIn
        CheckinNoteViewer.persistant = persistant
        CheckinNoteViewer.Viewnote = True
        CheckinNoteViewer.NoteNumber = CheckInNote
        CheckinNoteViewer.ShowDialog()
    End Sub
    'Private Sub Savebutton(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    save()
    'End Sub
    Private Sub BtnAddIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddIssue.Click
        save()
        Dim AddIssue As New FrmIssue
        AddIssue.persistant = persistant
        AddIssue.CustomerNumber = CustomerNumber
        AddIssue.Store = Store
        AddIssue.WONumber = WONumber
        AddIssue.IssueNumber = 0
        AddIssue.ShowDialog()
        If AddIssue.IssueNumber > 0 Then
            'an issue was created
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            cmd.Connection = conn
            cmd.CommandText = "Insert Into ServiceWOtoIssue Set WOnumber = '" + WONumber.ToString + "', WOstore = '" + Store + "', Issue = '" + AddIssue.IssueNumber.ToString + "'"
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As Exception
            End Try
            conn.Dispose()
            funcs.updateWOPrioritylist(WONumber.ToString, Store, persistant.myconnstring)
            If cmbStatus.SelectedItem = "Waiting For Pickup" Then
                cmbStatus.SelectedItem = "Active"
                cmbchanged(cmbStatus)
            End If
            save()
            WritetoScreen()
        End If
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
    Private Sub DVIssues_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DVissues.CellContentClick
        Try
            Dim xRow As Integer
            Dim XNum As Integer
            If DVissues.Columns(e.ColumnIndex).Name = "View2" Then
                xRow = e.RowIndex
                XNum = Issuedatatable.Rows(xRow).Item("Issue")
                Dim ViewIssue As New FrmIssue
                ViewIssue.persistant = persistant
                ViewIssue.IssueNumber = XNum
                ViewIssue.CustomerNumber = CustomerNumber
                ViewIssue.Store = Store
                ViewIssue.WONumber = WONumber
                ViewIssue.ShowDialog()
                If WONumber <> 0 Then
                    GetNewIssueData()
                End If
                WritetoScreen()
            ElseIf DVissues.Columns(e.ColumnIndex).Name = "RI" Then
                If MsgBox("Are you sure you want to delete this issue?", MsgBoxStyle.YesNo, "Delete Confirmation") = MsgBoxResult.Yes Then
                    'remove the issue
                    xRow = e.RowIndex
                    Dim swoi As Integer = Issuedatatable.Rows(xRow).Item("Num")
                    Dim sii As Integer = Issuedatatable.Rows(xRow).Item("Issue")
                    Dim conn As New MySqlConnection
                    Dim cmd As New MySqlCommand
                    conn.ConnectionString = persistant.myconnstring
                    cmd.Connection = conn
                    'Dim cmdstring As String = "DELETE from ServicePics where PicID = " & pid
                    cmd.CommandText = "DELETE from ServiceWOtoIssue where Num = " & swoi
                    'Debug.WriteLine(cmdstring)
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        cmd.CommandText = "DELETE from ServiceIssue where Issue = " & sii
                        cmd.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                    conn.Dispose()
                    'refresh the picture list
                    If WONumber <> 0 Then
                        GetNewIssueData()
                    End If
                    WritetoScreen()

                End If

            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub FrmWO_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        save()
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim pay As New frmlogpayment
        pay.WO = WONumber
        pay.PO = 0
        pay.Store = Store
        pay.persistant = persistant
        pay.ShowDialog()
        WritetoScreen()
    End Sub
    Private Sub closewo()
        If cmbStatus.SelectedItem <> "Waiting For Pickup" Then
            funcs.wodoneremovefrompriority(WONumber.ToString, Store, persistant.myconnstring)
            DateDone.Value = DateTime.Now
            datechanged(DateDone)
        End If

        cmbStatus.SelectedItem = "Closed"
        cmbchanged(cmbStatus)

        WritetoScreen()

    End Sub

    Private Sub btnCompleted_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompleted.Click
        ' If txtbalance.Text = "$0.00" Then
        Dim anynotdone As Boolean = False
        For x As Integer = 0 To DVissues.Rows.Count - 1
            If Issuedatatable.Rows(x).Item("Status") <> "Completed" Then
                anynotdone = True
            End If
        Next
        If anynotdone = True Then
            If MessageBox.Show("Are you sure you want to close this work order? Some issues are not resolved.", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                closewo()

            End If
        Else
            closewo()
        End If
        '    Else
        '  MessageBox.Show("You can't close a work order until the outstanding balance is $0.00")
        '  End If

        WritetoScreen()
    End Sub

    Private Sub btnreadytogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreadytogo.Click
        cmbStatus.SelectedItem = "Waiting For Pickup"
        cmbchanged(cmbStatus)
        DateDone.Value = DateTime.Now
        datechanged(DateDone)
        funcs.wodoneremovefrompriority(WONumber.ToString, Store, persistant.myconnstring)
        WritetoScreen()
    End Sub

    Private Sub printCheckin(ByVal note As String)
        save()
        Dim issuedatax As New IssueData
        Dim posx, posy, align As Integer
        Dim txtboxname As String ' text,
        Dim size As Integer
        Dim inputfile, savelocation As String
        Dim imagex As Image
        inputfile = persistant.installdir + "\resources\contracts\BlankWO.pdf"
        Dim storeName As String = "Shipwreck"

        Select Case Store
            Case "ATL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\ATLlogo.PNG")
                storeName = "Atlantis"
            Case "KEL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\ATLlogo.PNG")
                storeName = "Atlantis"
            Case "BTH"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\BTHlogo.PNG")
                storeName = "Boathouse"
            Case "GAL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\GALlogo.PNG")
                storeName = "Galleon"
            Case "OGO"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\OGOlogo.PNG")
                storeName = "Ogopogo"
            Case "REN"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\RENlogo.PNG")
                storeName = "Renfrew"
            Case "EDM"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\EDMlogo.PNG")
                storeName = "Shipwreck"
            Case "SYL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\SYLlogo.PNG")
                storeName = "Lighthouse"

                'Case "ATL"
                '    imagex = Image.GetInstance(persistant.installdir + "\resources\images\ATLlogo.PNG")
                '    officeName = "Atlantis"
                'Case "KEL"
                '    imagex = Image.GetInstance(persistant.installdir + "\resources\images\ATLlogo.PNG")
                '    officeName = ""
                'Case "BTH"
                '    imagex = Image.GetInstance(persistant.installdir + "\resources\images\BTHlogo.PNG")
                '    officeName = ""
                'Case "GAL"
                '    imagex = Image.GetInstance(persistant.installdir + "\resources\images\GALlogo.PNG")
                '    officeName = ""
                'Case "OGO"
                '    imagex = Image.GetInstance(persistant.installdir + "\resources\images\OGOlogo.PNG")
                '    officeName = ""
                'Case "REN"
                '    imagex = Image.GetInstance(persistant.installdir + "\resources\images\RENlogo.PNG")
                '    officeName = ""
                'Case "EDM"
                '    imagex = Image.GetInstance(persistant.installdir + "\resources\images\EDMlogo.PNG")
                '    officeName = ""
                'Case "SYL"
                '    imagex = Image.GetInstance(persistant.installdir + "\resources\images\SYLlogo.PNG")
                '    officeName = ""
        End Select

        If Not Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO") Then Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO")
        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO\" + Store + "-" + WONumber.ToString + "-CheckIn.pdf"

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
        imagex.ScalePercent(65)
        doc.Add(imagex)
        txtboxname = "WO-number"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WOX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WOY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        size = 12
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, WONumber.ToString, posx, posy, 0)
        cb.EndText()
        'Personal Data
        For x As Integer = 0 To 16
            txtboxname = "WO-" + mydata(x).txtbox.name
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, "WOX", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, "WOY", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                size = 10
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                cb.EndText()
            End If
        Next
        'Date Data
        For x As Integer = 20 To 22
            txtboxname = "WO-" + mydata(x).txtbox.name
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, "WOX", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, "WOY", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                size = 10
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(mydata(x).txtbox.value, "dd/MM/yyyy"), posx, posy, 0)
                cb.EndText()
            End If
        Next

        Dim issuenum As Integer
        issuedatax.myconnstring = persistant.myconnstring
        Dim p1 As New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14)))
        Dim p2 As New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14)))
        Dim p3 As New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14)))
        Dim blankp As New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14)))
        p1.SpacingBefore = 260


        For x As Integer = 0 To Issuedatatable.Rows.Count - 1
            'Print an issue
            issuenum = Issuedatatable.Rows(x).Item("Issue")
            issuedatax.IssueNumber = issuenum
            issuedatax.Download()

            p1.Add("Type of Work:   ")
            p1.Add(issuedatax.getvalue("Type"))
            p1.Add("                 ")
            p1.Add("Payment Type:   ")
            p1.Add(issuedatax.getvalue("Payment"))
            doc.Add(p1)
            doc.Add(blankp)
            p2.Add(issuedatax.getvalue("Description"))
            doc.Add(p2)
            doc.Add(blankp)
            p3.Add("_____________________________________________________________________________")
            doc.Add(p3)
            doc.Add(blankp)
            p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14)))
            p2 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14)))
            p3 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14)))
        Next
        doc.Add(blankp)
        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14)))
        p1.Add("Damage Noted During Check In Process:")
        doc.Add(p1)
        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))

        p1.Add(note)
        doc.Add(p1)
        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))

        'inputfile = persistant.installdir + "\resources\Contracts\condWO.pdf"
        'reader = New PdfReader(inputfile)
        'doc.NewPage()
        'Import = writer.GetImportedPage(reader, 1)
        'cb.AddTemplate(Import, 0, 0)

        inputfile = persistant.installdir + "\resources\Contracts\BlankPage.pdf"
        reader = New PdfReader(inputfile)
        doc.NewPage()
        Import = writer.GetImportedPage(reader, 1)
        cb.AddTemplate(Import, 0, 0)

        'cb.BeginText()
        'cb.SetFontAndSize(bf, 18)
        'cb.ShowTextAligned(1, "SERVICE WORK CONDITIONS", 300, 700, 0)
        'cb.EndText()


        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 16)))
        p1.SpacingBefore = 130
        blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
        p1.Add("                                    SERVICE WORK CONDITIONS")
        doc.Add(p1)
        doc.Add(blankp)

        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11)))
        blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9)))
        p1.Add("1. The Customer is responsible for the REMOVAL OF ALL LOOSE ITEMS from the boat, in order to allow the mechanics proper access to the boat interior.  Dealership is not responsible for loss of, theft of, or damage to any items left in the boat. ")
        doc.Add(p1)
        doc.Add(blankp)

        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11)))
        blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9)))
        p1.Add("2. Dealership will make every effort to ensure that work is performed within the estimated time frame, but we are UNABLE TO GUARANTEE AN EXACT COMPLETION DATE. ")
        doc.Add(p1)
        doc.Add(blankp)

        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11)))
        blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9)))
        p1.Add("3. Dealership's liability for work incorrectly performed is limited only to the repair of the faulty work. Dealership will not reimburse the customer for CORRECTIVE WORK done by other shops. After service work has been performed, the customer is advised to lake test their boat and motor in the local area before leaving on long trips or holidays.  Dealership will not compensate the customer for travel costs, inconvenience, or loss of time or holidays resulting from work performed incorrectly. ")
        doc.Add(p1)
        doc.Add(blankp)

        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11)))
        blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9)))
        p1.Add("4. " & storeName & " is RESPONSIBLE ONLY FOR THE WORK THAT IS BILLED to the customer on this invoice. Dealership will not assume responsibility for any liabilities or damages resulting from work not billed to the customer on this invoice. ")
        doc.Add(p1)
        doc.Add(blankp)

        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11)))
        blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9)))
        p1.Add("5. Dealership reserves the RIGHT NOT TO COMPLETE A WORK ORDER if for any reason Dealership staff determines that the work should not continue. The customer is responsible for the billed time plus parts to that point. ")
        doc.Add(p1)
        doc.Add(blankp)

        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11)))
        blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9)))
        p1.Add("6. STORAGE FEES of $25 per day will be charged on unclaimed items starting one day after the customer has been informed that the service work has been completed. The customer must approve a quote for repairs within 3 days of receiving that quote to avoid storage fees. Any item not picked up after 30 days is subject to sale by the dealership at prices determined by the dealership, to cover the repair costs. ")
        doc.Add(p1)
        doc.Add(blankp)

        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11)))
        blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9)))
        p1.Add("7. The customer is responsible for ensuring that the boat is correctly attached to it's trailer and that the BOAT AND TRAILER ARE CORRECTLY ATTACHED to the tow vehicle before leaving the dealership’s service area.  Dealership staff will assist the customer if time permits, but will not assume responsibility for the correct and safe attachment of the boat, motor, and trailer to the customer's tow vehicle or the correct tarping of the boat . ")
        doc.Add(p1)
        doc.Add(blankp)

        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11)))
        blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9)))
        p1.Add("8.THE CUSTOMER MUST INSURE his boat, motor, trailer, and contents for all losses or damages including, but not limited to: weather damage, fire, theft, or vandalism that might occur while their boat is in for service. ")
        doc.Add(p1)
        doc.Add(blankp)

        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11)))
        blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9)))
        p1.Add("9. I hereby WAIVE MY RIGHT TO A WRITTEN ESTIMATE as afforded me by the consumer protection act. ")
        doc.Add(p1)
        doc.Add(blankp)

        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11)))
        blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9)))
        p1.Add("10. This dealership will not be held responsible for the cleanliness of my boat if dropped off without proper storage covers properly secured to my boat. ")
        doc.Add(p1)
        doc.Add(blankp)

        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_BOLD, 9)))
        blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8)))
        p1.Add("I have read and understand the conditions listed on this form. Until full payment is made and received on this work order I acknowledge the existence of a repair lien on the watercraft described herein. I also acknowledge that the said lien shall continue in force at all times whether the watercraft is in my possession or not, until the work order is paid in full. ")
        doc.Add(p1)
        doc.Add(blankp)

        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
        'blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14)))
        p1.Add("STAFF:  __________________________ CUSTOMER: ___________________________  DATE: _______________ ")
        doc.Add(p1)

        p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9)))
        'blankp = New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14)))
        p1.Add("                            PLEASE PRINT PLEASE SIGN ")
        doc.Add(p1)
        'doc.Add(blankp)

        inputfile = persistant.installdir + "\resources\Contracts\BoatInspection.pdf"
        reader = New PdfReader(inputfile)
        doc.NewPage()
        Import = writer.GetImportedPage(reader, 1)
        cb.AddTemplate(Import, 0, 0)

        'fill in the known information
        posx = 75
        'posy = 676
        align = 0
        size = 12
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        'text = txtbuyer1last.Text & IIf(txtbuyer1first.Text <> "", ",", "")

        'Name
        cb.ShowTextAligned(align, mydata(0).txtbox.text, posx, 708, 0)
        'Date
        cb.ShowTextAligned(align, Format(mydata(21).txtbox.value, "dd/MM/yyyy"), posx, 671, 0)
        'Work Order #
        cb.ShowTextAligned(align, WONumber.ToString(), posx, 652, 0)
        posx = 420
        'Boat Make / Model
        cb.ShowTextAligned(align, mydata(8).txtbox.text & " / " & mydata(9).txtbox.text, posx, 708, 0)
        'Boat Serial #
        cb.ShowTextAligned(align, mydata(10).txtbox.text, posx, 689, 0)
        'Engine Model
        cb.ShowTextAligned(align, mydata(11).txtbox.text & " / " & mydata(12).txtbox.text, posx, 671, 0)
        'Engine Serial #
        cb.ShowTextAligned(align, mydata(13).txtbox.text, posx, 652, 0)
        cb.EndText()


        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    'Private Function printConditions(ByVal office As String) As PdfReader

    '    Dim inputfile As String = persistant.installdir + "\resources\Contracts\condWO.pdf"
    '    Dim reader As New PdfReader(inputfile)
    '    doc.NewPage()
    '    Import = writer.GetImportedPage(reader, 1)
    '    cb.AddTemplate(Import, 0, 0)

    'End Function

    Private Sub printFinal()
        Dim partstotal As Decimal = 0
        Dim partsissuetotal As Decimal = 0
        Dim worktotal As Decimal = 0
        Dim sublettotal As Decimal = 0
        Dim feestotal As Decimal = 0
        Dim issuetotal As Decimal = 0
        Dim adjustmenttotal As Decimal = 0

        save()
        Dim issuedatax As New IssueData
        Dim posx, posy, align As Integer
        Dim text, txtboxname As String
        Dim size As Integer
        Dim inputfile, savelocation As String
        Dim imagex As Image
        inputfile = persistant.installdir + "\resources\contracts\BlankWO.pdf"
        Dim storeName As String = ""

        Select Case Store
            Case "ATL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\ATLlogo.PNG")
                storeName = "Atlantis"
            Case "KEL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\ATLlogo.PNG")
                storeName = "Atlantis"
            Case "BTH"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\BTHlogo.PNG")
                storeName = "Boathouse"
            Case "GAL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\GALlogo.PNG")
                storeName = "Galleon"
            Case "OGO"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\OGOlogo.PNG")
                storeName = "Ogopogo"
            Case "REN"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\RENlogo.PNG")
                storeName = "Renfrew"
            Case "EDM"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\EDMlogo.PNG")
                storeName = "Shipwreck"
            Case "SYL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\SYLlogo.PNG")
                storeName = "Lighthouse"
        End Select

        If Not Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO") Then Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO")
        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO\" + Store + "-" + WONumber.ToString + "-Bill.pdf"

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
        imagex.ScalePercent(65)
        doc.Add(imagex)
        txtboxname = "WO-number"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WOX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WOY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        size = 12
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, WONumber.ToString, posx, posy, 0)
        cb.EndText()
        'Personal Data
        For x As Integer = 0 To 16
            txtboxname = "WO-" + mydata(x).txtbox.name
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, "WOX", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, "WOY", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                size = 10
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                cb.EndText()
            End If
        Next
        'Date Data
        For x As Integer = 20 To 22
            txtboxname = "WO-" + mydata(x).txtbox.name
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, "WOX", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, "WOY", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                size = 10
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(mydata(x).txtbox.value, "dd/MM/yyyy"), posx, posy, 0)
                cb.EndText()
            End If
        Next
        'date completed
        txtboxname = "WO-" + mydata(23).txtbox.name
        If cmbStatus.SelectedItem = "Waiting For Pickup" Or cmbStatus.SelectedItem = "Void" Or cmbStatus.SelectedItem = "Closed" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "WOX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "WOY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 10
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(mydata(23).txtbox.value, "dd/MM/yyyy"), posx, posy, 0)
            cb.EndText()
        End If
        Dim issuenum As Integer
        issuedatax.myconnstring = persistant.myconnstring
        Dim p1 As New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))
        Dim p1a As New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))
        Dim p1b As New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))
        Dim p1c As New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))
        Dim p2 As New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))
        Dim p3 As New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_BOLD, 14)))
        Dim p4 As New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
        Dim p5 As New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))
        Dim blankp As New Paragraph(New Chunk(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))
        p1.SpacingBefore = 260
        Dim partext As Decimal
        Dim widths(4) As Single
        widths(0) = 0.1
        widths(1) = 0.2
        widths(2) = 0.3
        widths(3) = 0.2
        widths(4) = 0.2
        Dim customertopay As Boolean
        For x As Integer = 0 To Issuedatatable.Rows.Count - 1
            partsissuetotal = 0
            issuetotal = 0

            'Print an issue
            issuenum = Issuedatatable.Rows(x).Item("Issue")
            issuedatax.IssueNumber = issuenum
            issuedatax.Download()

            If issuedatax.getvalue("billcustomer") = "YES" Then
                customertopay = True
            Else
                customertopay = False
            End If


            p1.Add(New Chunk("Type of Work:   ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12)))
            p1.Add(New Chunk(issuedatax.getvalue("Type"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
            p1.Add(New Chunk("   Payment Type:   ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12)))
            p1.Add(New Chunk(issuedatax.getvalue("Payment"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
            If issuedatax.getvalue("Payment") = "WARRANTY" Or issuedatax.getvalue("Payment") = "Quote Requested" Then
                p1.Add(New Chunk("   Approval Status:   ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12)))
                p1.Add(New Chunk(issuedatax.getvalue("Approval"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
            End If
            doc.Add(p1)
            If issuedatax.getvalue("Payment") = "WARRANTY" Then 'Or issuedatax.getvalue("Payment") = "Quote Requested"
                p1c.Add(New Chunk(" Warrany Type:  ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12)))
                p1c.Add(New Chunk(issuedatax.getvalue("WarrantyType"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                p1c.Add(New Chunk("        Credit #:  ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12)))
                p1c.Add(New Chunk(issuedatax.getvalue("CreditNum"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                p1a.Add(New Chunk("Requested Amt:  ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12)))
                p1a.Add(New Chunk(Format(issuedatax.getvalue("RequestedAmt"), "Currency"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                p1a.Add(New Chunk("   Requested Hrs:     ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12)))
                p1a.Add(New Chunk(issuedatax.getvalue("RequestedHrs"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                p1b.Add(New Chunk(" Approved Amt:  ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12)))
                p1b.Add(New Chunk(Format(issuedatax.getvalue("ApprovedAmt"), "Currency"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                p1b.Add(New Chunk("    Approved Hrs:     ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12)))
                p1b.Add(New Chunk(issuedatax.getvalue("ApprovedHrs"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                doc.Add(p1c)
                doc.Add(p1a)
                doc.Add(p1b)
            End If
            doc.Add(blankp)
            p3.Add("Description of Problem:")
            doc.Add(p3)
            p3 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12)))
            p2.Add(issuedatax.getvalue("Description"))
            doc.Add(blankp)
            doc.Add(p2)
            p2 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
            p3.Add("Summary of Work Performed:")
            doc.Add(p3)
            p3 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12)))
            p2.Add(issuedatax.getvalue("Workdone"))
            doc.Add(blankp)
            doc.Add(p2)
            p2 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
            doc.Add(blankp)
            'Parts -  
            'exclude if the issue is a quote
            If Not issuedatax.getvalue("Payment") = "Quote Requested" Then
                If issuedatax.parts.Rows.Count > 0 Then
                    p3.Add("Parts Used:")
                    doc.Add(p3)
                    doc.Add(blankp)
                    p3 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12)))
                    Dim table As PdfPTable = New PdfPTable(5)
                    table.AddCell("QTY")
                    table.AddCell("PART #")
                    table.AddCell("DESCRIPTION")
                    table.AddCell("PRICE")

                    table.AddCell("EXT PRICE")
                    For y As Integer = 0 To issuedatax.parts.Rows.Count - 1
                        table.AddCell(issuedatax.parts.Rows(y).Item("Quantity"))
                        table.AddCell(issuedatax.parts.Rows(y).Item("PartNumber"))
                        table.AddCell(issuedatax.parts.Rows(y).Item("Part"))
                        table.AddCell(Format(issuedatax.parts.Rows(y).Item("Price"), "Currency"))
                        If customertopay Then
                            partext = CDec(issuedatax.parts.Rows(y).Item("Price")) * CDec(issuedatax.parts.Rows(y).Item("Quantity"))
                        Else
                            partext = 0
                        End If
                        table.AddCell(Format(partext, "Currency"))
                        partstotal = partstotal + partext
                        partsissuetotal = partsissuetotal + partext
                    Next
                    table.SetWidths(widths)
                    doc.Add(table)
                    doc.Add(blankp)

                    issuetotal = issuetotal + partsissuetotal
                End If
            End If

            If issuedatax.getvalue("Type") <> "Standard Service (PREPAID)" Then
                If issuedatax.getvalue("Type") = "Standard Service" Then
                    'Do std service line
                    If customertopay Then
                        worktotal = worktotal + CDec(issuedatax.getvalue("servicepkgprice"))
                        issuetotal = issuetotal + CDec(issuedatax.getvalue("servicepkgprice"))
                        p4.Add("                                         Standard Service:         ")
                        p4.Add(Format(issuedatax.getvalue("servicepkgprice"), "Currency"))
                        p4.SetAlignment("Right")
                        doc.Add(p4)
                        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                    Else
                        p4.Add("                                         Standard Service:         ")
                        p4.Add(Format(0, "Currency"))
                        p4.SetAlignment("Right")
                        doc.Add(p4)
                        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                    End If

                Else
                    'removed in version 3.85
                    'If CDec(issuedatax.getvalue("SubletCost")) * CDec(issuedatax.getvalue("SubletMarkup")) > 0 Then
                    '    'do sublet line
                    '    If customertopay Then
                    '        sublettotal = sublettotal + CDec(issuedatax.getvalue("SubletCost") * CDec(issuedatax.getvalue("SubletMarkup")))
                    '        issuetotal = issuetotal + CDec(issuedatax.getvalue("SubletCost") * CDec(issuedatax.getvalue("SubletMarkup")))

                    '        p4.Add("                                 Sublet Work Perfomed:         ")
                    '        p4.Add(Format(CDec(issuedatax.getvalue("SubletCost")) * CDec(issuedatax.getvalue("SubletMarkup")), "Currency"))
                    '        p4.SetAlignment("Right")
                    '        doc.Add(p4)
                    '        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                    '    Else
                    '        p4.Add("                                 Sublet Work Perfomed:         ")
                    '        p4.Add(Format(0, "Currency"))
                    '        p4.SetAlignment("Right")
                    '        doc.Add(p4)
                    '        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                    '    End If


                    'Else
                    'Do labour line
                    If customertopay Then
                        worktotal = worktotal + CDec(issuedatax.getvalue("QuoteRate")) * CDec(issuedatax.getvalue("BillableHrs"))
                        issuetotal = issuetotal + CDec(issuedatax.getvalue("QuoteRate") * CDec(issuedatax.getvalue("BillableHrs")))
                        'added in version 3.85
                        sublettotal = sublettotal + CDec(issuedatax.getvalue("SubletCost") * CDec(issuedatax.getvalue("SubletMarkup")))

                        p4.Add("                                         Work Perfomed:         ")
                        p4.Add(Format(CDec(issuedatax.getvalue("QuoteRate")) * CDec(issuedatax.getvalue("BillableHrs")), "Currency"))
                        p4.SetAlignment("Right")
                        doc.Add(p4)
                        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                    Else
                        p4.Add("                                         Work Perfomed:         ")
                        p4.Add(Format(0, "Currency"))
                        p4.SetAlignment("Right")
                        doc.Add(p4)
                        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                    End If

                    'End If
                End If
            Else
            p4.Add("Standard Service:         PREPAID")
            p4.SetAlignment("Right")
            doc.Add(p4)

            End If

            'parts total
            If partsissuetotal > 0 Then
                p4.Add("                                         Parts:         ")
                p4.Add(Format(partsissuetotal, "Currency"))
                p4.SetAlignment("Right")
                doc.Add(p4)
                p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
            End If

            'fees

            If issuedatax.getvalue("Type") = "Standard Service" Or issuedatax.getvalue("Type") = "Standard Service (PREPAID)" Then
                'Do std service Fees
                p4.Add("              Shop Supplies:         ")
                p4.Add(Format((CDec(issuedatax.getvalue("QuoteRate")) * CDec(issuedatax.getvalue("BillableHrs"))) * 0.0, "Currency"))
                p4.SetAlignment("Right")
                doc.Add(p4)
                p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                feestotal = feestotal + (CDec(issuedatax.getvalue("QuoteRate")) * CDec(issuedatax.getvalue("BillableHrs"))) * 0.0
                issuetotal = issuetotal + (CDec(issuedatax.getvalue("QuoteRate")) * CDec(issuedatax.getvalue("BillableHrs"))) * 0.0
            Else
                'removed in version 3.85
                'If issuedatax.getvalue("Type") = "SUBLET" Then
                '    'do sublet Fees
                'Else
                'Do labour Fees
                If customertopay Then
                    p4.Add("              Shop Supplies:         ")
                    p4.Add(Format((CDec(issuedatax.getvalue("QuoteRate")) * CDec(issuedatax.getvalue("BillableHrs"))) * shopSuppliesRate, "Currency"))
                    p4.SetAlignment("Right")
                    doc.Add(p4)
                    p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                    feestotal = feestotal + (CDec(issuedatax.getvalue("QuoteRate")) * CDec(issuedatax.getvalue("BillableHrs"))) * shopSuppliesRate
                    issuetotal = issuetotal + (CDec(issuedatax.getvalue("QuoteRate")) * CDec(issuedatax.getvalue("BillableHrs"))) * shopSuppliesRate
                Else
                    p4.Add("              Shop Supplies:         ")
                    p4.Add(Format(0, "Currency"))
                    p4.SetAlignment("Right")
                    doc.Add(p4)
                    p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
                End If

                'End If
            End If

            'subtotal
            p4.Add("              Sub Total:         ")
            p4.Add(Format(issuetotal, "Currency"))
            p4.SetAlignment("Right")
            doc.Add(p4)
            p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))

            p5.Add("_____________________________________________________________________________")
            doc.Add(p5)
            doc.Add(blankp)
            p1 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
            p1a = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
            p1b = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
            p1c = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
            p2 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
            p5 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))


        Next


        'Total Labour
        p4.Add("              Labour Total:         ")
        p4.Add(Format(worktotal, "Currency"))
        p4.SetAlignment("Right")
        doc.Add(p4)
        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))

        'Total Sublet
        p4.Add("              Sublet Total:         ")
        p4.Add(Format(sublettotal, "Currency"))
        p4.SetAlignment("Right")
        doc.Add(p4)
        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))


        'Total Parts
        p4.Add("              Parts Total:         ")
        p4.Add(Format(partstotal, "Currency"))
        p4.SetAlignment("Right")
        doc.Add(p4)
        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))


        'Total fees
        p4.Add("              Total Shop Supplies:         ")
        p4.Add(Format(feestotal, "Currency"))
        p4.SetAlignment("Right")
        doc.Add(p4)
        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))
        If CmbEnviro.SelectedItem = "YES" Then

            'Evirofee
            p4.Add("              Enviro Fee:         ")
            p4.Add(Format(11, "Currency"))
            p4.SetAlignment("Right")
            doc.Add(p4)
            p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))

            feestotal = feestotal + 11
        End If
        adjustmenttotal = 0

        'Show adjustments, total them up.
        For x As Integer = 0 To DVadjustments.RowCount - 1
            p4.Add(DVadjustments.Item("Reason", x).Value + ":         ")
            p4.Add(Format(CDec(Replace(DVadjustments.Item("Amount", x).Value, "$", "")), "Currency"))
            p4.SetAlignment("Right")
            doc.Add(p4)
            p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))
            doc.Add(blankp)
            adjustmenttotal = adjustmenttotal + CDec(Replace(DVadjustments.Item("Amount", x).Value, "$", ""))

        Next


        'Total before tax
        p4.Add("              Sub Total:         ")
        p4.Add(Format(worktotal + sublettotal + partstotal + feestotal + adjustmenttotal, "Currency"))
        p4.SetAlignment("Right")
        doc.Add(p4)
        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))
        doc.Add(blankp)


        If Me.Store = "OGO" Or Me.Store = "GAL" Or Me.Store = "BTH" Or Me.Store = "KEL" Then
            'PST
            p4.Add("              PST:         ")
            p4.Add(Format((worktotal + sublettotal + partstotal + feestotal + adjustmenttotal) * PSTRate, "Currency"))
            p4.SetAlignment("Right")
            doc.Add(p4)
            p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))
            'GST
            p4.Add("              GST:         ")
            p4.Add(Format((worktotal + sublettotal + partstotal + feestotal + adjustmenttotal) * GSTRate, "Currency"))
            p4.SetAlignment("Right")
            doc.Add(p4)
            p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))
            'TOTAL
            p4.Add("              TOTAL:         ")
            p4.Add(Format((worktotal + sublettotal + partstotal + feestotal + adjustmenttotal) * (1 + PSTRate + GSTRate), "Currency"))
            p4.SetAlignment("Right")
            doc.Add(p4)
            p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13)))

        Else
            'GST
            p4.Add("              GST:         ")
            p4.Add(Format((worktotal + sublettotal + partstotal + feestotal + adjustmenttotal) * GSTRate, "Currency"))
            p4.SetAlignment("Right")
            doc.Add(p4)
            p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12)))

            'TOTAL
            p4.Add("              TOTAL:         ")
            p4.Add(Format((worktotal + sublettotal + partstotal + feestotal + adjustmenttotal) * (1 + GSTRate), "Currency"))
            p4.SetAlignment("Right")
            doc.Add(p4)
            p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13)))
        End If

        doc.Add(blankp)
        doc.Add(blankp)
        doc.Add(blankp)
        doc.Add(blankp)
        doc.Add(blankp)

        p4.Add("              ___________________________________         ")
        p4.SetAlignment("Left")
        doc.Add(p4)
        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13)))
        p4.Add("                   Customer Signature         ")
        p4.SetAlignment("Left")
        doc.Add(p4)
        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13)))
        'p4.Add("CUSTOMER MUST INSTALL BOAT PLUG BEFORE LAUNCHING")

        p4.Add("During the process of the this work order, I realize that the technician may have removed the plug for my boat. I understand that it is my responsibility to check my plug before launching my boat. " & vbNewLine & vbNewLine & _
            "I realize that it is also my responsibility to ask for any parts that I have ordered, and any special requests such as battery removal, etc. " & storeName & " Marine will not be held responsible for the shipping of any parts, accessories or batteries that I did not ask for and take with me upon picking up my boat.")
        p4.SetAlignment("Left")
        doc.Add(p4)
        p4 = New Paragraph(New Chunk("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13)))

        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        printFinal()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim pick As New frmSalesman
        pick.persistant = persistant
        Dim wostorelong As String = persistant.getvalue(persistant.tbl_location, "Store", "CODE = '" + Store + "'", 0)
        pick.filter = "usergroup < 0"
        pick.ShowDialog()
        'If wostorelong = "Shipwreck Edmonton" Or wostorelong = "Atlantis" Then
        '    pick.filter = "usergroup < 0 AND ( store = 'Atlantis' OR store = 'Shipwreck Edmonton' )"
        '    pick.ShowDialog()
        'Else
        '    pick.filter = "usergroup < 0 AND store = '" + wostorelong + "'"
        '    pick.ShowDialog()
        'End If
        If persistant.temppass <> "None" Then
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            For x As Integer = 0 To Issuedatatable.Rows.Count - 1
                If Issuedatatable.Rows(x).Item("Type") <> "SUBLET" Then
                    If Issuedatatable.Rows(x).Item("Assigned") = "" Then
                        'assign it to temppass
                        conn.ConnectionString = persistant.myconnstring
                        cmd.Connection = conn
                        cmd.CommandText = "Update ServiceIssue Set Assigned = '" + persistant.getvalue(persistant.tbl_users, "user", "name = '" + persistant.temppass + "'", 0) + "' WHERE Issue = " + Issuedatatable.Rows(x).Item("Issue").ToString
                        Try
                            conn.Open()
                            cmd.ExecuteNonQuery()
                            conn.Close()
                        Catch ex As Exception
                        End Try
                        conn.Dispose()

                    End If
                End If
            Next
            WritetoScreen()
            funcs.updateWOPrioritylist(WONumber.ToString, Store, persistant.myconnstring)

        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If Me.listpdiforms.SelectedItems.Count > 0 Then
            For x As Integer = 0 To Me.listpdiforms.Items.Count - 1
                If listpdiforms.SelectedItems.Contains(listpdiforms.Items.Item(x)) Then printPDI(Me.listpdiforms.Items.Item(x))
            Next
        End If
    End Sub
    Private Sub printPDI(ByVal form As String)

        '### DO PRINT
        save()

        Dim posx, posy, align As Integer
        Dim text, txtboxname, datacode As String
        Dim size As Integer
        Dim inputfile, savelocation As String
        datacode = "PDI"

        If Not Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO") Then Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO")
        Select Case form
            Case "Boat and sterndrive"
                inputfile = persistant.installdir + "\resources\Contracts\PDIBandS.pdf"

                savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO" + Me.WONumber.ToString + "-" + Me.Store + "+PDIbs.pdf"

            Case "Boat and outboard"
                inputfile = persistant.installdir + "\resources\Contracts\PDIBandO.pdf"

                savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO" + Me.WONumber.ToString + "-" + Me.Store + "+PDIbo.pdf"

            Case "Small outboard"
                inputfile = persistant.installdir + "\resources\Contracts\PDIsm.pdf"

                savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO" + Me.WONumber.ToString + "-" + Me.Store + "+PDIo.pdf"

            Case "Trailer"
                inputfile = persistant.installdir + "\resources\Contracts\PDIT.pdf"

                savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO" + Me.WONumber.ToString + "-" + Me.Store + "+PDIt.pdf"

            Case "In-Service Checklist 2013"
                inputfile = persistant.installdir + "\resources\Contracts\InServiceChecklist2013.pdf"

                savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\WO" + Me.WONumber.ToString + "-" + Me.Store + "+InSericeChecklist2013.pdf"

        End Select


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

        size = 10

        If form = "In-Service Checklist 2013" Then
            'boat brand
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.TxtBoatMake.Text, 370, 727, 0)
            cb.EndText()

            'Cust name
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtName.Text, 130, 705, 0)
            cb.EndText()

            'Date
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(Now, "MMM dd/yyyy"), 510, 705, 0)
            cb.EndText()

            'dealer name and location
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(0, persistant.getvalue(persistant.tbl_location, "store", "code = '" + Store + "'", 0), 130, 692, 0)
            cb.EndText()

            'Boat model
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtboatmodel.Text, 130, 679, 0)
            cb.EndText()

            'Boat serial num
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtBoatSerial.Text, 400, 679, 0)
            cb.EndText()

            'Engine make/model
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtmotormake.Text & " / " & Me.txtmotormodel.Text, 130, 667, 0)
            cb.EndText()

            'Engine serial
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtmotorserial.Text, 130, 654, 0)
            cb.EndText()

            'Cust phone
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtphone1.Text, 170, 133, 0)
            cb.EndText()

            'Cust address
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtAddress.Text, 170, 115, 0)
            cb.EndText()

            'Cust email
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.Txtemail.Text, 170, 97, 0)
            cb.EndText()

        Else
            txtboxname = "PDIstore"
            If form = "Trailer" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            Else
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            End If
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_location, "store", "code = '" + Store + "'", 0), posx, posy, 0)
            cb.EndText()

            size = 8

            txtboxname = "PDIcust"
            If form = "Trailer" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            Else
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            End If
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtName.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "PDIbos"
            If form = "Trailer" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            Else
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            End If
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.BOSnumber.ToString, posx, posy, 0)
            cb.EndText()


            txtboxname = "PDIbos"
            If form = "Trailer" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            Else
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            End If
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.BOSnumber.ToString, posx, posy, 0)
            cb.EndText()

            txtboxname = "PDIwonum"
            If form = "Trailer" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            Else
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            End If
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.WONumber.ToString, posx, posy, 0)
            cb.EndText()

            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            Dim cmdstring As String
            conn.ConnectionString = persistant.myconnstring
            cmd.Connection = conn
            conn.Open()
            cmdstring = "select `bos`.`Salesman` AS `Salesman` from (`ServiceWO` join `bos` on(((`ServiceWO`.`Store` = `bos`.`Store`) and (`ServiceWO`.`BOS` = `bos`.`BOS_number`)))) where `ServiceWO`.`Number` = '" + WONumber.ToString + "' AND `ServiceWO`.`Store` = '" + Store + "'"
            cmd.CommandText = cmdstring
            text = cmd.ExecuteScalar
            conn.Close()
            conn.Dispose()

            txtboxname = "PDIsales"
            If form = "Trailer" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            Else
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            End If
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()


            Select Case form
                Case "Trailer"
                    text = Me.txttrailermake.Text
                Case Else
                    text = Me.txtmotormake.Text
            End Select

            txtboxname = "PDImmake"
            If form = "Trailer" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            Else
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            End If
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()

            Select Case form

                Case "Trailer"
                    text = Me.txttrailermodel.Text
                Case Else
                    text = Me.txtmotormodel.Text
            End Select

            txtboxname = "PDImmodel"
            If form = "Trailer" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            Else
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            End If
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()
            Select Case form
                Case "Trailer"
                    text = Me.txttrailerserial.Text
                Case Else
                    text = Me.txtmotorserial.Text
            End Select

            txtboxname = "PDImserial"
            If form = "Trailer" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            Else
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            End If
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()

            Select Case form
                Case "Small outboard"
                Case Else
                    text = Me.TxtBoatMake.Text + " " + txtboatmodel.Text
                    txtboxname = "PDIboat"
                    If form = "Trailer" Then
                        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    Else
                        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    End If
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, text, posx, posy, 0)
                    cb.EndText()
            End Select

            Select Case form
                Case "Small outboard"
                Case Else
                    text = Me.txtBoatSerial.Text
                    txtboxname = "PDIboathin"
                    If form = "Trailer" Then
                        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    Else
                        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    End If
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, text, posx, posy, 0)
                    cb.EndText()
            End Select
        End If


        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    Private Sub BtnRePrintCheckin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRePrintCheckin.Click
        Dim checkinnotetext As String = ""
        '    Try
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim cmdstring As String = ""
        conn.ConnectionString = persistant.myconnstring
        cmd.Connection = conn
        conn.Open()
        cmd.CommandText = "Select Note from ServiceCheckInNotes where Number = " + CheckInNote.ToString
        Dim retval As Object = cmd.ExecuteScalar
        If Not IsDBNull(retval) Then
            checkinnotetext = retval
        End If
        conn.Close()
        conn.Dispose()
        printCheckin(checkinnotetext)
        '      Catch ex As Exception
        '    End Try
    End Sub

    Private Sub BtnAddAdjustment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddAdjustment.Click
        Dim add As New FrmAdjustWOBill
        add.persistant = persistant
        add.Store = Store
        add.WO = WONumber
        add.ShowDialog()
        WritetoScreen()

    End Sub

    Private Sub refreshadjustmentsdataview(ByVal data As DataTable)
        adjustmentdatatable.Clear()
        adjustmentdatatable.Merge(data)
        adjustmentbindingsource.ResetBindings(False)

        If adjustmentbinded = False Then
            adjustmentbindingsource.DataSource = adjustmentdatatable
            DVadjustments.DataSource = adjustmentbindingsource
            adjustmentbinded = True
        End If

        If DVadjustments.RowCount > 0 Then
            Me.DVadjustments.Columns("Delete").Visible = True
            Me.DVadjustments.Columns("Edit").Visible = True
            Me.DVadjustments.ColumnHeadersVisible = True

        Else
            Me.DVadjustments.Columns("Delete").Visible = False
            Me.DVadjustments.Columns("Edit").Visible = False
            Me.DVadjustments.ColumnHeadersVisible = False
        End If
        Me.DVadjustments.Columns("Number").Visible = False
        Me.DVadjustments.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
    End Sub
    Private Sub Getnewadjustdata()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        Dim SQL As String
        SQL = buildadjustmentssql()

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            adjustmentnewdata.Clear()
            adapter.Fill(adjustmentnewdata)
            conn.Close()
            refreshadjustmentsdataview(adjustmentnewdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub
    Private Function buildadjustmentssql() As String
        buildadjustmentssql = "SELECT Number, Reason, Concat('$', amount) as Amount FROM ServiceWOadjustment where WO = '" + WONumber.ToString + "' AND store = '" + Store + "'"
    End Function

    Private Sub DVadjustments_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DVadjustments.CellContentClick
        If DVadjustments.Columns(e.ColumnIndex).Name = "Delete" Then
            Try
                Dim conn As New MySqlConnection
                Dim cmd As New MySqlCommand
                conn.ConnectionString = persistant.myconnstring
                cmd.Connection = conn
                conn.Open()
                cmd.CommandText = "DELETE from ServiceWOadjustment where Number = " + DVadjustments.Item("Number", e.RowIndex).Value.ToString
                cmd.ExecuteNonQuery()
                conn.Close()
                conn.Dispose()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
        If DVadjustments.Columns(e.ColumnIndex).Name = "Edit" Then
            Dim edit As New FrmAdjustWOBill
            edit.persistant = persistant
            edit.Store = Store
            edit.WO = WONumber
            edit.Adjustment = CInt(DVadjustments.Item("Number", e.RowIndex).Value)
            edit.ShowDialog()
        End If
        WritetoScreen()

    End Sub

    Private Sub btnReactivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReactivate.Click
        If BOSnumber > 0 Then
            cmbStatus.SelectedItem = "Approved Rig"
        Else
            cmbStatus.SelectedItem = "Active"
        End If
        cmbchanged(cmbStatus)
        funcs.updateWOPrioritylist(WONumber.ToString, Store, persistant.myconnstring)
        WritetoScreen()
        save()

    End Sub

    Private Sub btnVoid_Click(sender As System.Object, e As System.EventArgs) Handles btnVoid.Click
        Dim resval As MsgBoxResult = MessageBox.Show("Are you sure you want to void this Work Order?", "VOID WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
        If resval = MsgBoxResult.Yes Then
            cmbStatus.SelectedItem = "Void"
            cmbchanged(cmbStatus)
        End If
    End Sub

    'Private Sub btnOrderParts_Click(sender As System.Object, e As System.EventArgs) Handles btnOrderParts.Click
    '    Dim OrderParts As New frmPartsOrder
    '    OrderParts.persistant = Me.persistant
    '    OrderParts.WONumber = WONumber
    '    OrderParts.CustomerNumber = CustomerNumber
    '    OrderParts.Store = Store
    '    OrderParts.Show()

    'End Sub

    Private Sub btnViewLogByCust_Click(sender As System.Object, e As System.EventArgs) Handles btnViewLogByCust.Click
        Dim custCalls As New frmOldCallLogByCustomer
        custCalls.CustomerNumber = CustomerNumber
        custCalls.persistant = persistant
        custCalls.ShowDialog()
    End Sub


    Private Sub btnCourtesyClean_Click(sender As System.Object, e As System.EventArgs) Handles btnCourtesyClean.Click

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring

        cmd.Connection = conn

        Dim cmdstring As String = "Insert INTO ServiceIssue Set Type = 'Courtesy Clean', Status = "
        'in Edmonton we will auto assign it to JanJan
        If Store = "EDM" Then
            cmdstring &= "'Assigned', Assigned = 'JanJan2012',"
        Else
            cmdstring &= "'To Be Assigned',"
        End If
        cmdstring &= "Payment = 'No Quote Required', Approval = 'N/A', QuoteRate = 0, SubletCost = 0, SubletMarkup = 0, Customer = " & CustomerNumber & _
            ",billablehrs = 0, servicepkgprice = 0, billCustomer = 'NO'"


        cmd.CommandText = cmdstring
        'Debug.WriteLine(cmdstring)
        Try
            conn.Open()
            cmd.ExecuteNonQuery()

            'get the new inserted issue number
            cmd.CommandText = "Select MAX(Issue) from ServiceIssue"
            Dim IssueNumber As Integer = CInt(cmd.ExecuteScalar)

            'link the two records together
            cmd.CommandText = "Insert INTO ServiceWOtoIssue Set WONumber = " & WONumber & ",WOStore = '" & Store & "', Issue = " & IssueNumber & ""
            cmd.ExecuteNonQuery()

            conn.Close()
            WritetoScreen()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
        conn.Dispose()

    End Sub

    Private Sub btnOrderParts_Click(sender As System.Object, e As System.EventArgs) Handles btnOrderParts.Click

    End Sub


    Private Sub Label35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label35.Click

    End Sub
    Private Sub chkPST_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPST.CheckedChanged

    End Sub
End Class