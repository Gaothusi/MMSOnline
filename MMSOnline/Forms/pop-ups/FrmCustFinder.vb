Imports MySql.Data.MySqlClient
Imports System.Data

Public Class FrmCustFinder
    Public persistant As PreFetch
    Public Selectmode As Boolean
    Public SelectedProfile As Integer

    Private mydata As New DataTable
    Private newdata As New DataTable
    Private bindingsource1 As New BindingSource
    Private binded As Boolean


    Private Sub refreshdata(ByVal data As DataTable)
        mydata.Clear()
        mydata.Merge(data)
        bindingsource1.ResetBindings(False)

        If binded = False Then
            bindingsource1.DataSource = mydata
            DataGridView1.DataSource = bindingsource1
            binded = True
        End If

        '       For x As Integer = 0 To DataView.Rows.Count - 1
        'If DataView.Item("Locked", x).Value = 1 Then
        ' DataView.Rows(x).DefaultCellStyle.BackColor = Color.LightSteelBlue
        'End If
        'If persistant.myuserLEVEL = 6 Or persistant.myuserLEVEL = 7 Or persistant.myuserID = "admin" Then
        ' If DataView.Item("Finance Status", x).Value = "Unsubmited" And DataView.Item("Business Manager", x).Value.ToString = "" Then
        ' DataView.Rows(x).DefaultCellStyle.BackColor = Color.Yellow
        'End If
        'End If
        'Next
        Me.DataGridView1.Columns("number").Visible = False

        Me.DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader)
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
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
    End Sub
    Private Function buildsql() As String


        Dim fstring As String = ""
        Dim sstring As String = ""

        sstring = "Select Name, Warnings, City, Prov, Bmake as 'Boat Make', Bmodel as 'Boat Model', Number from ServiceCustomers"
        If txtName.Text = "" And TxtCity.Text = "" And txtphone1.Text = "" And txtBoatSerial.Text = "" And txtmotorserial.Text = "" And txttrailerserial.Text = "" Then
            'if all blank
            Debug.WriteLine("Find All")
        Else
            fstring = fstring + " where "
            If txtName.Text <> "" Then
                If fstring <> " where " Then
                    fstring = fstring + " AND "
                End If
                fstring = fstring + " Name like '%" + txtName.Text + "%'"
            End If
            If TxtCity.Text <> "" Then
                If fstring <> " where " Then
                    fstring = fstring + " AND "
                End If
                fstring = fstring + " City like '%" + TxtCity.Text + "%'"
            End If
            If txtphone1.Text <> "" Then
                If fstring <> " where " Then
                    fstring = fstring + " AND "
                End If
                fstring = fstring + " (Phone1 like '%" + txtphone1.Text + "%' OR Phone2 like '%" + txtphone1.Text + "%')"
            End If
            If txtBoatSerial.Text <> "" Then
                If fstring <> " where " Then
                    fstring = fstring + " AND "
                End If
                fstring = fstring + " Bserial like '%" + txtBoatSerial.Text + "%'"
            End If
            If txtmotorserial.Text <> "" Then
                If fstring <> " where " Then
                    fstring = fstring + " AND "
                End If
                fstring = fstring + " Mserial like '%" + txtmotorserial.Text + "%'"
            End If
            If txttrailerserial.Text <> "" Then
                If fstring <> " where " Then
                    fstring = fstring + " AND "
                End If
                fstring = fstring + " Tserial like '%" + txttrailerserial.Text + "%'"
            End If


        End If

        buildsql = sstring + fstring


    End Function
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim xRow As Integer
        Dim xCustomer As String
     
        If e.ColumnIndex = 0 And mydata.Rows.Count > 0 Then
            xRow = e.RowIndex
            xCustomer = mydata.Rows(xRow).Item("Number").ToString
            If Selectmode = True Then
                SelectedProfile = CInt(xCustomer)
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Else
                Dim Editer As New frmEditCustomer
                Editer.persistant = persistant
                Editer.CustomerNumber = xCustomer
                Editer.Show()
            End If
        End If

    End Sub
    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Dim CreateProfile As New frmAddCustomer
        CreateProfile.persistant = persistant
        CreateProfile.ShowDialog()
        If CreateProfile.DialogResult = Windows.Forms.DialogResult.OK Then
            SelectedProfile = CreateProfile.CustomerNumber
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub
    Private Sub Textboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttrailerserial.Leave, txtphone1.Leave, txtName.Leave, txtmotorserial.Leave, TxtCity.Leave, txtBoatSerial.Leave
        sender.text = Replace(sender.text, "'", "")
        sender.text = Replace(sender.text, ";", "")
        sender.text = Replace(sender.text, "\", "")
        sender.text = Replace(sender.text, "/", "")
    End Sub
    Private Sub FrmCustFinder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Selectmode = True Then
            btnCreate.Visible = True
            DataGridView1.Columns(0).HeaderText = "Select"
        End If
    End Sub
End Class