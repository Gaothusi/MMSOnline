
Imports System.Windows.Forms
Imports System.Data
Imports MySql.Data.MySqlClient

Public Class frmCheckIn
    Public persistant As PreFetch
    Public Viewnote As Boolean
    Public NoteNumber As Integer
    Public Note
    Private saved As Boolean

    Private Sub FRMnote_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If New Do Nothing, If Viewing and existing note load it up and make it sure it can't be edited
        saved = False
        Select Case Viewnote
            Case True

                Dim conn As New MySqlConnection
                Dim cmd As New MySqlCommand
                conn.ConnectionString = persistant.myconnstring
                cmd.Connection = conn
                conn.Open()

                cmd.CommandText = "SELECT Note from ServiceCheckInNotes where Number = '" + NoteNumber.ToString + "'"
                Me.txtnotes.Text = cmd.ExecuteScalar.ToString


                cmd.CommandText = "SELECT time from ServiceCheckInNotes where Number = '" + NoteNumber.ToString + "'"
                Me.DateTimePicker1.Value = CDate(cmd.ExecuteScalar.ToString)

                conn.Close()
                conn.Dispose()

                Me.txtnotes.Enabled = False
                Me.DateTimePicker1.Enabled = False

            Case False
                DateTimePicker1.Value = DateTime.Now
                'Do Nothing
        End Select


    End Sub

    Private Sub BtnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGo.Click
        'If new insert.  If old just exit.

        Select Case Viewnote
            Case True
                'Do Nothing
            Case False
                txtnotes.Text = Replace(txtnotes.Text, "'", "\'")
                txtnotes.Text = Replace(txtnotes.Text, ";", "\;")

                Dim conn As New MySqlConnection
                Dim cmd As New MySqlCommand
                conn.ConnectionString = persistant.myconnstring
                cmd.Connection = conn
                conn.Open()

                Dim tempstring As String

                tempstring = "INSERT INTO ServiceCheckInNotes (Note, time) VALUES ("
                tempstring = tempstring + "'" & txtnotes.Text & "', "
                tempstring = tempstring + "'" & Microsoft.VisualBasic.Strings.Format(Me.DateTimePicker1.Value, "yyyy-MM-dd H:mm:ss") & "'); "
                cmd.CommandText = tempstring


                cmd.ExecuteNonQuery()

                cmd.CommandText = "Select MAX(Number) from ServiceCheckInNotes"
                NoteNumber = CInt(cmd.ExecuteScalar)
                Note = txtnotes.Text
                conn.Close()
                conn.Dispose()
                saved = True
        End Select

        Me.DialogResult = System.Windows.Forms.DialogResult.OK

    End Sub

    Private Sub frmCheckIn_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If saved = False Then Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub
End Class