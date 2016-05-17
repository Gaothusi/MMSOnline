Imports System.Windows.Forms
Imports System.Data
Imports MySql.Data.MySqlClient

Public Class frmcommadjustaddedit
    Public Adjustment As Integer
    Public user As String
    Public persistant As PreFetch
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand


        Dim x As String
        Dim amount As Decimal
        x = Replace(txtamount.Text, "$", "")
        x = Replace(x, ",", "")
        x = Replace(x, "(", "-")
        x = Replace(x, ")", "")
        amount = Val(x)

        Dim reason = Replace(txtreason.Text, "'", "")
        reason = Replace(reason, "/", "")
        reason = Replace(reason, "\", "")


        If Adjustment > 0 Then
            'update old
            Try
                conn.ConnectionString = persistant.myconnstring
                cmd.Connection = conn
                conn.Open()
                cmd.CommandText = "UPDATE commadjustments set Amount = '" + amount.ToString + "', Reason = '" + reason + "' where Adjustment = " + Adjustment.ToString + ""
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            conn.Dispose()
        Else
            'insert new
            Try
                conn.ConnectionString = persistant.myconnstring
                cmd.Connection = conn
                conn.Open()
                cmd.CommandText = "INSERT into commadjustments set Amount = '" + amount.ToString + "', Reason = '" + reason + "', User = '" + user + "'"
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            conn.Dispose()
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

  
    Private Sub frmcommadjustaddedit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Adjustment > 0 Then
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            Try
                conn.ConnectionString = persistant.myconnstring
                cmd.Connection = conn
                conn.Open()
                cmd.CommandText = "SELECT Amount from commadjustments where Adjustment = " + Adjustment.ToString + ""
                txtamount.Text = cmd.ExecuteScalar
                cmd.CommandText = "SELECT Reason from commadjustments where Adjustment = " + Adjustment.ToString + ""
                txtreason.Text = cmd.ExecuteScalar
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            conn.Dispose()
        End If
    End Sub
End Class
