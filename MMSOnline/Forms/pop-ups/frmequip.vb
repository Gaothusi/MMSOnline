Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Data

Public Class frmequip
    Public persistant As PreFetch

    Private input As String
    Dim canclose As Boolean


    Public WriteOnly Property inputstring() As String
        Set(ByVal value As String)
            input = value
        End Set
    End Property


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If TextBox1.Text <> "" Then
            Dim conn As New MySqlConnection
            Dim myCommand As New MySqlCommand
            Dim cmdtxt As String
            conn.ConnectionString = persistant.myconnstring

            myCommand.Connection = conn
            cmdtxt = "INSERT into equip SET Shortform = '" + Replace(input, "'", "\'") + "', longform = '" + Replace(TextBox1.Text, "'", "\'") + "'"
            myCommand.CommandText = cmdtxt
            canclose = True

            Try
                conn.Open()
                myCommand.ExecuteNonQuery()
                conn.Close()

            Catch ex As MySqlException
                MessageBox.Show("ERROR: " + ex.Message)
                canclose = False
            End Try
            conn.Dispose()

            Me.Close()
        Else
            MessageBox.Show("You must enter a description for this equipment before continuing")
        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub frmequip_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = Not canclose
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub frmequip_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = "Please enter the meaning of " + input + ":"
    End Sub
End Class
