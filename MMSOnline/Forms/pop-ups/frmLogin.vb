Imports MySql.Data.MySqlClient
'Imports Tamir.SharpSsh.jsch



<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnEZReset As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtserver = New System.Windows.Forms.TextBox()
        Me.txtusername = New System.Windows.Forms.TextBox()
        Me.txtpassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdLogin = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkRemember = New System.Windows.Forms.CheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnEZReset = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtserver
        '
        Me.txtserver.Location = New System.Drawing.Point(112, 79)
        Me.txtserver.Name = "txtserver"
        Me.txtserver.Size = New System.Drawing.Size(171, 20)
        Me.txtserver.TabIndex = 0
        '
        'txtusername
        '
        Me.txtusername.Location = New System.Drawing.Point(112, 108)
        Me.txtusername.Name = "txtusername"
        Me.txtusername.Size = New System.Drawing.Size(171, 20)
        Me.txtusername.TabIndex = 1
        '
        'txtpassword
        '
        Me.txtpassword.Location = New System.Drawing.Point(112, 136)
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.Size = New System.Drawing.Size(171, 20)
        Me.txtpassword.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Location = New System.Drawing.Point(13, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Server:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Location = New System.Drawing.Point(13, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "User Name:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label3.Location = New System.Drawing.Point(12, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 20)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Password:"
        '
        'cmdLogin
        '
        Me.cmdLogin.Location = New System.Drawing.Point(208, 194)
        Me.cmdLogin.Name = "cmdLogin"
        Me.cmdLogin.Size = New System.Drawing.Size(75, 23)
        Me.cmdLogin.TabIndex = 4
        Me.cmdLogin.Text = "Login"
        Me.cmdLogin.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(112, 194)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label5.Location = New System.Drawing.Point(38, 157)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(130, 20)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Remember Me:"
        '
        'chkRemember
        '
        Me.chkRemember.AutoSize = True
        Me.chkRemember.Location = New System.Drawing.Point(174, 162)
        Me.chkRemember.Name = "chkRemember"
        Me.chkRemember.Size = New System.Drawing.Size(15, 14)
        Me.chkRemember.TabIndex = 3
        Me.chkRemember.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImage = Global.MMSOnline.My.Resources.Resources.MMSOnline
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(1, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(294, 73)
        Me.PictureBox1.TabIndex = 14
        Me.PictureBox1.TabStop = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 228)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(295, 22)
        Me.StatusStrip1.TabIndex = 15
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'btnEZReset
        '
        Me.btnEZReset.Enabled = False
        Me.btnEZReset.Location = New System.Drawing.Point(17, 194)
        Me.btnEZReset.Name = "btnEZReset"
        Me.btnEZReset.Size = New System.Drawing.Size(75, 23)
        Me.btnEZReset.TabIndex = 16
        Me.btnEZReset.Text = "Easy Reset"
        Me.btnEZReset.UseVisualStyleBackColor = True
        Me.btnEZReset.Visible = False
        '
        'frmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.MMSOnline.My.Resources.Resources.BkgSquare
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(295, 250)
        Me.Controls.Add(Me.btnEZReset)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.chkRemember)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdLogin)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtpassword)
        Me.Controls.Add(Me.txtusername)
        Me.Controls.Add(Me.txtserver)
        Me.Controls.Add(Me.Label2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.TopMost = True
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtserver As System.Windows.Forms.TextBox
    Friend WithEvents txtusername As System.Windows.Forms.TextBox
    Friend WithEvents txtpassword As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdLogin As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkRemember As System.Windows.Forms.CheckBox


End Class

Public Class frmLogin
    Dim persistant As New PreFetch
    Dim cancled, connected, allgood As Boolean

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtpassword.Select()
        'persistant.paymentmath.Newb()
        'Me.Text = "MMS Online - Version " + PreFetch.version.ToString
        Dim ver As String = Mid(My.Application.Info.Version.ToString.Replace(".", ""), 1, 3)
        Me.Text = "MMS Online - Version 4.0.4.25" '& ver.Substring(0, 1) & "." & ver.Substring(1) '3.26"

        Me.AcceptButton = cmdLogin
        Me.CancelButton = cmdCancel
        If My.Settings.Username <> "" Then
            txtusername.Text = My.Settings.Username
            chkRemember.Checked = True
        End If
        If My.Settings.Servername <> "" Then
            txtserver.Text = My.Settings.Servername
        Else
            txtserver.Text = "mmsonline.mmservices.ca"
        End If

        txtpassword.PasswordChar = "*"
        cancled = True
        connected = False
        allgood = False

    End Sub

    Private Sub txtusername_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtusername.LostFocus
        If txtusername.Text.ToLower = "dave" Or txtusername.Text.ToLower = "dank" Then
            btnEZReset.Visible = True
        End If
    End Sub

    Private Sub txtpassword_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtpassword.TextChanged
        If txtpassword.Text.Length > 0 Then btnEZReset.Enabled = True
    End Sub

    Private Sub btnEZReset_Click(sender As System.Object, e As System.EventArgs) Handles btnEZReset.Click
        'login using the user credentials and reset the MMSData user
        Dim conn As New MySqlConnection
        Dim myCommand As New MySqlCommand
        Dim adpt As New MySqlDataAdapter
        Dim myConnString As String
        If txtserver.Text = "localhost" Then PreFetch.secure = False

        If PreFetch.secure = True Then

            If connected = False Then
                Try
                    Dim RandomClass As New Random()
                    persistant.port = RandomClass.Next(3100, 4100)

                    ToolStripStatusLabel1.Text = "Establishing Secure Link To Server"
                    'Establish SSH tunnel to server
                    Me.Refresh()
                    Dim user, host As String
                    user = "database"
                    host = txtserver.Text
                    persistant.tunnel = persistant.jschx.getSession(user, host, 22)
                    persistant.tunnel.setPassword("NEa191DE")
                    Dim config As New System.Collections.Hashtable
                    config.Add("StrictHostKeyChecking", "no")
                    config.Add("cipher.s2c", "aes128-cbc,3des-cbc")
                    config.Add("cipher.c2s", "aes128-cbc,3des-cbc")
                    persistant.tunnel.setConfig(config)
                    persistant.tunnel.connect()
                    persistant.tunnel.setPortForwardingL(persistant.port, "localhost", 3306)
                    connected = True
                Catch ex As Exception
                    Debug.WriteLine(ex.Message)
                    MessageBox.Show(ex.Message)

                    'MessageBox.Show("Unable to Connect to Server")
                    ToolStripStatusLabel1.Text = ""
                    GoTo endp
                End Try
            End If
            ToolStripStatusLabel1.Text = "Secure Link To Server Established"
            Me.Refresh()

        End If

        My.Settings.Servername = txtserver.Text
        persistant.serveraddr = txtserver.Text

        If chkRemember.Checked Then
            My.Settings.Username = txtusername.Text
            My.Settings.Save()
        Else
            My.Settings.Username = ""
        End If

        If PreFetch.secure = True Then
            myConnString = "server=localhost;" _
             & "user id=" & txtusername.Text & ";" _
             & "password=" & txtpassword.Text & ";" _
             & "database=mysql;port=" & persistant.port & ";pooling=true;"

        Else
            myConnString = "server=" + persistant.serveraddr + ";" _
             & "user id=" & txtusername.Text & ";" _
             & "password=" & txtpassword.Text & ";" _
             & "database=mysql;port=3306;pooling=true;"
        End If

        conn.ConnectionString = myConnString

        Try
            'get user info
            conn.Open()
            myCommand.Connection = conn
            myCommand.CommandText = "SELECT user, password, PASSWORD('" & txtpassword.Text & "') as pswd, usergroup, store, name from user"
            adpt.SelectCommand = myCommand
            adpt.Fill(persistant.tbl_users)

            Dim strPass As String = persistant.getvalue(persistant.tbl_users, "password", "user = '" + txtusername.Text + "'", 0)
            Dim strPass2 As String = persistant.getvalue(persistant.tbl_users, "pswd", "user = '" + txtusername.Text + "'", 0)
            'If Not strPass = strPass2 Then
            '    MessageBox.Show("Incorrect User Name or Password")
            '    Application.Exit()
            '    Exit Sub
            'End If
            persistant.myuserLEVEL = persistant.getvalue(persistant.tbl_users, "usergroup", "user = '" + txtusername.Text + "'", 0)
            persistant.mystore = persistant.getvalue(persistant.tbl_users, "store", "user = '" + txtusername.Text + "'", 0)
            persistant.myusername = persistant.getvalue(persistant.tbl_users, "name", "user = '" + txtusername.Text + "'", 0)
            persistant.myuserID = txtusername.Text
            'done
            conn.Close()


            persistant.myconnstring = myConnString
            persistant.password = txtpassword.Text


            'Modify
            Dim conn2 As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn2.ConnectionString = myConnString
            cmd.Connection = conn2
            If Me.txtpassword.Text = "*********" Then
                cmd.CommandText = "Update user set name ='MMSData', store ='Admin', usergroup ='9' where User ='MMSData'"
            Else
                cmd.CommandText = "Update user set name ='MMSData', store ='Admin', usergroup ='9', password = PASSWORD('Filipino') where User ='MMSData'"
            End If
            Try
                conn2.Open()
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Delete from db where User ='MMSData'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Delete from tables_priv where User ='MMSData'"
                cmd.ExecuteNonQuery()
                setprivs("MMSData", 9, conn2)

                conn2.Close()

                Me.txtpassword.Text = "*********"
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            End Try
            conn2.Dispose()

            'Dim Loadingfrm As New frmload
            'Loadingfrm.data = persistant
            'Loadingfrm.Show()
            'allgood = True
            'cancled = False
            'Me.Hide()
            'Me.Close()
        Catch myerror As MySqlException
            MessageBox.Show(myerror.Message)

            ' MessageBox.Show("Your user name and password are not valid.  User names and passwords are case sensitive so please check your Caps Lock Key.  If you need to reset your password please contact an administrator")
        Finally
            conn.Dispose()
        End Try
endp:

    End Sub
    Private Sub setprivs(ByVal user As String, ByVal level As Integer, ByRef con As MySqlConnection)
        Dim cmd As New MySqlCommand
        cmd.Connection = con
        Try
            If user = "Krystal" Or user = "Duncan" Or user = "admin" Or user = "Walter" Or user = "BrianM" Or user = "Lenny" Or user = "BarryL" Or user = "Freddie" Or user = "Mar" Or user = "Payroll" Then
                cmd.CommandText = "Insert into db set Host = 'localhost', Db = 'time', User = '" + user + "', Select_priv = 'Y', Insert_priv = 'Y', Update_priv = 'Y', Delete_priv = 'Y'"
                cmd.ExecuteNonQuery()
            End If

            If level = 9 Then
                cmd.CommandText = "Insert into db set Host = 'localhost', Db = 'mms', User = '" + user + "', Select_priv = 'Y', Insert_priv = 'Y', Update_priv = 'Y', Delete_priv = 'Y'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into db set Host = 'localhost', Db = 'mysql', User = '" + user + "', Select_priv = 'Y', Insert_priv = 'Y', Update_priv = 'Y', Delete_priv = 'Y'"
                cmd.ExecuteNonQuery()
            Else
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'banks', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'boats', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'bos', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'equip', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'financestatus', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'inventory', Table_priv = 'Select,Update'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'location', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'motors', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'online', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'orders', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'salrates', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'salratesbc', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'salrates2012', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'salratesbc2012', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'shopstatus', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'status', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'userlevels', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'printdata', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mysql', User = '" + user + "', Table_name = 'user', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'certsused', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'certsframe', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'commadjustments', Table_priv = 'Select'"
                cmd.ExecuteNonQuery()

                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceCallLog', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceCheckInNotes', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceCustomers', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceIssue', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServicePartsOrders', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceParts', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServicePayments', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServicePkgs', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServicePriority', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceWO', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceWorkLog', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceWOtoIssue', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'ServiceWOadjustment', Table_priv = 'Select,Insert,Update,Delete'"
                cmd.ExecuteNonQuery()

                If level > 4 Then
                    cmd.CommandText = "Insert into tables_priv set Host = 'localhost', Db = 'mms', User = '" + user + "', Table_name = 'inv_restricted', Table_priv = 'Select,Insert,Update,Delete'"
                    cmd.ExecuteNonQuery()
                End If
            End If
            cmd.CommandText = "flush privileges"
            cmd.ExecuteNonQuery()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub cmdLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        Dim conn As New MySqlConnection
        Dim myCommand As New MySqlCommand
        Dim adpt As New MySqlDataAdapter
        Dim myConnString As String
        If txtserver.Text = "localhost" Then PreFetch.secure = False

        If PreFetch.secure = True Then

            If connected = False Then
                Try
                    Dim RandomClass As New Random()
                    persistant.port = RandomClass.Next(3100, 4100)

                    ToolStripStatusLabel1.Text = "Establishing Secure Link To Server"
                    'Establish SSH tunnel to server
                    Me.Refresh()
                    Dim user, host As String
                    user = "database"
                    host = txtserver.Text
                    persistant.tunnel = persistant.jschx.getSession(user, host, 22)
                    persistant.tunnel.setPassword("NEa191DE")
                    Dim config As New System.Collections.Hashtable
                    config.Add("StrictHostKeyChecking", "no")
                    config.Add("cipher.s2c", "aes128-cbc,3des-cbc")
                    config.Add("cipher.c2s", "aes128-cbc,3des-cbc")
                    persistant.tunnel.setConfig(config)
                    persistant.tunnel.connect()
                    persistant.tunnel.setPortForwardingL(persistant.port, "localhost", 3306)
                    connected = True
                Catch ex As Exception
                    Debug.WriteLine(ex.Message)
                    MessageBox.Show(ex.Message)

                    'MessageBox.Show("Unable to Connect to Server")
                    ToolStripStatusLabel1.Text = ""
                    GoTo endp
                End Try
            End If
            ToolStripStatusLabel1.Text = "Secure Link To Server Established"
            Me.Refresh()

        End If
      
        My.Settings.Servername = txtserver.Text
        persistant.serveraddr = txtserver.Text

        If chkRemember.Checked Then
            My.Settings.Username = txtusername.Text
            My.Settings.Save()
        Else
            My.Settings.Username = ""
        End If
        'If PreFetch.secure = True Then
        '    myConnString = "server=localhost;" _
        '     & "user id=" & txtusername.Text & ";" _
        '     & "password=" & txtpassword.Text & ";" _
        '     & "database=mysql;port=" & persistant.port & ";pooling=true;"

        'Else
        '    myConnString = "server=" + persistant.serveraddr + ";" _
        '     & "user id=" & txtusername.Text & ";" _
        '     & "password=" & txtpassword.Text & ";" _
        '     & "database=mysql;port=3306;pooling=true;"
        'End If

        'If PreFetch.secure = True Then
        '    myConnString = "server=localhost;" _
        '     & "user id=Dave;" _
        '     & "password=Filipino;" _
        '     & "database=mysql;port=" & persistant.port & ";pooling=true;"

        'Else
        '    myConnString = "server=" + persistant.serveraddr + ";" _
        '     & "user id=Dave;" _
        '     & "password=Filipino;" _
        '     & "database=mysql;port=3306;pooling=true;"
        'End If



        If PreFetch.secure = True Then
            myConnString = "server=localhost;" _
             & "user id=MMSData;" _
             & "password=Filipino;" _
             & "database=mysql;port=" & persistant.port & ";pooling=true;"

        Else
            myConnString = "server=" + persistant.serveraddr + ";" _
             & "user id=MMSData;" _
             & "password=Filipino;" _
             & "database=mysql;port=3306;pooling=true;"
        End If
        'If PreFetch.secure = True Then
        '    myConnString = "server=localhost;" _
        '     & "user id=root;" _
        '     & "password=Filipino;" _
        '     & "database=mysql;port=" & persistant.port & ";pooling=true;"

        'Else
        '    myConnString = "server=" + persistant.serveraddr + ";" _
        '     & "user id=root;" _
        '     & "password=Filipino;" _
        '     & "database=mysql;port=3306;pooling=true;"
        'End If

        conn.ConnectionString = myConnString

        Try
            'get user info
            conn.Open()
            myCommand.Connection = conn
            myCommand.CommandText = "SELECT user, password, PASSWORD('" & txtpassword.Text & "') as pswd, usergroup, store, name from user"
            adpt.SelectCommand = myCommand
            adpt.Fill(persistant.tbl_users)

            Dim strPass As String = persistant.getvalue(persistant.tbl_users, "password", "user = '" + txtusername.Text + "'", 0)
            Dim strPass2 As String = persistant.getvalue(persistant.tbl_users, "pswd", "user = '" + txtusername.Text + "'", 0)
            If Not strPass = strPass2 Then
                MessageBox.Show("Incorrect User Name or Password")
                Application.Exit()
                Exit Sub
            End If
            persistant.myuserLEVEL = persistant.getvalue(persistant.tbl_users, "usergroup", "user = '" + txtusername.Text + "'", 0)
            persistant.mystore = persistant.getvalue(persistant.tbl_users, "store", "user = '" + txtusername.Text + "'", 0)
            persistant.myusername = persistant.getvalue(persistant.tbl_users, "name", "user = '" + txtusername.Text + "'", 0)
            persistant.myuserID = txtusername.Text
            'done
            conn.Close()

            'If PreFetch.secure = True Then
            '    myConnString = "server=localhost;" _
            '     & "user id=" & txtusername.Text & ";" _
            '     & "password=" & txtpassword.Text & ";" _
            '     & "database=mms;port=" & persistant.port & ";pooling=true;"
            'Else
            '    myConnString = "server=" + persistant.serveraddr + ";" _
            '     & "user id=" & txtusername.Text & ";" _
            '     & "password=" & txtpassword.Text & ";" _
            '     & "database=mms;port=3306;pooling=true;"
            'End If


            'If PreFetch.secure = True Then
            '    myConnString = "server=localhost;" _
            '     & "user id=Dave;" _
            '     & "password=Filipino;" _
            '     & "database=mms;port=" & persistant.port & ";pooling=true;"
            'Else
            '    myConnString = "server=" + persistant.serveraddr + ";" _
            '     & "user id=Dave;" _
            '     & "password=Filipino;" _
            '     & "database=mms;port=3306;pooling=true;"
            'End If

            If PreFetch.secure = True Then
                myConnString = "server=localhost;" _
                 & "user id=MMSData;" _
                 & "password=Filipino;" _
                 & "database=mms;port=" & persistant.port & ";pooling=true;"
            Else
                myConnString = "server=" + persistant.serveraddr + ";" _
                 & "user id=MMSData;" _
                 & "password=Filipino;" _
                 & "database=mms;port=3306;pooling=true;"
            End If
            'If PreFetch.secure = True Then
            '    myConnString = "server=localhost;" _
            '     & "user id=root;" _
            '     & "password=Filipino;" _
            '     & "database=mms;port=" & persistant.port & ";pooling=true;"
            'Else
            '    myConnString = "server=" + persistant.serveraddr + ";" _
            '     & "user id=root;" _
            '     & "password=Filipino;" _
            '     & "database=mms;port=3306;pooling=true;"
            'End If
            persistant.myconnstring = myConnString

            persistant.password = txtpassword.Text

            Dim Loadingfrm As New frmload
            Loadingfrm.data = persistant
            Loadingfrm.Show()
            allgood = True
            cancled = False
            Me.Hide()
            Me.Close()
        Catch myerror As MySqlException
            MessageBox.Show(myerror.Message)

            ' MessageBox.Show("Your user name and password are not valid.  User names and passwords are case sensitive so please check your Caps Lock Key.  If you need to reset your password please contact an administrator")
        Finally
            conn.Dispose()
        End Try
endp:

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Application.Exit()
    End Sub

    Private Sub frmLogin_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not allgood Then
            If connected Then
                persistant.tunnel.delPortForwardingL("localhost", persistant.port)
                persistant.tunnel.disconnect()
                persistant.tunnel = Nothing
                persistant.jschx = Nothing
            End If

            persistant = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            Application.Exit()

        End If

    End Sub

End Class
