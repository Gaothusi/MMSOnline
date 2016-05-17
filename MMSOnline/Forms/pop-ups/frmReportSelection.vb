Public Class frmReportSelection
    Public persistant As PreFetch
    Private reportnum As Integer


    Private Sub rbBOSInventory_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbBOSInventory.Click
        reportnum = 1
    End Sub

    Private Sub RadioButton1_CheckedChanged_1(sender As System.Object, e As System.EventArgs) Handles RadioButton1.Click
        reportnum = 4
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.Click
        'Me.RadioButton1.Checked = False
        'Me.RadioButton2.Checked = True
        'Me.RadioButton3.Checked = False
        reportnum = 2
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.Click
        'Me.RadioButton1.Checked = False
        'Me.RadioButton2.Checked = False
        'Me.RadioButton3.Checked = True
        reportnum = 3
    End Sub

    Private Sub rbTechHours_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbTechHours.Click
        reportnum = 6
    End Sub

    Private Sub rbTechHoursBillable_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbTechHoursBillable.Click
        reportnum = 8
    End Sub

    Private Sub rbTechHoursShop_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbTechHoursShop.Click
        reportnum = 7
    End Sub

    Private Sub rbTireTax_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbTireTax.Click
        reportnum = 9
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If reportnum > 0 Then
            If reportnum > 5 Then
                Dim datePicker As New frmReportTimePicker
                datePicker.persistant = persistant
                datePicker.reportnum = reportnum
                datePicker.Show()
            Else
                Dim reporter As New frmReport
                reporter.persistant = persistant
                reporter.reporttype = reportnum
                reporter.Show()
            End If
            Me.Close()
        End If
    End Sub


End Class