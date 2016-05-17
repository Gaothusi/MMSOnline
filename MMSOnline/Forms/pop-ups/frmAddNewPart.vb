Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class frmAddNewPart

    Public persistant As PreFetch
    Public PartID As Integer
    Public PartsOrderID As Integer
    Public IssueNum As Integer
    Public WONum As Integer
    Public Store As String
    Public CustNum As Integer
    Public OrderMe As Boolean

    Private parts As New PartsData
    Private Const mydatacount As Integer = 7
    Private mydata(mydatacount) As element

    Private Sub frmAddNewPart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        permisions()
        setuparray()
        If PartID = 0 Then
            txtQuantity.Text = "1"
            decimalchanged(txtQuantity)
            If OrderMe Then
                cboOrderStatus.SelectedItem = "To Be Ordered"
            Else
                cboOrderStatus.Items.Add("In Stock")
                cboOrderStatus.Items.Add("To Be Ordered")
                cboOrderStatus.SelectedItem = "In Stock"
                cboOrderStatus.Visible = False
                lblOrderStatus.Visible = False
            End If
            cmbchanged(cboOrderStatus)
        Else
            parts.myconnstring = persistant.myconnstring
            parts.Number = PartID
            parts.Download()
            For x As Integer = 0 To mydatacount
                If mydata(x).dbtable = "Part" Then mydata(x).value = parts.getvalue(mydata(x).dbfeild)
            Next
            For x As Integer = 0 To mydatacount
                If x = 3 Then
                    If mydata(x).value <> "" Then mydata(x).txtbox.text = Format(mydata(x).value, "Currency")
                ElseIf x = 6 Or x = 1 Or x = 7 Then
                    'if this is in stock then clear the combobox and only allow in stock and to be ordered (requested by Jean)

                    If mydata(x).value <> "" Then
                        If mydata(x).value = "In Stock" And x = 6 Then
                            cboOrderStatus.Items.Clear()
                            cboOrderStatus.Items.Add("In Stock")
                            cboOrderStatus.Items.Add("To Be Ordered")
                            cboOrderStatus.Items.Add("Order")
                            cboOrderStatus.Items.Add("Back Ordered")
                            cboOrderStatus.Items.Add("Received")
                            cboOrderStatus.Items.Add("Picked Up")
                            cboOrderStatus.Items.Add("Placed In Stock")
                            cboOrderStatus.Items.Add("Installed")

                        End If
                        mydata(x).txtbox.SelectedItem = mydata(x).value
                    End If
                    'If mydata(x).value <> "" Then mydata(x).txtbox.SelectedItem = mydata(x).value
                    'If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
                Else
                    If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
                End If

            Next
        End If
    End Sub

    Private Sub setuparray()
        Dim i As Integer

        For x As Integer = 0 To (mydata.Length - 1)
            mydata(x) = New element
        Next

        i = 0
        mydata(i).txtbox = Me.txtDesc
        mydata(i).dbfeild = "Part"
        mydata(i).dbtable = "Part"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 1
        'mydata(i).txtbox = Me.txtSupplier
        mydata(i).txtbox = Me.cboSupplier
        mydata(i).dbfeild = "Supplier"
        mydata(i).dbtable = "Part"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 2
        mydata(i).txtbox = Me.txtpartnum
        mydata(i).dbfeild = "PartNumber"
        mydata(i).dbtable = "Part"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 3
        mydata(i).txtbox = Me.txtprice
        mydata(i).dbfeild = "Price"
        mydata(i).dbtable = "Part"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 4
        mydata(i).txtbox = Me.txtQuantity
        mydata(i).dbfeild = "Quantity"
        mydata(i).dbtable = "Part"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 5
        mydata(i).txtbox = Me.txtConfirmationNum
        mydata(i).dbfeild = "ConfirmationNum"
        mydata(i).dbtable = "Part"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 6
        mydata(i).txtbox = Me.cboOrderStatus
        mydata(i).dbfeild = "Status"
        mydata(i).dbtable = "Part"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 7
        mydata(i).txtbox = Me.cboPaymentType
        mydata(i).dbfeild = "PaymentType"
        mydata(i).dbtable = "Part"
        mydata(i).changed = False
        mydata(i).value = ""

    End Sub

    Private Sub permisions()
        If persistant.myuserLEVEL > 4 Then
            cboOrderStatus.Items.Add("Void")
        End If
    End Sub


    Private Sub save()
        txtPrice.Select()
        txtDesc.Select()

        For x As Integer = 0 To mydatacount
            mydata(x).value = Replace(mydata(x).value, "'", "\'")
            mydata(x).value = Replace(mydata(x).value, ";", "\;")
        Next

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        cmd.Connection = conn
        Dim cmdstring As String = ""
        If PartID = 0 Then
            'if the partorderID is 0 then create a new order ID and send it to the new part form.
            If PartsOrderID = 0 And OrderMe = True Then
                'Dim conn1 As New MySqlConnection
                'Dim cmd1 As New MySqlCommand
                conn.ConnectionString = persistant.myconnstring
                cmd.Connection = conn
                Dim ob As String = persistant.getvalue(persistant.tbl_users, "name", "user = '" + persistant.myuserID + "'", 0)
                Dim strSQL As String = ""
                Try
                    strSQL = "Insert INTO ServicePartsOrders Set Store = '" & Store & "', " & _
                        "WorkOrderNum = '" & WONum.ToString & "', CustomerProfile = '" & CustNum.ToString & "', " & _
                        "OrderedBy = '" & ob & "'"

                Catch ex As Exception

                End Try

                cmd.CommandText = strSQL
                Debug.WriteLine(strSQL)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    'get the created partsOrderID
                    cmd.CommandText = "Select MAX(PartsOrderID) from ServicePartsOrders"
                    PartsOrderID = CInt(cmd.ExecuteScalar)
                    conn.Close()
                Catch ex As MySqlException
                    MessageBox.Show(ex.Message)
                End Try
                conn.Dispose()

            End If

            cmdstring = "Insert INTO ServiceParts Set "
            For x As Integer = 0 To mydatacount
                If mydata(x).dbtable = "Part" And mydata(x).changed = True Then
                    cmdstring = cmdstring & mydata(x).dbfeild & " = '" & mydata(x).value & "', "
                End If
            Next
            cmdstring = cmdstring & "IssueNumber = '" & IssueNum.ToString & "', "
            cmdstring = cmdstring & "PartsOrderID = '" & PartsOrderID.ToString & "'"

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

        Else
            Dim somethingchanged As Boolean = False
            For x As Integer = 0 To mydatacount
                If mydata(x).dbtable = "Part" And mydata(x).changed = True Then
                    somethingchanged = True
                End If
            Next

            If somethingchanged Then
                cmdstring = "Update ServiceParts Set "
                For x As Integer = 0 To mydatacount
                    If mydata(x).dbtable = "Part" And mydata(x).changed = True Then
                        If cmdstring <> "Update ServiceParts Set " Then
                            cmdstring = cmdstring & ", "
                        End If
                        cmdstring = cmdstring & mydata(x).dbfeild & " = '" & mydata(x).value & "'"
                    End If
                Next
                cmdstring = cmdstring & "Where Number = '" & PartID.ToString & "'"

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
            End If
        End If

        For x As Integer = 0 To mydatacount
            mydata(x).value = Replace(mydata(x).value, "\'", "'")
            mydata(x).value = Replace(mydata(x).value, "\;", ";")
        Next

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If cboOrderStatus.SelectedItem = "Void" Then txtPrice.Text = "0"
        save()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub moneychanged(ByRef sender As Object)

        Dim x As String
        Dim y As Decimal
        x = Replace(sender.text, "$", "")
        x = Replace(x, ",", "")
        x = Replace(x, "(", "-")
        x = Replace(x, ")", "")
        y = Val(x)

        If y <> 0 Then
            For i As Integer = 0 To (mydata.Length - 1)
                If sender.Equals(mydata(i).txtbox) = True Then
                    If Val(mydata(i).value) <> y Then
                        mydata(i).value = y
                        mydata(i).changed = True
                    End If
                    If i > 41 And i < 49 Then
                        mydata(i - 12).value = "1"
                        mydata(i - 12).txtbox.checked = True
                    End If
                    If i > 63 And i < 69 Then
                        mydata(i - 27).value = "1"
                        mydata(i - 27).txtbox.checked = True
                    End If
                    sender.text = Format(x, "currency")
                End If
            Next
        End If

        If y = 0 Then
            For i As Integer = 0 To (mydata.Length - 1)
                If sender.Equals(mydata(i).txtbox) = True Then
                    If Val(mydata(i).value) <> y Then
                        mydata(i).value = y.ToString
                        mydata(i).changed = True
                    End If
                    If i > 41 And i < 49 Then
                        mydata(i - 12).value = "0"
                        mydata(i - 12).txtbox.checked = False
                    End If
                    If i > 63 And i < 69 Then
                        mydata(i - 27).value = "0"
                        mydata(i - 27).txtbox.checked = False
                    End If
                    sender.text = ""
                End If
            Next
        End If
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
    Private Sub cmbchanged(ByVal sender As Object)
        Try
            Dim data As String
            For i As Integer = 0 To (mydata.Length - 1)
                If sender.Equals(mydata(i).txtbox) = True Then
                    data = sender.selecteditem
                    mydata(i).value = data
                    mydata(i).changed = True
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub
    Private Sub decimalchanged(ByRef sender As Object)
        Dim y As Decimal

        Try
            y = Val(sender.text)

        Catch ex As Exception
            y = 0
        End Try

        If y <> 0 Then
            For i As Integer = 0 To (mydata.Length - 1)
                If sender.Equals(mydata(i).txtbox) = True Then
                    If Val(mydata(i).value) <> y Then
                        mydata(i).value = y.ToString
                        mydata(i).changed = True
                    End If
                    sender.text = y.ToString
                End If
            Next
        End If

        If y = 0 Then
            For i As Integer = 0 To (mydata.Length - 1)
                If sender.Equals(mydata(i).txtbox) = True Then
                    If Val(mydata(i).value) <> y Then
                        mydata(i).value = y.ToString
                        mydata(i).changed = True
                    End If
                    sender.text = "0.0"
                End If
            Next
        End If
    End Sub
    Private Sub moneyboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrice.Leave
        moneychanged(sender)
    End Sub
    Private Sub txtquantity_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQuantity.Leave
        decimalchanged(sender)
    End Sub
    Private Sub stringboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDesc.Leave, txtPartNum.Leave, txtSupplier.Leave, txtConfirmationNum.Leave
        stringchanged(sender)
    End Sub
    Private Sub cboOrderStatus_Leave(sender As Object, e As System.EventArgs) Handles cboOrderStatus.Leave, cboSupplier.Leave, cboPaymentType.Leave
        cmbchanged(sender)
    End Sub

End Class