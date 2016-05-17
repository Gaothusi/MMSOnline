Imports MySql.Data.MySqlClient
Imports System.Data

Public Class BOSdata
    Dim myconnstring As String
    Dim mystore As String
    Dim myBOS As Integer
    Dim mytable As New DataTable

    Public WriteOnly Property connectionString() As String
        Set(ByVal value As String)
            myconnstring = value
        End Set
    End Property
    Public WriteOnly Property bos() As Integer
        Set(ByVal value As Integer)
            myBOS = value
        End Set
    End Property
    Public WriteOnly Property store() As String
        Set(ByVal value As String)
            mystore = value
        End Set
    End Property
    Public Sub locked(ByVal yesno As Boolean, ByVal lockedby As String)
        Try
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            Dim cmdstring As String
            Dim val As Integer
            Dim by As String = lockedby
            If yesno = True Then val = 1 Else val = 0
            If val = 0 Then by = ""
            conn.ConnectionString = myconnstring
            cmd.Connection = conn
            conn.Open()
            cmdstring = "UPDATE bos SET locked = '" + val.ToString + "', lockedby = '" + by + "' where bos_number = '" + myBOS.ToString + "' AND store = '" + mystore + "'"
            cmd.CommandText = cmdstring
            ' debug.writeline(cmdstring)
            cmd.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
        Catch ex As MySqlException
            Debug.WriteLine(ex.Message)
        End Try
    End Sub
    Public Sub Download()
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim cmdstring As String
        Dim adapter As New MySqlDataAdapter
        mytable.Clear()
        conn.ConnectionString = myconnstring
        cmd.Connection = conn
        conn.Open()
        cmdstring = "SELECT * FROM bos where bos_number = '" + myBOS.ToString + "' AND store = '" + mystore + "'"
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
