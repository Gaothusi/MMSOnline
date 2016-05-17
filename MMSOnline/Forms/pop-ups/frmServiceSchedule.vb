Imports MySql.Data.MySqlClient
Imports Calendar

Public Class frmServiceSchedule
    '*********************************************************************************************
    '*** The Layer property of the Appointment class is used as an ID for the database in this code.
    '*********************************************************************************************
    Public persistant As PreFetch
    Public Store As String
    Public Department As String
    Public isLoading As Boolean = True
    Private m_Appointments As New List(Of Appointment)
    Private selectionStarted As Boolean = False
    Private tableName As String = "ServiceSchedule" 'default setting

    Private Sub frmServiceSchedule_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveAppointments()
    End Sub

    Private Sub frmServiceSchedule_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'set default dayview settings
        DayView1.DaysToShow = 5
        DayView1.Renderer = New Office12Renderer()
        'set default combo box settings
        cboDaysToView.SelectedItem = "5"
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_location, "") - 1)
            If persistant.getvalue(persistant.tbl_location, "Code", "", x) <> "ADM" Then cboStore.Items.Add(persistant.getvalue(persistant.tbl_location, "Code", "", x))
        Next
        If Store = "ADM" Then Store = "EDM"
        cboStore.SelectedItem = Store
        DayView1.StartDate = DateTime.Now

        Select Case Department
            Case "Sales"
                tableName = "SalesSchedule"
                Me.Text = "Delivery Schedule"
            Case "Service"
                tableName = "ServiceSchedule"
                Me.Text = "Service Schedule"
        End Select
        'Load up existing data
        LoadAppointments()

        AddHandler DayView1.NewAppointment, New NewAppointmentEventHandler(AddressOf dayView1_NewAppointment)
        AddHandler DayView1.SelectionChanged, New EventHandler(AddressOf dayView1_SelectionChanged)
        AddHandler DayView1.ResolveAppointments, New Calendar.ResolveAppointmentsEventHandler(AddressOf dayView1_ResolveAppointments)
        AddHandler DayView1.MouseMove, New System.Windows.Forms.MouseEventHandler(AddressOf dayView1_MouseMove)
        'AddHandler DayView1.KeyDown, New System.Windows.Forms.KeyEventHandler(AddressOf dayView1_KeyDown)

        isLoading = False
    End Sub

    Private Sub dayView1_NewAppointment(sender As Object, args As NewAppointmentEventArgs)
        Dim m_Appointment As New Appointment()

        m_Appointment.StartDate = args.StartDate
        m_Appointment.EndDate = args.EndDate
        m_Appointment.Title = args.Title
        m_Appointment.BorderColor = Color.SteelBlue
        m_Appointment.Layer = 0 'use this variable to save the index in.
        'm_Appointment.Group = "2"

        m_Appointments.Add(m_Appointment)
    End Sub

    Private Sub dayView1_MouseMove(sender As Object, e As MouseEventArgs)
        Label2.Text = DayView1.GetTimeAt(e.X, e.Y).ToString()
    End Sub

    Private Sub dayView1_SelectionChanged(sender As Object, e As EventArgs)
        selectionStarted = True
        If DayView1.Selection = SelectionType.DateRange Then
            Label3.Text = DayView1.SelectionStart.ToString() & ":" & DayView1.SelectionEnd.ToString()
        ElseIf DayView1.Selection = SelectionType.Appointment Then
            Label3.Text = DayView1.SelectedAppointment.StartDate.ToString() & ":" & DayView1.SelectedAppointment.EndDate.ToString()
        End If
    End Sub

    Private Sub dayView1_ResolveAppointments(sender As Object, args As ResolveAppointmentsEventArgs)
        Dim m_Apps As New List(Of Appointment)
        'search the database and load the m_Appointments list

        Try
            For Each m_App As Appointment In m_Appointments
                If (m_App.StartDate >= args.StartDate) AndAlso (m_App.StartDate <= args.EndDate) Then
                    m_Apps.Add(m_App)
                End If
            Next

        Catch ex As Exception

        End Try

        args.Appointments = m_Apps
    End Sub

    'Private Sub dayView1_KeyDown(sender As Object, e As KeyEventArgs)
    '    'l'utilisation des touches Suppr 
    '    If dayView1.SelectedAppointment IsNot Nothing Then
    '        If e.KeyValue.ToString() = "46" Then
    '            Dim ID As Integer = DayView1.SelectedAppointment.Layer
    '            m_Appointments.Remove(DayView1.SelectedAppointment)
    '            If ID <> 0 Then DeleteAppointment(ID)
    '            DayView1.Invalidate()

    '        End If
    '    End If
    'End Sub

    Private Sub MonthCalendar1_DateChanged(sender As System.Object, e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        DayView1.StartDate = MonthCalendar1.SelectionStart

        If Not isLoading Then
            'save and reload the data
            SaveAppointments()
            LoadAppointments()
        End If
    End Sub

    Private Sub cboDaysToView_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboDaysToView.SelectedIndexChanged
        Select Case cboDaysToView.SelectedIndex
            Case 0
                DayView1.DaysToShow = 1
            Case 1
                DayView1.DaysToShow = 3
            Case 2
                DayView1.DaysToShow = 5
            Case 3
                DayView1.DaysToShow = 7
        End Select

        If Not isLoading Then
            'save and reload the data
            SaveAppointments()
            LoadAppointments()
        End If
    End Sub

    Private Sub cboStore_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboStore.SelectedIndexChanged
        If Not isLoading Then
            'save and reload the data
            'The form does not refresh unless some data has been set so we just reset the date again.
            DayView1.StartDate = MonthCalendar1.SelectionStart
            SaveAppointments()
            Store = cboStore.SelectedItem
            LoadAppointments()

        End If
    End Sub

    Private Sub btnCreate_Click(sender As System.Object, e As System.EventArgs) Handles btnCreate.Click

        Try
            If selectionStarted Then

                Dim m_App As Appointment = New Appointment()
                m_App.StartDate = DayView1.SelectionStart
                m_App.EndDate = DayView1.SelectionEnd
                m_App.BorderColor = Color.SteelBlue
                m_App.Layer = 0

                m_Appointments.Add(m_App)

                DayView1.Invalidate()
            Else
                MessageBox.Show("Please highlight the appointment time before creating.", "Selection Missing", MessageBoxButtons.OK)

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        Try
            '    'if delete is clicked and no schedule is selected then it will error
            '    'in that case this code will be skipped and nothing will happen.
            If Not (DayView1.SelectedAppointment Is DBNull.Value Or DayView1.SelectedAppointment.Locked) Then
                Dim ID As Integer = DayView1.SelectedAppointment.Layer
                'DayView1.FinishEditing(False)
                m_Appointments.Remove(DayView1.SelectedAppointment)
                If ID <> 0 Then DeleteAppointment(ID)
                DayView1.Invalidate()
            End If
        Catch ex As Exception
            MessageBox.Show("Please select the appointment to delete first.", "Selection Missing", MessageBoxButtons.OK)
        End Try

    End Sub

    Private Sub SaveAppointments()

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        'sd = Replace(Replace(sd, "'", "\'"), ";", "\;")
        'ed = Replace(Replace(sd, "'", "\'"), ";", "\;")
        'MsgBox(sd.ToString & "  " & ed.ToString)

        Try
            conn.ConnectionString = persistant.myconnstring
            cmd.Connection = conn
            conn.Open()
            Dim aC As Integer = m_Appointments.Count
            For Each app As Appointment In m_Appointments
                Dim cmdstring As String = ""
                Dim endString As String = ""
                If app.Title = "" Then Continue For
                If app.Layer = 0 Then
                    'insert
                    cmdstring = "Insert INTO " & tableName & " Set Store = '" & Store & "', "
                Else
                    'update
                    cmdstring = "Update " & tableName & " Set "
                    endString = " Where ScheduleID = '" & app.Layer & "'"
                End If
                cmdstring &= "Title = '" & app.Title & "', StartSched = '" & Format(app.StartDate, "yyyy-MM-dd H:mm:ss") & "', EndSched = '" & Format(app.EndDate, "yyyy-MM-dd H:mm:ss") & "', BoarderColor = '" & 0 & "', FillColor = '" & 0 & "'"
                cmdstring &= endString

                cmd.CommandText = cmdstring
                'Debug.WriteLine(cmdstring)
                cmd.ExecuteNonQuery()
                'cmd.CommandText = "Select MAX(Number) + 1 as NewWONum from ServiceWO where Store = '" + Me.Store + "'"
                'WONumber = CInt(cmd.ExecuteScalar)
            Next
            conn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()


    End Sub

    Private Sub LoadAppointments()
        Dim SQL As String = "Select s.ScheduleID, s.StartSched, s.EndSched, s.Title, s.BoarderColor, s.FillColor " & _
                            "From " & tableName & " as s " & _
                            "Where s.Store = '" & Store & "' " & _
                                "AND s.StartSched > '" & Format(DayView1.StartDate, "yyyy-MM-dd 0:00:00") & "'" & _
                                "AND s.EndSched < '" & Format(DayView1.StartDate.AddDays(cboDaysToView.SelectedItem), "yyyy-MM-dd 23:59:59") & "'"
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter
        Dim newdata As New DataTable

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            newdata.Clear()
            adapter.Fill(newdata)
            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()

        m_Appointments.Clear()
        For Each row As DataRow In newdata.Rows
            Dim m_Appointment As New Appointment
            m_Appointment.StartDate = row.Item("StartSched")
            m_Appointment.EndDate = row.Item("EndSched")
            m_Appointment.Title = row.Item("Title")
            m_Appointment.Layer = row.Item("ScheduleID")

            m_Appointment.BorderColor = Color.SteelBlue

            m_Appointments.Add(m_Appointment)
        Next

    End Sub

    Private Sub DeleteAppointment(ByVal ID As Integer)
        Dim SQL As String = "Delete From " & tableName & _
                            " Where Store = '" & Store & "' " & _
                                "AND ScheduleID = " & ID
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter
        Dim newdata As New DataTable

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            cmd.ExecuteNonQuery()

            'adapter.SelectCommand = cmd
            'newdata.Clear()
            'adapter.Fill(newdata)
            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()

    End Sub

  
End Class