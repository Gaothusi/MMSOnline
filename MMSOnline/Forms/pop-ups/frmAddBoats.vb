Imports MySql.Data.MySqlClient
Imports System.Data
Imports MMSOnline.inventorydata
Imports MMSOnline.equipment


Public Class frmAddBoats
    Public persistant As PreFetch
    Public passedboat As New orderinfo
    Dim cantleave As Boolean

    Dim locations As DataColumn
    Dim equip As New equipment
    Private mydata As New DataTable()

    Public Sub cleartable()
        DataView.Rows.Clear()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim x As New inventorydata
        Dim myrow As New DataGridViewRow
        Dim mycol As New DataGridViewColumn
        Dim mycell As DataGridViewCell
        Dim writeok As Boolean
        Dim motormake As String = ""
        Dim boatmake As String = ""

        writeok = True
        x.persistant = persistant
        equip.persistant = persistant


        For y As Integer = 0 To (DataView.Rows.Count - 2)
            myrow = DataView.Rows.Item(y)
            For z As Integer = 0 To (DataView.Columns.Count - 1)
                mycol = DataView.Columns.Item(z)
                mycell = myrow.Cells.Item(z)
                If mycol.Name = "Location" Then x.Location = mycell.Value
                If mycol.Name = "Year" Then
                    Try
                        x.Boat_year = mycell.Value
                    Catch ex As Exception
                        MessageBox.Show("Your must enter a valid model year")
                        writeok = False
                    End Try
                End If
                If mycol.Name = "Make" Then
                    x.Boat_brand = mycell.Value
                    boatmake = mycell.Value

                End If

                If mycol.Name = "Model" Then
                    x.Boat_model = mycell.Value
                    If boatmake <> "" And mycell.Value <> "" Then equip.boat(boatmake, mycell.Value)
                End If

                If mycol.Name = "HIN" Then x.Boat_hin = mycell.Value
                If mycol.Name = "Unit" Then x.Boat_serial = mycell.Value
                If mycol.Name = "Color" Then x.Boat_color = mycell.Value
                If mycol.Name = "Equipment" Then
                    x.Equipment = mycell.Value
                    equip.check(mycell.Value)
                End If

                If mycol.Name = "Engine_Make" Then
                    x.Engine_make = mycell.Value
                    motormake = mycell.Value
                End If

                If mycol.Name = "Engine_Model" Then
                    x.engine_model = mycell.Value
                    If motormake <> "" And mycell.Value <> "" Then equip.motor(motormake, mycell.Value)
                End If

                If mycol.Name = "Engine_serial" Then x.engine_serial = mycell.Value
                If mycol.Name = "Engine_year" Then
                    Try
                        x.engine_year = mycell.Value
                    Catch ex As Exception
                        MessageBox.Show("Your must enter a valid motor model year")
                        writeok = False
                    End Try
                End If
                If mycol.Name = "Drive_Model" Then x.drive_model = mycell.Value
                If mycol.Name = "Drive_serial" Then x.drive_serial = mycell.Value
                If mycol.Name = "Tplate_serial" Then x.Tplate_serial = mycell.Value
                If mycol.Name = "Trailer_Make" Then x.Trailer_make = mycell.Value
                If mycol.Name = "Trailer_Model" Then x.trailer_model = mycell.Value
                If mycol.Name = "Trailer_Serial" Then x.trailer_serial = mycell.Value
                If mycol.Name = "Trailer_Year" Then
                    Try
                        x.trailer_year = mycell.Value
                    Catch ex As Exception
                        MessageBox.Show("Your must enter a valid trailer model year")
                        writeok = False
                    End Try
                End If

                If mycol.Name = "Trailer_color" Then x.trailer_color = mycell.Value
                If mycol.Name = "Price" Then
                    Try
                        x.price = mycell.Value
                    Catch ex As Exception
                        MessageBox.Show("Your must enter a valid price")
                        writeok = False
                    End Try
                End If

                If mycol.Name = "Discount" Then
                    Try
                        x.discount = mycell.Value
                    Catch ex As Exception
                        MessageBox.Show("Your must enter a valid discount")
                        writeok = False
                    End Try
                End If


                If mycol.Name = "Comments" Then x.comments = mycell.Value
                If mycol.Name = "Here" Then x.here = mycell.Value
                If mycol.Name = "Arrivaldate" Then
                    Try
                        x.est_arrival_date = mycell.Value
                    Catch ex As Exception
                        MessageBox.Show("Your must enter a valid date in the form Month/Day/Year")
                        writeok = False
                    End Try
                End If
                If mycol.Name = "Discount_reason" Then x.discount_reason = mycell.Value
                If mycol.Name = "Load_num" Then x.Load_Number = mycell.Value
            Next
            If writeok = True Then
                cantleave = False

                x.state = 1
                If x.WriteDB() = False Then writeok = False
            End If
            x.Clear()
        Next
        If writeok = True Then DataView.Rows.Clear()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        closew()
    End Sub
    Private Sub closew()
        If cantleave = True Then
            MessageBox.Show("You must add the request boat before you can exit")
        Else
            If DataView.Rows.Count > 1 Then
                If MessageBox.Show("Are you sure you want to exit without adding these boats to inventory?", "Exit", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Me.Close()
                End If
            Else
                Me.Close()
            End If
        End If

    End Sub

    Private Sub frmAddBoats_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If cantleave Then
            e.Cancel = True


        End If
    End Sub

    Private Sub frmAddBoats_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim mydatatable As New DataTable
        Dim mydatarow As DataRowView

        Dim tempstring As String
        Dim numberoflines As Integer
        Dim temptable As New DataGridView
        Dim b As New BindingSource

        b.DataSource = persistant.tbl_location
        b.Filter = "Store = '" + persistant.mystore + "'"
        temptable.DataSource = b
        mydatarow = b.Item(0)

        numberoflines = mydatarow.Item("Num_lines")
        For y As Integer = 0 To persistant.tbl_location.Columns.Count - 1
            If Val(mydatarow.Item(y)) = 1 Or Val(mydatarow.Item(y)) = True Then
                tempstring = persistant.tbl_location.Columns(y).ColumnName
                tempstring = Replace(tempstring, "_", " ")
                Me.Make.Items.Add(tempstring)
            End If
        Next


        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
            If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "ADM" Then Me.Location.Items.Add(persistant.getvalue(persistant.tbl_location, "Code", "", x))
        Next

        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_motorbrands, "") - 1)
            Me.Engine_Make.Items.Add(persistant.getvalue(persistant.tbl_motorbrands, "Brand", "", x))
        Next

        If passedboat.boatmake <> Nothing Then
            cantleave = True

            Dim myrow As New DataGridViewRow
            Dim mycol As New DataGridViewColumn
            Dim mycell As DataGridViewCell
            myrow = DataView.Rows.Item(0)
            For z As Integer = 0 To (DataView.Columns.Count - 1)
                mycol = DataView.Columns.Item(z)
                mycell = myrow.Cells.Item(z)
                If mycol.Name = "Location" Then mycell.Value = passedboat.store
                If mycol.Name = "Make" Then
                    For x As Integer = 0 To Make.Items.Count - 1
                        If Make.Items.Item(x).ToString.ToLower = passedboat.boatmake.ToLower Then mycell.Value = Make.Items.Item(x)
                    Next
                End If

                If mycol.Name = "Model" Then mycell.Value = passedboat.boatmodel
                If mycol.Name = "Year" Then mycell.Value = passedboat.boatyear.ToString
                If mycol.Name = "Color" Then mycell.Value = passedboat.colour
                If mycol.Name = "Equipment" Then mycell.Value = passedboat.equipment
                If mycol.Name = "Engine_Make" Then
                    For x As Integer = 0 To Engine_Make.Items.Count - 1
                        If Engine_Make.Items.Item(x).ToString.ToLower = passedboat.motormake.ToLower Then mycell.Value = Engine_Make.Items.Item(x)
                    Next
                End If

                If mycol.Name = "Engine_Model" Then mycell.Value = passedboat.motormodel
                If mycol.Name = "Price" Then mycell.Value = passedboat.price.ToString
                If mycol.Name = "Comments" Then mycell.Value = passedboat.notes + " - " + passedboat.customername + " [" + passedboat.store + " " + passedboat.bos.ToString + "] FOR " + passedboat.salesman
            Next
        End If

    End Sub


    Private Sub DataView_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DataView.RowsAdded
        DataView.Item(1, Me.DataView.Rows.Count - 1).Value = 2007
    End Sub

End Class
