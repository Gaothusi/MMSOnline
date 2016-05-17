Imports MySql.Data.MySqlClient
Imports MMSOnline.inventorydata
Public Class frmOrderBoat
    Public persistant As PreFetch
    Public cancled As Boolean = True
    Public control_number As Integer = 0

    Public boatmake, boatmodel, boatyear, motormake, motormodel, motoryear, equipment, notes, status, colour As String
    Public price As Decimal
    Public bos, ordernumber As Integer
    Public customername, store, salesman As String

    Public Function Startorder(ByVal pbos As Integer, ByVal pstore As String, ByVal psalesman As String, ByVal pcustomer As String) As Integer
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        status = "New Order"
        bos = pbos
        customername = pcustomer
        store = pstore
        salesman = psalesman
        Me.btnApproved.Visible = False
        Me.btnDenied.Visible = False
        Me.btnorder.Visible = False

        boatmake = ""
        boatmodel = ""
        boatyear = ""
        motormake = ""
        motormodel = ""
        motoryear = ""
        equipment = ""
        notes = ""
        status = ""
        colour = ""
        price = 0
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "INSERT into orders SET bos = '" + bos.ToString + "', store = '" + store + "', salesman = '" + salesman + "', customer = '" + customername + "'"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "SELECT max(ordernumber) from orders where bos ='" + bos.ToString + "' and store = '" + store + "'"
            ordernumber = Val(cmd.ExecuteScalar)
            conn.Close()
            Startorder = ordernumber
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
        conn.Dispose()
    End Function
    Public Sub vieworder(ByVal pordernumber As Integer)
        ordernumber = pordernumber
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adpt As New MySqlDataAdapter
        Dim temptable As New DataTable
        conn.ConnectionString = persistant.myconnstring
        cmd.CommandText = "SELECT * from orders where ordernumber = '" + pordernumber.ToString + "'"

        Try
            conn.Open()
            cmd.Connection = conn
            adpt.SelectCommand = cmd
            adpt.Fill(temptable)
            conn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
        conn.Dispose()
        For x As Integer = 0 To (temptable.Columns.Count - 1)
            If temptable.Columns(x).ColumnName.ToLower = "bmake" Then boatmake = temptable.Rows(0).Item(x)
            If temptable.Columns(x).ColumnName.ToLower = "bmodel" Then boatmodel = temptable.Rows(0).Item(x)
            If temptable.Columns(x).ColumnName.ToLower = "byear" Then boatyear = temptable.Rows(0).Item(x)
            If temptable.Columns(x).ColumnName.ToLower = "mmake" Then motormake = temptable.Rows(0).Item(x)
            If temptable.Columns(x).ColumnName.ToLower = "mmodel" Then motormodel = temptable.Rows(0).Item(x)
            If temptable.Columns(x).ColumnName.ToLower = "myear" Then motoryear = temptable.Rows(0).Item(x)
            If temptable.Columns(x).ColumnName.ToLower = "equip" Then equipment = temptable.Rows(0).Item(x)
            If temptable.Columns(x).ColumnName.ToLower = "notes" Then notes = temptable.Rows(0).Item(x)
            If temptable.Columns(x).ColumnName.ToLower = "status" Then status = temptable.Rows(0).Item(x)
            If temptable.Columns(x).ColumnName.ToLower = "colour" Then colour = temptable.Rows(0).Item(x)
            If temptable.Columns(x).ColumnName.ToLower = "price" Then price = Val(temptable.Rows(0).Item(x))
            If temptable.Columns(x).ColumnName.ToLower = "bos" Then bos = Val(temptable.Rows(0).Item(x))
            If temptable.Columns(x).ColumnName.ToLower = "customer" Then customername = temptable.Rows(0).Item(x)
            If temptable.Columns(x).ColumnName.ToLower = "salesman" Then salesman = temptable.Rows(0).Item(x)
            If temptable.Columns(x).ColumnName.ToLower = "store" Then store = temptable.Rows(0).Item(x)
        Next
        cancled = False
        Me.Button1.Visible = False
        Me.btnupdate.Visible = False
        Me.btnApproved.Visible = False
        Me.btnDenied.Visible = False
        Me.btnorder.Visible = False

        If persistant.myuserID = salesman Then

            Me.btnupdate.Visible = True

        End If
        If persistant.myuserLEVEL > 3 Then
            Me.btnApproved.Visible = True
            Me.btnDenied.Visible = True
            Me.btnupdate.Visible = True
            Me.Button1.Visible = True

        End If
        If persistant.myuserLEVEL > 3 And status = "Approved" Then
            Me.btnApproved.Visible = False
            Me.btnDenied.Visible = False
            Me.btnorder.Visible = True
        End If

        writescreen()
    End Sub
    Private Sub writescreen()
        Me.txtbos.Text = bos.ToString
        Me.txtstore.Text = store
        Me.txtsalesman.Text = salesman
        Me.txtcustomer.Text = customername
        If price <> 0 Then
            Me.txtprice.Text = Format(price, "currency")
        Else
            Me.txtprice.Text = ""
        End If

        Me.txtbmodel.Text = boatmodel
        Me.txtbyear.Text = boatyear
        Me.txtmmodel.Text = motormodel
        Me.txtmyear.Text = motoryear
        Me.txtequip.Text = equipment
        Me.txtcomments.Text = notes
        Me.txtcolour.Text = colour
        For x As Integer = 0 To cmbstatus.Items.Count - 1
            If cmbstatus.Items(x).ToString.ToLower = status.ToLower Then cmbstatus.SelectedIndex = x
        Next
        For x As Integer = 0 To cmbbmake.Items.Count - 1
            If cmbbmake.Items(x).ToString.ToLower = boatmake.ToLower Then cmbbmake.SelectedIndex = x
        Next
        For x As Integer = 0 To cmbmake.Items.Count - 1
            If cmbmake.Items(x).ToString.ToLower = motormake.ToLower Then cmbmake.SelectedIndex = x
        Next
        If cmbstatus.SelectedIndex = -1 Then cmbstatus.SelectedItem = "New Order"
    End Sub
    Private Sub updateorder()
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        cmd.CommandText = "UPDATE orders Set bmake = '" + Me.cmbbmake.SelectedItem + "', bmodel = '" + Me.txtbmodel.Text + "',  byear  = '" + Me.txtbyear.Text + "', mmake  = '" + Me.cmbmake.SelectedItem + "', mmodel  = '" + Me.txtmmodel.Text + "', myear  = '" + Me.txtmyear.Text + "', equip  = '" + Me.txtequip.Text + "', notes  = '" + Me.txtcomments.Text + "', status = '" + Me.cmbstatus.SelectedItem.ToString + "',  colour  = '" + Me.txtcolour.Text + "', price  = '" + stringtomoney(Me.txtprice.Text).ToString + "' WHERE ordernumber = '" + ordernumber.ToString + "'"
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.ExecuteNonQuery()
            cmd.CommandText = "UPDATE bos set ordernumber = '" + ordernumber.ToString + "', control_number = '2' WHERE bos_number = '" + bos.ToString + "' AND store = '" + store + "'"
            cmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
        conn.Dispose()
        cancled = False

    End Sub
    Private Sub ordernow()
        Dim passthisdata As New orderinfo
        passthisdata.boatmake = Me.cmbbmake.SelectedItem
        passthisdata.boatmodel = Me.txtbmodel.Text
        passthisdata.boatyear = Me.txtbyear.Text
        passthisdata.bos = Val(Me.txtbos.Text)
        passthisdata.colour = Me.txtcolour.Text
        passthisdata.customername = Me.txtcustomer.Text
        passthisdata.equipment = Me.txtequip.Text
        passthisdata.motormake = Me.cmbmake.SelectedItem.ToString
        passthisdata.motormodel = Me.txtmmodel.Text
        passthisdata.notes = Me.txtcomments.Text
        passthisdata.price = stringtomoney(Me.txtprice.Text)
        passthisdata.salesman = Me.txtsalesman.Text
        passthisdata.store = Me.txtstore.Text
        Dim orderfrm As New frmaddboatsn

        orderfrm.persistant = persistant
        orderfrm.passedboat = passthisdata
        orderfrm.ShowDialog()
        Me.cmbstatus.SelectedItem = "Ordered"
        Me.updateorder()

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring

        Try
            cmd.CommandText = "SELECT max(control_number) from inventory"
            conn.Open()
            cmd.Connection = conn
            control_number = Val(cmd.ExecuteScalar)
            cmd.CommandText = "UPDATE bos set Control_Number = '" + control_number.ToString + "', ordernumber = '0', status = '3' WHERE BOS_number = '" + bos.ToString + "' AND Store = '" + store + "'"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "UPDATE inventory set Bos_Number = '" + bos.ToString + "', Bos_store = '" + store + "' WHERE control_number = '" + control_number.ToString + "'"
            cmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
        conn.Dispose()
        Me.Close()



    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        updateorder()
        Me.Close()

    End Sub

    Private Sub btnorder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnorder.Click
        ordernow()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Me.Close()
    End Sub

    Private Sub txtprice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprice.Leave
        Me.txtprice.Text = Format(stringtomoney(Me.txtprice.Text), "currency")
    End Sub

    Private Sub loadlists()

        Dim mydatatable As New DataTable
        Dim mydatarow As DataRowView

        Dim tempstring As String
        Dim numberoflines As Integer
        Dim temptable As New DataGridView
        Dim b As New BindingSource

        b.DataSource = persistant.tbl_location
        b.Filter = "Code = '" + Me.store + "'"
        temptable.DataSource = b
        mydatarow = b.Item(0)

        numberoflines = mydatarow.Item("Num_lines")
        For y As Integer = 0 To persistant.tbl_location.Columns.Count - 1
            If Val(mydatarow.Item(y)) = 1 Or Val(mydatarow.Item(y)) = True Then
                tempstring = persistant.tbl_location.Columns(y).ColumnName

                tempstring = Replace(tempstring, "_", " ")
                cmbbmake.Items.Add(tempstring)
            End If
        Next


        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_motorbrands, "") - 1)
            Me.cmbmake.Items.Add(persistant.getvalue(persistant.tbl_motorbrands, "Brand", "", x))
        Next
    End Sub
    Private Function stringtomoney(ByVal input As String) As Decimal
        Dim tempx As String
        tempx = Replace(input, "$", "")
        tempx = Replace(tempx, ",", "")
        stringtomoney = Val(tempx)
    End Function

    Private Sub frmOrderBoat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadlists()
        writescreen()
    End Sub

    Private Sub btnDenied_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDenied.Click
        Me.cmbstatus.SelectedItem = "Denied"
        Me.updateorder()
        Me.Close()
    End Sub

    Private Sub btnApproved_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApproved.Click
        Me.cmbstatus.SelectedItem = "Approved"
        Me.updateorder()
        Me.btnorder.Visible = True
        Me.btnApproved.Visible = False
        Me.btnDenied.Visible = False

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim bosfrm As New frmBOS
        bosfrm.persistant = persistant
        bosfrm.mybosstorecode = store
        bosfrm.mybosnumber = bos
        bosfrm.downloaddata()
        bosfrm.Show()
    End Sub
End Class