<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInv
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
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.BtnLocNone = New System.Windows.Forms.Button
        Me.BtnLocAll = New System.Windows.Forms.Button
        Me.BtnStatusNone = New System.Windows.Forms.Button
        Me.BtnAllStatus = New System.Windows.Forms.Button
        Me.BtnGo = New System.Windows.Forms.Button
        Me.txtunitfltr = New System.Windows.Forms.TextBox
        Me.txtserfltr = New System.Windows.Forms.TextBox
        Me.txtmdlflter = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnunselall = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnselectall = New System.Windows.Forms.Button
        Me.listshow = New System.Windows.Forms.CheckedListBox
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdxl = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.chkonOrder = New System.Windows.Forms.CheckBox
        Me.liststatus = New System.Windows.Forms.CheckedListBox
        Me.ComboBrand = New System.Windows.Forms.ComboBox
        Me.listyear = New System.Windows.Forms.CheckedListBox
        Me.listStore = New System.Windows.Forms.CheckedListBox
        Me.txtloading = New System.Windows.Forms.TextBox
        Me.DataView = New System.Windows.Forms.DataGridView
        Me.btnEdit = New System.Windows.Forms.DataGridViewButtonColumn
        Me.XLchk = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
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
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackgroundImage = Global.MMSOnline.My.Resources.Resources.BkgTL
        Me.SplitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SplitContainer1.Panel1.Controls.Add(Me.BtnLocNone)
        Me.SplitContainer1.Panel1.Controls.Add(Me.BtnLocAll)
        Me.SplitContainer1.Panel1.Controls.Add(Me.BtnStatusNone)
        Me.SplitContainer1.Panel1.Controls.Add(Me.BtnAllStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.BtnGo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtunitfltr)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtserfltr)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtmdlflter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnunselall)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnselectall)
        Me.SplitContainer1.Panel1.Controls.Add(Me.listshow)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdPrint)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdxl)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkonOrder)
        Me.SplitContainer1.Panel1.Controls.Add(Me.liststatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ComboBrand)
        Me.SplitContainer1.Panel1.Controls.Add(Me.listyear)
        Me.SplitContainer1.Panel1.Controls.Add(Me.listStore)
        Me.SplitContainer1.Panel1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtloading)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataView)
        Me.SplitContainer1.Size = New System.Drawing.Size(792, 598)
        Me.SplitContainer1.SplitterDistance = 200
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 0
        '
        'BtnLocNone
        '
        Me.BtnLocNone.Location = New System.Drawing.Point(300, 4)
        Me.BtnLocNone.Name = "BtnLocNone"
        Me.BtnLocNone.Size = New System.Drawing.Size(48, 23)
        Me.BtnLocNone.TabIndex = 31
        Me.BtnLocNone.Text = "None"
        Me.BtnLocNone.UseVisualStyleBackColor = True
        '
        'BtnLocAll
        '
        Me.BtnLocAll.Location = New System.Drawing.Point(246, 4)
        Me.BtnLocAll.Name = "BtnLocAll"
        Me.BtnLocAll.Size = New System.Drawing.Size(48, 23)
        Me.BtnLocAll.TabIndex = 30
        Me.BtnLocAll.Text = "All"
        Me.BtnLocAll.UseVisualStyleBackColor = True
        '
        'BtnStatusNone
        '
        Me.BtnStatusNone.Location = New System.Drawing.Point(471, 4)
        Me.BtnStatusNone.Name = "BtnStatusNone"
        Me.BtnStatusNone.Size = New System.Drawing.Size(48, 23)
        Me.BtnStatusNone.TabIndex = 29
        Me.BtnStatusNone.Text = "None"
        Me.BtnStatusNone.UseVisualStyleBackColor = True
        '
        'BtnAllStatus
        '
        Me.BtnAllStatus.Location = New System.Drawing.Point(417, 4)
        Me.BtnAllStatus.Name = "BtnAllStatus"
        Me.BtnAllStatus.Size = New System.Drawing.Size(48, 23)
        Me.BtnAllStatus.TabIndex = 28
        Me.BtnAllStatus.Text = "All"
        Me.BtnAllStatus.UseVisualStyleBackColor = True
        '
        'BtnGo
        '
        Me.BtnGo.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.BtnGo.Location = New System.Drawing.Point(676, 163)
        Me.BtnGo.Name = "BtnGo"
        Me.BtnGo.Size = New System.Drawing.Size(104, 23)
        Me.BtnGo.TabIndex = 27
        Me.BtnGo.Text = "Search"
        Me.BtnGo.UseVisualStyleBackColor = True
        '
        'txtunitfltr
        '
        Me.txtunitfltr.Location = New System.Drawing.Point(59, 175)
        Me.txtunitfltr.Name = "txtunitfltr"
        Me.txtunitfltr.Size = New System.Drawing.Size(126, 22)
        Me.txtunitfltr.TabIndex = 26
        '
        'txtserfltr
        '
        Me.txtserfltr.Location = New System.Drawing.Point(59, 150)
        Me.txtserfltr.Name = "txtserfltr"
        Me.txtserfltr.Size = New System.Drawing.Size(126, 22)
        Me.txtserfltr.TabIndex = 25
        '
        'txtmdlflter
        '
        Me.txtmdlflter.Location = New System.Drawing.Point(59, 126)
        Me.txtmdlflter.Name = "txtmdlflter"
        Me.txtmdlflter.Size = New System.Drawing.Size(126, 22)
        Me.txtmdlflter.TabIndex = 24
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label8.Location = New System.Drawing.Point(3, 179)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 16)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Unit #:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label7.Location = New System.Drawing.Point(3, 156)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 16)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Serial:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Location = New System.Drawing.Point(3, 132)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 16)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Model:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(676, 47)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(104, 23)
        Me.Button1.TabIndex = 20
        Me.Button1.Text = "Delete Boats"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'btnunselall
        '
        Me.btnunselall.Location = New System.Drawing.Point(676, 76)
        Me.btnunselall.Name = "btnunselall"
        Me.btnunselall.Size = New System.Drawing.Size(104, 23)
        Me.btnunselall.TabIndex = 19
        Me.btnunselall.Text = "UnSelect All"
        Me.btnunselall.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label6.Location = New System.Drawing.Point(529, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 16)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Show:"
        Me.Label6.Visible = False
        '
        'btnselectall
        '
        Me.btnselectall.Location = New System.Drawing.Point(676, 105)
        Me.btnselectall.Name = "btnselectall"
        Me.btnselectall.Size = New System.Drawing.Size(104, 23)
        Me.btnselectall.TabIndex = 10
        Me.btnselectall.Text = "Select All"
        Me.btnselectall.UseVisualStyleBackColor = True
        '
        'listshow
        '
        Me.listshow.CheckOnClick = True
        Me.listshow.FormattingEnabled = True
        Me.listshow.Location = New System.Drawing.Point(532, 32)
        Me.listshow.Name = "listshow"
        Me.listshow.Size = New System.Drawing.Size(124, 157)
        Me.listshow.TabIndex = 16
        Me.listshow.Visible = False
        '
        'cmdPrint
        '
        Me.cmdPrint.Location = New System.Drawing.Point(676, 18)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(104, 23)
        Me.cmdPrint.TabIndex = 5
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label5.Location = New System.Drawing.Point(354, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 16)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Status:"
        '
        'cmdxl
        '
        Me.cmdxl.Location = New System.Drawing.Point(676, 134)
        Me.cmdxl.Name = "cmdxl"
        Me.cmdxl.Size = New System.Drawing.Size(104, 23)
        Me.cmdxl.TabIndex = 5
        Me.cmdxl.Text = "Group Modify"
        Me.cmdxl.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label4.Location = New System.Drawing.Point(22, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 16)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Year:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label3.Location = New System.Drawing.Point(183, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 16)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Location:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Location = New System.Drawing.Point(22, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 16)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Brand:"
        '
        'chkonOrder
        '
        Me.chkonOrder.AutoSize = True
        Me.chkonOrder.BackColor = System.Drawing.Color.Transparent
        Me.chkonOrder.ForeColor = System.Drawing.Color.MidnightBlue
        Me.chkonOrder.Location = New System.Drawing.Point(364, 174)
        Me.chkonOrder.Name = "chkonOrder"
        Me.chkonOrder.Size = New System.Drawing.Size(155, 20)
        Me.chkonOrder.TabIndex = 4
        Me.chkonOrder.Text = "Display Units on order"
        Me.chkonOrder.UseVisualStyleBackColor = False
        '
        'liststatus
        '
        Me.liststatus.CheckOnClick = True
        Me.liststatus.FormattingEnabled = True
        Me.liststatus.Location = New System.Drawing.Point(364, 28)
        Me.liststatus.Name = "liststatus"
        Me.liststatus.Size = New System.Drawing.Size(154, 140)
        Me.liststatus.TabIndex = 3
        '
        'ComboBrand
        '
        Me.ComboBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBrand.FormattingEnabled = True
        Me.ComboBrand.Location = New System.Drawing.Point(31, 31)
        Me.ComboBrand.Name = "ComboBrand"
        Me.ComboBrand.Size = New System.Drawing.Size(155, 24)
        Me.ComboBrand.TabIndex = 0
        '
        'listyear
        '
        Me.listyear.CheckOnClick = True
        Me.listyear.FormattingEnabled = True
        Me.listyear.Location = New System.Drawing.Point(32, 84)
        Me.listyear.Name = "listyear"
        Me.listyear.Size = New System.Drawing.Size(154, 38)
        Me.listyear.TabIndex = 2
        '
        'listStore
        '
        Me.listStore.CheckOnClick = True
        Me.listStore.FormattingEnabled = True
        Me.listStore.Location = New System.Drawing.Point(192, 28)
        Me.listStore.Name = "listStore"
        Me.listStore.Size = New System.Drawing.Size(154, 157)
        Me.listStore.TabIndex = 1
        '
        'txtloading
        '
        Me.txtloading.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtloading.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloading.Location = New System.Drawing.Point(0, 0)
        Me.txtloading.Name = "txtloading"
        Me.txtloading.ReadOnly = True
        Me.txtloading.Size = New System.Drawing.Size(792, 29)
        Me.txtloading.TabIndex = 2
        Me.txtloading.Text = "Downloading Data"
        Me.txtloading.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtloading.Visible = False
        '
        'DataView
        '
        Me.DataView.AllowUserToAddRows = False
        Me.DataView.AllowUserToDeleteRows = False
        Me.DataView.AllowUserToOrderColumns = True
        Me.DataView.AllowUserToResizeRows = False
        DataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle17
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle18
        Me.DataView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.btnEdit, Me.XLchk})
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataView.DefaultCellStyle = DataGridViewCellStyle19
        Me.DataView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataView.Location = New System.Drawing.Point(0, 0)
        Me.DataView.Name = "DataView"
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataView.RowHeadersDefaultCellStyle = DataGridViewCellStyle20
        Me.DataView.RowHeadersVisible = False
        Me.DataView.Size = New System.Drawing.Size(792, 397)
        Me.DataView.TabIndex = 0
        '
        'btnEdit
        '
        Me.btnEdit.HeaderText = "Details"
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.btnEdit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.btnEdit.Text = "Details"
        Me.btnEdit.UseColumnTextForButtonValue = True
        Me.btnEdit.Width = 5
        '
        'XLchk
        '
        Me.XLchk.HeaderText = "Edit?"
        Me.XLchk.Name = "XLchk"
        Me.XLchk.Width = 5
        '
        'frmInv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 598)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmInv"
        Me.Text = "MMS Online - Inventory"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents liststatus As System.Windows.Forms.CheckedListBox
    Friend WithEvents ComboBrand As System.Windows.Forms.ComboBox
    Friend WithEvents listyear As System.Windows.Forms.CheckedListBox
    Friend WithEvents listStore As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkonOrder As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdxl As System.Windows.Forms.Button
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents DataView As System.Windows.Forms.DataGridView
    Friend WithEvents btnEdit As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents XLchk As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents btnselectall As System.Windows.Forms.Button
    Friend WithEvents txtloading As System.Windows.Forms.TextBox
    Friend WithEvents listshow As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnunselall As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtunitfltr As System.Windows.Forms.TextBox
    Friend WithEvents txtserfltr As System.Windows.Forms.TextBox
    Friend WithEvents txtmdlflter As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnGo As System.Windows.Forms.Button
    Friend WithEvents BtnStatusNone As System.Windows.Forms.Button
    Friend WithEvents BtnAllStatus As System.Windows.Forms.Button
    Friend WithEvents BtnLocNone As System.Windows.Forms.Button
    Friend WithEvents BtnLocAll As System.Windows.Forms.Button
End Class
