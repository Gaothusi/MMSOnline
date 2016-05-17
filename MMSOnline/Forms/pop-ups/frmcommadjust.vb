Imports System.Windows.Forms
Imports System.Data
Imports MySql.Data.MySqlClient

Public Class frmcommadjust
    Private binderset As New BindingSource
    Private dataset As New DataSet
    Public user As String
    Public persistant As PreFetch

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub frmcommadjust_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtuser.Text = user
        getcollection()
        binderset.DataSource = dataset
        binderset.DataMember = dataset.Tables(0).TableName
        DataGridView1.DataSource = binderset
        If dataset.Tables(0).Rows.Count > 0 Then
            Me.DataGridView1.Columns("Adjustment").Visible = False
        End If

    End Sub

    Private Sub getcollection()
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adpt As New MySqlDataAdapter
        dataset.Clear()

        Try
            conn.ConnectionString = persistant.myconnstring
            cmd.Connection = conn
            conn.Open()
            cmd.CommandText = "SELECT Adjustment, Amount, Reason from commadjustments where User = '" + user + "'"
            adpt.SelectCommand = cmd
            adpt.Fill(dataset)
            adpt.Dispose()
            conn.Close()

            If dataset.Tables(0).Rows.Count > 0 Then
                Me.DataGridView1.Columns("View").Visible = True
                Me.DataGridView1.Columns("del").Visible = True
                Me.DataGridView1.ColumnHeadersVisible = True
            Else
                Me.DataGridView1.Columns("View").Visible = False
                Me.DataGridView1.Columns("del").Visible = False
                Me.DataGridView1.ColumnHeadersVisible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Dispose()
    End Sub

    Private Sub BtnADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnADD.Click
        Dim add As New frmcommadjustaddedit
        add.persistant = persistant
        add.user = user
        add.ShowDialog()
        getcollection()
        If dataset.Tables(0).Rows.Count > 0 Then
            Me.DataGridView1.Columns("Adjustment").Visible = False
        End If
    End Sub



    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 0 Then
            Dim edit As New frmcommadjustaddedit
            edit.persistant = persistant
            edit.user = user
            edit.Adjustment = CInt(DataGridView1.Item("Adjustment", e.RowIndex).Value)
            edit.ShowDialog()
        End If

        If e.ColumnIndex = 1 Then
            Dim conn As New MySqlConnection
            Try
                Dim cmd As New MySqlCommand
                conn.ConnectionString = persistant.myconnstring
                cmd.Connection = conn
                conn.Open()
                cmd.CommandText = "DELETE from commadjustments where Adjustment = " + DataGridView1.Item("Adjustment", e.RowIndex).Value.ToString
                cmd.ExecuteNonQuery()
                conn.Close()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            conn.Dispose()
        End If

        getcollection()
        If dataset.Tables(0).Rows.Count > 0 Then
            Me.DataGridView1.Columns("Adjustment").Visible = False
        End If
    End Sub
End Class
