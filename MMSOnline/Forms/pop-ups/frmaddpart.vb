Imports System.Windows.Forms
Imports MySql.Data.MySqlClient


Public Class frmaddpart
    Public persistant As PreFetch
    Public IssueNumber As Integer
    Public Number As Integer

    Private parts As New Partsdata
    Private Const mydatacount As Integer = 4
    Private mydata(mydatacount) As element

    Private Sub frmaddpart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setuparray()
        If Number = 0 Then
            txtquantity.Text = "1"
            decimalchanged(txtquantity)
        Else
            parts.myconnstring = persistant.myconnstring
            parts.Number = Number
            parts.Download()
            For x As Integer = 0 To mydatacount
                If mydata(x).dbtable = "Part" Then mydata(x).value = parts.getvalue(mydata(x).dbfeild)
            Next
            For x As Integer = 0 To mydatacount
                If x = 3 Then
                    If mydata(x).value <> "" Then mydata(x).txtbox.text = Format(mydata(x).value, "Currency")
                Else
                    If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
                End If

            Next
        End If
    End Sub

    Private Sub save()
        Me.txtprice.Select()
        Me.txtdesc.Select()

        For x As Integer = 0 To mydatacount
            mydata(x).value = Replace(mydata(x).value, "'", "\'")
            mydata(x).value = Replace(mydata(x).value, ";", "\;")
        Next

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        cmd.Connection = conn
        Dim cmdstring As String
        If Number = 0 Then

            cmdstring = "Insert INTO ServiceParts Set "
            For x As Integer = 0 To mydatacount
                If mydata(x).dbtable = "Part" And mydata(x).changed = True Then
                    cmdstring = cmdstring + "" + mydata(x).dbfeild + " = '" + mydata(x).value + "', "
                End If
            Next
            cmdstring = cmdstring + "IssueNumber = '" + Me.IssueNumber.ToString + "'"

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
            Dim somethingchanged As Boolean = False
            For x As Integer = 0 To mydatacount
                If mydata(x).dbtable = "Part" And mydata(x).changed = True Then
                    somethingchanged = True
                End If
            Next

            If somethingchanged Then
                cmdstring = "Update ServiceParts Set "
                For x As Integer = 0 To mydatacount
                    If mydata(x).dbtable = "Part" And mydata(x).changed = True Then
                        If cmdstring <> "Update ServiceParts Set " Then
                            cmdstring = cmdstring + ", "
                        End If
                        cmdstring = cmdstring + "" + mydata(x).dbfeild + " = '" + mydata(x).value + "'"
                    End If
                Next
                cmdstring = cmdstring + "Where Number = '" + Me.Number.ToString + "'"

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
        End If

        For x As Integer = 0 To mydatacount
            mydata(x).value = Replace(mydata(x).value, "\'", "'")
            mydata(x).value = Replace(mydata(x).value, "\;", ";")
        Next

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        save()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
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
    Private Sub moneyboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprice.Leave
        moneychanged(sender)
    End Sub
    Private Sub stringboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdesc.Leave, txtpartnum.Leave, txtsupplier.Leave
        stringchanged(sender)
    End Sub
    Private Sub showok()
        If txtprice.Text = "" And txtdesc.Text = "" Then
            Me.OK_Button.Enabled = False
        Else
            Me.OK_Button.Enabled = True

        End If
    End Sub

    Private Sub setuparray()
        Dim i As Integer

        For x As Integer = 0 To (mydata.Length - 1)
            mydata(x) = New element

        Next

        i = 0
        mydata(i).txtbox = Me.txtdesc
        mydata(i).dbfeild = "Part"
        mydata(i).dbtable = "Part"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 1
        mydata(i).txtbox = Me.txtpartnum
        mydata(i).dbfeild = "PartNumber"
        mydata(i).dbtable = "Part"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 2
        mydata(i).txtbox = Me.txtsupplier
        mydata(i).dbfeild = "Supplier"
        mydata(i).dbtable = "Part"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 3
        mydata(i).txtbox = Me.txtprice
        mydata(i).dbfeild = "Price"
        mydata(i).dbtable = "Part"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 4
        mydata(i).txtbox = Me.txtquantity
        mydata(i).dbfeild = "Quantity"
        mydata(i).dbtable = "Part"
        mydata(i).changed = False
        mydata(i).value = ""
    End Sub


    Private Sub txtdesc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprice.TextChanged, txtdesc.TextChanged
        showok()
    End Sub

    Private Sub txtquantity_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtquantity.Leave
        decimalchanged(sender)
    End Sub
End Class
