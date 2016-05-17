<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrderBoat
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.cmbmake = New System.Windows.Forms.ComboBox
        Me.cmbbmake = New System.Windows.Forms.ComboBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtcolour = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtprice = New System.Windows.Forms.TextBox
        Me.txtmyear = New System.Windows.Forms.TextBox
        Me.txtmmodel = New System.Windows.Forms.TextBox
        Me.txtcomments = New System.Windows.Forms.TextBox
        Me.txtequip = New System.Windows.Forms.TextBox
        Me.txtbyear = New System.Windows.Forms.TextBox
        Me.txtbmodel = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnDenied = New System.Windows.Forms.Button
        Me.btnApproved = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnorder = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbstatus = New System.Windows.Forms.ComboBox
        Me.txtsalesman = New System.Windows.Forms.TextBox
        Me.txtbos = New System.Windows.Forms.TextBox
        Me.txtstore = New System.Windows.Forms.TextBox
        Me.txtcustomer = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.btncancel = New System.Windows.Forms.Button
        Me.btnupdate = New System.Windows.Forms.Button
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btncancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnupdate)
        Me.SplitContainer1.Size = New System.Drawing.Size(850, 649)
        Me.SplitContainer1.SplitterDistance = 605
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbmake)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbbmake)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label15)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtcolour)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label13)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label12)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label11)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label10)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label9)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtprice)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtmyear)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtmmodel)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtcomments)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtequip)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtbyear)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtbmodel)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnDenied)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnApproved)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnorder)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.cmbstatus)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtsalesman)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtbos)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtstore)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtcustomer)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label14)
        Me.SplitContainer2.Size = New System.Drawing.Size(850, 605)
        Me.SplitContainer2.SplitterDistance = 592
        Me.SplitContainer2.SplitterWidth = 5
        Me.SplitContainer2.TabIndex = 0
        '
        'cmbmake
        '
        Me.cmbmake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbmake.FormattingEnabled = True
        Me.cmbmake.Location = New System.Drawing.Point(23, 93)
        Me.cmbmake.Name = "cmbmake"
        Me.cmbmake.Size = New System.Drawing.Size(218, 24)
        Me.cmbmake.TabIndex = 3
        '
        'cmbbmake
        '
        Me.cmbbmake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbbmake.FormattingEnabled = True
        Me.cmbbmake.Location = New System.Drawing.Point(23, 44)
        Me.cmbbmake.Name = "cmbbmake"
        Me.cmbbmake.Size = New System.Drawing.Size(218, 24)
        Me.cmbbmake.TabIndex = 0
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(23, 127)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(47, 16)
        Me.Label15.TabIndex = 21
        Me.Label15.Text = "Colour"
        '
        'txtcolour
        '
        Me.txtcolour.Location = New System.Drawing.Point(23, 147)
        Me.txtcolour.Margin = New System.Windows.Forms.Padding(4)
        Me.txtcolour.Name = "txtcolour"
        Me.txtcolour.Size = New System.Drawing.Size(145, 22)
        Me.txtcolour.TabIndex = 5
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(23, 435)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(72, 16)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "Comments"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(339, 395)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(58, 24)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "Price:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(26, 184)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(142, 16)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Requested Equipment"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(489, 74)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 16)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Motor Year"
        Me.Label10.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(246, 74)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(83, 16)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Motor Model"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(23, 74)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(79, 16)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Motor Make"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(489, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 16)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Boat Year"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(245, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 16)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Boat Model"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(23, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 16)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Boat Make"
        '
        'txtprice
        '
        Me.txtprice.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprice.Location = New System.Drawing.Point(404, 392)
        Me.txtprice.Margin = New System.Windows.Forms.Padding(4)
        Me.txtprice.Name = "txtprice"
        Me.txtprice.Size = New System.Drawing.Size(175, 29)
        Me.txtprice.TabIndex = 7
        '
        'txtmyear
        '
        Me.txtmyear.Location = New System.Drawing.Point(492, 94)
        Me.txtmyear.Margin = New System.Windows.Forms.Padding(4)
        Me.txtmyear.Name = "txtmyear"
        Me.txtmyear.Size = New System.Drawing.Size(87, 22)
        Me.txtmyear.TabIndex = 10
        Me.txtmyear.Visible = False
        '
        'txtmmodel
        '
        Me.txtmmodel.Location = New System.Drawing.Point(249, 94)
        Me.txtmmodel.Margin = New System.Windows.Forms.Padding(4)
        Me.txtmmodel.Name = "txtmmodel"
        Me.txtmmodel.Size = New System.Drawing.Size(234, 22)
        Me.txtmmodel.TabIndex = 4
        '
        'txtcomments
        '
        Me.txtcomments.Location = New System.Drawing.Point(23, 455)
        Me.txtcomments.Margin = New System.Windows.Forms.Padding(4)
        Me.txtcomments.Multiline = True
        Me.txtcomments.Name = "txtcomments"
        Me.txtcomments.Size = New System.Drawing.Size(556, 154)
        Me.txtcomments.TabIndex = 8
        '
        'txtequip
        '
        Me.txtequip.Location = New System.Drawing.Point(23, 204)
        Me.txtequip.Margin = New System.Windows.Forms.Padding(4)
        Me.txtequip.Multiline = True
        Me.txtequip.Name = "txtequip"
        Me.txtequip.Size = New System.Drawing.Size(556, 180)
        Me.txtequip.TabIndex = 6
        '
        'txtbyear
        '
        Me.txtbyear.Location = New System.Drawing.Point(492, 44)
        Me.txtbyear.Margin = New System.Windows.Forms.Padding(4)
        Me.txtbyear.Name = "txtbyear"
        Me.txtbyear.Size = New System.Drawing.Size(87, 22)
        Me.txtbyear.TabIndex = 2
        '
        'txtbmodel
        '
        Me.txtbmodel.Location = New System.Drawing.Point(249, 44)
        Me.txtbmodel.Margin = New System.Windows.Forms.Padding(4)
        Me.txtbmodel.Name = "txtbmodel"
        Me.txtbmodel.Size = New System.Drawing.Size(234, 22)
        Me.txtbmodel.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(57, 211)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(135, 33)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "View Deal"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnDenied
        '
        Me.btnDenied.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDenied.Location = New System.Drawing.Point(58, 487)
        Me.btnDenied.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDenied.Name = "btnDenied"
        Me.btnDenied.Size = New System.Drawing.Size(135, 28)
        Me.btnDenied.TabIndex = 2
        Me.btnDenied.Text = "Order Denied"
        Me.btnDenied.UseVisualStyleBackColor = True
        '
        'btnApproved
        '
        Me.btnApproved.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApproved.Location = New System.Drawing.Point(58, 523)
        Me.btnApproved.Margin = New System.Windows.Forms.Padding(4)
        Me.btnApproved.Name = "btnApproved"
        Me.btnApproved.Size = New System.Drawing.Size(135, 28)
        Me.btnApproved.TabIndex = 1
        Me.btnApproved.Text = "Order Approved"
        Me.btnApproved.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(38, 162)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 16)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Salesman"
        '
        'btnorder
        '
        Me.btnorder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnorder.Location = New System.Drawing.Point(58, 523)
        Me.btnorder.Margin = New System.Windows.Forms.Padding(4)
        Me.btnorder.Name = "btnorder"
        Me.btnorder.Size = New System.Drawing.Size(135, 28)
        Me.btnorder.TabIndex = 3
        Me.btnorder.Text = "Order Now"
        Me.btnorder.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(38, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 16)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Customer"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(38, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Store"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(38, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 16)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "BOS Number"
        '
        'cmbstatus
        '
        Me.cmbstatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbstatus.Enabled = False
        Me.cmbstatus.FormattingEnabled = True
        Me.cmbstatus.Items.AddRange(New Object() {"New Order", "Requested", "Approved", "Denied", "Ordered", "Canceled"})
        Me.cmbstatus.Location = New System.Drawing.Point(41, 575)
        Me.cmbstatus.Name = "cmbstatus"
        Me.cmbstatus.Size = New System.Drawing.Size(175, 24)
        Me.cmbstatus.TabIndex = 0
        '
        'txtsalesman
        '
        Me.txtsalesman.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtsalesman.Location = New System.Drawing.Point(41, 182)
        Me.txtsalesman.Margin = New System.Windows.Forms.Padding(4)
        Me.txtsalesman.Name = "txtsalesman"
        Me.txtsalesman.ReadOnly = True
        Me.txtsalesman.Size = New System.Drawing.Size(175, 22)
        Me.txtsalesman.TabIndex = 8
        '
        'txtbos
        '
        Me.txtbos.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtbos.Location = New System.Drawing.Point(41, 44)
        Me.txtbos.Margin = New System.Windows.Forms.Padding(4)
        Me.txtbos.Name = "txtbos"
        Me.txtbos.ReadOnly = True
        Me.txtbos.Size = New System.Drawing.Size(175, 22)
        Me.txtbos.TabIndex = 5
        '
        'txtstore
        '
        Me.txtstore.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtstore.Location = New System.Drawing.Point(41, 90)
        Me.txtstore.Margin = New System.Windows.Forms.Padding(4)
        Me.txtstore.Name = "txtstore"
        Me.txtstore.ReadOnly = True
        Me.txtstore.Size = New System.Drawing.Size(175, 22)
        Me.txtstore.TabIndex = 7
        '
        'txtcustomer
        '
        Me.txtcustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtcustomer.Location = New System.Drawing.Point(41, 136)
        Me.txtcustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.txtcustomer.Name = "txtcustomer"
        Me.txtcustomer.ReadOnly = True
        Me.txtcustomer.Size = New System.Drawing.Size(175, 22)
        Me.txtcustomer.TabIndex = 6
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(33, 559)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(45, 16)
        Me.Label14.TabIndex = 14
        Me.Label14.Text = "Status"
        '
        'btncancel
        '
        Me.btncancel.Location = New System.Drawing.Point(4, 6)
        Me.btncancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(98, 28)
        Me.btncancel.TabIndex = 1
        Me.btncancel.Text = "Close"
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.Location = New System.Drawing.Point(746, 1)
        Me.btnupdate.Margin = New System.Windows.Forms.Padding(4)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(100, 28)
        Me.btnupdate.TabIndex = 0
        Me.btnupdate.Text = "Save"
        Me.btnupdate.UseVisualStyleBackColor = True
        '
        'frmOrderBoat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(850, 649)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmOrderBoat"
        Me.Text = "Order a Boat"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtmyear As System.Windows.Forms.TextBox
    Friend WithEvents txtmmodel As System.Windows.Forms.TextBox
    Friend WithEvents txtstore As System.Windows.Forms.TextBox
    Friend WithEvents txtcustomer As System.Windows.Forms.TextBox
    Friend WithEvents txtbos As System.Windows.Forms.TextBox
    Friend WithEvents txtcomments As System.Windows.Forms.TextBox
    Friend WithEvents txtequip As System.Windows.Forms.TextBox
    Friend WithEvents txtbyear As System.Windows.Forms.TextBox
    Friend WithEvents txtbmodel As System.Windows.Forms.TextBox
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents btnorder As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents txtprice As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbstatus As System.Windows.Forms.ComboBox
    Friend WithEvents txtsalesman As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtcolour As System.Windows.Forms.TextBox
    Friend WithEvents btnDenied As System.Windows.Forms.Button
    Friend WithEvents btnApproved As System.Windows.Forms.Button
    Friend WithEvents cmbmake As System.Windows.Forms.ComboBox
    Friend WithEvents cmbbmake As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
