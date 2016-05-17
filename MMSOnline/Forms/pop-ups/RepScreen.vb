Imports MySql.Data.MySqlClient
Imports System.Data
Imports MMSOnline.Functions

Public Class RepScreen
    Public persistant As PreFetch
    Public brand As String
    Dim loaded, binded, changed, firstloaded As Boolean
    Private mydata As New DataTable()
    Dim datatable As New DataTable
    Private bindingsource1 As New BindingSource

    Private Sub RepScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loaded = False
        binded = False
        changed = False
        firstloaded = True
        buildlists()
    End Sub


    Private Sub refreshdata(ByVal data As DataTable)
        Dim templist As New List(Of Integer)
        Dim curcell As DataGridViewCell
        Dim acell As DataGridViewCell
        If DataView.SelectedCells.Count > 0 Then
            curcell = DataView.SelectedCells.Item(0)
            acell = DataView.FirstDisplayedCell
            DataView.CurrentCell = acell
            DataView.CurrentCell = curcell
            curcell.Selected = True
        End If

        mydata.Clear()
        mydata.Merge(data)
        bindingsource1.EndEdit()
        bindingsource1.ResetBindings(False)
        changed = False

        If binded = False Then
            bindingsource1.DataSource = mydata
            DataView.DataSource = bindingsource1
            bindingsource1.Sort = "Model, Year, Engine, Equipment"
            binded = True
        End If
    End Sub
    Private Function buildsql() As String


        Dim fstring As String
        Dim sstring As String

        fstring = ""
        sstring = ""
        'Model
        If Me.txtmdlflter.Text <> "" Then
            fstring = fstring + " AND i.Boat_Model LIKE '%" + txtmdlflter.Text + "%'"
        End If

        'Serial
        If Me.txtserfltr.Text <> "" Then
            fstring = fstring + " AND i.Boat_HIN LIKE '%" + txtserfltr.Text + "%'"
        End If

        'BRAND
        fstring = fstring + " AND i.Boat_Brand LIKE '%" + brand + "%'"


        'STORE
        If listStore.Items.Count() <> listStore.CheckedItems.Count() Then
            If listStore.CheckedItems.Count() = 0 Then
                sstring = "i.Location = 'x'"
            Else
                sstring = ""
                For x As Integer = 0 To (listStore.Items.Count() - 1)
                    If listStore.GetItemCheckState(x) = CheckState.Checked Then
                        If sstring <> "" Then
                            sstring = sstring + " OR "
                        End If
                        sstring = sstring + "i.Location = '" + Storecode(listStore.Items.Item(x)) + "'"
                    End If
                Next x
            End If
            fstring = fstring + " AND (" + sstring + ")"
        End If

        'Status
        If ListStatus.CheckedItems.Count <> ListStatus.Items.Count Then
            If ListStatus.CheckedItems.Count() = 0 Then
                fstring = fstring + "i.State = 'x'"
            Else
                sstring = ""
                For x As Integer = 0 To (ListStatus.Items.Count() - 1)
                    If ListStatus.GetItemCheckState(x) = CheckState.Checked Then
                        If sstring <> "" Then
                            sstring = sstring + " OR "
                        End If
                        sstring = sstring + "i.State = '" + persistant.getvalue(persistant.tbl_status, "Code", "State = '" + ListStatus.Items.Item(x) + "'", 0) + "'"
                    End If
                Next x
                fstring = fstring + " AND (" + sstring + ")"
            End If
        End If

        buildsql = "Select i.location as Location, " _
       & "i.boat_model as Model, i.Engine_model as Engine, i.Drive_model as Drive, " _
       & "i.trailer_model as Trailer, i.Equipment as Equipment, i.boat_Year as Year, " _
       & "i.Boat_color as 'Boat Color'," _
       & "i.here as Arrived, i.est_arrival_date as 'Est Arrival Date', i.Boat_HIN as HIN, i.boat_serial as Unit, " _
       & "s.state as Status from inventory as i, status as s where i.state = s.code" _
       & fstring

    End Function
    Private Sub go()
        txtloading.Visible = True
        Me.Refresh()
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter
        Dim SQL As String
        SQL = buildsql()
        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            datatable.Clear()
            adapter.SelectCommand = cmd
            adapter.Fill(datatable)
            loaded = True
            changed = True
            conn.Close()
            refreshdata(datatable)
            Me.DataView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
        txtloading.Visible = False
        Me.Refresh()
        ToolStripStatusLabel1.Text = datatable.Rows.Count.ToString + " Boats listed"
    End Sub


    Private Sub buildlists()
        Dim mydatatable As New DataTable
        Dim mydatarow As DataRowView
        Dim temptable As New DataGridView
        Dim b As New BindingSource
        b.DataSource = persistant.tbl_location
        b.Filter = "Store = '" + persistant.mystore + "'"
        temptable.DataSource = b
        mydatarow = b.Item(0)

        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
            If persistant.getvalue(persistant.tbl_location, "Store", "", x) <> "Admin" Then listStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "", x))
        Next
        For x As Integer = 0 To (listStore.Items.Count() - 1)
            listStore.SetItemChecked(x, True)
        Next x

        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_status, "code > 0") - 1)
            liststatus.Items.Add(persistant.getvalue(persistant.tbl_status, "State", "code > 0", x))
        Next
        liststatus.SetItemChecked(0, True)
    End Sub

    Private Function Storecode(ByVal xStore As String) As String
        Storecode = persistant.getvalue(persistant.tbl_location, "Code", "Store = '" + xStore + "'", 0)
    End Function

    Private Sub BtnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGo.Click
        go()
    End Sub

    Private Sub ToolStripStatusLabel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel2.Click
        Dim chngp As New frmchangepass
        chngp.persistant = persistant
        chngp.ShowDialog()
    End Sub
End Class