Imports MySql.Data.MySqlClient
Public Class Functions
    Public Shared Sub btndoesdisplay(ByVal sender As Object, ByVal myuserlevel As Integer, ByVal minlevel As Integer)
        If myuserlevel < minlevel Then
            sender.Visible = False
        Else
            sender.visible = True
        End If
    End Sub

    Public Sub updateWOPrioritylist(ByVal WO As String, ByVal store As String, ByRef connstring As String)
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter
        Dim sql, sql2, sql3, temps As String
        Dim datatableA, datatableB, datatablec As New DataTable
        Dim nextpriority As Integer
        Dim priority, number, ptemp As Integer
        conn.ConnectionString = connstring

        sql = "select distinct `ServiceIssue`.`Assigned` AS `Tech` from (`ServiceIssue` join `ServiceWOtoIssue` on((`ServiceIssue`.`Issue` = `ServiceWOtoIssue`.`Issue`))) where ((`ServiceIssue`.`Assigned` <> _latin1'') and (`ServiceIssue`.`Assigned` is not null))"
        sql = sql + " AND `ServiceWOtoIssue`.`WOnumber` = '" + WO + "' AND `ServiceWOtoIssue`.`WOstore` = '" + store + "'"
        sql2 = "SELECT number, tech, priority from ServicePriority where WO = '" + WO + "' AND Store = '" + store + "'"
        sql3 = "SELECT number, priority from ServicePriority where tech = "

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = sql
        adapter.SelectCommand = cmd
        adapter.Fill(datatableA)

        cmd.CommandText = sql2
        adapter.SelectCommand = cmd
        adapter.Fill(datatableB)

        If datatableB.Rows.Count > 0 Then
            'Check if all techs listed in priority list are still assigned to WO
            For x As Integer = 0 To datatableB.Rows.Count - 1
                If howmanyrows(datatableA, "Tech = '" + datatableB.Rows(x).Item("tech") + "'") = 0 Then
                    'tech no longer asssigned to WO, remove him
                    number = CInt(datatableB.Rows(x).Item("number"))
                    cmd.CommandText = "Delete from ServicePriority Where Number = " + number.ToString
                    cmd.ExecuteNonQuery()

                    priority = CInt(getvalue(datatableB, "priority", "number = " + number.ToString, 0))
                    cmd.CommandText = sql3 + "'" + datatableB.Rows(x).Item("tech") + "' AND priority > " + priority.ToString
                    adapter.SelectCommand = cmd
                    adapter.Fill(datatablec)

                    If datatablec.Rows.Count > 0 Then
                        For y As Integer = 0 To datatablec.Rows.Count - 1
                            ptemp = CInt(getvalue(datatablec, "priority", "", y))
                            number = CInt(getvalue(datatablec, "number", "", y))
                            cmd.CommandText = "Update ServicePriority Set priority = " + (ptemp - 1).ToString + " where number = " + number.ToString
                            cmd.ExecuteNonQuery()
                        Next
                    End If


                End If
            Next
        End If

        If datatableA.Rows.Count > 0 Then
            'Check if all techs listed are on priority list
            For x As Integer = 0 To datatableA.Rows.Count - 1
                If howmanyrows(datatableB, "tech = '" + datatableA.Rows(x).Item(0) + "'") = 0 Then
                    'tech not shown in priority list add 
                    cmd.CommandText = "SELECT MAX(priority) from ServicePriority where Tech = '" + datatableA.Rows(x).Item(0) + "'"
                    temps = cmd.ExecuteScalar.ToString
                    If temps = "" Then
                        nextpriority = 0

                    Else
                        nextpriority = CInt(temps)
                    End If

                    nextpriority = nextpriority + 1
                    cmd.CommandText = "Insert into ServicePriority Set Tech = '" + datatableA.Rows(x).Item(0) + "', Store = '" + store + "', WO = '" + WO + "', Priority = '" + nextpriority.ToString + "'"
                    cmd.ExecuteNonQuery()
                End If
            Next
        End If

        conn.Close()
        conn.Dispose()

    End Sub
    Public Sub wodoneremovefrompriority(ByVal WO As String, ByVal store As String, ByRef connstring As String)

        '  Try
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter
        Dim sql, sql2 As String
        Dim datatableA, datatableB As New DataTable
        Dim priority, number, ptemp As Integer

        conn.ConnectionString = connstring
        sql = "select distinct `ServiceIssue`.`Assigned` AS `Tech` from (`ServiceIssue` join `ServiceWOtoIssue` on((`ServiceIssue`.`Issue` = `ServiceWOtoIssue`.`Issue`))) where ((`ServiceIssue`.`Assigned` <> _latin1'') and (`ServiceIssue`.`Assigned` is not null))"
        sql = sql + " AND `ServiceWOtoIssue`.`WOnumber` = '" + WO + "' AND `ServiceWOtoIssue`.`WOstore` = '" + store + "'"

        conn.Open()
        cmd.Connection = conn
        cmd.CommandText = sql
        adapter.SelectCommand = cmd
        adapter.Fill(datatableA)

        If datatableA.Rows.Count > 0 Then
            'For each tech remove this WO from prioritys
            'For each tech find all WOs with a higher priority and move them all down one
            For x As Integer = 0 To datatableA.Rows.Count - 1
                If datatableA.Rows(x).Item(0).ToString.Length <> 0 Then
                    cmd.CommandText = "Select number, tech, store, WO, priority from ServicePriority where tech = '" + datatableA.Rows(x).Item(0) + "'"
                    adapter.SelectCommand = cmd
                    datatableB.Clear()
                    adapter.Fill(datatableB)
                    priority = CInt(getvalue(datatableB, "priority", "WO = " + WO + " AND Store = '" + store + "'", 0))
                    number = CInt(getvalue(datatableB, "number", "WO = " + WO + " AND Store = '" + store + "'", 0))
                    cmd.CommandText = "Delete from ServicePriority where number = " + number.ToString
                    cmd.ExecuteNonQuery()
                    If howmanyrows(datatableB, "priority > " + priority.ToString) > 0 Then
                        For y As Integer = 0 To (howmanyrows(datatableB, "priority > " + priority.ToString) - 1)
                            ptemp = CInt(getvalue(datatableB, "priority", "priority > " + priority.ToString, y))
                            number = CInt(getvalue(datatableB, "number", "priority > " + priority.ToString, y))
                            cmd.CommandText = "Update ServicePriority Set priority = " + (ptemp - 1).ToString + " where number = " + number.ToString
                            cmd.ExecuteNonQuery()
                        Next
                    End If
                End If
            Next
        End If

        conn.Close()
        conn.Dispose()
        '  Catch ex As Exception

        ' End Try


    End Sub

    Private Function getvalue(ByVal table As DataTable, ByVal col As String, ByVal filter As String, ByVal row As Integer) As String
        Dim temptable As New DataGridView
        Dim temprow As DataRowView

        Dim colnumber As Integer
        Dim b As New BindingSource
        b.DataSource = table
        b.Filter = filter
        temptable.DataSource = b

        For x As Integer = 0 To (table.Columns.Count - 1)
            If table.Columns(x).ColumnName.ToLower = col.ToLower Then colnumber = x
        Next

        '  MessageBox.Show(colnumber.ToString)
        '  getvalue = temptable.Item(colnumber, row).Value.ToString
        If b.Count < row Or b.Count = 0 Then
            getvalue = ""
        Else
            temprow = b.Item(row)
            getvalue = temprow.Item(colnumber).ToString()
        End If
    End Function
    Private Function howmanyrows(ByVal table As DataTable, ByVal filter As String) As Integer
        howmanyrows = 0
        Dim b As New BindingSource
        b.DataSource = table
        b.Filter = filter
        howmanyrows = b.Count
    End Function

End Class
