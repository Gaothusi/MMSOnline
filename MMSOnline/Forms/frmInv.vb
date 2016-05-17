Imports MySql.Data.MySqlClient
Imports System.Data
Imports MMSOnline.Functions

Public Class frmInv
    Public persistant As PreFetch
    Public mybos As frmBOS
    Private iamselectingboat As Boolean
    Dim loaded, binded, changed, firstloaded As Boolean
    Private mydata As New DataTable()
    Dim datatable As New DataTable
    Private bindingsource1 As New BindingSource
    Public WriteOnly Property selectingboat() As Boolean
        Set(ByVal value As Boolean)
            iamselectingboat = value
        End Set
    End Property


    Private Sub frmInv_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loaded = False
        binded = False
        changed = False
        firstloaded = True
        buildlists()

        For x As Integer = 0 To (listyear.Items.Count() - 1)
            listyear.SetItemChecked(x, True)
        Next x

        liststatus.SetItemChecked(0, True)
        If iamselectingboat = True Then
            Label5.Visible = False
            liststatus.Visible = False
        End If

        For x As Integer = 0 To Me.DataView.Columns.Count - 1
            If DataView.Columns(x).Name <> DataView.Columns("XLchk").Name Then
                DataView.Columns(x).ReadOnly = True
            Else
                DataView.Columns(x).ReadOnly = False
            End If
        Next
        If iamselectingboat Then
            btnselectall.Visible = False
            btnunselall.Visible = False
            cmdxl.Visible = False
            Me.DataView.Columns("xlchk").Visible = False
            Me.cmdPrint.Visible = False
        End If

        btndoesdisplay(cmdxl, persistant.myuserLEVEL, 8)
        btndoesdisplay(btnselectall, persistant.myuserLEVEL, 8)
        btndoesdisplay(btnunselall, persistant.myuserLEVEL, 8)
        If persistant.myuserLEVEL < 8 Then Me.DataView.Columns("xlchk").Visible = False
        Me.chkonOrder.Checked = True
        If persistant.myuserID = "Rob" Or persistant.myuserID = "admin" Then
            Me.Button1.Visible = True
        End If
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


        If Me.DataView.Rows.Count <> 0 Then
            For x As Integer = 0 To Me.DataView.Rows.Count - 1
                If Me.DataView.Item("xlchk", x).Value = "1" Then 'Or Me.DataView.Item("xlchk", x).Value.ToString = "True" 
                    templist.Add(Val(Me.DataView.Item("CTRL", x).Value))
                End If
            Next
        End If

        mydata.Clear()
        mydata.Merge(data)
        bindingsource1.EndEdit()
        bindingsource1.ResetBindings(False)


        changed = False
        '     bindingsource1.DataSource = mydata

        If binded = False Then
            bindingsource1.DataSource = mydata
            DataView.DataSource = bindingsource1
            bindingsource1.Sort = "Make, Model, Year, Engine, Equipment"
            binded = True
        End If

        If templist.Count <> 0 And Me.DataView.Rows.Count <> 0 Then
            For x As Integer = 0 To Me.DataView.Rows.Count - 1
                If templist.Contains(Val(Me.DataView.Item("CTRL", x).Value)) Then
                    Me.DataView.Item("xlchk", x).Value = 1
                End If
            Next
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

        'Unit
        If Me.txtunitfltr.Text <> "" Then
            fstring = fstring + " AND i.Boat_Serial LIKE '%" + txtunitfltr.Text + "%'"
        End If

        'BRAND
        If ComboBrand.SelectedItem.ToString <> "All" Then
            fstring = fstring + " AND i.Boat_Brand LIKE '%" + ComboBrand.SelectedItem.ToString + "%'"
        End If

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

        'Year
        If listyear.Items.Count() <> listyear.CheckedItems.Count() Then
            If listyear.CheckedItems.Count() = 0 Then
                sstring = "i.Boat_Year = '0000'"
            Else
                sstring = ""
                For x As Integer = 0 To (listyear.Items.Count() - 1)
                    If listyear.GetItemCheckState(x) = CheckState.Checked Then
                        If sstring <> "" Then
                            sstring = sstring + " OR "
                        End If
                        sstring = sstring + "i.Boat_Year = '" + listyear.Items.Item(x) + "'"
                    End If
                Next x
            End If

            fstring = fstring + " AND (" + sstring + ")"
        End If

        'Status
        If liststatus.CheckedItems.Count <> liststatus.Items.Count Then
            If liststatus.CheckedItems.Count() = 0 Then
                fstring = fstring + "i.State = 'x'"
            Else
                sstring = ""
                For x As Integer = 0 To (liststatus.Items.Count() - 1)
                    If liststatus.GetItemCheckState(x) = CheckState.Checked Then
                        If sstring <> "" Then
                            sstring = sstring + " OR "
                        End If
                        sstring = sstring + "i.State = '" + persistant.getvalue(persistant.tbl_status, "Code", "State = '" + liststatus.Items.Item(x) + "'", 0) + "'"
                    End If
                Next x
                fstring = fstring + " AND (" + sstring + ")"
            End If
        End If
      

        'HERE
        If chkonOrder.Checked <> True Then
            fstring = fstring + " AND i.Here = 'YES'"
        End If

        buildsql = "Select i.location as Location, " _
       & "i.boat_brand as Make, i.boat_model as Model, i.Engine_model as Engine, i.Drive_model as Drive, " _
       & "i.trailer_model as Trailer, i.Equipment as Equipment, i.boat_Year as Year, " _
       & "i.Boat_color as 'Boat Color', i.Trailer_color as 'Trailer Color', " _
       & "Concat('$',i.price) as 'Price', concat('$',i.discount) as Discount, " _
       & "i.here as Arrived, i.est_arrival_date as 'Est Arrival Date', i.comments as Comments, i.Boat_HIN as HIN, i.boat_serial as Unit, " _
       & "s.state as Status, i.Control_Number as CTRL, concat('$',i.Commision) as Commision from inventory as i, status as s where i.state = s.code" _
       & fstring


       


    End Function
    Private Sub BtnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGo.Click
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



    End Sub


    Private Sub buildlists()
        Dim mydatatable As New DataTable
        Dim mydatarow As DataRowView
        Dim tempstring As String
        ComboBrand.Items.Add("All")
        'ComboBrand.Items.Add("Supreme")
        'ComboBrand.Items.Add("Scarab")
        Dim numberoflines As Integer
        Dim temptable As New DataGridView
        Dim b As New BindingSource

        b.DataSource = persistant.tbl_location
        b.Filter = "Store = '" + persistant.mystore + "'"
        temptable.DataSource = b
        mydatarow = b.Item(0)

        numberoflines = mydatarow.Item("Num_lines")
        Dim brandsToExclude As New ArrayList
        brandsToExclude.Add("Searart")
        brandsToExclude.Add("Hewes")

        For y As Integer = 0 To persistant.tbl_location.Columns.Count - 1
            If Val(mydatarow.Item(y)) = 1 Or Val(mydatarow.Item(y)) = True Then
                tempstring = persistant.tbl_location.Columns(y).ColumnName
                tempstring = Replace(tempstring, "_", " ")
                If Not brandsToExclude.Contains(tempstring) Then
                    ComboBrand.Items.Add(tempstring)
                End If
            End If
        Next
        ComboBrand.Sorted = True
        ComboBrand.SelectedItem = "All"

        'If persistant.myuserID = "BrettP" Then
        '    ComboBrand.Items.Clear()
        '    ComboBrand.Items.Add("Centurion")
        '    ComboBrand.SelectedItem = "Centurion"
        'End If

        'If persistant.myuserID = "BrianM" Then
        '    ComboBrand.Items.Add("Bayliner")
        '    ComboBrand.Items.Add("Maxum")
        '    ComboBrand.SelectedItem = "All"
        'End If


        'If persistant.myuserID = "JimN" Then
        '    ComboBrand.Items.Add("Glastron")
        '    ComboBrand.SelectedItem = "All"
        'End If

        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "Prnt = '" + persistant.myparent + "'") - 1)
            If persistant.getvalue(persistant.tbl_location, "Store", "Prnt = '" + persistant.myparent + "'", x) <> "Admin" Then listStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "Prnt = '" + persistant.myparent + "'", x))
        Next

        For x As Integer = 0 To (listStore.Items.Count() - 1)
            listStore.SetItemChecked(x, True)
        Next x

        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "Prnt <> '" + persistant.myparent + "'") - 1)
            If persistant.getvalue(persistant.tbl_location, "Store", "Prnt <> '" + persistant.myparent + "'", x) <> "Admin" Then listStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "Prnt <> '" + persistant.myparent + "'", x))
        Next

        '        If persistant.mystoreCODE = "OGO" Then
        'listStore.Items.Clear()
        'listStore.Items.Add("Ogopogo")
        'listStore.SetItemChecked(0, True)
        'End If

        If persistant.mystore = "Admin" Then
            For x As Integer = 0 To (listStore.Items.Count() - 1)
                listStore.SetItemChecked(x, True)
            Next x
        End If

        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_status, "code > 0") - 1)
            liststatus.Items.Add(persistant.getvalue(persistant.tbl_status, "State", "code > 0", x))
        Next

        For x As Integer = 0 To persistant.tbl_years.Count - 1
            listyear.Items.Add(persistant.tbl_years.Item(x))
        Next

    End Sub
    Private Function Storecode(ByVal xStore As String) As String
        Storecode = persistant.getvalue(persistant.tbl_location, "Code", "Store = '" + xStore + "'", 0)
    End Function

    Private Sub DataView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataView.CellContentClick

        If e.RowIndex > -1 Then
            'Clicked the Details button 
            'Load the data into the form
            If e.ColumnIndex = 0 Then
                Dim invedit1 As New frmeditinv
                invedit1.persistant = persistant
                invedit1.controlnumber = DataView.Item("CTRL", e.RowIndex).Value
                If iamselectingboat = True Then
                    invedit1.unlockserials = False
                    invedit1.selectingboat = True
                    invedit1.bos = mybos
                    invedit1.invform = Me
                End If
                invedit1.Show()
            End If
        End If

    End Sub
    Private Sub cmdxl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdxl.Click
        Dim templist As New List(Of Integer)
        For x As Integer = 0 To Me.DataView.RowCount - 1
            If DataView.Item("XLchk", x).Value = "1" Then templist.Add(DataView.Item("CTRL", x).Value)
        Next
        If templist.Count = 0 Then
            MessageBox.Show("Please select the boats you wish to edit")
        Else
            Dim frm As New frmxlsheet
            frm.persistant = persistant
            frm.boats = templist
            frm.Show()
        End If
    End Sub
    Private Sub btnselectall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselectall.Click
        If Me.DataView.Rows.Count <> 0 Then
            For x As Integer = 0 To Me.DataView.Rows.Count - 1
                Me.DataView.Item("xlchk", x).Value = True
            Next
        End If
    End Sub
    Private Sub DataView_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataView.CellContentDoubleClick
        If e.RowIndex > -1 And iamselectingboat Then
            mybos.selectboat(DataView.Item("CTRL", e.RowIndex).Value)
            Me.Close()
        End If
    End Sub
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        'Select all
        If Me.DataView.Rows.Count <> 0 Then
            For x As Integer = 0 To Me.DataView.Rows.Count - 1
                Me.DataView.Item("xlchk", x).Value = True
            Next
        End If

        Dim templist As New List(Of Integer)

        For x As Integer = 0 To Me.DataView.RowCount - 1
            If DataView.Item("XLchk", x).Value = "1" Then templist.Add(DataView.Item("CTRL", x).Value)
        Next
        If templist.Count = 0 Then
            MessageBox.Show("There are no visable boats to print.")
        Else
            Dim frm As New frmxlsheet
            frm.persistant = persistant
            frm.viewonly = True
            frm.boats = templist
            frm.Show()
        End If

    End Sub
    Private Sub listshow_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listshow.SelectedIndexChanged

        For y As Integer = 0 To Me.DataView.Columns.Count - 1
            If Me.DataView.Columns(y).Name <> "locked" Then Me.DataView.Columns(y).Visible = True
        Next
        For x As Integer = 0 To Me.listshow.Items.Count - 1
            If listshow.GetItemCheckState(x) = CheckState.Unchecked Then
                For y As Integer = 0 To Me.DataView.Columns.Count - 1
                    If Me.listshow.Items(x) = Me.DataView.Columns(y).Name Then
                        Me.DataView.Columns(y).Visible = False
                    End If
                Next
            End If

        Next
    End Sub
    Private Sub unselectall(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnunselall.Click
        If Me.DataView.Rows.Count <> 0 Then
            For x As Integer = 0 To Me.DataView.Rows.Count - 1
                Me.DataView.Item("xlchk", x).Value = False
            Next
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim tempx As String
        Try
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            conn.Open()
            cmd.Connection = conn
            If MessageBox.Show("Are you wish to Perminatly Delete ALL checked boats?", "Confirm", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                For x As Integer = 0 To Me.DataView.RowCount - 1
                    If DataView.Item("XLchk", x).Value = "1" Then

                        tempx = DataView.Item("CTRL", x).Value
                        cmd.CommandText = "DELETE FROM inventory where Control_Number = '" + tempx + "'"
                        cmd.ExecuteNonQuery()
                        cmd.CommandText = "DELETE FROM invrestricted where Control_Number = '" + tempx + "'"
                        cmd.ExecuteNonQuery()
                    End If
                Next
            End If
            cmd.Dispose()
            conn.Close()
            conn.Dispose()
        Catch ex As MySqlException
            MessageBox.Show("Error: " + ex.Message)
        End Try
    End Sub

  
    Private Sub BtnAllStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAllStatus.Click
        For x As Integer = 0 To (liststatus.Items.Count - 1)
            liststatus.SetItemChecked(x, True)
        Next x
    End Sub

    Private Sub BtnStatusNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStatusNone.Click
        For x As Integer = 0 To (liststatus.Items.Count - 1)
            liststatus.SetItemChecked(x, False)
        Next x
    End Sub

    Private Sub BtnLocAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLocAll.Click
        For x As Integer = 0 To (listStore.Items.Count - 1)
            listStore.SetItemChecked(x, True)
        Next x
    End Sub

    Private Sub BtnLocNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLocNone.Click
        For x As Integer = 0 To (listStore.Items.Count - 1)
            listStore.SetItemChecked(x, False)
        Next x
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)

    End Sub
End Class