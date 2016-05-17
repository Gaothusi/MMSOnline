Imports MySql.Data.MySqlClient

Public Class Form1
    Public persistant
    Public equip As New equipment


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim conn As New MySqlConnection
        Dim myCommand As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        conn.Open()
        myCommand.Connection = conn

        For x As Integer = Val(Me.TextBox1.Text) To Val(Me.TextBox2.Text)
            Try
                myCommand.CommandText = "INSERT into invrestricted SET Control_number = '" + x.ToString + "'"
                myCommand.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show("ERROR: " + ex.Message)
            End Try
        Next
        conn.Close()
        conn.Dispose()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim adpt As New MySqlDataAdapter
        Dim conn As New MySqlConnection
        Dim myCommand As New MySqlCommand
        Dim temptable As New DataTable

        conn.ConnectionString = persistant.myconnstring
        conn.Open()
        myCommand.Connection = conn

        myCommand.CommandText = "SELECT * from inventory where control_number > 2"
        adpt.SelectCommand = myCommand
        adpt.Fill(temptable)

        For x As Integer = 0 To temptable.Rows.Count - 1

            equip.boat(temptable.Rows(x).Item("Boat_Brand"), temptable.Rows(x).Item("Boat_Model"))
            equip.motor(temptable.Rows(x).Item("Engine_Make"), temptable.Rows(x).Item("Engine_Model"))
            equip.check(temptable.Rows(x).Item("Equipment"))

        Next
        conn.Close()
        conn.Dispose()

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        equip.persistant = persistant

    End Sub


End Class