Imports MySql.Data.MySqlClient
Imports System.Data
Imports MMSOnline.inventorydata
Imports MMSOnline.equipment

Public Class frmeditinv
    Dim invdata As New inventorydata
    Public persistant As PreFetch
    Public unlockserials As Boolean

    Dim mycontrolnumber As Integer
    Dim statustable As New DataTable
    Dim canclose As Boolean
    Dim equip As New equipment
    Dim iamselectingboat As Boolean
    Public bos As frmBOS
    Public invform As frmInv



    Public WriteOnly Property selectingboat() As Boolean
        Set(ByVal value As Boolean)
            iamselectingboat = value
        End Set
    End Property
 
    Public WriteOnly Property controlnumber() As Integer
        Set(ByVal value As Integer)
            mycontrolnumber = value
        End Set
    End Property


    Private Sub frmeditinv_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        statustable = persistant.tbl_status
        Me.CancelButton = CmdClose
        invdata.persistant = persistant

        invdata.controlnumber = mycontrolnumber
        equip.persistant = persistant
        buildlists()

        If persistant.myuserLEVEL < 8 Then
            setup4sales()
        End If

        txtControl_number.Text = invdata.Control_number
        invdata.readDB()
        displaydata()
        Me.Text = invdata.Boat_year.ToString + " " + invdata.Boat_brand + " " + invdata.Boat_model + "  -  " + invdata.Boat_hin

        Cmdsave.Enabled = False
        canclose = False
        If iamselectingboat = True Then
            setupforselecting()
        End If

        If Val(invdata.bos_num) > 0 And invdata.bos_store <> "" Then
            Me.btnviewdeal.Visible = True
        End If
        If unlockserials = True Then
            Cmdsave.Visible = True
        End If

    End Sub
    Private Sub frmeditinv_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'e.Cancel = Not canclose
        'Me.Close()
    End Sub

    'Buttons

    Private Sub Cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmdsave.Click
        If txtmotormake.Text <> "" And txtmotormodel.Text <> "" Then
            equip.motor(Me.txtmotormake.Text, Me.txtmotormodel.Text)
        End If
        If txtboatmake.Text <> "" And txtboatmodel.Text <> "" Then
            equip.boat(Me.txtboatmake.Text, Me.txtboatmodel.Text)
        End If
        If Me.txtboatmake.Text = "" Or Me.txtboatmodel.Text = "" Or Me.txtboatyear.Text = "" Then
            MessageBox.Show("You have left a manditory feild blank.")
            GoTo z
        End If
        invdata.updateDB()
        Cmdsave.Enabled = False
z:

    End Sub
    Private Sub cmdReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        invdata.readDB()
        displaydata()
    End Sub
    Private Sub CmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdClose.Click
        If Cmdsave.Enabled = True And persistant.myuserLEVEL > 8 Then
            If MessageBox.Show("Are you sure you want to exit without saving your changes?", "Exit", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                'canclose = True
                Me.Close()
            End If
        Else
            'canclose = True
            Me.Close()
        End If

    End Sub
    'Subs

    Private Sub buildlists()

        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
            If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "Adm" Then cmbLocation.Items.Add(persistant.getvalue(persistant.tbl_location, "Code", "", x))
        Next

        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_status, "") - 1)
            cmbStatus.Items.Add(persistant.getvalue(persistant.tbl_status, "State", "", x))
        Next

    End Sub
    Private Sub setup4sales()
        Me.TabControl1.TabPages.Remove(Me.TabPage3)
        Me.TabControl1.TabPages.Remove(Me.TabPage4)
        chkhere.Enabled = False
        chkboatreg.Enabled = False
        chkfree_interest.Enabled = False
        chkMaturitydate.Enabled = False
        chkmotorreg.Enabled = False
        chknothere.Enabled = False
        cmbLocation.Enabled = False
        cmbstatus.enabled = False
        cmbFinancedCompany.Enabled = False
        datearrival.Enabled = False
        dateboatreg.Enabled = False
        datefreeinterest.Enabled = False
        dateGEpaid.Enabled = False
        dateInvoice.Enabled = False
        datematurity.Enabled = False
        datemotorreg.Enabled = False
        listequip.Enabled = False
        txtboat_color.ReadOnly = True
        If Me.unlockserials = False Then
            txtboathin.ReadOnly = True
            txtboatmake.ReadOnly = True
            txtboatmodel.ReadOnly = True
            txtboatunit.ReadOnly = True
            txtboatyear.ReadOnly = True
            txtdrivemodel.ReadOnly = True
            txtdriveserial.ReadOnly = True
            txtmotormake.ReadOnly = True
            txtmotormodel.ReadOnly = True
            txtmotorserial.ReadOnly = True
            txtmotoryear.ReadOnly = True
            txttrailermake.ReadOnly = True
            txttrailermodel.ReadOnly = True
            txttrailerserial.ReadOnly = True
            txttraileryear.ReadOnly = True
            txtdrivemake.ReadOnly = True
            txtdriveyear.ReadOnly = True
            txttplatemake.ReadOnly = True
            txttplatemodel.ReadOnly = True
            txttplateyear.ReadOnly = True
            txttplateserial.ReadOnly = True
        End If
     
        txtcomments.ReadOnly = False
        txtControl_number.ReadOnly = True
        txtDiscount.ReadOnly = True
        txtdiscount_reason.ReadOnly = True
        txtequipment.ReadOnly = True
        txtPrice.ReadOnly = True
        txttrailer_color.ReadOnly = True
        txtremaining_CDN_Cost.ReadOnly = True
        txtboat_us_cost.ReadOnly = True
        txtCDNcost.ReadOnly = True
        txtEST_CDN_Cost.ReadOnly = True
        txtfrt_us_cost.ReadOnly = True
        txtGE_AMT_financed.ReadOnly = True
        txtGE_paid.ReadOnly = True
        TXTGE_X_Rate.ReadOnly = True
        txtGEowing.ReadOnly = True
        TxtInv_Moved_to_COGS.ReadOnly = True
        txtInvoicenumber.ReadOnly = True
        txtLoadNumber.ReadOnly = True
        txtnotes.ReadOnly = True
        txtPLA.ReadOnly = True
        txtRebate.ReadOnly = True
        txttotal_us_cost.ReadOnly = True
        txttrl_us_cost.ReadOnly = True
      
        cmdReset.Visible = True
        Cmdsave.Visible = True

    End Sub
    Private Sub setupforselecting()
        canclose = True
        cmdselect.Visible = True
        Cmdsave.Visible = False
        cmdReset.Visible = False

    End Sub
    Private Sub displaydata()
        Dim list As New List(Of String)

        If invdata.Location <> "" Then
            cmbLocation.SelectedItem = invdata.Location
        End If
        For x As Integer = 0 To statustable.Rows.Count - 1
            If invdata.state = statustable.Rows(x).Item(0) Then
                cmbStatus.SelectedIndex = x

            End If
        Next

        txtboat_color.Text = invdata.Boat_color
        txtboathin.Text = invdata.Boat_hin
        txtboatmake.Text = invdata.Boat_brand
        txtboatmodel.Text = invdata.Boat_model
        txtboatunit.Text = invdata.Boat_serial
        txtboatyear.Text = invdata.Boat_year
        txtcomments.Text = invdata.comments
        txtDiscount.Text = Format(invdata.discount, "currency")
        txtdiscount_reason.Text = invdata.discount_reason
        txtdrivemodel.Text = invdata.drive_model
        txtdriveserial.Text = invdata.drive_serial
        txtequipment.Text = invdata.Equipment
        txtmotormake.Text = invdata.Engine_make
        txtmotormodel.Text = invdata.engine_model
        txtmotorserial.Text = invdata.engine_serial
        txtmotoryear.Text = invdata.engine_year
        txtPrice.Text = Format(invdata.price, "currency")
        txttplateserial.Text = invdata.Tplate_serial
        txttrailer_color.Text = invdata.trailer_color
        txttrailermake.Text = invdata.Trailer_make
        txttrailermodel.Text = invdata.trailer_model
        txttrailerserial.Text = invdata.trailer_serial
        txttraileryear.Text = invdata.trailer_year
        txtremaining_CDN_Cost.Text = Format(invdata.Remaining_CDN_cost_inv, "currency")
        txtboat_us_cost.Text = Format(invdata.Boat_US_cost, "currency")
        txtCDNcost.Text = Format(invdata.Canadian_cost, "currency")
        txtEST_CDN_Cost.Text = Format(invdata.EST_Can_Cost, "currency")
        txtfrt_us_cost.Text = Format(invdata.FRT_US_cost, "currency")
        txtGE_AMT_financed.Text = Format(invdata.GE_Ammount_Financed, "currency")
        txtGE_paid.Text = Format(invdata.GE_Amt_Paid, "currency")
        TXTGE_X_Rate.Text = invdata.GE_X_Rate
        txtGEowing.Text = Format(invdata.GE_amt_Owing, "currency")
        TxtInv_Moved_to_COGS.Text = Format(invdata.Inv_Move_to_Cost_of_Goods_sold, "currency")
        txtInvoicenumber.Text = invdata.Invoice_Number
        txtLoadNumber.Text = invdata.Load_Number
        txtnotes.Text = invdata.Accounting_Notes
        txtPLA.Text = invdata.PLA_cost_or_Cheque
        txtRebate.Text = Format(invdata.Rebate, "currency")
        txttotal_us_cost.Text = Format(invdata.TOTAL_US_cost, "currency")
        txttrl_us_cost.Text = Format(invdata.TRL_US_cost, "currency")
        If invdata.Tplate_serial <> "" Then
            Me.txttplatemake.Text = invdata.Engine_make
            Me.txttplatemodel.Text = "T - Plate"
            Me.txttplateyear.Text = invdata.engine_year
        End If
        If invdata.drive_serial <> "" Or invdata.drive_model <> "" Then
            Me.txtdrivemake.Text = invdata.Engine_make
            Me.txtdriveyear.Text = invdata.engine_year
        End If
        If invdata.here = True Then
            Me.datearrival.Visible = False
            Me.chkhere.Checked = True
            Me.chknothere.Checked = False
            Me.Label10.Visible = False
            Me.datearrival.Visible = False

        Else
            Me.Label10.Visible = True
            Me.chkhere.Checked = False
            Me.chknothere.Checked = True
            Me.datearrival.Visible = True

            If invdata.est_arrival_date <> CDate("1/1/0001 12:00:00 AM") Then datearrival.Value = invdata.est_arrival_date
        End If
        If invdata.Invoice_Number <> "" Then
            Me.dateInvoice.Visible = True
            Me.Label41.Visible = True
            If invdata.Invoice_Date <> CDate("1/1/0001 12:00:00 AM") Then dateInvoice.Value = invdata.Invoice_Date
        Else
            Me.dateInvoice.Visible = False
            Me.Label41.Visible = False
        End If
        If invdata.GE_Amt_Paid <> 0 Then
            Me.dateGEpaid.Visible = True
            Me.Label44.Visible = True
            Me.txtPLA.Visible = True
            Me.Label43.Visible = True
            If invdata.GE_paid_date <> CDate("1/1/0001 12:00:00 AM") Then dateGEpaid.Value = invdata.GE_paid_date
        Else
            Me.dateGEpaid.Visible = False
            Me.Label44.Visible = False
            Me.txtPLA.Visible = False
            Me.Label43.Visible = False

        End If
        If invdata.Motor_Reg_Date <> CDate("1/1/0001 12:00:00 AM") Then
            chkmotorreg.Checked = True
            chkmotorreg.Enabled = False
            datemotorreg.Value = invdata.Motor_Reg_Date
            Me.Label32.Visible = True
            Me.datemotorreg.Visible = True
        Else
            chkmotorreg.Checked = False
            Me.Label32.Visible = False
            Me.datemotorreg.Visible = False
        End If

        If invdata.Boat_Reg_date <> CDate("1/1/0001 12:00:00 AM") Then
            chkboatreg.Checked = True
            chkboatreg.Enabled = False
            dateboatreg.Value = invdata.Boat_Reg_date
            Me.Label33.Visible = True
            Me.dateboatreg.Visible = True
        Else
            chkboatreg.Checked = False
            Me.Label33.Visible = False
            Me.dateboatreg.Visible = False
        End If
        If invdata.Free_lnterest_Until <> CDate("1/1/0001 12:00:00 AM") Then
            datefreeinterest.Value = invdata.Free_lnterest_Until
            datefreeinterest.Visible = True
        Else
            datefreeinterest.Visible = False
        End If

        If invdata.Maturity_Date <> CDate("1/1/0001 12:00:00 AM") Then
            datematurity.Value = invdata.Maturity_Date
            datematurity.Visible = True
        Else
            datematurity.Visible = False
        End If
        list.Clear()
        equip.short2long(Me.txtequipment.Text, list)
        Me.listequip.Items.Clear()
        Me.listequip.Items.AddRange(list.ToArray)
    End Sub
    Private Sub stringchanged(ByRef sender As Object, ByRef data_location As Object)
        data_location = sender.text
        If sender.text = "" Then data_location = " "
        Cmdsave.Enabled = True
    End Sub
    Private Sub moneychanged(ByRef sender As Object, ByRef data_location As Object)
        Dim x As String
        Dim y As Decimal
        x = Replace(sender.text, "$", "")
        x = Replace(x, ",", "")
        y = Val(x)
        If y <> 0 Then
            data_location = y
            sender.text = Format(y, "currency")
        End If

        If y = 0 Then
            data_location = 0
            sender.text = "$0.00"
        End If
        Cmdsave.Enabled = True

    End Sub
    Private Sub updatemath()
        invdata.TOTAL_US_cost = invdata.Boat_US_cost + invdata.TRL_US_cost + invdata.FRT_US_cost
        invdata.EST_Can_Cost = invdata.TOTAL_US_cost * invdata.GE_X_Rate
        invdata.Remaining_CDN_cost_inv = invdata.Canadian_cost - invdata.Inv_Move_to_Cost_of_Goods_sold
        invdata.GE_amt_Owing = invdata.GE_Ammount_Financed - invdata.GE_Amt_Paid
        Me.txttotal_us_cost.Text = Format(invdata.TOTAL_US_cost, "currency")
        Me.txtEST_CDN_Cost.Text = Format(invdata.EST_Can_Cost, "currency")
        Me.txtremaining_CDN_Cost.Text = Format(invdata.Remaining_CDN_cost_inv, "currency")
        Me.txtGEowing.Text = Format(invdata.GE_amt_Owing, "currency")
    End Sub

    'Year changed

    Private Sub txtboatyear_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtboatyear.Leave
        invdata.Boat_year = Int(Val(Me.txtboatyear.Text))
        Cmdsave.Enabled = True
        displaydata()

    End Sub
    Private Sub txtmotoryear_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmotoryear.Leave
        invdata.engine_year = Int(Val(Me.txtmotoryear.Text))
        Cmdsave.Enabled = True
        displaydata()
    End Sub
    Private Sub txttraileryear_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttraileryear.Leave
        invdata.trailer_year = Int(Val(Me.txttraileryear.Text))
        Cmdsave.Enabled = True
        displaydata()
    End Sub
    'String changed
    Private Sub txtboat_color_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtboat_color.Leave
        stringchanged(txtboat_color, invdata.Boat_color)
    End Sub
    Private Sub txtboathin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtboathin.Leave
        stringchanged(txtboathin, invdata.Boat_hin)
    End Sub
    Private Sub txtboatmake_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtboatmake.Leave
        stringchanged(txtboatmake, invdata.Boat_brand)
    End Sub
    Private Sub txtboatmodel_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtboatmodel.Leave
        stringchanged(txtboatmodel, invdata.Boat_model)
    End Sub
    Private Sub txtboatunit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtboatunit.Leave
        stringchanged(txtboatunit, invdata.Boat_serial)
    End Sub
    Private Sub txtcomments_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcomments.TextChanged
        stringchanged(txtcomments, invdata.comments)
    End Sub
    Private Sub txtdiscount_reason_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdiscount_reason.Leave
        stringchanged(txtdiscount_reason, invdata.discount_reason)
    End Sub
    Private Sub txtdrivemodel_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdrivemodel.Leave
        stringchanged(txtdrivemodel, invdata.drive_model)
        displaydata()
    End Sub
    Private Sub txtdriveserial_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdriveserial.Leave
        stringchanged(txtdriveserial, invdata.drive_serial)
        displaydata()
    End Sub
    Private Sub txtequipment_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtequipment.Leave
        equip.check(txtequipment.Text)
        stringchanged(txtequipment, invdata.Equipment)
        displaydata()
    End Sub
    Private Sub txtInvoicenumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInvoicenumber.Leave
        stringchanged(txtInvoicenumber, invdata.Invoice_Number)
        displaydata()
    End Sub
    Private Sub txtLoadNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLoadNumber.Leave
        stringchanged(txtLoadNumber, invdata.Load_Number)
    End Sub
    Private Sub txtmotormake_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmotormake.Leave
        stringchanged(txtmotormake, invdata.Engine_make)
    End Sub
    Private Sub txtmotormodel_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmotormodel.Leave
        stringchanged(txtmotormodel, invdata.engine_model)
    End Sub
    Private Sub txtmotorserial_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmotorserial.Leave
        stringchanged(txtmotorserial, invdata.engine_serial)
    End Sub
    Private Sub txtnotes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtnotes.Leave
        stringchanged(txtnotes, invdata.Accounting_Notes)
    End Sub
    Private Sub txtPLA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPLA.Leave
        stringchanged(txtPLA, invdata.PLA_cost_or_Cheque)
    End Sub
    Private Sub txttplateserial_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttplateserial.Leave
        stringchanged(txttplateserial, invdata.Tplate_serial)
        displaydata()
    End Sub
    Private Sub txttrailer_color_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttrailer_color.Leave
        stringchanged(txttrailer_color, invdata.trailer_color)
    End Sub
    Private Sub txttrailermake_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttrailermake.Leave
        stringchanged(txttrailermake, invdata.Trailer_make)
    End Sub
    Private Sub txttrailermodel_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttrailermodel.Leave
        stringchanged(txttrailermodel, invdata.trailer_model)
    End Sub
    Private Sub txttrailerserial_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttrailerserial.Leave
        stringchanged(txttrailerserial, invdata.trailer_serial)
    End Sub
    'Money changed
    Private Sub txtCDNcost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCDNcost.Leave
        moneychanged(txtCDNcost, invdata.Canadian_cost)
    End Sub
    Private Sub txtDiscount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiscount.Leave
        moneychanged(txtDiscount, invdata.discount)
    End Sub
    Private Sub txtfrt_us_cost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfrt_us_cost.Leave
        moneychanged(txtfrt_us_cost, invdata.FRT_US_cost)
        updatemath()
    End Sub
    Private Sub txtGE_AMT_financed_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGE_AMT_financed.Leave
        moneychanged(txtGE_AMT_financed, invdata.GE_Ammount_Financed)
    End Sub
    Private Sub txtGE_paid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGE_paid.Leave
        moneychanged(txtGE_paid, invdata.GE_Amt_Paid)
        updatemath()
        displaydata()

    End Sub
    Private Sub TXTGE_X_Rate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTGE_X_Rate.Leave
        stringchanged(TXTGE_X_Rate, invdata.GE_X_Rate)
        updatemath()
    End Sub
    Private Sub TxtInv_Moved_to_COGS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtInv_Moved_to_COGS.Leave
        moneychanged(TxtInv_Moved_to_COGS, invdata.Inv_Move_to_Cost_of_Goods_sold)
        updatemath()
    End Sub
    Private Sub txtPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrice.Leave
        moneychanged(txtPrice, invdata.price)
    End Sub
    Private Sub txtRebate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRebate.Leave
        moneychanged(txtRebate, invdata.Rebate)
    End Sub
    Private Sub txttrl_us_cost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttrl_us_cost.Leave
        moneychanged(Me.txttrl_us_cost, invdata.TRL_US_cost)
        updatemath()
    End Sub
    Private Sub txtboat_us_cost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtboat_us_cost.Leave
        moneychanged(Me.txtboat_us_cost, invdata.Boat_US_cost)
        updatemath()
    End Sub

    'comboboxs
    Private Sub cmbLocation_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLocation.Leave
        invdata.Location = cmbLocation.SelectedItem
        Cmdsave.Enabled = True

    End Sub
    Private Sub cmbStatus_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedValueChanged
        For x As Integer = 0 To statustable.Rows.Count - 1
            If cmbStatus.SelectedItem = statustable.Rows(x).Item(1) Then
                invdata.state = statustable.Rows(x).Item(0)

            End If
        Next
        Cmdsave.Enabled = True

    End Sub
    'checkboxs

    Private Sub chkhere_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkhere.CheckedChanged
        If chkhere.Checked = True Then
            invdata.here = True
        Else
            invdata.here = False
        End If
        Me.Cmdsave.Enabled = True
        displaydata()
    End Sub
    Private Sub chknothere_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chknothere.CheckedChanged
        If chknothere.Checked = True Then
            invdata.here = False
        Else
            invdata.here = True
        End If
        Me.Cmdsave.Enabled = True
        displaydata()
    End Sub
    Private Sub chkboatreg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkboatreg.CheckedChanged
        Me.Label33.Visible = True
        Me.dateboatreg.Visible = True
    End Sub
    Private Sub chkmotorreg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkmotorreg.CheckedChanged
        Me.Label32.Visible = True
        Me.datemotorreg.Visible = True
    End Sub
    Private Sub chkfree_interest_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkfree_interest.CheckedChanged
        If chkfree_interest.Checked = True Then
            Me.datefreeinterest.Visible = True
        Else
            Me.datefreeinterest.Visible = False
            invdata.Free_lnterest_Until = CDate("1/1/0001 12:00:00 AM")
        End If

    End Sub
    Private Sub chkMaturitydate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMaturitydate.CheckedChanged
        If Me.chkMaturitydate.Checked = True Then
            Me.datematurity.Visible = True
        Else
            Me.datematurity.Visible = False
            invdata.Maturity_Date = CDate("1/1/0001 12:00:00 AM")
        End If

    End Sub

    'Dates

    Private Sub datearrival_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles datearrival.ValueChanged
        invdata.est_arrival_date = datearrival.Value
        Me.Cmdsave.Enabled = True
    End Sub
    Private Sub dateInvoice_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dateInvoice.ValueChanged
        invdata.Invoice_Date = dateInvoice.Value
        Me.Cmdsave.Enabled = True
    End Sub
    Private Sub datefreeinterest_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles datefreeinterest.ValueChanged
        invdata.Free_lnterest_Until = datefreeinterest.Value
        Me.Cmdsave.Enabled = True
    End Sub
    Private Sub datematurity_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles datematurity.ValueChanged
        invdata.Maturity_Date = datematurity.Value
        Me.Cmdsave.Enabled = True
    End Sub
    Private Sub dateGEpaid_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dateGEpaid.ValueChanged
        invdata.GE_paid_date = Me.dateGEpaid.Value
        Me.Cmdsave.Enabled = True
    End Sub
    Private Sub dateboatreg_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dateboatreg.ValueChanged
        invdata.Boat_Reg_date = Me.dateboatreg.Value
        Me.Cmdsave.Enabled = True
    End Sub
    Private Sub datemotorreg_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles datemotorreg.ValueChanged
        invdata.Motor_Reg_Date = Me.datemotorreg.Value
        Me.Cmdsave.Enabled = True
    End Sub

    Private Sub cmdselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdselect.Click
        bos.selectboat(mycontrolnumber)
        invform.Close()
        Me.Close()
    End Sub


    Private Sub btnviewdeal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnviewdeal.Click
        Dim bos As New frmBOS
        bos.persistant = persistant
        bos.mybosstorecode = invdata.bos_store
        bos.mybosnumber = invdata.bos_num
        bos.downloaddata()
        bos.Show()
    End Sub
End Class

