<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmxlsheet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmxlsheet))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.AxSpreadsheet1 = New AxMicrosoft.Office.Interop.Owc11.AxSpreadsheet
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.AxSpreadsheet1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.AxSpreadsheet1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1116, 757)
        Me.SplitContainer1.SplitterDistance = 29
        Me.SplitContainer1.TabIndex = 0
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(128, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(129, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Autosize Columns"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(110, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Save to Server"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'AxSpreadsheet1
        '
        Me.AxSpreadsheet1.DataSource = Nothing
        Me.AxSpreadsheet1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxSpreadsheet1.Enabled = True
        Me.AxSpreadsheet1.Location = New System.Drawing.Point(0, 0)
        Me.AxSpreadsheet1.Name = "AxSpreadsheet1"
        Me.AxSpreadsheet1.OcxState = CType(resources.GetObject("AxSpreadsheet1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSpreadsheet1.Size = New System.Drawing.Size(0, 0)
        Me.AxSpreadsheet1.TabIndex = 0
        '
        'frmxlsheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1116, 757)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmxlsheet"
        Me.Text = "Inventory Data"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.AxSpreadsheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents AxSpreadsheet1 As AxMicrosoft.Office.Interop.Owc11.AxSpreadsheet
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
