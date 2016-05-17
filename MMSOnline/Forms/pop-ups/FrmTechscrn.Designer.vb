<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTechScrb
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
        Me.PrintMyTime = New System.Windows.Forms.Label()
        Me.ShowHideMain = New System.Windows.Forms.Label()
        Me.Refreshbtn = New System.Windows.Forms.Button()
        Me.liststore = New System.Windows.Forms.ListBox()
        Me.listtech = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtloading = New System.Windows.Forms.TextBox()
        Me.DataView = New System.Windows.Forms.DataGridView()
        Me.View = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
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
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Window
        Me.SplitContainer1.Panel1.BackgroundImage = Global.MMSOnline.My.Resources.Resources.BkgTL
        Me.SplitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SplitContainer1.Panel1.Controls.Add(Me.PrintMyTime)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ShowHideMain)
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
        Me.SplitContainer1.Size = New System.Drawing.Size(1056, 714)
        Me.SplitContainer1.SplitterDistance = 161
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 0
        '
        'PrintMyTime
        '
        Me.PrintMyTime.AutoSize = True
        Me.PrintMyTime.BackColor = System.Drawing.Color.Transparent
        Me.PrintMyTime.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrintMyTime.ForeColor = System.Drawing.Color.MidnightBlue
        Me.PrintMyTime.Location = New System.Drawing.Point(4, 86)
        Me.PrintMyTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PrintMyTime.Name = "PrintMyTime"
        Me.PrintMyTime.Size = New System.Drawing.Size(116, 19)
        Me.PrintMyTime.TabIndex = 21
        Me.PrintMyTime.Text = "Print My Time"
        '
        'ShowHideMain
        '
        Me.ShowHideMain.AutoSize = True
        Me.ShowHideMain.BackColor = System.Drawing.Color.Transparent
        Me.ShowHideMain.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ShowHideMain.ForeColor = System.Drawing.Color.MidnightBlue
        Me.ShowHideMain.Location = New System.Drawing.Point(4, 58)
        Me.ShowHideMain.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ShowHideMain.Name = "ShowHideMain"
        Me.ShowHideMain.Size = New System.Drawing.Size(178, 19)
        Me.ShowHideMain.TabIndex = 19
        Me.ShowHideMain.Text = "Show/Hide Main Page"
        '
        'Refreshbtn
        '
        Me.Refreshbtn.Location = New System.Drawing.Point(21, 671)
        Me.Refreshbtn.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Refreshbtn.Name = "Refreshbtn"
        Me.Refreshbtn.Size = New System.Drawing.Size(160, 28)
        Me.Refreshbtn.TabIndex = 20
        Me.Refreshbtn.Text = "Refresh"
        Me.Refreshbtn.UseVisualStyleBackColor = True
        Me.Refreshbtn.Visible = False
        '
        'liststore
        '
        Me.liststore.FormattingEnabled = True
        Me.liststore.ItemHeight = 16
        Me.liststore.Location = New System.Drawing.Point(21, 222)
        Me.liststore.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.liststore.Name = "liststore"
        Me.liststore.Size = New System.Drawing.Size(159, 196)
        Me.liststore.TabIndex = 19
        Me.liststore.Visible = False
        '
        'listtech
        '
        Me.listtech.FormattingEnabled = True
        Me.listtech.ItemHeight = 16
        Me.listtech.Location = New System.Drawing.Point(21, 453)
        Me.listtech.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.listtech.Name = "listtech"
        Me.listtech.Size = New System.Drawing.Size(159, 212)
        Me.listtech.TabIndex = 18
        Me.listtech.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 425)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 25)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Tech:"
        Me.Label1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 193)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 25)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Store:"
        Me.Label2.Visible = False
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
        'txtloading
        '
        Me.txtloading.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtloading.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloading.Location = New System.Drawing.Point(0, 0)
        Me.txtloading.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtloading.Name = "txtloading"
        Me.txtloading.ReadOnly = True
        Me.txtloading.Size = New System.Drawing.Size(890, 34)
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
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray
        Me.DataView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.DataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.View})
        Me.DataView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataView.GridColor = System.Drawing.SystemColors.ControlLightLight
        Me.DataView.Location = New System.Drawing.Point(0, 0)
        Me.DataView.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataView.Name = "DataView"
        Me.DataView.ReadOnly = True
        Me.DataView.RowHeadersVisible = False
        Me.DataView.Size = New System.Drawing.Size(890, 714)
        Me.DataView.TabIndex = 2
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
        'Timer1
        '
        Me.Timer1.Interval = 120000
        '
        'FrmTechScrb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1056, 714)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "FrmTechScrb"
        Me.Text = " "
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents txtloading As System.Windows.Forms.TextBox
    Friend WithEvents DataView As System.Windows.Forms.DataGridView
    Friend WithEvents listtech As System.Windows.Forms.ListBox
    Friend WithEvents liststore As System.Windows.Forms.ListBox
    Friend WithEvents Refreshbtn As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents View As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents ShowHideMain As System.Windows.Forms.Label
    Friend WithEvents PrintMyTime As System.Windows.Forms.Label
End Class
