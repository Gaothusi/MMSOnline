<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuickPaymentCalc
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.TxtRate = New System.Windows.Forms.TextBox
        Me.TxtAmm = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ChkAutomode = New System.Windows.Forms.CheckBox
        Me.TxtPrice = New System.Windows.Forms.TextBox
        Me.TxtPmt = New System.Windows.Forms.TextBox
        Me.TxtGST = New System.Windows.Forms.TextBox
        Me.TxtPST = New System.Windows.Forms.TextBox
        Me.TxtFee = New System.Windows.Forms.TextBox
        Me.TxtIns = New System.Windows.Forms.TextBox
        Me.TxtTotal = New System.Windows.Forms.TextBox
        Me.BtnClipboard = New System.Windows.Forms.Button
        Me.ChkGST = New System.Windows.Forms.CheckBox
        Me.ChkPST = New System.Windows.Forms.CheckBox
        Me.ChkFee = New System.Windows.Forms.CheckBox
        Me.ChkIns = New System.Windows.Forms.CheckBox
        Me.TXTdown = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.ChkGSTdown = New System.Windows.Forms.CheckBox
        Me.txtAdds = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(150, 391)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        Me.OK_Button.Visible = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Done"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(90, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Price"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(109, 186)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "GST"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(112, 212)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 20)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "PST"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(71, 264)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 20)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Insurance"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(32, 238)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(119, 20)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Add Admin Fee"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(37, 290)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(114, 20)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Total Financed"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(73, 322)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 20)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Payment"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(9, 41)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 20)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Rate:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(119, 41)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(102, 20)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "Amortization:"
        '
        'TxtRate
        '
        Me.TxtRate.Location = New System.Drawing.Point(64, 40)
        Me.TxtRate.Name = "TxtRate"
        Me.TxtRate.Size = New System.Drawing.Size(38, 20)
        Me.TxtRate.TabIndex = 12
        '
        'TxtAmm
        '
        Me.TxtAmm.Location = New System.Drawing.Point(227, 40)
        Me.TxtAmm.Name = "TxtAmm"
        Me.TxtAmm.Size = New System.Drawing.Size(23, 20)
        Me.TxtAmm.TabIndex = 13
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(254, 46)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(36, 15)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "years"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox1.Controls.Add(Me.ChkAutomode)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.TxtAmm)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.TxtRate)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(302, 69)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        '
        'ChkAutomode
        '
        Me.ChkAutomode.AutoSize = True
        Me.ChkAutomode.Checked = True
        Me.ChkAutomode.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkAutomode.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.ChkAutomode.Location = New System.Drawing.Point(13, 17)
        Me.ChkAutomode.Name = "ChkAutomode"
        Me.ChkAutomode.Size = New System.Drawing.Size(56, 21)
        Me.ChkAutomode.TabIndex = 15
        Me.ChkAutomode.Text = "Auto"
        Me.ChkAutomode.UseVisualStyleBackColor = True
        '
        'TxtPrice
        '
        Me.TxtPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPrice.Location = New System.Drawing.Point(140, 75)
        Me.TxtPrice.Name = "TxtPrice"
        Me.TxtPrice.Size = New System.Drawing.Size(84, 26)
        Me.TxtPrice.TabIndex = 16
        '
        'TxtPmt
        '
        Me.TxtPmt.Enabled = False
        Me.TxtPmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPmt.Location = New System.Drawing.Point(157, 316)
        Me.TxtPmt.Name = "TxtPmt"
        Me.TxtPmt.Size = New System.Drawing.Size(67, 26)
        Me.TxtPmt.TabIndex = 17
        '
        'TxtGST
        '
        Me.TxtGST.Enabled = False
        Me.TxtGST.Location = New System.Drawing.Point(157, 186)
        Me.TxtGST.Name = "TxtGST"
        Me.TxtGST.Size = New System.Drawing.Size(67, 20)
        Me.TxtGST.TabIndex = 18
        '
        'TxtPST
        '
        Me.TxtPST.Enabled = False
        Me.TxtPST.Location = New System.Drawing.Point(157, 212)
        Me.TxtPST.Name = "TxtPST"
        Me.TxtPST.Size = New System.Drawing.Size(67, 20)
        Me.TxtPST.TabIndex = 19
        '
        'TxtFee
        '
        Me.TxtFee.Enabled = False
        Me.TxtFee.Location = New System.Drawing.Point(157, 238)
        Me.TxtFee.Name = "TxtFee"
        Me.TxtFee.Size = New System.Drawing.Size(67, 20)
        Me.TxtFee.TabIndex = 20
        '
        'TxtIns
        '
        Me.TxtIns.Enabled = False
        Me.TxtIns.Location = New System.Drawing.Point(157, 264)
        Me.TxtIns.Name = "TxtIns"
        Me.TxtIns.Size = New System.Drawing.Size(67, 20)
        Me.TxtIns.TabIndex = 21
        '
        'TxtTotal
        '
        Me.TxtTotal.Enabled = False
        Me.TxtTotal.Location = New System.Drawing.Point(157, 290)
        Me.TxtTotal.Name = "TxtTotal"
        Me.TxtTotal.Size = New System.Drawing.Size(67, 20)
        Me.TxtTotal.TabIndex = 22
        '
        'BtnClipboard
        '
        Me.BtnClipboard.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BtnClipboard.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnClipboard.Location = New System.Drawing.Point(16, 319)
        Me.BtnClipboard.Name = "BtnClipboard"
        Me.BtnClipboard.Size = New System.Drawing.Size(47, 23)
        Me.BtnClipboard.TabIndex = 23
        Me.BtnClipboard.Text = "Copy"
        '
        'ChkGST
        '
        Me.ChkGST.AutoSize = True
        Me.ChkGST.Checked = True
        Me.ChkGST.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkGST.Location = New System.Drawing.Point(230, 191)
        Me.ChkGST.Name = "ChkGST"
        Me.ChkGST.Size = New System.Drawing.Size(15, 14)
        Me.ChkGST.TabIndex = 24
        Me.ChkGST.UseVisualStyleBackColor = True
        '
        'ChkPST
        '
        Me.ChkPST.AutoSize = True
        Me.ChkPST.Location = New System.Drawing.Point(230, 217)
        Me.ChkPST.Name = "ChkPST"
        Me.ChkPST.Size = New System.Drawing.Size(15, 14)
        Me.ChkPST.TabIndex = 25
        Me.ChkPST.UseVisualStyleBackColor = True
        '
        'ChkFee
        '
        Me.ChkFee.AutoSize = True
        Me.ChkFee.Checked = True
        Me.ChkFee.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkFee.Location = New System.Drawing.Point(230, 243)
        Me.ChkFee.Name = "ChkFee"
        Me.ChkFee.Size = New System.Drawing.Size(15, 14)
        Me.ChkFee.TabIndex = 26
        Me.ChkFee.UseVisualStyleBackColor = True
        '
        'ChkIns
        '
        Me.ChkIns.AutoSize = True
        Me.ChkIns.Checked = True
        Me.ChkIns.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkIns.Location = New System.Drawing.Point(230, 269)
        Me.ChkIns.Name = "ChkIns"
        Me.ChkIns.Size = New System.Drawing.Size(15, 14)
        Me.ChkIns.TabIndex = 27
        Me.ChkIns.UseVisualStyleBackColor = True
        '
        'TXTdown
        '
        Me.TXTdown.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTdown.Location = New System.Drawing.Point(140, 148)
        Me.TXTdown.Name = "TXTdown"
        Me.TXTdown.Size = New System.Drawing.Size(84, 26)
        Me.TXTdown.TabIndex = 29
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(18, 154)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(116, 20)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "Down Payment"
        '
        'ChkGSTdown
        '
        Me.ChkGSTdown.AutoSize = True
        Me.ChkGSTdown.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.ChkGSTdown.Location = New System.Drawing.Point(230, 153)
        Me.ChkGSTdown.Name = "ChkGSTdown"
        Me.ChkGSTdown.Size = New System.Drawing.Size(77, 17)
        Me.ChkGSTdown.TabIndex = 30
        Me.ChkGSTdown.Text = "GST down"
        Me.ChkGSTdown.UseVisualStyleBackColor = True
        '
        'txtAdds
        '
        Me.txtAdds.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdds.Location = New System.Drawing.Point(140, 107)
        Me.txtAdds.Name = "txtAdds"
        Me.txtAdds.Size = New System.Drawing.Size(84, 26)
        Me.txtAdds.TabIndex = 32
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(90, 113)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 20)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "Adds"
        '
        'frmQuickPaymentCalc
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(308, 432)
        Me.Controls.Add(Me.txtAdds)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ChkGSTdown)
        Me.Controls.Add(Me.TXTdown)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.ChkIns)
        Me.Controls.Add(Me.ChkFee)
        Me.Controls.Add(Me.ChkPST)
        Me.Controls.Add(Me.ChkGST)
        Me.Controls.Add(Me.BtnClipboard)
        Me.Controls.Add(Me.TxtTotal)
        Me.Controls.Add(Me.TxtIns)
        Me.Controls.Add(Me.TxtFee)
        Me.Controls.Add(Me.TxtPST)
        Me.Controls.Add(Me.TxtGST)
        Me.Controls.Add(Me.TxtPmt)
        Me.Controls.Add(Me.TxtPrice)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmQuickPaymentCalc"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Quick Payment Calculator"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtRate As System.Windows.Forms.TextBox
    Friend WithEvents TxtAmm As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ChkAutomode As System.Windows.Forms.CheckBox
    Friend WithEvents TxtPrice As System.Windows.Forms.TextBox
    Friend WithEvents TxtPmt As System.Windows.Forms.TextBox
    Friend WithEvents TxtGST As System.Windows.Forms.TextBox
    Friend WithEvents TxtPST As System.Windows.Forms.TextBox
    Friend WithEvents TxtFee As System.Windows.Forms.TextBox
    Friend WithEvents TxtIns As System.Windows.Forms.TextBox
    Friend WithEvents TxtTotal As System.Windows.Forms.TextBox
    Friend WithEvents BtnClipboard As System.Windows.Forms.Button
    Friend WithEvents ChkGST As System.Windows.Forms.CheckBox
    Friend WithEvents ChkPST As System.Windows.Forms.CheckBox
    Friend WithEvents ChkFee As System.Windows.Forms.CheckBox
    Friend WithEvents ChkIns As System.Windows.Forms.CheckBox
    Friend WithEvents TXTdown As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ChkGSTdown As System.Windows.Forms.CheckBox
    Friend WithEvents txtAdds As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label

End Class
