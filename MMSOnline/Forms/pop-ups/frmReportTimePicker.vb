Public Class frmReportTimePicker

    Public persistant As PreFetch
    Public reportnum As Integer
    Public tech As String = ""

    Private Sub frmReportTimePicker_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        startDay.Value = Date.Today.AddDays(-14)
        endDay.Value = Date.Today

    End Sub
    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        'DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
        Dim reporter As New frmReport
        reporter.persistant = persistant
        reporter.reporttype = reportnum
        reporter.tech = tech
        reporter.startDate = startDay.Value
        reporter.endDate = endDay.Value
        reporter.Show()
        Me.Close()

    End Sub

End Class