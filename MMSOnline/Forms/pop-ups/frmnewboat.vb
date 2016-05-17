Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Data

Public Class frmnewboat
    Public persistant As PreFetch

    Private make, model As String
    Dim canclose As Boolean

    Public WriteOnly Property boatmake() As String
        Set(ByVal value As String)
            make = value
        End Set
    End Property
    Public WriteOnly Property boatmodel() As String
        Set(ByVal value As String)
            model = value
        End Set
    End Property

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Val(TextBox1.Text).ToString <> "" And Val(TextBox2.Text).ToString <> "" And ComboBox1.SelectedIndex <> 0 Then
            Dim conn As New MySqlConnection
            Dim myCommand As New MySqlCommand
            Dim cmdtxt As String
            conn.ConnectionString = persistant.myconnstring

            myCommand.Connection = conn
            cmdtxt = "INSERT into boats SET brand = '" + Replace(make, "'", "\'") + "', model = '" + Replace(model, "'", "\'") + "', length = '" + Val(TextBox1.Text).ToString + "', width = '" + Val(TextBox2.Text).ToString + "' , material = '" + ComboBox1.SelectedItem.ToString + "'"
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
            MessageBox.Show("You must fill in all the needed information before you may continue")
        End If

    End Sub

    Private Sub frmnewmotor_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = Not canclose
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub frmnewmotor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        canclose = False
        ComboBox1.SelectedIndex = 0

        Label1.Text = "Please enter the following information about"
        Label2.Text = "the " + make + " " + model + " your attempting to add:"
    End Sub

End Class
