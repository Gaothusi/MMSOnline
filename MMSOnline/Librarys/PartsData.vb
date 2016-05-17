Imports MySql.Data.MySqlClient
Imports System.Data


Public Class PartsData

    Public myconnstring As String
    Public Number As Integer
    Private mytable As New DataTable

    Public Sub Download()
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim cmdstring As String
        Dim adapter As New MySqlDataAdapter
        mytable.Clear()
        conn.ConnectionString = myconnstring
        cmd.Connection = conn
        conn.Open()
        cmdstring = "SELECT * FROM ServiceParts where Number = '" + Number.ToString + "'"
        cmd.CommandText = cmdstring
        adapter.SelectCommand = cmd
        adapter.Fill(mytable)
        conn.Close()
        conn.Dispose()
    End Sub
    Public Function getvalue(ByVal datawanted As String) As String
        Dim mydatarow As DataRow
        Dim mydatacol As DataColumn
        getvalue = ""
        If mytable.Rows.Count > 0 Then
            mydatarow = mytable.Rows(0)
            For y As Integer = 0 To (mytable.Columns.Count - 1)
                mydatacol = mytable.Columns(y)
                If datawanted.ToLower = mydatacol.ColumnName.ToLower Then
                    getvalue = mydatarow.Item(y).ToString
                End If
            Next
        Else
            getvalue = ""
        End If
    End Function

End Class
