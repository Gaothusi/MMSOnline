Imports MySql.Data.MySqlClient

Public Class equipment
    Public persistant As PreFetch

    Sub motor(ByVal make As String, ByVal model As String)


        If persistant.howmanyrows(persistant.tbl_motors, "brand = '" + make + "' AND Model = '" + model + "'") = 0 Then
            Dim queryuser As New frmnewmotor
            queryuser.motormake = make
            queryuser.motormodel = model
            queryuser.persistant = persistant
            queryuser.ShowDialog()
            persistant.update()
        End If

    End Sub
    Sub boat(ByVal make As String, ByVal model As String)

        If persistant.howmanyrows(persistant.tbl_boats, "brand = '" + make + "' AND Model = '" + model + "'") = 0 Then
            Dim queryuser As New frmnewboat
            queryuser.boatmake = make
            queryuser.boatmodel = model
            queryuser.persistant = persistant
            queryuser.ShowDialog()
            persistant.update()
        End If

    End Sub
    Sub check(ByVal input As String)

        For count As Integer = 0 To howmany(input) - 1

            If persistant.howmanyrows(persistant.tbl_equip, "Shortform = '" + peicex(input, count) + "'") = 0 Then
                Dim queryuser As New frmequip
                queryuser.inputstring = peicex(input, count)
                queryuser.persistant = persistant
                queryuser.ShowDialog()
                persistant.update()
            End If
        Next

    End Sub
    Sub short2long(ByVal input As String, ByRef output As List(Of String))

        Dim templist As New List(Of String)
        For count As Integer = 0 To howmany(input) - 1
            templist.Add(persistant.getvalue(persistant.tbl_equip, "longform", "shortform = '" + peicex(input, count) + "'", 0))
        Next

        output = templist
    End Sub
    Function howmany(ByVal input As String) As Integer
        Dim count As Integer = 0
        Dim lastfound As Integer = 0

        If Replace(input, " ", "") = "" Then
            howmany = 0
            GoTo z

        End If

        If InStr(input, "/") = 0 Then
            howmany = 1
            GoTo z
        End If
        lastfound = InStr(input, "/")
        count = count + 1

a:
        If lastfound <> 0 Then
            lastfound = InStr(lastfound + 1, input, "/")
            count = count + 1
            GoTo a
        End If
        howmany = count

z:
    End Function
    Function peicex(ByVal input As String, ByVal x As Integer)
        Dim leftbound, rightbound As Integer
        If howmany(input) = 0 Then
            peicex = ""
            GoTo z
        End If
        If howmany(input) = 1 Then
            peicex = input
            GoTo z
        End If
        If x = 0 Then
            rightbound = InStr(input, "/")
            peicex = Left(input, rightbound - 1)
            GoTo z
        End If
        If x = howmany(input) - 1 Then
            leftbound = 1
a:
            rightbound = InStr(leftbound, input, "/")
            If rightbound <> 0 Then
                leftbound = rightbound + 1
                GoTo a
            End If
            peicex = Right(input, Len(input) - (leftbound - 1))
            GoTo z
        End If
        rightbound = 0
        leftbound = 0
        For y As Integer = 0 To x
            leftbound = rightbound
            rightbound = InStr(leftbound + 1, input, "/")
        Next
        peicex = Mid(input, leftbound + 1, rightbound - (leftbound + 1))
z:

    End Function
End Class
