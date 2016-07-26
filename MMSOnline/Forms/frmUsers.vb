Imports MySql.Data.MySqlClient

Public Class frmUsers
    Public persistant As PreFetch

    Private connstring As String
    Private mode As Integer
    '1 = add
    '2 = remove
    '3 = modify
    Private filterusers As String


    Private Sub frmUsers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        filllists()
        'If PreFetch.secure = True Then
        '    connstring = "server=localhost;" _
        '          & "user id=" & persistant.myuserID & ";" _
        '          & "password=" & persistant.password & ";" _
        '          & "database=mysql;port=" & persistant.port
        'Else
        '    connstring = "server=" + persistant.serveraddr + ";" _
        '           & "user id=" & persistant.myuserID & ";" _
        '           & "password=" & persistant.password & ";" _
        '           & "database=mysql;port=3306;pooling=true;"
        'End If


        'If PreFetch.secure = True Then
        '    connstring = "server=localhost;" _
        '          & "user id=Dave;" _
        '          & "password=Filipino;" _
        '          & "database=mysql;port=" & persistant.port
        'Else
        '    connstring = "server=" + persistant.serveraddr + ";" _
        '           & "user id=Dave;" _
        '           & "password=Filipino;" _
        '           & "database=mysql;port=3306;pooling=true;"
        'End If

        If PreFetch.secure = True Then
            connstring = "server=localhost;" _
                  & "user id=MMSData;" _
                  & "password=Filipino;" _
                  & "database=mms;port=" & persistant.port
        Else
            connstring = "server=" + persistant.serveraddr + ";" _
                   & "user id=MMSData;" _
                   & "password=Filipino;" _
                   & "database=mms;port=3306;pooling=true;"
        End If
        'If PreFetch.secure = True Then
        '    connstring = "server=localhost;" _
        '          & "user id=root;" _
        '          & "password=Filipino;" _
        '          & "database=mysql;port=" & persistant.port
        'Else
        '    connstring = "server=" + persistant.serveraddr + ";" _
        '           & "user id=root;" _
        '           & "password=Filipino;" _
        '           & "database=mysql;port=3306;pooling=true;"
        'End If

        'If persistant.myuserID = "admin" Or persistant.myuserID = "Dave" Then
        '    Button1.Visible = True
        'End If

    End Sub
    Private Sub filllists()
        ListStore.Items.Add("All")
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
            ListStore.Items.Add(persistant.getvalue(persistant.tbl_location, "store", "", x))
            cmbstore.Items.Add(persistant.getvalue(persistant.tbl_location, "store", "", x))
        Next
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_userlevels, "") - 1)
            cmblevel.Items.Add(persistant.getvalue(persistant.tbl_userlevels, "level", "", x))
        Next
    End Sub

    Private Sub draw()
        'Add
        If mode = 1 Then
            Me.Panel1.Visible = False
            Me.Panel2.Visible = True
            btnremove.Visible = False
            Me.btnDoIt.Text = "Add User"
            Me.txtlogin.ReadOnly = False
        End If

        'Remove
        If mode = 2 Then
            Me.Panel1.Visible = True
            Me.Panel2.Visible = False
            btnremove.Visible = True
        End If

        'Modify
        If mode = 3 Then
            Me.Panel1.Visible = True
            Me.Panel2.Visible = True
            btnremove.Visible = False
            Me.btnDoIt.Text = "Modify User"
            Me.txtlogin.ReadOnly = True

        End If
    End Sub
    Private Function clean(ByVal input As String) As String
        Dim temp As String
        temp = Replace(input, "'", "")
        clean = Replace(temp, ";", "")
    End Function

    Private Sub refreshusers()
        Dim temp As String

        persistant.update()
        Me.ListUser.SelectedIndex = -1
        Me.ListUser.Items.Clear()
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_users, filterusers) - 1)

            temp = persistant.getvalue(persistant.tbl_users, "name", filterusers, x)
            If temp <> "Root access" And temp <> "Administrator" And temp <> "" And temp <> "MMSData" Then '
                'If temp <> "Root access" And temp <> "Administrator" And temp <> "" Then
                ListUser.Items.Add(temp)
            End If
        Next
        ListUser.Sorted = True
    End Sub

    Private Sub ListStore_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListStore.SelectedIndexChanged
        If ListStore.SelectedItem = "All" Then
            filterusers = ""
        Else
            filterusers = "store = '" + ListStore.SelectedItem + "'"
        End If
        refreshusers()
    End Sub

    Private Sub ListUser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListUser.SelectedIndexChanged
        If ListUser.SelectedIndex <> -1 Then
            If mode = 3 Then
                Dim userlevel As String
                Me.txtfullname.Text = persistant.getvalue(persistant.tbl_users, "name", "Name = '" + Me.ListUser.SelectedItem + "'", 0)
                Me.txtlogin.Text = persistant.getvalue(persistant.tbl_users, "user", "Name = '" + Me.ListUser.SelectedItem + "'", 0)
                Me.txtpassword.Text = "*********"
                Me.cmbstore.SelectedItem = persistant.getvalue(persistant.tbl_users, "store", "Name = '" + Me.ListUser.SelectedItem + "'", 0)
                userlevel = persistant.getvalue(persistant.tbl_users, "usergroup", "Name = '" + Me.ListUser.SelectedItem + "'", 0)
                Me.cmblevel.SelectedItem = persistant.getvalue(persistant.tbl_userlevels, "Level", "code = '" + userlevel + "'", 0)
            End If
        Else
            Me.txtfullname.Text = ""
            Me.txtlogin.Text = ""
            Me.txtpassword.Text = ""
            Me.cmbstore.SelectedIndex = -1
            Me.cmblevel.SelectedIndex = -1
        End If

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        mode = 1
        draw()

    End Sub

    Private Sub btnRem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRem.Click
        mode = 2
        draw()
    End Sub

    Private Sub btnMod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMod.Click
        mode = 3
        draw()

    End Sub


    Private Sub btnDoIt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDoIt.Click
        If Me.txtfullname.Text = "" Or Me.txtlogin.Text = "" Or Me.txtpassword.Text = "" Or Me.cmbstore.SelectedIndex = -1 Or Me.cmblevel.SelectedIndex = -1 Then
            MessageBox.Show("Please make sure you havn't left any feilds blank")
        Else
            'Add
            If mode = 1 Then
                Dim conn As New MySqlConnection
                Dim cmd As New MySqlCommand
                conn.ConnectionString = connstring
                cmd.Connection = conn
                 Try
                    conn.Open()

                    cmd.CommandText = "Insert into users set User ='" + clean(Me.txtlogin.Text) + "', YTDsales = '0',YTDbo = '0', name ='" + clean(Me.txtfullname.Text) + "', store ='" + Me.cmbstore.SelectedItem + "', usergroup ='" + persistant.getvalue(persistant.tbl_userlevels, "code", "Level = '" + Me.cmblevel.SelectedItem + "'", 0) + "', password = PASSWORD('" + clean(Me.txtpassword.Text) + "')"
                    'If persistant.getvalue(persistant.tbl_userlevels, "code", "Level = '" + Me.cmblevel.SelectedItem + "'", 0) = 6 Then
                    '    cmd.CommandText = "Insert into user set User ='" + clean(Me.txtlogin.Text) + "', host = 'localhost', name ='" + clean(Me.txtfullname.Text) + "', store ='" + Me.cmbstore.SelectedItem + "', usergroup ='" + persistant.getvalue(persistant.tbl_userlevels, "code", "Level = '" + Me.cmblevel.SelectedItem + "'", 0) + "', password = PASSWORD('" + clean(Me.txtpassword.Text) + "')" _
                    '    & ", Reload_Priv = 'Y', Grant_Priv = 'Y', Create_user_priv = 'Y'"
                    'Else
                    '    cmd.CommandText = "Insert into user set User ='" + clean(Me.txtlogin.Text) + "', host = 'localhost', name ='" + clean(Me.txtfullname.Text) + "', store ='" + Me.cmbstore.SelectedItem + "', usergroup ='" + persistant.getvalue(persistant.tbl_userlevels, "code", "Level = '" + Me.cmblevel.SelectedItem + "'", 0) + "', password = PASSWORD('" + clean(Me.txtpassword.Text) + "')"
                    'End If
                    cmd.ExecuteNonQuery()
                    setprivs(clean(Me.txtlogin.Text), persistant.getvalue(persistant.tbl_userlevels, "code", "Level = '" + Me.cmblevel.SelectedItem + "'", 0), conn)

                    conn.Close()
                    Me.txtfullname.Text = ""
                    Me.txtlogin.Text = ""
                    Me.txtpassword.Text = ""
                    Me.cmbstore.SelectedIndex = -1
                    Me.cmblevel.SelectedIndex = -1
                Catch ex As MySqlException
                    MessageBox.Show(ex.Message)
                End Try
                conn.Dispose()

        End If

            'Modify
            If mode = 3 Then
                Dim conn As New MySqlConnection
                Dim cmd As New MySqlCommand
                conn.ConnectionString = connstring
                cmd.Connection = conn
                If Me.txtpassword.Text = "*********" Then
                    cmd.CommandText = "Update users set name ='" + clean(Me.txtfullname.Text) + "', store ='" + Me.cmbstore.SelectedItem + "', usergroup ='" + persistant.getvalue(persistant.tbl_userlevels, "code", "Level = '" + Me.cmblevel.SelectedItem + "'", 0) + "'" _
                                          & " where User ='" + clean(Me.txtlogin.Text) + "'"
                Else
                    cmd.CommandText = "Update users set name ='" + clean(Me.txtfullname.Text) + "', store ='" + Me.cmbstore.SelectedItem + "', usergroup ='" + persistant.getvalue(persistant.tbl_userlevels, "code", "Level = '" + Me.cmblevel.SelectedItem + "'", 0) + "', password = PASSWORD('" + clean(Me.txtpassword.Text) + "')" _
                               & " where User ='" + clean(Me.txtlogin.Text) + "'"
                End If
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    'cmd.CommandText = "Delete from db where User ='" + persistant.getvalue(persistant.tbl_users, "user", "Name = '" + Me.ListUser.SelectedItem + "'", 0) + "'"
                    'cmd.ExecuteNonQuery()
                    'cmd.CommandText = "Delete from tables_priv where User ='" + persistant.getvalue(persistant.tbl_users, "user", "Name = '" + Me.ListUser.SelectedItem + "'", 0) + "'"
                    'cmd.ExecuteNonQuery()
                    'setprivs(persistant.getvalue(persistant.tbl_users, "user", "Name = '" + Me.ListUser.SelectedItem + "'", 0), persistant.getvalue(persistant.tbl_userlevels, "code", "Level = '" + Me.cmblevel.SelectedItem + "'", 0), conn)

                    conn.Close()
 
                    Me.txtpassword.Text = "*********"
                Catch ex As MySqlException
                    MessageBox.Show(ex.Message)
                End Try
                conn.Dispose()
            End If
        End If

        refreshusers()
    End Sub

    Private Sub setprivs(ByVal user As String, ByVal level As Integer, ByRef con As MySqlConnection)
        Dim cmd As New MySqlCommand
        cmd.Connection = con
        Try
            If user = "Krystal" Or user = "Duncan" Or user = "admin" Or user = "Walter" Or user = "BrianM" Or user = "Lenny" Or user = "BarryL" Or user = "Freddie" Or user = "Mar" Or user = "Payroll" Then
                cmd.CommandText = "Insert into db set Host = 'localhost', Db = 'time', User = '" + user + "', Select_priv = 'Y', Insert_priv = 'Y', Update_priv = 'Y', Delete_priv = 'Y'"
                cmd.ExecuteNonQuery()
            End If

            'If level = 9 Then
            cmd.CommandText = "Insert into db set Host = 'localhost', Db = 'mms', User = '" + user + "'"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into db set Host = 'localhost', Db = 'mysql', User = '" + user + "'"
            cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into db set Host = 'localhost', Db = 'mms', User = '" + user + "', Select_priv = 'Y', Insert_priv = 'Y', Update_priv = 'Y', Delete_priv = 'Y'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into db set Host = 'localhost', Db = 'mysql', User = '" + user + "', Select_priv = 'Y', Insert_priv = 'Y', Update_priv = 'Y', Delete_priv = 'Y'"
            'cmd.ExecuteNonQuery()
            'Else
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'banks', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'boats', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'bos', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'equip', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'financestatus', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'inventory', Table_priv = 'Select,Update'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'location', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'motors', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'online', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'orders', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'salrates', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'salratesbc', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'salrates2012', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'salratesbc2012', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'shopstatus', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'status', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'userlevels', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'printdata', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mysql', User = '" + user + "', Table_name = 'user', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'certsused', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'certsframe', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'commadjustments', Table_priv = 'Select'"
            'cmd.ExecuteNonQuery()

            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceCallLog', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceCheckInNotes', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceCustomers', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceIssue', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServicePartsOrders', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceParts', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServicePayments', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServicePkgs', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServicePriority', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceWO', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceWorkLog', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceWOtoIssue', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()
            'cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceWOadjustment', Table_priv = 'Select,Insert,Update,Delete'"
            'cmd.ExecuteNonQuery()

            'If level > 4 Then
            '    cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'inv_restricted', Table_priv = 'Select,Insert,Update,Delete'"
            '    cmd.ExecuteNonQuery()
            'End If
            'End If
            'cmd.CommandText = "flush privileges"
            'cmd.ExecuteNonQuery()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
 
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        If Me.ListUser.SelectedIndex <> -1 Then
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = connstring
            cmd.Connection = conn
            Try
                conn.Open()
                cmd.CommandText = "Delete from users where User ='" + persistant.getvalue(persistant.tbl_users, "user", "Name = '" + Me.ListUser.SelectedItem + "'", 0) + "'"
                cmd.ExecuteNonQuery()
                'cmd.CommandText = "Delete from db where User ='" + persistant.getvalue(persistant.tbl_users, "user", "Name = '" + Me.ListUser.SelectedItem + "'", 0) + "'"
                'cmd.ExecuteNonQuery()
                'cmd.CommandText = "Delete from tables_priv where User ='" + persistant.getvalue(persistant.tbl_users, "user", "Name = '" + Me.ListUser.SelectedItem + "'", 0) + "'"
                'cmd.ExecuteNonQuery()
                'cmd.CommandText = "flush privileges"
                'cmd.ExecuteNonQuery()
                conn.Close()
                refreshusers()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()
        Else
            MessageBox.Show("Please select a User to Remove")
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim conn As New MySqlConnection
        'Dim cmd As New MySqlCommand
        'conn.ConnectionString = connstring
        'conn.Open()
        'cmd.Connection = conn
        'For x As Integer = 0 To ListUser.Items.Count - 1

        '    cmd.CommandText = "Delete from db where User ='" + persistant.getvalue(persistant.tbl_users, "user", "Name = '" + Me.ListUser.Items(x) + "'", 0) + "'"
        '    cmd.ExecuteNonQuery()
        '    cmd.CommandText = "Delete from tables_priv where User ='" + persistant.getvalue(persistant.tbl_users, "user", "Name = '" + Me.ListUser.Items(x) + "'", 0) + "'"
        '    cmd.ExecuteNonQuery()
        '    setprivs(persistant.getvalue(persistant.tbl_users, "user", "Name = '" + ListUser.Items(x) + "'", 0), persistant.getvalue(persistant.tbl_users, "usergroup", "Name = '" + Me.ListUser.Items(x) + "'", 0), conn)

        'Next
        'conn.Close()
        'conn.Dispose()
    End Sub
End Class