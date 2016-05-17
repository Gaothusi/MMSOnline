<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportSelection
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
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.rbTechHours = New System.Windows.Forms.RadioButton()
        Me.rbTechHoursShop = New System.Windows.Forms.RadioButton()
        Me.rbBOSInventory = New System.Windows.Forms.RadioButton()
        Me.rbTechHoursBillable = New System.Windows.Forms.RadioButton()
        Me.rbTireTax = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton1.Location = New System.Drawing.Point(46, 109)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(207, 20)
        Me.RadioButton1.TabIndex = 1
        Me.RadioButton1.Text = "Outstanding Finance Contracts"
        Me.RadioButton1.UseVisualStyleBackColor = False
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton2.Location = New System.Drawing.Point(46, 226)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(245, 20)
        Me.RadioButton2.TabIndex = 5
        Me.RadioButton2.Text = "All Active Deals - Ordered boats only"
        Me.RadioButton2.UseVisualStyleBackColor = False
        Me.RadioButton2.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(386, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Please Select Which Type of Report you would like to generate:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(152, 263)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(101, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Generate"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton3.Location = New System.Drawing.Point(46, 239)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(199, 20)
        Me.RadioButton3.TabIndex = 6
        Me.RadioButton3.Text = "All Active Deals - Small Items"
        Me.RadioButton3.UseVisualStyleBackColor = False
        Me.RadioButton3.Visible = False
        '
        'rbTechHours
        '
        Me.rbTechHours.AutoSize = True
        Me.rbTechHours.BackColor = System.Drawing.Color.Transparent
        Me.rbTechHours.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbTechHours.Location = New System.Drawing.Point(46, 196)
        Me.rbTechHours.Name = "rbTechHours"
        Me.rbTechHours.Size = New System.Drawing.Size(143, 20)
        Me.rbTechHours.TabIndex = 2
        Me.rbTechHours.Text = "Tech Hours - Actual"
        Me.rbTechHours.UseVisualStyleBackColor = False
        Me.rbTechHours.Visible = False
        '
        'rbTechHoursShop
        '
        Me.rbTechHoursShop.AutoSize = True
        Me.rbTechHoursShop.BackColor = System.Drawing.Color.Transparent
        Me.rbTechHoursShop.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbTechHoursShop.Location = New System.Drawing.Point(46, 210)
        Me.rbTechHoursShop.Name = "rbTechHoursShop"
        Me.rbTechHoursShop.Size = New System.Drawing.Size(172, 20)
        Me.rbTechHoursShop.TabIndex = 4
        Me.rbTechHoursShop.Text = "Tech Hours - Shop Time"
        Me.rbTechHoursShop.UseVisualStyleBackColor = False
        Me.rbTechHoursShop.Visible = False
        '
        'rbBOSInventory
        '
        Me.rbBOSInventory.AutoSize = True
        Me.rbBOSInventory.BackColor = System.Drawing.Color.Transparent
        Me.rbBOSInventory.Checked = True
        Me.rbBOSInventory.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbBOSInventory.Location = New System.Drawing.Point(46, 83)
        Me.rbBOSInventory.Name = "rbBOSInventory"
        Me.rbBOSInventory.Size = New System.Drawing.Size(181, 20)
        Me.rbBOSInventory.TabIndex = 0
        Me.rbBOSInventory.TabStop = True
        Me.rbBOSInventory.Text = "BOS and Inventory Joined"
        Me.rbBOSInventory.UseVisualStyleBackColor = False
        '
        'rbTechHoursBillable
        '
        Me.rbTechHoursBillable.AutoSize = True
        Me.rbTechHoursBillable.BackColor = System.Drawing.Color.Transparent
        Me.rbTechHoursBillable.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbTechHoursBillable.Location = New System.Drawing.Point(46, 135)
        Me.rbTechHoursBillable.Name = "rbTechHoursBillable"
        Me.rbTechHoursBillable.Size = New System.Drawing.Size(221, 20)
        Me.rbTechHoursBillable.TabIndex = 3
        Me.rbTechHoursBillable.Text = "Tech Hours - Billable/Shop Time"
        Me.rbTechHoursBillable.UseVisualStyleBackColor = False
        '
        'rbTireTax
        '
        Me.rbTireTax.AutoSize = True
        Me.rbTireTax.BackColor = System.Drawing.Color.Transparent
        Me.rbTireTax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbTireTax.Location = New System.Drawing.Point(46, 161)
        Me.rbTireTax.Name = "rbTireTax"
        Me.rbTireTax.Size = New System.Drawing.Size(136, 20)
        Me.rbTireTax.TabIndex = 8
        Me.rbTireTax.Text = "Tire Tax Collected"
        Me.rbTireTax.UseVisualStyleBackColor = False
        '
        'frmReportSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.MMSOnline.My.Resources.Resources.BkgSquare
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(409, 313)
        Me.Controls.Add(Me.rbTireTax)
        Me.Controls.Add(Me.rbTechHoursBillable)
        Me.Controls.Add(Me.rbBOSInventory)
        Me.Controls.Add(Me.rbTechHoursShop)
        Me.Controls.Add(Me.rbTechHours)
        Me.Controls.Add(Me.RadioButton3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Name = "frmReportSelection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report Selection"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbTechHours As System.Windows.Forms.RadioButton
    Friend WithEvents rbTechHoursShop As System.Windows.Forms.RadioButton
    Friend WithEvents rbBOSInventory As System.Windows.Forms.RadioButton
    Friend WithEvents rbTechHoursBillable As System.Windows.Forms.RadioButton
    Friend WithEvents rbTireTax As System.Windows.Forms.RadioButton
End Class
