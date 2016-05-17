<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServiceMain
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnScheduler = New System.Windows.Forms.Label()
        Me.btnWarrantyClaims = New System.Windows.Forms.Label()
        Me.btnOrderParts = New System.Windows.Forms.Label()
        Me.btnDeliveryList = New System.Windows.Forms.Label()
        Me.btnCompleted = New System.Windows.Forms.Label()
        Me.ShowHideMain = New System.Windows.Forms.Label()
        Me.btnPrioritizeWork = New System.Windows.Forms.Label()
        Me.btnSearchIssues = New System.Windows.Forms.Label()
        Me.btnNewWO = New System.Windows.Forms.Label()
        Me.btnSearchCustomers = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.BtnWFPU = New System.Windows.Forms.Button()
        Me.BtnRigs = New System.Windows.Forms.Button()
        Me.BtnActive = New System.Windows.Forms.Button()
        Me.BtnClear = New System.Windows.Forms.Button()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.cmdGo = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.listStore = New System.Windows.Forms.CheckedListBox()
        Me.txtWOnum = New System.Windows.Forms.TextBox()
        Me.txtname = New System.Windows.Forms.TextBox()
        Me.listStatus = New System.Windows.Forms.CheckedListBox()
        Me.txtloading = New System.Windows.Forms.TextBox()
        Me.DataView = New System.Windows.Forms.DataGridView()
        Me.Edit = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DataView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Window
        Me.SplitContainer1.Panel1.BackgroundImage = Global.MMSOnline.My.Resources.Resources.BkgTL
        Me.SplitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnScheduler)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnWarrantyClaims)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnOrderParts)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnDeliveryList)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnCompleted)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ShowHideMain)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnPrioritizeWork)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnSearchIssues)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNewWO)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnSearchCustomers)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PictureBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1076, 721)
        Me.SplitContainer1.SplitterDistance = 160
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 1
        Me.SplitContainer1.TabStop = False
        '
        'btnScheduler
        '
        Me.btnScheduler.AutoSize = True
        Me.btnScheduler.BackColor = System.Drawing.Color.Transparent
        Me.btnScheduler.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnScheduler.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnScheduler.Location = New System.Drawing.Point(12, 256)
        Me.btnScheduler.Name = "btnScheduler"
        Me.btnScheduler.Size = New System.Drawing.Size(73, 16)
        Me.btnScheduler.TabIndex = 27
        Me.btnScheduler.Text = "Scheduler"
        '
        'btnWarrantyClaims
        '
        Me.btnWarrantyClaims.AutoSize = True
        Me.btnWarrantyClaims.BackColor = System.Drawing.Color.Transparent
        Me.btnWarrantyClaims.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWarrantyClaims.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnWarrantyClaims.Location = New System.Drawing.Point(12, 231)
        Me.btnWarrantyClaims.Name = "btnWarrantyClaims"
        Me.btnWarrantyClaims.Size = New System.Drawing.Size(113, 16)
        Me.btnWarrantyClaims.TabIndex = 26
        Me.btnWarrantyClaims.Text = "Warranty Claims"
        Me.btnWarrantyClaims.Visible = False
        '
        'btnOrderParts
        '
        Me.btnOrderParts.AutoSize = True
        Me.btnOrderParts.BackColor = System.Drawing.Color.Transparent
        Me.btnOrderParts.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOrderParts.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnOrderParts.Location = New System.Drawing.Point(12, 206)
        Me.btnOrderParts.Name = "btnOrderParts"
        Me.btnOrderParts.Size = New System.Drawing.Size(86, 16)
        Me.btnOrderParts.TabIndex = 25
        Me.btnOrderParts.Text = "Parts Orders"
        '
        'btnDeliveryList
        '
        Me.btnDeliveryList.AutoSize = True
        Me.btnDeliveryList.BackColor = System.Drawing.Color.Transparent
        Me.btnDeliveryList.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeliveryList.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnDeliveryList.Location = New System.Drawing.Point(12, 181)
        Me.btnDeliveryList.Name = "btnDeliveryList"
        Me.btnDeliveryList.Size = New System.Drawing.Size(86, 16)
        Me.btnDeliveryList.TabIndex = 22
        Me.btnDeliveryList.Text = "Delivery List"
        '
        'btnCompleted
        '
        Me.btnCompleted.AutoSize = True
        Me.btnCompleted.BackColor = System.Drawing.Color.Transparent
        Me.btnCompleted.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCompleted.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnCompleted.Location = New System.Drawing.Point(12, 156)
        Me.btnCompleted.Name = "btnCompleted"
        Me.btnCompleted.Size = New System.Drawing.Size(77, 16)
        Me.btnCompleted.TabIndex = 19
        Me.btnCompleted.Text = "Completed"
        '
        'ShowHideMain
        '
        Me.ShowHideMain.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ShowHideMain.AutoSize = True
        Me.ShowHideMain.BackColor = System.Drawing.Color.Transparent
        Me.ShowHideMain.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ShowHideMain.ForeColor = System.Drawing.Color.MidnightBlue
        Me.ShowHideMain.Location = New System.Drawing.Point(3, 696)
        Me.ShowHideMain.Name = "ShowHideMain"
        Me.ShowHideMain.Size = New System.Drawing.Size(149, 16)
        Me.ShowHideMain.TabIndex = 18
        Me.ShowHideMain.Text = "Show/Hide Main Page"
        '
        'btnPrioritizeWork
        '
        Me.btnPrioritizeWork.AutoSize = True
        Me.btnPrioritizeWork.BackColor = System.Drawing.Color.Transparent
        Me.btnPrioritizeWork.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrioritizeWork.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnPrioritizeWork.Location = New System.Drawing.Point(12, 131)
        Me.btnPrioritizeWork.Name = "btnPrioritizeWork"
        Me.btnPrioritizeWork.Size = New System.Drawing.Size(103, 16)
        Me.btnPrioritizeWork.TabIndex = 17
        Me.btnPrioritizeWork.Text = "Prioritize Work"
        '
        'btnSearchIssues
        '
        Me.btnSearchIssues.AutoSize = True
        Me.btnSearchIssues.BackColor = System.Drawing.Color.Transparent
        Me.btnSearchIssues.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchIssues.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnSearchIssues.Location = New System.Drawing.Point(12, 106)
        Me.btnSearchIssues.Name = "btnSearchIssues"
        Me.btnSearchIssues.Size = New System.Drawing.Size(95, 16)
        Me.btnSearchIssues.TabIndex = 16
        Me.btnSearchIssues.Text = "Search Issues"
        '
        'btnNewWO
        '
        Me.btnNewWO.AutoSize = True
        Me.btnNewWO.BackColor = System.Drawing.Color.Transparent
        Me.btnNewWO.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewWO.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnNewWO.Location = New System.Drawing.Point(12, 56)
        Me.btnNewWO.Name = "btnNewWO"
        Me.btnNewWO.Size = New System.Drawing.Size(109, 16)
        Me.btnNewWO.TabIndex = 15
        Me.btnNewWO.Text = "Create New WO"
        '
        'btnSearchCustomers
        '
        Me.btnSearchCustomers.AutoSize = True
        Me.btnSearchCustomers.BackColor = System.Drawing.Color.Transparent
        Me.btnSearchCustomers.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchCustomers.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnSearchCustomers.Location = New System.Drawing.Point(12, 81)
        Me.btnSearchCustomers.Name = "btnSearchCustomers"
        Me.btnSearchCustomers.Size = New System.Drawing.Size(123, 16)
        Me.btnSearchCustomers.TabIndex = 14
        Me.btnSearchCustomers.Text = "Search Customers"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImage = Global.MMSOnline.My.Resources.Resources.MMSOnline
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(0, -6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(161, 50)
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.BtnWFPU)
        Me.SplitContainer2.Panel1.Controls.Add(Me.BtnRigs)
        Me.SplitContainer2.Panel1.Controls.Add(Me.BtnActive)
        Me.SplitContainer2.Panel1.Controls.Add(Me.BtnClear)
        Me.SplitContainer2.Panel1.Controls.Add(Me.CheckBox2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.CheckBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.DateTimePicker2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.DateTimePicker1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmdGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.listStore)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtWOnum)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.listStatus)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtloading)
        Me.SplitContainer2.Panel2.Controls.Add(Me.DataView)
        Me.SplitContainer2.Size = New System.Drawing.Size(915, 721)
        Me.SplitContainer2.SplitterDistance = 200
        Me.SplitContainer2.SplitterWidth = 1
        Me.SplitContainer2.TabIndex = 0
        '
        'BtnWFPU
        '
        Me.BtnWFPU.Location = New System.Drawing.Point(512, 138)
        Me.BtnWFPU.Name = "BtnWFPU"
        Me.BtnWFPU.Size = New System.Drawing.Size(52, 23)
        Me.BtnWFPU.TabIndex = 19
        Me.BtnWFPU.Text = "To P/U"
        Me.BtnWFPU.UseVisualStyleBackColor = True
        '
        'BtnRigs
        '
        Me.BtnRigs.Location = New System.Drawing.Point(512, 160)
        Me.BtnRigs.Name = "BtnRigs"
        Me.BtnRigs.Size = New System.Drawing.Size(141, 23)
        Me.BtnRigs.TabIndex = 18
        Me.BtnRigs.Text = "Rigs to approve"
        Me.BtnRigs.UseVisualStyleBackColor = True
        '
        'BtnActive
        '
        Me.BtnActive.Location = New System.Drawing.Point(567, 138)
        Me.BtnActive.Name = "BtnActive"
        Me.BtnActive.Size = New System.Drawing.Size(86, 23)
        Me.BtnActive.TabIndex = 17
        Me.BtnActive.Text = "Active"
        Me.BtnActive.UseVisualStyleBackColor = True
        '
        'BtnClear
        '
        Me.BtnClear.Location = New System.Drawing.Point(570, 5)
        Me.BtnClear.Name = "BtnClear"
        Me.BtnClear.Size = New System.Drawing.Size(83, 23)
        Me.BtnClear.TabIndex = 16
        Me.BtnClear.Text = "None"
        Me.BtnClear.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(374, 138)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox2.TabIndex = 5
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(374, 112)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 3
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 141)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(149, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Requested Completed Before:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(28, 115)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(134, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "To Be Dropped Off Before:"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(168, 134)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker2.TabIndex = 4
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(168, 108)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker1.TabIndex = 2
        '
        'cmdGo
        '
        Me.cmdGo.Location = New System.Drawing.Point(315, 160)
        Me.cmdGo.Name = "cmdGo"
        Me.cmdGo.Size = New System.Drawing.Size(75, 23)
        Me.cmdGo.TabIndex = 8
        Me.cmdGo.Text = "Search"
        Me.cmdGo.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(512, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Status"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(398, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Store"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(55, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(155, 25)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Search Criteria"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(87, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Work Order #:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(124, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Name:"
        '
        'listStore
        '
        Me.listStore.CheckOnClick = True
        Me.listStore.FormattingEnabled = True
        Me.listStore.Location = New System.Drawing.Point(398, 29)
        Me.listStore.Name = "listStore"
        Me.listStore.Size = New System.Drawing.Size(108, 154)
        Me.listStore.TabIndex = 6
        '
        'txtWOnum
        '
        Me.txtWOnum.Location = New System.Drawing.Point(168, 82)
        Me.txtWOnum.Name = "txtWOnum"
        Me.txtWOnum.Size = New System.Drawing.Size(111, 20)
        Me.txtWOnum.TabIndex = 1
        '
        'txtname
        '
        Me.txtname.Location = New System.Drawing.Point(168, 56)
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(111, 20)
        Me.txtname.TabIndex = 0
        '
        'listStatus
        '
        Me.listStatus.CheckOnClick = True
        Me.listStatus.FormattingEnabled = True
        Me.listStatus.Items.AddRange(New Object() {"Scheduled", "Requested Rig", "Approved Rig", "Active", "Waiting For Parts", "Waiting For Pickup", "Storage", "Closed", "Void"})
        Me.listStatus.Location = New System.Drawing.Point(512, 29)
        Me.listStatus.Name = "listStatus"
        Me.listStatus.Size = New System.Drawing.Size(141, 109)
        Me.listStatus.TabIndex = 7
        '
        'txtloading
        '
        Me.txtloading.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtloading.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloading.Location = New System.Drawing.Point(0, 0)
        Me.txtloading.Name = "txtloading"
        Me.txtloading.ReadOnly = True
        Me.txtloading.Size = New System.Drawing.Size(915, 29)
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
        Me.DataView.Name = "DataView"
        Me.DataView.ReadOnly = True
        Me.DataView.RowHeadersVisible = False
        Me.DataView.Size = New System.Drawing.Size(915, 520)
        Me.DataView.TabIndex = 0
        '
        'Edit
        '
        Me.Edit.HeaderText = "Details"
        Me.Edit.Name = "Edit"
        Me.Edit.ReadOnly = True
        Me.Edit.Text = "Edit"
        Me.Edit.UseColumnTextForButtonValue = True
        Me.Edit.Width = 45
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 120000
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 5000
        '
        'frmServiceMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(1076, 721)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmServiceMain"
        Me.Text = "MMSonline"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DataView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdGo As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents listStore As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtWOnum As System.Windows.Forms.TextBox
    Friend WithEvents txtname As System.Windows.Forms.TextBox
    Friend WithEvents listStatus As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtloading As System.Windows.Forms.TextBox
    Friend WithEvents DataView As System.Windows.Forms.DataGridView
    Friend WithEvents Edit As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnNewWO As System.Windows.Forms.Label
    Friend WithEvents btnSearchCustomers As System.Windows.Forms.Label
    Friend WithEvents btnSearchIssues As System.Windows.Forms.Label
    Friend WithEvents btnPrioritizeWork As System.Windows.Forms.Label
    Friend WithEvents ShowHideMain As System.Windows.Forms.Label
    Friend WithEvents BtnRigs As System.Windows.Forms.Button
    Friend WithEvents BtnActive As System.Windows.Forms.Button
    Friend WithEvents BtnClear As System.Windows.Forms.Button
    Friend WithEvents btnCompleted As System.Windows.Forms.Label
    Friend WithEvents BtnWFPU As System.Windows.Forms.Button
    Friend WithEvents btnDeliveryList As System.Windows.Forms.Label
    Friend WithEvents btnOrderParts As System.Windows.Forms.Label
    Friend WithEvents btnWarrantyClaims As System.Windows.Forms.Label
    Friend WithEvents btnScheduler As System.Windows.Forms.Label
End Class
