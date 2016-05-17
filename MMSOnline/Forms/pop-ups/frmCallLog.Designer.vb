<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCallLog
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
        Me.txtnotes = New System.Windows.Forms.TextBox()
        Me.BtnDone = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.txtwho = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCallType = New System.Windows.Forms.TextBox()
        Me.btnchangecaller = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtnotes
        '
        Me.txtnotes.Location = New System.Drawing.Point(12, 139)
        Me.txtnotes.Multiline = True
        Me.txtnotes.Name = "txtnotes"
        Me.txtnotes.Size = New System.Drawing.Size(568, 161)
        Me.txtnotes.TabIndex = 1
        '
        'BtnDone
        '
        Me.BtnDone.Location = New System.Drawing.Point(505, 313)
        Me.BtnDone.Name = "BtnDone"
        Me.BtnDone.Size = New System.Drawing.Size(75, 23)
        Me.BtnDone.TabIndex = 2
        Me.BtnDone.Text = "Done"
        Me.BtnDone.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 118)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 18)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Notes:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 18)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Date and Time of Call:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "d/M/yyyy h:mm:ss tt"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(265, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(315, 20)
        Me.DateTimePicker1.TabIndex = 0
        '
        'txtwho
        '
        Me.txtwho.Enabled = False
        Me.txtwho.Location = New System.Drawing.Point(265, 48)
        Me.txtwho.Name = "txtwho"
        Me.txtwho.Size = New System.Drawing.Size(225, 20)
        Me.txtwho.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 18)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Who Called:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 18)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Call Type"
        '
        'txtCallType
        '
        Me.txtCallType.Enabled = False
        Me.txtCallType.Location = New System.Drawing.Point(265, 84)
        Me.txtCallType.Name = "txtCallType"
        Me.txtCallType.ReadOnly = True
        Me.txtCallType.Size = New System.Drawing.Size(225, 20)
        Me.txtCallType.TabIndex = 14
        '
        'btnchangecaller
        '
        Me.btnchangecaller.Location = New System.Drawing.Point(505, 45)
        Me.btnchangecaller.Name = "btnchangecaller"
        Me.btnchangecaller.Size = New System.Drawing.Size(75, 23)
        Me.btnchangecaller.TabIndex = 13
        Me.btnchangecaller.Text = "Change"
        Me.btnchangecaller.UseVisualStyleBackColor = True
        '
        'frmCallLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(592, 350)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCallType)
        Me.Controls.Add(Me.btnchangecaller)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtwho)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.BtnDone)
        Me.Controls.Add(Me.txtnotes)
        Me.Name = "frmCallLog"
        Me.Text = "Log A Call"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtnotes As System.Windows.Forms.TextBox
    Friend WithEvents BtnDone As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtwho As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCallType As System.Windows.Forms.TextBox
    Friend WithEvents btnchangecaller As System.Windows.Forms.Button
End Class
