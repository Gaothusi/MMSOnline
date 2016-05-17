<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServiceSchedule
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
        Dim DrawTool1 As Calendar.DrawTool = New Calendar.DrawTool()
        Me.DayView1 = New Calendar.DayView()
        Me.lblDaysToView = New System.Windows.Forms.Label()
        Me.cboDaysToView = New System.Windows.Forms.ComboBox()
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboStore = New System.Windows.Forms.ComboBox()
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'DayView1
        '
        DrawTool1.DayView = Me.DayView1
        Me.DayView1.ActiveTool = DrawTool1
        Me.DayView1.AmPmDisplay = False
        Me.DayView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DayView1.AppHeightMode = Calendar.DayView.AppHeightDrawMode.TrueHeightAll
        Me.DayView1.DrawAllAppBorder = False
        Me.DayView1.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.DayView1.Location = New System.Drawing.Point(12, 187)
        Me.DayView1.MinHalfHourApp = False
        Me.DayView1.Name = "DayView1"
        Me.DayView1.SelectionEnd = New Date(CType(0, Long))
        Me.DayView1.SelectionStart = New Date(CType(0, Long))
        Me.DayView1.Size = New System.Drawing.Size(995, 475)
        Me.DayView1.StartDate = New Date(CType(0, Long))
        Me.DayView1.TabIndex = 0
        Me.DayView1.Text = "DayView1"
        '
        'lblDaysToView
        '
        Me.lblDaysToView.AutoSize = True
        Me.lblDaysToView.Location = New System.Drawing.Point(835, 16)
        Me.lblDaysToView.Name = "lblDaysToView"
        Me.lblDaysToView.Size = New System.Drawing.Size(79, 13)
        Me.lblDaysToView.TabIndex = 1
        Me.lblDaysToView.Text = "Days To View :"
        '
        'cboDaysToView
        '
        Me.cboDaysToView.FormattingEnabled = True
        Me.cboDaysToView.Items.AddRange(New Object() {"1", "3", "5", "7"})
        Me.cboDaysToView.Location = New System.Drawing.Point(920, 13)
        Me.cboDaysToView.Name = "cboDaysToView"
        Me.cboDaysToView.Size = New System.Drawing.Size(68, 21)
        Me.cboDaysToView.TabIndex = 2
        Me.cboDaysToView.Text = "1"
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.Location = New System.Drawing.Point(12, 13)
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(876, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Store :"
        '
        'cboStore
        '
        Me.cboStore.FormattingEnabled = True
        Me.cboStore.Location = New System.Drawing.Point(920, 40)
        Me.cboStore.Name = "cboStore"
        Me.cboStore.Size = New System.Drawing.Size(68, 21)
        Me.cboStore.TabIndex = 5
        '
        'btnCreate
        '
        Me.btnCreate.Location = New System.Drawing.Point(722, 159)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(130, 23)
        Me.btnCreate.TabIndex = 6
        Me.btnCreate.Text = "Create New"
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(858, 159)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(130, 23)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(376, 142)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(318, 16)
        Me.Label2.TabIndex = 8
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(376, 159)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(318, 16)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "<Nothing Selected>"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(283, 144)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Cusor Location : "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(250, 161)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(120, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Selected Appointment : "
        '
        'frmServiceSchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1019, 674)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.cboStore)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MonthCalendar1)
        Me.Controls.Add(Me.cboDaysToView)
        Me.Controls.Add(Me.lblDaysToView)
        Me.Controls.Add(Me.DayView1)
        Me.Name = "frmServiceSchedule"
        Me.Text = "Service Scheduler"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DayView1 As Calendar.DayView
    Friend WithEvents lblDaysToView As System.Windows.Forms.Label
    Friend WithEvents cboDaysToView As System.Windows.Forms.ComboBox
    Friend WithEvents MonthCalendar1 As System.Windows.Forms.MonthCalendar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboStore As System.Windows.Forms.ComboBox
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
