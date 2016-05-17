Imports System.Windows.Forms

Public Class FrmPCL
    Public hull, motor As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If ListHull.SelectedItems.Count > 0 And ListMotor.SelectedItems.Count > 0 Then
            hull = ListHull.SelectedItem
            motor = ListMotor.SelectedItem
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            MsgBox("Please select both the type of motor and hull")
        End If
      
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
