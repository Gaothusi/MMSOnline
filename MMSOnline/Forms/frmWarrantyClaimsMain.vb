Imports MySql.Data.MySqlClient
Imports System.Data
'Imports Microsoft.Office.Interop.Excel



Public Class frmWarrantyClaimsMain

    Public persistant As PreFetch

    Private mydata As New DataTable
    Private newdata As New DataTable
    Private bindingsource1 As New BindingSource
    Private binded As Boolean
    Private warrantyType As String = ""

    Private Sub frmWarrantyClaims_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        buildlists()
        permisions()
        cboWarrantyType.SelectedItem = "All"
    End Sub
    Private Sub permisions()
        'If persistant.myuserLEVEL = 1 Or persistant.myuserLEVEL > 7 Then
        'Else
        '    btnNewWO.Visible = False
        '    btnprioritize.Text = "View Assigned Work"
        'End If
        'If persistant.myuserLEVEL > 7 Then
        '    ShowHideMain.Visible = False
        'End If
    End Sub
    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Search()

    End Sub
    Private Sub Search()
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
        'btnExport.Visible = True
        Me.Refresh()
    End Sub

    Private Sub btnExport_Click(sender As System.Object, e As System.EventArgs) Handles btnExport.Click
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter
        Dim dt As New DataTable
        Dim cell As String
        Dim skipped As Integer = 0

        Dim SQL As String
        SQL = buildsql(True)

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd


            'newdata.Clear()
            adapter.Fill(dt)
            conn.Close()
            'refreshdata(newdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
            Exit Sub
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()

        If persistant.excel Is Nothing Then
            persistant.excel = New Microsoft.Office.Interop.Excel.Application
        End If
        Dim wb As Microsoft.Office.Interop.Excel.Workbook
        Dim ws As Microsoft.Office.Interop.Excel.Worksheet
        'wb = Me.AxSpreadsheet1.Workbooks.Item(1)
        'ws = wb.Sheets.Item(0)
        wb = persistant.excel.Workbooks.Add()
        wb.Activate()
        ws = wb.Worksheets.Item(1)

        For x As Integer = 0 To dt.Columns.Count - 1
            cell = letterx(x + 1 - skipped) + (1).ToString
            ws.Cells.Range(cell, cell).Value() = dt.Columns(x).ColumnName
            For y As Integer = 0 To dt.Rows.Count - 1
                cell = letterx(x + 1 - skipped) + (y + 2).ToString
                ws.Cells.Range(cell, cell).Value() = dt.Rows(y).Item(x).ToString
            Next
            'If dt.Columns(x).ColumnName.ToLower = "price" Or dt.Columns(x).ColumnName.ToLower = "discount" Then
            '    ws.Range(letterx(x + 1 - skipped) + (2).ToString, letterx(x + 1 - skipped) + (1000).ToString).NumberFormat = "$#,##"
            'End If
        Next

        Dim selection As Microsoft.Office.Interop.Excel.Range
        selection = ws.Range("a1").CurrentRegion
        selection.RowHeight = 13

        'ws.Range("A1").CurrentRegion.Sort(1, XlSortOrder.xlAscending, 2, Nothing, XlSortOrder.xlDescending, 3, XlSortOrder.xlDescending, XlYesNoGuess.xlYes, Nothing, Nothing, XlSortOrientation.xlSortRows)
        ws.Range("A1", "Z1").Font.Bold = True

        ws.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape
        ws.Columns.AutoFit()
        ws.PageSetup.Zoom = False

        ws.PageSetup.FitToPagesTall = 1
        ws.PageSetup.FitToPagesWide = 1
        ws.PageSetup.TopMargin = 0.1
        ws.PageSetup.BottomMargin = 0.1
        ws.PageSetup.LeftMargin = 0.1
        ws.PageSetup.RightMargin = 0.1

        'For x As Integer = 0 To 200
        '    ws.Columns(letterx(x)).Hidden = True
        'Next
        'ws.Columns("A").Hidden = False
        'ws.Columns("B").Hidden = False
        'ws.Columns("C").Hidden = False
        'ws.Columns("D").Hidden = False
        'ws.Columns("E").Hidden = False
        'ws.Columns("F").Hidden = False
        'ws.Columns("G").Hidden = False
        'ws.Columns("H").Hidden = False

        'ws.Columns("I").Hidden = False
        'ws.Columns("O").Hidden = False
        'ws.Columns("P").Hidden = False
        'ws.Columns("S").Hidden = False
        'ws.Columns("V").Hidden = False
        'ws.Columns("AA").Hidden = False
        'ws.Columns("AB").Hidden = False
        'ws.Columns("AD").Hidden = False
        persistant.excel.Visible = True
        persistant.excel.Windows(1).ScrollColumn = 1
        Me.Close()

    End Sub

    Private Function letterx(ByVal input As Integer) As String
        Dim alphabit, tempresult As String
        Dim x As Integer

        alphabit = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        tempresult = ""
        If input > 26 Then
            x = (input - (input Mod 26)) / 26
            If input Mod 26 = 0 Then
                tempresult = Mid(alphabit, x - 1, 1)
            Else
                tempresult = Mid(alphabit, x, 1)
            End If

        End If
        x = input Mod 26
        If x = 0 Then x = 26

        tempresult = tempresult + Mid(alphabit, x, 1)

        letterx = tempresult
    End Function

    Private Function buildsql(Optional ByVal toExcel As Boolean = False) As String
        Dim statusstring As String = ""
        Dim storestring As String = ""
        Dim fstring As String = ""
        Dim sstring As String = ""

        If toExcel Then
            ', W.TotalBilled as 'Total Billed', W.TotalPaid as 'Total Paid'
            sstring = "SELECT Distinct C.Name as 'Customer Name', M.WONumber as 'W/O #', S.WarrantyClaimNum as 'Manufacturer Claim #', C.Bserial as 'Serial #', S.RequestedAmt as 'Requested Amt', S.ApprovedAmt as 'Approved Amt',S.CreditNum as 'Credit #', S.Status, S.WarrantyType " & _
                        "From ServiceWOtoIssue AS M Inner Join ServiceIssue AS S ON M.Issue = S.Issue Inner Join ServiceWO as W On M.WONumber = W.Number Inner Join ServiceCustomers as C ON S.Customer = C.Number " & _
                        "WHERE S.Payment = 'WARRANTY' "
        Else
            sstring = "SELECT Distinct C.Name, C.Bserial as 'Boat Serial Num', S.Issue, M.WONumber as 'WO Num', C.Mserial as 'Motor Serial Num',M.WOStore as 'Store', S.Type, S.Status, S.Assigned, S.Approval, S.Description, S.WarrantyType " & _
                        "From ServiceWOtoIssue AS M Inner Join ServiceIssue AS S ON M.Issue = S.Issue Inner Join ServiceWO as W On M.WONumber = W.Number Inner Join ServiceCustomers as C ON S.Customer = C.Number " & _
                        "WHERE S.Payment = 'WARRANTY' "
        End If
        'sstring = "SELECT S.Issue, M.WONumber as 'WO Num',M.WOStore as 'Store', S.Type, S.Status, S.Assigned, S.Approval, S.Description FROM ServiceWOtoIssue AS M Inner Join ServiceIssue AS S ON M.Issue = S.Issue  WHERE S.Payment = 'WARRANTY' "
        'SELECT `M`.`Issue`, `S`.`Type`, `S`.`Status`, `S`.`Desc` FROM `ServiceWOtoIssue` AS `M` Inner Join  'ServiceIssue` AS `S` ON `M`.`Issue` = `S`.`Issue` where WOnum = '" + WONumber.ToString + "' AND WOstore = '" + Store + "'"

        'sstring = "Select spo.Store, sp.Status, spo.OrderedBy, sc.Name, spo.PartsOrderID as 'Parts Order', spo.WorkOrderNum as 'Work Order', sp.Supplier, sc.City, sc.Bmake as 'Boat Make', sc.Bmodel as 'Boat Model' " & _
        '    "FROM ServicePartsOrders as spo, ServiceCustomers as sc, ServiceParts as sp " & _
        '    "WHERE spo.CustomerProfile = sc.Number AND spo.PartsOrderID = sp.PartsOrderID AND spo.partsOrderID <> 0 "

        If txtName.Text <> "" Then
            fstring = fstring + " AND C.Name like '%" + txtName.Text + "%'"
        End If
        If txtMotor.Text <> "" Then
            fstring = fstring + " AND C.Mserial like '%" + txtMotor.Text + "%'"
        End If
        If txtWOnum.Text <> "" Then
            fstring = fstring + " AND M.WONumber like '%" + txtWOnum.Text + "%'"
        End If
        If txtBoatSer.Text <> "" Then
            fstring = fstring + " AND C.BSerial like '%" + txtBoatSer.Text + "%'"
        End If
        If chkDateFrom.Checked = True Then
            fstring = fstring + " AND W.DateSchDropOff > '" + Microsoft.VisualBasic.Strings.Format(dtpDateFrom.Value, "yyyy-MM-dd H:mm:ss") + "'"
        End If
        If chkDateTo.Checked = True Then
            fstring = fstring + " AND W.DateSchDropOff < '" + Microsoft.VisualBasic.Strings.Format(dtpDateTo.Value, "yyyy-MM-dd H:mm:ss") + "'"
        End If
        If warrantyType <> "" Then
            fstring = fstring + " AND S.WarrantyType like '%" + warrantyType + "%'"
        End If
        If txtClaimNum.Text <> "" Then
            fstring = fstring + " AND S.WarrantyClaimNum like '%" + txtClaimNum.Text + "%'"
        End If
        If listStore.Items.Count <> listStore.CheckedItems.Count Then
            If listStore.CheckedItems.Count = 0 Then
                fstring = fstring + " AND M.WOStore = 'X'"
            Else

                For x As Integer = 0 To (listStore.Items.Count() - 1)
                    If listStore.GetItemCheckState(x) = CheckState.Checked Then
                        If storestring <> "" Then
                            storestring = storestring + ", "
                        End If
                        storestring = storestring + "'" + listStore.Items.Item(x) + "'"
                    End If
                Next x
                fstring = fstring + " AND M.WOStore IN (" + storestring + ")"
            End If
        End If
        If listStatus.Items.Count <> listStatus.CheckedItems.Count Then
            If listStatus.CheckedItems.Count = 0 Then
                fstring = fstring + " AND S.Approval = 'X'"
            Else
                For x As Integer = 0 To (listStatus.Items.Count() - 1)
                    If listStatus.GetItemCheckState(x) = CheckState.Checked Then
                        If statusstring <> "" Then
                            statusstring = statusstring + ", "
                        End If
                        statusstring = statusstring + "'" + listStatus.Items.Item(x) + "'"
                    End If
                Next x
                fstring = fstring + " AND S.Approval IN (" + statusstring + ")"
            End If
        End If

        buildsql = sstring + fstring
    End Function

    Private Sub buildlists()
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
            If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "ADM" Then Me.listStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Code", "", x))
        Next
        For x As Integer = 0 To (listStore.Items.Count() - 1)
            If listStore.Items(x) = persistant.mystoreCODE Then listStore.SetItemChecked(x, True)
        Next x
        'listStatus.SetItemChecked(0, True)
        For x As Integer = 0 To (listStatus.Items.Count - 1)
            listStatus.SetItemChecked(x, True)
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

        If DataView.RowCount > 0 Then
            DataView.Columns("Edit").Visible = True
            DataView.Columns("Issue").Visible = False
        Else
            DataView.Columns("Edit").Visible = False
        End If

        For x As Integer = 0 To DataView.Rows.Count - 1
            If Not IsDBNull(DataView.Item("Approval", x).Value) Then
                If DataView.Item("Approval", x).Value = "To Be Processed" Then
                    DataView.Rows(x).DefaultCellStyle.BackColor = Color.Khaki
                End If
            End If
            If Not IsDBNull(DataView.Item("Status", x).Value) Then
                If DataView.Item("Status", x).Value = "In Progress" Then
                    DataView.Rows(x).DefaultCellStyle.BackColor = Color.LightCoral
                End If
            End If
        Next

        '       For x As Integer = 0 To DataView.Rows.Count - 1
        'If DataView.Item("Locked", x).Value = 1 Then
        ' DataView.Rows(x).DefaultCellStyle.BackColor = Color.LightSteelBlue
        'End If
        'If persistant.myuserLEVEL = 6 Or persistant.myuserLEVEL = 7 Or persistant.myuserID = "admin" Then
        'If DataView.Item("Finance Status", x).Value = "Unsubmited" And DataView.Item("Business Manager", x).Value.ToString = "" Then
        '    DataView.Rows(x).DefaultCellStyle.BackColor = Color.Yellow
        'End If
        'End If
        'Next
        Me.DataView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
    End Sub
    '#Region " Side Menu Buttons"

    'Private Sub lblNewOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblNewOrder.Click

    '    Dim newPOstore As String = ""

    '    'If admin decided what store
    '    If persistant.myuserLEVEL > 6 Or persistant.mystoreCODE = "ADM" Then
    '        Dim storepicker As New PickStore
    '        storepicker.persistant = Me.persistant
    '        storepicker.ShowDialog()
    '        If storepicker.DialogResult = Windows.Forms.DialogResult.OK Then
    '            newPOstore = storepicker.Store
    '        End If
    '    Else
    '        If persistant.mystoreCODE = "EDM" Or persistant.mystoreCODE = "ATL" Then
    '            newPOstore = "EDM"
    '        Else
    '            'If not Admin or combined store then just use their store
    '            newPOstore = persistant.mystoreCODE
    '        End If
    '    End If

    '    'If we have a store selected we can go on.
    '    If newPOstore <> "" Then
    '        Dim newPO As New frmPartsOrder
    '        newPO.persistant = persistant
    '        newPO.Store = newPOstore
    '        newPO.CustomerNumber = 0
    '        newPO.PartsOrderID = 0
    '        newPO.WONumber = 0
    '        newPO.IssueNumber = 0
    '        If newPO.ShowDialog = Windows.Forms.DialogResult.OK Then
    '            Search()
    '        End If

    '    End If
    'End Sub

    'Private Sub cmbcustomers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcustomers.Click
    '    Dim PickCust As New FrmCustFinder
    '    PickCust.persistant = persistant
    '    PickCust.Selectmode = False
    '    PickCust.Show()
    'End Sub

    '#End Region

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataView.CellContentClick
        Dim xRow As Integer
        Dim xNum As Integer
        Dim xStore As String
        Dim xWONum As Integer

        If e.ColumnIndex = 0 Then
            xRow = e.RowIndex
            xNum = CInt(DataView.Item("Issue", xRow).Value.ToString)
            xStore = DataView.Item("Store", xRow).Value.ToString
            xWONum = DataView.Item("WO Num", xRow).Value
            Dim issue As New FrmIssue
            issue.persistant = persistant
            issue.IssueNumber = xNum
            issue.Store = xStore
            issue.WONumber = xWONum
            'This will refresh the grid with the updated data when it returns
            If issue.ShowDialog = DialogResult.OK Then
                Search()
            End If

        End If
    End Sub

    Private Sub Textboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWOnum.Leave
        sender.text = Replace(sender.text, "'", "")
        sender.text = Replace(sender.text, ";", "")
        sender.text = Replace(sender.text, "\", "")
        sender.text = Replace(sender.text, "/", "")
    End Sub


    Private Sub btnStoreAll_Click(sender As System.Object, e As System.EventArgs) Handles btnStoreAll.Click
        For x As Integer = 0 To (listStore.Items.Count - 1)
            listStore.SetItemChecked(x, True)
        Next x
    End Sub

    Private Sub btnStoreNone_Click(sender As System.Object, e As System.EventArgs) Handles btnStoreNone.Click
        For x As Integer = 0 To (listStore.Items.Count - 1)
            listStore.SetItemChecked(x, False)
        Next x
    End Sub

    Private Sub btnStatusAll_Click(sender As System.Object, e As System.EventArgs) Handles btnStatusAll.Click
        For x As Integer = 0 To (listStatus.Items.Count - 1)
            listStatus.SetItemChecked(x, True)
        Next x
    End Sub

    Private Sub btnStatusNone_Click(sender As System.Object, e As System.EventArgs) Handles btnStatusNone.Click
        For x As Integer = 0 To (listStatus.Items.Count - 1)
            listStatus.SetItemChecked(x, False)
        Next x
    End Sub

    'Private Sub DataView_RowPostPaint(sender As System.Object, e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles DataView.RowPostPaint

    'End Sub

    Private Sub cboWarrantyType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboWarrantyType.SelectedIndexChanged
        warrantyType = cboWarrantyType.SelectedItem
        If warrantyType = "All" Then warrantyType = ""
    End Sub
End Class