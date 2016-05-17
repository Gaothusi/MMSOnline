Imports System.Data
Imports MySql.Data.MySqlClient
Imports Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices


Public Class frmReport
    'Public Properties
    Public persistant As PreFetch
    Public reporttype As Integer
    Public startDate As Date
    Public endDate As Date
    '       1 = bos and inventory joined
    '       2 = bos and orders
    '       3 = loose items
    '       4 = outstanding finance contracts
    '       5 = commision break down for deal X
    '       6 = Tech Hours - Actual
    '       7 = Tech Hours - Shop Time
    '       8 = Tech Hours - Billable & Shop Time
    '       9 = Tire Tax Collected
    Public bos As Integer
    Public bosstore As String
    Public tech As String = ""
    'Private Vars
    Private items(20) As String

    Private mydatatable As New System.Data.DataTable

    Private Sub frmReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Visible = False
        Dim loading As New frmreportwait
        loading.Show()
        loading.Refresh()
        Me.Refresh()
        runQuery()
        displaytable(mydatatable)
        loading.Close()
        'Me.Visible = True
        Me.Refresh()


    End Sub

    Public Sub runQuery()
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adpt As New MySqlDataAdapter

        conn.ConnectionString = persistant.myconnstring
        cmd.Connection = conn
        conn.Open()

        If reporttype = 1 Then
            cmd.CommandText = "select * from inventory as i Inner Join bos as b on i.control_number = b.control_number Where b.Status > '1' and b.Status < '4' and b.Control_number > 2"
        End If
        If reporttype = 2 Then
            cmd.CommandText = "select * from orders as o Inner Join bos as b on o.ordernumber = b.ordernumber Where b.Status > '1' and b.Status < '4' and b.Control_number = 2"
        End If
        If reporttype = 3 Then
            cmd.CommandText = "select * from bos Where Status > '1' and Status < '4' and Control_number = 1"
        End If
        If reporttype = 4 Then
            cmd.CommandText = "Select b.bos_number as BOS, b.store as Store, " _
       & "b.salesman as Salesman, b.salesman2 as Split, b.buyer1_last as Name, i.boat_year as Year, " _
       & "i.boat_brand as Make, i.boat_model as Model, s.state as 'Status', " _
       & "f.state as 'Finance Status', b.bizman as 'Business Manager', b.date_sold as 'Date Sold', " _
       & "b.date_delivered as 'Date Delivered' from bos as b, " _
       & "inventory as i, status as s, financestatus as f where " _
       & "b.control_number = i.control_number and b.status = s.code and b.finance_status = " _
       & "f.code and b.finance_status <> 7 and b.status > 3 and b.status < 7"
        End If
        If reporttype = 5 Then
            cmd.CommandText = "Select " _
       & "Antitheft_price, Antitheft, Antitheft_SB,	Protech_price, Protech,	Protech_SB,	Ext_warranty_price,	Ext_warranty, Ext_warranty_SB, 20_Hour_price, 20_Hour, 20_Hour_SB, Winterize_price, Winterize, Winterize_SB," _
       & "S_tire_price,	S_tire_SB,	S_tire,	Cover_price,	Cover_SB,	Cover,	Ski_Pack_price,	Ski_Pack_SB,	Ski_Pack,	Rock_Guard_price,	Rock_Guard_SB,	Rock_Guard,	S_Prop_price,	S_Prop_SB,	S_Prop,	Safety_Pkg_price,	Safety_Pkg_SB,	Safety_Pkg,	Lock_Pkg_price,	Lock_Pkg_SB,	Lock_Pkg,	o1_price,	o1_SB,	o1,	o2_price,	o2_SB,	o2,	o3_price,	o3_SB,	o3,	o4_price,	o4_SB,	o4,	o5_price,	o5_SB,	o5,	o6_price,	o6_SB,	o6,	o7_price,	o7_SB,	o7,	o8_price,	o8_SB,	o8 " _
       & "from bos where BOS_number = '" + Me.bos.ToString + "' AND Store = '" + Me.bosstore + "'"
            items(1) = "Antitheft"
            items(2) = "Protech"
            items(3) = "Ext_warranty"
            items(4) = "20_Hour"
            items(5) = "Winterize"
            items(6) = "S_tire"
            items(7) = "Cover"
            items(8) = "Ski_Pack"
            items(9) = "Rock_Guard"
            items(10) = "S_Prop"
            items(11) = "Safety_Pkg"
            items(12) = "Lock_Pkg"
            items(13) = "o1"
            items(14) = "o2"
            items(15) = "o3"
            items(16) = "o4"
            items(17) = "o5"
            items(18) = "o6"
            items(19) = "o7"
            items(20) = "o8"
        End If
        If reporttype = 6 Or reporttype = 7 Then
            cmd.CommandText = "Select swl.Tech,date_format(swl.timestart,'%Y/%m/%d') as 'Date', sum(swl.timeworked) as 'Charged Time',si.Type as 'Issue Type',swo2i.WONumber as 'WO #',sc.name as Customer " & _
                                    "From ServiceWorkLog as swl , ServiceCustomers as sc, ServiceIssue as si, ServiceWOtoIssue as swo2i " & _
                                    "Where  swl.customer = sc.number and swl.issue = si.issue and si.issue = swo2i.issue and " & _
                                        "swl.timeStart between '" & Format(startDate, "yyyy/MM/dd") & "' and '" & Format(endDate, "yyyy/MM/dd") & "'  and " & _
                                        "swl.Tech <> '' and sc.name " & IIf(reporttype = 6, "not", "") & " like 'Shop%' " & _
                                    "Group by swl.Tech,date_format(swl.timeStart,'%Y/%m/%d'),sc.name,si.Type  " & _
                                    "Order by swl.tech,swl.timestart"

        End If
        If reporttype = 8 Then
            Dim whereClause1 As String = ""
            Dim whereClause2 As String = ""
            If Not tech = "" Then
                whereClause1 = " and si.Assigned = '" & tech & "' "
                whereClause2 = " and swl.Tech = '" & tech & "' "
            End If

            cmd.CommandText = "select si.Assigned as 'Tech',date_format(swl.timestart,'%Y/%m/%d') as 'Date' ,si.BillableHrs as 'Billable Time', " & _
                                    "0 as 'Shop Time',si.Type as 'Issue Type',swo2i.WONumber as 'WO #',sc.name as Customer  ,  si.issue as 'issue ID' " & _
                                "from ServiceIssue as si inner join ServiceWorkLog as swl on si.issue = swl.issue,ServiceWOtoIssue as swo2i,ServiceCustomers as sc " & _
                                "where si.issue = swo2i.issue and si.customer = sc.number and swl.timeStart between '" & Format(startDate, "yyyy/MM/dd") & "' and '" & Format(endDate, "yyyy/MM/dd") & "' and " & _
                                    "si.Assigned <> '' and sc.name not like 'Shop%' and si.BillableHrs <> 0 " & whereClause1 & _
                                "group by si.issue  " & _
                            "Union Select swl.Tech as 'Tech',date_format(swl.timestart,'%Y/%m/%d') as 'Date',0 as 'Billable Time', sum(swl.timeworked) as 'Shop Time', " & _
                                    "si.Type as 'Issue Type',swo2i.WONumber as 'WO #',sc.name as Customer, 0 as 'issue ID' " & _
                                "From ServiceWorkLog as swl , ServiceCustomers as sc, ServiceIssue as si, ServiceWOtoIssue as swo2i " & _
                                "Where  swl.customer = sc.number and swl.issue = si.issue and si.issue = swo2i.issue and " & _
                                    "swl.timeStart between '" & Format(startDate, "yyyy/MM/dd") & "' and '" & Format(endDate, "yyyy/MM/dd") & "'  and " & _
                                    "swl.Tech <> '' and sc.name " & IIf(reporttype = 6, "not", "") & " like 'Shop%' " & whereClause2 & _
                                "Group by swl.Tech,date_format(swl.timeStart,'%Y/%m/%d'),sc.name,si.Type  " '& _
            'cmd.CommandText = "Select si.Assigned as 'Tech',date_format(swl.timestart,'%Y/%m/%d') as 'Date', sum(si.BillableHrs) as 'Billable Time',0 as 'Shop Time',si.Type as 'Issue Type',swo2i.WONumber as 'WO #',sc.name as Customer " & _
            '                        "From ServiceWorkLog as swl , ServiceCustomers as sc, ServiceIssue as si, ServiceWOtoIssue as swo2i " & _
            '                        "Where  swl.customer = sc.number and swl.issue = si.issue and si.issue = swo2i.issue and " & _
            '                            "swl.timeStart between '" & Format(startDate, "yyyy/MM/dd") & "' and '" & Format(endDate, "yyyy/MM/dd") & "'  and " & _
            '                            "si.Assigned <> '' and sc.name not like 'Shop%' and si.BillableHrs <> 0 " & whereClause1 & _
            '                        "Group by si.Assigned,date_format(swl.timeStart,'%Y/%m/%d'),sc.name,si.Type  " & _
            '                "Union Select swl.Tech as 'Tech',date_format(swl.timestart,'%Y/%m/%d') as 'Date',0 as 'Billable Time', sum(swl.timeworked) as 'Shop Time',si.Type as 'Issue Type',swo2i.WONumber as 'WO #',sc.name as Customer " & _
            '                        "From ServiceWorkLog as swl , ServiceCustomers as sc, ServiceIssue as si, ServiceWOtoIssue as swo2i " & _
            '                        "Where  swl.customer = sc.number and swl.issue = si.issue and si.issue = swo2i.issue and " & _
            '                            "swl.timeStart between '" & Format(startDate, "yyyy/MM/dd") & "' and '" & Format(endDate, "yyyy/MM/dd") & "'  and " & _
            '                            "swl.Tech <> '' and sc.name " & IIf(reporttype = 6, "not", "") & " like 'Shop%' " & whereClause2 & _
            '                        "Group by swl.Tech,date_format(swl.timeStart,'%Y/%m/%d'),sc.name,si.Type  " '& _

            '"Order by swl.Tech,swl.timestart"

            '"Order by si.Assigned,swl.timestart " & _

        End If
        If reporttype = 9 Then
            cmd.CommandText = "Select BOS_Number as 'BOS #', Store, date_Sold as 'Date Sold', tire_tax as 'Tire Tax' " & _
                            "From bos " & _
                            "Where date_sold between '" & Format(startDate, "yyyy/MM/dd") & "' and '" & Format(endDate, "yyyy/MM/dd") & "' " & _
                                "and tire_tax <> 0 " & _
                            "Order by date_sold"
        End If

        Try
            adpt.SelectCommand = cmd

            adpt.Fill(mydatatable)
            If reporttype = 8 Then
                mydatatable.DefaultView.Sort = "Tech,Date ASC"
                mydatatable = mydatatable.DefaultView.ToTable
            End If
        Catch ex As Exception

        End Try
        Dim empty As Boolean
        If reporttype < 5 Then
            For x As Integer = 0 To mydatatable.Columns.Count - 1
                empty = True
                For y As Integer = 0 To mydatatable.Rows.Count - 1
                    If mydatatable.Rows(y).Item(x).ToString <> "" Then empty = False
                Next
                If empty Then mydatatable.Columns(x).ColumnName = "DELETE ME" + x.ToString
            Next
        End If

        conn.Close()
        adpt.Dispose()
        conn.Dispose()

    End Sub
    Public Sub displaytable(ByRef data As System.Data.DataTable)
        Dim cell As String
        Dim skipped As Integer = 0

        If data.Rows.Count = 0 Then
            MsgBox("There is no data for this report.")
            Exit Sub
        End If
        Select Case reporttype
            Case 1
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
                        If data.Columns(x).ColumnName.ToLower = "price" Or data.Columns(x).ColumnName.ToLower = "discount" Then
                            ws.Range(letterx(x + 1 - skipped) + (2).ToString, letterx(x + 1 - skipped) + (1000).ToString).NumberFormat = "$#,##"
                        End If
                    Else
                        skipped = skipped + 1
                    End If
                Next

                Dim selection As Microsoft.Office.Interop.Excel.Range
                selection = ws.Range("a1").CurrentRegion
                selection.RowHeight = 13

                selection.Sort( _
                    Key1:=selection.Columns.Item("E"), _
                    Order1:=XlSortOrder.xlAscending, _
                    Header:=XlYesNoGuess.xlYes, _
                    OrderCustom:=1, _
                    MatchCase:=False, _
                    Orientation:=XlSortOrientation.xlSortColumns)

                selection.Sort( _
                    Key1:=selection.Columns.Item("D"), _
                    Order1:=XlSortOrder.xlAscending, _
                    Header:=XlYesNoGuess.xlYes, _
                    OrderCustom:=1, _
                    MatchCase:=False, _
                    Orientation:=XlSortOrientation.xlSortColumns)

                selection.Sort( _
                    Key1:=selection.Columns.Item("AB"), _
                    Order1:=XlSortOrder.xlAscending, _
                    Header:=XlYesNoGuess.xlYes, _
                    OrderCustom:=1, _
                    MatchCase:=False, _
                    Orientation:=XlSortOrientation.xlSortColumns)

                'ws.Range("A1").CurrentRegion.Sort(1, XlSortOrder.xlAscending, 2, Nothing, XlSortOrder.xlDescending, 3, XlSortOrder.xlDescending, XlYesNoGuess.xlYes, Nothing, Nothing, XlSortOrientation.xlSortRows)
                ws.Range("A1", "DX1").Font.Bold = True

                ws.PageSetup.Orientation = XlPageOrientation.xlLandscape
                ws.Columns.AutoFit()
                ws.PageSetup.Zoom = False

                ws.PageSetup.FitToPagesTall = 1
                ws.PageSetup.FitToPagesWide = 1
                ws.PageSetup.TopMargin = 0.1
                ws.PageSetup.BottomMargin = 0.1
                ws.PageSetup.LeftMargin = 0.1
                ws.PageSetup.RightMargin = 0.1

                For x As Integer = 0 To 200
                    ws.Columns(letterx(x)).Hidden = True
                Next
                ws.Columns("D").Hidden = False
                ws.Columns("E").Hidden = False
                ws.Columns("H").Hidden = False
                ws.Columns("I").Hidden = False
                ws.Columns("K").Hidden = False
                ws.Columns("L").Hidden = False
                ws.Columns("O").Hidden = False
                ws.Columns("P").Hidden = False
                ws.Columns("S").Hidden = False
                ws.Columns("V").Hidden = False
                ws.Columns("AA").Hidden = False
                ws.Columns("AB").Hidden = False
                ws.Columns("AD").Hidden = False
                persistant.excel.Visible = True
                persistant.excel.Windows(1).ScrollColumn = 1
                Me.Close()


            Case 2
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
                        If data.Columns(x).ColumnName.ToLower = "price" Or data.Columns(x).ColumnName.ToLower = "discount" Then
                            ws.Range(letterx(x + 1 - skipped) + (2).ToString, letterx(x + 1 - skipped) + (1000).ToString).NumberFormat = "$#,##"
                        End If
                    Else
                        skipped = skipped + 1
                    End If
                Next

                Dim selection As Microsoft.Office.Interop.Excel.Range
                selection = ws.Range("a1").CurrentRegion
                selection.RowHeight = 13

                selection.Sort( _
                    Key1:=selection.Columns.Item("G"), _
                    Order1:=XlSortOrder.xlAscending, _
                    Header:=XlYesNoGuess.xlYes, _
                    OrderCustom:=1, _
                    MatchCase:=False, _
                    Orientation:=XlSortOrientation.xlSortColumns)

                selection.Sort( _
                    Key1:=selection.Columns.Item("F"), _
                    Order1:=XlSortOrder.xlAscending, _
                    Header:=XlYesNoGuess.xlYes, _
                    OrderCustom:=1, _
                    MatchCase:=False, _
                    Orientation:=XlSortOrientation.xlSortColumns)

                selection.Sort( _
                    Key1:=selection.Columns.Item("U"), _
                    Order1:=XlSortOrder.xlAscending, _
                    Header:=XlYesNoGuess.xlYes, _
                    OrderCustom:=1, _
                    MatchCase:=False, _
                    Orientation:=XlSortOrientation.xlSortColumns)

                'ws.Range("A1").CurrentRegion.Sort(1, XlSortOrder.xlAscending, 2, Nothing, XlSortOrder.xlDescending, 3, XlSortOrder.xlDescending, XlYesNoGuess.xlYes, Nothing, Nothing, XlSortOrientation.xlSortRows)
                ws.Range("A1", "DX1").Font.Bold = True

                ws.PageSetup.Orientation = XlPageOrientation.xlLandscape
                ws.Columns.AutoFit()
                ws.PageSetup.Zoom = False

                ws.PageSetup.FitToPagesTall = 1
                ws.PageSetup.FitToPagesWide = 1
                ws.PageSetup.TopMargin = 0.1
                ws.PageSetup.BottomMargin = 0.1
                ws.PageSetup.LeftMargin = 0.1
                ws.PageSetup.RightMargin = 0.1


                For x As Integer = 0 To 200
                    ws.Columns(letterx(x)).Hidden = True
                Next
                ws.Columns("B").Hidden = False
                ws.Columns("C").Hidden = False
                ws.Columns("D").Hidden = False
                ws.Columns("E").Hidden = False
                ws.Columns("F").Hidden = False
                ws.Columns("G").Hidden = False
                ws.Columns("J").Hidden = False
                ws.Columns("O").Hidden = False
                ws.Columns("U").Hidden = False
                persistant.excel.Visible = True
                persistant.excel.Windows(1).ScrollColumn = 1
                Me.Close()


            Case 3
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
                        If data.Columns(x).ColumnName.ToLower = "price" Or data.Columns(x).ColumnName.ToLower = "discount" Then
                            ws.Range(letterx(x + 1 - skipped) + (2).ToString, letterx(x + 1 - skipped) + (1000).ToString).NumberFormat = "$#,##"
                        End If
                    Else
                        skipped = skipped + 1
                    End If
                Next

                Dim selection As Microsoft.Office.Interop.Excel.Range
                selection = ws.Range("a1").CurrentRegion
                selection.RowHeight = 13


                'ws.Range("A1").CurrentRegion.Sort(1, XlSortOrder.xlAscending, 2, Nothing, XlSortOrder.xlDescending, 3, XlSortOrder.xlDescending, XlYesNoGuess.xlYes, Nothing, Nothing, XlSortOrientation.xlSortRows)
                ws.Range("A1", "DX1").Font.Bold = True

                ws.PageSetup.Orientation = XlPageOrientation.xlLandscape
                ws.Columns.AutoFit()
                ws.PageSetup.Zoom = False

                ws.PageSetup.FitToPagesTall = 1
                ws.PageSetup.FitToPagesWide = 1
                ws.PageSetup.TopMargin = 0.1
                ws.PageSetup.BottomMargin = 0.1
                ws.PageSetup.LeftMargin = 0.1
                ws.PageSetup.RightMargin = 0.1


                For x As Integer = 0 To 200
                    '   ws.Columns(letterx(x)).Hidden = True
                Next

                persistant.excel.Visible = True
                persistant.excel.Windows(1).ScrollColumn = 1
                Me.Close()


            Case 4
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
                        If data.Columns(x).ColumnName.ToLower = "price" Or data.Columns(x).ColumnName.ToLower = "discount" Then
                            ws.Range(letterx(x + 1 - skipped) + (2).ToString, letterx(x + 1 - skipped) + (1000).ToString).NumberFormat = "$#,##"
                        End If
                    Else
                        skipped = skipped + 1
                    End If
                Next

                Dim selection As Microsoft.Office.Interop.Excel.Range
                selection = ws.Range("a1").CurrentRegion
                selection.RowHeight = 13

                selection.Sort( _
                    Key1:=selection.Columns.Item(13), _
                    Order1:=XlSortOrder.xlDescending, _
                    Header:=XlYesNoGuess.xlYes, _
                    OrderCustom:=1, _
                    MatchCase:=False, _
                    Orientation:=XlSortOrientation.xlSortColumns)


                'ws.Range("A1").CurrentRegion.Sort(1, XlSortOrder.xlAscending, 2, Nothing, XlSortOrder.xlDescending, 3, XlSortOrder.xlDescending, XlYesNoGuess.xlYes, Nothing, Nothing, XlSortOrientation.xlSortRows)
                ws.Range("A1", "DX1").Font.Bold = True

                ws.PageSetup.Orientation = XlPageOrientation.xlLandscape
                ws.Columns.AutoFit()
                ws.PageSetup.Zoom = False

                ws.PageSetup.FitToPagesTall = 1
                ws.PageSetup.FitToPagesWide = 1
                ws.PageSetup.TopMargin = 0.1
                ws.PageSetup.BottomMargin = 0.1
                ws.PageSetup.LeftMargin = 0.1
                ws.PageSetup.RightMargin = 0.1


                For x As Integer = 0 To 200
                    '   ws.Columns(letterx(x)).Hidden = True
                Next

                persistant.excel.Visible = True
                persistant.excel.Windows(1).ScrollColumn = 1
                Me.Close()


            Case 5
                Dim salesmen As New List(Of String)
                Dim temp As String
                For x As Integer = 1 To 20
                    temp = items(x) + "_SB"
                    If data.Rows.Item(0).Item(temp).ToString = "admin" Then
                        data.Rows.Item(0).Item(temp) = "Duncan"
                    End If
                Next


                For x As Integer = 1 To 20
                    temp = items(x) + "_SB"
                    If data.Rows.Item(0).Item(temp).ToString <> "" Then
                        If Not salesmen.Contains(data.Rows.Item(0).Item(temp)) Then
                            salesmen.Add(data.Rows.Item(0).Item(temp))
                        End If
                    End If
                Next

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
                Dim rowcounter As Integer = 1

                For Each salesman As String In salesmen
                    cell = "A" + (rowcounter).ToString
                    ws.Cells.Range(cell, cell).Value() = salesman
                    ws.Range(cell).Font.Bold = True
                    rowcounter = rowcounter + 1

                    For x As Integer = 1 To 12
                        temp = items(x) + "_SB"
                        If data.Rows.Item(0).Item(temp).ToString = salesman Then
                            If Val(data.Rows.Item(0).Item(items(x) + "_price").ToString) <> 0 And Val(data.Rows.Item(0).Item(items(x)).ToString) <> 0 Then
                                cell = "A" + (rowcounter).ToString
                                ws.Cells.Range(cell, cell).Value() = items(x)
                                cell = "B" + (rowcounter).ToString
                                ws.Cells.Range(cell, cell).Value() = Format(Val(data.Rows.Item(0).Item(items(x) + "_price").ToString), "currency")

                                rowcounter = rowcounter + 1
                            End If
                        End If
                    Next
                    For x As Integer = 13 To 20
                        temp = items(x) + "_SB"
                        If data.Rows.Item(0).Item(temp).ToString = salesman Then
                            If Val(data.Rows.Item(0).Item(items(x) + "_price").ToString) <> 0 And data.Rows.Item(0).Item(items(x)).ToString <> "" Then
                                cell = "A" + (rowcounter).ToString
                                ws.Cells.Range(cell, cell).Value() = data.Rows.Item(0).Item(items(x)).ToString
                                cell = "B" + (rowcounter).ToString
                                ws.Cells.Range(cell, cell).Value() = Format(Val(data.Rows.Item(0).Item(items(x) + "_price").ToString), "currency")

                                rowcounter = rowcounter + 1
                            End If
                        End If
                    Next
                Next


                Dim selection As Microsoft.Office.Interop.Excel.Range
                selection = ws.Range("a1").CurrentRegion
                selection.RowHeight = 13


                'ws.Range("A1").CurrentRegion.Sort(1, XlSortOrder.xlAscending, 2, Nothing, XlSortOrder.xlDescending, 3, XlSortOrder.xlDescending, XlYesNoGuess.xlYes, Nothing, Nothing, XlSortOrientation.xlSortRows)
                ws.Range("A1", "DX1").Font.Bold = True

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
                persistant.excel.Windows(1).ScrollColumn = 1
                Me.Close()


            Case 6, 7
                'Tech billable hours
                If persistant.excel Is Nothing Then
                    persistant.excel = New Microsoft.Office.Interop.Excel.Application
                End If
                Dim wb As Microsoft.Office.Interop.Excel.Workbook
                Dim ws As Microsoft.Office.Interop.Excel.Worksheet
                wb = persistant.excel.Workbooks.Add()
                wb.Activate()
                ws = wb.Worksheets.Item(1)

                'Generate the query and insert the data here
                Dim currentTech As String = Trim(data.Rows(1).Item(0).ToString.ToLower)
                'Dim currentTech As String = data.Rows(1).Item(0).ToString.ToLower
                Dim sheetYValue As Integer = 0
                Dim techHoursSum As Double = 0
                'Dim empty As Integer = 0
                Dim sheetNum As Integer = 1
                For y2 As Integer = 0 To data.Rows.Count - 1
                    For x2 As Integer = 0 To data.Columns.Count - 1
                        If sheetYValue = 0 Then
                            'write the header cell
                            cell = letterx(x2 + 1) + (1).ToString
                            'cell = letterx(x2 + 1) + (y2 + 1).ToString
                            ws.Cells.Range(cell, cell).Value = data.Columns(x2).ColumnName
                            If x2 = data.Columns.Count - 1 Then
                                sheetYValue += 1
                                y2 -= 1 'redo the last line
                            End If
                        Else
                            'write the data
                            Debug.WriteLine(currentTech & ":" & data.Rows(y2).Item(0).ToString.ToLower & ":")
                            If currentTech = Trim(data.Rows(y2).Item(0).ToString.ToLower) Then
                                'keep writing this tech data and add the hours
                                If x2 = 2 Then techHoursSum += data.Rows(y2).Item(2)
                                'cell = letterx(x2 + 1) + (y2 + 1 + empty).ToString
                                cell = letterx(x2 + 1) + (sheetYValue + 1).ToString
                                ws.Cells.Range(cell, cell).Value = data.Rows(y2).Item(x2).ToString
                                If x2 = data.Columns.Count - 1 Then sheetYValue += 1

                            Else
                                'write out the total hours
                                'cell = "B" + (y2 + 1 + empty).ToString
                                cell = "B" + (sheetYValue + 1).ToString
                                ws.Cells.Range(cell, cell).Value = "Total"
                                'cell = "C" + (y2 + 1 + empty).ToString
                                cell = "C" + (sheetYValue + 1).ToString
                                ws.Cells.Range(cell, cell).Value = techHoursSum.ToString



                                Dim selection As Microsoft.Office.Interop.Excel.Range
                                selection = ws.Range("a1").CurrentRegion
                                selection.RowHeight = 13

                                ws.Range("A1", "DX1").Font.Bold = True

                                ws.PageSetup.Orientation = XlPageOrientation.xlLandscape
                                ws.Columns.AutoFit()
                                ws.PageSetup.Zoom = False

                                ws.PageSetup.FitToPagesTall = 1
                                ws.PageSetup.FitToPagesWide = 1
                                ws.PageSetup.TopMargin = 0.1
                                ws.PageSetup.BottomMargin = 0.1
                                ws.PageSetup.LeftMargin = 0.1
                                ws.PageSetup.RightMargin = 0.1
                                ws.Name = currentTech

                                ws = wb.Worksheets.Add(After:=wb.Worksheets(sheetNum))
                                sheetNum += 1
                                'ws.Select()

                                'new tech so set values 
                                currentTech = Trim(data.Rows(y2).Item(0).ToString.ToLower)
                                sheetYValue = 0
                                'empty += 3
                                x2 = -1
                                techHoursSum = 0
                            End If
                        End If

                    Next

                Next

                'add total and name to the last sheet
                cell = "B" + (sheetYValue + 1).ToString
                ws.Cells.Range(cell, cell).Value = "Total"
                'cell = "C" + (y2 + 1 + empty).ToString
                cell = "C" + (sheetYValue + 1).ToString
                ws.Cells.Range(cell, cell).Value = techHoursSum.ToString

                Dim selectionLast As Microsoft.Office.Interop.Excel.Range
                selectionLast = ws.Range("a1").CurrentRegion
                selectionLast.RowHeight = 13

                ws.Range("A1", "DX1").Font.Bold = True

                ws.PageSetup.Orientation = XlPageOrientation.xlLandscape
                ws.Columns.AutoFit()
                ws.PageSetup.Zoom = False

                ws.PageSetup.FitToPagesTall = 1
                ws.PageSetup.FitToPagesWide = 1
                ws.PageSetup.TopMargin = 0.1
                ws.PageSetup.BottomMargin = 0.1
                ws.PageSetup.LeftMargin = 0.1
                ws.PageSetup.RightMargin = 0.1
                ws.Name = currentTech





                'For x As Integer = 0 To data.Columns.Count - 1
                '    cell = letterx(x + 1 - skipped) + (1).ToString
                '    ws.Cells.Range(cell, cell).Value() = data.Columns(x).ColumnName
                '    For y As Integer = 0 To data.Rows.Count - 1
                '        cell = letterx(x + 1 - skipped) + (y + 2).ToString
                '        ws.Cells.Range(cell, cell).Value() = data.Rows(y).Item(x).ToString
                '    Next
                '    'If data.Columns(x).ColumnName.ToLower = "price" Or data.Columns(x).ColumnName.ToLower = "discount" Then
                '    '    ws.Range(letterx(x + 1 - skipped) + (2).ToString, letterx(x + 1 - skipped) + (1000).ToString).NumberFormat = "$#,##"
                '    'End If
                'Next

                'Dim selection As Microsoft.Office.Interop.Excel.Range
                'selection = ws.Range("a1").CurrentRegion
                'selection.RowHeight = 13

                'ws.Range("A1", "DX1").Font.Bold = True

                'ws.PageSetup.Orientation = XlPageOrientation.xlLandscape
                'ws.Columns.AutoFit()
                'ws.PageSetup.Zoom = False

                'ws.PageSetup.FitToPagesTall = 1
                'ws.PageSetup.FitToPagesWide = 1
                'ws.PageSetup.TopMargin = 0.1
                'ws.PageSetup.BottomMargin = 0.1
                'ws.PageSetup.LeftMargin = 0.1
                'ws.PageSetup.RightMargin = 0.1

                persistant.excel.Visible = True
                persistant.excel.Windows(1).ScrollColumn = 1
                Me.Close()

            Case 8
                'Tech billable hours
                If persistant.excel Is Nothing Then
                    persistant.excel = New Microsoft.Office.Interop.Excel.Application
                End If
                Dim wb As Microsoft.Office.Interop.Excel.Workbook
                Dim ws As Microsoft.Office.Interop.Excel.Worksheet
                wb = persistant.excel.Workbooks.Add()
                wb.Activate()
                ws = wb.Worksheets.Item(1)

                'Generate the query and insert the data here
                Dim currentTech As String = Trim(data.Rows(1).Item(0).ToString.ToLower)
                'Dim currentTech As String = data.Rows(1).Item(0).ToString.ToLower
                Dim sheetYValue As Integer = 0
                Dim techHoursSum As Double = 0
                Dim shopHoursSum As Double = 0
                'Dim empty As Integer = 0
                Dim sheetNum As Integer = 1
                For y2 As Integer = 0 To data.Rows.Count - 1
                    For x2 As Integer = 0 To data.Columns.Count - 1
                        If sheetYValue = 0 Then
                            'write the header cell
                            cell = letterx(x2 + 1) + (1).ToString
                            'cell = letterx(x2 + 1) + (y2 + 1).ToString
                            ws.Cells.Range(cell, cell).Value = data.Columns(x2).ColumnName
                            If x2 = data.Columns.Count - 1 Then
                                sheetYValue += 1
                                y2 -= 1 'redo the last line
                            End If
                        Else
                            'write the data
                            Debug.WriteLine(currentTech & ":" & data.Rows(y2).Item(0).ToString.ToLower & ":")
                            If currentTech = Trim(data.Rows(y2).Item(0).ToString.ToLower) Then
                                'keep writing this tech data and add the hours
                                If x2 = 2 Then techHoursSum += data.Rows(y2).Item(2)
                                If x2 = 3 Then shopHoursSum += data.Rows(y2).Item(3)
                                'cell = letterx(x2 + 1) + (y2 + 1 + empty).ToString
                                cell = letterx(x2 + 1) + (sheetYValue + 1).ToString
                                ws.Cells.Range(cell, cell).Value = data.Rows(y2).Item(x2).ToString
                                If x2 = data.Columns.Count - 1 Then sheetYValue += 1

                            Else
                                'write out the total hours
                                'cell = "B" + (y2 + 1 + empty).ToString
                                cell = "B" + (sheetYValue + 1).ToString
                                ws.Cells.Range(cell, cell).Value = "Total"
                                'cell = "C" + (y2 + 1 + empty).ToString
                                cell = "C" + (sheetYValue + 1).ToString
                                ws.Cells.Range(cell, cell).Value = techHoursSum.ToString
                                cell = "D" + (sheetYValue + 1).ToString
                                ws.Cells.Range(cell, cell).Value = shopHoursSum.ToString



                                Dim selection As Microsoft.Office.Interop.Excel.Range
                                selection = ws.Range("a1").CurrentRegion
                                selection.RowHeight = 13

                                ws.Range("A1", "DX1").Font.Bold = True

                                ws.PageSetup.Orientation = XlPageOrientation.xlLandscape
                                ws.Columns.AutoFit()
                                ws.PageSetup.Zoom = False

                                ws.PageSetup.FitToPagesTall = 1
                                ws.PageSetup.FitToPagesWide = 1
                                ws.PageSetup.TopMargin = 0.1
                                ws.PageSetup.BottomMargin = 0.1
                                ws.PageSetup.LeftMargin = 0.1
                                ws.PageSetup.RightMargin = 0.1
                                ws.Name = currentTech

                                ws = wb.Worksheets.Add(After:=wb.Worksheets(sheetNum))
                                sheetNum += 1
                                'ws.Select()

                                'new tech so set values 
                                currentTech = Trim(data.Rows(y2).Item(0).ToString.ToLower)
                                sheetYValue = 0
                                'empty += 3
                                x2 = -1
                                techHoursSum = 0
                                shopHoursSum = 0
                            End If
                        End If

                    Next

                Next

                'add total and name to the last sheet
                cell = "B" + (sheetYValue + 1).ToString
                ws.Cells.Range(cell, cell).Value = "Total"
                'cell = "C" + (y2 + 1 + empty).ToString
                cell = "C" + (sheetYValue + 1).ToString
                ws.Cells.Range(cell, cell).Value = techHoursSum.ToString
                cell = "D" + (sheetYValue + 1).ToString
                ws.Cells.Range(cell, cell).Value = shopHoursSum.ToString

                Dim selectionLast As Microsoft.Office.Interop.Excel.Range
                selectionLast = ws.Range("a1").CurrentRegion
                selectionLast.RowHeight = 13

                ws.Range("A1", "DX1").Font.Bold = True

                ws.PageSetup.Orientation = XlPageOrientation.xlLandscape
                ws.Columns.AutoFit()
                ws.PageSetup.Zoom = False

                ws.PageSetup.FitToPagesTall = 1
                ws.PageSetup.FitToPagesWide = 1
                ws.PageSetup.TopMargin = 0.1
                ws.PageSetup.BottomMargin = 0.1
                ws.PageSetup.LeftMargin = 0.1
                ws.PageSetup.RightMargin = 0.1
                ws.Name = currentTech





                'For x As Integer = 0 To data.Columns.Count - 1
                '    cell = letterx(x + 1 - skipped) + (1).ToString
                '    ws.Cells.Range(cell, cell).Value() = data.Columns(x).ColumnName
                '    For y As Integer = 0 To data.Rows.Count - 1
                '        cell = letterx(x + 1 - skipped) + (y + 2).ToString
                '        ws.Cells.Range(cell, cell).Value() = data.Rows(y).Item(x).ToString
                '    Next
                '    'If data.Columns(x).ColumnName.ToLower = "price" Or data.Columns(x).ColumnName.ToLower = "discount" Then
                '    '    ws.Range(letterx(x + 1 - skipped) + (2).ToString, letterx(x + 1 - skipped) + (1000).ToString).NumberFormat = "$#,##"
                '    'End If
                'Next

                'Dim selection As Microsoft.Office.Interop.Excel.Range
                'selection = ws.Range("a1").CurrentRegion
                'selection.RowHeight = 13

                'ws.Range("A1", "DX1").Font.Bold = True

                'ws.PageSetup.Orientation = XlPageOrientation.xlLandscape
                'ws.Columns.AutoFit()
                'ws.PageSetup.Zoom = False

                'ws.PageSetup.FitToPagesTall = 1
                'ws.PageSetup.FitToPagesWide = 1
                'ws.PageSetup.TopMargin = 0.1
                'ws.PageSetup.BottomMargin = 0.1
                'ws.PageSetup.LeftMargin = 0.1
                'ws.PageSetup.RightMargin = 0.1

                persistant.excel.Visible = True
                persistant.excel.Windows(1).ScrollColumn = 1
                Me.Close()
            Case 9
                'tire tax report
                If persistant.excel Is Nothing Then
                    persistant.excel = New Microsoft.Office.Interop.Excel.Application
                End If
                Dim wb As Microsoft.Office.Interop.Excel.Workbook
                Dim ws As Microsoft.Office.Interop.Excel.Worksheet
                wb = persistant.excel.Workbooks.Add()
                wb.Activate()
                ws = wb.Worksheets.Item(1)

                Dim tireTaxTotal As Double = 0
                For x As Integer = 0 To data.Columns.Count - 1
                    cell = letterx(x + 1) + (1).ToString
                    ws.Cells.Range(cell, cell).Value() = data.Columns(x).ColumnName
                    For y As Integer = 0 To data.Rows.Count - 1
                        cell = letterx(x + 1) + (y + 2).ToString
                        ws.Cells.Range(cell, cell).Value() = data.Rows(y).Item(x).ToString
                        'column 3 has the tire tax amount
                        If x = 3 Then tireTaxTotal += data.Rows(y).Item(3)
                    Next
                    'If data.Columns(x).ColumnName.ToLower = "price" Or data.Columns(x).ColumnName.ToLower = "discount" Then
                    '    ws.Range(letterx(x + 1) + (2).ToString, letterx(x + 1) + (1000).ToString).NumberFormat = "$#,##"
                    'End If
                Next
                'add total and name to the last sheet
                cell = "C" + (data.Rows.Count + 2).ToString
                ws.Cells.Range(cell, cell).Value = "Total"
                'cell = "C" + (y2 + 1 + empty).ToString
                cell = "D" + (data.Rows.Count + 2).ToString
                ws.Cells.Range(cell, cell).Value = tireTaxTotal.ToString
                Dim selectionLast As Microsoft.Office.Interop.Excel.Range
                selectionLast = ws.Range("a1").CurrentRegion
                selectionLast.RowHeight = 13

                ws.Range("A1", "DX1").Font.Bold = True

                ws.PageSetup.Orientation = XlPageOrientation.xlLandscape
                ws.Columns.AutoFit()
                ws.PageSetup.Zoom = False

                ws.PageSetup.FitToPagesTall = 1
                ws.PageSetup.FitToPagesWide = 1
                ws.PageSetup.TopMargin = 0.1
                ws.PageSetup.BottomMargin = 0.1
                ws.PageSetup.LeftMargin = 0.1
                ws.PageSetup.RightMargin = 0.1
                ws.Name = "Tire Tax"

                persistant.excel.Visible = True
                persistant.excel.Windows(1).ScrollColumn = 1
                Me.Close()
        End Select


    End Sub
    Public Function formatcell(ByVal input As String, ByVal numeric As Boolean) As String
        Dim temp As String
        temp = input
        If numeric Then temp = Val(input).ToString
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

    Private Sub SplitContainer1_Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub
End Class