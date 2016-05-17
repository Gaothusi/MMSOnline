Public Class frmSalesman
    Public persistant As PreFetch
    Public filter As String
    Private canclose As Boolean

    Private Sub frmSalesman_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = Not canclose
    End Sub



    Private Sub frmSalesman_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        canclose = False

        ListBox1.Items.Add("None")

        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_users, filter) - 1)
            ListBox1.Items.Add(persistant.getvalue(persistant.tbl_users, "name", filter, x))
        Next
        ListBox1.Sorted = True
        For x As Integer = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(x) = "None" Then
                ListBox1.SelectedIndex = x

            End If
        Next

        ListBox1.Sorted = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        persistant.temppass = ListBox1.SelectedItem
        canclose = True
        Me.Close()

    End Sub
End Class