
Public Class paymentcalc
    Public persistant As PreFetch

    '   Public excel As New Microsoft.Office.Interop.Excel.Application
    '  Dim wb As Microsoft.Office.Interop.Excel.Workbook
    '  Dim ws As Microsoft.Office.Interop.Excel.Worksheet
    ' Dim rng As Microsoft.Office.Interop.Excel.Range
    '  Public Sub Newb()
    '    excel.Visible = False
    '     excel.ScreenUpdating = False
    '   excel.DisplayAlerts = False
    ' wb = excel.Workbooks.Add()
    '  wb.Activate()
    'ws = wb.Worksheets.Item(1)
    '  End Sub


    Public Function payment(ByVal rate As Decimal, ByVal ammort As Integer, ByVal dollers As Decimal) As Decimal
        '        rng = ws.Range("A1")
        '       rng.Value = dollers
        '      rng = ws.Range("A2")
        '     rng.Value = rate / 12
        '    rng = ws.Range("A3")
        '   rng.Value = ammort
        '  rng = ws.Range("C1")
        ' rng.Formula = "=PMT(A2,A3,A1)"

        'payment = Val(rng.Value())

        payment = Financial.Pmt(rate / 12, ammort, dollers) * -1

    End Function
    Public Function residual(ByVal rate As Decimal, ByVal term As Integer, ByVal ammort As Integer, ByVal dollers As Decimal) As Decimal
        '      rng = ws.Range("A1")
        '     rng.Value = dollers
        '    rng = ws.Range("A2")
        '   rng.Value = rate / 12
        '  rng = ws.Range("A3")
        '  rng.Value = ammort
        ' rng = ws.Range("A4")
        ' rng.Value = term
        ' rng = ws.Range("C1")
        ' rng.Formula = "=PMT(A2,A3,A1)"
        ' rng = ws.Range("C2")
        ' rng.Formula = "=FV(A2,A4,C1,A1)"
        ' residual = Val(rng.Value()) * -1

        residual = Financial.FV(rate / 12, term, Financial.Pmt(rate / 12, ammort, dollers), dollers) * -1



    End Function
    '   Public Sub die()
    '       If Not excel.Workbooks Is Nothing Then

    '       For Each wb In excel.Workbooks
    ' For Each ws In wb.Worksheets
    ' System.Runtime.InteropServices.Marshal.ReleaseComObject(ws)
    ' ws = Nothing
    ' Next
    ' wb.Close(False)
    ' System.Runtime.InteropServices.Marshal.ReleaseComObject(wb)
    ' wb = Nothing
    ' Next
    ' excel.Workbooks.Close()
    ' End If
    ' excel.DisplayAlerts = False
    ' excel.Quit()
    'End Sub
    Public Function creditlife(ByVal pmt As Decimal, ByVal term As Integer, ByVal buyers As Integer, ByVal Prov As String) As Decimal
        creditlife = 0
        Dim rate As Decimal = 0
        Dim low, high As Integer
        Dim table As DataTable


        Select Case Prov
            Case "AB"
                table = persistant.tbl_salrates

            Case "BC"
                table = persistant.tbl_salratesbc
        End Select

        For x As Integer = 0 To persistant.howmanyrows(table, "") - 1
            low = Val(persistant.getvalue(table, "Min Term", "", x))
            high = Val(persistant.getvalue(table, "Max Term", "", x))
            If term <= high And term >= low And buyers = 1 Then

                rate = Val(persistant.getvalue(table, "Life Single", "", x))

            End If
            If term <= high And term >= low And buyers = 2 Then
                rate = Val(persistant.getvalue(table, "Life Joint", "", x))
            End If
        Next


        creditlife = pmt * rate



    End Function

    Public Function reslife(ByVal residual As Decimal, ByVal term As Integer, ByVal buyers As Integer, ByVal Prov As String) As Decimal
        Dim table As DataTable


        Select Case Prov
            Case "AB"
                table = persistant.tbl_salrates

            Case "BC"
                table = persistant.tbl_salratesbc
        End Select
        reslife = 0
        Dim rate As Decimal = 0
        Dim low, high As Integer

        For x As Integer = 0 To persistant.howmanyrows(table, "") - 1
            low = Val(persistant.getvalue(table, "Min Term", "", x))
            high = Val(persistant.getvalue(table, "Max Term", "", x))
            If term <= high And term >= low And buyers = 1 Then

                rate = Val(persistant.getvalue(table, "Residual Life Single", "", x))

            End If
            If term <= high And term >= low And buyers = 2 Then
                rate = Val(persistant.getvalue(table, "Residual Life Joint", "", x))
            End If
        Next

        reslife = residual * rate
    End Function

    Public Function creditcritical(ByVal pmt As Decimal, ByVal term As Integer, ByVal buyers As Integer, ByVal Prov As String) As Decimal
        Dim table As DataTable


        Select Case Prov
            Case "AB"
                table = persistant.tbl_salrates

            Case "BC"
                table = persistant.tbl_salratesbc
        End Select

        creditcritical = 0
        Dim rate As Decimal = 0
        Dim low, high As Integer

        For x As Integer = 0 To persistant.howmanyrows(table, "") - 1
            low = Val(persistant.getvalue(table, "Min Term", "", x))
            high = Val(persistant.getvalue(table, "Max Term", "", x))
            If term <= high And term >= low And buyers = 1 Then

                rate = Val(persistant.getvalue(table, "Critical Single", "", x))

            End If
            If term <= high And term >= low And buyers = 2 Then
                rate = Val(persistant.getvalue(table, "Critical Joint", "", x))
            End If
        Next

        creditcritical = pmt * rate
    End Function

    Public Function rescritical(ByVal residual As Decimal, ByVal term As Integer, ByVal buyers As Integer, ByVal Prov As String) As Decimal
        Dim table As DataTable


        Select Case Prov
            Case "AB"
                table = persistant.tbl_salrates

            Case "BC"
                table = persistant.tbl_salratesbc
        End Select

        rescritical = 0
        Dim rate As Decimal = 0
        Dim low, high As Integer

        For x As Integer = 0 To persistant.howmanyrows(table, "") - 1
            low = Val(persistant.getvalue(table, "Min Term", "", x))
            high = Val(persistant.getvalue(table, "Max Term", "", x))
            If term <= high And term >= low And buyers = 1 Then

                rate = Val(persistant.getvalue(table, "Residual Critical Single", "", x))

            End If
            If term <= high And term >= low And buyers = 2 Then
                rate = Val(persistant.getvalue(table, "Residual Critical Joint", "", x))
            End If
        Next

        rescritical = residual * rate
    End Function

    Public Function ah(ByVal pmt As Decimal, ByVal term As Integer, ByVal buyers As Integer, ByVal type As String, ByVal Prov As String) As Decimal
        Dim table As DataTable


        Select Case Prov
            Case "AB"
                table = persistant.tbl_salrates

            Case "BC"
                table = persistant.tbl_salratesbc
        End Select

        ah = 0
        Dim rate As Decimal = 0
        Dim low, high As Integer
        Dim text As String = ""

        If type = "7 Day Retro" Then text = "7 day"
        If type = "14 Day Retro" Then text = "14 day"
        If type = "30 Day Retro" Then text = "30 day"
        If type = "30 Day Elim" Then text = "30 day elim"

        For x As Integer = 0 To persistant.howmanyrows(table, "") - 1
            low = Val(persistant.getvalue(table, "Min Term", "", x))
            high = Val(persistant.getvalue(table, "Max Term", "", x))
            If term <= high And term >= low And buyers = 1 Then

                rate = Val(persistant.getvalue(table, text + " Single", "", x))

            End If
            If term <= high And term >= low And buyers = 2 Then
                rate = Val(persistant.getvalue(table, text + " Joint", "", x))
            End If
        Next

        ah = pmt * rate
    End Function
    Public Function ap(ByVal pmt As Decimal, ByVal term As Integer, ByVal buyers As Integer, ByVal Prov As String) As Decimal
        Dim table As DataTable


        Select Case Prov
            Case "AB"
                table = persistant.tbl_salrates

            Case "BC"
                table = persistant.tbl_salratesbc
        End Select

        ap = 0
        Dim rate As Decimal = 0
        Dim low, high As Integer
        'Dim text As String = ""

        'text = "30 day elim"

        For x As Integer = 0 To persistant.howmanyrows(table, "") - 1
            low = Val(persistant.getvalue(table, "Min Term", "", x))
            high = Val(persistant.getvalue(table, "Max Term", "", x))
            If term <= high And term >= low And buyers = 1 Then

                rate = Val(persistant.getvalue(table, "30 day elim Single Plus", "", x))

            End If
            If term <= high And term >= low And buyers = 2 Then
                rate = Val(persistant.getvalue(table, "30 day elim Joint Plus", "", x))
            End If
        Next

        ap = pmt * rate
    End Function
End Class