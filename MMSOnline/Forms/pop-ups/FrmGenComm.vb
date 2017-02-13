Imports System.Windows.Forms
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.IO


Public Class FrmGenComm

    Public persistant As PreFetch
    Private binderset, binderset2 As New BindingSource
    Private dataset, dataset2, userset, storeset As New DataSet
    Public ws As Microsoft.Office.Interop.Excel.Worksheet
    Public earnedperstore(8) As Pair
    Private isLoading As Boolean

    Private Sub FrmGenComm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isLoading = True
        CmbPPB.SelectedItem = "NO"
        PopulateYearCBO()
        cboYear.SelectedItem = Year(Today).ToString
        permissions()
        isLoading = False
    End Sub

    Private Sub PopulateYearCBO()
        For i As Integer = 2011 To Year(Today)
            cboYear.Items.Add(i.ToString)
        Next
    End Sub

    Private Sub permissions()
        If Me.persistant.myuserLEVEL < 9 Then
            btnYTD.Visible = False
            BtnAdjust.Visible = False
            DataGridView1.Visible = False
            DataGridView2.Visible = False
        End If
        If Me.persistant.myuserLEVEL < 4 Then
            cmbgroup.Visible = False
            CmbPPB.Visible = False
            Label1.Visible = False
            Label2.Visible = False
            OK_Button.Text = "Generate"
        End If
        cmbgroup.SelectedItem = "Salesmen"
        CmbPPB.SelectedItem = "NO"
    End Sub
  
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim tempstring As String
        dataset = New DataSet
        If persistant.myuserLEVEL < 9 Then
            For x As Integer = 0 To DataGridView1.Rows.Count - 1
                tempstring = DataGridView1.Item(0, x).Value
                If tempstring = persistant.myuserID Then
                    DataGridView1.Rows(x).Cells(0).Selected = True
                    updatestorelist(tempstring)
                Else
                    DataGridView1.Rows(x).Cells(0).Selected = False
                End If
            Next
        End If

        '  DataGridView1
        If DataGridView1.SelectedCells.Count > 0 Then
            Dim startx As Integer
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            Dim adpt As New MySqlDataAdapter
            ProgressBar1.Value = 0

            If DataGridView2.RowCount > 0 Then
                For storex As Integer = 1 To DataGridView2.RowCount
                    earnedperstore(storex) = New Pair
                    DataGridView2.Rows(storex - 1).Selected = True

                    ProgressBar1.Visible = True
                    Me.Refresh()

                    Try
                        dataset.Tables(0).Clear()
                    Catch ex As Exception
                    End Try
                    Try
                        persistant.excel = New Microsoft.Office.Interop.Excel.Application
                        persistant.excel.Visible = False
                    Catch ex As Exception
                    End Try
                    Try
                        conn.ConnectionString = persistant.myconnstring
                        cmd.Connection = conn
                        conn.Open()
                        Select Case cmbgroup.SelectedItem
                            Case Is = "Business Managers"
                                If DataGridView1.SelectedCells(0).Value.ToString = "Duncan" Or DataGridView1.SelectedCells(0).Value.ToString = "Nikky" Or DataGridView1.SelectedCells(0).Value.ToString = "Wallee" Then
                                    cmd.CommandText = "SELECT BOS_Number, Date_sold, Date_delivered, CONCAT(buyer1_last, ', ', buyer1_first, '  ', buyer2_last, ' ', buyer2_first), RESERVE, financefee, inssold, Antitheft_price, protech_price, PTcost, ext_warranty_price, warranty_cost, BOcommadjust, BOcommadjust_note, 20_Hour_price, Winterize_price, 20_Hour_SB, Winterize_SB from bos where Status IN (7) and YEAR(Date_delivered) = " + cboYear.SelectedItem + " and bizman IN ('Duncan', 'Wallee', 'Nikky') and store = '" + DataGridView2.SelectedCells.Item(0).Value.ToString + "'"
                                Else
                                    cmd.CommandText = "SELECT BOS_Number, Date_sold, Date_delivered, CONCAT(buyer1_last, ', ', buyer1_first, '  ', buyer2_last, ' ', buyer2_first), RESERVE, financefee, inssold, Antitheft_price, protech_price, PTcost, ext_warranty_price, warranty_cost, BOcommadjust, BOcommadjust_note, 20_Hour_price, Winterize_price, 20_Hour_SB, Winterize_SB from bos where Status IN (7) and YEAR(Date_delivered) = " + cboYear.SelectedItem + " and bizman IN ('" + DataGridView1.SelectedCells(0).Value.ToString + "') and store = '" + DataGridView2.SelectedCells.Item(0).Value.ToString + "'"
                                End If
                                persistant.excel.Workbooks.Add(persistant.installdir + "\resources\bizman.xls")
                            Case Is = "Salesmen"
                                cmd.CommandText = "Update bos set salesman2=NULL where salesman2 = ''"
                                cmd.ExecuteNonQuery()
                                cmd.CommandText = "SELECT `bos`.`BOS_number`, `bos`.`Date_Sold`, `bos`.`Date_Delivered`, CONCAT(`bos`.buyer1_last, ', ', `bos`.buyer1_first, '  ', `bos`.buyer2_last, ' ', `bos`.buyer2_first), `inventory`.`Boat_Model`, `inventory`.`Commision`, `bos`.`S_tire_price`, `bos`.`Cover_price`, `bos`.`Ski_Pack_price`, `bos`.`Rock_Guard_price`, `bos`.`S_Prop_price`, `bos`.`Safety_Pkg_price`, `bos`.`Lock_Pkg_price`, `bos`.`20_Hour_price`, `bos`.`Winterize_price`, `bos`.`RESERVE`, `bos`.`Antitheft_price`, `bos`.`Protech_price`, `bos`.`Ext_warranty_price`, `bos`.`misccomm`, `bos`.`commadjust1`, `bos`.`commadjust_note1`, bos.smallitempay, bos.Winterize_SB, bos.20_Hour_SB FROM `bos` Inner Join `inventory` ON `bos`.`Control_Number` = `inventory`.`Control_Number` where commisoned = 'YES' AND Status IN (7) and YEAR(Date_delivered) = " + cboYear.SelectedItem + " and salesman = '" + DataGridView1.SelectedCells(0).Value.ToString + "' and salesman2 IS NULL and store = '" + DataGridView2.SelectedCells.Item(0).Value.ToString + "'"
                                persistant.excel.Workbooks.Add(persistant.installdir + "\resources\sales.xls")
                        End Select
                        adpt.SelectCommand = cmd
                        adpt.Fill(dataset)
                        adpt.Dispose()
                        conn.Close()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                    conn.Dispose()
                    ws = persistant.excel.Workbooks(persistant.excel.Workbooks.Count).Worksheets(1)
                    ws.Name = cboYear.SelectedItem

                    For x As Integer = 1 To dataset.Tables(0).Rows.Count
                        Select Case cmbgroup.SelectedItem
                            Case Is = "Business Managers"
                                For col As Integer = 1 To 12
                                    ws.Range(ColumnLetter(col) + (x + 2).ToString, ColumnLetter(col) + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(col - 1)
                                Next
                                ws.Range("X" + (x + 2).ToString, "X" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(12)
                                ws.Range("AE" + (x + 2).ToString, "AE" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(13)


                                Select Case DataGridView1.SelectedCells(0).Value.ToString
                                    Case "Duncan"
                                        Select Case CDate(dataset.Tables(0).Rows(x - 1).Item(1))
                                            Case Is < CDate("Sep 1, 2010")
                                                ws.Range("AF" + (x + 2).ToString, "AF" + (x + 2).ToString).Value = 0.333
                                            Case Is > CDate("Feb 1, 2011")
                                                ws.Range("AF" + (x + 2).ToString, "AF" + (x + 2).ToString).Value = 0
                                            Case Else
                                                ws.Range("AF" + (x + 2).ToString, "AF" + (x + 2).ToString).Value = 0.5
                                        End Select


                                        If dataset.Tables(0).Rows(x - 1).Item(16).ToString = "Duncan" Or dataset.Tables(0).Rows(x - 1).Item(16).ToString = "admin" Or dataset.Tables(0).Rows(x - 1).Item(16).ToString = "Wallee" Or dataset.Tables(0).Rows(x - 1).Item(16).ToString = "Nikky" Then
                                            ws.Range("Y" + (x + 2).ToString, "Y" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(14)
                                        End If
                                        If dataset.Tables(0).Rows(x - 1).Item(17).ToString = "Duncan" Or dataset.Tables(0).Rows(x - 1).Item(16).ToString = "admin" Or dataset.Tables(0).Rows(x - 1).Item(17).ToString = "Wallee" Or dataset.Tables(0).Rows(x - 1).Item(17).ToString = "Nikky" Then
                                            ws.Range("Z" + (x + 2).ToString, "Z" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(15)
                                        End If
                                    Case "Nikky"
                                        Select Case CDate(dataset.Tables(0).Rows(x - 1).Item(1))
                                            Case Is < CDate("Sep 1, 2010")
                                                ws.Range("AF" + (x + 2).ToString, "AF" + (x + 2).ToString).Value = 0.333
                                            Case Is > CDate("Feb 1, 2011")
                                                ws.Range("AF" + (x + 2).ToString, "AF" + (x + 2).ToString).Value = 0.5
                                            Case Else
                                                ws.Range("AF" + (x + 2).ToString, "AF" + (x + 2).ToString).Value = 0.5
                                        End Select

                                        If dataset.Tables(0).Rows(x - 1).Item(16).ToString = "Duncan" Or dataset.Tables(0).Rows(x - 1).Item(16).ToString = "admin" Or dataset.Tables(0).Rows(x - 1).Item(16).ToString = "Wallee" Or dataset.Tables(0).Rows(x - 1).Item(16).ToString = "Nikky" Then
                                            ws.Range("Y" + (x + 2).ToString, "Y" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(14)
                                        End If
                                        If dataset.Tables(0).Rows(x - 1).Item(17).ToString = "Duncan" Or dataset.Tables(0).Rows(x - 1).Item(16).ToString = "admin" Or dataset.Tables(0).Rows(x - 1).Item(17).ToString = "Wallee" Or dataset.Tables(0).Rows(x - 1).Item(17).ToString = "Nikky" Then
                                            ws.Range("Z" + (x + 2).ToString, "Z" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(15)
                                        End If

                                    Case "Wallee"
                                        Select Case CDate(dataset.Tables(0).Rows(x - 1).Item(1))
                                            Case Is < CDate("Sep 1, 2010")
                                                ws.Range("AF" + (x + 2).ToString, "AF" + (x + 2).ToString).Value = 0.333
                                            Case Is > CDate("Feb 1, 2011")
                                                ws.Range("AF" + (x + 2).ToString, "AF" + (x + 2).ToString).Value = 0.5
                                            Case Else
                                                ws.Range("AF" + (x + 2).ToString, "AF" + (x + 2).ToString).Value = 0
                                        End Select


                                        If dataset.Tables(0).Rows(x - 1).Item(16).ToString = "Duncan" Or dataset.Tables(0).Rows(x - 1).Item(16).ToString = "admin" Or dataset.Tables(0).Rows(x - 1).Item(16).ToString = "Wallee" Or dataset.Tables(0).Rows(x - 1).Item(16).ToString = "Nikky" Then
                                            ws.Range("Y" + (x + 2).ToString, "Y" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(14)
                                        End If
                                        If dataset.Tables(0).Rows(x - 1).Item(17).ToString = "Duncan" Or dataset.Tables(0).Rows(x - 1).Item(16).ToString = "admin" Or dataset.Tables(0).Rows(x - 1).Item(17).ToString = "Wallee" Or dataset.Tables(0).Rows(x - 1).Item(17).ToString = "Nikky" Then
                                            ws.Range("Z" + (x + 2).ToString, "Z" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(15)
                                        End If
                                    Case Else

                                        ws.Range("AF" + (x + 2).ToString, "AF" + (x + 2).ToString).Value = 1
                                        If dataset.Tables(0).Rows(x - 1).Item(16).ToString = DataGridView1.SelectedCells(0).Value.ToString() Then
                                            ws.Range("Y" + (x + 2).ToString, "Y" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(14)
                                        End If
                                        If dataset.Tables(0).Rows(x - 1).Item(17).ToString = DataGridView1.SelectedCells(0).Value.ToString() Then
                                            ws.Range("Z" + (x + 2).ToString, "Z" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(15)
                                        End If

                                End Select

                            Case Is = "Salesmen"
                                For col As Integer = 1 To 20
                                    ws.Range(ColumnLetter(col) + (x + 2).ToString, ColumnLetter(col) + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(col - 1)
                                    If dataset.Tables(0).Columns(col - 1).ColumnName = "Commision" Then
                                        If CDec(Replace(Replace(ws.Range(ColumnLetter(col) + (x + 2).ToString, ColumnLetter(col) + (x + 2).ToString).Value, "$", ""), ",", "")) < 0.01 Then
                                            ws.Range(ColumnLetter(col) + (x + 2).ToString, ColumnLetter(col) + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(22)
                                        End If
                                    End If
                                Next
                                ws.Range("AI" + (x + 2).ToString, "AI" + (x + 2).ToString).Value = 1
                                ws.Range("AJ" + (x + 2).ToString, "AJ" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(20)
                                ws.Range("AO" + (x + 2).ToString, "AO" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1).Item(21)
                                If dataset.Tables(0).Rows(x - 1).Item(22).ToString <> DataGridView1.SelectedCells(0).Value.ToString() Then
                                    ws.Range("O" + (x + 2).ToString, "O" + (x + 2).ToString).Value = 0
                                End If
                                If dataset.Tables(0).Rows(x - 1).Item(23).ToString <> DataGridView1.SelectedCells(0).Value.ToString() Then
                                    ws.Range("N" + (x + 2).ToString, "N" + (x + 2).ToString).Value = 0
                                End If



                        End Select
                    Next
                    Select Case cmbgroup.SelectedItem
                        Case Is = "Business Managers"
                            ws.Range("A" + (dataset.Tables(0).Rows.Count + 4).ToString, "AZ2000").Value = ""
                            ws.Range("AB" + (dataset.Tables(0).Rows.Count + 4).ToString, "AB" + (dataset.Tables(0).Rows.Count + 4).ToString).Formula = "=SUM(AB3:AB" + (dataset.Tables(0).Rows.Count + 3).ToString + ")"
                            ws.Range("AC" + (dataset.Tables(0).Rows.Count + 4).ToString, "AC" + (dataset.Tables(0).Rows.Count + 4).ToString).Formula = "=SUM(AC3:AC" + (dataset.Tables(0).Rows.Count + 3).ToString + ")"
                            ws.Range("AD" + (dataset.Tables(0).Rows.Count + 4).ToString, "AD" + (dataset.Tables(0).Rows.Count + 4).ToString).Formula = "=SUM(AD3:AD" + (dataset.Tables(0).Rows.Count + 3).ToString + ")"

                            earnedperstore(storex).store = DataGridView2.SelectedCells(0).Value.ToString
                            earnedperstore(storex).earned = ws.Range("AB" + (dataset.Tables(0).Rows.Count + 4).ToString, "AB" + (dataset.Tables(0).Rows.Count + 4).ToString).Value2
                            earnedperstore(storex).earnedPPB = ws.Range("AC" + (dataset.Tables(0).Rows.Count + 4).ToString, "AC" + (dataset.Tables(0).Rows.Count + 4).ToString).Value2
                    End Select

                    startx = dataset.Tables(0).Rows.Count

                    Select Case cmbgroup.SelectedItem
                        Case Is = "Salesmen"
                            'Split Deals Where I'm #1
                            dataset.Tables(0).Clear()
                            Try
                                conn.ConnectionString = persistant.myconnstring
                                cmd.Connection = conn
                                conn.Open()
                                cmd.CommandText = "SELECT `bos`.`BOS_number`, `bos`.`Date_Sold`, `bos`.`Date_Delivered`, CONCAT(`bos`.buyer1_last, ', ', `bos`.buyer1_first, '  ', `bos`.buyer2_last, ' ', `bos`.buyer2_first), `inventory`.`Boat_Model`, `inventory`.`Commision`, `bos`.`S_tire_price`, `bos`.`Cover_price`, `bos`.`Ski_Pack_price`, `bos`.`Rock_Guard_price`, `bos`.`S_Prop_price`, `bos`.`Safety_Pkg_price`, `bos`.`Lock_Pkg_price`, `bos`.`20_Hour_price`, `bos`.`Winterize_price`, `bos`.`RESERVE`, `bos`.`Antitheft_price`, `bos`.`Protech_price`, `bos`.`Ext_warranty_price`, `bos`.`misccomm`, `bos`.`commadjust1`, `bos`.`commadjust_note1`, bos.Winterize_SB, bos.20_Hour_SB FROM `bos` Inner Join `inventory` ON `bos`.`Control_Number` = `inventory`.`Control_Number` where commisoned = 'YES' AND Status IN (7) and YEAR(Date_delivered) = " + cboYear.SelectedItem + " and salesman = '" + DataGridView1.SelectedCells(0).Value.ToString + "' and salesman2 IS NOT NULL and store = '" + DataGridView2.SelectedCells.Item(0).Value.ToString + "'"
                                adpt.SelectCommand = cmd
                                adpt.Fill(dataset)
                                adpt.Dispose()
                                conn.Close()
                            Catch ex As Exception
                            End Try
                            conn.Dispose()
                            If dataset.Tables(0).Rows.Count > 0 Then
                                For x As Integer = startx + 1 To startx + dataset.Tables(0).Rows.Count
                                    For col As Integer = 1 To 20
                                        ws.Range(ColumnLetter(col) + (x + 2).ToString, ColumnLetter(col) + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1 - startx).Item(col - 1)
                                        If dataset.Tables(0).Columns(col - 1).ColumnName = "Commision" Then
                                            If CDec(Replace(Replace(ws.Range(ColumnLetter(col) + (x + 2).ToString, ColumnLetter(col) + (x + 2).ToString).Value, "$", ""), ",", "")) < 0.01 Then
                                                ws.Range(ColumnLetter(col) + (x + 2).ToString, ColumnLetter(col) + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1 - startx).Item(22)
                                            End If
                                        End If
                                    Next
                                    ws.Range("AI" + (x + 2).ToString, "AI" + (x + 2).ToString).Value = 0.5
                                    ws.Range("AJ" + (x + 2).ToString, "AJ" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1 - startx).Item(20)
                                    ws.Range("AO" + (x + 2).ToString, "AO" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1 - startx).Item(21)

                                    If dataset.Tables(0).Rows(x - 1 - startx).Item(22).ToString <> DataGridView1.SelectedCells(0).Value.ToString() Then
                                        ws.Range("O" + (x + 2).ToString, "O" + (x + 2).ToString).Value = 0
                                    End If
                                    If dataset.Tables(0).Rows(x - 1 - startx).Item(23).ToString <> DataGridView1.SelectedCells(0).Value.ToString() Then
                                        ws.Range("N" + (x + 2).ToString, "N" + (x + 2).ToString).Value = 0
                                    End If

                                Next
                            End If

                            startx = startx + dataset.Tables(0).Rows.Count
                            '
                            'Split Deals Where I'm #2
                            dataset.Tables(0).Clear()
                            Try
                                conn.ConnectionString = persistant.myconnstring
                                cmd.Connection = conn
                                conn.Open()
                                cmd.CommandText = "SELECT `bos`.`BOS_number`, `bos`.`Date_Sold`, `bos`.`Date_Delivered`, CONCAT(`bos`.buyer1_last, ', ', `bos`.buyer1_first, '  ', `bos`.buyer2_last, ' ', `bos`.buyer2_first), `inventory`.`Boat_Model`, `inventory`.`Commision`, `bos`.`S_tire_price`, `bos`.`Cover_price`, `bos`.`Ski_Pack_price`, `bos`.`Rock_Guard_price`, `bos`.`S_Prop_price`, `bos`.`Safety_Pkg_price`, `bos`.`Lock_Pkg_price`, `bos`.`20_Hour_price`, `bos`.`Winterize_price`, `bos`.`RESERVE`, `bos`.`Antitheft_price`, `bos`.`Protech_price`, `bos`.`Ext_warranty_price`, `bos`.`misccomm`, `bos`.`commadjust1`, `bos`.`commadjust_note1`, bos.Winterize_SB, bos.20_Hour_SB FROM `bos` Inner Join `inventory` ON `bos`.`Control_Number` = `inventory`.`Control_Number` where commisoned = 'YES' AND Status IN (7) and YEAR(Date_delivered) = " + cboYear.SelectedItem + " and salesman2 = '" + DataGridView1.SelectedCells(0).Value.ToString + "' and store = '" + DataGridView2.SelectedCells.Item(0).Value.ToString + "'"
                                adpt.SelectCommand = cmd
                                adpt.Fill(dataset)
                                adpt.Dispose()
                                conn.Close()
                            Catch ex As Exception
                            End Try
                            conn.Dispose()
                            If dataset.Tables(0).Rows.Count > 0 Then
                                For x As Integer = startx + 1 To startx + dataset.Tables(0).Rows.Count
                                    For col As Integer = 1 To 20
                                        ws.Range(ColumnLetter(col) + (x + 2).ToString, ColumnLetter(col) + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1 - startx).Item(col - 1)
                                        If dataset.Tables(0).Columns(col - 1).ColumnName = "Commision" Then
                                            If CDec(Replace(Replace(ws.Range(ColumnLetter(col) + (x + 2).ToString, ColumnLetter(col) + (x + 2).ToString).Value, "$", ""), ",", "")) < 0.01 Then
                                                ws.Range(ColumnLetter(col) + (x + 2).ToString, ColumnLetter(col) + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1 - startx).Item(22)
                                            End If
                                        End If
                                    Next
                                    ws.Range("AI" + (x + 2).ToString, "AI" + (x + 2).ToString).Value = 0.5
                                    ws.Range("AJ" + (x + 2).ToString, "AJ" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1 - startx).Item(20)
                                    ws.Range("AO" + (x + 2).ToString, "AO" + (x + 2).ToString).Value = dataset.Tables(0).Rows(x - 1 - startx).Item(21)

                                    If dataset.Tables(0).Rows(x - 1 - startx).Item(22).ToString <> DataGridView1.SelectedCells(0).Value.ToString() Then
                                        ws.Range("O" + (x + 2).ToString, "O" + (x + 2).ToString).Value = 0
                                    End If
                                    If dataset.Tables(0).Rows(x - 1 - startx).Item(23).ToString <> DataGridView1.SelectedCells(0).Value.ToString() Then
                                        ws.Range("N" + (x + 2).ToString, "N" + (x + 2).ToString).Value = 0
                                    End If

                                Next
                            End If
                            '
                            ws.Range("A" + (startx + dataset.Tables(0).Rows.Count + 4).ToString, "AZ2000").Value = ""
                            ws.Range("AL" + (startx + dataset.Tables(0).Rows.Count + 4).ToString, "AL" + (startx + dataset.Tables(0).Rows.Count + 4).ToString).Formula = "=SUM(AL3:AL" + (startx + dataset.Tables(0).Rows.Count + 3).ToString + ")"
                            ws.Range("AM" + (startx + dataset.Tables(0).Rows.Count + 4).ToString, "AM" + (startx + dataset.Tables(0).Rows.Count + 4).ToString).Formula = "=SUM(AM3:AM" + (startx + dataset.Tables(0).Rows.Count + 3).ToString + ")"
                            ws.Range("AN" + (startx + dataset.Tables(0).Rows.Count + 4).ToString, "AN" + (startx + dataset.Tables(0).Rows.Count + 4).ToString).Formula = "=SUM(AN3:AN" + (startx + dataset.Tables(0).Rows.Count + 3).ToString + ")"

                            earnedperstore(storex).store = DataGridView2.SelectedCells(0).Value.ToString
                            earnedperstore(storex).earned = ws.Range("AL" + (startx + dataset.Tables(0).Rows.Count + 4).ToString, "AL" + (startx + dataset.Tables(0).Rows.Count + 4).ToString).Value2
                            earnedperstore(storex).earnedPPB = ws.Range("AM" + (startx + dataset.Tables(0).Rows.Count + 4).ToString, "AM" + (startx + dataset.Tables(0).Rows.Count + 4).ToString).Value2
                    End Select
                    persistant.excel.Application.DisplayAlerts = False
                    If Not Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Comm") Then Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Comm")
                    Select Case cmbgroup.SelectedItem
                        Case Is = "Business Managers"
                            persistant.excel.Workbooks(persistant.excel.Workbooks.Count).SaveAs((System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Comm\" + DataGridView1.SelectedCells(0).Value.ToString + "-" + DataGridView2.SelectedCells(0).Value.ToString + "-BO.xls").ToString)
                        Case Is = "Salesmen"
                            persistant.excel.Workbooks(persistant.excel.Workbooks.Count).SaveAs((System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Comm\" + DataGridView1.SelectedCells(0).Value.ToString + "-" + DataGridView2.SelectedCells(0).Value.ToString + "-Sales.xls").ToString)
                    End Select

                    persistant.excel.Workbooks(persistant.excel.Workbooks.Count).Close()

                    persistant.excel.Application.DisplayAlerts = True
                    DataGridView2.Rows(storex - 1).Selected = False
                    Dim tempxx As Decimal
                    tempxx = (storex / DataGridView2.RowCount) * 100
                    ProgressBar1.Value = tempxx
                Next
            End If
            ProgressBar1.Visible = False

            persistant.excel.Workbooks.Add(persistant.installdir + "\resources\pay.xls")
            ws = persistant.excel.Workbooks(persistant.excel.Workbooks.Count).Worksheets(2)
            Try
                conn.ConnectionString = persistant.myconnstring
                cmd.Connection = conn
                conn.Open()
                cmd.CommandText = "SELECT Reason, Amount from commadjustments where User = '" + DataGridView1.SelectedCells(0).Value.ToString + "'"
                adpt.SelectCommand = cmd
                adpt.Fill(dataset2)
                adpt.Dispose()
                conn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()
            If dataset2.Tables(0).Rows.Count > 0 Then
                For x As Integer = 1 To dataset2.Tables(0).Rows.Count
                    ws.Range("A" + (x + 1).ToString, "A" + (x + 1).ToString).Value = dataset2.Tables(0).Rows(x - 1).Item(0)
                    ws.Range("B" + (x + 1).ToString, "B" + (x + 1).ToString).Value = dataset2.Tables(0).Rows(x - 1).Item(1)
                Next
            End If

            ws = persistant.excel.Workbooks(persistant.excel.Workbooks.Count).Worksheets(1)
            ws.Range("A1", "A1").Value = "Paid vs Commission " & cboYear.SelectedItem
            'If PreFetch.secure = True Then
            '    conn.ConnectionString = "server=localhost;" _
            '          & "user id=" & persistant.myuserID & ";" _
            '          & "password=" & persistant.password & ";" _
            '          & "database=mysql;port=" & persistant.port
            'Else
            '    conn.ConnectionString = "server=" + persistant.serveraddr + ";" _
            '           & "user id=" & persistant.myuserID & ";" _
            '           & "password=" & persistant.password & ";" _
            '           & "database=mysql;port=3306;pooling=true;"
            'End If

            'If PreFetch.secure = True Then
            '    conn.ConnectionString = "server=localhost;" _
            '          & "user id=Dave;" _
            '          & "password=Filipino;" _
            '          & "database=mysql;port=" & persistant.port
            'Else
            '    conn.ConnectionString = "server=" + persistant.serveraddr + ";" _
            '           & "user id=Dave;" _
            '           & "password=Filipino;" _
            '           & "database=mysql;port=3306;pooling=true;"
            'End If

            If PreFetch.secure = True Then
                conn.ConnectionString = "server=localhost;" _
                      & "user id=MMSData;" _
                      & "password=Filipino;" _
                      & "database=mysql;port=" & persistant.port
            Else
                conn.ConnectionString = "server=" + persistant.serveraddr + ";" _
                       & "user id=MMSData;" _
                       & "password=Filipino;" _
                       & "database=mysql;port=3306;pooling=true;"
            End If
            'If PreFetch.secure = True Then
            '    conn.ConnectionString = "server=localhost;" _
            '          & "user id=root;" _
            '          & "password=Filipino;" _
            '          & "database=mysql;port=" & persistant.port
            'Else
            '    conn.ConnectionString = "server=" + persistant.serveraddr + ";" _
            '           & "user id=root;" _
            '           & "password=Filipino;" _
            '           & "database=mysql;port=3306;pooling=true;"
            'End If

            cmd.Connection = conn
            conn.Open()
            Select Case cmbgroup.SelectedItem
                Case Is = "Business Managers"
                    ws.Range("A2", "A2").Value = DataGridView1.SelectedCells(0).Value.ToString + " - Business Office"
                    cmd.CommandText = "SELECT YTDbo from user where user = '" + DataGridView1.SelectedCells(0).Value.ToString + "'"
                Case Is = "Salesmen"
                    ws.Range("A2", "A2").Value = DataGridView1.SelectedCells(0).Value.ToString + " - Sales"
                    cmd.CommandText = "SELECT YTDsales from user where user = '" + DataGridView1.SelectedCells(0).Value.ToString + "'"
            End Select
            ws.Range("E13", "E13").Value = cmd.ExecuteScalar()
            conn.Close()
            conn.Dispose()
            conn.ConnectionString = persistant.myconnstring
            conn.Open()
            cmd.CommandText = "SELECT SUM(amount) from commadjustments where user = '" + DataGridView1.SelectedCells(0).Value.ToString + "'"
            ws.Range("E11", "E11").Value = cmd.ExecuteScalar()
            conn.Close()
            conn.Dispose()

            For storex As Integer = 1 To DataGridView2.RowCount
                DataGridView2.Rows(storex - 1).Selected = True
                ws.Range("B" + (storex + 3).ToString, "B" + (storex + 3).ToString).Value = earnedperstore(storex).store
                ws.Range("C" + (storex + 3).ToString, "C" + (storex + 3).ToString).Value = earnedperstore(storex).earned
                If CmbPPB.SelectedItem = "YES" Then ws.Range("D" + (storex + 3).ToString, "D" + (storex + 3).ToString).Value = earnedperstore(storex).earnedPPB
                DataGridView2.Rows(storex - 1).Selected = False
            Next

            If Not Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Comm") Then Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Comm")
            persistant.excel.Application.DisplayAlerts = False
            Select Case cmbgroup.SelectedItem
                Case Is = "Business Managers"
                    persistant.excel.Workbooks(persistant.excel.Workbooks.Count).SaveAs((System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Comm\" + DataGridView1.SelectedCells(0).Value.ToString + " - Paid Vs Commision - BO.xls").ToString)
                Case Is = "Salesmen"
                    persistant.excel.Workbooks(persistant.excel.Workbooks.Count).SaveAs((System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Comm\" + DataGridView1.SelectedCells(0).Value.ToString + " - Paid Vs Commision.xls").ToString)
            End Select
            persistant.excel.Application.DisplayAlerts = True

            persistant.excel.Visible = True
            If persistant.myuserLEVEL < 8 Then
                MsgBox("You can view your detailed store by store reports by viewing the excel files in your My Documents/MMS/COMM folder")
            End If
        Else
            If persistant.myuserLEVEL > 8 Then
                MsgBox("Please select a Team Member")
            Else
                MsgBox("Sorry, You don't have any boats marked sold for the year yet")
            End If

        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cboYear_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboYear.SelectedValueChanged
        If Not isLoading Then cmbgroup_Changed(sender, e)
    End Sub

    Private Sub cmbgroup_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbgroup.SelectedValueChanged
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adpt As New MySqlDataAdapter
        If DataGridView2.RowCount > 0 Then storeset.Tables(0).Clear()
        If DataGridView1.RowCount > 0 Then userset = New DataSet
        Try
            conn.ConnectionString = persistant.myconnstring
            cmd.Connection = conn
            conn.Open()
            Select Case cmbgroup.SelectedItem
                Case Is = "Business Managers"
                    cmd.CommandText = "SELECT DISTINCT bizman from bos where Status IN (7) and YEAR(Date_delivered) = " + cboYear.SelectedItem + " and bizman IS NOT NULL and bizman <> ''"
                Case Is = "Salesmen"
                    cmd.CommandText = "( SELECT salesman from bos where Status IN (7) AND YEAR(Date_delivered) = " + cboYear.SelectedItem + " and salesman IS NOT NULL) UNION ( SELECT salesman2 from bos where Status IN (7) AND YEAR(Date_delivered) = " + cboYear.SelectedItem + " and salesman2 IS NOT NULL) ORDER BY salesman"
            End Select
            adpt.SelectCommand = cmd
            adpt.Fill(userset)
            If userset.Tables(0).Rows.Count > 0 Then
                Dim removex As Integer = 99999
                For x As Integer = 0 To userset.Tables(0).Rows.Count - 1
                    If userset.Tables(0).Rows(x).Item(0).ToString = "" Then removex = x
                Next
                If removex <> 99999 Then
                    userset.Tables(0).Rows(removex).Delete()
                End If
            End If

            adpt.Dispose()
            conn.Close()
            binderset.DataSource = userset
            binderset.DataMember = userset.Tables(0).TableName
            DataGridView1.DataSource = binderset
            DataGridView1.AutoResizeColumns()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Dispose()
    End Sub

    Private Sub updatestorelist(ByVal staff As String)
        storeset.Clear()
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adpt As New MySqlDataAdapter
        Try
            conn.ConnectionString = persistant.myconnstring
            cmd.Connection = conn
            conn.Open()
            Select Case cmbgroup.SelectedItem
                Case Is = "Business Managers"
                    cmd.CommandText = "SELECT DISTINCT Store from bos where Status IN (7) and YEAR(Date_delivered) = " + cboYear.SelectedItem
                Case Is = "Salesmen"
                    cmd.CommandText = "SELECT DISTINCT Store from bos where (Salesman = '" + staff + "' AND YEAR(Date_delivered) = " + cboYear.SelectedItem + " and Status IN (7)) OR (Salesman2 = '" + staff + "' AND YEAR(Date_delivered) = " + cboYear.SelectedItem + " and Status IN (7))"
            End Select
            adpt.SelectCommand = cmd
            adpt.Fill(storeset)
            adpt.Dispose()
            conn.Close()
            binderset2.DataSource = storeset
            binderset2.DataMember = storeset.Tables(0).TableName
            DataGridView2.DataSource = binderset2
            DataGridView2.AutoResizeColumns()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Dispose()
    End Sub


    Function ColumnLetter(ByVal ColumnNumber As Integer) As String

        If ColumnNumber > 26 Then



            ' 1st character:  Subtract 1 to map the characters to 0-25,

            '                 but you don't have to remap back to 1-26

            '                 after the 'Int' operation since columns

            '                 1-26 have no prefix letter



            ' 2nd character:  Subtract 1 to map the characters to 0-25,

            '                 but then must remap back to 1-26 after

            '                 the 'Mod' operation by adding 1 back in

            '                 (included in the '65')



            ColumnLetter = Chr(Int((ColumnNumber - 1) / 26) + 64) & Chr(((ColumnNumber - 1) Mod 26) + 65)

        Else

            ' Columns A-Z

            ColumnLetter = Chr(ColumnNumber + 64)

        End If

    End Function

    Private Sub btnYTD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYTD.Click
        If DataGridView1.SelectedCells.Count > 0 Then
            Dim YTD As New frmYTD
            YTD.persistant = persistant
            YTD.user = DataGridView1.SelectedCells(0).Value.ToString
            YTD.group = cmbgroup.SelectedItem
            YTD.ShowDialog()
        Else
            MsgBox("Please select a Team Member")

        End If
    End Sub

   
    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        updatestorelist(DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value)
    End Sub


    Private Sub BtnAdjust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdjust.Click
        If DataGridView1.SelectedCells.Count > 0 Then
            Dim Adjust As New frmcommadjust
            Adjust.persistant = persistant
            Adjust.user = DataGridView1.SelectedCells(0).Value.ToString
            Adjust.ShowDialog()
        Else
            MsgBox("Please select a Team Member")
        End If
    End Sub

End Class

Public Class Pair
    Public store As String
    Public earned As Decimal
    Public earnedPPB As Decimal
End Class
