Imports MySql.Data.MySqlClient
Imports System.Data
Imports MMSOnline.Functions

Public Class frmmain
    Public persistant As PreFetch
    Dim count As Integer
    Dim loaded, firstloaded As Boolean
    Dim binded, changed As Boolean
    Private mydata As New DataTable()
    Dim datatable As New DataTable
    Private bindingsource1 As New BindingSource
    Private internetCheckCounter As Integer = 0


    Private Sub frmmain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If persistant.myusername.ToLower = "wabuyag" Then
            lblTesting.Visible = True
        End If
        btndoesdisplay(Me.btnWinterizeEmails, persistant.myuserLEVEL, 9)


        Me.Visible = False
        count = 1
        loaded = False
        binded = False
        changed = False
        firstloaded = True
        filllists()
        reset()
        '  loaded = True
        btndoesdisplay(Me.btnQuickPaymentCalc, persistant.myuserLEVEL, 4)
        btndoesdisplay(Me.btnNewBOS, persistant.myuserLEVEL, 1)
        btndoesdisplay(Me.btnAddInventory, persistant.myuserLEVEL, 8)
        btndoesdisplay(Me.btnRequestOrders, persistant.myuserLEVEL, 8)
        btndoesdisplay(Me.btnApprovedOrders, persistant.myuserLEVEL, 9)
        'btndoesdisplay(Me.btnUserManagement, persistant.myuserLEVEL, 9)
        'restrict this to select few people to avoid user rights getting messed up.
        If persistant.myuserID.ToLower = "wabuyag" Or
           persistant.myuserID.ToLower = "dank" Or
           persistant.myuserID.ToLower = "rob" Or
           persistant.myuserID.ToLower = "pat2013" Or
           persistant.myuserID.ToLower = "freddie2012" Or
           persistant.myuserID.ToLower = "fredtech" Or
           persistant.myuserID.ToLower = "fredadmin" Or
           persistant.myuserID.ToLower = "freddie" Then
            btnUserManagement.Visible = True
        End If

        btndoesdisplay(Me.btnViewReports, persistant.myuserLEVEL, 4)
        'LAbel11 is stats
        btndoesdisplay(Me.btnViewStats, persistant.myuserLEVEL, 6)
        btndoesdisplay(Me.btnCommissions, persistant.myuserLEVEL, 2)
        btndoesdisplay(Me.btnEditDefaultNote, persistant.myuserLEVEL, 9)
        btndoesdisplay(Me.btnService, persistant.myuserLEVEL, 1)
        If persistant.myuserLEVEL > 7 Then
            txtcommisioning.Visible = True
            chkcommisioned.Visible = True
            'If persistant.mystoreCODE = "EDM" Or persistant.mystoreCODE = "ADM" Then
            '    Label8.Visible = True
            'End If
            'comboSalesman.Visible = False
            'Label7.Visible = False
        End If
        Me.Refresh()
        Timer1.Enabled = True
        'If internetCheckCounter > 0 Then
        '    'connection re-established
        '    Me.Text = "MMS Online - connection re-established after (" & internetCheckCounter & ") retries."
        '    internetCheckCounter = 0
        'Else
        '    Me.Text = "MMS Online - first connection"
        'End If
    End Sub
    Private Sub frmmain_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
start:
        If persistant.openBOSs.Count > 0 Then
            persistant.openBOSs.Item(0).Close()
            GoTo start
        End If
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "Delete from online where user = '" + persistant.myuserID + "'"
            cmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As Exception
        End Try

        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()

        If PreFetch.secure = True Then
            persistant.tunnel.delPortForwardingL("127.0.0.1", persistant.port)
            persistant.tunnel.disconnect()
            persistant.tunnel = Nothing
            persistant.jschx = Nothing
        End If

        persistant = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()

    End Sub
    Private Sub form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        End
    End Sub
    Private Sub doit()
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        Dim SQL As String
        SQL = buildsql()

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            '            If count = 1 Then
            'cmd.CommandText = "Insert into online set count = '" + count.ToString + "', user = '" + persistant.myuserID + "'"
            'cmd.ExecuteNonQuery()
            'Else
            'cmd.CommandText = "Update online set count = '" + count.ToString + "' where user = '" + persistant.myuserID + "'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "select user from online where last < (NOW() - (" + (60000 / 1000 + 30).ToString + "))"

            '            Dim onlinetable As New DataTable
            '           adapter.SelectCommand = cmd
            '          adapter.Fill(onlinetable)
            '         If onlinetable.Rows.Count > 0 Then
            'For x As Integer = 0 To onlinetable.Rows.Count - 1
            ' cmd.CommandText = "Delete from online where user = '" + onlinetable.Rows(x).Item(0) + "'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Update bos set locked = 0, lockedby = '' where lockedby = '" + onlinetable.Rows(x).Item(0) + "'"
            'cmd.ExecuteNonQuery()
            'Next
            'End If
            ' End If

            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            datatable.Clear()
            adapter.Fill(datatable)
            txtloading.Visible = False
            refreshdata(datatable)
            loaded = True
            changed = True
            conn.Close()
            count = count + 1
        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub
    Private Sub refreshdata(ByVal data As DataTable)
        mydata.Clear()
        mydata.Merge(data)
        bindingsource1.EndEdit()
        bindingsource1.ResetBindings(False)
        changed = False

        If binded = False Then
            bindingsource1.DataSource = mydata
            DataView.DataSource = bindingsource1
            Me.DataView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader)
            DataView.Columns("Locked").Visible = False
            recolorlist()
            binded = True
        End If

        For x As Integer = 0 To DataView.Rows.Count - 1
            If DataView.Item("Locked", x).Value = 1 Then
                DataView.Rows(x).DefaultCellStyle.BackColor = Color.LightSteelBlue
            End If
            If persistant.myuserLEVEL = 6 Or persistant.myuserLEVEL = 7 Or persistant.myuserID = "admin" Then
                If DataView.Item("Finance Status", x).Value = "Unsubmited" And DataView.Item("Business Manager", x).Value.ToString = "" Then
                    DataView.Rows(x).DefaultCellStyle.BackColor = Color.Khaki
                End If
            End If
        Next
        Me.DataView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader)
    End Sub
    Private Sub recolorlist()

        For x As Integer = 0 To DataView.Rows.Count - 1
            If DataView.Item("Locked", x).Value = 1 Then
                DataView.Rows(x).DefaultCellStyle.BackColor = Color.LightSteelBlue
            End If
            If persistant.myuserLEVEL = 6 Or persistant.myuserLEVEL = 7 Or persistant.myuserID = "admin" Then
                If DataView.Item("Finance Status", x).Value = "Unsubmited" And DataView.Item("Business Manager", x).Value.ToString = "" Then
                    DataView.Rows(x).DefaultCellStyle.BackColor = Color.Khaki
                End If
            End If
        Next
    End Sub
    Private Sub reset()
        txtbuyername.Text = ""
        txtBOS.Text = ""
        comboBrand.SelectedItem = "All"
        For y As Integer = 0 To (listStatus.Items.Count() - 1)
            listStatus.SetItemChecked(y, False)
        Next y
        If persistant.myuserLEVEL > 3 Or persistant.myuserLEVEL = 1 Then
            comboSalesman.SelectedItem = "All"
        Else
            comboSalesman.SelectedItem = "Me"
            comboSalesman.Enabled = False
            'listStatus.SetItemChecked(0, True)
        End If

        listStatus.SetItemChecked(1, True)
        listStatus.SetItemChecked(2, True)
        listStore.Items.Clear()

        listStore.Items.Add("Atlantis AB") 'to view old records.

        'For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "Prnt = '" + persistant.myparent + "'") - 1)
        '    If persistant.getvalue(persistant.tbl_location, "Store", "Prnt = '" + persistant.myparent + "'", x) <> "Admin" Then listStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "Prnt = '" + persistant.myparent + "'", x))
        'Next

        If persistant.mystore = "Admin" Then
            For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "Prnt = '" + persistant.myparent + "'") - 1)
                If persistant.getvalue(persistant.tbl_location, "Store", "", x) <> "Admin" Then

                    listStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "Prnt = '" + persistant.myparent + "'", x))
                Else
                    'Do nothing
                End If
            Next
            'For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "Prnt <> '" + persistant.myparent + "'") - 1)
            '    If persistant.getvalue(persistant.tbl_location, "Store", "Prnt <> '" + persistant.myparent + "'", x) <> "Admin" Then listStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "Prnt <> '" + persistant.myparent + "'", x))
            'Next
            listStore.Items.Add("Boat House")
            listStore.Items.Add("Atlantis BC")
            listStore.Items.Add("Galleon")


        Else
            'If persistant.getvalue(persistant.tbl_location, "Store", "", x) = persistant.mystore Then
            listStore.Items.Add(persistant.mystore)
            'End If
        End If

        listStore.Sorted = True

        If loaded = False Then
            For x As Integer = 0 To (listStore.Items.Count() - 1)
                listStore.SetItemChecked(x, True)
            Next x
        End If



        If persistant.mystore = "Admin" Then
            For x As Integer = 0 To (listStore.Items.Count() - 1)
                listStore.SetItemChecked(x, True)
            Next x
        End If
    End Sub
    Private Sub filllists()

        Dim mydatatable As New DataTable
        Dim mydatarow As DataRowView
        'Dim numberoflines As Integer
        Dim tempstring As String

        comboSalesman.Items.Add("Me")
        comboSalesman.Items.Add("All")

        Dim temptable As New DataGridView
        Dim b As New BindingSource
        b.DataSource = persistant.tbl_location
        b.Filter = "Store = '" + persistant.mystore + "'"
        temptable.DataSource = b
        mydatarow = b.Item(0)

        'numberoflines = mydatarow.Item("Num_lines")
        For y As Integer = 0 To persistant.tbl_location.Columns.Count - 1
            'Debug.WriteLine(mydatarow.Item(y).ToString)
            If mydatarow.Item(y).ToString = "1" Or mydatarow.Item(y).ToString = "0" Or mydatarow.Item(y).ToString = "True" Or mydatarow.Item(y).ToString = "False" Then
                tempstring = persistant.tbl_location.Columns(y).ColumnName
                tempstring = Replace(tempstring, "_", " ")
                comboBrand.Items.Add(tempstring)
            End If
        Next

        comboBrand.Sorted = True

        comboBrand.Items.Add("None")
        comboBrand.Items.Add("Requested Order")
        comboBrand.Items.Add("All")

        'If persistant.myuserID = "BrianM" Then
        '    comboBrand.Items.Add("Maxum")
        '    comboBrand.Items.Add("Bayliner")
        '    comboBrand.SelectedItem = "All"
        'End If


        'If persistant.myuserID = "JimN" Then
        '    comboBrand.Items.Add("Glastron")
        '    comboBrand.SelectedItem = "All"
        'End If

        listStore.Items.Add("Atlantis AB") 'Add this to view old BOS's.

        If persistant.mystore = "Admin" Then
            For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "Prnt = '" + persistant.myparent + "'") - 1)
                'If persistant.getvalue(persistant.tbl_location, "Store", "", x) <> "Admin" Then

                listStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "Prnt = '" + persistant.myparent + "'", x))
                'Else
                'Do nothing
                'End If
            Next
        Else
            'If persistant.getvalue(persistant.tbl_location, "Store", "", x) = persistant.mystore Then
            listStore.Items.Add(persistant.mystore)
            'End If
        End If

        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_status, "") - 1)
            If persistant.getvalue(persistant.tbl_status, "State", "Code = '" + x.ToString + "'", 0) <> "Available" Then listStatus.Items.Add(persistant.getvalue(persistant.tbl_status, "State", "Code = '" + x.ToString + "'", 0))
        Next


    End Sub
    Private Sub cmdreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdreset.Click
        reset()
    End Sub
    Private Function Storecode(ByVal xStore As String) As String
        If xStore = "Atlantis AB" Then Return "ATL"
        Storecode = persistant.getvalue(persistant.tbl_location, "Code", "Store = '" + xStore + "'", 0)
    End Function
    '    Private Function buildsql() As String
    '        Dim fstring As String
    '        Dim sstring As String
    '        fstring = ""
    '        sstring = ""

    '        'STORE
    '        If listStore.CheckedItems.Count() = 0 Then
    '            sstring = "'x'"
    '        Else
    '            sstring = ""
    '            For x As Integer = 0 To (listStore.Items.Count() - 1)
    '                If listStore.GetItemCheckState(x) = CheckState.Checked Then
    '                    If sstring <> "" Then
    '                        sstring = sstring + ", "
    '                    End If
    '                    sstring = sstring + "'" + Storecode(listStore.Items.Item(x)) + "'"
    '                End If
    '            Next x
    '        End If

    '        fstring = "b.Store IN (" + sstring + ") AND "

    '        'BRAND
    '        If comboBrand.SelectedItem.ToString <> "All" Then
    '            If ChkSmallItems.Checked Then
    '                fstring = fstring + "(i.Boat_Brand = '" + comboBrand.SelectedItem.ToString + "' OR i.Boat_Brand = 'Small Item') AND "
    '            Else
    '                fstring = fstring + "i.Boat_Brand = '" + comboBrand.SelectedItem.ToString + "' AND "
    '            End If
    '        Else
    '            If ChkSmallItems.Checked Then
    '            Else
    '                sstring = ""
    '                For y As Integer = 0 To (comboBrand.Items.Count - 1)
    '                    If sstring <> "" Then sstring = sstring + " OR "
    '                    sstring = sstring + "i.Boat_Brand = '" + comboBrand.Items.Item(y) + "'"
    '                Next
    '                fstring = fstring + "(" + sstring + ") AND "

    '            End If
    '        End If

    '        'Salesman
    '        If persistant.myuserLEVEL = 6 Then
    '            If comboSalesman.SelectedItem.ToString <> "All" Then
    '                fstring = fstring + "(b.bizman = '" + persistant.myuserID + "' OR b.bizman = '' OR b.bizman is null) AND "
    '            End If
    '        Else
    '            If comboSalesman.SelectedItem.ToString <> "All" Then
    '                fstring = fstring + "(b.Salesman = '" + persistant.myuserID + "' OR b.Salesman2 = '" + persistant.myuserID + "') AND "
    '            End If
    '        End If


    '        'NAME
    '        If txtbuyername.Text <> "" Then
    '            fstring = fstring + "(Buyer1_First like '%" + txtbuyername.Text + "%' OR Buyer1_Last like '%" + txtbuyername.Text + "%' OR Buyer2_First like '%" + txtbuyername.Text + "%' OR Buyer2_Last like '%" + txtbuyername.Text + "%') AND "
    '        End If

    '        'BOS
    '        If txtBOS.Text <> "" Then
    '            fstring = fstring + "b.BOS_number = '" + txtBOS.Text.ToString + "' AND "
    '        End If

    '        'Commision Mode 
    '        If chkcommisioned.Checked = True Then
    '            fstring = fstring + "b.commisoned = 'NO' AND "
    '        End If

    '        'Status

    '        If listStatus.CheckedItems.Count() = 0 Then
    '            sstring = "b.Status = 'x'"
    '        Else
    '            sstring = ""
    '            For x As Integer = 0 To (listStatus.Items.Count() - 1)
    '                If listStatus.GetItemCheckState(x) = CheckState.Checked Then
    '                    If sstring <> "" Then
    '                        sstring = sstring + ", "
    '                    End If
    '                    If x = 0 Then
    '                        sstring = sstring + "'" + (x).ToString + "'"
    '                    Else
    '                        sstring = sstring + "'" + (x + 1).ToString + "'"
    '                    End If
    '                End If
    '            Next x
    '        End If
    '        fstring = fstring + "s.code IN (" + sstring + ")"

    '        buildsql = "Select b.bos_number as BOS, b.store as Store, " _
    '& "b.salesman as Salesman, b.salesman2 as Split, CONCAT(b.buyer1_last, ', ', b.buyer1_first, '  ', b.buyer2_last, ' ', b.buyer2_first) as Name, i.boat_year as Year, " _
    '& "i.boat_brand as Make, i.boat_model as Model, s.state as 'Status', " _
    '& "h.state as 'Shop Status', f.state as 'Finance Status', b.bizman as 'Business Manager', b.date_sold as 'Date Sold', " _
    '& "b.locked as 'Locked', b.proposed_delivery_date as " _
    '& "'Proposed Delivery Date', b.date_delivered as 'Date Delivered', b.commisoned as 'Commissioned', b.lastmodified as 'Last Modified' " _
    '& "from bos as b, inventory as i, status as s, financestatus as f, shopstatus as h where " _
    '& "b.control_number = i.control_number and b.status = s.code and b.finance_status = " _
    '& "f.code and b.shop_status = h.code AND " & fstring & " ORDER BY b.Date_Sold DESC, b.BOS_number DESC"
    Private Function buildsql() As String
        Dim fstring As String
        Dim sstring As String
        fstring = ""
        sstring = ""

        'STORE
        If listStore.CheckedItems.Count() = 0 Then
            sstring = "'x'"
        Else
            sstring = ""
            For x As Integer = 0 To (listStore.Items.Count() - 1)
                If listStore.GetItemCheckState(x) = CheckState.Checked Then
                    If sstring <> "" Then
                        sstring = sstring + ", "
                    End If
                    sstring = sstring + "'" + Storecode(listStore.Items.Item(x)) + "'"
                End If
            Next x
        End If

        'Identify Salesman and search all Stores for BOS with their names: Requested by Dank on May 17th 2017
        If persistant.myuserLEVEL = 6 Then
            fstring = "b.Store RLIKE '.*' AND "
        Else
            fstring = "b.Store IN (" + sstring + ") AND "

        End If

        'BRAND
        If comboBrand.SelectedItem.ToString <> "All" Then
            If ChkSmallItems.Checked Then
                fstring = fstring + "(i.Boat_Brand = '" + comboBrand.SelectedItem.ToString + "' OR i.Boat_Brand = 'Small Item') AND "
            Else
                fstring = fstring + "i.Boat_Brand = '" + comboBrand.SelectedItem.ToString + "' AND "
            End If
        Else
            If ChkSmallItems.Checked Then
            Else
                sstring = ""
                For y As Integer = 0 To (comboBrand.Items.Count - 1)
                    If sstring <> "" Then sstring = sstring + " OR "
                    sstring = sstring + "i.Boat_Brand = '" + comboBrand.Items.Item(y) + "'"
                Next
                fstring = fstring + "(" + sstring + ") AND "
            End If
        End If

        'Salesman
        If persistant.myuserLEVEL = 6 Then
            If comboSalesman.SelectedItem.ToString <> "All" Then
                fstring = fstring + "(b.bizman = '" + persistant.myuserID + "' OR b.bizman = '' OR b.bizman is null) AND "
            End If
        Else
            If comboSalesman.SelectedItem.ToString <> "All" Then
                fstring = fstring + "(b.Salesman = '" + persistant.myuserID + "' OR b.Salesman2 = '" + persistant.myuserID + "') AND "
            End If
        End If


        'NAME
        If txtbuyername.Text <> "" Then
            fstring = fstring + "(Buyer1_First like '%" + txtbuyername.Text + "%' OR Buyer1_Last like '%" + txtbuyername.Text + "%' OR Buyer2_First like '%" + txtbuyername.Text + "%' OR Buyer2_Last like '%" + txtbuyername.Text + "%') AND "
        End If

        'BOS
        If txtBOS.Text <> "" Then
            fstring = fstring + "b.BOS_number = '" + txtBOS.Text.ToString + "' AND "
        End If

        'Commision Mode 
        If chkcommisioned.Checked = True Then
            fstring = fstring + "b.commisoned = 'NO' AND "
        End If

        'Status

        If listStatus.CheckedItems.Count() = 0 Then
            sstring = "b.Status = 'x'"
        Else
            sstring = ""
            For x As Integer = 0 To (listStatus.Items.Count() - 1)
                If listStatus.GetItemCheckState(x) = CheckState.Checked Then
                    If sstring <> "" Then
                        sstring = sstring + ", "
                    End If
                    If x = 0 Then
                        sstring = sstring + "'" + (x).ToString + "'"
                    Else
                        sstring = sstring + "'" + (x + 1).ToString + "'"
                    End If
                End If
            Next x
        End If
        fstring = fstring + "s.code IN (" + sstring + ")"

        buildsql = "Select b.bos_number as BOS, b.store as Store, " _
& "b.salesman as Salesman, b.salesman2 as Split, CONCAT(b.buyer1_last, ', ', b.buyer1_first, '  ', b.buyer2_last, ' ', b.buyer2_first) as Name, i.boat_year as Year, " _
& "i.boat_brand as Make, i.boat_model as Model, s.state as 'Status', " _
& "h.state as 'Shop Status', f.state as 'Finance Status', b.bizman as 'Business Manager', b.date_sold as 'Date Sold', " _
& "b.locked as 'Locked', b.proposed_delivery_date as " _
& "'Proposed Delivery Date', b.date_delivered as 'Date Delivered', b.commisoned as 'Commissioned', b.lastmodified as 'Last Modified' " _
& "from bos as b, inventory as i, status as s, financestatus as f, shopstatus as h where " _
& "b.control_number = i.control_number and b.status = s.code and b.finance_status = " _
& "f.code and b.shop_status = h.code AND " & fstring & " ORDER BY b.Date_Sold DESC, b.BOS_number DESC"

        fstring = ""
        sstring = ""


        'Dim sdf As String = buildsql()
        'fstring = ""
        'sstring = ""
    End Function


#Region " Side Menu Items"

    Private Sub btnService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnService.Click

        ' Dim reporter As New frmCheckIn
        ' reporter.persistant = persistant
        ''reporter.Viewnote = True
        'reporter.NoteNumber = 1
        'reporter.ShowDialog()
        'If reporter.DialogResult = Windows.Forms.DialogResult.OK Then MessageBox.Show("The result was Ok")


        '   Dim reporter As New frmAddCustomer
        '   reporter.persistant = persistant
        '   reporter.ShowDialog()
        '   If reporter.DialogResult = Windows.Forms.DialogResult.OK Then MessageBox.Show(reporter.CustomerNumber.ToString)

        Dim x As New frmServiceMain
        x.persistant = Me.persistant
        x.Show()
    End Sub

    Private Sub cmdNewBOS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewBOS.Click
        If persistant.mystore <> "Admin" Then 'And persistant.myuserLEVEL < 5
            Dim bos1 As New frmBOS
            bos1.persistant = persistant
            bos1.mybosstorecode = persistant.mystoreCODE
            bos1.Show()
        Else
            Dim bosstarter As New frmadmnbos
            bosstarter.persistant = persistant
            bosstarter.Show()
        End If
    End Sub

    Private Sub btnViewInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewInventory.Click
        Dim inv1 As New frmInv
        inv1.persistant = persistant
        inv1.Show()
    End Sub

    Private Sub btnAddInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddInventory.Click
        'Dim addboats1 As New frmaddboatsn
        Dim addboats1 As New frmAddBoats
        addboats1.persistant = persistant
        addboats1.Show()
    End Sub

    Private Sub btnRequestOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequestOrders.Click
        Dim form As New FrmBoats2Order
        form.mode = 1
        form.persistant = persistant
        form.Show()
    End Sub

    Private Sub btnApprovedOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApprovedOrders.Click
        Dim form As New FrmBoats2Order
        form.mode = 2
        form.persistant = persistant
        form.Show()
    End Sub

    Private Sub btnViewReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewReports.Click
        Dim reporter As New frmReportSelection
        reporter.persistant = persistant
        reporter.Show()
    End Sub

    Private Sub btnViewStats_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewStats.Click
        Dim statsviewer As New frmStats
        statsviewer.persistant = Me.persistant
        statsviewer.Show()
    End Sub

    Private Sub btnUserManagement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserManagement.Click
        Dim frm As New frmUsers
        frm.persistant = persistant
        frm.Show()
    End Sub

    Private Sub btnCommissions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCommissions.Click
        Dim genc As New FrmGenComm
        genc.persistant = persistant
        genc.ShowDialog()
    End Sub

    Private Sub btnEditDefaultNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditDefaultNote.Click
        Dim temp As New FRMnote
        temp.persistant = Me.persistant
        temp.ShowDialog()
    End Sub

    Private Sub btnQuickPaymentCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuickPaymentCalc.Click
        Dim calc As New frmQuickPaymentCalc
        calc.persistant = persistant
        calc.Show()
    End Sub

    Private Sub btnDeliveryList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeliveryList.Click
        Dim Delivery As New FrmDeliveryList
        Delivery.persistant = persistant
        Delivery.Show()
    End Sub

    Private Sub lblTesting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTesting.Click

    End Sub

    Private Sub btnWinterizeEmails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWinterizeEmails.Click
        Dim eml As New frmEmail
        eml.persistant = persistant
        eml.ShowDialog()
    End Sub

    Private Sub btnChangeMyPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeMyPassword.Click
        Dim chngp As New frmchangepass
        chngp.persistant = persistant
        chngp.ShowDialog()
    End Sub

    'Private Sub lblWarrantyClaims_Click(sender As System.Object, e As System.EventArgs)
    '    Dim x As New frmWarrantyClaimsMain
    '    x.persistant = Me.persistant
    '    x.Show()
    'End Sub

#End Region

    Private Sub DataView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataView.CellContentClick
        Dim xRow As Integer
        Dim xStore As String
        Dim xBosn As Integer
        If e.ColumnIndex = 0 Then
            xRow = e.RowIndex
            xStore = DataView.Item(2, xRow).Value
            xBosn = DataView.Item(1, xRow).Value
            Dim bos As New frmBOS
            bos.persistant = persistant
            bos.mybosstorecode = xStore
            bos.mybosnumber = xBosn
            bos.downloaddata()
            bos.Show()
        End If

    End Sub

    Private Sub listshow_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listshow.SelectedIndexChanged

        For y As Integer = 0 To Me.DataView.Columns.Count - 1
            If Me.DataView.Columns(y).Name <> "locked" Then Me.DataView.Columns(y).Visible = True
        Next
        For x As Integer = 0 To Me.listshow.Items.Count - 1
            If listshow.GetItemCheckState(x) = CheckState.Unchecked Then
                For y As Integer = 0 To Me.DataView.Columns.Count - 1
                    If Me.listshow.Items(y) = Me.DataView.Columns(x).Name Then
                        Me.DataView.Columns(x).Visible = False
                    End If
                Next
            End If

        Next
    End Sub

    Private Sub recolorlist(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataView.ColumnHeaderMouseClick
        recolorlist()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        txtloading.Visible = True
        Me.Refresh()
        doit()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Dim ver As String = Mid(My.Application.Info.Version.ToString.Replace(".", ""), 1, 3)
        Dim version As Decimal = CDec(ver.Substring(0, 1) & "." & ver.Substring(1)) ' 3.26
        Dim dataVer As Double = Val(persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = 'Software_version'", 0))
        If dataVer > version Then
            'update
            MessageBox.Show("Version " & version.ToString & " of this software is out of date." & vbNewLine & "Version " & dataVer.ToString & " will be downloaded now.")
            Dim x As New frmFTPGet
            x.serveraddress = My.Settings.Servername
            x.ShowDialog()
            Me.Close()

            'Else
            '    'No update needed
            '    'Check for special users
            '    If persistant.myuserID = "BZue" Then
            '        Me.Visible = False
            '        Dim x As New RepScreen
            '        x.persistant = persistant
            '        x.brand = "Monterey"
            '        x.ShowDialog()
            '        Me.Close()
            '    End If


            '    If persistant.myuserID = "BZue2" Then
            '        Me.Visible = False
            '        Dim x As New RepScreen
            '        x.persistant = persistant
            '        x.brand = "Monterey"
            '        x.ShowDialog()
            '        Me.Close()
            '    End If



        End If
        'If persistant.mystoreCODE = "EDM" Then

        'check for service access
        If persistant.myuserLEVEL < 1 Then
            If persistant.myuserLEVEL < 0 Then
                'Tech
                Dim x As New FrmTechScrb
                x.persistant = Me.persistant
                x.mainform = Me

                x.ShowDialog()
                Me.Close()
            Else
                'Service Manager/Writer
                Dim x As New frmServiceMain
                x.persistant = Me.persistant
                x.mainform = Me

                x.ShowDialog()
                Me.Close()
            End If
        Else
            Me.Visible = True
        End If
        'Else
        ''Its a sales user and no update is needed
        'Me.Visible = True
        'End If

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        'This time i currerntly invoked every 2 minutes
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        Try
            If CheckForInternetConnection() Then
                'internet available after a few attempts
                If internetCheckCounter > 0 Then
                    Me.Visible = False
                    Dim Loadingfrm As New frmload
                    Loadingfrm.data = persistant
                    Loadingfrm.Show()
                    internetCheckCounter = 0
                    'MessageBox.Show("Internet connection and your MMSOnline onnection has been re-established!", "Notification - MMSOnline connections re-established", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, True)
                Else
                    conn.Open()
                    cmd.Connection = conn
                    cmd.CommandText = "SELECT DRAW from printdata where Data ='Software_version'"
                    cmd.ExecuteNonQuery()
                    conn.Close()
                End If

            Else
                internetCheckCounter = internetCheckCounter + 1
                'Notify user
                MessageBox.Show("You have lost internet connection.The system will keep trying to reconnect every 1 minute until the internet connection has been re-established.Click ok and make sure you re-establish internet connection.", "Critical Warning - Internet Connection lost!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, True)
            End If
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
        conn.Dispose()

    End Sub

    Private Sub BtnStatusNon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStatusNon.Click
        For x As Integer = 0 To (listStatus.Items.Count - 1)
            listStatus.SetItemChecked(x, False)
        Next x
    End Sub

    Private Sub BtnStatusAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStatusAll.Click
        For x As Integer = 0 To (listStatus.Items.Count - 1)
            listStatus.SetItemChecked(x, True)
        Next x
    End Sub

    Private Sub BtnStoreNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStoreNone.Click
        For x As Integer = 0 To (listStore.Items.Count - 1)
            listStore.SetItemChecked(x, False)
        Next x
    End Sub

    Private Sub BtnStoreAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStoreAll.Click
        For x As Integer = 0 To (listStore.Items.Count - 1)
            listStore.SetItemChecked(x, True)
        Next x
    End Sub

    Private Sub btnDeliverySchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeliverySchedule.Click
        Dim Scheduler As New frmServiceSchedule
        Scheduler.persistant = persistant
        Scheduler.Store = persistant.mystoreCODE
        Scheduler.Department = "Sales"
        Scheduler.ShowDialog()
    End Sub
    Public Shared Function CheckForInternetConnection() As Boolean
        Try
            Using client = New Net.WebClient()
                Using stream = client.OpenRead("http://www.google.com")
                    Return True
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function
End Class