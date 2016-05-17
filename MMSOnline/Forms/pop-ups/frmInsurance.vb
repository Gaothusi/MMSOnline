Imports System.Windows.Forms

Public Class frmInsurance
    Public comp, attn, ph, fx, pgs As String


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        comp = TextBox1.Text
        attn = TextBox2.Text
        ph = TextBox3.Text
        fx = TextBox4.Text
        pgs = TextBox5.Text

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


    Private Sub frmInsurance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Function txt2Phone(ByVal input As String) As String
        Dim strTemp As String
        Dim strPhone As String
        Dim strExtension As String = ""
        Dim intResult As Integer
        Dim output As String
        Dim DefaultAreaCode As String

        DefaultAreaCode = "780"
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

    Private Sub TextBox3_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.Leave, TextBox3.Leave
        Dim data, temp As String
      
        data = sender.text
        temp = txt2Phone(data)
        data = temp
        sender.text = data
       
    End Sub
End Class
