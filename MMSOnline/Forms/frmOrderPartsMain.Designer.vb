﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrderPartsMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtPaymentType = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPartNum = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtBoatSerNum = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnStatusNone = New System.Windows.Forms.Button()
        Me.btnStatusAll = New System.Windows.Forms.Button()
        Me.btnStoreNone = New System.Windows.Forms.Button()
        Me.btnStoreAll = New System.Windows.Forms.Button()
        Me.txtPartsOrderNum = New System.Windows.Forms.TextBox()
        Me.lblPartsOrder = New System.Windows.Forms.Label()
        Me.txtWOnum = New System.Windows.Forms.TextBox()
        Me.txtname = New System.Windows.Forms.TextBox()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.listStore = New System.Windows.Forms.CheckedListBox()
        Me.listStatus = New System.Windows.Forms.CheckedListBox()
        Me.txtloading = New System.Windows.Forms.TextBox()
        Me.DataView = New System.Windows.Forms.DataGridView()
        Me.Edit = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cmbcustomers = New System.Windows.Forms.Label()
        Me.lblNewOrder = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DataView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPaymentType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label9)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPartNum)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtBoatSerNum)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtSupplier)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnStatusNone)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnStatusAll)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnStoreNone)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnStoreAll)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPartsOrderNum)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPartsOrder)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtWOnum)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmdSearch)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.listStore)
        Me.SplitContainer2.Panel1.Controls.Add(Me.listStatus)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtloading)
        Me.SplitContainer2.Panel2.Controls.Add(Me.DataView)
        Me.SplitContainer2.Size = New System.Drawing.Size(1382, 978)
        Me.SplitContainer2.SplitterDistance = 160
        Me.SplitContainer2.SplitterWidth = 1
        Me.SplitContainer2.TabIndex = 0
        '
        'txtPaymentType
        '
        Me.txtPaymentType.Location = New System.Drawing.Point(499, 98)
        Me.txtPaymentType.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtPaymentType.Name = "txtPaymentType"
        Me.txtPaymentType.Size = New System.Drawing.Size(213, 22)
        Me.txtPaymentType.TabIndex = 46
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(387, 102)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(103, 17)
        Me.Label9.TabIndex = 47
        Me.Label9.Text = "Payment Type:"
        '
        'txtPartNum
        '
        Me.txtPartNum.Location = New System.Drawing.Point(499, 66)
        Me.txtPartNum.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtPartNum.Name = "txtPartNum"
        Me.txtPartNum.Size = New System.Drawing.Size(213, 22)
        Me.txtPartNum.TabIndex = 44
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(399, 70)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(92, 17)
        Me.Label8.TabIndex = 45
        Me.Label8.Text = "Part Number:"
        '
        'txtBoatSerNum
        '
        Me.txtBoatSerNum.Location = New System.Drawing.Point(157, 123)
        Me.txtBoatSerNum.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtBoatSerNum.Name = "txtBoatSerNum"
        Me.txtBoatSerNum.Size = New System.Drawing.Size(213, 22)
        Me.txtBoatSerNum.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(52, 127)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 17)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "Boat Serial #:"
        '
        'txtSupplier
        '
        Me.txtSupplier.Location = New System.Drawing.Point(499, 38)
        Me.txtSupplier.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.Size = New System.Drawing.Size(213, 22)
        Me.txtSupplier.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(427, 42)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 17)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Supplier:"
        '
        'btnStatusNone
        '
        Me.btnStatusNone.Location = New System.Drawing.Point(1071, 4)
        Me.btnStatusNone.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnStatusNone.Name = "btnStatusNone"
        Me.btnStatusNone.Size = New System.Drawing.Size(64, 28)
        Me.btnStatusNone.TabIndex = 39
        Me.btnStatusNone.Text = "None"
        Me.btnStatusNone.UseVisualStyleBackColor = True
        '
        'btnStatusAll
        '
        Me.btnStatusAll.Location = New System.Drawing.Point(999, 4)
        Me.btnStatusAll.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnStatusAll.Name = "btnStatusAll"
        Me.btnStatusAll.Size = New System.Drawing.Size(64, 28)
        Me.btnStatusAll.TabIndex = 38
        Me.btnStatusAll.Text = "All"
        Me.btnStatusAll.UseVisualStyleBackColor = True
        '
        'btnStoreNone
        '
        Me.btnStoreNone.Location = New System.Drawing.Point(869, 4)
        Me.btnStoreNone.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnStoreNone.Name = "btnStoreNone"
        Me.btnStoreNone.Size = New System.Drawing.Size(64, 28)
        Me.btnStoreNone.TabIndex = 37
        Me.btnStoreNone.Text = "None"
        Me.btnStoreNone.UseVisualStyleBackColor = True
        '
        'btnStoreAll
        '
        Me.btnStoreAll.Location = New System.Drawing.Point(797, 4)
        Me.btnStoreAll.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnStoreAll.Name = "btnStoreAll"
        Me.btnStoreAll.Size = New System.Drawing.Size(64, 28)
        Me.btnStoreAll.TabIndex = 36
        Me.btnStoreAll.Text = "All"
        Me.btnStoreAll.UseVisualStyleBackColor = True
        '
        'txtPartsOrderNum
        '
        Me.txtPartsOrderNum.Location = New System.Drawing.Point(157, 95)
        Me.txtPartsOrderNum.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtPartsOrderNum.Name = "txtPartsOrderNum"
        Me.txtPartsOrderNum.Size = New System.Drawing.Size(213, 22)
        Me.txtPartsOrderNum.TabIndex = 2
        '
        'lblPartsOrder
        '
        Me.lblPartsOrder.AutoSize = True
        Me.lblPartsOrder.Location = New System.Drawing.Point(52, 98)
        Me.lblPartsOrder.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPartsOrder.Name = "lblPartsOrder"
        Me.lblPartsOrder.Size = New System.Drawing.Size(98, 17)
        Me.lblPartsOrder.TabIndex = 13
        Me.lblPartsOrder.Text = "Parts Order #:"
        '
        'txtWOnum
        '
        Me.txtWOnum.Location = New System.Drawing.Point(157, 66)
        Me.txtWOnum.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtWOnum.Name = "txtWOnum"
        Me.txtWOnum.Size = New System.Drawing.Size(213, 22)
        Me.txtWOnum.TabIndex = 1
        '
        'txtname
        '
        Me.txtname.Location = New System.Drawing.Point(157, 38)
        Me.txtname.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(213, 22)
        Me.txtname.TabIndex = 0
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(613, 130)
        Me.cmdSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(100, 28)
        Me.cmdSearch.TabIndex = 7
        Me.cmdSearch.Text = "Search"
        Me.cmdSearch.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(935, 10)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 17)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Status"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(740, 10)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 17)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Store"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(67, 10)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(195, 31)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Search Criteria"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(49, 70)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 17)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Work Order #:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 42)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 17)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Customer Name:"
        '
        'listStore
        '
        Me.listStore.CheckOnClick = True
        Me.listStore.FormattingEnabled = True
        Me.listStore.Location = New System.Drawing.Point(747, 36)
        Me.listStore.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.listStore.Name = "listStore"
        Me.listStore.Size = New System.Drawing.Size(185, 140)
        Me.listStore.TabIndex = 5
        '
        'listStatus
        '
        Me.listStatus.CheckOnClick = True
        Me.listStatus.FormattingEnabled = True
        Me.listStatus.Items.AddRange(New Object() {"To Be Ordered", "Ordered", "Back Ordered", "Received", "Picked Up", "Placed In Stock", "Installed", "Void", "Quote"})
        Me.listStatus.Location = New System.Drawing.Point(941, 36)
        Me.listStatus.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.listStatus.Name = "listStatus"
        Me.listStatus.Size = New System.Drawing.Size(192, 140)
        Me.listStatus.TabIndex = 6
        '
        'txtloading
        '
        Me.txtloading.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtloading.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloading.Location = New System.Drawing.Point(0, 0)
        Me.txtloading.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtloading.Name = "txtloading"
        Me.txtloading.ReadOnly = True
        Me.txtloading.Size = New System.Drawing.Size(1382, 34)
        Me.txtloading.TabIndex = 1
        Me.txtloading.Text = "Downloading Data"
        Me.txtloading.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtloading.Visible = False
        '
        'DataView
        '
        Me.DataView.AllowUserToAddRows = False
        Me.DataView.AllowUserToDeleteRows = False
        Me.DataView.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray
        Me.DataView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Edit})
        Me.DataView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataView.GridColor = System.Drawing.SystemColors.ControlLightLight
        Me.DataView.Location = New System.Drawing.Point(0, 0)
        Me.DataView.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataView.Name = "DataView"
        Me.DataView.ReadOnly = True
        Me.DataView.RowHeadersVisible = False
        Me.DataView.Size = New System.Drawing.Size(1382, 817)
        Me.DataView.TabIndex = 0
        '
        'Edit
        '
        Me.Edit.HeaderText = "Details"
        Me.Edit.Name = "Edit"
        Me.Edit.ReadOnly = True
        Me.Edit.Text = "Edit"
        Me.Edit.UseColumnTextForButtonValue = True
        Me.Edit.Width = 57
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImage = Global.MMSOnline.My.Resources.Resources.MMSOnline
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(0, -7)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(215, 62)
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'cmbcustomers
        '
        Me.cmbcustomers.AutoSize = True
        Me.cmbcustomers.BackColor = System.Drawing.Color.Transparent
        Me.cmbcustomers.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcustomers.ForeColor = System.Drawing.Color.MidnightBlue
        Me.cmbcustomers.Location = New System.Drawing.Point(16, 100)
        Me.cmbcustomers.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.cmbcustomers.Name = "cmbcustomers"
        Me.cmbcustomers.Size = New System.Drawing.Size(155, 19)
        Me.cmbcustomers.TabIndex = 14
        Me.cmbcustomers.Text = "Search Customers"
        Me.cmbcustomers.Visible = False
        '
        'lblNewOrder
        '
        Me.lblNewOrder.AutoSize = True
        Me.lblNewOrder.BackColor = System.Drawing.Color.Transparent
        Me.lblNewOrder.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewOrder.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblNewOrder.Location = New System.Drawing.Point(16, 69)
        Me.lblNewOrder.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNewOrder.Name = "lblNewOrder"
        Me.lblNewOrder.Size = New System.Drawing.Size(151, 19)
        Me.lblNewOrder.TabIndex = 15
        Me.lblNewOrder.Text = "Create New Order"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Window
        Me.SplitContainer1.Panel1.BackgroundImage = Global.MMSOnline.My.Resources.Resources.BkgTL
        Me.SplitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblNewOrder)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbcustomers)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PictureBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1543, 978)
        Me.SplitContainer1.SplitterDistance = 160
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 2
        Me.SplitContainer1.TabStop = False
        '
        'frmOrderPartsMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1543, 978)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmOrderPartsMain"
        Me.Text = "MMSonline"
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DataView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnStatusNone As System.Windows.Forms.Button
    Friend WithEvents btnStatusAll As System.Windows.Forms.Button
    Friend WithEvents btnStoreNone As System.Windows.Forms.Button
    Friend WithEvents btnStoreAll As System.Windows.Forms.Button
    Friend WithEvents txtPartsOrderNum As System.Windows.Forms.TextBox
    Friend WithEvents lblPartsOrder As System.Windows.Forms.Label
    Friend WithEvents txtWOnum As System.Windows.Forms.TextBox
    Friend WithEvents txtname As System.Windows.Forms.TextBox
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents listStore As System.Windows.Forms.CheckedListBox
    Friend WithEvents listStatus As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtloading As System.Windows.Forms.TextBox
    Friend WithEvents DataView As System.Windows.Forms.DataGridView
    Friend WithEvents Edit As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmbcustomers As System.Windows.Forms.Label
    Friend WithEvents lblNewOrder As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtSupplier As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBoatSerNum As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPartNum As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPaymentType As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
