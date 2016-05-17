Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Data
Imports MMSOnline.Functions
Public Class frmlogpayment
    Public persistant As PreFetch
    Public Store As String
    Public WO As Integer
    Public PO As Integer
    Private today, amount, slip As String
 
    Private Sub frmEditCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = DateTime.Now
        datechanged(DateTimePicker1)
    End Sub

    Private Sub txtleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtslip.TextChanged
        stringchanged(sender)
    End Sub
    Private Sub dateleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        datechanged(sender)
    End Sub

    Private Sub moneyleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtamount.Leave
        moneychanged(sender)
        txtamount.Text = Format(amount, "Currency")
    End Sub
    Private Sub moneychanged(ByRef sender As Object)

        Dim x As String
        Dim y As Decimal
        x = Replace(sender.text, "$", "")
        x = Replace(x, ",", "")
        x = Replace(x, "(", "-")
        x = Replace(x, ")", "")
        y = Val(x)
        amount = y.ToString

    End Sub

    Private Sub datechanged(ByVal sender As Object)
        today = Microsoft.VisualBasic.Strings.Format(sender.Value, "yyyy-MM-dd H:mm:ss")
    End Sub
    Private Sub stringchanged(ByVal sender As Object)
        slip = sender.text
    End Sub
    Public Function timemath(ByVal timein As DateTime, ByVal timeout As DateTime) As Decimal
        Dim span As TimeSpan
        span = timeout - timein
        timemath = (span.TotalMinutes / 60)
    End Function
    Private Sub save()
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        cmd.Connection = conn
        Dim cmdstring As String = "Insert into ServicePayments Set "
        cmdstring = cmdstring + "PO = '" + PO.ToString + "', "
        cmdstring = cmdstring + "WO = '" + WO.ToString + "', "
        cmdstring = cmdstring + "Store = '" + Store + "', "
        cmdstring = cmdstring + "Amount = '" + amount + "', "
        cmdstring = cmdstring + "date = '" + today + "', "
        cmdstring = cmdstring + "CounterSlip = '" + slip + "'"
        cmd.CommandText = cmdstring
        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
        conn.Dispose()
    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If CDec(amount) <> 0 And txtslip.Text <> "" Then
            save()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show("Please try again:  you need to enter a amount and a counter slip number to save a payment")
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
