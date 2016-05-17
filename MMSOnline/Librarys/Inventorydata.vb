Imports MySql.Data.MySqlClient
Imports System.Data

Public Class inventorydata
    Public persistant As PreFetch

    Public Control_number As Integer


   

    Public WriteOnly Property controlnumber() As Integer
        Set(ByVal value As Integer)
            Control_number = value
        End Set
    End Property

    Public Location As String
    Public Boat_year As Integer
    Public Boat_brand As String
    Public Boat_model As String
    Public Boat_serial As String
    Public Boat_hin As String
    Public Boat_color As String
    Public Equipment As String
    Public Engine_make As String
    Public engine_model As String
    Public engine_serial As String
    Public engine_year As Integer
    Public drive_model As String
    Public drive_serial As String
    Public Tplate_serial As String
    Public Trailer_make As String
    Public trailer_model As String
    Public trailer_serial As String
    Public trailer_year As Integer
    Public trailer_color As String
    Public price As Decimal
    Public discount As Decimal
    Public comments As String
    Public here As Boolean
    Public est_arrival_date As Date
    Public discount_reason As String
    Public state As Integer
    Public bos_num As Integer
    Public bos_store As String
    Public Commision As String

    Public Load_Number As String
    Public Invoice_Number As String
    Public Invoice_Date As Date
    Public Boat_US_cost As Decimal
    Public TRL_US_cost As Decimal
    Public FRT_US_cost As Decimal
    Public TOTAL_US_cost As Decimal
    Public GE_X_Rate As Decimal
    Public EST_Can_Cost As Decimal
    Public Canadian_cost As Decimal
    Public Inv_Move_to_Cost_of_Goods_sold As Decimal
    Public Remaining_CDN_cost_inv As Decimal
    Public GE_Ammount_Financed As Decimal
    Public PLA_cost_or_Cheque As String
    Public GE_Amt_Paid As Decimal
    Public GE_amt_Owing As Decimal
    Public GE_paid_date As Date
    Public Free_lnterest_Until As Date
    Public Maturity_Date As Date
    Public Financed_Company As String
    Public Rebate As Decimal
    Public Boat_Reg_date As Date
    Public Motor_Reg_Date As Date
    Public Accounting_Notes As String

    Public Sub Clear()
        bos_num = Nothing
        bos_store = Nothing
        Control_number = Nothing
        Location = Nothing
        Boat_year = Nothing
        Boat_brand = Nothing
        Boat_model = Nothing
        Boat_serial = Nothing
        Boat_hin = Nothing
        Boat_color = Nothing
        Equipment = Nothing
        Engine_make = Nothing
        engine_model = Nothing
        engine_serial = Nothing
        engine_year = Nothing
        drive_model = Nothing
        drive_serial = Nothing
        Tplate_serial = Nothing
        Trailer_make = Nothing
        trailer_model = Nothing
        trailer_serial = Nothing
        trailer_year = Nothing
        trailer_color = Nothing
        price = Nothing
        discount = Nothing
        comments = Nothing
        Commision = Nothing
        here = Nothing
        est_arrival_date = Nothing
        discount_reason = Nothing
        Load_Number = Nothing
        Invoice_Number = Nothing
        Invoice_Date = Nothing
        Boat_US_cost = Nothing
        TRL_US_cost = Nothing
        FRT_US_cost = Nothing
        TOTAL_US_cost = Nothing
        GE_X_Rate = Nothing
        EST_Can_Cost = Nothing
        Canadian_cost = Nothing
        Inv_Move_to_Cost_of_Goods_sold = Nothing
        Remaining_CDN_cost_inv = Nothing
        GE_Ammount_Financed = Nothing
        PLA_cost_or_Cheque = Nothing
        GE_Amt_Paid = Nothing
        GE_amt_Owing = Nothing
        GE_paid_date = Nothing
        Free_lnterest_Until = Nothing
        Maturity_Date = Nothing
        Financed_Company = Nothing
        Rebate = Nothing
        Boat_Reg_date = Nothing
        Motor_Reg_Date = Nothing
        Accounting_Notes = Nothing
        state = Nothing
    End Sub

    Public Sub readDB()
        Dim conn As New MySqlConnection
        Dim myCommand As New MySqlCommand
        Dim reader As MySqlDataReader
        Dim tempstring As String
        tempstring = "1/1/0001 12:00:00 AM"
        Boat_Reg_date = CDate(tempstring)
        Free_lnterest_Until = CDate(tempstring)
        GE_paid_date = CDate(tempstring)
        Invoice_Date = CDate(tempstring)
        Maturity_Date = CDate(tempstring)
        Motor_Reg_Date = CDate(tempstring)
        est_arrival_date = CDate(tempstring)

        conn.ConnectionString = persistant.myconnstring
        conn.Open()
        myCommand.Connection = conn
        myCommand.CommandText = "SELECT * from inventory where control_number = ?ctrlnum"
        myCommand.Prepare()
        'myCommand.Parameters.AddWithValue("?ctrlnum", Control_number)
        myCommand.Parameters.Add("?ctrlnum", Control_number)
        reader = myCommand.ExecuteReader()

        While (reader.Read)
            tempstring = reader("here")
            If tempstring = "YES" Then here = True Else here = False
            Location = reader("Location").ToString
            Try
                tempstring = reader("est_arrival_date").ToString
                If tempstring <> "" Then est_arrival_date = CDate(tempstring)
            Catch ex As Exception
            End Try
            bos_num = Val(reader("BOS_Number").ToString)
            bos_store = reader("BOS_Store").ToString
            Boat_color = reader("Boat_color").ToString
            Boat_hin = reader("Boat_hin").ToString
            Boat_brand = reader("Boat_brand").ToString
            Boat_model = reader("Boat_model").ToString
            Boat_serial = reader("Boat_serial").ToString
            Boat_year = reader("Boat_year").ToString
            comments = reader("comments").ToString
            Control_number = reader("Control_number").ToString
            discount = Val(reader("discount").ToString)
            discount_reason = reader("discount_reason").ToString
            drive_model = reader("drive_model").ToString
            drive_serial = reader("drive_serial").ToString
            Equipment = reader("Equipment").ToString
            Engine_make = reader("Engine_make").ToString
            engine_model = reader("engine_model").ToString
            engine_serial = reader("engine_serial").ToString
            engine_year = Val(reader("engine_year").ToString)
            price = Val(reader("price").ToString)
            Tplate_serial = reader("Tplate_serial").ToString
            trailer_color = reader("trailer_color").ToString
            Trailer_make = reader("Trailer_make").ToString
            trailer_model = reader("trailer_model").ToString
            trailer_serial = reader("trailer_serial").ToString
            trailer_year = Val(reader("trailer_year").ToString)
            state = reader("state").ToString
            commision = reader("commision").ToString
        End While
        reader.Close()


        If persistant.myuserLEVEL > 7 Then
            myCommand.Connection = conn
            myCommand.CommandText = "SELECT * from invrestricted where control_number = ?ctrlnum"
            myCommand.Prepare()
            'myCommand.Parameters.AddWithValue("?ctrlnum", Control_number)
            reader = myCommand.ExecuteReader()

            While (reader.Read)
                Financed_Company = reader("Financed_Company").ToString
                Try
                    'If the date is bad it will not even get the string value of the date
                    tempstring = reader("Boat_Reg_date").ToString
                    If tempstring <> "" Then Boat_Reg_date = CDate(tempstring)
                Catch ex As Exception
                End Try
                Try
                    tempstring = reader("Free_interest_Until").ToString
                    If tempstring <> "" Then Free_lnterest_Until = CDate(tempstring)
                Catch ex As Exception
                End Try
                Try
                    tempstring = reader("GE_paid_date").ToString
                    If tempstring <> "" Then GE_paid_date = CDate(tempstring)
                Catch ex As Exception
                End Try
                Try
                    tempstring = reader("Invoice_Date").ToString
                    If tempstring <> "" Then Invoice_Date = CDate(tempstring)
                Catch ex As Exception
                End Try
                Try
                    tempstring = reader("Maturity_Date").ToString
                    If tempstring <> "" Then Maturity_Date = CDate(tempstring)
                Catch ex As Exception
                End Try
                Try
                    tempstring = reader("Motor_Reg_Date").ToString
                    If tempstring <> "" Then Motor_Reg_Date = CDate(tempstring)
                Catch ex As Exception
                End Try

                Remaining_CDN_cost_inv = Val(reader("Remaining_CDN_cost_inv").ToString)
                Boat_US_cost = Val(reader("Boat_US_cost").ToString)
                Canadian_cost = Val(reader("Canadian_cost_Inv").ToString)
                EST_Can_Cost = Val(reader("EST_Can_Cost").ToString)
                FRT_US_cost = Val(reader("FRT_US_cost").ToString)
                GE_Ammount_Financed = Val(reader("GE_Ammount_Financed").ToString)
                GE_Amt_Paid = Val(reader("GE_Amt_Paid").ToString)
                GE_X_Rate = Val(reader("GE_X_Rate").ToString)
                GE_amt_Owing = Val(reader("GE_amt_Owing").ToString)
                Inv_Move_to_Cost_of_Goods_sold = Val(reader("Inv_Move_to_Cost_of_Goods_sold").ToString)
                Invoice_Number = reader("Invoice_Number").ToString
                Load_Number = reader("Load_Number").ToString
                Accounting_Notes = reader("Accounting_Notes").ToString
                PLA_cost_or_Cheque = reader("PLA_or_Cheque").ToString
                Rebate = Val(reader("Rebate").ToString)
                TOTAL_US_cost = Val(reader("TOTAL_US_cost").ToString)

            End While
        End If
        reader.Close()
        conn.Close()
        conn.Dispose()

    End Sub
    Public Function WriteDB() As Boolean

        Dim conn As New MySqlConnection
        Dim myCommand As New MySqlCommand
        Dim cmdtxt As String
        conn.ConnectionString = persistant.myconnstring
        conn.Open()


        myCommand.Connection = conn
        cmdtxt = "INSERT into inventory SET "

        If Location <> Nothing Then cmdtxt = cmdtxt + "Location = '" + cleanup(Location) + "'"
        If Boat_year <> Nothing Then cmdtxt = cmdtxt + ", Boat_year = '" + Boat_year.ToString + "'"
        If Boat_brand <> Nothing Then cmdtxt = cmdtxt + ", Boat_brand = '" + cleanup(Boat_brand) + "'"
        If Boat_model <> Nothing Then cmdtxt = cmdtxt + ", Boat_model = '" + cleanup(Boat_model) + "'"
        If Boat_serial <> Nothing Then cmdtxt = cmdtxt + ", Boat_serial = '" + cleanup(Boat_serial) + "'"
        If Boat_hin <> Nothing Then cmdtxt = cmdtxt + ", Boat_hin = '" + cleanup(Boat_hin) + "'"
        If Boat_color <> Nothing Then cmdtxt = cmdtxt + ", Boat_color = '" + cleanup(Boat_color) + "'"
        If Equipment <> Nothing Then cmdtxt = cmdtxt + ", Equipment = '" + cleanup(Equipment) + "'"
        If Engine_make <> Nothing Then cmdtxt = cmdtxt + ", Engine_make = '" + cleanup(Engine_make) + "'"
        If engine_model <> Nothing Then cmdtxt = cmdtxt + ", engine_model = '" + cleanup(engine_model) + "'"
        If engine_serial <> Nothing Then cmdtxt = cmdtxt + ", engine_serial = '" + cleanup(engine_serial) + "'"
        If engine_year <> Nothing Then cmdtxt = cmdtxt + ", engine_year = '" + engine_year.ToString + "'"
        If drive_model <> Nothing Then cmdtxt = cmdtxt + ", drive_model = '" + cleanup(drive_model) + "'"
        If drive_serial <> Nothing Then cmdtxt = cmdtxt + ", drive_serial = '" + cleanup(drive_serial) + "'"
        If Tplate_serial <> Nothing Then cmdtxt = cmdtxt + ", Tplate_serial = '" + cleanup(Tplate_serial) + "'"
        If Trailer_make <> Nothing Then cmdtxt = cmdtxt + ", Trailer_make = '" + cleanup(Trailer_make) + "'"
        If trailer_model <> Nothing Then cmdtxt = cmdtxt + ", trailer_model = '" + cleanup(trailer_model) + "'"
        If trailer_serial <> Nothing Then cmdtxt = cmdtxt + ", trailer_serial = '" + cleanup(trailer_serial) + "'"
        If trailer_year <> Nothing Then cmdtxt = cmdtxt + ", trailer_year = '" + trailer_year.ToString + "'"
        If trailer_color <> Nothing Then cmdtxt = cmdtxt + ", trailer_color = '" + cleanup(trailer_color) + "'"
        If bos_num <> Nothing Then cmdtxt = cmdtxt + ", BOS_number = '" + cleanup(bos_num) + "'"
        If bos_num = Nothing Then cmdtxt = cmdtxt + ", BOS_number = Null"
        If bos_store <> Nothing Then cmdtxt = cmdtxt + ", BOS_Store = '" + cleanup(bos_store) + "'"
        If price <> Nothing Then cmdtxt = cmdtxt + ", price = '" + price.ToString + "'"
        If discount <> Nothing Then cmdtxt = cmdtxt + ", discount = '" + discount.ToString + "'"
        If comments <> Nothing Then cmdtxt = cmdtxt + ", comments = '" + cleanup(comments) + "'"
        If here = True Then cmdtxt = cmdtxt + ", here = 'YES'"
        If here = False Then cmdtxt = cmdtxt + ", here = 'NO'"
        If est_arrival_date <> Nothing Then cmdtxt = cmdtxt + ", est_arrival_date = '" + Format(est_arrival_date, "yyyy-MM-dd") + "'"
        If discount_reason <> Nothing Then cmdtxt = cmdtxt + ", discount_reason = '" + cleanup(discount_reason) + "'"
        If Commision <> Nothing Then cmdtxt = cmdtxt + ", Commision = '" + cleanup(Commision) + "'"
        cmdtxt = cmdtxt + ", state = '" + state.ToString + "'"

        myCommand.CommandText = cmdtxt



        Try
            myCommand.ExecuteNonQuery()

            myCommand.CommandText = "Select MAX(control_number) from inventory"
            Try
                Control_number = myCommand.ExecuteScalar()
            Catch ex As Exception
                MessageBox.Show("ERROR: " + ex.Message)
                WriteDB = False

            End Try
            cmdtxt = "INSERT into invrestricted SET Control_number = '" + Control_number.ToString + "'"
            If Load_Number <> Nothing Then cmdtxt = cmdtxt + "Load_Number = '" + cleanup(Load_Number) + "'"
            If Invoice_Number <> Nothing Then cmdtxt = cmdtxt + ", Invoice_Number = '" + cleanup(Invoice_Number) + "'"
            If Invoice_Date <> Nothing Then cmdtxt = cmdtxt + ", Invoice_Date = '" + Format(Invoice_Date, "yyyy-MM-dd") + "'"
            If Boat_US_cost <> Nothing Then cmdtxt = cmdtxt + ", Boat_US_cost = '" + Boat_US_cost.ToString + "'"
            If TRL_US_cost <> Nothing Then cmdtxt = cmdtxt + ", TRL_US_cost = '" + TRL_US_cost.ToString + "'"
            If FRT_US_cost <> Nothing Then cmdtxt = cmdtxt + ", FRT_US_cost = '" + FRT_US_cost.ToString + "'"
            If TOTAL_US_cost <> Nothing Then cmdtxt = cmdtxt + ", TOTAL_US_cost = '" + TOTAL_US_cost.ToString + "'"
            If GE_X_Rate <> Nothing Then cmdtxt = cmdtxt + ", GE_X_Rate = '" + GE_X_Rate.ToString + "'"
            If EST_Can_Cost <> Nothing Then cmdtxt = cmdtxt + ", EST_Can_Cost = '" + EST_Can_Cost.ToString + "'"
            If Canadian_cost <> Nothing Then cmdtxt = cmdtxt + ", Canadian_cost_Inv = '" + Canadian_cost.ToString + "'"
            If Inv_Move_to_Cost_of_Goods_sold <> Nothing Then cmdtxt = cmdtxt + ", Inv_Move_to_Cost_of_Goods_sold = '" + Inv_Move_to_Cost_of_Goods_sold.ToString + "'"
            If Remaining_CDN_cost_inv <> Nothing Then cmdtxt = cmdtxt + ", Remaining_CDN_cost_inv = '" + Remaining_CDN_cost_inv.ToString + "'"
            If GE_Ammount_Financed <> Nothing Then cmdtxt = cmdtxt + ", GE_Ammount_Financed = '" + GE_Ammount_Financed.ToString + "'"
            If PLA_cost_or_Cheque <> Nothing Then cmdtxt = cmdtxt + ", PLA_cost_or_Cheque = '" + cleanup(PLA_cost_or_Cheque) + "'"
            If GE_Amt_Paid <> Nothing Then cmdtxt = cmdtxt + ", GE_Amt_Paid = '" + GE_Amt_Paid.ToString + "'"
            If GE_amt_Owing <> Nothing Then cmdtxt = cmdtxt + ", GE_amt_Owing = '" + GE_amt_Owing.ToString + "'"
            If GE_paid_date <> Nothing Then cmdtxt = cmdtxt + ", GE_paid_date = '" + Format(GE_paid_date, "yyyy-MM-dd") + "'"
            If Free_lnterest_Until <> Nothing Then cmdtxt = cmdtxt + ", Free_lnterest_Until = '" + Format(Free_lnterest_Until, "yyyy-MM-dd") + "'"
            If Maturity_Date <> Nothing Then cmdtxt = cmdtxt + ", Maturity_Date = '" + Format(Maturity_Date, "yyyy-MM-dd") + "'"
            If Financed_Company <> Nothing Then cmdtxt = cmdtxt + ", Financed_Company = '" + cleanup(Financed_Company) + "'"
            If Rebate <> Nothing Then cmdtxt = cmdtxt + ", Rebate = '" + Rebate.ToString + "'"
            If Boat_Reg_date <> Nothing Then cmdtxt = cmdtxt + ", Boat_Reg_date = '" + Format(Boat_Reg_date, "yyyy-MM-dd") + "'"
            If Motor_Reg_Date <> Nothing Then cmdtxt = cmdtxt + ", Motor_Reg_Date = '" + Format(Motor_Reg_Date, "yyyy-MM-dd") + "'"
            If Accounting_Notes <> Nothing Then cmdtxt = cmdtxt + ", Accounting_Notes = '" + cleanup(Accounting_Notes) + "'"
            myCommand.CommandText = cmdtxt
            Try
                myCommand.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show("ERROR: " + ex.Message)
                WriteDB = False

            End Try

            WriteDB = True

        Catch ex As Exception
            MessageBox.Show("ERROR: " + ex.Message)
            WriteDB = False

        End Try

        conn.Close()
        conn.Dispose()

    End Function
    Public Function updateDB() As Boolean

        Dim conn As New MySqlConnection
        Dim myCommand As New MySqlCommand
        Dim cmdtxt As String
        conn.ConnectionString = persistant.myconnstring
        conn.Open()

        myCommand.Connection = conn
        cmdtxt = "update inventory SET "
        If Location <> Nothing Then cmdtxt = cmdtxt + "Location = '" + cleanup(Location) + "'"
        If Boat_year <> Nothing Then cmdtxt = cmdtxt + ", Boat_year = '" + cleanup(Boat_year.ToString) + "'"
        If Boat_brand <> Nothing Then cmdtxt = cmdtxt + ", Boat_brand = '" + cleanup(Boat_brand) + "'"
        If Boat_model <> Nothing Then cmdtxt = cmdtxt + ", Boat_model = '" + cleanup(Boat_model) + "'"
        If Boat_serial <> Nothing Then cmdtxt = cmdtxt + ", Boat_serial = '" + cleanup(Boat_serial) + "'"
        If Boat_hin <> Nothing Then cmdtxt = cmdtxt + ", Boat_hin = '" + cleanup(Boat_hin) + "'"
        If Boat_color <> Nothing Then cmdtxt = cmdtxt + ", Boat_color = '" + cleanup(Boat_color) + "'"
        If Equipment <> Nothing Then cmdtxt = cmdtxt + ", Equipment = '" + cleanup(Equipment) + "'"
        If Engine_make <> Nothing Then cmdtxt = cmdtxt + ", Engine_make = '" + cleanup(Engine_make) + "'"
        If engine_model <> Nothing Then cmdtxt = cmdtxt + ", engine_model = '" + cleanup(engine_model) + "'"
        If engine_serial <> Nothing Then cmdtxt = cmdtxt + ", engine_serial = '" + cleanup(engine_serial) + "'"
        If engine_year <> Nothing Then cmdtxt = cmdtxt + ", engine_year = '" + cleanup(engine_year.ToString) + "'"
        If drive_model <> Nothing Then cmdtxt = cmdtxt + ", drive_model = '" + cleanup(drive_model) + "'"
        If drive_serial <> Nothing Then cmdtxt = cmdtxt + ", drive_serial = '" + cleanup(drive_serial) + "'"
        If Tplate_serial <> Nothing Then cmdtxt = cmdtxt + ", Tplate_serial = '" + cleanup(Tplate_serial) + "'"
        If Trailer_make <> Nothing Then cmdtxt = cmdtxt + ", Trailer_make = '" + cleanup(Trailer_make) + "'"
        If trailer_model <> Nothing Then cmdtxt = cmdtxt + ", trailer_model = '" + cleanup(trailer_model) + "'"
        If trailer_serial <> Nothing Then cmdtxt = cmdtxt + ", trailer_serial = '" + cleanup(trailer_serial) + "'"
        If trailer_year <> Nothing Then cmdtxt = cmdtxt + ", trailer_year = '" + cleanup(trailer_year.ToString) + "'"
        If trailer_color <> Nothing Then cmdtxt = cmdtxt + ", trailer_color = '" + cleanup(trailer_color) + "'"
        If bos_num <> Nothing Then cmdtxt = cmdtxt + ", BOS_number = '" + cleanup(bos_num) + "'"
        If bos_num = Nothing Then cmdtxt = cmdtxt + ", BOS_number = Null"
        If bos_store <> Nothing Then cmdtxt = cmdtxt + ", BOS_Store = '" + cleanup(bos_store) + "'"
        If bos_store = Nothing Then cmdtxt = cmdtxt + ", BOS_Store = Null"
        If price <> Nothing Then cmdtxt = cmdtxt + ", price = '" + cleanup(price.ToString) + "'"
        If discount <> Nothing Then cmdtxt = cmdtxt + ", discount = '" + cleanup(discount.ToString) + "'"
        If comments <> Nothing Then cmdtxt = cmdtxt + ", comments = '" + cleanup(comments) + "'"
        If here = True Then cmdtxt = cmdtxt + ", here = 'YES'"
        If here = False Then cmdtxt = cmdtxt + ", here = 'NO'"
        If est_arrival_date <> CDate("1/1/0001 12:00:00 AM") Then cmdtxt = cmdtxt + ", est_arrival_date = '" + Format(est_arrival_date, "yyyy-MM-dd") + "'"
        If discount_reason <> Nothing Then cmdtxt = cmdtxt + ", discount_reason = '" + cleanup(discount_reason) + "'"
        cmdtxt = cmdtxt + ", state = '" + state.ToString + "'"
        cmdtxt = cmdtxt + " where control_number = " + Control_number.ToString


        myCommand.CommandText = cmdtxt
        Debug.WriteLine(cmdtxt)

        Try
            myCommand.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("ERROR: " + ex.Message)
            updateDB = False
        End Try


        If persistant.myuserLEVEL > 7 Then
            cmdtxt = "Update invrestricted SET"
            If Load_Number <> Nothing Then cmdtxt = cmdtxt + " Load_Number = '" + cleanup(Load_Number) + "',"
            If Invoice_Number <> Nothing Then cmdtxt = cmdtxt + " Invoice_Number = '" + cleanup(Invoice_Number) + "',"
            If Invoice_Date <> CDate("1/1/0001 12:00:00 AM") Then cmdtxt = cmdtxt + ", Invoice_Date = '" + Format(Invoice_Date, "yyyy-MM-dd") + "',"
            If Boat_US_cost <> Nothing Then cmdtxt = cmdtxt + " Boat_US_cost = '" + Boat_US_cost.ToString + "',"
            If TRL_US_cost <> Nothing Then cmdtxt = cmdtxt + " TRL_US_cost = '" + TRL_US_cost.ToString + "',"
            If FRT_US_cost <> Nothing Then cmdtxt = cmdtxt + " FRT_US_cost = '" + FRT_US_cost.ToString + "',"
            If TOTAL_US_cost <> Nothing Then cmdtxt = cmdtxt + " TOTAL_US_cost = '" + TOTAL_US_cost.ToString + "',"
            If GE_X_Rate <> Nothing Then cmdtxt = cmdtxt + " GE_X_Rate = '" + GE_X_Rate.ToString + "',"
            If EST_Can_Cost <> Nothing Then cmdtxt = cmdtxt + " EST_Can_Cost = '" + EST_Can_Cost.ToString + "',"
            If Canadian_cost <> Nothing Then cmdtxt = cmdtxt + " Canadian_cost_inv = '" + Canadian_cost.ToString + "',"
            If Inv_Move_to_Cost_of_Goods_sold <> Nothing Then cmdtxt = cmdtxt + " Inv_Move_to_Cost_of_Goods_sold = '" + Inv_Move_to_Cost_of_Goods_sold.ToString + "',"
            If Remaining_CDN_cost_inv <> Nothing Then cmdtxt = cmdtxt + " Remaining_CDN_cost_inv = '" + Remaining_CDN_cost_inv.ToString + "',"
            If GE_Ammount_Financed <> Nothing Then cmdtxt = cmdtxt + " GE_Ammount_Financed = '" + GE_Ammount_Financed.ToString + "',"
            If PLA_cost_or_Cheque <> Nothing Then cmdtxt = cmdtxt + " PLA_cost_or_Cheque = '" + cleanup(PLA_cost_or_Cheque) + "',"
            If GE_Amt_Paid <> Nothing Then cmdtxt = cmdtxt + " GE_Amt_Paid = '" + GE_Amt_Paid.ToString + "',"
            If GE_amt_Owing <> Nothing Then cmdtxt = cmdtxt + " GE_amt_Owing = '" + GE_amt_Owing.ToString + "',"
            If GE_paid_date <> CDate("1/1/0001 12:00:00 AM") Then cmdtxt = cmdtxt + " GE_paid_date = '" + Format(GE_paid_date, "yyyy-MM-dd") + "',"
            If Free_lnterest_Until <> CDate("1/1/0001 12:00:00 AM") Then cmdtxt = cmdtxt + " Free_lnterest_Until = '" + Format(Free_lnterest_Until, "yyyy-MM-dd") + "',"
            If Maturity_Date <> CDate("1/1/0001 12:00:00 AM") Then cmdtxt = cmdtxt + " Maturity_Date = '" + Format(Maturity_Date, "yyyy-MM-dd") + "',"
            If Financed_Company <> Nothing Then cmdtxt = cmdtxt + " Financed_Company = '" + cleanup(Financed_Company) + "',"
            If Rebate <> Nothing Then cmdtxt = cmdtxt + " Rebate = '" + Rebate.ToString + "',"
            If Boat_Reg_date <> CDate("1/1/0001 12:00:00 AM") Then cmdtxt = cmdtxt + " Boat_Reg_date = '" + Format(Boat_Reg_date, "yyyy-MM-dd") + "',"
            If Motor_Reg_Date <> CDate("1/1/0001 12:00:00 AM") Then cmdtxt = cmdtxt + " Motor_Reg_Date = '" + Format(Motor_Reg_Date, "yyyy-MM-dd") + "',"
            If Accounting_Notes <> Nothing Then cmdtxt = cmdtxt + " Accounting_Notes = '" + cleanup(Accounting_Notes) + "',"
            cmdtxt = Left(cmdtxt, cmdtxt.Length - 1)
            cmdtxt = cmdtxt + " WHERE Control_number = " + Control_number.ToString




            myCommand.CommandText = cmdtxt
            Try
                If cmdtxt <> "Update invrestricted SE" + " WHERE Control_number = " + Control_number.ToString Then
                    myCommand.ExecuteNonQuery()

                End If

                updateDB = True

            Catch ex As Exception
                MessageBox.Show("ERROR: " + ex.Message)
                updateDB = False

            End Try
        End If
        conn.Close()
        conn.Dispose()

    End Function
    Function cleanup(ByVal stringin As String) As String
        cleanup = Replace(stringin, "'", "\'")
    End Function
    Function getvalue(ByVal datawanted As String) As String
        getvalue = ""
        If datawanted.ToLower = "here".ToLower Then getvalue = here.ToString
        If datawanted.ToLower = "est_arrival_date".ToLower Then getvalue = est_arrival_date.ToShortDateString
        If datawanted.ToLower = "Invoice_Date".ToLower Then getvalue = Invoice_Date.ToShortDateString
        If datawanted.ToLower = "GE_paid_date".ToLower Then getvalue = GE_paid_date.ToShortDateString
        If datawanted.ToLower = "Free_lnterest_Until".ToLower Then getvalue = Free_lnterest_Until.ToShortDateString
        If datawanted.ToLower = "Maturity_Date".ToLower Then getvalue = Maturity_Date.ToShortDateString
        If datawanted.ToLower = "Boat_Reg_date".ToLower Then getvalue = Boat_Reg_date.ToShortDateString
        If datawanted.ToLower = "Motor_Reg_Date".ToLower Then getvalue = Motor_Reg_Date.ToShortDateString
        If datawanted.ToLower = "price".ToLower Then getvalue = price.ToString
        If datawanted.ToLower = "discount".ToLower Then getvalue = discount.ToString
        If datawanted.ToLower = "Boat_US_cost".ToLower Then getvalue = Boat_US_cost.ToString
        If datawanted.ToLower = "TRL_US_cost".ToLower Then getvalue = TRL_US_cost.ToString
        If datawanted.ToLower = "FRT_US_cost".ToLower Then getvalue = FRT_US_cost.ToString
        If datawanted.ToLower = "TOTAL_US_cost".ToLower Then getvalue = TOTAL_US_cost.ToString
        If datawanted.ToLower = "GE_X_Rate".ToLower Then getvalue = GE_X_Rate.ToString
        If datawanted.ToLower = "EST_Can_Cost".ToLower Then getvalue = EST_Can_Cost.ToString
        If datawanted.ToLower = "Canadian_cost".ToLower Then getvalue = Canadian_cost.ToString
        If datawanted.ToLower = "Inv_Move_to_Cost_of_Goods_sold".ToLower Then getvalue = Inv_Move_to_Cost_of_Goods_sold.ToString
        If datawanted.ToLower = "Remaining_CDN_cost_inv".ToLower Then getvalue = Remaining_CDN_cost_inv.ToString
        If datawanted.ToLower = "GE_Ammount_Financed".ToLower Then getvalue = GE_Ammount_Financed.ToString
        If datawanted.ToLower = "GE_Amt_Paid".ToLower Then getvalue = GE_Amt_Paid.ToString
        If datawanted.ToLower = "GE_amt_Owing".ToLower Then getvalue = GE_amt_Owing.ToString
        If datawanted.ToLower = "Rebate".ToLower Then getvalue = Rebate.ToString
        If datawanted.ToLower = "Boat_year".ToLower Then getvalue = Boat_year.ToString
        If datawanted.ToLower = "engine_year".ToLower Then getvalue = engine_year.ToString
        If datawanted.ToLower = "trailer_year".ToLower Then getvalue = trailer_year.ToString
        If datawanted.ToLower = "state".ToLower Then getvalue = state.ToString
        If datawanted.ToLower = "bos_num".ToLower Then getvalue = bos_num.ToString
        If datawanted.ToLower = "Location".ToLower Then getvalue = Location
        If datawanted.ToLower = "Boat_brand".ToLower Then getvalue = Boat_brand
        If datawanted.ToLower = "Boat_model".ToLower Then getvalue = Boat_model
        If datawanted.ToLower = "Boat_serial".ToLower Then getvalue = Boat_serial
        If datawanted.ToLower = "Boat_hin".ToLower Then getvalue = Boat_hin
        If datawanted.ToLower = "Boat_color".ToLower Then getvalue = Boat_color
        If datawanted.ToLower = "Equipment".ToLower Then getvalue = Equipment
        If datawanted.ToLower = "Engine_make".ToLower Then getvalue = Engine_make
        If datawanted.ToLower = "engine_model".ToLower Then getvalue = engine_model
        If datawanted.ToLower = "engine_serial".ToLower Then getvalue = engine_serial
        If datawanted.ToLower = "drive_model".ToLower Then getvalue = drive_model
        If datawanted.ToLower = "drive_serial".ToLower Then getvalue = drive_serial
        If datawanted.ToLower = "Tplate_serial".ToLower Then getvalue = Tplate_serial
        If datawanted.ToLower = "Trailer_make".ToLower Then getvalue = Trailer_make
        If datawanted.ToLower = "trailer_model".ToLower Then getvalue = trailer_model
        If datawanted.ToLower = "trailer_serial".ToLower Then getvalue = trailer_serial
        If datawanted.ToLower = "trailer_color".ToLower Then getvalue = trailer_color
        If datawanted.ToLower = "comments".ToLower Then getvalue = comments
        If datawanted.ToLower = "discount_reason".ToLower Then getvalue = discount_reason
        If datawanted.ToLower = "bos_store".ToLower Then getvalue = bos_store
        If datawanted.ToLower = "Load_Number".ToLower Then getvalue = Load_Number
        If datawanted.ToLower = "Invoice_Number".ToLower Then getvalue = Invoice_Number
        If datawanted.ToLower = "PLA_cost_or_Cheque".ToLower Then getvalue = PLA_cost_or_Cheque
        If datawanted.ToLower = "Financed_Company".ToLower Then getvalue = Financed_Company
        If datawanted.ToLower = "Accounting_Notes".ToLower Then getvalue = Accounting_Notes
    End Function
End Class

Public Class orderinfo
    Public boatmake, boatmodel, boatyear, motormake, motormodel, motoryear, equipment, notes, status, colour As String
    Public price As Decimal
    Public bos As Integer
    Public customername, store, salesman As String
End Class