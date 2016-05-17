<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOldCallLogByCustomer
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
        Me.Label28 = New System.Windows.Forms.Label()
        Me.DVCalls = New System.Windows.Forms.DataGridView()
        Me.View = New System.Windows.Forms.DataGridViewButtonColumn()
        CType(Me.DVCalls, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(12, 9)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(83, 24)
        Me.Label28.TabIndex = 48
        Me.Label28.Text = "Call Log:"
        '
        'DVCalls
        '
        Me.DVCalls.AllowUserToAddRows = False
        Me.DVCalls.AllowUserToDeleteRows = False
        Me.DVCalls.AllowUserToResizeRows = False
        Me.DVCalls.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DVCalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DVCalls.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.View})
        Me.DVCalls.Location = New System.Drawing.Point(12, 36)
        Me.DVCalls.Name = "DVCalls"
        Me.DVCalls.ReadOnly = True
        Me.DVCalls.RowHeadersVisible = False
        Me.DVCalls.Size = New System.Drawing.Size(430, 275)
        Me.DVCalls.TabIndex = 47
        '
        'View
        '
        Me.View.HeaderText = "View"
        Me.View.Name = "View"
        Me.View.ReadOnly = True
        Me.View.Text = "View"
        Me.View.Visible = False
        '
        'frmOldCallLogByCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(454, 323)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.DVCalls)
        Me.Name = "frmOldCallLogByCustomer"
        Me.Text = "Call Log By Customer"
        CType(Me.DVCalls, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents DVCalls As System.Windows.Forms.DataGridView
    Friend WithEvents View As System.Windows.Forms.DataGridViewButtonColumn
End Class
