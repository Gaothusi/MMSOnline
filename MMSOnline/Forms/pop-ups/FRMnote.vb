Imports System.Windows.Forms
Imports System.Data
Imports MySql.Data.MySqlClient

Public Class FRMnote
    Public persistant As PreFetch
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        cmd.Connection = conn
        conn.Open()
        cmd.CommandText = "UPDATE printdata set DRAW = '" + txtnotes1.Text + "' where data = 'note'"
        cmd.ExecuteNonQuery()
        conn.Close()
        conn.Dispose()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub FRMnote_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        cmd.Connection = conn
        conn.Open()
        cmd.CommandText = "SELECT DRAW from printdata where data = 'note'"
        Me.txtnotes1.Text = cmd.ExecuteScalar
        conn.Close()
        conn.Dispose()
    End Sub
End Class
