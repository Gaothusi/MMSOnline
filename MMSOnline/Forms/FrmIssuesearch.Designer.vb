<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmIssuesearch
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
        Me.Label8 = New System.Windows.Forms.Label()
        Me.labelLeft = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnallcustomers = New System.Windows.Forms.Button()
        Me.btnallwarranty = New System.Windows.Forms.Button()
        Me.btnallstatuss = New System.Windows.Forms.Button()
        Me.btnalltypes = New System.Windows.Forms.Button()
        Me.btnAllstores = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.liststatus = New System.Windows.Forms.CheckedListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.listwork = New System.Windows.Forms.CheckedListBox()
        Me.btnpickcustomer = New System.Windows.Forms.Button()
        Me.cmdGo = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.listStore = New System.Windows.Forms.CheckedListBox()
        Me.txtWOnum = New System.Windows.Forms.TextBox()
        Me.txtname = New System.Windows.Forms.TextBox()
        Me.listwarranty = New System.Windows.Forms.CheckedListBox()
        Me.txtloading = New System.Windows.Forms.TextBox()
        Me.DataView = New System.Windows.Forms.DataGridView()
        Me.Edit = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.labelLeft)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PictureBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(792, 580)
        Me.SplitContainer1.SplitterDistance = 160
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 2
        Me.SplitContainer1.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label8.Location = New System.Drawing.Point(48, 78)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(92, 14)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Unassigned Work"
        '
        'labelLeft
        '
        Me.labelLeft.AutoSize = True
        Me.labelLeft.BackColor = System.Drawing.Color.Transparent
        Me.labelLeft.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelLeft.ForeColor = System.Drawing.Color.MidnightBlue
        Me.labelLeft.Location = New System.Drawing.Point(12, 52)
        Me.labelLeft.Name = "labelLeft"
        Me.labelLeft.Size = New System.Drawing.Size(128, 16)
        Me.labelLeft.TabIndex = 16
        Me.labelLeft.Text = "Common Searches"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnallcustomers)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnallwarranty)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnallstatuss)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnalltypes)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAllstores)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.liststatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.listwork)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnpickcustomer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmdGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.listStore)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtWOnum)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.listwarranty)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtloading)
        Me.SplitContainer2.Panel2.Controls.Add(Me.DataView)
        Me.SplitContainer2.Size = New System.Drawing.Size(631, 580)
        Me.SplitContainer2.SplitterDistance = 200
        Me.SplitContainer2.SplitterWidth = 1
        Me.SplitContainer2.TabIndex = 0
        '
        'btnallcustomers
        '
        Me.btnallcustomers.Location = New System.Drawing.Point(12, 97)
        Me.btnallcustomers.Name = "btnallcustomers"
        Me.btnallcustomers.Size = New System.Drawing.Size(93, 23)
        Me.btnallcustomers.TabIndex = 21
        Me.btnallcustomers.Text = "All Customers"
        Me.btnallcustomers.UseVisualStyleBackColor = True
        '
        'btnallwarranty
        '
        Me.btnallwarranty.Location = New System.Drawing.Point(584, 3)
        Me.btnallwarranty.Name = "btnallwarranty"
        Me.btnallwarranty.Size = New System.Drawing.Size(36, 23)
        Me.btnallwarranty.TabIndex = 20
        Me.btnallwarranty.Text = "All"
        Me.btnallwarranty.UseVisualStyleBackColor = True
        Me.btnallwarranty.Visible = False
        '
        'btnallstatuss
        '
        Me.btnallstatuss.Location = New System.Drawing.Point(471, 3)
        Me.btnallstatuss.Name = "btnallstatuss"
        Me.btnallstatuss.Size = New System.Drawing.Size(36, 23)
        Me.btnallstatuss.TabIndex = 19
        Me.btnallstatuss.Text = "All"
        Me.btnallstatuss.UseVisualStyleBackColor = True
        '
        'btnalltypes
        '
        Me.btnalltypes.Location = New System.Drawing.Point(358, 3)
        Me.btnalltypes.Name = "btnalltypes"
        Me.btnalltypes.Size = New System.Drawing.Size(36, 23)
        Me.btnalltypes.TabIndex = 18
        Me.btnalltypes.Text = "All"
        Me.btnalltypes.UseVisualStyleBackColor = True
        '
        'btnAllstores
        '
        Me.btnAllstores.Location = New System.Drawing.Point(245, 3)
        Me.btnAllstores.Name = "btnAllstores"
        Me.btnAllstores.Size = New System.Drawing.Size(36, 23)
        Me.btnAllstores.TabIndex = 17
        Me.btnAllstores.Text = "All"
        Me.btnAllstores.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(400, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Status"
        '
        'liststatus
        '
        Me.liststatus.CheckOnClick = True
        Me.liststatus.FormattingEnabled = True
        Me.liststatus.Items.AddRange(New Object() {"Reported", "To Be Assigned", "To Be Quoted", "Waiting on Warranty", "Assigned", "In Progress", "Completed", "Not Completed"})
        Me.liststatus.Location = New System.Drawing.Point(400, 29)
        Me.liststatus.Name = "liststatus"
        Me.liststatus.Size = New System.Drawing.Size(107, 154)
        Me.liststatus.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(287, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Type of work"
        '
        'listwork
        '
        Me.listwork.CheckOnClick = True
        Me.listwork.FormattingEnabled = True
        Me.listwork.Items.AddRange(New Object() {"Standard Service", "Standard Service (PREPAID)", "Rig - Sterndrive", "Rig - Outboard", "Rig - Jet", "Rig - V-Drive", "Gelcoat", "Major Mechanical", "Minor Mechanical", "Electronic", "Tower Install", "Other", "SUBLET", "Upholstery", "Metal Work"})
        Me.listwork.Location = New System.Drawing.Point(287, 29)
        Me.listwork.Name = "listwork"
        Me.listwork.Size = New System.Drawing.Size(107, 154)
        Me.listwork.TabIndex = 13
        '
        'btnpickcustomer
        '
        Me.btnpickcustomer.Location = New System.Drawing.Point(108, 97)
        Me.btnpickcustomer.Name = "btnpickcustomer"
        Me.btnpickcustomer.Size = New System.Drawing.Size(93, 23)
        Me.btnpickcustomer.TabIndex = 12
        Me.btnpickcustomer.Text = "Select Customer"
        Me.btnpickcustomer.UseVisualStyleBackColor = True
        '
        'cmdGo
        '
        Me.cmdGo.Location = New System.Drawing.Point(126, 160)
        Me.cmdGo.Name = "cmdGo"
        Me.cmdGo.Size = New System.Drawing.Size(75, 23)
        Me.cmdGo.TabIndex = 8
        Me.cmdGo.Text = "Search"
        Me.cmdGo.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(513, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Warranty"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(207, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Store"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(155, 25)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Search Criteria"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Work Order #:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "By Customer:"
        '
        'listStore
        '
        Me.listStore.CheckOnClick = True
        Me.listStore.FormattingEnabled = True
        Me.listStore.Location = New System.Drawing.Point(207, 29)
        Me.listStore.Name = "listStore"
        Me.listStore.Size = New System.Drawing.Size(74, 154)
        Me.listStore.TabIndex = 6
        '
        'txtWOnum
        '
        Me.txtWOnum.Location = New System.Drawing.Point(90, 45)
        Me.txtWOnum.Name = "txtWOnum"
        Me.txtWOnum.Size = New System.Drawing.Size(111, 20)
        Me.txtWOnum.TabIndex = 1
        Me.txtWOnum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtname
        '
        Me.txtname.Enabled = False
        Me.txtname.Location = New System.Drawing.Point(90, 71)
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(111, 20)
        Me.txtname.TabIndex = 0
        Me.txtname.Text = "ALL"
        Me.txtname.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'listwarranty
        '
        Me.listwarranty.CheckOnClick = True
        Me.listwarranty.FormattingEnabled = True
        Me.listwarranty.Items.AddRange(New Object() {"Pending", "Approved", "Submited", "Denied"})
        Me.listwarranty.Location = New System.Drawing.Point(513, 29)
        Me.listwarranty.Name = "listwarranty"
        Me.listwarranty.Size = New System.Drawing.Size(107, 154)
        Me.listwarranty.TabIndex = 7
        '
        'txtloading
        '
        Me.txtloading.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtloading.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloading.Location = New System.Drawing.Point(0, 0)
        Me.txtloading.Name = "txtloading"
        Me.txtloading.ReadOnly = True
        Me.txtloading.Size = New System.Drawing.Size(631, 29)
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
        Me.DataView.Size = New System.Drawing.Size(631, 379)
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
        Me.Timer2.Interval = 60000
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 5000
        '
        'FrmIssuesearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 580)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmIssuesearch"
        Me.Text = "Search Issues"
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
    Friend WithEvents listwarranty As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtloading As System.Windows.Forms.TextBox
    Friend WithEvents DataView As System.Windows.Forms.DataGridView
    Friend WithEvents Edit As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnpickcustomer As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents liststatus As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents listwork As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnAllstores As System.Windows.Forms.Button
    Friend WithEvents btnallwarranty As System.Windows.Forms.Button
    Friend WithEvents btnallstatuss As System.Windows.Forms.Button
    Friend WithEvents btnalltypes As System.Windows.Forms.Button
    Friend WithEvents btnallcustomers As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents labelLeft As System.Windows.Forms.Label
End Class
