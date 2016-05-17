Imports System.Windows.Forms
Imports System.Data
Imports MySql.Data.MySqlClient

Public Class frmCallLog
    Public persistant As PreFetch
    'Public isBOSCall As Boolean = False
    Public Viewnote As Boolean
    Public CustNumber As String = 0
    Public NoteNumber As Integer
    Public CallType As String
    Public ParentID As Integer
    Private saved As Boolean
    'Public isPOCall As Boolean = False

    Private Sub BtnDone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDone.Click
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
                'If isBOSCall Then
                'tempstring = "INSERT INTO BOSCallLog (Note, time, Who, CustProfile, CallType, ParentID) VALUES ("
                'Else
                tempstring = "INSERT INTO ServiceCallLog (Note, time, Who, CustProfile, CallType, ParentID) VALUES ("
                'End If
                tempstring = tempstring & "'" & txtnotes.Text & "', "
                tempstring = tempstring & "'" & Microsoft.VisualBasic.Strings.Format(Me.DateTimePicker1.Value, "yyyy-MM-dd H:mm:ss") & "', "
                tempstring = tempstring & "'" & txtwho.Text & "', "
                'If isPOCall Then
                '    tempstring = tempstring + "'" & CustNumber.ToString & "', "
                '    tempstring = tempstring + "'PO'); "
                'Else
                tempstring = tempstring & "'" & CustNumber.ToString & "', "
                tempstring = tempstring & "'" & txtCallType.Text & "', "
                tempstring = tempstring & "'" & ParentID.ToString & "'); "
                'End If
                cmd.CommandText = tempstring

                cmd.ExecuteNonQuery()

                conn.Close()
                conn.Dispose()
                saved = True
        End Select

        Me.DialogResult = System.Windows.Forms.DialogResult.OK

    End Sub

    Private Sub frmCallLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If New Do Nothing, If Viewing and existing note load it up and make it sure it can't be edited
        saved = False
        Select Case Viewnote
            Case True
                'btnChangeCallType.Visible = False
                btnchangecaller.Visible = False
                Dim conn As New MySqlConnection
                Dim cmd As New MySqlCommand
                conn.ConnectionString = persistant.myconnstring
                cmd.Connection = conn
                conn.Open()

                cmd.CommandText = "SELECT Note from ServiceCallLog where Number = '" + NoteNumber.ToString + "'"
                Me.txtnotes.Text = cmd.ExecuteScalar.ToString

                cmd.CommandText = "SELECT Who from ServiceCallLog where Number = '" + NoteNumber.ToString + "'"
                Me.txtwho.Text = cmd.ExecuteScalar.ToString

                cmd.CommandText = "SELECT CustProfile from ServiceCallLog where Number = '" + NoteNumber.ToString + "'"
                Me.CustNumber = CInt(cmd.ExecuteScalar.ToString)

                cmd.CommandText = "SELECT ParentID from ServiceCallLog where Number = '" + NoteNumber.ToString + "'"
                Me.ParentID = CInt(cmd.ExecuteScalar.ToString)

                cmd.CommandText = "SELECT CallType from ServiceCallLog where Number = '" + NoteNumber.ToString + "'"
                txtCallType.Text = cmd.ExecuteScalar.ToString

                cmd.CommandText = "SELECT time from ServiceCallLog where Number = '" + NoteNumber.ToString + "'"
                Me.DateTimePicker1.Value = CDate(cmd.ExecuteScalar.ToString)

                conn.Close()
                conn.Dispose()

                Me.txtnotes.ReadOnly = True
                Me.DateTimePicker1.Enabled = False

            Case False
                DateTimePicker1.Value = DateTime.Now
                txtwho.Text = persistant.myusername
                txtCallType.Text = CallType
        End Select

    End Sub

    Private Sub frmCallLog_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If saved = False Then Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub changecaller_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchangecaller.Click
        Dim pick As New frmSalesman
        pick.persistant = persistant
        pick.filter = ""
        pick.ShowDialog()
        If persistant.temppass <> "None" Then
            txtwho.Text = persistant.temppass
        End If
    End Sub

    'Private Sub btnChangeCallType_Click(sender As System.Object, e As System.EventArgs) Handles btnChangeCallType.Click
    '    If txtCallType.Text = "Parts Order" Then
    '        txtCallType.Text = "Work Order"
    '    Else
    '        txtCallType.Text = "Parts Order"
    '    End If
    'End Sub
End Class