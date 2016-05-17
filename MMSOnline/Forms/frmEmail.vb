Imports MySql.Data.MySqlClient
Imports System.Data
Imports MMSOnline.Functions

Public Class frmEmail
    Public persistant As PreFetch
    'Dim loaded, binded, changed, firstloaded As Boolean
    'Private mydata As New DataTable()
    'Dim datatable As New DataTable
    'Private bindingsource1 As New BindingSource

    Private Sub frmEmail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'loaded = False
        'binded = False
        'changed = False
        'firstloaded = True
        buildlists()
    End Sub

    Private Function buildsql() As String

   
        ' Dim fstring As String
        ' Dim sstring As String

        ' fstring = ""
        ' sstring = ""
        ' 'Model
        ' If Me.txtmdlflter.Text <> "" Then
        '     fstring = fstring + " AND i.Boat_Model LIKE '%" + txtmdlflter.Text + "%'"
        ' End If

        ' 'Serial
        ' If Me.txtserfltr.Text <> "" Then
        '     fstring = fstring + " AND i.Boat_HIN LIKE '%" + txtserfltr.Text + "%'"
        ' End If

        ' 'Unit
        ' If Me.txtunitfltr.Text <> "" Then
        '     fstring = fstring + " AND i.Boat_Serial LIKE '%" + txtunitfltr.Text + "%'"
        ' End If

        ' 'BRAND
        ' If cboEngineType.SelectedItem.ToString <> "All" Then
        '     fstring = fstring + " AND i.Boat_Brand LIKE '%" + cboEngineType.SelectedItem.ToString + "%'"
        ' End If

        ' 'STORE
        ' If listStore.Items.Count() <> listStore.CheckedItems.Count() Then
        '     If listStore.CheckedItems.Count() = 0 Then
        '         sstring = "i.Location = 'x'"
        '     Else
        '         sstring = ""
        '         For x As Integer = 0 To (listStore.Items.Count() - 1)
        '             If listStore.GetItemCheckState(x) = CheckState.Checked Then
        '                 If sstring <> "" Then
        '                     sstring = sstring + " OR "
        '                 End If
        '                 sstring = sstring + "i.Location = '" + Storecode(listStore.Items.Item(x)) + "'"
        '             End If
        '         Next x
        '     End If
        '     fstring = fstring + " AND (" + sstring + ")"
        ' End If

        ' 'Year
        ' If listyear.Items.Count() <> listyear.CheckedItems.Count() Then
        '     If listyear.CheckedItems.Count() = 0 Then
        '         sstring = "i.Boat_Year = '0000'"
        '     Else
        '         sstring = ""
        '         For x As Integer = 0 To (listyear.Items.Count() - 1)
        '             If listyear.GetItemCheckState(x) = CheckState.Checked Then
        '                 If sstring <> "" Then
        '                     sstring = sstring + " OR "
        '                 End If
        '                 sstring = sstring + "i.Boat_Year = '" + listyear.Items.Item(x) + "'"
        '             End If
        '         Next x
        '     End If

        '     fstring = fstring + " AND (" + sstring + ")"
        ' End If

        ' 'Status
        ' If liststatus.CheckedItems.Count <> liststatus.Items.Count Then
        '     If liststatus.CheckedItems.Count() = 0 Then
        '         fstring = fstring + "i.State = 'x'"
        '     Else
        '         sstring = ""
        '         For x As Integer = 0 To (liststatus.Items.Count() - 1)
        '             If liststatus.GetItemCheckState(x) = CheckState.Checked Then
        '                 If sstring <> "" Then
        '                     sstring = sstring + " OR "
        '                 End If
        '                 sstring = sstring + "i.State = '" + persistant.getvalue(persistant.tbl_status, "Code", "State = '" + liststatus.Items.Item(x) + "'", 0) + "'"
        '             End If
        '         Next x
        '         fstring = fstring + " AND (" + sstring + ")"
        '     End If
        ' End If


        ' 'HERE
        ' If chkonOrder.Checked <> True Then
        '     fstring = fstring + " AND i.Here = 'YES'"
        ' End If

        ' buildsql = "Select i.location as Location, " _
        '& "i.boat_brand as Make, i.boat_model as Model, i.Engine_model as Engine, i.Drive_model as Drive, " _
        '& "i.trailer_model as Trailer, i.Equipment as Equipment, i.boat_Year as Year, " _
        '& "i.Boat_color as 'Boat Color', i.Trailer_color as 'Trailer Color', " _
        '& "Concat('$',i.price) as 'Price', concat('$',i.discount) as Discount, " _
        '& "i.here as Arrived, i.est_arrival_date as 'Est Arrival Date', i.comments as Comments, i.Boat_HIN as HIN, i.boat_serial as Unit, " _
        '& "s.state as Status, i.Control_Number as CTRL, concat('$',i.Commision) as Commision from inventory as i, status as s where i.state = s.code" _
        '& fstring





    End Function
 


    Private Sub buildlists()
        'Dim mydatatable As New DataTable
        'Dim mydatarow As DataRowView
        'Dim tempstring As String
        ''cboEngineType.Items.Add("All")
        'Dim numberoflines As Integer
        'Dim temptable As New DataGridView
        'Dim b As New BindingSource

        'b.DataSource = persistant.tbl_location
        'b.Filter = "Store = '" + persistant.mystore + "'"
        'temptable.DataSource = b
        'mydatarow = b.Item(0)

        'numberoflines = mydatarow.Item("Num_lines")
        'For y As Integer = 0 To persistant.tbl_location.Columns.Count - 1
        '    If Val(mydatarow.Item(y)) = 1 Then
        '        tempstring = persistant.tbl_location.Columns(y).ColumnName
        '        tempstring = Replace(tempstring, "_", " ")
        '        cboEngineType.Items.Add(tempstring)
        '    End If
        'Next

        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "Prnt = '" + persistant.myparent + "'") - 1)
            If persistant.getvalue(persistant.tbl_location, "Store", "Prnt = '" + persistant.myparent + "'", x) <> "Admin" Then listStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "Prnt = '" + persistant.myparent + "'", x))
        Next

        For x As Integer = 0 To (listStore.Items.Count() - 1)
            listStore.SetItemChecked(x, True)
        Next x

        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "Prnt <> '" + persistant.myparent + "'") - 1)
            If persistant.getvalue(persistant.tbl_location, "Store", "Prnt <> '" + persistant.myparent + "'", x) <> "Admin" Then listStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Store", "Prnt <> '" + persistant.myparent + "'", x))
        Next

        If persistant.mystore = "Admin" Then
            For x As Integer = 0 To (listStore.Items.Count() - 1)
                listStore.SetItemChecked(x, True)
            Next x
        End If

    End Sub
    'Private Function Storecode(ByVal xStore As String) As String
    '    Storecode = persistant.getvalue(persistant.tbl_location, "Code", "Store = '" + xStore + "'", 0)
    'End Function

    Private Sub BtnLocAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLocAll.Click
        For x As Integer = 0 To (listStore.Items.Count - 1)
            listStore.SetItemChecked(x, True)
        Next x
    End Sub

    Private Sub BtnLocNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLocNone.Click
        For x As Integer = 0 To (listStore.Items.Count - 1)
            listStore.SetItemChecked(x, False)
        Next x
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Function Storecode(ByVal xStore As String) As String
        Storecode = persistant.getvalue(persistant.tbl_location, "Code", "Store = '" + xStore + "'", 0)
    End Function

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        If cboEngineType.Text = "" Then
            MessageBox.Show("First select an Engine Type")
            Exit Sub
        End If

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter
        Dim temptable As New DataTable
        Dim strEmails As String = ""

        Dim storeList As String = ""
        For x As Integer = 0 To (listStore.Items.Count() - 1)
            If listStore.GetItemCheckState(x) = CheckState.Checked Then
                If storeList = "" Then
                    storeList = "'" & Storecode(listStore.Items.Item(x)) & "'"
                Else
                    storeList &= ",'" & Storecode(listStore.Items.Item(x)) & "'"
                End If
            End If
        Next x

        If storeList = "" Then
            MessageBox.Show("There are no Stores selected")
            Exit Sub
        End If

        Dim SQL As String = "SELECT CONCAT(trim(BOTH FROM bos.E_Mail),' ')  as eml " & _
                            "FROM bos " & _
                            "WHERE bos.Store IN (" & storeList & ") AND " & _
                                    "bos.motor_make_override = '" & cboEngineType.Text & "' AND " & _
                                    "bos.E_Mail is not null AND " & _
                                    "bos.E_Mail <> '' AND " & _
                                    "INSTR(bos.E_Mail,'@') > 0 AND " & _
                                    "INSTR(trim(both from bos.E_Mail),' ') = 0 " & _
                            "ORDER BY bos.E_Mail ASC"

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            adapter.Fill(temptable)
            If temptable.Rows.Count > 0 Then
                For x As Integer = 0 To temptable.Rows.Count - 1
                    strEmails &= temptable.Rows(x).Item(0) + ";"
                Next
            End If
            txtEmailList.Text = strEmails
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & vbCrLf & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()

    End Sub

    Private Sub btnCopy_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy.Click
        Clipboard.SetText(txtEmailList.Text)
    End Sub

End Class