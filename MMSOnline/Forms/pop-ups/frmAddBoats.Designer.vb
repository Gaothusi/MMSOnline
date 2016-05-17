<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddBoats
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
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.DataView = New System.Windows.Forms.DataGridView
        Me.Location = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.Year = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Make = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.Model = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.HIN = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Unit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Color = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Equipment = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Engine_Make = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.Engine_Model = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Drive_Model = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Trailer_Model = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Trailer_color = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Price = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Discount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Discount_reason = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Comments = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Arrivaldate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Here = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataView)
        Me.SplitContainer1.Size = New System.Drawing.Size(1242, 639)
        Me.SplitContainer1.SplitterDistance = 30
        Me.SplitContainer1.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(185, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(128, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Close"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(40, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(139, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Add Boats To Inventory"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataView
        '
        Me.DataView.ColumnHeadersHeight = 25
        Me.DataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DataView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Location, Me.Year, Me.Make, Me.Model, Me.HIN, Me.Unit, Me.Color, Me.Equipment, Me.Engine_Make, Me.Engine_Model, Me.Drive_Model, Me.Trailer_Model, Me.Trailer_color, Me.Price, Me.Discount, Me.Discount_reason, Me.Comments, Me.Arrivaldate, Me.Here})
        Me.DataView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataView.Location = New System.Drawing.Point(0, 0)
        Me.DataView.Name = "DataView"
        Me.DataView.RowHeadersVisible = False
        Me.DataView.RowHeadersWidth = 30
        Me.DataView.Size = New System.Drawing.Size(1242, 605)
        Me.DataView.TabIndex = 0
        '
        'Location
        '
        Me.Location.HeaderText = "Location"
        Me.Location.Name = "Location"
        Me.Location.Width = 55
        '
        'Year
        '
        Me.Year.HeaderText = "Year"
        Me.Year.Name = "Year"
        Me.Year.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Year.Width = 50
        '
        'Make
        '
        Me.Make.HeaderText = "Make"
        Me.Make.Name = "Make"
        Me.Make.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Make.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Make.Width = 70
        '
        'Model
        '
        Me.Model.HeaderText = "Model"
        Me.Model.Name = "Model"
        Me.Model.Width = 85
        '
        'HIN
        '
        Me.HIN.HeaderText = "HIN"
        Me.HIN.Name = "HIN"
        Me.HIN.Width = 90
        '
        'Unit
        '
        Me.Unit.HeaderText = "Unit #"
        Me.Unit.Name = "Unit"
        Me.Unit.Width = 50
        '
        'Color
        '
        Me.Color.HeaderText = "Color"
        Me.Color.Name = "Color"
        Me.Color.Width = 50
        '
        'Equipment
        '
        Me.Equipment.HeaderText = "Equipment"
        Me.Equipment.Name = "Equipment"
        Me.Equipment.Width = 150
        '
        'Engine_Make
        '
        Me.Engine_Make.HeaderText = "Engine Make"
        Me.Engine_Make.Name = "Engine_Make"
        Me.Engine_Make.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Engine_Make.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Engine_Make.Width = 80
        '
        'Engine_Model
        '
        Me.Engine_Model.HeaderText = "Engine Model"
        Me.Engine_Model.Name = "Engine_Model"
        Me.Engine_Model.Width = 80
        '
        'Drive_Model
        '
        Me.Drive_Model.HeaderText = "Drive Model"
        Me.Drive_Model.Name = "Drive_Model"
        Me.Drive_Model.Width = 80
        '
        'Trailer_Model
        '
        Me.Trailer_Model.HeaderText = "Trailer Model"
        Me.Trailer_Model.Name = "Trailer_Model"
        Me.Trailer_Model.Width = 80
        '
        'Trailer_color
        '
        Me.Trailer_color.HeaderText = "Trailer Color"
        Me.Trailer_color.Name = "Trailer_color"
        Me.Trailer_color.Width = 80
        '
        'Price
        '
        Me.Price.HeaderText = "Price"
        Me.Price.Name = "Price"
        Me.Price.Width = 80
        '
        'Discount
        '
        Me.Discount.HeaderText = "Discount"
        Me.Discount.Name = "Discount"
        Me.Discount.Width = 80
        '
        'Discount_reason
        '
        Me.Discount_reason.HeaderText = "Discount Reason"
        Me.Discount_reason.Name = "Discount_reason"
        '
        'Comments
        '
        Me.Comments.HeaderText = "Comments"
        Me.Comments.Name = "Comments"
        '
        'Arrivaldate
        '
        Me.Arrivaldate.HeaderText = "Est Arrival Date"
        Me.Arrivaldate.Name = "Arrivaldate"
        Me.Arrivaldate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Arrivaldate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Arrivaldate.Width = 60
        '
        'Here
        '
        Me.Here.HeaderText = "Has Arrived"
        Me.Here.Name = "Here"
        Me.Here.Width = 50
        '
        'frmAddBoats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button2
        Me.ClientSize = New System.Drawing.Size(1242, 639)
        Me.ControlBox = False
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmAddBoats"
        Me.Text = "Add Inventory"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataView As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend Shadows WithEvents Location As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Year As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Make As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Model As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HIN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Color As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Equipment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Engine_Make As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Engine_Model As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Drive_Model As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Trailer_Model As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Trailer_color As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Discount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Discount_reason As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Comments As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Arrivaldate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Here As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
