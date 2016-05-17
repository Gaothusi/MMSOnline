Imports MySql.Data.MySqlClient
Imports System.Data


Public Class FrmIssuesearch
    Public persistant As PreFetch
    Private mydata As New DataTable
    Private newdata As New DataTable
    Private bindingsource1 As New BindingSource
    Private binded As Boolean
    Private customer As Integer

    Private Sub frmServiceMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        buildlists()
    End Sub
    Private Sub Textboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWOnum.Leave
        sender.text = Replace(sender.text, "'", "")
        sender.text = Replace(sender.text, ";", "")
        sender.text = Replace(sender.text, "\", "")
        sender.text = Replace(sender.text, "/", "")
    End Sub
    Private Sub buildlists()
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
            If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "ADM" Then Me.listStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Code", "", x))
        Next
        For x As Integer = 0 To (listStore.Items.Count() - 1)
            If listStore.Items(x) = persistant.mystoreCODE Then
                listStore.SetItemChecked(x, True)
            End If

        Next
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

        '       For x As Integer = 0 To DataView.Rows.Count - 1
        'If DataView.Item("Locked", x).Value = 1 Then
        ' DataView.Rows(x).DefaultCellStyle.BackColor = Color.LightSteelBlue
        'End If
        'If persistant.myuserLEVEL = 6 Or persistant.myuserLEVEL = 7 Or persistant.myuserID = "admin" Then
        ' If DataView.Item("Finance Status", x).Value = "Unsubmited" And DataView.Item("Business Manager", x).Value.ToString = "" Then
        ' DataView.Rows(x).DefaultCellStyle.BackColor = Color.Yellow
        'End If
        'End If
        'Next
        Me.DataView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader)
    End Sub
    Private Function buildsql() As String
        Dim statusstring As String = ""
        Dim storestring As String = ""
        Dim fstring As String = ""
        Dim sstring As String = ""

        sstring = "SELECT WO2Issue.WOnumber, WO2Issue.WOstore, WO2Issue.Issue, WO.DateReqComp, Cust.Name, Cust.Bmodel, Cust.Bmake, WO.Status, Issue.`Type`, Issue.`Status`, Issue.Payment, Issue.Approval, Issue.Assigned, Issue.Description, Issue.BillableHrs, Issue.BilledHrs FROM ServiceWO AS WO Inner Join ServiceWOtoIssue AS WO2Issue ON WO.Number = WO2Issue.WOnumber AND WO.Store = WO2Issue.WOstore Inner Join ServiceIssue AS Issue ON WO2Issue.Issue = Issue.Issue Inner Join ServiceCustomers AS Cust ON WO.CustomerProfile = Cust.Number WHERE Issue.Issue > 0"
        If txtname.Text <> "ALL" Then
            fstring = fstring + " AND Cust.Number = " + customer.ToString + ""
        End If
        If txtWOnum.Text <> "" Then
            fstring = fstring + " AND WO2Issue.WONumber like '%" + txtWOnum.Text + "%'"
        End If
        'store
        If listStore.Items.Count <> listStore.CheckedItems.Count Then
            If listStore.CheckedItems.Count = 0 Then
                fstring = fstring + " AND WO2Issue.WOstore = 'X'"
            Else

                For x As Integer = 0 To (listStore.Items.Count() - 1)
                    If listStore.GetItemCheckState(x) = CheckState.Checked Then
                        If storestring <> "" Then
                            storestring = storestring + ", "
                        End If
                        storestring = storestring + "'" + listStore.Items.Item(x) + "'"
                    End If
                Next x
                fstring = fstring + " AND WO2Issue.WOstore IN (" + storestring + ")"
            End If
        End If
        'type of work
        If listwork.Items.Count <> listwork.CheckedItems.Count Then
            If listwork.CheckedItems.Count = 0 Then
                fstring = fstring + " AND Issue.`Type` = 'X'"
            Else
                For x As Integer = 0 To (listwork.Items.Count() - 1)
                    If listwork.GetItemCheckState(x) = CheckState.Checked Then
                        If statusstring <> "" Then
                            statusstring = statusstring + ", "
                        End If
                        statusstring = statusstring + "'" + listwork.Items.Item(x) + "'"
                    End If
                Next x
                fstring = fstring + " AND Issue.`Type` IN (" + statusstring + ")"
            End If
        End If
        statusstring = ""

        If liststatus.Items.Count <> liststatus.CheckedItems.Count Then
            If liststatus.CheckedItems.Count = 0 Then
                fstring = fstring + " AND Issue.`Status` = 'X'"
            Else
                For x As Integer = 0 To (liststatus.Items.Count() - 1)
                    If liststatus.GetItemCheckState(x) = CheckState.Checked Then
                        If statusstring <> "" Then
                            statusstring = statusstring + ", "
                        End If
                        statusstring = statusstring + "'" + liststatus.Items.Item(x) + "'"
                    End If
                Next x
                fstring = fstring + " AND Issue.`Status` IN (" + statusstring + ")"
            End If
        End If

        statusstring = ""
        If listwarranty.CheckedItems.Count <> 0 Then
            fstring = fstring + " AND Issue.Payment = 'WARRANTY'"
            If listwarranty.Items.Count <> listwarranty.CheckedItems.Count Then
                For x As Integer = 0 To (listwarranty.Items.Count() - 1)
                    If listwarranty.GetItemCheckState(x) = CheckState.Checked Then
                        If statusstring <> "" Then
                            statusstring = statusstring + ", "
                        End If
                        statusstring = statusstring + "'" + listwarranty.Items.Item(x) + "'"
                    End If
                Next x
                fstring = fstring + " AND Issue.Approval IN (" + statusstring + ")"
            End If
        End If
        buildsql = sstring + fstring

    End Function
    Private Sub cmdGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click
        txtloading.Visible = True
        Me.Refresh()
        'Just fill newdata and pass it along to refreshdata
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
            adapter.SelectCommand = cmd
            newdata.Clear()
            adapter.Fill(newdata)
            conn.Close()
            refreshdata(newdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()

        txtloading.Visible = False
        Me.Refresh()
    End Sub
    Private Sub btnallcustomers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnallcustomers.Click
        txtname.Text = "ALL"
    End Sub
    Private Sub btnAllstores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllstores.Click
        For x As Integer = 0 To (listStore.Items.Count() - 1)
            listStore.SetItemCheckState(x, CheckState.Checked)
        Next
    End Sub
    Private Sub btnalltypes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnalltypes.Click
        For x As Integer = 0 To (listwork.Items.Count() - 1)
            listwork.SetItemCheckState(x, CheckState.Checked)
        Next
    End Sub
    Private Sub btnallstatuss_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnallstatuss.Click
        For x As Integer = 0 To (liststatus.Items.Count() - 1)
            liststatus.SetItemCheckState(x, CheckState.Checked)
        Next
    End Sub
    Private Sub btnallwarranty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnallwarranty.Click
        For x As Integer = 0 To (listwarranty.Items.Count() - 1)
            listwarranty.SetItemCheckState(x, CheckState.Checked)
        Next
    End Sub
    Private Sub btnpickcustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpickcustomer.Click
        Dim PickCust As New FrmCustFinder
        PickCust.persistant = persistant
        PickCust.Selectmode = True
        PickCust.ShowDialog()
        If PickCust.DialogResult = Windows.Forms.DialogResult.OK Then
            customer = PickCust.SelectedProfile
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            Try
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = "Select Name from ServiceCustomers where Number = " + customer.ToString + ""
                txtname = cmd.ExecuteScalar()
                conn.Close()
            Catch ex As Exception
                MessageBox.Show("Error Connecting to Database: " & ex.Message)
            End Try
            If conn.State <> ConnectionState.Closed Then conn.Close()
            conn.Dispose()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataView.CellContentClick
        Dim xRow As Integer
        Dim xNum As Integer
        Dim xVal As String

        If e.ColumnIndex = 0 Then
            xRow = e.RowIndex
            xNum = CInt(mydata.Rows(xRow).Item("WOnumber").ToString)
            xVal = mydata.Rows(xRow).Item("WOstore").ToString
            Dim ViewWO As New FrmWO
            ViewWO.persistant = persistant
            ViewWO.WONumber = xNum
            ViewWO.Store = xVal
            ViewWO.Show()
            '    xNum = CInt(mydata.Rows(xRow).Item("Issue").ToString)

            '   Dim ViewIssue As New FrmIssue
            '  ViewIssue.persistant = persistant
            ' ViewIssue.IssueNumber = xNum
            'ViewIssue.Show()

        End If
    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click
        For x As Integer = 0 To (listwork.Items.Count() - 1)
            listwork.SetItemCheckState(x, CheckState.Checked)
        Next
        For x As Integer = 0 To (liststatus.Items.Count() - 1)
            If liststatus.Items.Item(x).ToString = "To Be Assigned" Then
                liststatus.SetItemCheckState(x, CheckState.Checked)
            Else
                liststatus.SetItemCheckState(x, CheckState.Unchecked)
            End If
        Next
        For x As Integer = 0 To (listwarranty.Items.Count() - 1)
            listwarranty.SetItemCheckState(x, CheckState.Unchecked)
        Next
        txtloading.Visible = True
        Me.Refresh()
        'Just fill newdata and pass it along to refreshdata
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
            adapter.SelectCommand = cmd
            newdata.Clear()
            adapter.Fill(newdata)
            conn.Close()
            refreshdata(newdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()

        txtloading.Visible = False
        Me.Refresh()

    End Sub

    
End Class