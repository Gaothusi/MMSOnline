Imports System.Windows.Forms

Public Class frmNewIssue
    Public Type, Payment, Approval As String


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Type = CmbTypeOfWork.SelectedItem
        Payment = Cmbpaymenttype.SelectedItem
        Select Case CmbTypeOfWork.SelectedItem
            Case "Standard Service"
                Approval = "N/A"
                Payment = "Standard Service"
            Case Else
                Select Case Cmbpaymenttype.SelectedItem
                    Case "RIG"
                        Approval = "N/A"
                    Case "No Quote Requested"
                        Approval = "N/A"
                    Case "Quote Requested"
                        Approval = "Pending"
                    Case "WARRANTY"
                        Approval = "To Be Processed"
                End Select
        End Select

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CmbTypeOfWork_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbTypeOfWork.SelectedIndexChanged
        Select Case CmbTypeOfWork.SelectedItem
            Case "Standard Service"
                OK_Button.Visible = True
                Label4.Visible = False
                Cmbpaymenttype.ValueMember = False
            Case "Rig - Sterndrive"
                OK_Button.Visible = True
                Cmbpaymenttype.SelectedItem = "RIG"
                Cmbpaymenttype.Visible = True
                Label4.Visible = True
            Case "Rig - Jet"
                OK_Button.Visible = True
                Cmbpaymenttype.SelectedItem = "RIG"
                Cmbpaymenttype.Visible = True
                Label4.Visible = True

            Case "Rig - Outboard"
                OK_Button.Visible = True
                Cmbpaymenttype.SelectedItem = "RIG"
                Cmbpaymenttype.Visible = True
                Label4.Visible = True

            Case "Rig - V-Drive"
                OK_Button.Visible = True
                Cmbpaymenttype.SelectedItem = "RIG"
                Cmbpaymenttype.Visible = True
                Label4.Visible = True

            Case Else
                Cmbpaymenttype.SelectedItem = "Quote Requested"
                Cmbpaymenttype.Visible = True
                Label4.Visible = True
        End Select
        

    End Sub

    Private Sub Cmbpaymenttype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmbpaymenttype.SelectedIndexChanged
        OK_Button.Visible = True
    End Sub
End Class
