Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Data
Imports MMSOnline.Functions

Public Class frmLogWork
    Public persistant As PreFetch
    Public Const mydatacount As Integer = 4
    Public mydata(mydatacount) As element
    Public tech As String
    Public LogNumber, Customer, Issue As Integer
    'Private timerIsRunning As Boolean
    Private Worklogdata As New LogData

    Private Sub frmLogWork_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setuparray()
        If LogNumber > 0 Then
            downloaddata()
            WritetoScreen()
            'editing an existing record so remove the timer features
            'if the timer is running then enable the stop button
            If lblT.Text = "1" Then
                'get the start time and figure out the number of hrs and minutes and set the timer value
                Dim span As TimeSpan = DateTime.Now - timestart.Value
                lblTimerVal.Text = CInt(span.TotalMinutes).ToString & " mins"
                btnStartStop.Text = "Stop Timer"
                btnStartStop.BackColor = Color.LightGreen
                Timer1.Start()
            Else
                btnStartStop.Visible = False
                'calculate the minutes of this log
                lblTimerVal.Text = CInt((timeend.Value - timestart.Value).TotalMinutes).ToString & " mins"
            End If

            If txtAssignedTo.Text = persistant.myuserID Or persistant.myuserLEVEL > 7 Or persistant.myuserLEVEL = 1 Then
            Else
                timeend.Enabled = False
                timestart.Enabled = False
                txtNotes.ReadOnly = True
                btnAssign.Visible = False
            End If
        Else
            timestart.Value = DateTime.Now
            timeend.Value = DateTime.Now
            datechanged(timestart)
            datechanged(timeend)
            If persistant.myuserLEVEL < 0 Then
                txtAssignedTo.Text = persistant.myuserID
            Else
                txtAssignedTo.Text = tech
            End If
            stringchanged(txtAssignedTo)
        End If
    End Sub

    Private Sub setuparray()
        Dim i As Integer

        For x As Integer = 0 To (mydata.Length - 1)
            mydata(x) = New element

        Next

        i = 0
        mydata(i).txtbox = Me.txtNotes
        mydata(i).dbfeild = "Notes"
        mydata(i).dbtable = "WorkLog"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 1
        mydata(i).txtbox = Me.timestart
        mydata(i).dbfeild = "timestart"
        mydata(i).dbtable = "WorkLog"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 2
        mydata(i).txtbox = Me.timeend
        mydata(i).dbfeild = "timeend"
        mydata(i).dbtable = "WorkLog"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 3
        mydata(i).txtbox = Me.txtAssignedTo
        mydata(i).dbfeild = "Tech"
        mydata(i).dbtable = "WorkLog"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 4
        mydata(i).txtbox = Me.lblT
        mydata(i).dbfeild = "TimerFlag"
        mydata(i).dbtable = "WorkLog"
        mydata(i).changed = False
        mydata(i).value = 0

    End Sub

    Public Sub downloaddata()

        Worklogdata.Lognumber = LogNumber
        Worklogdata.myconnstring = persistant.myconnstring
        Worklogdata.Download()

        For x As Integer = 0 To mydatacount
            If mydata(x).dbtable = "WorkLog" Then mydata(x).value = Worklogdata.getvalue(mydata(x).dbfeild)
            'If x = 4 Then timerIsRunning = IIf(mydata(4).value = 1, True, False)
        Next

    End Sub

    Private Sub WritetoScreen()
        For x As Integer = 0 To 0
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
        Next
        For x As Integer = 1 To 2
            If mydata(x).value <> "" Then mydata(x).txtbox.value = CDate(mydata(x).value)
        Next
        For x As Integer = 3 To 4
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
        Next
    End Sub

    Private Sub txtleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNotes.Leave
        stringchanged(sender)
    End Sub
    Private Sub dateleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timestart.Leave, timeend.Leave
        datechanged(sender)
    End Sub

    Private Sub datechanged(ByVal sender As Object)
        Dim data As String
        For i As Integer = 0 To (mydata.Length - 1)
            If sender.Equals(mydata(i).txtbox) = True Then
                data = Microsoft.VisualBasic.Strings.Format(sender.Value, "yyyy-MM-dd H:mm:ss")
                mydata(i).value = data
                mydata(i).changed = True

            End If
        Next
    End Sub
    Private Sub stringchanged(ByVal sender As Object)
        Dim data As String
        For i As Integer = 0 To (mydata.Length - 1)
            If sender.Equals(mydata(i).txtbox) = True Then
                data = sender.text
                mydata(i).value = data
                mydata(i).changed = True
            End If
        Next
    End Sub
    Public Function timemath(ByVal timein As DateTime, ByVal timeout As DateTime) As Decimal
        Dim span As TimeSpan
        span = timeout - timein
        timemath = (span.TotalMinutes / 60)
    End Function
    Private Sub save()
        If LogNumber = 0 Then
            OK_Button.Select()
            For x As Integer = 0 To mydatacount
                mydata(x).value = Replace(mydata(x).value, "'", "\'")
                mydata(x).value = Replace(mydata(x).value, ";", "\;")
            Next
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            cmd.Connection = conn
            Dim cmdstring As String = "Insert into ServiceWorkLog Set "

            For x As Integer = 0 To mydatacount
                If mydata(x).dbtable = "WorkLog" And mydata(x).changed = True Then
                    'If x = 4 Then
                    '    cmdstring = cmdstring + mydata(x).dbfeild + " = '" + IIf(timerIsRunning, 1, 0).ToString + "', "
                    'Else
                    cmdstring = cmdstring + mydata(x).dbfeild + " = '" + mydata(x).value + "', "
                    'End If
                End If
            Next
            cmdstring = cmdstring + "Customer = '" + Customer.ToString + "', "
            cmdstring = cmdstring + "Issue = '" + Issue.ToString + "', "
            cmdstring = cmdstring + "TimeWorked = '" + timemath(timestart.Value, timeend.Value).ToString + "'"
            cmd.CommandText = cmdstring
            Debug.WriteLine(cmdstring)
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()
            For x As Integer = 0 To mydatacount
                mydata(x).value = Replace(mydata(x).value, "\'", "'")
                mydata(x).value = Replace(mydata(x).value, "\;", ";")
            Next
        Else
            For x As Integer = 0 To mydatacount
                mydata(x).value = Replace(mydata(x).value, "'", "\'")
                mydata(x).value = Replace(mydata(x).value, ";", "\;")
            Next
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            cmd.Connection = conn
            Dim cmdstring As String = "Update ServiceWorkLog Set "

            For x As Integer = 0 To mydatacount
                If mydata(x).dbtable = "WorkLog" And mydata(x).changed = True Then
                    'If x = 4 Then
                    '    cmdstring = cmdstring + mydata(x).dbfeild + " = '" + IIf(timerIsRunning, 1, 0).ToString + "', "
                    'Else
                    cmdstring = cmdstring + mydata(x).dbfeild + " = '" + mydata(x).value + "', "
                    'End If
                End If
            Next
            cmdstring = cmdstring + "TimeWorked = '" + timemath(timestart.Value, timeend.Value).ToString + "'  where Number = '" + LogNumber.ToString + "'"
            cmd.CommandText = cmdstring
            Debug.WriteLine(cmdstring)
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()
            For x As Integer = 0 To mydatacount
                mydata(x).value = Replace(mydata(x).value, "\'", "'")
                mydata(x).value = Replace(mydata(x).value, "\;", ";")
            Next
        End If
    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If CInt((timeend.Value - timestart.Value).TotalMinutes) > 0 Or lblT.Text = "1" Then
            save()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show("You can't log work where the time worked is less than or equal to zero")
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


    Private Sub btnAssign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssign.Click
        Dim pick As New frmSalesman
        pick.persistant = persistant
        pick.filter = "usergroup < 0"
        pick.ShowDialog()
        If persistant.temppass = "None" Then
        Else
            txtAssignedTo.Text = persistant.getvalue(persistant.tbl_users, "user", "name = '" + persistant.temppass + "'", 0)
            stringchanged(txtAssignedTo)
        End If
    End Sub

    Private secondVal As Integer = 0
    Private timerVal As Integer = 0
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        secondVal += 1
        If secondVal Mod 2 = 0 Then
            lblTimerRunning.Visible = True
        Else
            lblTimerRunning.Visible = False
        End If
        If secondVal = 60 Then
            secondVal = 0
            timerVal += 1
            lblTimerVal.Text = timerVal.ToString & " mins"
        End If
    End Sub

    Private Sub btnStartStop_Click(sender As System.Object, e As System.EventArgs) Handles btnStartStop.Click
        If btnStartStop.Text = "Start Timer" Then
            lblT.Text = "1"
            stringchanged(lblT)
            timestart.Value = DateTime.Now
            datechanged(timestart)
            btnStartStop.Text = "Stop Timer"
            btnStartStop.BackColor = Color.LightGreen
            Timer1.Start()
        Else
            lblT.Text = "0"
            stringchanged(lblT)
            timeend.Value = DateTime.Now
            datechanged(timeend)
            btnStartStop.Text = "Start Timer"
            btnStartStop.Enabled = False
            lblTimerRunning.Visible = False
            btnStartStop.BackColor = DefaultBackColor
            Timer1.Stop()
        End If
    End Sub
End Class
