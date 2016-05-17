Imports System.Windows.Forms

Public Class PickStore
    Public persistant As PreFetch
    Public Store As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If listStore.SelectedItems.Count = 0 Then
            MessageBox.Show("Please select a store")
        Else
            If ListStore.SelectedItems.Count > 1 Then
                MessageBox.Show("You may only select one store.")
            Else
                'Once store selected. All good.
                Me.Store = ListStore.SelectedItem.ToString
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub PickStore_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        buildlists()

    End Sub
    Private Sub buildlists()
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
            If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "ADM" Then Me.ListStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Code", "", x))
        Next
    End Sub
End Class
