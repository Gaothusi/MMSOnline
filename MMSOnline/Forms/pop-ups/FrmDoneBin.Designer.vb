﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDoneBin
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.DataView = New System.Windows.Forms.DataGridView
        Me.View = New System.Windows.Forms.DataGridViewButtonColumn
        Me.Refreshbtn = New System.Windows.Forms.Button
        Me.liststore = New System.Windows.Forms.ListBox
        Me.txtloading = New System.Windows.Forms.TextBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.Label2 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        CType(Me.DataView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataView
        '
        Me.DataView.AllowUserToAddRows = False
        Me.DataView.AllowUserToDeleteRows = False
        Me.DataView.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray
        Me.DataView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.DataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.View})
        Me.DataView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataView.GridColor = System.Drawing.SystemColors.ControlLightLight
        Me.DataView.Location = New System.Drawing.Point(0, 0)
        Me.DataView.Name = "DataView"
        Me.DataView.ReadOnly = True
        Me.DataView.RowHeadersVisible = False
        Me.DataView.Size = New System.Drawing.Size(619, 562)
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
        Me.liststore.Location = New System.Drawing.Point(16, 319)
        Me.liststore.Name = "liststore"
        Me.liststore.Size = New System.Drawing.Size(120, 160)
        Me.liststore.TabIndex = 19
        '
        'txtloading
        '
        Me.txtloading.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtloading.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloading.Location = New System.Drawing.Point(0, 0)
        Me.txtloading.Name = "txtloading"
        Me.txtloading.ReadOnly = True
        Me.txtloading.Size = New System.Drawing.Size(619, 29)
        Me.txtloading.TabIndex = 3
        Me.txtloading.Text = "Downloading Data"
        Me.txtloading.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtloading.Visible = False
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PictureBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtloading)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataView)
        Me.SplitContainer1.Size = New System.Drawing.Size(784, 562)
        Me.SplitContainer1.SplitterDistance = 161
        Me.SplitContainer1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 296)
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
        'FrmDoneBin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmDoneBin"
        Me.Text = "Completed Work to be priced"
        CType(Me.DataView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataView As System.Windows.Forms.DataGridView
    Friend WithEvents Refreshbtn As System.Windows.Forms.Button
    Friend WithEvents liststore As System.Windows.Forms.ListBox
    Friend WithEvents txtloading As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents View As System.Windows.Forms.DataGridViewButtonColumn
End Class
