<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogWork
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.timestart = New System.Windows.Forms.DateTimePicker()
        Me.timeend = New System.Windows.Forms.DateTimePicker()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnAssign = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAssignedTo = New System.Windows.Forms.TextBox()
        Me.btnStartStop = New System.Windows.Forms.Button()
        Me.lblTimerVal = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblTimerRunning = New System.Windows.Forms.Label()
        Me.lblT = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(331, 493)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
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
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'timestart
        '
        Me.timestart.CustomFormat = "M/d/yyyy h:mm tt"
        Me.timestart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timestart.Location = New System.Drawing.Point(12, 79)
        Me.timestart.Name = "timestart"
        Me.timestart.Size = New System.Drawing.Size(173, 20)
        Me.timestart.TabIndex = 13
        '
        'timeend
        '
        Me.timeend.CustomFormat = "M/d/yyyy h:mm tt"
        Me.timeend.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timeend.Location = New System.Drawing.Point(250, 79)
        Me.timeend.Name = "timeend"
        Me.timeend.Size = New System.Drawing.Size(173, 20)
        Me.timeend.TabIndex = 14
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(12, 154)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(462, 333)
        Me.txtNotes.TabIndex = 56
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(9, 133)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(118, 18)
        Me.Label26.TabIndex = 57
        Me.Label26.Text = "Work Perfomed:"
        Me.Label26.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 18)
        Me.Label1.TabIndex = 58
        Me.Label1.Text = "Start Time:"
        Me.Label1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(247, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 18)
        Me.Label2.TabIndex = 59
        Me.Label2.Text = "End Time:"
        Me.Label2.Visible = False
        '
        'btnAssign
        '
        Me.btnAssign.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAssign.Location = New System.Drawing.Point(274, 108)
        Me.btnAssign.Name = "btnAssign"
        Me.btnAssign.Size = New System.Drawing.Size(140, 22)
        Me.btnAssign.TabIndex = 68
        Me.btnAssign.Text = "Change"
        Me.btnAssign.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(110, 18)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "Work Done By:"
        '
        'txtAssignedTo
        '
        Me.txtAssignedTo.Enabled = False
        Me.txtAssignedTo.Location = New System.Drawing.Point(128, 110)
        Me.txtAssignedTo.Name = "txtAssignedTo"
        Me.txtAssignedTo.Size = New System.Drawing.Size(140, 20)
        Me.txtAssignedTo.TabIndex = 66
        '
        'btnStartStop
        '
        Me.btnStartStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartStop.Location = New System.Drawing.Point(15, 12)
        Me.btnStartStop.Name = "btnStartStop"
        Me.btnStartStop.Size = New System.Drawing.Size(140, 32)
        Me.btnStartStop.TabIndex = 69
        Me.btnStartStop.Text = "Start Timer"
        Me.btnStartStop.UseVisualStyleBackColor = True
        '
        'lblTimerVal
        '
        Me.lblTimerVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimerVal.Location = New System.Drawing.Point(168, 12)
        Me.lblTimerVal.Name = "lblTimerVal"
        Me.lblTimerVal.Size = New System.Drawing.Size(100, 32)
        Me.lblTimerVal.TabIndex = 70
        Me.lblTimerVal.Text = "0 mins"
        Me.lblTimerVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'lblTimerRunning
        '
        Me.lblTimerRunning.AutoSize = True
        Me.lblTimerRunning.Location = New System.Drawing.Point(282, 13)
        Me.lblTimerRunning.Name = "lblTimerRunning"
        Me.lblTimerRunning.Size = New System.Drawing.Size(76, 13)
        Me.lblTimerRunning.TabIndex = 71
        Me.lblTimerRunning.Text = "Timer Running"
        Me.lblTimerRunning.Visible = False
        '
        'lblT
        '
        Me.lblT.AutoSize = True
        Me.lblT.Location = New System.Drawing.Point(425, 24)
        Me.lblT.Name = "lblT"
        Me.lblT.Size = New System.Drawing.Size(13, 13)
        Me.lblT.TabIndex = 72
        Me.lblT.Text = "0"
        Me.lblT.Visible = False
        '
        'frmLogWork
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(489, 534)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblT)
        Me.Controls.Add(Me.lblTimerRunning)
        Me.Controls.Add(Me.lblTimerVal)
        Me.Controls.Add(Me.btnStartStop)
        Me.Controls.Add(Me.btnAssign)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtAssignedTo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.timeend)
        Me.Controls.Add(Me.timestart)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLogWork"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Work Log"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents timestart As System.Windows.Forms.DateTimePicker
    Friend WithEvents timeend As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnAssign As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAssignedTo As System.Windows.Forms.TextBox
    Friend WithEvents btnStartStop As System.Windows.Forms.Button
    Friend WithEvents lblTimerVal As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblTimerRunning As System.Windows.Forms.Label
    Friend WithEvents lblT As System.Windows.Forms.Label

End Class
