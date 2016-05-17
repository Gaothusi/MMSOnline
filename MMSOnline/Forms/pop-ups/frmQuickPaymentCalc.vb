Imports System.Windows.Forms
Imports MMSOnline.Functions

Public Class frmQuickPaymentCalc
    Public persistant As PreFetch
    Private loaded As Boolean = False
    Private prov As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmQuickPaymentCalc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Set Down = 0
        TXTdown.Text = "$0.00"
        TxtAmm.Text = "15"
        TxtRate.Text = "7"
        prov = persistant.getvalue(persistant.tbl_location, "Prov", "Code = '" + persistant.mystoreCODE + "'", 0)
        If prov = "BC" Then
            ChkPST.Checked = True
        End If
        loaded = True
        domath()
    End Sub
    Private Sub domath()
        Dim Price, Adds, Down, Rate, term, Amm, TOTAL, TotalWithTax, INS, AmmountFinanced, Pmt As Decimal
        Dim goodtogo As Boolean
        goodtogo = True

        ' update Fee
        If ChkFee.Checked = True Then
            TxtFee.Text = Format("299", "Currency")
            TOTAL = TOTAL + 299
        Else
            TxtFee.Text = Format("0", "Currency")
        End If

        'add finance Fee $199
        TOTAL = TOTAL + 199

        ' check that all 4 numbers calculate
        Try
            Dim x As String
            x = Replace(TxtPrice.Text, "$", "")
            x = Replace(x, ",", "")
            x = Replace(x, "(", "-")
            x = Replace(x, ")", "")
            Price = Val(x)

            x = Replace(TXTdown.Text, "$", "")
            x = Replace(x, ",", "")
            x = Replace(x, "(", "-")
            x = Replace(x, ")", "")
            Down = Val(x)

            x = Replace(TxtRate.Text, "%", "")
            x = Replace(x, ",", "")
            x = Replace(x, "(", "-")
            x = Replace(x, ")", "")
            Rate = Val(x) / 100

            x = Replace(TxtAmm.Text, "y", "")
            x = Replace(x, ",", "")
            x = Replace(x, "(", "-")
            x = Replace(x, ")", "")
            Amm = Val(x)

            x = Replace(txtAdds.Text, "$", "")
            x = Replace(x, ",", "")
            x = Replace(x, "(", "-")
            x = Replace(x, ")", "")
            Adds = Val(x)

            If ChkAutomode.Checked Then

                Select Case Price - Down
                    Case Is < 7500
                        TxtRate.Text = "0"
                        Rate = 0
                    Case 7500 To 22999.99
                        TxtRate.Text = "7.5"
                        Rate = 0.0679
                    Case 23000 To 39999.99
                        TxtRate.Text = "7.39"
                        Rate = 0.0693
                    Case Is >= 40000
                        TxtRate.Text = "7.29"
                        Rate = 0.0693
                End Select

                If Price - Down >= 25000 Then
                    TxtAmm.Text = "20"
                    Amm = 20
                Else
                    TxtAmm.Text = "15"
                    Amm = 15
                End If
            End If

            If Amm = 0 Then goodtogo = False
            If Rate = 0 Then goodtogo = False



        Catch ex As Exception
            goodtogo = False

        End Try

        If ChkAutomode.Checked Then
            TxtRate.Enabled = False
            TxtAmm.Enabled = False
        Else
            TxtRate.Enabled = True
            TxtAmm.Enabled = True
        End If

        If goodtogo Then
            If Amm > 5 Then
                term = 60
                Amm = Amm * 12

            Else
                Amm = Amm * 12
                term = Amm
            End If
            TOTAL = TOTAL + Price + Adds
            ' if yes do GST, PST math
            If ChkGST.Checked = True Then
                TxtGST.Text = Format(TOTAL * GSTRate, "currency")
                TotalWithTax = TOTAL * (1 + GSTRate)
            Else
                TxtGST.Text = Format(0, "currency")
                TotalWithTax = TOTAL
            End If
            If ChkGSTdown.Checked = True Then
                TXTdown.Enabled = False
                TXTdown.Text = Format((TotalWithTax - TOTAL), "currency")
            Else
                TXTdown.Enabled = True
            End If
            If ChkPST.Checked = True Then
                TxtPST.Text = Format(TOTAL * PSTRate, "currency")
                TotalWithTax = TotalWithTax + (TOTAL * PSTRate)
            Else
                TxtPST.Text = Format(0, "currency")
            End If
            ' calc insurance
            AmmountFinanced = TotalWithTax - Down
            If ChkIns.Checked Then
                Dim last_dollerstotal As Decimal = 0
                Dim res, monthlypmt As Decimal
                While last_dollerstotal <> AmmountFinanced
                    last_dollerstotal = AmmountFinanced
                    Pmt = FormatNumber(persistant.paymentmath.payment(Rate, Amm, AmmountFinanced), 2)
                    res = FormatNumber(persistant.paymentmath.residual(Rate, term, Amm, AmmountFinanced), 2)
                    monthlypmt = FormatNumber(Pmt * term, 2)
                    INS = persistant.paymentmath.creditlife(monthlypmt, term, 1, prov) + persistant.paymentmath.reslife(res, term, 1, prov) + persistant.paymentmath.ah(monthlypmt, term, 1, "7 Day Retro", prov)
                    AmmountFinanced = TotalWithTax - Down + INS
                End While
                TxtIns.Text = Format(INS, "currency")
            Else
                INS = 0
                TxtIns.Text = Format(0, "currency")
            End If
            AmmountFinanced = TotalWithTax - Down + INS
            TxtTotal.Text = Format(AmmountFinanced, "currency")
            ' do payment math
            Pmt = persistant.paymentmath.payment(Rate, Amm, AmmountFinanced)
            TxtPmt.Text = Format(Pmt, "currency")
        Else
            ' if no set GST, PST, Insurance, Total, Payment = 0
            TxtGST.Text = "$0.00"
            TxtPST.Text = "$0.00"
            TxtIns.Text = "$0.00"
            TxtPmt.Text = "$0.00"
            If ChkGSTdown.Checked Then TXTdown.Text = "$0.00"
        End If
    End Sub

    Private Sub TxtRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdds.TextChanged, TxtRate.TextChanged, TxtPrice.TextChanged, TxtAmm.TextChanged, TXTdown.TextChanged
        If loaded Then domath()
    End Sub
    Private Sub ChkIns_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAutomode.CheckStateChanged, ChkFee.CheckStateChanged, ChkPST.CheckStateChanged, ChkIns.CheckStateChanged, ChkGST.CheckStateChanged, ChkGSTdown.CheckStateChanged
        If loaded Then domath()
    End Sub

    Private Sub BtnClipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClipboard.Click
        Clipboard.SetText(TxtPmt.Text)
    End Sub
End Class
