Imports MySql.Data.MySqlClient
Imports System.Data
Imports MMSOnline.Functions


Public Class frmEditCustomer
    Public persistant As PreFetch
    Public Const mydatacount As Integer = 20
    Public mydata(mydatacount) As element
    Public CustomerNumber As Integer
    Private Customerdata As New CustData
    Private Callsdatatable As New DataTable
    Private Callsnewdata As New DataTable
    Private Callsbindingsource As New BindingSource
    Private Callsbinded As Boolean
    Private WOdatatable As New DataTable
    Private WOnewdata As New DataTable
    Private WObindingsource As New BindingSource
    Private WObinded As Boolean
    Private Sub frmEditCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setuparray()
        downloaddata()
        WritetoScreen()

    End Sub

    Private Sub setuparray()
        Dim i As Integer

        For x As Integer = 0 To (mydata.Length - 1)
            mydata(x) = New element

        Next

        i = 0
        mydata(i).txtbox = Me.txtName
        mydata(i).dbfeild = "Name"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 1
        mydata(i).txtbox = Me.txtAddress
        mydata(i).dbfeild = "Address"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 2
        mydata(i).txtbox = Me.TxtCity
        mydata(i).dbfeild = "City"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 3
        mydata(i).txtbox = Me.txtProv
        mydata(i).dbfeild = "Prov"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 4
        mydata(i).txtbox = Me.txtpostal
        mydata(i).dbfeild = "Postal"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 5
        mydata(i).txtbox = Me.txtphone1
        mydata(i).dbfeild = "Phone1"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 6
        mydata(i).txtbox = Me.Txtphone2
        mydata(i).dbfeild = "Phone2"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 7
        mydata(i).txtbox = Me.Txtemail
        mydata(i).dbfeild = "Email"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 8
        mydata(i).txtbox = Me.TxtBoatMake
        mydata(i).dbfeild = "Bmake"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 9
        mydata(i).txtbox = Me.txtboatmodel
        mydata(i).dbfeild = "Bmodel"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 10
        mydata(i).txtbox = Me.txtBoatSerial
        mydata(i).dbfeild = "Bserial"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 11
        mydata(i).txtbox = Me.txtmotormake
        mydata(i).dbfeild = "Mmake"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 12
        mydata(i).txtbox = Me.txtmotormodel
        mydata(i).dbfeild = "Mmodel"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 13
        mydata(i).txtbox = Me.txtmotorserial
        mydata(i).dbfeild = "Mserial"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 14
        mydata(i).txtbox = Me.txttrailermake
        mydata(i).dbfeild = "Tmake"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 15
        mydata(i).txtbox = Me.txttrailermodel
        mydata(i).dbfeild = "Tmodel"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 16
        mydata(i).txtbox = Me.txttrailerserial
        mydata(i).dbfeild = "Tserial"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 17
        mydata(i).txtbox = Me.txtnotes
        mydata(i).dbfeild = "Notes"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 18
        mydata(i).txtbox = Me.txtcolor
        mydata(i).dbfeild = "Color"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 19
        mydata(i).txtbox = Me.txtyear
        mydata(i).dbfeild = "Year"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 20
        mydata(i).txtbox = Me.txtWarnings
        mydata(i).dbfeild = "Warnings"
        mydata(i).dbtable = "Customer"
        mydata(i).changed = False
        mydata(i).value = ""

    End Sub

    Public Sub downloaddata()

        Customerdata.CustomerNumber = CustomerNumber
        Customerdata.myconnstring = persistant.myconnstring

        Customerdata.Download()

        For x As Integer = 0 To mydatacount
            If mydata(x).dbtable = "Customer" Then mydata(x).value = Customerdata.getvalue(mydata(x).dbfeild)
        Next

    End Sub

    Private Sub WritetoScreen()
        For x As Integer = 0 To mydatacount
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
        Next
        Getnewcalldata()
        GetnewWOdata()


    End Sub

    Private Sub txtleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcolor.Leave, txtyear.Leave, txttrailerserial.Leave, txttrailermodel.Leave, txttrailermake.Leave, txtProv.Leave, txtpostal.Leave, Txtphone2.Leave, txtphone1.Leave, txtName.Leave, txtmotorserial.Leave, txtmotormodel.Leave, txtmotormake.Leave, Txtemail.Leave, TxtCity.Leave, txtBoatSerial.Leave, txtboatmodel.Leave, TxtBoatMake.Leave, txtAddress.Leave, txtnotes.Leave, txtWarnings.Leave
        stringchanged(sender)
    End Sub

    Private Sub stringchanged(ByVal sender As Object)
        Dim data, temp As String
        For i As Integer = 0 To (mydata.Length - 1)
            If sender.Equals(mydata(i).txtbox) = True Then
                data = sender.text
                If i = 5 Or i = 6 Then
                    temp = txt2Phone(data)
                    data = temp
                    sender.text = data
                End If
                If i = 4 Then
                    temp = txt2Postal(data)
                    data = temp
                    sender.text = data
                End If
                mydata(i).value = data
                mydata(i).changed = True
            End If
        Next
    End Sub

    Private Function txt2Postal(ByVal input As String) As String
        Dim strTemp As String
        strTemp = ""
        strTemp = Replace(input, "(", "")
        strTemp = Replace(strTemp, ")", "")
        strTemp = Replace(strTemp, "-", "")
        strTemp = Replace(strTemp, " ", "")
        If strTemp <> Nothing Then
            strTemp = strTemp.ToUpper
            If strTemp.Length = 6 Then
                txt2Postal = Mid(strTemp, 1, 3) + " " + Mid(strTemp, 4, 3)
            Else
                txt2Postal = input
            End If
        Else
            txt2Postal = input
        End If



    End Function

    Private Function txt2Phone(ByVal input As String) As String
        Dim strTemp As String
        Dim strPhone As String
        Dim strExtension As String = ""
        Dim intResult As Integer
        Dim output As String
        Dim DefaultAreaCode As String
        DefaultAreaCode = "780"
        If Me.txtProv.Text.ToLower = "bc" Then DefaultAreaCode = "250"

        '
        ' Remove all the grouping characters for
        ' now. We'll add them back in later.
        '
        strTemp = Replace(input, "(", "")
        strTemp = Replace(strTemp, ")", "")
        strTemp = Replace(strTemp, "-", "")
        strTemp = Replace(strTemp, " ", "")
        strTemp = Replace(strTemp, "X", "x")

        '
        ' Break up the digits into the number and
        ' the extension, if any.
        '
        intResult = InStr(1, strTemp, "x", vbTextCompare)
        If intResult > 0 Then
            strExtension = Mid(strTemp, intResult + 1)
            strPhone = Mid(strTemp, 1, intResult - 1)
        Else
            strPhone = strTemp
        End If

        If Mid(strPhone, 1, 1) = "1" Then
            strPhone = Mid(strPhone, 2)
        End If

        If Len(strPhone) <> 7 And Len(strPhone) <> 10 Then
            txt2Phone = input
            GoTo xxx
        End If

        '
        ' Prepend the default area code
        '
        If Len(strPhone) = 7 Then
            strPhone = DefaultAreaCode + strPhone
        End If

        '
        ' Build the new phone number
        '
        output = "(" + Mid(strPhone, 1, 3) & ") " + Mid(strPhone, 4, 3) + "-" + Mid(strPhone, 7, 4)

        '
        ' Add the extension, if any
        '
        If strExtension <> "" Then
            output = output & " x" & strExtension
        End If
        txt2Phone = output

xxx:

    End Function

    Private Sub save()
        Me.txtName.Select()

        For x As Integer = 0 To mydatacount
            mydata(x).value = Replace(mydata(x).value, "'", "\'")
            mydata(x).value = Replace(mydata(x).value, ";", "\;")
        Next

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring

        cmd.Connection = conn
        Dim cmdstring As String = "Update ServiceCustomers Set "

        For x As Integer = 0 To mydatacount
            If mydata(x).dbtable = "Customer" And mydata(x).changed = True Then
                cmdstring = cmdstring + mydata(x).dbfeild + " = '" + mydata(x).value + "', "
            End If
        Next
        cmdstring = Mid(cmdstring, 1, cmdstring.Length - 2)
        If cmdstring <> "Update ServiceCustomers Se" Then
            cmdstring = cmdstring + " Where number = '" + CustomerNumber.ToString + "'"

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

        For x As Integer = 0 To mydatacount
            mydata(x).value = Replace(mydata(x).value, "\'", "'")
            mydata(x).value = Replace(mydata(x).value, "\;", ";")
        Next

    End Sub

    Private Sub frmEditCustomer_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        save()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        save()
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

        Dim SQL As String
        SQL = buildcallsql()

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
    Private Function buildcallsql() As String
        buildcallsql = "SELECT Number, Who, time, Note FROM ServiceCallLog where CustProfile = '" + CustomerNumber.ToString + "' ORDER BY time DESC"
    End Function

    Private Sub DVCalls_CellContentClick_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DVCalls.CellContentClick
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
    End Sub

    Private Sub refreshWOdataview(ByVal data As DataTable)
        WOdatatable.Clear()
        WOdatatable.Merge(data)
        WObindingsource.ResetBindings(False)

        If WObinded = False Then
            WObindingsource.DataSource = WOdatatable
            DVWO.DataSource = WObindingsource
            WObinded = True
        End If
        ' Me.DVWO.Columns("Note").Width = 220
        ' Me.DVWO.Columns("Who").Width = 40
        If DVWO.RowCount > 0 Then
            Me.dvWO.Columns("Open").Visible = True
            Me.DVWO.ColumnHeadersVisible = True

        Else
            Me.dvWO.Columns("Open").Visible = False
            Me.DVWO.ColumnHeadersVisible = True

        End If

        Me.dvWO.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

    End Sub
    Private Sub GetnewWOdata()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        Dim SQL As String
        SQL = buildWOql()

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            WOnewdata.Clear()
            adapter.Fill(WOnewdata)
            conn.Close()
            refreshWOdataview(WOnewdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub
    Private Function buildWOql() As String
        buildWOql = "SELECT ServiceWO.Number, ServiceWO.Store, ServiceWO.`Status`, ServiceWO.DateReqComp FROM ServiceWO WHERE ServiceWO.CustomerProfile = " + Me.CustomerNumber.ToString + ""
    End Function



    Private Sub btnLogCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogCall.Click
        Dim logcall As New frmCallLog
        logcall.persistant = persistant
        logcall.CustNumber = CustomerNumber
        logcall.ShowDialog()
        WritetoScreen()
    End Sub

    Private Sub dvWO_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dvWO.CellContentClick
        Dim xRow As Integer
        Dim XNum As Integer
        Dim xstore As String
        If e.ColumnIndex = 1 Then
            xRow = e.RowIndex
            XNum = CInt(WOdatatable.Rows(xRow).Item("Number"))
            xstore = WOdatatable.Rows(xRow).Item("Store")
            Dim ViewWO As New FrmWO
            ViewWO.persistant = persistant
            ViewWO.WONumber = XNum
            ViewWO.Store = xstore
            ViewWO.Show()
        End If
    End Sub
End Class