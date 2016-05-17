'Imports System.Data
Imports MySql.Data.MySqlClient



Public Class frmOldCallLogByCustomer

    Public persistant As PreFetch
    Public CustomerNumber As Integer

    Private Callsdatatable As New DataTable
    Private Callsnewdata As New DataTable
    Private Callsbindingsource As New BindingSource
    Private Callsbinded As Boolean

    Private Sub frmOldCallLogByCustomer_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        GetNewCallData()
    End Sub

    Private Sub DVCalls_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DVCalls.CellContentClick
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
        GetNewCallData()
        'WritetoScreen()

    End Sub
    Private Sub RefreshCallsDataView(ByVal data As DataTable)
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

        Me.DVCalls.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells) ' .DisplayedCellsExceptHeader)

    End Sub

    Private Sub GetNewCallData()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        Dim SQL As String = "SELECT Number, Who, time, CallType as 'Call Type', Note FROM ServiceCallLog where CustProfile = '" + CustomerNumber.ToString + "' ORDER BY time DESC"

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            Callsnewdata.Clear()
            adapter.Fill(Callsnewdata)
            conn.Close()
            RefreshCallsDataView(Callsnewdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub



End Class