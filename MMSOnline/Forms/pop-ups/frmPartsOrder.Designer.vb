<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPartsOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPartsOrder))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnPrintOrder = New System.Windows.Forms.Button()
        Me.btnAddPart = New System.Windows.Forms.Button()
        Me.DVparts = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnViewEdit = New System.Windows.Forms.Button()
        Me.txtcolor = New System.Windows.Forms.TextBox()
        Me.txtyear = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtProv = New System.Windows.Forms.TextBox()
        Me.Txtphone2 = New System.Windows.Forms.TextBox()
        Me.Txtemail = New System.Windows.Forms.TextBox()
        Me.txtphone1 = New System.Windows.Forms.TextBox()
        Me.txtpostal = New System.Windows.Forms.TextBox()
        Me.txtmotorserial = New System.Windows.Forms.TextBox()
        Me.txtBoatSerial = New System.Windows.Forms.TextBox()
        Me.txttrailerserial = New System.Windows.Forms.TextBox()
        Me.txtmotormodel = New System.Windows.Forms.TextBox()
        Me.txtboatmodel = New System.Windows.Forms.TextBox()
        Me.txttrailermodel = New System.Windows.Forms.TextBox()
        Me.txtmotormake = New System.Windows.Forms.TextBox()
        Me.TxtBoatMake = New System.Windows.Forms.TextBox()
        Me.TxtCity = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txttrailermake = New System.Windows.Forms.TextBox()
        Me.btnCustData = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtOrderID = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtWONum = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.DVCalls = New System.Windows.Forms.DataGridView()
        Me.View = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.btnLogACall = New System.Windows.Forms.Button()
        Me.btnViewByCust = New System.Windows.Forms.Button()
        Me.btnReceived = New System.Windows.Forms.Button()
        Me.btnVoid = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtStore = New System.Windows.Forms.TextBox()
        Me.txtOrderedBy = New System.Windows.Forms.TextBox()
        Me.btnAddPayment = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.DVPayments = New System.Windows.Forms.DataGridView()
        Me.DataGridViewButtonColumn1 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.pnlPayment = New System.Windows.Forms.Panel()
        Me.lblWOPaymentNote = New System.Windows.Forms.Label()
        Me.pnlPayment2 = New System.Windows.Forms.Panel()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtPaid = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtBalance = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.txtOrderedDate = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblPaymentType = New System.Windows.Forms.Label()
        Me.cboPaymentType = New System.Windows.Forms.ComboBox()
        Me.RP = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.View2 = New System.Windows.Forms.DataGridViewButtonColumn()
        CType(Me.DVparts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.DVCalls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DVPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPayment.SuspendLayout()
        Me.pnlPayment2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnPrintOrder
        '
        Me.btnPrintOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintOrder.Location = New System.Drawing.Point(712, 265)
        Me.btnPrintOrder.Name = "btnPrintOrder"
        Me.btnPrintOrder.Size = New System.Drawing.Size(100, 31)
        Me.btnPrintOrder.TabIndex = 65
        Me.btnPrintOrder.Text = "Print Order"
        Me.btnPrintOrder.UseVisualStyleBackColor = True
        '
        'btnAddPart
        '
        Me.btnAddPart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddPart.Location = New System.Drawing.Point(604, 265)
        Me.btnAddPart.Name = "btnAddPart"
        Me.btnAddPart.Size = New System.Drawing.Size(100, 31)
        Me.btnAddPart.TabIndex = 62
        Me.btnAddPart.Text = "Add Part"
        Me.btnAddPart.UseVisualStyleBackColor = True
        Me.btnAddPart.Visible = False
        '
        'DVparts
        '
        Me.DVparts.AllowUserToAddRows = False
        Me.DVparts.AllowUserToDeleteRows = False
        Me.DVparts.AllowUserToResizeRows = False
        Me.DVparts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DVparts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.DVparts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DVparts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RP, Me.View2})
        Me.DVparts.Location = New System.Drawing.Point(12, 299)
        Me.DVparts.Name = "DVparts"
        Me.DVparts.ReadOnly = True
        Me.DVparts.RowHeadersVisible = False
        Me.DVparts.Size = New System.Drawing.Size(799, 305)
        Me.DVparts.TabIndex = 64
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 272)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(222, 24)
        Me.Label3.TabIndex = 63
        Me.Label3.Text = "List of Parts on this Order:"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btnViewEdit)
        Me.Panel1.Controls.Add(Me.txtcolor)
        Me.Panel1.Controls.Add(Me.txtyear)
        Me.Panel1.Controls.Add(Me.Label38)
        Me.Panel1.Controls.Add(Me.Label39)
        Me.Panel1.Controls.Add(Me.txtProv)
        Me.Panel1.Controls.Add(Me.Txtphone2)
        Me.Panel1.Controls.Add(Me.Txtemail)
        Me.Panel1.Controls.Add(Me.txtphone1)
        Me.Panel1.Controls.Add(Me.txtpostal)
        Me.Panel1.Controls.Add(Me.txtmotorserial)
        Me.Panel1.Controls.Add(Me.txtBoatSerial)
        Me.Panel1.Controls.Add(Me.txttrailerserial)
        Me.Panel1.Controls.Add(Me.txtmotormodel)
        Me.Panel1.Controls.Add(Me.txtboatmodel)
        Me.Panel1.Controls.Add(Me.txttrailermodel)
        Me.Panel1.Controls.Add(Me.txtmotormake)
        Me.Panel1.Controls.Add(Me.TxtBoatMake)
        Me.Panel1.Controls.Add(Me.TxtCity)
        Me.Panel1.Controls.Add(Me.txtAddress)
        Me.Panel1.Controls.Add(Me.txtName)
        Me.Panel1.Controls.Add(Me.txttrailermake)
        Me.Panel1.Controls.Add(Me.btnCustData)
        Me.Panel1.Controls.Add(Me.Label25)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Location = New System.Drawing.Point(13, 91)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(789, 163)
        Me.Panel1.TabIndex = 66
        '
        'btnViewEdit
        '
        Me.btnViewEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnViewEdit.Location = New System.Drawing.Point(690, 86)
        Me.btnViewEdit.Name = "btnViewEdit"
        Me.btnViewEdit.Size = New System.Drawing.Size(95, 35)
        Me.btnViewEdit.TabIndex = 43
        Me.btnViewEdit.Text = "View / Edit Details"
        Me.btnViewEdit.UseVisualStyleBackColor = True
        '
        'txtcolor
        '
        Me.txtcolor.Enabled = False
        Me.txtcolor.Location = New System.Drawing.Point(513, 118)
        Me.txtcolor.Name = "txtcolor"
        Me.txtcolor.Size = New System.Drawing.Size(99, 20)
        Me.txtcolor.TabIndex = 42
        '
        'txtyear
        '
        Me.txtyear.Enabled = False
        Me.txtyear.Location = New System.Drawing.Point(514, 141)
        Me.txtyear.Name = "txtyear"
        Me.txtyear.Size = New System.Drawing.Size(99, 20)
        Me.txtyear.TabIndex = 41
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(466, 141)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(42, 18)
        Me.Label38.TabIndex = 40
        Me.Label38.Text = "Year:"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(424, 120)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(84, 18)
        Me.Label39.TabIndex = 39
        Me.Label39.Text = "Boat Color:"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtProv
        '
        Me.txtProv.Enabled = False
        Me.txtProv.Location = New System.Drawing.Point(317, 69)
        Me.txtProv.Name = "txtProv"
        Me.txtProv.Size = New System.Drawing.Size(58, 20)
        Me.txtProv.TabIndex = 37
        '
        'Txtphone2
        '
        Me.Txtphone2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txtphone2.Enabled = False
        Me.Txtphone2.Location = New System.Drawing.Point(652, 20)
        Me.Txtphone2.Name = "Txtphone2"
        Me.Txtphone2.Size = New System.Drawing.Size(99, 20)
        Me.Txtphone2.TabIndex = 36
        '
        'Txtemail
        '
        Me.Txtemail.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txtemail.Enabled = False
        Me.Txtemail.Location = New System.Drawing.Point(513, 46)
        Me.Txtemail.Name = "Txtemail"
        Me.Txtemail.Size = New System.Drawing.Size(171, 20)
        Me.Txtemail.TabIndex = 35
        Me.Txtemail.Visible = False
        '
        'txtphone1
        '
        Me.txtphone1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtphone1.Enabled = False
        Me.txtphone1.Location = New System.Drawing.Point(464, 20)
        Me.txtphone1.Name = "txtphone1"
        Me.txtphone1.Size = New System.Drawing.Size(99, 20)
        Me.txtphone1.TabIndex = 34
        '
        'txtpostal
        '
        Me.txtpostal.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtpostal.Enabled = False
        Me.txtpostal.Location = New System.Drawing.Point(513, 69)
        Me.txtpostal.Name = "txtpostal"
        Me.txtpostal.Size = New System.Drawing.Size(99, 20)
        Me.txtpostal.TabIndex = 33
        Me.txtpostal.Visible = False
        '
        'txtmotorserial
        '
        Me.txtmotorserial.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtmotorserial.Enabled = False
        Me.txtmotorserial.Location = New System.Drawing.Point(785, 43)
        Me.txtmotorserial.Name = "txtmotorserial"
        Me.txtmotorserial.Size = New System.Drawing.Size(171, 20)
        Me.txtmotorserial.TabIndex = 32
        Me.txtmotorserial.Visible = False
        '
        'txtBoatSerial
        '
        Me.txtBoatSerial.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBoatSerial.Enabled = False
        Me.txtBoatSerial.Location = New System.Drawing.Point(513, 95)
        Me.txtBoatSerial.Name = "txtBoatSerial"
        Me.txtBoatSerial.Size = New System.Drawing.Size(171, 20)
        Me.txtBoatSerial.TabIndex = 31
        '
        'txttrailerserial
        '
        Me.txttrailerserial.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txttrailerserial.Enabled = False
        Me.txttrailerserial.Location = New System.Drawing.Point(785, 66)
        Me.txttrailerserial.Name = "txttrailerserial"
        Me.txttrailerserial.Size = New System.Drawing.Size(171, 20)
        Me.txttrailerserial.TabIndex = 30
        Me.txttrailerserial.Visible = False
        '
        'txtmotormodel
        '
        Me.txtmotormodel.Enabled = False
        Me.txtmotormodel.Location = New System.Drawing.Point(317, 115)
        Me.txtmotormodel.Name = "txtmotormodel"
        Me.txtmotormodel.Size = New System.Drawing.Size(99, 20)
        Me.txtmotormodel.TabIndex = 29
        '
        'txtboatmodel
        '
        Me.txtboatmodel.Enabled = False
        Me.txtboatmodel.Location = New System.Drawing.Point(317, 92)
        Me.txtboatmodel.Name = "txtboatmodel"
        Me.txtboatmodel.Size = New System.Drawing.Size(99, 20)
        Me.txtboatmodel.TabIndex = 28
        '
        'txttrailermodel
        '
        Me.txttrailermodel.Enabled = False
        Me.txttrailermodel.Location = New System.Drawing.Point(318, 138)
        Me.txttrailermodel.Name = "txttrailermodel"
        Me.txttrailermodel.Size = New System.Drawing.Size(99, 20)
        Me.txttrailermodel.TabIndex = 27
        '
        'txtmotormake
        '
        Me.txtmotormake.Enabled = False
        Me.txtmotormake.Location = New System.Drawing.Point(110, 115)
        Me.txtmotormake.Name = "txtmotormake"
        Me.txtmotormake.Size = New System.Drawing.Size(99, 20)
        Me.txtmotormake.TabIndex = 26
        '
        'TxtBoatMake
        '
        Me.TxtBoatMake.Enabled = False
        Me.TxtBoatMake.Location = New System.Drawing.Point(110, 92)
        Me.TxtBoatMake.Name = "TxtBoatMake"
        Me.TxtBoatMake.Size = New System.Drawing.Size(99, 20)
        Me.TxtBoatMake.TabIndex = 25
        '
        'TxtCity
        '
        Me.TxtCity.Enabled = False
        Me.TxtCity.Location = New System.Drawing.Point(81, 69)
        Me.TxtCity.Name = "TxtCity"
        Me.TxtCity.Size = New System.Drawing.Size(128, 20)
        Me.TxtCity.TabIndex = 24
        '
        'txtAddress
        '
        Me.txtAddress.Enabled = False
        Me.txtAddress.Location = New System.Drawing.Point(81, 46)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(294, 20)
        Me.txtAddress.TabIndex = 23
        Me.txtAddress.Visible = False
        '
        'txtName
        '
        Me.txtName.Enabled = False
        Me.txtName.Location = New System.Drawing.Point(81, 22)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(294, 20)
        Me.txtName.TabIndex = 22
        '
        'txttrailermake
        '
        Me.txttrailermake.Enabled = False
        Me.txttrailermake.Location = New System.Drawing.Point(110, 138)
        Me.txttrailermake.Name = "txttrailermake"
        Me.txttrailermake.Size = New System.Drawing.Size(99, 20)
        Me.txttrailermake.TabIndex = 21
        '
        'btnCustData
        '
        Me.btnCustData.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCustData.Location = New System.Drawing.Point(690, 123)
        Me.btnCustData.Name = "btnCustData"
        Me.btnCustData.Size = New System.Drawing.Size(95, 35)
        Me.btnCustData.TabIndex = 0
        Me.btnCustData.Text = "Select a Customer"
        Me.btnCustData.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(213, 140)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(98, 18)
        Me.Label25.TabIndex = 16
        Me.Label25.Text = "Trailer Model:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(10, 137)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(94, 18)
        Me.Label26.TabIndex = 15
        Me.Label26.Text = "Trailer Make:"
        '
        'Label22
        '
        Me.Label22.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(422, 97)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(84, 18)
        Me.Label22.TabIndex = 13
        Me.Label22.Text = "Boat Serial:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(223, 94)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(88, 18)
        Me.Label20.TabIndex = 11
        Me.Label20.Text = "Boat Model:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(214, 117)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(97, 18)
        Me.Label21.TabIndex = 12
        Me.Label21.Text = "Motor Model:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(20, 94)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 18)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Boat Make:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(241, 68)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 18)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Province:"
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(575, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 18)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Phone #2:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(390, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(67, 18)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "Phone #:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(395, 48)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(112, 18)
        Me.Label15.TabIndex = 9
        Me.Label15.Text = "E-mail Address:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label15.Visible = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(11, 114)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(93, 18)
        Me.Label19.TabIndex = 10
        Me.Label19.Text = "Motor Make:"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(413, 69)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(94, 18)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Postal Code:"
        Me.Label7.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 68)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 18)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "City:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(17, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 18)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Address:"
        Me.Label5.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(17, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 18)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Name:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(153, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Customer Profile:"
        '
        'Label24
        '
        Me.Label24.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(691, 65)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(94, 18)
        Me.Label24.TabIndex = 17
        Me.Label24.Text = "Trailer Serial:"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label24.Visible = False
        '
        'Label23
        '
        Me.Label23.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(690, 43)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(93, 18)
        Me.Label23.TabIndex = 14
        Me.Label23.Text = "Motor Serial:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label23.Visible = False
        '
        'txtOrderID
        '
        Me.txtOrderID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderID.Location = New System.Drawing.Point(122, 9)
        Me.txtOrderID.Name = "txtOrderID"
        Me.txtOrderID.ReadOnly = True
        Me.txtOrderID.Size = New System.Drawing.Size(123, 20)
        Me.txtOrderID.TabIndex = 68
        Me.txtOrderID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(10, 11)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(106, 18)
        Me.Label12.TabIndex = 67
        Me.Label12.Text = "Part Order #:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(319, 11)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(48, 18)
        Me.Label17.TabIndex = 71
        Me.Label17.Text = "Store:"
        '
        'txtWONum
        '
        Me.txtWONum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWONum.Location = New System.Drawing.Point(373, 35)
        Me.txtWONum.Name = "txtWONum"
        Me.txtWONum.ReadOnly = True
        Me.txtWONum.Size = New System.Drawing.Size(123, 20)
        Me.txtWONum.TabIndex = 73
        Me.txtWONum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(316, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 18)
        Me.Label2.TabIndex = 74
        Me.Label2.Text = "WO #:"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(16, 620)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(83, 24)
        Me.Label28.TabIndex = 78
        Me.Label28.Text = "Call Log:"
        '
        'DVCalls
        '
        Me.DVCalls.AllowUserToAddRows = False
        Me.DVCalls.AllowUserToDeleteRows = False
        Me.DVCalls.AllowUserToResizeRows = False
        Me.DVCalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DVCalls.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.View})
        Me.DVCalls.Location = New System.Drawing.Point(12, 647)
        Me.DVCalls.Name = "DVCalls"
        Me.DVCalls.ReadOnly = True
        Me.DVCalls.RowHeadersVisible = False
        Me.DVCalls.Size = New System.Drawing.Size(799, 98)
        Me.DVCalls.TabIndex = 77
        '
        'View
        '
        Me.View.HeaderText = "View"
        Me.View.Name = "View"
        Me.View.ReadOnly = True
        Me.View.Text = "View"
        Me.View.Visible = False
        '
        'btnLogACall
        '
        Me.btnLogACall.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogACall.Location = New System.Drawing.Point(711, 613)
        Me.btnLogACall.Name = "btnLogACall"
        Me.btnLogACall.Size = New System.Drawing.Size(100, 31)
        Me.btnLogACall.TabIndex = 79
        Me.btnLogACall.Text = "Log a Call"
        Me.btnLogACall.UseVisualStyleBackColor = True
        '
        'btnViewByCust
        '
        Me.btnViewByCust.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewByCust.Location = New System.Drawing.Point(526, 613)
        Me.btnViewByCust.Name = "btnViewByCust"
        Me.btnViewByCust.Size = New System.Drawing.Size(179, 31)
        Me.btnViewByCust.TabIndex = 80
        Me.btnViewByCust.Text = "View All Customer Calls"
        Me.btnViewByCust.UseVisualStyleBackColor = True
        '
        'btnReceived
        '
        Me.btnReceived.BackColor = System.Drawing.SystemColors.Control
        Me.btnReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReceived.Image = CType(resources.GetObject("btnReceived.Image"), System.Drawing.Image)
        Me.btnReceived.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnReceived.Location = New System.Drawing.Point(406, 265)
        Me.btnReceived.Name = "btnReceived"
        Me.btnReceived.Size = New System.Drawing.Size(153, 31)
        Me.btnReceived.TabIndex = 81
        Me.btnReceived.Text = "All Parts Received"
        Me.btnReceived.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnReceived.UseVisualStyleBackColor = True
        '
        'btnVoid
        '
        Me.btnVoid.BackColor = System.Drawing.SystemColors.Control
        Me.btnVoid.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVoid.Image = CType(resources.GetObject("btnVoid.Image"), System.Drawing.Image)
        Me.btnVoid.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnVoid.Location = New System.Drawing.Point(296, 265)
        Me.btnVoid.Name = "btnVoid"
        Me.btnVoid.Size = New System.Drawing.Size(104, 31)
        Me.btnVoid.TabIndex = 82
        Me.btnVoid.Text = "Void Order"
        Me.btnVoid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnVoid.UseVisualStyleBackColor = True
        Me.btnVoid.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(29, 37)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(87, 18)
        Me.Label13.TabIndex = 83
        Me.Label13.Text = "Ordered By:"
        '
        'txtStore
        '
        Me.txtStore.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStore.Location = New System.Drawing.Point(373, 9)
        Me.txtStore.Name = "txtStore"
        Me.txtStore.ReadOnly = True
        Me.txtStore.Size = New System.Drawing.Size(123, 20)
        Me.txtStore.TabIndex = 85
        Me.txtStore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtOrderedBy
        '
        Me.txtOrderedBy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderedBy.Location = New System.Drawing.Point(122, 35)
        Me.txtOrderedBy.Name = "txtOrderedBy"
        Me.txtOrderedBy.ReadOnly = True
        Me.txtOrderedBy.Size = New System.Drawing.Size(123, 20)
        Me.txtOrderedBy.TabIndex = 86
        Me.txtOrderedBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnAddPayment
        '
        Me.btnAddPayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddPayment.Location = New System.Drawing.Point(700, 4)
        Me.btnAddPayment.Name = "btnAddPayment"
        Me.btnAddPayment.Size = New System.Drawing.Size(109, 31)
        Me.btnAddPayment.TabIndex = 89
        Me.btnAddPayment.Text = "Add Payment"
        Me.btnAddPayment.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(14, 11)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(176, 24)
        Me.Label14.TabIndex = 88
        Me.Label14.Text = "Payment Collection:"
        '
        'DVPayments
        '
        Me.DVPayments.AllowUserToAddRows = False
        Me.DVPayments.AllowUserToDeleteRows = False
        Me.DVPayments.AllowUserToResizeRows = False
        Me.DVPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DVPayments.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewButtonColumn1})
        Me.DVPayments.Location = New System.Drawing.Point(10, 38)
        Me.DVPayments.Name = "DVPayments"
        Me.DVPayments.ReadOnly = True
        Me.DVPayments.RowHeadersVisible = False
        Me.DVPayments.Size = New System.Drawing.Size(799, 98)
        Me.DVPayments.TabIndex = 87
        '
        'DataGridViewButtonColumn1
        '
        Me.DataGridViewButtonColumn1.HeaderText = "View"
        Me.DataGridViewButtonColumn1.Name = "DataGridViewButtonColumn1"
        Me.DataGridViewButtonColumn1.ReadOnly = True
        Me.DataGridViewButtonColumn1.Text = "View"
        Me.DataGridViewButtonColumn1.Visible = False
        '
        'pnlPayment
        '
        Me.pnlPayment.Controls.Add(Me.lblWOPaymentNote)
        Me.pnlPayment.Controls.Add(Me.DVPayments)
        Me.pnlPayment.Controls.Add(Me.btnAddPayment)
        Me.pnlPayment.Controls.Add(Me.Label14)
        Me.pnlPayment.Enabled = False
        Me.pnlPayment.Location = New System.Drawing.Point(2, 751)
        Me.pnlPayment.Name = "pnlPayment"
        Me.pnlPayment.Size = New System.Drawing.Size(821, 150)
        Me.pnlPayment.TabIndex = 90
        '
        'lblWOPaymentNote
        '
        Me.lblWOPaymentNote.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWOPaymentNote.Location = New System.Drawing.Point(210, 11)
        Me.lblWOPaymentNote.Name = "lblWOPaymentNote"
        Me.lblWOPaymentNote.Size = New System.Drawing.Size(451, 23)
        Me.lblWOPaymentNote.TabIndex = 90
        Me.lblWOPaymentNote.Text = "* Payment for this order is done in the Work Order *"
        Me.lblWOPaymentNote.Visible = False
        '
        'pnlPayment2
        '
        Me.pnlPayment2.Controls.Add(Me.txtTotal)
        Me.pnlPayment2.Controls.Add(Me.txtPaid)
        Me.pnlPayment2.Controls.Add(Me.Label16)
        Me.pnlPayment2.Controls.Add(Me.Label18)
        Me.pnlPayment2.Controls.Add(Me.txtBalance)
        Me.pnlPayment2.Controls.Add(Me.Label27)
        Me.pnlPayment2.Location = New System.Drawing.Point(578, 3)
        Me.pnlPayment2.Name = "pnlPayment2"
        Me.pnlPayment2.Size = New System.Drawing.Size(223, 88)
        Me.pnlPayment2.TabIndex = 91
        Me.pnlPayment2.Visible = False
        '
        'txtTotal
        '
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(110, 6)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(99, 20)
        Me.txtTotal.TabIndex = 92
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPaid
        '
        Me.txtPaid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaid.Location = New System.Drawing.Point(110, 32)
        Me.txtPaid.Name = "txtPaid"
        Me.txtPaid.ReadOnly = True
        Me.txtPaid.Size = New System.Drawing.Size(99, 20)
        Me.txtPaid.TabIndex = 91
        Me.txtPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(17, 8)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(86, 18)
        Me.Label16.TabIndex = 90
        Me.Label16.Text = "Total + Tax:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(7, 60)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(96, 18)
        Me.Label18.TabIndex = 89
        Me.Label18.Text = "Balance Due:"
        '
        'txtBalance
        '
        Me.txtBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalance.Location = New System.Drawing.Point(110, 58)
        Me.txtBalance.Name = "txtBalance"
        Me.txtBalance.ReadOnly = True
        Me.txtBalance.Size = New System.Drawing.Size(99, 20)
        Me.txtBalance.TabIndex = 88
        Me.txtBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(7, 34)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(96, 18)
        Me.Label27.TabIndex = 87
        Me.Label27.Text = "Amount Paid:"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(16, 907)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(64, 24)
        Me.Label29.TabIndex = 92
        Me.Label29.Text = "Notes:"
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(13, 934)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(799, 77)
        Me.txtNotes.TabIndex = 93
        '
        'txtOrderedDate
        '
        Me.txtOrderedDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderedDate.Location = New System.Drawing.Point(122, 61)
        Me.txtOrderedDate.Name = "txtOrderedDate"
        Me.txtOrderedDate.ReadOnly = True
        Me.txtOrderedDate.Size = New System.Drawing.Size(123, 20)
        Me.txtOrderedDate.TabIndex = 95
        Me.txtOrderedDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(15, 62)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(101, 18)
        Me.Label30.TabIndex = 94
        Me.Label30.Text = "Ordered Date:"
        '
        'lblPaymentType
        '
        Me.lblPaymentType.AutoSize = True
        Me.lblPaymentType.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentType.Location = New System.Drawing.Point(261, 63)
        Me.lblPaymentType.Name = "lblPaymentType"
        Me.lblPaymentType.Size = New System.Drawing.Size(106, 18)
        Me.lblPaymentType.TabIndex = 96
        Me.lblPaymentType.Text = "Payment Type:"
        Me.lblPaymentType.Visible = False
        '
        'cboPaymentType
        '
        Me.cboPaymentType.FormattingEnabled = True
        Me.cboPaymentType.Items.AddRange(New Object() {"", "Customer", "Warranty", "Bill Of Sale", "Work Order", "Stock"})
        Me.cboPaymentType.Location = New System.Drawing.Point(373, 61)
        Me.cboPaymentType.Name = "cboPaymentType"
        Me.cboPaymentType.Size = New System.Drawing.Size(123, 21)
        Me.cboPaymentType.TabIndex = 97
        Me.cboPaymentType.Visible = False
        '
        'RP
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Red
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RP.DefaultCellStyle = DataGridViewCellStyle1
        Me.RP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RP.HeaderText = "Remove"
        Me.RP.MinimumWidth = 50
        Me.RP.Name = "RP"
        Me.RP.ReadOnly = True
        Me.RP.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RP.Width = 53
        '
        'View2
        '
        Me.View2.HeaderText = "View"
        Me.View2.Name = "View2"
        Me.View2.ReadOnly = True
        Me.View2.Visible = False
        Me.View2.Width = 36
        '
        'frmPartsOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(822, 1023)
        Me.Controls.Add(Me.cboPaymentType)
        Me.Controls.Add(Me.lblPaymentType)
        Me.Controls.Add(Me.txtOrderedDate)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.pnlPayment2)
        Me.Controls.Add(Me.pnlPayment)
        Me.Controls.Add(Me.txtOrderedBy)
        Me.Controls.Add(Me.txtStore)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.btnVoid)
        Me.Controls.Add(Me.btnReceived)
        Me.Controls.Add(Me.btnViewByCust)
        Me.Controls.Add(Me.btnLogACall)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.DVCalls)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtWONum)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtOrderID)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnPrintOrder)
        Me.Controls.Add(Me.btnAddPart)
        Me.Controls.Add(Me.DVparts)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label12)
        Me.Name = "frmPartsOrder"
        Me.Text = "Parts Order"
        CType(Me.DVparts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DVCalls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DVPayments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPayment.ResumeLayout(False)
        Me.pnlPayment.PerformLayout()
        Me.pnlPayment2.ResumeLayout(False)
        Me.pnlPayment2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnPrintOrder As System.Windows.Forms.Button
    Friend WithEvents btnAddPart As System.Windows.Forms.Button
    Friend WithEvents DVparts As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtcolor As System.Windows.Forms.TextBox
    Friend WithEvents txtyear As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtProv As System.Windows.Forms.TextBox
    Friend WithEvents Txtphone2 As System.Windows.Forms.TextBox
    Friend WithEvents Txtemail As System.Windows.Forms.TextBox
    Friend WithEvents txtphone1 As System.Windows.Forms.TextBox
    Friend WithEvents txtpostal As System.Windows.Forms.TextBox
    Friend WithEvents txtmotorserial As System.Windows.Forms.TextBox
    Friend WithEvents txtBoatSerial As System.Windows.Forms.TextBox
    Friend WithEvents txttrailerserial As System.Windows.Forms.TextBox
    Friend WithEvents txtmotormodel As System.Windows.Forms.TextBox
    Friend WithEvents txtboatmodel As System.Windows.Forms.TextBox
    Friend WithEvents txttrailermodel As System.Windows.Forms.TextBox
    Friend WithEvents txtmotormake As System.Windows.Forms.TextBox
    Friend WithEvents TxtBoatMake As System.Windows.Forms.TextBox
    Friend WithEvents TxtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txttrailermake As System.Windows.Forms.TextBox
    Friend WithEvents btnCustData As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtOrderID As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtWONum As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents DVCalls As System.Windows.Forms.DataGridView
    Friend WithEvents View As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents btnLogACall As System.Windows.Forms.Button
    Friend WithEvents btnViewByCust As System.Windows.Forms.Button
    Friend WithEvents btnReceived As System.Windows.Forms.Button
    Friend WithEvents btnVoid As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtStore As System.Windows.Forms.TextBox
    Friend WithEvents txtOrderedBy As System.Windows.Forms.TextBox
    Friend WithEvents btnAddPayment As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents DVPayments As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewButtonColumn1 As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents pnlPayment As System.Windows.Forms.Panel
    Friend WithEvents lblWOPaymentNote As System.Windows.Forms.Label
    Friend WithEvents pnlPayment2 As System.Windows.Forms.Panel
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtPaid As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents btnViewEdit As System.Windows.Forms.Button
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents txtOrderedDate As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblPaymentType As System.Windows.Forms.Label
    Friend WithEvents cboPaymentType As System.Windows.Forms.ComboBox
    Friend WithEvents RP As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents View2 As System.Windows.Forms.DataGridViewButtonColumn
End Class
