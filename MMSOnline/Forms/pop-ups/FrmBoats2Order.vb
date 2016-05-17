Imports MySql.Data.MySqlClient

Public Class FrmBoats2Order
    Public persistant As PreFetch

    Public mode As Integer
    '1 = Requested
    '2 = Approved to be ordered

    Private mydata As New DataTable
    Private bindingsource1 As New BindingSource



    Private Sub refreshdata()
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
            mydata.Clear()

            adapter.SelectCommand = cmd
            adapter.Fill(mydata)
            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()

    End Sub
    Private Function buildsql() As String
        Dim temp As String
        temp = ""
        If mode = 1 Then
            temp = "SELECT ordernumber, bos as 'BOS #', store as Store, salesman as Salesman, customer as 'Customer', byear as Year, bmake as Make, bmodel as Model, colour as Colour, mmake as 'Motor Make', mmodel as 'Motor Model', equip as Equipment, price as Price From orders where Status = 'Requested'"
        End If
        If mode = 2 Then
            temp = "SELECT ordernumber, bos as 'BOS #', store as Store, salesman as Salesman, customer as 'Customer', byear as Year, bmake as Make, bmodel as Model, colour as Colour, mmake as 'Motor Make', mmodel as 'Motor Model', equip as Equipment, price as Price From orders where Status = 'Approved'"
        End If

        buildsql = temp
    End Function
    Private Sub showdata()
        bindingsource1.DataSource = mydata
        bindingsource1.Filter = Nothing
        DataGridView.DataSource = bindingsource1
        DataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
        DataGridView.Columns(1).Visible = False
    End Sub

    Private Sub FrmBoats2Order_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        refreshdata()
        showdata()
        If mode = 1 Then
            Me.Text = "Requested Orders"
        End If
        If mode = 2 Then
            Me.Text = "Approved Orders"
        End If
    End Sub

    Private Sub DataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView.CellContentClick

        Dim xRow As Integer
        Dim xordernumber As String
        If e.ColumnIndex = 0 Then
            DataGridView.Columns(1).Visible = True
            xRow = e.RowIndex
            xordernumber = DataGridView.Item(1, xRow).Value
            Dim order As New frmOrderBoat
            order.persistant = persistant
            order.vieworder(xordernumber)
            order.Show()
            DataGridView.Columns(1).Visible = False
        End If
5:
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        refreshdata()
        showdata()
    End Sub
End Class