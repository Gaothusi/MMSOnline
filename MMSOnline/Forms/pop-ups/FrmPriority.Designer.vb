<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPriority
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPriority))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Refreshbtn = New System.Windows.Forms.Button()
        Me.liststore = New System.Windows.Forms.ListBox()
        Me.listtech = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtloading = New System.Windows.Forms.TextBox()
        Me.DataView = New System.Windows.Forms.DataGridView()
        Me.Up = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Down = New System.Windows.Forms.DataGridViewImageColumn()
        Me.View = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.Window
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Refreshbtn)
        Me.SplitContainer1.Panel1.Controls.Add(Me.liststore)
        Me.SplitContainer1.Panel1.Controls.Add(Me.listtech)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PictureBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtloading)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataView)
        Me.SplitContainer1.Size = New System.Drawing.Size(792, 580)
        Me.SplitContainer1.SplitterDistance = 161
        Me.SplitContainer1.TabIndex = 0
        '
        'Refreshbtn
        '
        Me.Refreshbtn.Location = New System.Drawing.Point(16, 485)
        Me.Refreshbtn.Name = "Refreshbtn"
        Me.Refreshbtn.Size = New System.Drawing.Size(120, 23)
        Me.Refreshbtn.TabIndex = 20
        Me.Refreshbtn.Text = "Refresh"
        Me.Refreshbtn.UseVisualStyleBackColor = True
        '
        'liststore
        '
        Me.liststore.FormattingEnabled = True
        Me.liststore.Location = New System.Drawing.Point(16, 104)
        Me.liststore.Name = "liststore"
        Me.liststore.Size = New System.Drawing.Size(120, 160)
        Me.liststore.TabIndex = 19
        '
        'listtech
        '
        Me.listtech.FormattingEnabled = True
        Me.listtech.Location = New System.Drawing.Point(16, 292)
        Me.listtech.Name = "listtech"
        Me.listtech.Size = New System.Drawing.Size(120, 173)
        Me.listtech.TabIndex = 18
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 269)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 20)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Tech:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 20)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Store:"
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
        'txtloading
        '
        Me.txtloading.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtloading.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloading.Location = New System.Drawing.Point(0, 0)
        Me.txtloading.Name = "txtloading"
        Me.txtloading.ReadOnly = True
        Me.txtloading.Size = New System.Drawing.Size(627, 29)
        Me.txtloading.TabIndex = 3
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
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray
        Me.DataView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.DataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Up, Me.Down, Me.View})
        Me.DataView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataView.GridColor = System.Drawing.SystemColors.ControlLightLight
        Me.DataView.Location = New System.Drawing.Point(0, 0)
        Me.DataView.Name = "DataView"
        Me.DataView.ReadOnly = True
        Me.DataView.RowHeadersVisible = False
        Me.DataView.Size = New System.Drawing.Size(627, 580)
        Me.DataView.TabIndex = 2
        '
        'Up
        '
        Me.Up.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Up.HeaderText = "Up"
        Me.Up.Image = CType(resources.GetObject("Up.Image"), System.Drawing.Image)
        Me.Up.MinimumWidth = 15
        Me.Up.Name = "Up"
        Me.Up.ReadOnly = True
        Me.Up.Width = 15
        '
        'Down
        '
        Me.Down.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Down.HeaderText = "Down"
        Me.Down.Image = CType(resources.GetObject("Down.Image"), System.Drawing.Image)
        Me.Down.MinimumWidth = 15
        Me.Down.Name = "Down"
        Me.Down.ReadOnly = True
        Me.Down.Width = 15
        '
        'View
        '
        Me.View.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.View.HeaderText = "View"
        Me.View.MinimumWidth = 25
        Me.View.Name = "View"
        Me.View.ReadOnly = True
        Me.View.Width = 25
        '
        'FrmPriority
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 580)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmPriority"
        Me.Text = "Assigned Work"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents txtloading As System.Windows.Forms.TextBox
    Friend WithEvents DataView As System.Windows.Forms.DataGridView
    Friend WithEvents liststore As System.Windows.Forms.ListBox
    Friend WithEvents Refreshbtn As System.Windows.Forms.Button
    Friend WithEvents Up As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Down As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents View As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents listtech As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
