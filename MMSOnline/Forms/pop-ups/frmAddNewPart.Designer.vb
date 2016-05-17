<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddNewPart
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
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPartNum = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.txtConfirmationNum = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblOrderStatus = New System.Windows.Forms.Label()
        Me.cboOrderStatus = New System.Windows.Forms.ComboBox()
        Me.cboSupplier = New System.Windows.Forms.ComboBox()
        Me.cboPaymentType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtQuantity
        '
        Me.txtQuantity.Location = New System.Drawing.Point(223, 143)
        Me.txtQuantity.Margin = New System.Windows.Forms.Padding(4)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(225, 22)
        Me.txtQuantity.TabIndex = 4
        Me.txtQuantity.Text = "1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(128, 145)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 24)
        Me.Label4.TabIndex = 106
        Me.Label4.Text = "Quantity:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(99, 17)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 24)
        Me.Label3.TabIndex = 105
        Me.Label3.Text = "Description:"
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(223, 15)
        Me.txtDesc.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(225, 22)
        Me.txtDesc.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(128, 49)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 24)
        Me.Label2.TabIndex = 104
        Me.Label2.Text = "Supplier:"
        '
        'txtSupplier
        '
        Me.txtSupplier.Location = New System.Drawing.Point(223, 47)
        Me.txtSupplier.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.Size = New System.Drawing.Size(225, 22)
        Me.txtSupplier.TabIndex = 1
        Me.txtSupplier.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(87, 81)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 24)
        Me.Label1.TabIndex = 103
        Me.Label1.Text = "Part Number:"
        '
        'txtPartNum
        '
        Me.txtPartNum.Location = New System.Drawing.Point(223, 79)
        Me.txtPartNum.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPartNum.Name = "txtPartNum"
        Me.txtPartNum.Size = New System.Drawing.Size(225, 22)
        Me.txtPartNum.TabIndex = 2
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(53, 113)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(151, 24)
        Me.Label22.TabIndex = 102
        Me.Label22.Text = "Part Price (retail):"
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(223, 111)
        Me.txtPrice.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(225, 22)
        Me.txtPrice.TabIndex = 3
        '
        'OK_Button
        '
        Me.OK_Button.Location = New System.Drawing.Point(223, 283)
        Me.OK_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(89, 28)
        Me.OK_Button.TabIndex = 7
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(360, 283)
        Me.Cancel_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(89, 28)
        Me.Cancel_Button.TabIndex = 8
        Me.Cancel_Button.Text = "Cancel"
        '
        'txtConfirmationNum
        '
        Me.txtConfirmationNum.Location = New System.Drawing.Point(223, 175)
        Me.txtConfirmationNum.Margin = New System.Windows.Forms.Padding(4)
        Me.txtConfirmationNum.Name = "txtConfirmationNum"
        Me.txtConfirmationNum.Size = New System.Drawing.Size(225, 22)
        Me.txtConfirmationNum.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(69, 177)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(135, 24)
        Me.Label5.TabIndex = 108
        Me.Label5.Text = "Confirmation #:"
        '
        'lblOrderStatus
        '
        Me.lblOrderStatus.AutoSize = True
        Me.lblOrderStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderStatus.Location = New System.Drawing.Point(87, 209)
        Me.lblOrderStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblOrderStatus.Name = "lblOrderStatus"
        Me.lblOrderStatus.Size = New System.Drawing.Size(119, 24)
        Me.lblOrderStatus.TabIndex = 110
        Me.lblOrderStatus.Text = "Order Status:"
        '
        'cboOrderStatus
        '
        Me.cboOrderStatus.FormattingEnabled = True
        Me.cboOrderStatus.Items.AddRange(New Object() {"Quote", "To Be Ordered", "Ordered", "Back Ordered", "Received", "Picked Up", "Placed In Stock", "Installed"})
        Me.cboOrderStatus.Location = New System.Drawing.Point(223, 207)
        Me.cboOrderStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.cboOrderStatus.Name = "cboOrderStatus"
        Me.cboOrderStatus.Size = New System.Drawing.Size(225, 24)
        Me.cboOrderStatus.TabIndex = 6
        '
        'cboSupplier
        '
        Me.cboSupplier.FormattingEnabled = True
        Me.cboSupplier.Items.AddRange(New Object() {"", "Bayliner", "BRP Rotax", "Centurion", "EZ Loader", "Fishn Hole", "Four Winns", "Glastron", "Hewescraft", "Indmar", "Karavan", "Land n Sea", "Mercury", "Monster", "PCM", "Riverhawk", "Roswell", "Rotax", "Sanger", "Scarab", "Sea Ray", "Shorelander", "Smoker / Sylvan", "Volvo", "Western", "Other"})
        Me.cboSupplier.Location = New System.Drawing.Point(223, 47)
        Me.cboSupplier.Margin = New System.Windows.Forms.Padding(4)
        Me.cboSupplier.Name = "cboSupplier"
        Me.cboSupplier.Size = New System.Drawing.Size(225, 24)
        Me.cboSupplier.TabIndex = 1
        '
        'cboPaymentType
        '
        Me.cboPaymentType.FormattingEnabled = True
        Me.cboPaymentType.Items.AddRange(New Object() {"", "Customer", "Warranty", "Bill Of Sale", "Work Order", "Stock"})
        Me.cboPaymentType.Location = New System.Drawing.Point(223, 240)
        Me.cboPaymentType.Margin = New System.Windows.Forms.Padding(4)
        Me.cboPaymentType.Name = "cboPaymentType"
        Me.cboPaymentType.Size = New System.Drawing.Size(225, 24)
        Me.cboPaymentType.TabIndex = 111
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(73, 244)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(136, 24)
        Me.Label6.TabIndex = 112
        Me.Label6.Text = "Payment Type:"
        '
        'frmAddNewPart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 347)
        Me.Controls.Add(Me.cboPaymentType)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboSupplier)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.cboOrderStatus)
        Me.Controls.Add(Me.lblOrderStatus)
        Me.Controls.Add(Me.txtConfirmationNum)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtQuantity)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtSupplier)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPartNum)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.txtPrice)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmAddNewPart"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Part Info"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtQuantity As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSupplier As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPartNum As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents txtConfirmationNum As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblOrderStatus As System.Windows.Forms.Label
    Friend WithEvents cboOrderStatus As System.Windows.Forms.ComboBox
    Friend WithEvents cboSupplier As System.Windows.Forms.ComboBox
    Friend WithEvents cboPaymentType As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
