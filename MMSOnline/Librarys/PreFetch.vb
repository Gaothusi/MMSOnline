Imports MySql.Data.MySqlClient
Imports System.Data
Imports Tamir.SharpSsh.jsch
Imports Microsoft.Win32
Public Class PreFetch
    Public paymentmath As New paymentcalc
    Public tbl_location, tbl_banks, tbl_boats, tbl_financestatus, tbl_salrates, tbl_salratesbc, tbl_userlevels As New DataTable
    Public tbl_motors, tbl_shopstatus, tbl_equip, tbl_status, tbl_motorbrands, tbl_users, tbl_temp_SWO_Users, tbl_printdata, tbl_Certsframe As New DataTable
    Public tbl_years, invcols, invrcols As New List(Of String)
    Public myuserID, myusername, myconnstring, mystore, mystoreCODE, myparent, temppass, password, serveraddr As String
    Public myuserLEVEL As Integer
    Public jschx As New JSch
    Public tunnel As Session
    Public port As Integer
    Public installdir, ActiveTech As String
    Public openBOSs As New List(Of frmBOS)
    Public excel As Microsoft.Office.Interop.Excel.Application

    Public Shared secure As Boolean = True

    Public Sub fetch()
        ' Dim rk As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\MMS\MMSonline")
        ' installdir = rk.GetValue("install_dir")
        ' rk.Close()
        installdir = Application.StartupPath
        'MessageBox.Show(installdir)

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adpt As New MySqlDataAdapter
        Dim temptable As New DataTable
        Try
            conn.ConnectionString = myconnstring
            cmd.Connection = conn
            conn.Open()
            'Get Store Code
            cmd.CommandText = "SELECT Code FROM location WHERE Store = '" + mystore + "'"
            mystoreCODE = cmd.ExecuteScalar
            'Get Parent Company
            cmd.CommandText = "SELECT Prnt from location where Store = '" + mystore + "'"
            myparent = cmd.ExecuteScalar
            'Get location table
            cmd.CommandText = "SELECT * from location"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_location)
            'Get banks table
            cmd.CommandText = "SELECT * from banks"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_banks)
            'Get boats table
            cmd.CommandText = "SELECT * from boats"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_boats)
            'Get financestatus table
            cmd.CommandText = "SELECT * from financestatus"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_financestatus)
            'Get motors table
            cmd.CommandText = "SELECT * from motors"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_motors)
            'Get shopstatus table
            cmd.CommandText = "SELECT * from shopstatus"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_shopstatus)
            'Get shorttolong table
            cmd.CommandText = "SELECT * from equip"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_equip)
            'Get status table
            cmd.CommandText = "SELECT * from status"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_status)

            cmd.CommandText = "SELECT * from salrates2012"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_salrates)

            cmd.CommandText = "SELECT * from salratesbc2012"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_salratesbc)

            cmd.CommandText = "SELECT * from userlevels"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_userlevels)

            cmd.CommandText = "SELECT * from printdata"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_printdata)

            cmd.CommandText = "SELECT * from certsframe"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_Certsframe)


            cmd.CommandText = "SELECT DISTINCT brand from motors order by type"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_motorbrands)

            cmd.CommandText = "SELECT DISTINCT boat_Year FROM inventory ORDER BY `boat_Year` DESC"
            adpt.SelectCommand = cmd
            adpt.Fill(temptable)
            For x As Integer = 0 To temptable.Rows.Count - 1
                tbl_years.Add(temptable.Rows(x).Item(0).ToString)
            Next

            If myuserLEVEL > 4 Then
                temptable.Reset()

                cmd.CommandText = "SELECT * FROM inventory where control_number = 1"
                adpt.SelectCommand = cmd
                adpt.Fill(temptable)
                For x As Integer = 0 To temptable.Columns.Count - 1
                    invcols.Add(temptable.Columns(x).ColumnName.ToLower)
                Next
                temptable.Reset()

                cmd.CommandText = "SELECT * FROM invrestricted where control_number = 1"
                adpt.SelectCommand = cmd
                adpt.Fill(temptable)
                For x As Integer = 0 To temptable.Columns.Count - 1
                    invrcols.Add(temptable.Columns(x).ColumnName.ToLower)
                Next
            End If
            adpt.Dispose()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        conn.Dispose()
    End Sub
    Public Sub update()
        'Updates boats, motors, and short2long
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adpt As New MySqlDataAdapter
        Dim temptable As New DataTable

        Try

            conn.ConnectionString = myconnstring
            cmd.Connection = conn
            conn.Open()

            tbl_boats.Clear()
            tbl_motors.Clear()
            tbl_equip.Clear()
            tbl_years.Clear()
            tbl_motorbrands.Clear()

            'Get boats table
            cmd.CommandText = "SELECT * from boats"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_boats)

            'Get motors table
            cmd.CommandText = "SELECT * from motors"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_motors)

            'Get shorttolong table
            cmd.CommandText = "SELECT * from equip"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_equip)

            cmd.CommandText = "SELECT DISTINCT brand from motors order by type"
            adpt.SelectCommand = cmd
            adpt.Fill(tbl_motorbrands)

            cmd.CommandText = "SELECT DISTINCT boat_Year FROM inventory"
            adpt.SelectCommand = cmd
            adpt.Fill(temptable)
            For x As Integer = 0 To temptable.Rows.Count - 1
                tbl_years.Add(temptable.Rows(x).Item(0).ToString)
            Next
            conn.Close()
            '        If myuserLEVEL > 5 Then
            '            tbl_users.Clear()
            '            Dim tempconstring = "server=localhost;" _
            '& "user id=" & myuserID & ";" _
            '& "password=" & password & ";" _
            '& "database=mysql;port=" & port
            If myuserLEVEL > 5 Then
                tbl_users.Clear()
                'Dim tempconstring = "server=localhost;" _
                '    & "user id=Dave;" _
                '    & "password=Filipino;" _
                '    & "database=mysql;port=" & port
                Dim tempconstring = "server=localhost;" _
                    & "user id=root;" _
                    & "password=Filipino;" _
                    & "database=mysql;port=" & port
                'Dim tempconstring = "server=localhost;" _
                '     & "user id=root;" _
                '     & "password=Filipino;" _
                '     & "database=mysql;port=" & port


                conn.ConnectionString = tempconstring
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = "SELECT user, usergroup, store, name from user"
                adpt.SelectCommand = cmd
                adpt.Fill(tbl_users)
                adpt.Dispose()
                conn.Close()
            End If
        Catch ex As Exception
        End Try
        conn.Dispose()
    End Sub
    Public Function getvalue(ByVal table As DataTable, ByVal col As String, ByVal filter As String, ByVal row As Integer) As String
        Dim temptable As New DataGridView
        Dim temprow As DataRowView

        Dim colnumber As Integer
        Dim b As New BindingSource
        b.DataSource = table
        b.Filter = filter
        temptable.DataSource = b

        For x As Integer = 0 To (table.Columns.Count - 1)
            If table.Columns(x).ColumnName.ToLower = col.ToLower Then colnumber = x
        Next

        '  MessageBox.Show(colnumber.ToString)
        '  getvalue = temptable.Item(colnumber, row).Value.ToString
        If b.Count < row Or b.Count = 0 Then
            getvalue = ""
        Else
            temprow = b.Item(row)
            getvalue = temprow.Item(colnumber).ToString()
        End If
    End Function
    Public Function howmanyrows(ByVal table As DataTable, ByVal filter As String) As Integer
        howmanyrows = 0
        Dim b As New BindingSource
        b.DataSource = table
        b.Filter = filter
        howmanyrows = b.Count
    End Function

End Class

