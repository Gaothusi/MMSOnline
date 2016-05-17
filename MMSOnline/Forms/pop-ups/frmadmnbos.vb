<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmadmnbos
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
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.ListBox2 = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(360, 38)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(315, 381)
        Me.ListBox1.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(281, 429)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(129, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Continue"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(12, 38)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(315, 381)
        Me.ListBox2.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Store:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(357, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Salesman:"
        '
        'frmadmnbos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(687, 465)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ListBox1)
        Me.Name = "frmadmnbos"
        Me.Text = "Select Store And Salesman"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label

 
End Class

Public Class frmadmnbos
    Public persistant As PreFetch




    Private Sub frmSalesman_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ListBox1.Items.Add("None")

        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_users, "User <> 'root'") - 1)
            ListBox1.Items.Add(persistant.getvalue(persistant.tbl_users, "name", "User <> 'root'", x))
        Next
        ListBox1.SelectedIndex = 0


        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "store <> 'admin' and store <> 'Atlantis'") - 1)
            ListBox2.Items.Add(persistant.getvalue(persistant.tbl_location, "store", "store <> 'admin' and store <> 'Atlantis'", x))
        Next
        If persistant.myuserLEVEL = 3 Then
            ListBox1.Visible = False
            Label2.Visible = False

        End If
        ListBox1.Sorted = True

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ListBox2.SelectedIndex = -1 Then
            MessageBox.Show("You must select a store for this Bill of Sale.")
        Else
            Dim bos As New frmBOS
            bos.persistant = persistant
            bos.mybosstorecode = persistant.getvalue(persistant.tbl_location, "Code", "store = '" + ListBox2.SelectedItem + "'", 0)
            persistant.temppass = ListBox1.SelectedItem
            If persistant.myuserLEVEL = 3 Then
                persistant.temppass = persistant.myusername
            End If

            bos.Show()
            Me.Close()
        End If


    End Sub
End Class