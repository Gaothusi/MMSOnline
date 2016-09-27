Imports MySql.Data.MySqlClient

Public Class frmchangepass
    Public persistant As PreFetch

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = persistant.password Then
            If TextBox2.Text = TextBox3.Text Then
                Try
                    Dim conn As New MySqlConnection
                    Dim myCommand As New MySqlCommand
                    conn.ConnectionString = persistant.myconnstring
                    conn.Open()
                    myCommand.Connection = conn
                    myCommand.CommandText = "SET PASSWORD = PASSWORD('" + TextBox3.Text + "')"
                    myCommand.ExecuteNonQuery()
                    conn.Close()
                    conn.Dispose()
                    persistant.password = TextBox3.Text

                    '            If PreFetch.secure = True Then
                    '                persistant.myconnstring = "server=localhost;" _
                    '             & "user id=" & persistant.myuserID & ";" _
                    '             & "password=" & persistant.password & ";" _
                    '             & "database=mms;port=" & persistant.port & ";pooling=true;"
                    '            Else
                    '                persistant.myconnstring = "server=" + persistant.serveraddr + ";" _
                    '& "user id=" & persistant.myuserID & ";" _
                    '& "password=" & persistant.password & ";" _
                    '& "database=mms;port=3306;pooling=true;"
                    '            End If

                    'If PreFetch.secure = True Then
                    '    persistant.myconnstring = "server=localhost;" _
                    ' & "user id=Dave;" _
                    ' & "password=Filipino;" _
                    ' & "database=mms;port=" & persistant.port & ";pooling=true;"
                    'Else
                    '    persistant.myconnstring = "server=" + persistant.serveraddr + ";" _
                    '& "user id=Dave;" _
                    '& "password=Filipino;" _
                    '& "database=mms;port=3306;pooling=true;"
                    'End If

                    If PreFetch.secure = True Then
                        persistant.myconnstring = "server=localhost;" _
                     & "user id=root;" _
                     & "password=Filipino;" _
                     & "database=mms;port=" & persistant.port & ";pooling=true;"
                    Else
                        persistant.myconnstring = "server=" + persistant.serveraddr + ";" _
                    & "user id=root;" _
                    & "password=Filipino;" _
                    & "database=mms;port=3306;pooling=true;"
                    End If
                    'If PreFetch.secure = True Then
                    '    persistant.myconnstring = "server=localhost;" _
                    ' & "user id=root;" _
                    ' & "password=Filipino;" _
                    ' & "database=mms;port=" & persistant.port & ";pooling=true;"
                    'Else
                    '    persistant.myconnstring = "server=" + persistant.serveraddr + ";" _
                    '& "user id=root;" _
                    '& "password=Filipino;" _
                    '& "database=mms;port=3306;pooling=true;"
                    'End If



                    MessageBox.Show("Password Changed")
                    Me.Close()
                Catch ex As MySqlException
                    MessageBox.Show("Password Change Failed For the Following Reason:  " + ex.Message)
                End Try
            Else
                MessageBox.Show("Your two new passwords do not match.")
            End If
        Else
            MessageBox.Show("You entered your old password incorrectly")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub frmchangepass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.PasswordChar = "*"
        TextBox2.PasswordChar = "*"
        TextBox3.PasswordChar = "*"

    End Sub
End Class