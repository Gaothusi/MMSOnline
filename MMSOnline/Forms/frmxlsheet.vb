

Imports System.Data
Imports MySql.Data.MySqlClient
Imports Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices



Public Class frmxlsheet
    Public persistant As PreFetch
    Public boats As List(Of Integer)
    Public viewonly As Boolean = False
    Private mydatatable As New System.Data.DataTable

    Private Sub frmtemp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If viewonly Then
            Me.Button1.Visible = False
            Me.Button2.Visible = False
        End If
        downloadboats()
        displaytable(mydatatable)
    End Sub

    Public Sub saveboats()
        Dim e As New equipment
        e.persistant = persistant
        Dim boatmake, boatmodel, motormake, motormodel, equip As String
        boatmake = ""
        boatmodel = ""
        motormake = ""
        motormodel = ""
        equip = ""


        Me.AxSpreadsheet1.Cells(1, 1).Select()

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        conn.Open()
        cmd.Connection = conn


        Dim row, xcol As Integer
        Dim invstring, invrstring, cell As String
        Dim done As Boolean = False
        Dim cols As New List(Of colinfo)

        xcol = 1

        While done = False
            cell = letterx(xcol) + "1"
            Dim col As New colinfo
            col.colnum = letterx(xcol)
            col.header = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
            If col.header <> "" Then
                If isinv(col.header) Then col.table = "inventory"
                If isinvr(col.header) Then col.table = "invrestricted"
                If col.header.ToLower <> "control_number" Then cols.Add(col)
                col.numberic = False
                If col.header = "here" Then col.numberic = True
                If col.header = "est_arrival_date" Then col.numberic = True
                If col.header = "Invoice_Date" Then col.numberic = True
                If col.header = "GE_paid_date" Then col.numberic = True
                If col.header = "Free_lnterest_Until" Then col.numberic = True
                If col.header = "Maturity_Date" Then col.numberic = True
                If col.header = "Boat_Reg_date" Then col.numberic = True
                If col.header = "Motor_Reg_Date" Then col.numberic = True
                If col.header = "price" Then col.numberic = True
                If col.header = "discount" Then col.numberic = True
                If col.header = "Boat_US_cost" Then col.numberic = True
                If col.header = "TRL_US_cost" Then col.numberic = True
                If col.header = "FRT_US_cost" Then col.numberic = True
                If col.header = "TOTAL_US_cost" Then col.numberic = True
                If col.header = "GE_X_Rate" Then col.numberic = True
                If col.header = "EST_Can_Cost" Then col.numberic = True
                If col.header = "Canadian_cost" Then col.numberic = True
                If col.header = "Inv_Move_to_Cost_of_Goods_sold" Then col.numberic = True
                If col.header = "Remaining_CDN_cost_inv" Then col.numberic = True
                If col.header = "GE_Ammount_Financed" Then col.numberic = True
                If col.header = "GE_Amt_Paid" Then col.numberic = True
                If col.header = "GE_amt_Owing" Then col.numberic = True
                If col.header = "Rebate" Then col.numberic = True
                If col.header = "Boat_year" Then col.numberic = True
                If col.header = "engine_year" Then col.numberic = True
                If col.header = "trailer_year" Then col.numberic = True
                If col.header = "state" Then col.numberic = True
                If col.header = "bos_num" Then col.numberic = True
                If col.header = "unit_cost" Then col.numberic = True
                If col.header = "unit_value" Then col.numberic = True
                If col.header = "commision" Then col.numberic = True
            Else
                done = True
            End If
            xcol = xcol + 1
        End While

        row = 2
        invstring = ""
        invrstring = ""
        Dim tempstr As String

        While Me.AxSpreadsheet1.Cells.Range("A" + row.ToString, "A" + row.ToString).Text().ToString <> "" 
            invstring = "update inventory set "
            invrstring = "update invrestricted set "
            For Each col As colinfo In cols

                cell = col.colnum + row.ToString

                If col.header.ToLower = "boat_brand" Then boatmake = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                If col.header.ToLower = "boat_model" Then boatmodel = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                If col.header.ToLower = "engine_make" Then motormake = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                If col.header.ToLower = "engine_model" Then motormodel = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                If col.header.ToLower = "equipment" Then equip = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value <> Nothing Then
                    If col.table = "inventory" Then
                        If col.header.ToLower = "est_arrival_date" Then
                            Try
                                tempstr = Format(CDate(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value), "yyyy-MM-dd")
                                invstring = invstring + col.header + " = '" + tempstr + "', "
                            Catch ex As Exception
                                invstring = invstring + col.header + " = NULL, "
                            End Try
                        Else
                            invstring = invstring + col.header + " = " + formatcell(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value(), col.numberic) + ", "
                        End If

                    End If
                    If col.table = "invrestricted" Then
                        invrstring = invrstring + col.header + " = " + formatcell(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value(), col.numberic) + ", "

                    End If
                End If

            Next
            If boatmake <> "" And boatmodel <> "" Then e.boat(boatmake, boatmodel)
            If motormake <> "" And motormodel <> "" Then e.motor(motormake, motormodel)
            e.check(equip)
            invstring = invstring.Remove(invstring.Length - 2, 2)
            invrstring = invrstring.Remove(invrstring.Length - 2, 2)
            invstring = invstring + " where control_number = '" + Me.AxSpreadsheet1.Cells.Range("A" + row.ToString, "A" + row.ToString).Value().ToString + "'"
            invrstring = invrstring + " where control_number = '" + Me.AxSpreadsheet1.Cells.Range("A" + row.ToString, "A" + row.ToString).Value().ToString + "'"
            'update row
            Try
                cmd.CommandText = invstring
                cmd.ExecuteNonQuery()
                cmd.CommandText = invrstring
                cmd.ExecuteNonQuery()

            Catch ex As Exception
            End Try

            row = row + 1
        End While

        row = 2
        invstring = ""
        invrstring = ""
        Dim hereval As String
        Dim statespecififed As Boolean

        While Me.AxSpreadsheet1.Cells.Range("B" + row.ToString, "B" + row.ToString).Text().ToString <> ""
            If Me.AxSpreadsheet1.Cells.Range("A" + row.ToString, "A" + row.ToString).Text().ToString = "" Then
                statespecififed = False

                invstring = "insert into inventory set "
                   For Each col As colinfo In cols

                    cell = col.colnum + row.ToString
              
                    If col.header.ToLower = "here" Then
                        hereval = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value
                        If hereval = Nothing Then hereval = "yes"

                        If hereval.ToLower = "no" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "NO"
                        Else
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "YES"
                        End If
                    End If

                    If col.header.ToLower = "state" Then
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value <> Nothing Then statespecififed = True
                    End If

                    If col.header.ToLower = "boat_brand" Then
                        'Fix brand Case and spelling if you can
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "four winns" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Four Winns"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "fourwinns" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Four Winns"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "maxum" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Maxum"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "bayliner" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Bayliner"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "searay" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Searay"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "hewescraft" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Hewescraft"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "centurion" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Centurion"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "monterey" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Monterey"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "montarey" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Monterey"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "monteray" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Monterey"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "glastron" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Glastron"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "sylvan" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Sylvan"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "smokercraft" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Smokercraft"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "used" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Used"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "odyssey" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Odyssey"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "double eagle" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Double Eagle"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "cdory" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "C-Dory"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "c dory" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "C-Dory"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "c-dory" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "C-Dory"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "campion" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Campion"
                        End If
                    End If

                    If col.header.ToLower = "boat_brand" Then boatmake = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                    If col.header.ToLower = "boat_model" Then boatmodel = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                    If col.header.ToLower = "engine_make" Then motormake = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                    If col.header.ToLower = "engine_model" Then motormodel = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                    If col.header.ToLower = "equipment" Then equip = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                    If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value <> Nothing Then
                        If col.table = "inventory" Then
                            If col.header.ToLower = "est_arrival_date" Then
                                Try
                                    tempstr = Format(CDate(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value), "yyyy-MM-dd")
                                    invstring = invstring + col.header + " = '" + tempstr + "', "
                                Catch ex As Exception
                                    invstring = invstring + col.header + " = NULL, "
                                End Try
                            Else
                                invstring = invstring + col.header + " = " + formatcell(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value(), col.numberic) + ", "
                            End If
                        End If
                    End If

                Next
                If boatmake <> "" And boatmodel <> "" Then e.boat(boatmake, boatmodel)
                If motormake <> "" And motormodel <> "" Then e.motor(motormake, motormodel)
                e.check(equip)
                invstring = invstring.Remove(invstring.Length - 2, 2)
                If statespecififed = False Then invstring = invstring + ", State = '1'"
                  'update row
                Try
                    cmd.CommandText = invstring
                    Debug.WriteLine(invstring)

                    cmd.ExecuteNonQuery()

                Catch ex As MySqlException
                    Debug.WriteLine(ex.Message)
                End Try

            End If
      

            row = row + 1
        End While


        'Create missing invres records

        If True Then
            Dim numinvrecord, numinvrrecord As Integer
            Try
                'conn.ConnectionString = persistant.myconnstring
                cmd.Connection = conn
                cmd.CommandText = "Select max(control_number) from inventory"
                numinvrecord = Val(cmd.ExecuteScalar())
                cmd.CommandText = "Select max(control_number) from invrestricted"
                numinvrrecord = Val(cmd.ExecuteScalar())
                If numinvrecord <> numinvrrecord Then
                    For x As Integer = (numinvrrecord + 1) To numinvrecord
                        Try
                            cmd.CommandText = "INSERT into invrestricted SET Control_number = '" + x.ToString + "'"
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception
                            MessageBox.Show("ERROR: " + ex.Message)
                        End Try
                    Next
                End If
            Catch ex As Exception

            End Try
        End If
        Try
            conn.Close()
        Catch ex As MySqlException
            Debug.WriteLine(ex.Message)
        End Try


        conn.Close()
        conn.Dispose()
    End Sub
    Public Sub downloadboats()
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adpt As New MySqlDataAdapter

        Dim bn As Integer
        Dim cmdselectionx As String

        conn.ConnectionString = persistant.myconnstring
        cmd.Connection = conn
        conn.Open()

        If viewonly Then
            cmdselectionx = ""
            For x As Integer = 0 To boats.Count - 1
                cmdselectionx = cmdselectionx + "control_number='" + boats.Item(x).ToString + "' OR "
            Next
            cmdselectionx = Mid(cmdselectionx, 1, cmdselectionx.Length - 4)
            cmd.CommandText = "select Boat_Brand, Boat_Model, Engine_Model, Drive_Model, Trailer_Model, Trailer_Color, Equipment, Boat_Color, Price, Discount, Discount_Reason, Boat_Year, Location, Boat_Serial, Boat_HIN, Comments, Here, Est_Arrival_date from inventory Where " + cmdselectionx
        Else
            cmdselectionx = ""
            For x As Integer = 0 To boats.Count - 1
                cmdselectionx = cmdselectionx + "i.control_number='" + boats.Item(x).ToString + "' OR "
            Next
            cmdselectionx = Mid(cmdselectionx, 1, cmdselectionx.Length - 4)
            cmd.CommandText = "select * from inventory as i Inner Join invrestricted as ir on i.control_number = ir.control_number Where " + cmdselectionx
        End If
        adpt.SelectCommand = cmd
        adpt.Fill(mydatatable)
        Dim empty As Boolean

        If viewonly Then
            For x As Integer = 0 To mydatatable.Columns.Count - 1
                empty = True
                For y As Integer = 0 To mydatatable.Rows.Count - 1
                    If mydatatable.Rows(y).Item(x).ToString <> "" Then empty = False
                Next
                If empty Then mydatatable.Columns(x).ColumnName = "DELETE ME" + x.ToString
            Next
        End If

        conn.Close()
        conn.Dispose()
        adpt.Dispose()
        For x As Integer = 0 To mydatatable.Columns.Count - 1

            If mydatatable.Columns(x).ColumnName.ToLower = "control_number1".ToLower Then
                bn = x
            End If
        Next

        If bn <> 0 Then mydatatable.Columns.RemoveAt(bn)
    End Sub
    Public Sub displaytable(ByRef data As System.Data.DataTable)
        Dim cell As String
        Dim skipped As Integer = 0

        If viewonly = False Then
            For x As Integer = 0 To data.Columns.Count - 1
                cell = letterx(x + 1 - skipped) + (1).ToString
                If Mid(data.Columns(x).ColumnName.ToString, 1, 9) <> "DELETE ME" Then
                    Me.AxSpreadsheet1.Cells.Range(cell, cell).Value() = data.Columns(x).ColumnName
                    For y As Integer = 0 To data.Rows.Count - 1
                        cell = letterx(x + 1 - skipped) + (y + 2).ToString
                        Me.AxSpreadsheet1.Cells.Range(cell, cell).Value() = data.Rows(y).Item(x).ToString
                    Next
                Else
                    skipped = skipped + 1
                End If
            Next

            Me.AxSpreadsheet1.Columns.AutoFit()
        End If

        If viewonly Then

            If persistant.excel Is Nothing Then
                persistant.excel = New Microsoft.Office.Interop.Excel.Application
            End If
            Dim wb As Microsoft.Office.Interop.Excel.Workbook
            Dim ws As Microsoft.Office.Interop.Excel.Worksheet
            'wb = Me.AxSpreadsheet1.Workbooks.Item(1)
            'ws = wb.Sheets.Item(0)
            wb = persistant.excel.Workbooks.Add()
            wb.Activate()
            ws = wb.Worksheets.Item(1)

            For x As Integer = 0 To data.Columns.Count - 1
                cell = letterx(x + 1 - skipped) + (1).ToString
                If Mid(data.Columns(x).ColumnName.ToString, 1, 9) <> "DELETE ME" Then
                    ws.Cells.Range(cell, cell).Value() = data.Columns(x).ColumnName
                    For y As Integer = 0 To data.Rows.Count - 1
                        cell = letterx(x + 1 - skipped) + (y + 2).ToString
                        ws.Cells.Range(cell, cell).Value() = data.Rows(y).Item(x).ToString
                    Next
                    If data.Columns(x).ColumnName.ToLower = "price" Or data.Columns(x).ColumnName.ToLower = "discount" Or data.Columns(x).ColumnName.ToLower = "unit_cost" Or data.Columns(x).ColumnName.ToLower = "unit_value" Then
                        ws.Range(letterx(x + 1 - skipped) + (2).ToString, letterx(x + 1 - skipped) + (1000).ToString).NumberFormat = "$#,##"
                    End If
                Else
                    skipped = skipped + 1
                End If
            Next

            Dim selection As Microsoft.Office.Interop.Excel.Range
            selection = ws.Range("a1").CurrentRegion


            For c As Integer = 3 To 1 Step -1
                selection.Sort( _
                    Key1:=selection.Columns.Item(c), _
                    Order1:=XlSortOrder.xlAscending, _
                    Header:=XlYesNoGuess.xlYes, _
                    OrderCustom:=1, _
                    MatchCase:=False, _
                    Orientation:=XlSortOrientation.xlSortColumns)
            Next c

            'ws.Range("A1").CurrentRegion.Sort(1, XlSortOrder.xlAscending, 2, Nothing, XlSortOrder.xlDescending, 3, XlSortOrder.xlDescending, XlYesNoGuess.xlYes, Nothing, Nothing, XlSortOrientation.xlSortRows)
            ws.Range("A1", "X1").Font.Bold = True

            ws.PageSetup.Orientation = XlPageOrientation.xlLandscape
            ws.Columns.AutoFit()
            ws.PageSetup.Zoom = False

            ws.PageSetup.FitToPagesTall = 1
            ws.PageSetup.FitToPagesWide = 1
            ws.PageSetup.TopMargin = 0.1
            ws.PageSetup.BottomMargin = 0.1
            ws.PageSetup.LeftMargin = 0.1
            ws.PageSetup.RightMargin = 0.1

            persistant.excel.Visible = True
            Me.Close()
        End If

    End Sub
    Public Function formatcell(ByVal input As String, ByVal numeric As Boolean) As String
        Dim temp As String
        temp = input
        If numeric Then temp = Val(input).ToString
        temp = Replace(temp, "'", "\'")
        temp = Replace(temp, ";", "\;")
        formatcell = "'" + temp + "'"

        If input = "" Then formatcell = "null"

    End Function
    Public Function letterx(ByVal input As Integer) As String
        Dim alphabit, tempresult As String
        Dim x As Integer

        alphabit = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        tempresult = ""
        If input > 26 Then
            x = (input - (input Mod 26)) / 26
            If input Mod 26 = 0 Then
                tempresult = Mid(alphabit, x - 1, 1)
            Else
                tempresult = Mid(alphabit, x, 1)
            End If

        End If
        x = input Mod 26
        If x = 0 Then x = 26

        tempresult = tempresult + Mid(alphabit, x, 1)

        letterx = tempresult
    End Function
    Public Function isinv(ByVal col As String) As Boolean
        isinv = persistant.invcols.Contains(col.ToLower)
    End Function
    Public Function isinvr(ByVal col As String) As Boolean
        isinvr = persistant.invrcols.Contains(col.ToLower)
    End Function


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        '   TextBox2.Text = letterx(Val(TextBox1.Text))

        '  TextBox2.Text = Me.AxSpreadsheet1.Cells.Range(TextBox1.Text, TextBox1.Text).Value
        saveboats()
        '  For Each x As String In persistant.invcols
        'MessageBox.Show(x)
        'Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.AxSpreadsheet1.Columns.AutoFit()
    End Sub


End Class

Class colinfo
    Public header As String
    Public table As String
    Public colnum As String
    Public numberic As Boolean
End Class