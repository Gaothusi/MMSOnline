<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmail
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
        Me.BtnLocNone = New System.Windows.Forms.Button()
        Me.BtnLocAll = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboEngineType = New System.Windows.Forms.ComboBox()
        Me.listStore = New System.Windows.Forms.CheckedListBox()
        Me.txtEmailList = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnLocNone
        '
        Me.BtnLocNone.Location = New System.Drawing.Point(126, 78)
        Me.BtnLocNone.Name = "BtnLocNone"
        Me.BtnLocNone.Size = New System.Drawing.Size(48, 23)
        Me.BtnLocNone.TabIndex = 2
        Me.BtnLocNone.Text = "None"
        Me.BtnLocNone.UseVisualStyleBackColor = True
        '
        'BtnLocAll
        '
        Me.BtnLocAll.Location = New System.Drawing.Point(72, 78)
        Me.BtnLocAll.Name = "BtnLocAll"
        Me.BtnLocAll.Size = New System.Drawing.Size(48, 23)
        Me.BtnLocAll.TabIndex = 1
        Me.BtnLocAll.Text = "All"
        Me.BtnLocAll.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSearch.Location = New System.Drawing.Point(18, 273)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(154, 23)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label3.Location = New System.Drawing.Point(20, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 16)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Store:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Location = New System.Drawing.Point(20, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 16)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Engine Type:"
        '
        'cboEngineType
        '
        Me.cboEngineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEngineType.FormattingEnabled = True
        Me.cboEngineType.Items.AddRange(New Object() {"Mercruiser", "Mercury"})
        Me.cboEngineType.Location = New System.Drawing.Point(18, 40)
        Me.cboEngineType.Name = "cboEngineType"
        Me.cboEngineType.Size = New System.Drawing.Size(154, 21)
        Me.cboEngineType.TabIndex = 0
        '
        'listStore
        '
        Me.listStore.CheckOnClick = True
        Me.listStore.FormattingEnabled = True
        Me.listStore.Location = New System.Drawing.Point(18, 102)
        Me.listStore.Name = "listStore"
        Me.listStore.Size = New System.Drawing.Size(154, 154)
        Me.listStore.TabIndex = 3
        '
        'txtEmailList
        '
        Me.txtEmailList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmailList.Location = New System.Drawing.Point(6, 19)
        Me.txtEmailList.Multiline = True
        Me.txtEmailList.Name = "txtEmailList"
        Me.txtEmailList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEmailList.Size = New System.Drawing.Size(507, 236)
        Me.txtEmailList.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.cboEngineType)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.listStore)
        Me.GroupBox1.Controls.Add(Me.BtnLocAll)
        Me.GroupBox1.Controls.Add(Me.BtnLocNone)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(195, 311)
        Me.GroupBox1.TabIndex = 32
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter Settings"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnCopy)
        Me.GroupBox2.Controls.Add(Me.btnClose)
        Me.GroupBox2.Controls.Add(Me.txtEmailList)
        Me.GroupBox2.Location = New System.Drawing.Point(214, 13)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(519, 310)
        Me.GroupBox2.TabIndex = 33
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Results"
        '
        'btnCopy
        '
        Me.btnCopy.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnCopy.Location = New System.Drawing.Point(6, 272)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(101, 23)
        Me.btnCopy.TabIndex = 0
        Me.btnCopy.Text = "Copy"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.Location = New System.Drawing.Point(412, 272)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(101, 23)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmEmail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 335)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmEmail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MMS Online - Email Search"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cboEngineType As System.Windows.Forms.ComboBox
    Friend WithEvents listStore As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents BtnLocNone As System.Windows.Forms.Button
    Friend WithEvents BtnLocAll As System.Windows.Forms.Button
    Friend WithEvents txtEmailList As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnCopy As System.Windows.Forms.Button
End Class
