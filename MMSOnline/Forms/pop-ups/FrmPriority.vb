Imports MySql.Data.MySqlClient
Imports System.Data


Public Class FrmPriority
    Public persistant As PreFetch

    Private mydata As New DataTable
    Private newdata As New DataTable
    Private IssuesforthoseWOs As New DataTable
    Private bindingsource1 As New BindingSource
    Private binded As Boolean

    Private store, storelong As String
    Private tech As String
    Dim conn As New MySqlConnection

    Private Sub buildlists()
        'for development troubleshooting
        Dim tempStoreName = persistant.mystoreCODE
        If persistant.mystoreCODE = "ADM" Then
            For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
                If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "ADM" Then Me.liststore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "", x))
            Next
        Else
            Me.liststore.Items.Add(persistant.mystore)
        End If

        'end of dev troubeshooting
        'For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
        '    If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "ADM" Then Me.liststore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "", x))
        'Next
        For x As Integer = 0 To (liststore.Items.Count() - 1)
            If liststore.Items(x) = persistant.mystore Then liststore.SelectedItems.Add(liststore.Items(x))
        Next x
    End Sub

    Private Sub refreshdata(ByVal data As DataTable)
        mydata.Clear()
        mydata.Merge(data)

        For x As Integer = 0 To IssuesforthoseWOs.Rows.Count - 1
            For xx As Integer = 0 To mydata.Rows.Count - 1
                If IssuesforthoseWOs.Rows(x).Item("Store") = mydata.Rows(xx).Item("Store") And IssuesforthoseWOs.Rows(x).Item("WO") = mydata.Rows(xx).Item("WO") Then
                    mydata.Rows(xx).Item("Description of Work") = mydata.Rows(xx).Item("Description of Work") + " " + IssuesforthoseWOs.Rows(x).Item("Description of Work")
                End If
            Next
        Next

        bindingsource1.ResetBindings(False)

        If binded = False Then
            bindingsource1.DataSource = mydata
            DataView.DataSource = bindingsource1
            binded = True
        End If


        Me.DataView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader)
    End Sub


    Private Function buildsql() As String
        Dim selecttext As String = "select `ServicePriority`.`number` AS `number`,`ServicePriority`.`priority` AS `prioritynumber`,`ServicePriority`.`WO` AS `WO`,`ServicePriority`.`Store` AS `Store`,`ServicePriority`.`Tech` AS `Tech`,`ServiceCustomers`.`Name` AS `Name`, `ServiceWO`.`DateReqComp` AS `Date Requested`,`ServiceWO`.`Status` AS `Status`, `ServiceCustomers`.`Bmake` AS `Make`,`ServiceCustomers`.`Bmodel` AS `Model`,`ServiceCustomers`.`Mmodel` AS `Engine`,`ServiceCustomers`.`Color` AS `Color`, `ServiceWO`.`Priority` AS `Priority`,'' as 'Description of Work',`ServiceWO`.`DateActDropOff` AS `Dropped Off` from ((`ServiceWO` join `ServicePriority` on(((`ServiceWO`.`Number` = `ServicePriority`.`WO`) and (`ServiceWO`.`Store` = `ServicePriority`.`Store`)))) join `ServiceCustomers` on((`ServiceCustomers`.`Number` = `ServiceWO`.`CustomerProfile`)))"
        buildsql = selecttext + " WHERE `ServicePriority`.`Store` IN (" + store + ") AND `ServicePriority`.`Tech` = '" + tech + "' and ServiceWO.Status = 'Active'   order by `ServiceWO`.`Priority`"
        ' 
    End Function

    Private Function buildsql2() As String
        Dim selecttext As String = "select `ServiceIssue`.`Type` AS `Description of Work`,`ServicePriority`.`Store` AS `Store`,`ServicePriority`.`WO` AS `WO` from ((`ServicePriority` join `ServiceWOtoIssue` on(((`ServicePriority`.`WO` = `ServiceWOtoIssue`.`WOnumber`) and (`ServicePriority`.`Store` = `ServiceWOtoIssue`.`WOstore`)))) join `ServiceIssue` on((`ServiceWOtoIssue`.`Issue` = `ServiceIssue`.`Issue`)))"
        buildsql2 = selecttext + " WHERE `ServicePriority`.`Store` IN (" + store + ") AND `ServicePriority`.`Tech` = '" + tech + "'"
    End Function

    Private Sub Go()
        txtloading.Visible = True
        Me.Refresh()
        'Just fill newdata and pass it along to refreshdata

        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        Dim SQL As String
        SQL = buildsql()

        Try
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            newdata.Clear()
            adapter.Fill(newdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        SQL = buildsql2()
        Try
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            IssuesforthoseWOs.Clear()
            adapter.Fill(IssuesforthoseWOs)
        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        refreshdata(newdata)

        bindingsource1.Sort = "prioritynumber"
        DataView.Columns("number").Visible = False
        txtloading.Visible = False
        Me.Refresh()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataView.CellContentClick

        If e.RowIndex <> -1 Then
            Dim xRow As Integer
            Dim xcol As Integer
            Dim xNum As Integer
            Dim cmd As New MySqlCommand
            cmd.Connection = conn
            xcol = e.ColumnIndex
            xRow = e.RowIndex
            xNum = CInt(mydata.Rows(xRow).Item("Number").ToString)
            If DataView.Columns(xcol).Name = "Up" Then
                If DataView.Rows(xRow).Cells("Prioritynumber").Value <> "1" Then
                    cmd.CommandText = "Update ServicePriority Set Priority = " + CInt((DataView.Rows(xRow).Cells("Prioritynumber").Value) - 1).ToString + " where number = " + DataView.Rows(xRow).Cells("number").Value.ToString + ""
                    cmd.ExecuteNonQuery()
                    cmd.CommandText = "Update ServicePriority Set Priority = " + CInt((DataView.Rows(xRow - 1).Cells("Prioritynumber").Value) + 1).ToString + " where number = " + DataView.Rows(xRow - 1).Cells("number").Value.ToString + ""
                    cmd.ExecuteNonQuery()
                    Go()
                End If
            End If
            If DataView.Columns(xcol).Name = "Down" Then
                If DataView.Rows(xRow).Cells("Prioritynumber").Value <> mydata.Rows.Count.ToString Then
                    cmd.CommandText = "Update ServicePriority Set Priority = " + CInt(DataView.Rows(xRow).Cells("Prioritynumber").Value + 1).ToString + " where number = " + DataView.Rows(xRow).Cells("number").Value.ToString
                    cmd.ExecuteNonQuery()
                    cmd.CommandText = "Update ServicePriority Set Priority = " + CInt((DataView.Rows(xRow + 1).Cells("Prioritynumber").Value) - 1).ToString + " where number = " + DataView.Rows(xRow + 1).Cells("number").Value.ToString + ""
                    cmd.ExecuteNonQuery()
                    Go()
                End If
            End If

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
    Private Sub FrmPriority_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        buildlists()
        conn.ConnectionString = persistant.myconnstring
        conn.Open()
        If persistant.myuserLEVEL < 0 Then
            liststore.SelectedItem = persistant.mystore
            listtech.SelectedItem = persistant.myuserID
            liststore.Visible = False
            listtech.Visible = False
            Label1.Visible = False
            Label2.Visible = False
            DataView.Columns("Up").Visible = False
            DataView.Columns("Down").Visible = False

        End If

    End Sub
    Private Sub FrmPriority_UnLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosing
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()

    End Sub

    Private Sub Refreshbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Refreshbtn.Click
        If liststore.SelectedItems.Count = 0 Or listtech.SelectedItems.Count = 0 Then
            MessageBox.Show("You need to select both a tech and a store first.")
        Else
            Go()
        End If


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