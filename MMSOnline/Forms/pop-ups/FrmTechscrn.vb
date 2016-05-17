Imports MySql.Data.MySqlClient
Imports System.Data


Public Class FrmTechScrb
    Public persistant As PreFetch
    Private mydata As New DataTable
    Private bindingsource1 As New BindingSource
    Private binded As Boolean
    Private store, storelong As String
    Private tech As String
    Public mainform As Form


    Dim conn As New MySqlConnection

    Private Sub FrmPriority_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        buildlists()
        conn.ConnectionString = persistant.myconnstring
        conn.Open()
        storelong = persistant.mystore
        If storelong = "Shipwreck Edmonton" Or liststore.SelectedItems(0) = "Atlantis" Then
            store = "'EDM', 'ATL'"
            storelong = "'Shipwreck Edmonton', 'Atlantis'"
        Else
            storelong = "'" + liststore.SelectedItems(0) + "'"
            store = "'" + persistant.getvalue(persistant.tbl_location, "Code", "Store = '" + liststore.SelectedItems(0) + "'", 0) + "'"
        End If
        tech = persistant.myuserID
        Go()
        Timer1.Enabled = True
    End Sub
    Private Sub buildlists()
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
            If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "ADM" Then Me.liststore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "", x))
        Next
        For x As Integer = 0 To (liststore.Items.Count() - 1)
            If liststore.Items(x) = persistant.mystore Then liststore.SelectedItems.Add(liststore.Items(x))
        Next x
    End Sub

    Private Sub refreshdata(ByVal data As DataTable)
        mydata.Clear()
        mydata.Merge(data)
        bindingsource1.ResetBindings(False)

        If binded = False Then
            bindingsource1.DataSource = mydata
            DataView.DataSource = bindingsource1
            binded = True
        End If


        Me.DataView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader)
    End Sub
    Private Function buildsql() As String
        Dim selecttext As String = "select ServicePriority.number AS Number,ServicePriority.priority AS prioritynumber,ServicePriority.WO AS WO, " & _
                                        "ServicePriority.Store AS Store,ServicePriority.Tech AS Tech,ServiceCustomers.Name AS Name, ServiceWO.DateReqComp AS 'Date Requested', " & _
                                        "ServiceWO.Status AS Status, ServiceCustomers.Bmake AS Make,ServiceCustomers.Bmodel AS Model,ServiceCustomers.Mmodel AS Engine, " & _
                                        "ServiceCustomers.Color AS Color, ServiceWO.Priority AS Priority,ServiceWO.DateActDropOff AS 'Dropped Off' " & _
                                    "from ((ServiceWO join ServicePriority on(((ServiceWO.Number = ServicePriority.WO) and (ServiceWO.Store = ServicePriority.Store)))) " & _
                                        "join ServiceCustomers on((ServiceCustomers.Number = ServiceWO.CustomerProfile))) " & _
                                    " WHERE ServicePriority.Tech = '" + tech + "' and ServiceWO.Status Not IN ('Void') " & _
                                        " and ServicePriority.WO IN (Select swi.WOnumber From ServiceIssue as si join ServiceWOtoIssue as swi on si.Issue = swi.Issue " & _
                                        " where si.Assigned = '" + tech + "' and si.Status Not IN ('Completed')) " & _
                                    " order by ServiceWO.Priority"
        buildsql = selecttext
        'and ServiceWO.Status Not IN ('Complete')

        'Dim selecttext As String = "select `ServicePriority`.`number` AS `number`,`ServicePriority`.`priority` AS `prioritynumber`,`ServicePriority`.`WO` AS `WO`,`ServicePriority`.`Store` AS `Store`,`ServicePriority`.`Tech` AS `Tech`,`ServiceCustomers`.`Name` AS `Name`, `ServiceWO`.`DateReqComp` AS `Date Requested`,`ServiceWO`.`Status` AS `Status`, `ServiceCustomers`.`Bmake` AS `Make`,`ServiceCustomers`.`Bmodel` AS `Model`,`ServiceCustomers`.`Mmodel` AS `Engine`,`ServiceCustomers`.`Color` AS `Color`, `ServiceWO`.`Priority` AS `Priority`,`ServiceWO`.`DateActDropOff` AS `Dropped Off` from ((`ServiceWO` join `ServicePriority` on(((`ServiceWO`.`Number` = `ServicePriority`.`WO`) and (`ServiceWO`.`Store` = `ServicePriority`.`Store`)))) join `ServiceCustomers` on((`ServiceCustomers`.`Number` = `ServiceWO`.`CustomerProfile`)))"
        'buildsql = selecttext + " WHERE `ServicePriority`.`Store` IN (" + store + ") AND `ServicePriority`.`Tech` = '" + tech + "' order by `ServiceWO`.`Priority`"

    End Function
    Private Sub Go()
        txtloading.Visible = True
        Me.Refresh()
        'Just fill newdata and pass it along to refreshdata
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter
        Dim SQL As String
        Dim newdata As New DataTable
        SQL = buildsql()
        Try
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            newdata.Clear()
            adapter.Fill(newdata)
            refreshdata(newdata)
        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        bindingsource1.Sort = "prioritynumber"
        DataView.Columns("Number").Visible = False
        txtloading.Visible = False
        Me.Refresh()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataView.CellContentClick
        If e.RowIndex <> -1 Then
            Dim xRow As Integer
            Dim xcol As Integer
            'Dim xNum As Integer
            Dim cmd As New MySqlCommand
            cmd.Connection = conn
            xcol = e.ColumnIndex
            xRow = e.RowIndex

            'xNum = CInt(DataView.Rows(xRow).Cells("Number").Value)
            'xNum = CInt(mydata.Rows(xRow).Item("Number").ToString)
            If DataView.Columns(xcol).Name = "View" Then
                Dim viewstore, viewnum As String
                viewnum = DataView.Rows(xRow).Cells("WO").Value
                viewstore = DataView.Rows(xRow).Cells("Store").Value
                Dim ViewWO As New FrmWO
                ViewWO.persistant = persistant
                ViewWO.WONumber = CInt(viewnum)
                ViewWO.Store = viewstore
                ViewWO.Show()
                Go()
            End If
        End If
    End Sub
    Private Sub liststore_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles liststore.SelectedValueChanged
        listtech.Items.Clear()
        If liststore.SelectedItems.Count > 0 Then
            If liststore.SelectedItems(0) = "Shipwreck Edmonton" Or liststore.SelectedItems(0) = "Atlantis" Then
                store = "'EDM', 'ATL'"
                storelong = "'Shipwreck Edmonton', 'Atlantis'"
            Else
                storelong = "'" + liststore.SelectedItems(0) + "'"
                store = "'" + persistant.getvalue(persistant.tbl_location, "Code", "Store = '" + liststore.SelectedItems(0) + "'", 0) + "'"
            End If
            For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_users, "Store IN (" + storelong + ") AND Usergroup < 0") - 1)
                listtech.Items.Add(persistant.getvalue(persistant.tbl_users, "User", "Store IN (" + storelong + ") AND Usergroup < 0", x))
            Next
        End If
        mydata.Clear()
    End Sub
    Private Sub listtech_changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listtech.SelectedValueChanged
        tech = listtech.SelectedItems(0)
        Go()
        For Each Col As System.Windows.Forms.DataGridViewColumn In DataView.Columns
            If Col.Name = "prioritynumber" Then
                Col.SortMode = DataGridViewColumnSortMode.Programmatic
            Else
                Col.SortMode = DataGridViewColumnSortMode.NotSortable
            End If
        Next
    End Sub

    Private Sub FrmPriority_UnLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosing
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub

    Private Sub Refreshbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Refreshbtn.Click
        Go()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Go()
    End Sub

    Private Sub ShowHideMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowHideMain.Click
        If mainform.Visible = True Then
            mainform.Visible = False
        Else
            mainform.Visible = True
        End If
    End Sub

    Private Sub PrintMyTime_Click(sender As System.Object, e As System.EventArgs) Handles PrintMyTime.Click
        Dim datePicker As New frmReportTimePicker
        datePicker.persistant = persistant
        datePicker.reportnum = 8
        datePicker.tech = persistant.myuserID
        datePicker.Show()

    End Sub

    Private Sub DataView_RowPostPaint(sender As System.Object, e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles DataView.RowPostPaint

        For x As Integer = 0 To DataView.Rows.Count - 1
            If Not IsDBNull(DataView.Item("Priority", x).Value) Then
                If DataView.Item("Priority", x).Value = "ASAP" Or DataView.Item("Priority", x).Value = "Same Day" Then
                    DataView.Rows(x).DefaultCellStyle.BackColor = Color.LightCoral
                End If
                If DataView.Item("Priority", x).Value = "Rush" Then
                    DataView.Rows(x).DefaultCellStyle.BackColor = Color.Khaki
                End If
            End If
        Next

    End Sub

End Class