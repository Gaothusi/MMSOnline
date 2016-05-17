Public Class frmload
    Public data As PreFetch
    Private first As Boolean = True



    Private Sub frmload_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmload_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If first Then
            first = False

            data.fetch()

            Dim mainForm As New frmmain
            data.paymentmath.persistant = data
            mainForm.persistant = data
            mainForm.Show()
            Me.Hide()
            Me.Close()
        End If

    End Sub
End Class