Imports System.Data
Imports MySql.Data.MySqlClient
'Imports AxMicrosoft.Office.Interop.Owc11

Public Class frmaddboatsn
    Public persistant As PreFetch
    Public passedboat As New orderinfo
    Private colorder(22) As feildslist
    Private cantleave As Boolean

    Private Sub frmtemp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim headercell, datacell, header As String
        buildcollist()
        writeheaders()
        cantleave = False
        If passedboat.boatmake <> Nothing Then
            cantleave = True
            For z As Integer = 1 To 23
                headercell = letterx(z) + "1"
                datacell = letterx(z) + "2"
                header = Me.AxSpreadsheet1.Cells.Range(headercell, headercell).Value.ToLower
                If header = "location" Then Me.AxSpreadsheet1.Cells.Range(datacell, datacell).Value = passedboat.store
                If header = "boat_brand" Then Me.AxSpreadsheet1.Cells.Range(datacell, datacell).Value = passedboat.boatmake
                If header = "boat_model" Then Me.AxSpreadsheet1.Cells.Range(datacell, datacell).Value = passedboat.boatmodel
                If header = "boat_year" Then Me.AxSpreadsheet1.Cells.Range(datacell, datacell).Value = passedboat.boatyear.ToString
                If header = "boat_color" Then Me.AxSpreadsheet1.Cells.Range(datacell, datacell).Value = passedboat.colour
                If header = "equipment" Then Me.AxSpreadsheet1.Cells.Range(datacell, datacell).Value = passedboat.equipment
                If header = "engine_make" Then Me.AxSpreadsheet1.Cells.Range(datacell, datacell).Value = passedboat.motormake
                If header = "engine_model" Then Me.AxSpreadsheet1.Cells.Range(datacell, datacell).Value = passedboat.motormodel
                If header = "price" Then Me.AxSpreadsheet1.Cells.Range(datacell, datacell).Value = passedboat.price.ToString
                If header = "comments" Then Me.AxSpreadsheet1.Cells.Range(datacell, datacell).Value = passedboat.notes + " - " + passedboat.customername + " [" + passedboat.store + " " + passedboat.bos.ToString + "] FOR " + passedboat.salesman
            Next
        End If
        Me.AxSpreadsheet1.Columns.AutoFit()
    End Sub

    Private Sub frmAddBoats_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If cantleave = True Then
            MessageBox.Show("You must add the request boat before you can exit")
            e.Cancel = True
        Else
            If Me.AxSpreadsheet1.Cells.Range("A2", "A2").Text().ToString <> "" Then
                If MessageBox.Show("Are you sure you want to exit without adding these boats to inventory?", "Exit", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Public Sub saveboats()
        Dim temploccode As String
        Dim e As New equipment
        e.persistant = persistant
        Dim hereval As String
        Me.AxSpreadsheet1.Cells(1, 1).Select()

        Dim equip, boatmake, boatmodel, motormake, motormodel As String
        Dim blankrowsinarow As Integer

        equip = ""
        boatmake = ""
        boatmodel = ""
        motormake = ""
        motormodel = ""

        Dim row As Integer
        Dim invstring, cell, headercell, header As String
        Dim cols As New List(Of colinfo)
        Dim ok, done As Boolean
        ok = True
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        row = 2
        invstring = ""
        done = False
        blankrowsinarow = 0

        conn.ConnectionString = persistant.myconnstring
        cmd.Connection = conn

        Try
            conn.Open()
        Catch ex As MySqlException
            Debug.WriteLine(ex.Message)
        End Try

        While done = False
            If Me.AxSpreadsheet1.Cells.Range("A" + row.ToString, "A" + row.ToString).Text().ToString <> "" Then
                blankrowsinarow = 0

                invstring = "insert into inventory set "
                For col As Integer = 1 To 23
                    cell = letterx(col) + row.ToString
                    headercell = letterx(col) + "1"
                    header = Me.AxSpreadsheet1.Cells.Range(headercell, headercell).Value.ToLower

                    If header = "boat_year" And Val(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()) = 0 Then
                        MessageBox.Show("All boats must have a year")
                        ok = False
                    End If

                    If header = "boat_brand" Then
                        'Fix brand Case and spelling if you can
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "four winns" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Four Winns"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "fourwinns" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Four Winns"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "maxum" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Maxum"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "bayliner" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Bayliner"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "searay" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Searay"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "hewescraft" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Hewescraft"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "centurion" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Centurion"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "monterey" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Monterey"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "montarey" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Monterey"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "monteray" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Monterey"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "glastron" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Glastron"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "sylvan" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Sylvan"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "smokercraft" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Smokercraft"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "used" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Used"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "odyssey" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Odyssey"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "double eagle" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Double Eagle"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "cdory" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "C-Dory"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "c dory" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "C-Dory"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "c-dory" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "C-Dory"
                        End If
                        If Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower = "campion" Then
                            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value = "Campion"
                        End If


                        boatmake = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                        If formatcell(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value(), colorder(col - 1).numeric) = "null" Then
                            MessageBox.Show("You must enter a Brand for every boat")
                            ok = False
                        End If
                    End If

                    If header = "boat_model" Then
                        boatmodel = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                        If formatcell(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value(), colorder(col - 1).numeric) = "null" Then
                            MessageBox.Show("You must enter a Model for every boat")
                            ok = False
                        End If
                    End If

                    If header = "engine_make" Then
                        motormake = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                        If formatcell(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value(), colorder(col - 1).numeric) = "null" Then
                            MessageBox.Show("You must enter a Engine Make for every boat")
                            ok = False
                        End If
                    End If

                    If header = "engine_model" Then
                        motormodel = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                        If formatcell(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value(), colorder(col - 1).numeric) = "null" Then
                            MessageBox.Show("You must enter a Engine Model for every boat")
                            ok = False
                        End If
                    End If

                    If header = "location" Then
                        temploccode = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value.ToString.ToLower
                        If temploccode = "edm" Or temploccode = "atl" Or temploccode = "ren" Or temploccode = "syl" Or temploccode = "ogo" Or temploccode = "bth" Or temploccode = "gal" Then
                        Else
                            MessageBox.Show("You must enter a valid 3 letter location code for every boat")
                            ok = False
                        End If
                    End If

                    If header = "equipment" Then equip = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value()
                    For x As Integer = 0 To 22
                        If colorder(x).value.ToLower = header Then
                            If header = "est_arrival_date" Then
                                Try
                                    invstring = invstring + header + " = '" + Format(CDate(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value), "yyyy-MM-dd H:mm:ss") + "', "
                                Catch ex As Exception
                                    '  MessageBox.Show("You must enter a valid date for the expected arrival date")
                                    '  ok = False
                                End Try
                            Else
                                If header = "here" Then
                                    hereval = Me.AxSpreadsheet1.Cells.Range(cell, cell).Value
                                    If hereval = Nothing Then hereval = "yes"
                                    If hereval.ToLower = "no" Then
                                        invstring = invstring + "here = 'NO', "
                                    Else
                                        invstring = invstring + "here = 'YES', "
                                    End If
                                Else
                                    If header = "boat_year" Then
                                        invstring = invstring + header + " = " + formatcell(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value(), colorder(x).numeric) + ", "
                                        invstring = invstring + "Trailer_Year = " + formatcell(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value(), colorder(x).numeric) + ", "
                                        invstring = invstring + "Engine_Year = " + formatcell(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value(), colorder(x).numeric) + ", "
                                    Else
                                        If formatcell(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value(), colorder(x).numeric).Length <> 0 And formatcell(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value(), colorder(x).numeric) <> "null" Then
                                            invstring = invstring + header + " = " + formatcell(Me.AxSpreadsheet1.Cells.Range(cell, cell).Value(), colorder(x).numeric) + ", "
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                Next
                If boatmake <> "" And boatmodel <> "" Then e.boat(boatmake, boatmodel)
                If motormake <> "" And motormodel <> "" Then e.motor(motormake, motormodel)
                e.check(equip)
                invstring = invstring.Remove(invstring.Length - 2, 2)
                If ok Then
                    Try
                        Debug.WriteLine(invstring)
                        cmd.CommandText = invstring
                        cmd.ExecuteNonQuery()

                    Catch ex As MySqlException
                        Debug.WriteLine(invstring)
                        Debug.WriteLine(ex.Message)

                        ok = False
                    End Try
                End If
                row = row + 1
            Else
                blankrowsinarow = blankrowsinarow + 1
                row = row + 1
                If blankrowsinarow = 5 Then done = True
            End If

        End While

        If ok Then
            cantleave = False

            Me.AxSpreadsheet1.Rows.Clear()
            writeheaders()
            Dim numinvrecord, numinvrrecord As Integer
            Try
                'conn.ConnectionString = persistant.myconnstring
                cmd.Connection = conn
                cmd.CommandText = "Select max(control_number) from inventory"
                numinvrecord = Val(cmd.ExecuteScalar())
                cmd.CommandText = "Select max(control_number) from invrestricted"
                numinvrrecord = Val(cmd.ExecuteScalar())
                If numinvrecord <> numinvrrecord Then
                    For x As Integer = (numinvrrecord + 1) To numinvrecord
                        Try
                            cmd.CommandText = "INSERT into invrestricted SET Control_number = '" + x.ToString + "'"
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception
                            MessageBox.Show("ERROR: " + ex.Message)
                        End Try
                    Next
                End If
            Catch ex As Exception
                ok = False
            End Try
        End If
        Try
            conn.Close()
        Catch ex As MySqlException
            Debug.WriteLine(ex.Message)
        End Try

        conn.Dispose()


    End Sub
    Private Sub buildcollist()
        Dim x As Integer

        For i As Integer = 0 To 22
            colorder(i) = New feildslist
        Next

        x = 0
        colorder(x).value = "Boat_brand"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "Boat_model"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "engine_make"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "engine_model"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "drive_model"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "trailer_make"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "trailer_model"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "trailer_color"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "Equipment"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "Boat_color"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "price"
        colorder(x).numeric = True
        x = x + 1
        colorder(x).value = "discount"
        colorder(x).numeric = True
        x = x + 1
        colorder(x).value = "discount_reason"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "Boat_serial"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "Boat_hin"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "Boat_year"
        colorder(x).numeric = True
        x = x + 1
        colorder(x).value = "Location"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "comments"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "here"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "engine_serial"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "drive_serial"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "tplate_serial"
        colorder(x).numeric = False
        x = x + 1
        colorder(x).value = "est_arrival_date"
        colorder(x).numeric = False
    End Sub
    Public Sub writeheaders()
        Dim cell As String
        For x As Integer = 0 To 22
            cell = letterx(x + 1) + (1).ToString
            Me.AxSpreadsheet1.Cells.Range(cell, cell).Value() = colorder(x).value
        Next
    End Sub

    Public Function formatcell(ByVal input As String, ByVal numeric As Boolean) As String
        Dim temp As String
        temp = input
        If numeric Then temp = Val(input).ToString
        temp = Replace(temp, "'", "\'")
        temp = Replace(temp, ";", "\;")
        formatcell = "'" + temp + "'"

        If input = "" Then formatcell = "null"

    End Function
    Public Function letterx(ByVal input As Integer) As String
        Dim alphabit, tempresult As String
        Dim x As Integer

        alphabit = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        tempresult = ""
        If input > 26 Then
            x = (input - (input Mod 26)) / 26
            If input Mod 26 = 0 Then
                tempresult = Mid(alphabit, x - 1, 1)
            Else
                tempresult = Mid(alphabit, x, 1)
            End If

        End If
        x = input Mod 26
        If x = 0 Then x = 26

        tempresult = tempresult + Mid(alphabit, x, 1)

        letterx = tempresult
    End Function


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        saveboats()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.AxSpreadsheet1.Columns.AutoFit()
    End Sub
End Class


Public Class feildslist
    Public value As String
    Public numeric As Boolean
End Class

