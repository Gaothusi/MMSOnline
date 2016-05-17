Imports MySql.Data.MySqlClient
Imports System.Data



Public Class FrmDoneBin
    Public persistant As PreFetch

    Private mydata As New DataTable
    Private newdata As New DataTable
    Private IssuesforthoseWOs As New DataTable
    Private bindingsource1 As New BindingSource
    Private binded As Boolean

    Private store, storelong As String
    Dim conn As New MySqlConnection

    Private Sub FrmDoneBin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        buildlists()
        conn.ConnectionString = persistant.myconnstring
        conn.Open()
    End Sub

    Private Sub buildlists()
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
            If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "ADM" Then Me.liststore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "", x))
        Next
        For x As Integer = 0 To (liststore.Items.Count() - 1)
            If liststore.Items(x) = persistant.mystore Then liststore.SelectedItems.Add(liststore.Items(x))
        Next x
    End Sub

    Private Sub FrmDoneBin_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
    End Sub


    Private Sub refreshdata(ByVal data As DataTable)
        Dim notdone As Integer = 1

        While notdone <> 0
            notdone = 0

            For x As Integer = 0 To data.Rows.Count - 1
                If data.Rows(x).Item("Status") <> "Completed" Then
                    notdone = CInt(data.Rows(x).Item("WO").ToString)
                End If
            Next
            If notdone <> 0 Then
                For x As Integer = data.Rows.Count - 1 To 0 Step -1

                    If data.Rows(x).Item("WO") = notdone Then
                        data.Rows(x).Delete()
                    End If
                Next
            End If
            data.AcceptChanges()
        End While
        notdone = 1
        Dim last As Integer = 0


        For x As Integer = 0 To data.Rows.Count - 1
            If CInt(data.Rows(x).Item("WO").ToString) = last Then
                data.Rows(x).Delete()
            Else
                last = CInt(data.Rows(x).Item("WO").ToString)
            End If

        Next
        data.AcceptChanges()


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
        Dim selecttext As String = "select `ServicePriority`.`Store` AS `Store`,`ServicePriority`.`WO` AS `WO`,`ServiceIssue`.`Status` AS `Status`,`ServiceCustomers`.`Name` AS `Name` from ((((`ServicePriority` join `ServiceWOtoIssue` on(((`ServicePriority`.`WO` = `ServiceWOtoIssue`.`WOnumber`) and (`ServicePriority`.`Store` = `ServiceWOtoIssue`.`WOstore`)))) join `ServiceIssue` on((`ServiceWOtoIssue`.`Issue` = `ServiceIssue`.`Issue`))) join `ServiceWO` on(((`ServiceWO`.`Number` = `ServicePriority`.`WO`) and (`ServiceWO`.`Store` = `ServicePriority`.`Store`)))) join `ServiceCustomers` on((`ServiceWO`.`CustomerProfile` = `ServiceCustomers`.`Number`)))"
        buildsql = selecttext + " WHERE `ServicePriority`.`Store` IN (" + store + ") order by `ServicePriority`.`WO`"
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

        refreshdata(newdata)
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

End Class