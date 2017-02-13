Imports MySql.Data.MySqlClient
Imports System.Data


Public Class frmServiceMain
    Public persistant As PreFetch
    Public mainform As Form

    Private mydata As New DataTable
    Private newdata As New DataTable
    Private bindingsource1 As New BindingSource
    Private binded As Boolean

    Private Sub frmServiceMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        buildlists()
        permisions()
    End Sub
    Private Sub permisions()
        If persistant.myuserLEVEL = 1 Or persistant.myuserLEVEL > 7 Then
        Else
            btnNewWO.Visible = False
            btnPrioritizeWork.Text = "View Assigned Work"
        End If
        If persistant.myuserLEVEL > 7 Then
            ShowHideMain.Visible = False
        End If
        If persistant.myuserLEVEL > 0 Then
            btnWarrantyClaims.Visible = True
        End If
    End Sub
    Private Sub cmdGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click
        go()

    End Sub
    Private Sub go()
        txtloading.Visible = True
        Me.Refresh()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        Dim SQL As String
        SQL = buildsql()

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            newdata.Clear()
            adapter.Fill(newdata)
            conn.Close()
            refreshdata(newdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()

        txtloading.Visible = False
        Me.Refresh()
    End Sub

    Private Function buildsql() As String
        Dim statusstring As String = ""
        Dim storestring As String = ""
        Dim fstring As String = ""
        Dim sstring As String = ""

        sstring = "Select s.Number, s.Store, c.Name, c.City, c.Bmake as 'Boat Make', c.Bmodel as 'Boat Model', s.Status, s.Priority as 'Priority', " &
                        "s.DateReqComp as 'Requested Complete', s.DateSchDropOff as 'Scheduled Drop off' " &
                    "from ServiceWO as s, ServiceCustomers as c where s.CustomerProfile = c.Number"

        If txtname.Text <> "" Then
            fstring = fstring + " AND c.Name like '%" + txtname.Text + "%'"
        End If
        If txtWOnum.Text <> "" Then
            fstring = fstring + " AND s.Number like '%" + txtWOnum.Text + "%'"
        End If
        If CheckBox1.Checked = True Then
            fstring = fstring + " AND s.DateSchDropOff < '" + Microsoft.VisualBasic.Strings.Format(DateTimePicker1.Value, "yyyy-MM-dd H:mm:ss") + "'"
        End If
        If CheckBox2.Checked = True Then
            fstring = fstring + " AND s.DateReqComp < '" + Microsoft.VisualBasic.Strings.Format(DateTimePicker2.Value, "yyyy-MM-dd H:mm:ss") + "'"
        End If
        If listStore.CheckedItems.Count > 0 Then
            For x As Integer = 0 To (listStore.Items.Count() - 1)
                If listStore.GetItemCheckState(x) = CheckState.Checked Then
                    If storestring <> "" Then
                        storestring = storestring + ", "
                    End If
                    storestring = storestring + "'" + listStore.Items.Item(x) + "'"
                End If
            Next x
            fstring = fstring + " AND s.Store IN (" + storestring + ")"
        Else
            fstring = fstring + " AND s.Store = 'X'"
        End If


        'If listStore.Items.Count <> listStore.CheckedItems.Count Then
        '    If listStore.CheckedItems.Count = 0 Then
        '        fstring = fstring + " AND s.Store = 'X'"
        '    Else

        '        For x As Integer = 0 To (listStore.Items.Count() - 1)
        '            If listStore.GetItemCheckState(x) = CheckState.Checked Then
        '                If storestring <> "" Then
        '                    storestring = storestring + ", "
        '                End If
        '                storestring = storestring + "'" + listStore.Items.Item(x) + "'"
        '            End If
        '        Next x
        '        fstring = fstring + " AND s.Store IN (" + storestring + ")"
        '    End If
        'End If
        If listStatus.Items.Count <> listStatus.CheckedItems.Count Then
            If listStatus.CheckedItems.Count = 0 Then
                fstring = fstring + " AND s.Status = 'X'"
            Else
                For x As Integer = 0 To (listStatus.Items.Count() - 1)
                    If listStatus.GetItemCheckState(x) = CheckState.Checked Then
                        If statusstring <> "" Then
                            statusstring = statusstring + ", "
                        End If
                        statusstring = statusstring + "'" + listStatus.Items.Item(x) + "'"
                    End If
                Next x
                fstring = fstring + " AND s.Status IN (" + statusstring + ")"
            End If
        End If

        buildsql = sstring + fstring
    End Function

    Private Sub buildlists()
        'establish a database connection and get store name based on the username supplied on the login page.MMSData is currently used
        Dim conn As New MySqlConnection
        Dim myCommand As New MySqlCommand
        Dim adptTemp As New MySqlDataAdapter
        Dim myConnString As String
        Dim currentUserStore As String
        'Try
        '    Dim RandomClass As New Random()
        '    persistant.port = RandomClass.Next(3100, 4100)

        '    'Establish SSH tunnel to server
        '    Me.Refresh()
        '    Dim user, host As String
        '    user = "database"
        '    host = My.Settings.Servername
        '    persistant.tunnel = persistant.jschx.getSession(user, host, 22)
        '    persistant.tunnel.setPassword("NEa191DE")
        '    Dim config As New System.Collections.Hashtable
        '    config.Add("StrictHostKeyChecking", "no")
        '    config.Add("cipher.s2c", "aes128-cbc,3des-cbc")
        '    config.Add("cipher.c2s", "aes128-cbc,3des-cbc")
        '    persistant.tunnel.setConfig(config)
        '    persistant.tunnel.connect()
        '    persistant.tunnel.setPortForwardingL(persistant.port, "localhost", 3306)
        'Catch ex As Exception
        '    Debug.WriteLine(ex.Message)
        '    MessageBox.Show(ex.Message)
        'End Try

        myConnString = "server=localhost;" _
                  & "user id=MMSData;" _
                  & "password=Filipino;" _
                 & ";database=mms;port=" & persistant.port & ";pooling=true;"

        conn.ConnectionString = myConnString

        Try
            'get user info
            conn.Open()
            myCommand.Connection = conn
            myCommand.CommandText = "SELECT user, password, PASSWORD('" & persistant.password & "') as pswd, usergroup, store, name from users"
            adptTemp.SelectCommand = myCommand
            adptTemp.Fill(persistant.tbl_temp_SWO_Users)

            currentUserStore = persistant.getvalue(persistant.tbl_temp_SWO_Users, "store", "user = '" + persistant.myuserID + "'", 0)
            conn.Close()

        Catch myerror As MySqlException
            MessageBox.Show(myerror.Message)
        Finally
            conn.Dispose()
        End Try
        'End of fetching store name



        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
            'only add the stores that the user is in unless they are admin
            If currentUserStore = "Admin" Then 'persistant.mystoreCODE
                If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "ADM" Then
                    Me.listStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Code", "", x))
                End If
            Else
                If persistant.getvalue(persistant.tbl_location, "Store", "", x) = currentUserStore Then 'it used to be persistant.storeCODE
                    Me.listStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Code", "", x))
                End If
            End If
        Next
        For x As Integer = 0 To (listStore.Items.Count() - 1)
            If listStore.Items(x) = currentUserStore Then listStore.SetItemChecked(x, True)
        Next x
        listStatus.SetItemChecked(2, True)
        listStatus.SetItemChecked(3, True)
    End Sub
    Private Sub refreshdata(ByVal data As DataTable)
        mydata.Clear()
        mydata.Merge(data)
        bindingsource1.ResetBindings(False)

        If binded = False Then
            bindingsource1.DataSource = mydata
            DataView.DataSource = bindingsource1
            binded = True
        End If

        For x As Integer = 0 To DataView.Rows.Count - 1
            'If DataView.Item("Locked", x).Value = 1 Then
            '    DataView.Rows(x).DefaultCellStyle.BackColor = Color.LightSteelBlue
            'End If
            'If persistant.myuserLEVEL = 6 Or persistant.myuserLEVEL = 7 Or persistant.myuserID = "admin" Then
            If IsDBNull(DataView.Item("Priority", x).Value) Then
            Else
                If DataView.Item("Priority", x).Value = "ASAP" Or DataView.Item("Priority", x).Value = "Same Day" Then
                    DataView.Rows(x).DefaultCellStyle.BackColor = Color.LightCoral
                End If
                If DataView.Item("Priority", x).Value = "Rush" Then
                    DataView.Rows(x).DefaultCellStyle.BackColor = Color.Khaki
                End If
            End If
            'End If
        Next

        Me.DataView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader)
    End Sub


#Region " Side Menu Buttons"

    Private Sub btnNewWO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewWO.Click

        Dim newWOstore As String = ""

        'If admin decided what store
        If persistant.mystoreCODE = "ADM" Then 'persistant.myuserLEVEL > 6 Or 
            Dim storepicker As New PickStore
            storepicker.persistant = Me.persistant
            storepicker.ShowDialog()
            If storepicker.DialogResult = Windows.Forms.DialogResult.OK Then
                newWOstore = storepicker.Store
            End If
        Else
            If persistant.mystoreCODE = "EDM" Or persistant.mystoreCODE = "ATL" Then
                newWOstore = "EDM"
            Else
                'If not Admin or combined store then just use their store
                newWOstore = persistant.mystoreCODE
            End If
        End If

        'If we have a store selected we can go on.
        If newWOstore <> "" Then
            'Next up we will select an appointment date before we create a new WO

            Dim newWO As New FrmWO
            '!!!!TEMP SET DATE TO NOW FIX ME

            newWO.persistant = Me.persistant
            newWO.newWOschDate = Date.Now
            newWO.Store = newWOstore
            newWO.WONumber = 0

            newWO.Show()

        End If
    End Sub

    Private Sub cmbcustomers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCustomers.Click
        Dim PickCust As New FrmCustFinder
        PickCust.persistant = persistant
        PickCust.Selectmode = False
        PickCust.Show()
    End Sub

    Private Sub btnissues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchIssues.Click
        Dim Searcissues As New FrmIssuesearch
        Searcissues.persistant = persistant
        Searcissues.Show()
    End Sub

    Private Sub btnprioritize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrioritizeWork.Click
        Dim prioritize As New FrmPriority
        prioritize.persistant = persistant
        prioritize.Show()
    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompleted.Click
        Dim Donebin As New FrmDoneBin
        Donebin.persistant = persistant
        Donebin.Show()
    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeliveryList.Click
        Dim Delivery As New FrmDeliveryList
        Delivery.persistant = persistant
        Delivery.Show()
    End Sub

    Private Sub btnOrderParts_Click(sender As System.Object, e As System.EventArgs) Handles btnOrderParts.Click
        Dim x As New frmOrderPartsMain
        x.persistant = Me.persistant
        x.Show()
    End Sub

    Private Sub btnWarrantyClaims_Click(sender As System.Object, e As System.EventArgs) Handles btnWarrantyClaims.Click
        Dim x As New frmWarrantyClaimsMain
        x.persistant = Me.persistant
        x.Show()
    End Sub

#End Region

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataView.CellContentClick
        Dim xRow As Integer
        Dim xNum As Integer
        Dim xStore As String

        If e.ColumnIndex = 0 Then
            xRow = e.RowIndex
            xNum = CInt(DataView.Item("Number", xRow).Value.ToString)
            xStore = DataView.Item("Store", xRow).Value.ToString
            Dim ViewWO As New FrmWO
            ViewWO.persistant = persistant
            ViewWO.WONumber = xNum
            ViewWO.Store = xStore
            If ViewWO.ShowDialog = DialogResult.OK Then
                go()
            End If
            'ViewWO.Show()
        End If
    End Sub

    Private Sub Textboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtname.Leave, txtWOnum.Leave
        sender.text = Replace(sender.text, "'", "")
        sender.text = Replace(sender.text, ";", "")
        sender.text = Replace(sender.text, "\", "")
        sender.text = Replace(sender.text, "/", "")
    End Sub

    Private Sub ShowHideMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowHideMain.Click
        If mainform.Visible = True Then
            mainform.Visible = False
        Else
            mainform.Visible = True
        End If
    End Sub

    Private Sub BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        listStatus.SetItemChecked(0, False)
        listStatus.SetItemChecked(1, False)
        listStatus.SetItemChecked(2, False)
        listStatus.SetItemChecked(3, False)
        listStatus.SetItemChecked(4, False)
        listStatus.SetItemChecked(5, False)
        listStatus.SetItemChecked(6, False)
        listStatus.SetItemChecked(7, False)
        go()

    End Sub

    Private Sub BtnActive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnActive.Click
        listStatus.SetItemChecked(0, False)
        listStatus.SetItemChecked(1, False)
        listStatus.SetItemChecked(2, True)
        listStatus.SetItemChecked(3, True)
        listStatus.SetItemChecked(4, False)
        listStatus.SetItemChecked(5, False)
        listStatus.SetItemChecked(6, False)
        listStatus.SetItemChecked(7, False)
        go()
    End Sub

    Private Sub BtnRigs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRigs.Click
        listStatus.SetItemChecked(0, False)
        listStatus.SetItemChecked(1, True)
        listStatus.SetItemChecked(2, False)
        listStatus.SetItemChecked(3, False)
        listStatus.SetItemChecked(4, False)
        listStatus.SetItemChecked(5, False)
        listStatus.SetItemChecked(6, False)
        listStatus.SetItemChecked(7, False)
        go()

    End Sub

    Private Sub BtnWFPU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnWFPU.Click
        listStatus.SetItemChecked(0, False)
        listStatus.SetItemChecked(1, False)
        listStatus.SetItemChecked(2, False)
        listStatus.SetItemChecked(3, False)
        listStatus.SetItemChecked(4, False)
        listStatus.SetItemChecked(5, True)
        listStatus.SetItemChecked(6, False)
        listStatus.SetItemChecked(7, False)
        go()

    End Sub


    Private Sub btnSchedule_Click(sender As System.Object, e As System.EventArgs) Handles btnScheduler.Click
        Dim Scheduler As New frmServiceSchedule
        Scheduler.persistant = persistant
        Scheduler.Store = persistant.mystoreCODE
        Scheduler.Department = "Service"
        Scheduler.ShowDialog()
        'If ViewWO.ShowDialog = DialogResult.OK Then
        '    go()
        'End If

    End Sub


End Class