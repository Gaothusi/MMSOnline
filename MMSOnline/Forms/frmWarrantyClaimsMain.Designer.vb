<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWarrantyClaimsMain
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnStatusNone = New System.Windows.Forms.Button()
        Me.btnStatusAll = New System.Windows.Forms.Button()
        Me.btnStoreNone = New System.Windows.Forms.Button()
        Me.btnStoreAll = New System.Windows.Forms.Button()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtClaimNum = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboWarrantyType = New System.Windows.Forms.ComboBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.chkDateTo = New System.Windows.Forms.CheckBox()
        Me.chkDateFrom = New System.Windows.Forms.CheckBox()
        Me.dtpDateTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtMotor = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtBoatSer = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtWOnum = New System.Windows.Forms.TextBox()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.listStore = New System.Windows.Forms.CheckedListBox()
        Me.listStatus = New System.Windows.Forms.CheckedListBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtloading = New System.Windows.Forms.TextBox()
        Me.DataView = New System.Windows.Forms.DataGridView()
        Me.Edit = New System.Windows.Forms.DataGridViewButtonColumn()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DataView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnStatusNone
        '
        Me.btnStatusNone.Location = New System.Drawing.Point(707, 6)
        Me.btnStatusNone.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStatusNone.Name = "btnStatusNone"
        Me.btnStatusNone.Size = New System.Drawing.Size(64, 28)
        Me.btnStatusNone.TabIndex = 39
        Me.btnStatusNone.Text = "None"
        Me.btnStatusNone.UseVisualStyleBackColor = True
        '
        'btnStatusAll
        '
        Me.btnStatusAll.Location = New System.Drawing.Point(641, 6)
        Me.btnStatusAll.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStatusAll.Name = "btnStatusAll"
        Me.btnStatusAll.Size = New System.Drawing.Size(64, 28)
        Me.btnStatusAll.TabIndex = 38
        Me.btnStatusAll.Text = "All"
        Me.btnStatusAll.UseVisualStyleBackColor = True
        '
        'btnStoreNone
        '
        Me.btnStoreNone.Location = New System.Drawing.Point(505, 6)
        Me.btnStoreNone.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStoreNone.Name = "btnStoreNone"
        Me.btnStoreNone.Size = New System.Drawing.Size(64, 28)
        Me.btnStoreNone.TabIndex = 37
        Me.btnStoreNone.Text = "None"
        Me.btnStoreNone.UseVisualStyleBackColor = True
        '
        'btnStoreAll
        '
        Me.btnStoreAll.Location = New System.Drawing.Point(440, 6)
        Me.btnStoreAll.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStoreAll.Name = "btnStoreAll"
        Me.btnStoreAll.Size = New System.Drawing.Size(64, 28)
        Me.btnStoreAll.TabIndex = 36
        Me.btnStoreAll.Text = "All"
        Me.btnStoreAll.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtClaimNum)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label12)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label11)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboWarrantyType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnExport)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkDateTo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkDateFrom)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpDateTo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpDateFrom)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label9)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label10)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtMotor)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtBoatSer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnStatusNone)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnStatusAll)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnStoreNone)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnStoreAll)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtWOnum)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmdSearch)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.listStore)
        Me.SplitContainer2.Panel1.Controls.Add(Me.listStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label7)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtloading)
        Me.SplitContainer2.Panel2.Controls.Add(Me.DataView)
        Me.SplitContainer2.Size = New System.Drawing.Size(1189, 1037)
        Me.SplitContainer2.SplitterDistance = 230
        Me.SplitContainer2.SplitterWidth = 1
        Me.SplitContainer2.TabIndex = 0
        '
        'txtClaimNum
        '
        Me.txtClaimNum.Location = New System.Drawing.Point(157, 156)
        Me.txtClaimNum.Margin = New System.Windows.Forms.Padding(4)
        Me.txtClaimNum.Name = "txtClaimNum"
        Me.txtClaimNum.Size = New System.Drawing.Size(213, 22)
        Me.txtClaimNum.TabIndex = 96
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(93, 160)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(62, 17)
        Me.Label12.TabIndex = 97
        Me.Label12.Text = "Claim # :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(89, 247)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 17)
        Me.Label11.TabIndex = 95
        Me.Label11.Text = "Supplier :"
        '
        'cboWarrantyType
        '
        Me.cboWarrantyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWarrantyType.FormattingEnabled = True
        Me.cboWarrantyType.Items.AddRange(New Object() {"All", "Atlantis", "Bayliner", "Boathouse", "Centurion", "Ez Loader", "Four Winns", "Glastron", "Hewescraft", "Indmar", "Jensen Electronics", "Karavan", "Lighthouse", "Mercury", "Mid West", "Monster", "Monterey", "PCM", "Pro Tech/Diamond Coat", "Prospec Electronics", "Renfrew", "Riverhawk", "Rotax", "Sanger", "Scarab", "Sea Ray", "Shipwreck", "Sylvan/Smoker", "Volvo", "Yamaha", "Other"})
        Me.cboWarrantyType.Location = New System.Drawing.Point(157, 244)
        Me.cboWarrantyType.Margin = New System.Windows.Forms.Padding(4)
        Me.cboWarrantyType.Name = "cboWarrantyType"
        Me.cboWarrantyType.Size = New System.Drawing.Size(213, 24)
        Me.cboWarrantyType.TabIndex = 94
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(957, 236)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(195, 28)
        Me.btnExport.TabIndex = 55
        Me.btnExport.Text = "Export Results to Excel"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'chkDateTo
        '
        Me.chkDateTo.AutoSize = True
        Me.chkDateTo.Location = New System.Drawing.Point(353, 215)
        Me.chkDateTo.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDateTo.Name = "chkDateTo"
        Me.chkDateTo.Size = New System.Drawing.Size(18, 17)
        Me.chkDateTo.TabIndex = 54
        Me.chkDateTo.UseVisualStyleBackColor = True
        '
        'chkDateFrom
        '
        Me.chkDateFrom.AutoSize = True
        Me.chkDateFrom.Location = New System.Drawing.Point(353, 187)
        Me.chkDateFrom.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDateFrom.Name = "chkDateFrom"
        Me.chkDateFrom.Size = New System.Drawing.Size(18, 17)
        Me.chkDateFrom.TabIndex = 53
        Me.chkDateFrom.UseVisualStyleBackColor = True
        '
        'dtpDateTo
        '
        Me.dtpDateTo.Location = New System.Drawing.Point(157, 212)
        Me.dtpDateTo.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(187, 22)
        Me.dtpDateTo.TabIndex = 52
        '
        'dtpDateFrom
        '
        Me.dtpDateFrom.Location = New System.Drawing.Point(157, 183)
        Me.dtpDateFrom.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpDateFrom.Name = "dtpDateFrom"
        Me.dtpDateFrom.Size = New System.Drawing.Size(187, 22)
        Me.dtpDateFrom.TabIndex = 51
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(36, 217)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(121, 17)
        Me.Label9.TabIndex = 50
        Me.Label9.Text = "Drop Off Date To:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(23, 187)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(136, 17)
        Me.Label10.TabIndex = 48
        Me.Label10.Text = "Drop Off Date From:"
        '
        'txtMotor
        '
        Me.txtMotor.Location = New System.Drawing.Point(157, 128)
        Me.txtMotor.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMotor.Name = "txtMotor"
        Me.txtMotor.Size = New System.Drawing.Size(213, 22)
        Me.txtMotor.TabIndex = 45
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(104, 132)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 17)
        Me.Label8.TabIndex = 46
        Me.Label8.Text = "Motor :"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(157, 100)
        Me.txtName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(213, 22)
        Me.txtName.TabIndex = 2
        '
        'txtBoatSer
        '
        Me.txtBoatSer.Location = New System.Drawing.Point(157, 71)
        Me.txtBoatSer.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBoatSer.Name = "txtBoatSer"
        Me.txtBoatSer.Size = New System.Drawing.Size(213, 22)
        Me.txtBoatSer.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(64, 75)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 17)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Boat Serial #:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(577, 4)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 17)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Approval"
        '
        'txtWOnum
        '
        Me.txtWOnum.Location = New System.Drawing.Point(157, 43)
        Me.txtWOnum.Margin = New System.Windows.Forms.Padding(4)
        Me.txtWOnum.Name = "txtWOnum"
        Me.txtWOnum.Size = New System.Drawing.Size(213, 22)
        Me.txtWOnum.TabIndex = 0
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(808, 236)
        Me.cmdSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(100, 28)
        Me.cmdSearch.TabIndex = 5
        Me.cmdSearch.Text = "Search"
        Me.cmdSearch.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(577, 18)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 17)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Status"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(383, 16)
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
        Me.Label4.Location = New System.Drawing.Point(69, 4)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(195, 31)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Search Criteria"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(59, 47)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 17)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Work Order #:"
        '
        'listStore
        '
        Me.listStore.CheckOnClick = True
        Me.listStore.FormattingEnabled = True
        Me.listStore.Location = New System.Drawing.Point(383, 42)
        Me.listStore.Margin = New System.Windows.Forms.Padding(4)
        Me.listStore.Name = "listStore"
        Me.listStore.Size = New System.Drawing.Size(185, 225)
        Me.listStore.TabIndex = 2
        '
        'listStatus
        '
        Me.listStatus.CheckOnClick = True
        Me.listStatus.FormattingEnabled = True
        Me.listStatus.Items.AddRange(New Object() {"To Be Processed", "Waiting for Approval", "Pre Authorized", "Approved ", "Waiting for Parts", "Waiting on Customer", "Returning Boat to Factory", "Paid Not Complete", "Complete Unpaid", "Complete Paid", "Rejected"})
        Me.listStatus.Location = New System.Drawing.Point(577, 42)
        Me.listStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.listStatus.Name = "listStatus"
        Me.listStatus.Size = New System.Drawing.Size(192, 225)
        Me.listStatus.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(104, 103)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 17)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "Name :"
        '
        'txtloading
        '
        Me.txtloading.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtloading.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloading.Location = New System.Drawing.Point(0, 0)
        Me.txtloading.Margin = New System.Windows.Forms.Padding(4)
        Me.txtloading.Name = "txtloading"
        Me.txtloading.ReadOnly = True
        Me.txtloading.Size = New System.Drawing.Size(1189, 34)
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
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray
        Me.DataView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Edit})
        Me.DataView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataView.GridColor = System.Drawing.SystemColors.ControlLightLight
        Me.DataView.Location = New System.Drawing.Point(0, 0)
        Me.DataView.Margin = New System.Windows.Forms.Padding(4)
        Me.DataView.Name = "DataView"
        Me.DataView.ReadOnly = True
        Me.DataView.RowHeadersVisible = False
        Me.DataView.Size = New System.Drawing.Size(1189, 806)
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
        'frmWarrantyClaimsMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1189, 1037)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmWarrantyClaimsMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Warranty Claims"
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DataView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnStatusNone As System.Windows.Forms.Button
    Friend WithEvents btnStatusAll As System.Windows.Forms.Button
    Friend WithEvents btnStoreNone As System.Windows.Forms.Button
    Friend WithEvents btnStoreAll As System.Windows.Forms.Button
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtWOnum As System.Windows.Forms.TextBox
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents listStore As System.Windows.Forms.CheckedListBox
    Friend WithEvents listStatus As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtloading As System.Windows.Forms.TextBox
    Friend WithEvents DataView As System.Windows.Forms.DataGridView
    Friend WithEvents Edit As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBoatSer As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtMotor As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents chkDateTo As System.Windows.Forms.CheckBox
    Friend WithEvents chkDateFrom As System.Windows.Forms.CheckBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboWarrantyType As System.Windows.Forms.ComboBox
    Friend WithEvents txtClaimNum As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
End Class
