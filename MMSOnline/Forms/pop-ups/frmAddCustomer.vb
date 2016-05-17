Imports MySql.Data.MySqlClient
Imports System.Data
Imports MMSOnline.Functions


Public Class frmAddCustomer
    Public persistant As PreFetch
    Public Const mydatacount2 As Integer = 18
    Public mydata2(mydatacount2) As element
    Public CustomerNumber As Integer
    Private Customerdata As New CustData
    Dim saved As Boolean

    Private Sub frmEditCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setuparray()
        saved = False
    End Sub

    Private Sub setuparray()
        Dim i As Integer

        For x As Integer = 0 To (mydata2.Length - 1)
            mydata2(x) = New element

        Next

        i = 0
        mydata2(i).txtbox = Me.txtName
        mydata2(i).dbfeild = "Name"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 1
        mydata2(i).txtbox = Me.txtAddress
        mydata2(i).dbfeild = "Address"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 2
        mydata2(i).txtbox = Me.TxtCity
        mydata2(i).dbfeild = "City"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 3
        mydata2(i).txtbox = Me.txtProv
        mydata2(i).dbfeild = "Prov"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 4
        mydata2(i).txtbox = Me.txtpostal
        mydata2(i).dbfeild = "Postal"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 5
        mydata2(i).txtbox = Me.txtphone1
        mydata2(i).dbfeild = "Phone1"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 6
        mydata2(i).txtbox = Me.Txtphone2
        mydata2(i).dbfeild = "Phone2"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 7
        mydata2(i).txtbox = Me.Txtemail
        mydata2(i).dbfeild = "Email"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 8
        mydata2(i).txtbox = Me.TxtBoatMake
        mydata2(i).dbfeild = "Bmake"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 9
        mydata2(i).txtbox = Me.txtboatmodel
        mydata2(i).dbfeild = "Bmodel"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 10
        mydata2(i).txtbox = Me.txtBoatSerial
        mydata2(i).dbfeild = "Bserial"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 11
        mydata2(i).txtbox = Me.txtmotormake
        mydata2(i).dbfeild = "Mmake"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 12
        mydata2(i).txtbox = Me.txtmotormodel
        mydata2(i).dbfeild = "Mmodel"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 13
        mydata2(i).txtbox = Me.txtmotorserial
        mydata2(i).dbfeild = "Mserial"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 14
        mydata2(i).txtbox = Me.txttrailermake
        mydata2(i).dbfeild = "Tmake"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 15
        mydata2(i).txtbox = Me.txttrailermodel
        mydata2(i).dbfeild = "Tmodel"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 16
        mydata2(i).txtbox = Me.txttrailerserial
        mydata2(i).dbfeild = "Tserial"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 17
        mydata2(i).txtbox = Me.txtcolor
        mydata2(i).dbfeild = "Color"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""

        i = 18
        mydata2(i).txtbox = Me.txtyear
        mydata2(i).dbfeild = "Year"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = False
        mydata2(i).value = ""


    End Sub
    Private Sub namechanged()
        Dim Text As String = txtN1L.Text
        If txtN1F.Text <> "" Then
            Text = Text + ", " + txtN1F.Text
        End If

        If txtN2L.Text <> "" Then
            Text = Text + " & " + txtN2L.Text
        End If

        If txtN2F.Text <> "" Then
            Text = Text + ", " + txtN2F.Text
        End If
        txtName.Text = Text
        stringchanged(txtName)
    End Sub

    Private Sub txtleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcolor.Leave, txtyear.Leave, txttrailerserial.Leave, txttrailermodel.Leave, txttrailermake.Leave, txtProv.Leave, txtpostal.Leave, Txtphone2.Leave, txtphone1.Leave, txtName.Leave, txtmotorserial.Leave, txtmotormodel.Leave, txtmotormake.Leave, Txtemail.Leave, TxtCity.Leave, txtBoatSerial.Leave, txtboatmodel.Leave, TxtBoatMake.Leave, txtAddress.Leave

        stringchanged(sender)
    End Sub

    Private Sub stringchanged(ByVal sender As Object)
        Dim data, temp As String
        For i As Integer = 0 To (mydata2.Length - 1)
            If sender.Equals(mydata2(i).txtbox) = True Then
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
                mydata2(i).value = data
                mydata2(i).changed = True
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

        For x As Integer = 0 To mydatacount2
            mydata2(x).value = Replace(mydata2(x).value, "'", "\'")
            mydata2(x).value = Replace(mydata2(x).value, ";", "\;")
        Next

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring

        cmd.Connection = conn
        Dim cmdstring As String = "Insert Into ServiceCustomers Set "

        For x As Integer = 0 To mydatacount2
            If mydata2(x).dbtable = "Customer" And mydata2(x).changed = True Then
                cmdstring = cmdstring + mydata2(x).dbfeild + " = '" + mydata2(x).value + "', "
            End If
        Next
        cmdstring = Mid(cmdstring, 1, cmdstring.Length - 2)
        If cmdstring <> "Insert Into ServiceCustomers Se" Then
            cmd.CommandText = cmdstring
            Debug.WriteLine(cmdstring)

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Select Max(Number) from ServiceCustomers"
                CustomerNumber = cmd.ExecuteScalar
                conn.Close()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            End Try
        End If
        conn.Dispose()

        For x As Integer = 0 To mydatacount2
            mydata2(x).value = Replace(mydata2(x).value, "\'", "'")
            mydata2(x).value = Replace(mydata2(x).value, "\;", ";")
        Next

    End Sub

    Private Sub frmEditCustomer_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If saved = False Then Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub BtnCustData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCustData.Click
        If txtN1L.Text = "" Or txtN1F.Text = "" Or _
            txtAddress.Text = "" Or TxtCity.Text = "" Or _
            txtProv.Text = "" Or txtpostal.Text = "" Or Txtemail.Text = "" Then
            MessageBox.Show("You can not save until all address information is filled out.")
        Else
            save()
            saved = True
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
        'If Me.txtName.Text <> "" Then
        'Else
        '    MessageBox.Show("You can not save until you enter the customer's name")
        'End If

    End Sub

    Private Sub txtN1L_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtN2L.TextChanged, txtN2F.TextChanged, txtN1L.TextChanged, txtN1F.TextChanged
        namechanged()
    End Sub
End Class