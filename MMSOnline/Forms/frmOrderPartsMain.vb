
Imports MySql.Data.MySqlClient
Imports System.Data



Public Class frmOrderPartsMain

    Public persistant As PreFetch
    'Public mainform As Form

    Private mydata As New DataTable
    Private newdata As New DataTable
    Private bindingsource1 As New BindingSource
    Private binded As Boolean

    Private Sub frmOrderParts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        buildlists()
        permisions()
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
        Me.Refresh()
    End Sub

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
        Me.DataView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
    End Sub
    Private Function buildsql() As String
        Dim statusstring As String = ""
        Dim storestring As String = ""
        Dim fstring As String = ""
        Dim sstring As String = ""

        ', sc.City, sc.Bmake as 'Boat Make', sc.Bmodel as 'Boat Model', sc.BSerial as 'Boat Serial'
        sstring = "Select spo.Store, sp.Status, spo.OrderedBy, sc.Name, spo.PartsOrderID as 'Parts Order', spo.WorkOrderNum as 'Work Order', sp.Supplier, sp.PartNumber as 'Part Number',sp.PaymentType as 'Payment Type' " & _
            "FROM ServicePartsOrders as spo, ServiceCustomers as sc, ServiceParts as sp " & _
            "WHERE spo.CustomerProfile = sc.Number AND spo.PartsOrderID = sp.PartsOrderID AND spo.partsOrderID <> 0 "

        If txtname.Text <> "" Then
            fstring = fstring + " AND sc.Name like '%" + txtname.Text + "%'"
        End If
        If txtWOnum.Text <> "" Then
            fstring = fstring + " AND spo.WorkOrderNum like '%" + txtWOnum.Text + "%'"
        End If
        If txtPartsOrderNum.Text <> "" Then
            fstring = fstring + " AND spo.PartsOrderID like '%" + txtPartsOrderNum.Text + "%'"
        End If
        If txtBoatSerNum.Text <> "" Then
            fstring = fstring + " AND sc.BSerial like '%" + txtBoatSerNum.Text + "%'"
        End If
        If txtSupplier.Text <> "" Then
            fstring = fstring + " AND sp.Supplier like '%" + txtSupplier.Text + "%'"
        End If
        If txtPartNum.Text <> "" Then
            fstring = fstring + " AND sp.PartNumber like '%" + txtPartNum.Text + "%'"
        End If
        If txtPartNum.Text <> "" Then
            fstring = fstring + " AND sp.PaymentType like '%" + txtPaymentType.Text + "%'"
        End If
        If listStore.Items.Count <> listStore.CheckedItems.Count Then
            If listStore.CheckedItems.Count = 0 Then
                fstring = fstring + " AND spo.Store = 'X'"
            Else

                For x As Integer = 0 To (listStore.Items.Count() - 1)
                    If listStore.GetItemCheckState(x) = CheckState.Checked Then
                        If storestring <> "" Then
                            storestring = storestring + ", "
                        End If
                        storestring = storestring + "'" + listStore.Items.Item(x) + "'"
                    End If
                Next x
                fstring = fstring + " AND spo.Store IN (" + storestring + ")"
            End If
        End If
        If listStatus.Items.Count <> listStatus.CheckedItems.Count Then
            If listStatus.CheckedItems.Count = 0 Then
                fstring = fstring + " AND sp.Status = 'X'"
            Else
                For x As Integer = 0 To (listStatus.Items.Count() - 1)
                    If listStatus.GetItemCheckState(x) = CheckState.Checked Then
                        If statusstring <> "" Then
                            statusstring = statusstring + ", "
                        End If
                        statusstring = statusstring + "'" + listStatus.Items.Item(x) + "'"
                    End If
                Next x
                fstring = fstring + " AND sp.Status IN (" + statusstring + ")"
            End If
        End If

        buildsql = sstring + fstring
    End Function

#Region " Side Menu Buttons"

    Private Sub lblNewOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblNewOrder.Click

        Dim newPOstore As String = ""

        'If admin decided what store
        If persistant.myuserLEVEL > 6 Or persistant.mystoreCODE = "ADM" Then
            Dim storepicker As New PickStore
            storepicker.persistant = Me.persistant
            storepicker.ShowDialog()
            If storepicker.DialogResult = Windows.Forms.DialogResult.OK Then
                newPOstore = storepicker.Store
            End If
        Else
            If persistant.mystoreCODE = "EDM" Or persistant.mystoreCODE = "ATL" Then
                newPOstore = "EDM"
            Else
                'If not Admin or combined store then just use their store
                newPOstore = persistant.mystoreCODE
            End If
        End If

        'If we have a store selected we can go on.
        If newPOstore <> "" Then
            Dim newPO As New frmPartsOrder
            newPO.persistant = persistant
            newPO.Store = newPOstore
            newPO.CustomerNumber = 0
            newPO.PartsOrderID = 0
            newPO.WONumber = 0
            newPO.IssueNumber = 0
            If newPO.ShowDialog = Windows.Forms.DialogResult.OK Then
                Search()
            End If

        End If
    End Sub

    Private Sub cmbcustomers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcustomers.Click
        Dim PickCust As New FrmCustFinder
        PickCust.persistant = persistant
        PickCust.Selectmode = False
        PickCust.Show()
    End Sub

#End Region

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataView.CellContentClick
        Dim xRow As Integer
        Dim xNum As Integer
        Dim xStore As String

        If e.ColumnIndex = 0 Then
            xRow = e.RowIndex
            xNum = CInt(DataView.Item("Parts Order", xRow).Value.ToString)
            xStore = DataView.Item("Store", xRow).Value.ToString
            Dim PartsOrder As New frmPartsOrder
            PartsOrder.persistant = persistant
            PartsOrder.PartsOrderID = xNum
            PartsOrder.Store = xStore

            'This will refresh the grid with the updated data when it returns
            If PartsOrder.ShowDialog = DialogResult.OK Then
                Search()
            End If
        End If
    End Sub

    Private Sub Textboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtname.Leave, txtWOnum.Leave, txtPartsOrderNum.Leave
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
End Class