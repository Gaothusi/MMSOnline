Imports MySql.Data.MySqlClient

Public Class frmYTD
    Public persistant As PreFetch
    Public user, group As String


    Private Sub frmYTD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand

        'If PreFetch.secure = True Then
        '    conn.ConnectionString = "server=localhost;" _
        '      & "user id=" & persistant.myuserID & ";" _
        '      & "password=" & persistant.password & ";" _
        '      & "database=mysql;port=" & persistant.port
        'Else
        '    conn.ConnectionString = "server=" + persistant.serveraddr + ";" _
        '           & "user id=" & persistant.myuserID & ";" _
        '           & "password=" & persistant.password & ";" _
        '           & "database=mysql;port=3306;pooling=true;"
        'End If

        'If PreFetch.secure = True Then
        '    conn.ConnectionString = "server=localhost;" _
        '      & "user id=Dave;" _
        '      & "password=Filipino;" _
        '      & "database=mysql;port=" & persistant.port
        'Else
        '    conn.ConnectionString = "server=" + persistant.serveraddr + ";" _
        '           & "user id=Dave;" _
        '           & "password=Filipino;" _
        '           & "database=mysql;port=3306;pooling=true;"
        'End If

        If PreFetch.secure = True Then
            conn.ConnectionString = "server=localhost;" _
              & "user id=MMSData;" _
              & "password=Filipino;" _
              & "database=mysql;port=" & persistant.port
        Else
            conn.ConnectionString = "server=" + persistant.serveraddr + ";" _
                   & "user id=MMSData;" _
                   & "password=Filipino;" _
                   & "database=mysql;port=3306;pooling=true;"
        End If
        'If PreFetch.secure = True Then
        '    conn.ConnectionString = "server=localhost;" _
        '      & "user id=root;" _
        '      & "password=Filipino;" _
        '      & "database=mysql;port=" & persistant.port
        'Else
        '    conn.ConnectionString = "server=" + persistant.serveraddr + ";" _
        '           & "user id=root;" _
        '           & "password=Filipino;" _
        '           & "database=mysql;port=3306;pooling=true;"
        'End If

        cmd.Connection = conn
        conn.Open()
        Select Case group
            Case Is = "Business Managers"
                cmd.CommandText = "SELECT YTDbo from user where user = '" + user + "'"
                Label1.Text = user + " - Business Manager"
            Case Is = "Salesmen"
                cmd.CommandText = "SELECT YTDsales from user where user = '" + user + "'"
                Label1.Text = user + " - Sales"
        End Select
        txtYTD.Text = Format(cmd.ExecuteScalar().ToString, "currency")
        conn.Close()
        conn.Dispose()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub TextBox2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        Dim x As String
        Dim y As Decimal
        x = Replace(TextBox2.Text, "$", "")
        x = Replace(x, ",", "")
        x = Replace(x, "(", "-")
        x = Replace(x, ")", "")
        y = Val(x)
        TextBox2.Text = Format(y.ToString, "currency")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim x As String
        Dim newytd As Decimal
        Dim oldytd As Decimal
        x = Replace(txtYTD.Text, "$", "")
        x = Replace(x, ",", "")
        x = Replace(x, "(", "-")
        x = Replace(x, ")", "")
        oldytd = Val(x)

        x = Replace(TextBox2.Text, "$", "")
        x = Replace(x, ",", "")
        x = Replace(x, "(", "-")
        x = Replace(x, ")", "")
        newytd = Val(x) + oldytd

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        'If PreFetch.secure = True Then
        '    conn.ConnectionString = "server=localhost;" _
        '      & "user id=" & persistant.myuserID & ";" _
        '      & "password=" & persistant.password & ";" _
        '      & "database=mysql;port=" & persistant.port

        'Else
        '    conn.ConnectionString = "server=" + persistant.serveraddr + ";" _
        '           & "user id=" & persistant.myuserID & ";" _
        '           & "password=" & persistant.password & ";" _
        '           & "database=mysql;port=3306;pooling=true;"
        'End If

        'If PreFetch.secure = True Then
        '    conn.ConnectionString = "server=localhost;" _
        '      & "user id=Dave;" _
        '      & "password=Filipino;" _
        '      & "database=mysql;port=" & persistant.port

        'Else
        '    conn.ConnectionString = "server=" + persistant.serveraddr + ";" _
        '           & "user id=Dave;" _
        '           & "password=Filipino;" _
        '           & "database=mysql;port=3306;pooling=true;"
        'End If


        If PreFetch.secure = True Then
            conn.ConnectionString = "server=localhost;" _
              & "user id=MMSData;" _
              & "password=Filipino;" _
              & "database=mysql;port=" & persistant.port

        Else
            conn.ConnectionString = "server=" + persistant.serveraddr + ";" _
                   & "user id=MMSData;" _
                   & "password=Filipino;" _
                   & "database=mysql;port=3306;pooling=true;"
        End If
        'If PreFetch.secure = True Then
        '    conn.ConnectionString = "server=localhost;" _
        '      & "user id=root;" _
        '      & "password=Filipino;" _
        '      & "database=mysql;port=" & persistant.port

        'Else
        '    conn.ConnectionString = "server=" + persistant.serveraddr + ";" _
        '           & "user id=root;" _
        '           & "password=Filipino;" _
        '           & "database=mysql;port=3306;pooling=true;"
        'End If

        cmd.Connection = conn
        conn.Open()
        Select Case group
            Case Is = "Business Managers"
                cmd.CommandText = "UPDATE user SET YTDbo = '" + newytd.ToString + "' where user = '" + user + "'"
            Case Is = "Salesmen"
                cmd.CommandText = "UPDATE user SET YTDsales = '" + newytd.ToString + "' where user = '" + user + "'"
        End Select
        cmd.ExecuteNonQuery()
        conn.Close()
        conn.Dispose()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class