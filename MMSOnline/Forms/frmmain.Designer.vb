<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmmain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
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
        Me.btnDeliverySchedule = New System.Windows.Forms.Label()
        Me.btnWinterizeEmails = New System.Windows.Forms.Label()
        Me.lblTesting = New System.Windows.Forms.Label()
        Me.btnDeliveryList = New System.Windows.Forms.Label()
        Me.btnQuickPaymentCalc = New System.Windows.Forms.Label()
        Me.btnEditDefaultNote = New System.Windows.Forms.Label()
        Me.btnCommissions = New System.Windows.Forms.Label()
        Me.btnViewStats = New System.Windows.Forms.Label()
        Me.btnViewReports = New System.Windows.Forms.Label()
        Me.btnChangeMyPassword = New System.Windows.Forms.Label()
        Me.btnUserManagement = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnApprovedOrders = New System.Windows.Forms.Label()
        Me.btnRequestOrders = New System.Windows.Forms.Label()
        Me.btnService = New System.Windows.Forms.Label()
        Me.btnAddInventory = New System.Windows.Forms.Label()
        Me.btnNewBOS = New System.Windows.Forms.Label()
        Me.btnViewInventory = New System.Windows.Forms.Label()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.BtnStoreNone = New System.Windows.Forms.Button()
        Me.BtnStoreAll = New System.Windows.Forms.Button()
        Me.BtnStatusNon = New System.Windows.Forms.Button()
        Me.BtnStatusAll = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.chkcommisioned = New System.Windows.Forms.CheckBox()
        Me.txtcommisioning = New System.Windows.Forms.Label()
        Me.ChkSmallItems = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.listshow = New System.Windows.Forms.CheckedListBox()
        Me.cmdreset = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.comboSalesman = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.comboBrand = New System.Windows.Forms.ComboBox()
        Me.listStore = New System.Windows.Forms.CheckedListBox()
        Me.txtBOS = New System.Windows.Forms.TextBox()
        Me.txtbuyername = New System.Windows.Forms.TextBox()
        Me.listStatus = New System.Windows.Forms.CheckedListBox()
        Me.txtloading = New System.Windows.Forms.TextBox()
        Me.DataView = New System.Windows.Forms.DataGridView()
        Me.Edit = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
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
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Window
        Me.SplitContainer1.Panel1.BackgroundImage = Global.MMSOnline.My.Resources.Resources.BkgTL
        Me.SplitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnDeliverySchedule)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnWinterizeEmails)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTesting)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnDeliveryList)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnQuickPaymentCalc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnEditDefaultNote)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnCommissions)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnViewStats)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnViewReports)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnChangeMyPassword)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnUserManagement)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PictureBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnApprovedOrders)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnRequestOrders)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnService)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddInventory)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNewBOS)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnViewInventory)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1056, 766)
        Me.SplitContainer1.SplitterDistance = 160
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 0
        Me.SplitContainer1.TabStop = False
        '
        'btnDeliverySchedule
        '
        Me.btnDeliverySchedule.AutoSize = True
        Me.btnDeliverySchedule.BackColor = System.Drawing.Color.Transparent
        Me.btnDeliverySchedule.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeliverySchedule.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnDeliverySchedule.Location = New System.Drawing.Point(17, 556)
        Me.btnDeliverySchedule.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnDeliverySchedule.Name = "btnDeliverySchedule"
        Me.btnDeliverySchedule.Size = New System.Drawing.Size(149, 19)
        Me.btnDeliverySchedule.TabIndex = 24
        Me.btnDeliverySchedule.Text = "Delivery Schedule"
        '
        'btnWinterizeEmails
        '
        Me.btnWinterizeEmails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnWinterizeEmails.AutoSize = True
        Me.btnWinterizeEmails.BackColor = System.Drawing.Color.Transparent
        Me.btnWinterizeEmails.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWinterizeEmails.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnWinterizeEmails.Location = New System.Drawing.Point(17, 699)
        Me.btnWinterizeEmails.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnWinterizeEmails.Name = "btnWinterizeEmails"
        Me.btnWinterizeEmails.Size = New System.Drawing.Size(140, 19)
        Me.btnWinterizeEmails.TabIndex = 23
        Me.btnWinterizeEmails.Text = "Winterize Emails"
        '
        'lblTesting
        '
        Me.lblTesting.AutoSize = True
        Me.lblTesting.BackColor = System.Drawing.Color.Transparent
        Me.lblTesting.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTesting.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblTesting.Location = New System.Drawing.Point(17, 596)
        Me.lblTesting.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTesting.Name = "lblTesting"
        Me.lblTesting.Size = New System.Drawing.Size(125, 19)
        Me.lblTesting.TabIndex = 22
        Me.lblTesting.Text = "Testing Button"
        Me.lblTesting.Visible = False
        '
        'btnDeliveryList
        '
        Me.btnDeliveryList.AutoSize = True
        Me.btnDeliveryList.BackColor = System.Drawing.Color.Transparent
        Me.btnDeliveryList.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeliveryList.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnDeliveryList.Location = New System.Drawing.Point(17, 521)
        Me.btnDeliveryList.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnDeliveryList.Name = "btnDeliveryList"
        Me.btnDeliveryList.Size = New System.Drawing.Size(106, 19)
        Me.btnDeliveryList.TabIndex = 21
        Me.btnDeliveryList.Text = "Delivery List"
        '
        'btnQuickPaymentCalc
        '
        Me.btnQuickPaymentCalc.AutoSize = True
        Me.btnQuickPaymentCalc.BackColor = System.Drawing.Color.Transparent
        Me.btnQuickPaymentCalc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnQuickPaymentCalc.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnQuickPaymentCalc.Location = New System.Drawing.Point(17, 485)
        Me.btnQuickPaymentCalc.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnQuickPaymentCalc.Name = "btnQuickPaymentCalc"
        Me.btnQuickPaymentCalc.Size = New System.Drawing.Size(166, 19)
        Me.btnQuickPaymentCalc.TabIndex = 20
        Me.btnQuickPaymentCalc.Text = "Quick Payment Calc"
        '
        'btnEditDefaultNote
        '
        Me.btnEditDefaultNote.AutoSize = True
        Me.btnEditDefaultNote.BackColor = System.Drawing.Color.Transparent
        Me.btnEditDefaultNote.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditDefaultNote.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnEditDefaultNote.Location = New System.Drawing.Point(16, 449)
        Me.btnEditDefaultNote.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnEditDefaultNote.Name = "btnEditDefaultNote"
        Me.btnEditDefaultNote.Size = New System.Drawing.Size(142, 19)
        Me.btnEditDefaultNote.TabIndex = 19
        Me.btnEditDefaultNote.Text = "Edit Default Note"
        '
        'btnCommissions
        '
        Me.btnCommissions.AutoSize = True
        Me.btnCommissions.BackColor = System.Drawing.Color.Transparent
        Me.btnCommissions.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCommissions.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnCommissions.Location = New System.Drawing.Point(16, 414)
        Me.btnCommissions.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnCommissions.Name = "btnCommissions"
        Me.btnCommissions.Size = New System.Drawing.Size(114, 19)
        Me.btnCommissions.TabIndex = 18
        Me.btnCommissions.Text = "Commissions"
        '
        'btnViewStats
        '
        Me.btnViewStats.AutoSize = True
        Me.btnViewStats.BackColor = System.Drawing.Color.Transparent
        Me.btnViewStats.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewStats.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnViewStats.Location = New System.Drawing.Point(16, 342)
        Me.btnViewStats.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnViewStats.Name = "btnViewStats"
        Me.btnViewStats.Size = New System.Drawing.Size(92, 19)
        Me.btnViewStats.TabIndex = 17
        Me.btnViewStats.Text = "View Stats"
        '
        'btnViewReports
        '
        Me.btnViewReports.AutoSize = True
        Me.btnViewReports.BackColor = System.Drawing.Color.Transparent
        Me.btnViewReports.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewReports.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnViewReports.Location = New System.Drawing.Point(16, 306)
        Me.btnViewReports.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnViewReports.Name = "btnViewReports"
        Me.btnViewReports.Size = New System.Drawing.Size(114, 19)
        Me.btnViewReports.TabIndex = 16
        Me.btnViewReports.Text = "View Reports"
        '
        'btnChangeMyPassword
        '
        Me.btnChangeMyPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnChangeMyPassword.AutoSize = True
        Me.btnChangeMyPassword.BackColor = System.Drawing.Color.Transparent
        Me.btnChangeMyPassword.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangeMyPassword.ForeColor = System.Drawing.Color.LightSlateGray
        Me.btnChangeMyPassword.Location = New System.Drawing.Point(16, 735)
        Me.btnChangeMyPassword.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnChangeMyPassword.Name = "btnChangeMyPassword"
        Me.btnChangeMyPassword.Size = New System.Drawing.Size(179, 19)
        Me.btnChangeMyPassword.TabIndex = 15
        Me.btnChangeMyPassword.Text = "Change My Password"
        '
        'btnUserManagement
        '
        Me.btnUserManagement.AutoSize = True
        Me.btnUserManagement.BackColor = System.Drawing.Color.Transparent
        Me.btnUserManagement.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUserManagement.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnUserManagement.Location = New System.Drawing.Point(16, 378)
        Me.btnUserManagement.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnUserManagement.Name = "btnUserManagement"
        Me.btnUserManagement.Size = New System.Drawing.Size(150, 19)
        Me.btnUserManagement.TabIndex = 14
        Me.btnUserManagement.Text = "User Management"
        Me.btnUserManagement.Visible = False
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
        'btnApprovedOrders
        '
        Me.btnApprovedOrders.AutoSize = True
        Me.btnApprovedOrders.BackColor = System.Drawing.Color.Transparent
        Me.btnApprovedOrders.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApprovedOrders.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnApprovedOrders.Location = New System.Drawing.Point(16, 271)
        Me.btnApprovedOrders.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnApprovedOrders.Name = "btnApprovedOrders"
        Me.btnApprovedOrders.Size = New System.Drawing.Size(145, 19)
        Me.btnApprovedOrders.TabIndex = 12
        Me.btnApprovedOrders.Text = "Approved Orders"
        '
        'btnRequestOrders
        '
        Me.btnRequestOrders.AutoSize = True
        Me.btnRequestOrders.BackColor = System.Drawing.Color.Transparent
        Me.btnRequestOrders.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRequestOrders.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnRequestOrders.Location = New System.Drawing.Point(16, 235)
        Me.btnRequestOrders.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnRequestOrders.Name = "btnRequestOrders"
        Me.btnRequestOrders.Size = New System.Drawing.Size(153, 19)
        Me.btnRequestOrders.TabIndex = 11
        Me.btnRequestOrders.Text = "Requested Orders"
        '
        'btnService
        '
        Me.btnService.AutoSize = True
        Me.btnService.BackColor = System.Drawing.Color.Transparent
        Me.btnService.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnService.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnService.Location = New System.Drawing.Point(16, 87)
        Me.btnService.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnService.Name = "btnService"
        Me.btnService.Size = New System.Drawing.Size(80, 22)
        Me.btnService.TabIndex = 10
        Me.btnService.Text = "Service"
        Me.btnService.Visible = False
        '
        'btnAddInventory
        '
        Me.btnAddInventory.AutoSize = True
        Me.btnAddInventory.BackColor = System.Drawing.Color.Transparent
        Me.btnAddInventory.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddInventory.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnAddInventory.Location = New System.Drawing.Point(16, 199)
        Me.btnAddInventory.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnAddInventory.Name = "btnAddInventory"
        Me.btnAddInventory.Size = New System.Drawing.Size(119, 19)
        Me.btnAddInventory.TabIndex = 2
        Me.btnAddInventory.Text = "Add Inventory"
        '
        'btnNewBOS
        '
        Me.btnNewBOS.AutoSize = True
        Me.btnNewBOS.BackColor = System.Drawing.Color.Transparent
        Me.btnNewBOS.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewBOS.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnNewBOS.Location = New System.Drawing.Point(16, 126)
        Me.btnNewBOS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnNewBOS.Name = "btnNewBOS"
        Me.btnNewBOS.Size = New System.Drawing.Size(151, 22)
        Me.btnNewBOS.TabIndex = 0
        Me.btnNewBOS.Text = "New Bill of Sale"
        '
        'btnViewInventory
        '
        Me.btnViewInventory.AutoSize = True
        Me.btnViewInventory.BackColor = System.Drawing.Color.Transparent
        Me.btnViewInventory.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewInventory.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnViewInventory.Location = New System.Drawing.Point(16, 164)
        Me.btnViewInventory.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnViewInventory.Name = "btnViewInventory"
        Me.btnViewInventory.Size = New System.Drawing.Size(125, 19)
        Me.btnViewInventory.TabIndex = 1
        Me.btnViewInventory.Text = "View Inventory"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.BtnStoreNone)
        Me.SplitContainer2.Panel1.Controls.Add(Me.BtnStoreAll)
        Me.SplitContainer2.Panel1.Controls.Add(Me.BtnStatusNon)
        Me.SplitContainer2.Panel1.Controls.Add(Me.BtnStatusAll)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnSearch)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkcommisioned)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtcommisioning)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ChkSmallItems)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label10)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label9)
        Me.SplitContainer2.Panel1.Controls.Add(Me.listshow)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmdreset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.comboSalesman)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.comboBrand)
        Me.SplitContainer2.Panel1.Controls.Add(Me.listStore)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtBOS)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtbuyername)
        Me.SplitContainer2.Panel1.Controls.Add(Me.listStatus)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtloading)
        Me.SplitContainer2.Panel2.Controls.Add(Me.DataView)
        Me.SplitContainer2.Size = New System.Drawing.Size(895, 766)
        Me.SplitContainer2.SplitterDistance = 200
        Me.SplitContainer2.SplitterWidth = 1
        Me.SplitContainer2.TabIndex = 0
        '
        'BtnStoreNone
        '
        Me.BtnStoreNone.Location = New System.Drawing.Point(425, 7)
        Me.BtnStoreNone.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnStoreNone.Name = "BtnStoreNone"
        Me.BtnStoreNone.Size = New System.Drawing.Size(64, 28)
        Me.BtnStoreNone.TabIndex = 35
        Me.BtnStoreNone.Text = "None"
        Me.BtnStoreNone.UseVisualStyleBackColor = True
        '
        'BtnStoreAll
        '
        Me.BtnStoreAll.Location = New System.Drawing.Point(353, 7)
        Me.BtnStoreAll.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnStoreAll.Name = "BtnStoreAll"
        Me.BtnStoreAll.Size = New System.Drawing.Size(64, 28)
        Me.BtnStoreAll.TabIndex = 34
        Me.BtnStoreAll.Text = "All"
        Me.BtnStoreAll.UseVisualStyleBackColor = True
        '
        'BtnStatusNon
        '
        Me.BtnStatusNon.Location = New System.Drawing.Point(631, 7)
        Me.BtnStatusNon.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnStatusNon.Name = "BtnStatusNon"
        Me.BtnStatusNon.Size = New System.Drawing.Size(64, 28)
        Me.BtnStatusNon.TabIndex = 33
        Me.BtnStatusNon.Text = "None"
        Me.BtnStatusNon.UseVisualStyleBackColor = True
        '
        'BtnStatusAll
        '
        Me.BtnStatusAll.Location = New System.Drawing.Point(559, 7)
        Me.BtnStatusAll.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnStatusAll.Name = "BtnStatusAll"
        Me.BtnStatusAll.Size = New System.Drawing.Size(64, 28)
        Me.BtnStatusAll.TabIndex = 32
        Me.BtnStatusAll.Text = "All"
        Me.BtnStatusAll.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(195, 212)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 28)
        Me.btnSearch.TabIndex = 20
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'chkcommisioned
        '
        Me.chkcommisioned.AutoSize = True
        Me.chkcommisioned.Location = New System.Drawing.Point(201, 192)
        Me.chkcommisioned.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkcommisioned.Name = "chkcommisioned"
        Me.chkcommisioned.Size = New System.Drawing.Size(18, 17)
        Me.chkcommisioned.TabIndex = 19
        Me.chkcommisioned.UseVisualStyleBackColor = True
        Me.chkcommisioned.Visible = False
        '
        'txtcommisioning
        '
        Me.txtcommisioning.AutoSize = True
        Me.txtcommisioning.Location = New System.Drawing.Point(27, 192)
        Me.txtcommisioning.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.txtcommisioning.Name = "txtcommisioning"
        Me.txtcommisioning.Size = New System.Drawing.Size(169, 17)
        Me.txtcommisioning.TabIndex = 18
        Me.txtcommisioning.Text = "Hide Commisioned Deals:"
        Me.txtcommisioning.Visible = False
        '
        'ChkSmallItems
        '
        Me.ChkSmallItems.AutoSize = True
        Me.ChkSmallItems.Location = New System.Drawing.Point(155, 166)
        Me.ChkSmallItems.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChkSmallItems.Name = "ChkSmallItems"
        Me.ChkSmallItems.Size = New System.Drawing.Size(18, 17)
        Me.ChkSmallItems.TabIndex = 17
        Me.ChkSmallItems.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(27, 169)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(121, 17)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Show Small Items:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(699, 14)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 17)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Show:"
        Me.Label9.Visible = False
        '
        'listshow
        '
        Me.listshow.CheckOnClick = True
        Me.listshow.FormattingEnabled = True
        Me.listshow.Location = New System.Drawing.Point(703, 39)
        Me.listshow.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.listshow.Name = "listshow"
        Me.listshow.Size = New System.Drawing.Size(121, 174)
        Me.listshow.TabIndex = 14
        Me.listshow.Visible = False
        '
        'cmdreset
        '
        Me.cmdreset.Location = New System.Drawing.Point(31, 212)
        Me.cmdreset.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdreset.Name = "cmdreset"
        Me.cmdreset.Size = New System.Drawing.Size(100, 28)
        Me.cmdreset.TabIndex = 6
        Me.cmdreset.Text = "Reset"
        Me.cmdreset.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(27, 143)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 17)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Salesman:"
        '
        'comboSalesman
        '
        Me.comboSalesman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboSalesman.FormattingEnabled = True
        Me.comboSalesman.Location = New System.Drawing.Point(133, 133)
        Me.comboSalesman.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.comboSalesman.Name = "comboSalesman"
        Me.comboSalesman.Size = New System.Drawing.Size(160, 24)
        Me.comboSalesman.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(497, 12)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 17)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Status:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(303, 12)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 17)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Store:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 5)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(195, 31)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Search Criteria"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 111)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Brand:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 76)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 17)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "BOS #:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 44)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 17)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Name:"
        '
        'comboBrand
        '
        Me.comboBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBrand.FormattingEnabled = True
        Me.comboBrand.Location = New System.Drawing.Point(133, 101)
        Me.comboBrand.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.comboBrand.Name = "comboBrand"
        Me.comboBrand.Size = New System.Drawing.Size(160, 24)
        Me.comboBrand.TabIndex = 2
        '
        'listStore
        '
        Me.listStore.CheckOnClick = True
        Me.listStore.FormattingEnabled = True
        Me.listStore.Location = New System.Drawing.Point(303, 39)
        Me.listStore.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.listStore.Name = "listStore"
        Me.listStore.Size = New System.Drawing.Size(185, 174)
        Me.listStore.TabIndex = 4
        '
        'txtBOS
        '
        Me.txtBOS.Location = New System.Drawing.Point(133, 68)
        Me.txtBOS.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtBOS.Name = "txtBOS"
        Me.txtBOS.Size = New System.Drawing.Size(160, 22)
        Me.txtBOS.TabIndex = 1
        '
        'txtbuyername
        '
        Me.txtbuyername.Location = New System.Drawing.Point(133, 36)
        Me.txtbuyername.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtbuyername.Name = "txtbuyername"
        Me.txtbuyername.Size = New System.Drawing.Size(160, 22)
        Me.txtbuyername.TabIndex = 0
        '
        'listStatus
        '
        Me.listStatus.CheckOnClick = True
        Me.listStatus.FormattingEnabled = True
        Me.listStatus.Location = New System.Drawing.Point(497, 39)
        Me.listStatus.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.listStatus.Name = "listStatus"
        Me.listStatus.Size = New System.Drawing.Size(196, 174)
        Me.listStatus.TabIndex = 5
        '
        'txtloading
        '
        Me.txtloading.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtloading.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloading.Location = New System.Drawing.Point(0, 0)
        Me.txtloading.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtloading.Name = "txtloading"
        Me.txtloading.ReadOnly = True
        Me.txtloading.Size = New System.Drawing.Size(895, 34)
        Me.txtloading.TabIndex = 1
        Me.txtloading.Text = "Downloading Data"
        Me.txtloading.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtloading.Visible = False
        '
        'DataView
        '
        Me.DataView.AllowUserToAddRows = False
        Me.DataView.AllowUserToDeleteRows = False
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
        Me.DataView.Size = New System.Drawing.Size(895, 565)
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
        'Timer1
        '
        Me.Timer1.Interval = 200
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 60000
        '
        'Timer3
        '
        '
        'frmmain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1056, 766)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmmain"
        Me.Text = "MMS Online"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
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
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataView As System.Windows.Forms.DataGridView
    Friend WithEvents listStatus As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents comboBrand As System.Windows.Forms.ComboBox
    Friend WithEvents listStore As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtBOS As System.Windows.Forms.TextBox
    Friend WithEvents txtbuyername As System.Windows.Forms.TextBox
    Friend WithEvents cmdreset As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents comboSalesman As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnViewInventory As System.Windows.Forms.Label
    Friend WithEvents btnNewBOS As System.Windows.Forms.Label
    Friend WithEvents btnAddInventory As System.Windows.Forms.Label
    Friend WithEvents btnService As System.Windows.Forms.Label
    Friend WithEvents btnApprovedOrders As System.Windows.Forms.Label
    Friend WithEvents btnRequestOrders As System.Windows.Forms.Label
    Friend WithEvents Edit As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnUserManagement As System.Windows.Forms.Label
    Friend WithEvents txtloading As System.Windows.Forms.TextBox
    Friend WithEvents btnChangeMyPassword As System.Windows.Forms.Label
    Friend WithEvents btnViewReports As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents listshow As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ChkSmallItems As System.Windows.Forms.CheckBox
    Friend WithEvents btnViewStats As System.Windows.Forms.Label
    Friend WithEvents btnCommissions As System.Windows.Forms.Label
    Friend WithEvents btnEditDefaultNote As System.Windows.Forms.Label
    Friend WithEvents chkcommisioned As System.Windows.Forms.CheckBox
    Friend WithEvents txtcommisioning As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents btnQuickPaymentCalc As System.Windows.Forms.Label
    Friend WithEvents BtnStoreNone As System.Windows.Forms.Button
    Friend WithEvents BtnStoreAll As System.Windows.Forms.Button
    Friend WithEvents BtnStatusNon As System.Windows.Forms.Button
    Friend WithEvents BtnStatusAll As System.Windows.Forms.Button
    Friend WithEvents btnDeliveryList As System.Windows.Forms.Label
    Friend WithEvents lblTesting As System.Windows.Forms.Label
    Friend WithEvents btnWinterizeEmails As System.Windows.Forms.Label
    Friend WithEvents btnDeliverySchedule As System.Windows.Forms.Label
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
End Class
