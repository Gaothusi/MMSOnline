Imports MySql.Data.MySqlClient
Imports System.Data


Public Class FrmDeliveryList
    Public persistant As PreFetch

    Private mydata, mydata2 As New DataTable
    Private newdata As New DataTable
    Private bindingsource1 As New BindingSource
    Private binded As Boolean

    Private store, storelong As String
    Private tech As String
    Dim conn As New MySqlConnection


    Private Sub refreshdata(ByRef data As DataTable)
        mydata.Clear()
        mydata.Merge(data)

        bindingsource1.ResetBindings(False)

        If binded = False Then
            bindingsource1.DataSource = mydata
            DataView.DataSource = bindingsource1
            binded = True
        End If

        Me.DataView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
    End Sub

    Private Function buildsql() As String
        'Dim selecttext As String = "select `ServiceCustomers`.`Name` AS `Name`,`bos`.`Salesman` AS `Salesman`,`ServiceWO`.`Number` AS `WO`,`ServiceWO`.`Store` AS `Store`,`ServiceWO`.`DateReqComp` AS `Delivery Time`,DATE_FORMAT(`ServiceWO`.`DateReqComp`,'%W, %M %e, %Y') as Date, DATE_FORMAT(`ServiceWO`.`DateReqComp`,'%h:%i %p') as Time, `ServiceIssue`.`Type` AS `Type`,`ServiceIssue`.`Status` AS `Status`,`ServiceIssue`.`Assigned` AS `Tech`,`ServiceIssue`.`Description` AS `Description`,`ServiceWOtoIssue`.`Issue` AS `Issue`,`bos`.`Protech_price` AS `ProtechPrice` from ((((`ServiceWO` join `bos` on(((`ServiceWO`.`BOS` = `bos`.`BOS_number`) and (`ServiceWO`.`Store` = `bos`.`Store`)))) join `ServiceWOtoIssue` on(((`ServiceWO`.`Number` = `ServiceWOtoIssue`.`WOnumber`) and (`ServiceWO`.`Store` = `ServiceWOtoIssue`.`WOstore`)))) join `ServiceIssue` on((`ServiceWOtoIssue`.`Issue` = `ServiceIssue`.`Issue`))) join `ServiceCustomers` on((`ServiceWO`.`CustomerProfile` = `ServiceCustomers`.`Number`))) where (`ServiceWO`.`Status` in ('Requested Rig', 'Approved Rig', 'Closed')) AND (`bos`.`Status` IN ('2', '3')) "
        Dim selecttext As String = "select DISTINCT ServiceCustomers.Name AS Name,bos.Salesman AS Salesman,concat(inventory.boat_Year,' ',inventory.boat_Brand,' ',inventory.boat_Model) as 'Boat Type', ServiceWO.Number AS WO,ServiceWO.Store AS Store, " & _
            "ServiceWO.DateReqComp AS `Delivery Time`,DATE_FORMAT(ServiceWO.DateReqComp,'%W, %M %e, %Y') as Date, DATE_FORMAT(ServiceWO.DateReqComp,'%h:%i %p') as Time, " & _
            "ServiceIssue.Status AS Status,ServiceIssue.Assigned AS Tech,ServiceIssue.Description AS Description, " & _
            "ServiceIssue.Type AS Type,ServiceWOtoIssue.Issue AS Issue,bos.Protech_price AS ProtechPrice " & _
            "from (((((ServiceWO join bos on((ServiceWO.BOS = bos.BOS_number) and (ServiceWO.Store = bos.Store))) " & _
                "join ServiceWOtoIssue on((ServiceWO.Number = ServiceWOtoIssue.WOnumber) and (ServiceWO.Store = ServiceWOtoIssue.WOstore))) " & _
                "join ServiceIssue on(ServiceWOtoIssue.Issue = ServiceIssue.Issue)) " & _
                "join ServiceCustomers on(ServiceWO.CustomerProfile = ServiceCustomers.Number)) " & _
                "join inventory on(bos.BOS_number = inventory.BOS_number)) " & _
            "where (ServiceWO.Status in ('Requested Rig', 'Approved Rig')) AND (bos.Status IN ('2', '3', '4', '5', '6', '16', '17', '18', '19', '20')) "
        '
        '"where (ServiceWO.Status in ('Requested Rig', 'Approved Rig', 'Closed')) AND (bos.Status IN ('2', '3', '4', '5', '6', '16', '17')) "
        '"where (ServiceWO.Status in ('Requested Rig', 'Approved Rig', 'Closed')) AND (bos.Status IN ('2', '3')) "
        buildsql = selecttext + " AND ServiceWO.Store in (" + store + ")"
    End Function

    Private Sub buildlists()
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
            If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "ADM" Then Me.liststore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "", x))
        Next
        For x As Integer = 0 To (liststore.Items.Count() - 1)
            If liststore.Items(x) = persistant.mystore Then liststore.SelectedItem = liststore.Items(x) ' liststore.SelectedItems.Add(liststore.Items(x))
        Next x
        If persistant.mystore = "Admin" Then liststore.SelectedItem = "Shipwreck Edmonton"
    End Sub

    Private Sub FrmDeliveryList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        buildlists()
        conn.ConnectionString = persistant.myconnstring
        conn.Open()
        If persistant.myuserLEVEL < 3 Then
            liststore.Visible = False
            Label2.Visible = False
        End If
        If persistant.myuserLEVEL = 1 Or persistant.myuserLEVEL > 3 Then
            CheckBox1.Visible = True
            CheckBox1.CheckState = CheckState.Checked
        Else
            CheckBox1.Visible = False
        End If
        Go()
    End Sub

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
        If CheckBox1.Checked = False Then
            newdata.Columns.Add("Delete")
            For x As Integer = 0 To newdata.Rows.Count - 1
                'For every row
                If x <> newdata.Rows.Count - 1 Then
                    'if its not the last row
                    For xx As Integer = x + 1 To newdata.Rows.Count - 1
                        'Check every line past this line to see if its from the same WO.
                        If newdata.Rows(x).Item("WO") = newdata.Rows(xx).Item("WO") Then
                            'if they are the same add that and zap it
                            newdata.Rows(x).Item("Type") = newdata.Rows(x).Item("Type").ToString + " " + newdata.Rows(xx).Item("Type").ToString
                            newdata.Rows(x).Item("Status") = newdata.Rows(x).Item("Status").ToString + " " + newdata.Rows(xx).Item("Status").ToString
                            '   newdata.Rows(x).Item("`Description`") = newdata.Rows(x).Item("`Description`").ToString + " " + newdata.Rows(xx).Item("`Description`").ToString
                            newdata.Rows(xx).Item("Delete") = "1"
                            'newdata.AcceptChanges()
                        End If
                    Next
                End If
            Next
            For x As Integer = 0 To newdata.Rows.Count - 1


                Try
                    If newdata.Rows(x).Item("Delete") = "1" Then
                        newdata.Rows(x).Delete()
                    End If
                Catch ex As Exception

                End Try
            Next
            newdata.Columns.Remove("Delete")

            '   DataView.Columns("Issue").Visible = False

        End If

        If newdata.Columns.Contains("Protech") = False Then
            newdata.Columns.Add("Protech")
        End If

        newdata.AcceptChanges()

        For x As Integer = 0 To newdata.Rows.Count - 1
            Try
                If CDec(newdata.Rows(x).Item("ProtechPrice")) > "1" Then
                    newdata.Rows(x).Item("Protech") = "YES"
                Else
                    newdata.Rows(x).Item("Protech") = "NO"
                End If
            Catch ex As Exception
                newdata.Rows(x).Item("Protech") = "NO"
            End Try
        Next

        newdata.AcceptChanges()

        refreshdata(newdata)

        bindingsource1.Sort = "Delivery Time"
        DataView.Columns("Delivery Time").Visible = False
        DataView.Columns("Issue").Visible = False
        DataView.Columns("ProtechPrice").Visible = False
        DataView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader)
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
        If liststore.SelectedItems.Count > 0 Then
            If liststore.SelectedItems(0) = "Shipwreck Edmonton" Or liststore.SelectedItems(0) = "Atlantis" Then
                store = "'EDM', 'ATL'"
                storelong = "'Shipwreck Edmonton', 'Atlantis'"
            Else
                storelong = "'" + liststore.SelectedItems(0) + "'"
                store = "'" + persistant.getvalue(persistant.tbl_location, "Code", "Store = '" + liststore.SelectedItems(0) + "'", 0) + "'"
            End If
        End If
        mydata.Clear()
        'running go here causes an error in the connection. Not sure why
        'figure this out when there is time to work on it.
        'Go()
    End Sub

    Private Sub FrmPriority_UnLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosing
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub

    Private Sub Refreshbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Refreshbtn.Click
        If liststore.SelectedItems.Count = 0 Then
            MessageBox.Show("You need to select a store first.")
        Else
            Go()
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        DataView.Columns("View").Visible = False
        DataView.Columns("Description").Visible = False
        PrintFactory.Print(Me.DataView, "MMSonline", "Delivery list", "L")
        DataView.Columns("View").Visible = True
        DataView.Columns("Description").Visible = True
    End Sub
End Class


