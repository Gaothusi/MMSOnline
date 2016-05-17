Imports MySql.Data.MySqlClient
Imports System.Data
Imports MMSOnline.Functions
Imports System.IO
Imports iTextSharp.text.pdf
Imports iTextSharp.text

Public Class frmBOS
    Public persistant As PreFetch
    Public mybosstorecode, Prov As String
    Public mybosnumber As Integer
    Private mybosstore, mybosparent As String
    Private myboat, oldboat, ordernumber As Integer
    Private dealstatus, fandistatus, shopstatus, localareacode, usedquoteboat As Integer
    Private lockedmode, beeninserted, loaded, cansave As Boolean
    Private bosdata As New BOSdata
    Private invdata As New inventorydata
    Private FandI As New financingdata
    Private order As New frmOrderBoat
    Public Const mydatacount As Integer = 149
    Public mydata(mydatacount) As element
    Private Const mydatacount2 As Integer = 18
    Private mydata2(mydatacount2) As element

    Private Sub frmBOS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loaded = False
        cansave = False

        If Not Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\BOS") Then Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\BOS")

        Prov = persistant.getvalue(persistant.tbl_location, "Prov", "Code = '" + mybosstorecode + "'", 0)
        persistant.openBOSs.Add(Me)

        builtlists()
        If mybosnumber = Nothing Then
            setupnewbos()
        Else
            mybosstore = persistant.getvalue(persistant.tbl_location, "Store", "Code = '" + mybosstorecode + "'", 0)
            beeninserted = True
            If Val(bosdata.getvalue("locked")) = 0 Then
                Me.Text = mydata(2).value + " - " + mydata(81).value + " " + mydata(82).value
            Else
                Me.Text = mydata(2).value + " - " + mydata(81).value + " " + mydata(82).value + "         LOCKED: " + bosdata.getvalue("lockedby") + " is editing this bill of sale."
            End If
        End If
        logo()

        mybosparent = persistant.getvalue(persistant.tbl_location, "Prnt", "Store = '" + mybosstore + "'", 0)
        WritetoScreen()
        permisions()
        cmbcsfuel.SelectedItem = "Gas"
        cmbcsdeduct.SelectedItem = "$100"
        IAPproduct.SelectedItem = "Inboard/Sterndrive/Jet"
        IAPfuel.SelectedItem = "Gas"
        IAPdeduct.SelectedItem = "$100"
        IAPoemWarranty.Text = 2
        IAPextWarranty.Text = 6

        If Me.cmbDealStatus.SelectedItem <> "Quote" Then
            'set the required fields visible
            pnReq1.Visible = True
            pnReq2.Visible = True
        End If

        loaded = True

    End Sub

    Private Sub frmBOS_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        bosdata.bos = mybosnumber
        bosdata.store = mybosstore

        If beeninserted Then
            If lockedmode = False Then
                save()
                bosdata.locked(False, persistant.myuserID)
            End If
        End If
        'mydata2 = Buyer1_last
        If beeninserted = False And Replace(mydata(2).txtbox.text, " ", "") <> "" Then
            save()
            bosdata.locked(False, persistant.myuserID)
        End If

        'if the buyers last name is blank and the deal status is a quote then delete the record.
        If Replace(mydata(2).txtbox.text, " ", "") = "" And Me.cmbDealStatus.SelectedItem = "Quote" Then
            If beeninserted = True Then
                Dim conn As New MySqlConnection
                Dim cmd As New MySqlCommand
                conn.ConnectionString = persistant.myconnstring
                Try
                    conn.Open()
                    cmd.Connection = conn
                    cmd.CommandText = "delete from bos where bos_number = '" + mybosnumber.ToString + "' and Store = '" + mybosstorecode + "'"
                    cmd.ExecuteNonQuery()
                    conn.Close()
                Catch ex As MySqlException
                    MessageBox.Show(ex.Message)
                End Try
                conn.Dispose()
            End If
        End If

        'If not a quote and last name is not blank then update the notes1 field.
        If lockedmode = True And Replace(mydata(2).txtbox.text, " ", "") <> "" And Me.cmbDealStatus.SelectedItem <> "Quote" Then
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            Try
                conn.Open()
                cmd.Connection = conn
                Dim xstr As String
                xstr = Replace(Me.txtnotes.Text.ToString, "'", "\'")
                xstr = Replace(xstr, ";", "\;")
                cmd.CommandText = "UPDATE bos set notes1 = '" + xstr + "' where bos_number = '" + mybosnumber.ToString + "' and Store = '" + mybosstorecode + "'"
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()
        End If
        persistant.openBOSs.Remove(Me)
    End Sub


    'Private Sub updaterates()
    '    '   Me.cmbrate.Items.Clear()
    '    '  If Val(persistant.getvalue(persistant.tbl_banks, "R1", "Bank = '" + Me.ListBanks.SelectedItem + "'", 0)) <> 0 Then Me.cmbrate.Items.Add(Val(persistant.getvalue(persistant.tbl_banks, "R1", "Bank = '" + Me.ListBanks.SelectedItem + "'", 0)))
    '    ' If Val(persistant.getvalue(persistant.tbl_banks, "R2", "Bank = '" + Me.ListBanks.SelectedItem + "'", 0)) <> 0 Then Me.cmbrate.Items.Add(Val(persistant.getvalue(persistant.tbl_banks, "R2", "Bank = '" + Me.ListBanks.SelectedItem + "'", 0)))
    '    'If Val(persistant.getvalue(persistant.tbl_banks, "R3", "Bank = '" + Me.ListBanks.SelectedItem + "'", 0)) <> 0 Then Me.cmbrate.Items.Add(Val(persistant.getvalue(persistant.tbl_banks, "R3", "Bank = '" + Me.ListBanks.SelectedItem + "'", 0)))
    '    'If Val(persistant.getvalue(persistant.tbl_banks, "R4", "Bank = '" + Me.ListBanks.SelectedItem + "'", 0)) <> 0 Then Me.cmbrate.Items.Add(Val(persistant.getvalue(persistant.tbl_banks, "R4", "Bank = '" + Me.ListBanks.SelectedItem + "'", 0)))
    '    'If Val(persistant.getvalue(persistant.tbl_banks, "R5", "Bank = '" + Me.ListBanks.SelectedItem + "'", 0)) <> 0 Then Me.cmbrate.Items.Add(Val(persistant.getvalue(persistant.tbl_banks, "R5", "Bank = '" + Me.ListBanks.SelectedItem + "'", 0)))
    '    'FandI.fee = Val(persistant.getvalue(persistant.tbl_banks, "Fee", "Bank = '" + Me.ListBanks.SelectedItem + "'", 0))
    '    'FandI.bankfee = Val(persistant.getvalue(persistant.tbl_banks, "BankFee", "Bank = '" + Me.ListBanks.SelectedItem + "'", 0))
    'End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.cmbDealStatus.SelectedItem <> "Quote" Then save()
    End Sub

    Private Sub printSALOld()
        'Check if this store has SAL certs enabled
        If Val(persistant.getvalue(persistant.tbl_Certsframe, "Active", "Product = 'SAL' AND Store = '" + mybosstorecode + "'", 0)) <> 3 Then
            'If Yes

            Dim SALcert, digits, recordexists As New Integer
            Dim SALCertstr, SALprefix, formatstr As String
            digits = Val(persistant.getvalue(persistant.tbl_Certsframe, "Digits", "Product = 'SAL' AND Store = '" + mybosstorecode + "'", 0))
            SALprefix = persistant.getvalue(persistant.tbl_Certsframe, "Prefix", "Product = 'SAL' AND Store = '" + mybosstorecode + "'", 0)
            'Check if this bos has a cert #
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            Try
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = "Select SALcert from certsused where Store = '" + mybosstorecode + "' AND BOS = '" + mybosnumber.ToString + "'"
                'If this BOS already has a cert # use that
                Try
                    SALcert = Val(cmd.ExecuteScalar)
                Catch ex As Exception
                    SALcert = 0
                End Try

                If SALcert = 0 Then
                    'IF not use the next one for store
                    cmd.CommandText = "Select MAX(SALcert) from certsused where Store = '" + mybosstorecode + "'"
                    SALcert = Val(cmd.ExecuteScalar.ToString) + 1
                    'IF none ever used for store use the start number
                    If SALcert = 0 Then
                        SALcert = Val(persistant.getvalue(persistant.tbl_Certsframe, "Start", "Product = 'SAL' AND Store = '" + mybosstorecode + "'", 0))
                    End If
                    'Write the number we took for this BOS to the records
                    cmd.ExecuteNonQuery()

                    'Write the number we took for this BOS to the records
                    cmd.CommandText = "Select BOS from certsused where Store = '" + mybosstorecode + "' AND BOS = '" + mybosnumber.ToString + "'"
                    recordexists = Val(cmd.ExecuteScalar)
                    If recordexists = 0 Then
                        cmd.CommandText = "Insert into certsused set SALcert = '" + SALcert.ToString + "', Store = '" + mybosstorecode + "', BOS = '" + mybosnumber.ToString + "'"
                    Else
                        cmd.CommandText = "Update certsused set SALcert = '" + SALcert.ToString + "' where Store = '" + mybosstorecode + "' and BOS = '" + mybosnumber.ToString + "'"
                    End If
                    cmd.ExecuteNonQuery()

                End If

                cmd.Dispose()
                conn.Close()
            Catch ex As MySqlException

                MessageBox.Show("Error: " + ex.Message)
            End Try
            conn.Dispose()
            formatstr = ""

            For x As Integer = 1 To digits
                formatstr = formatstr + "0"
            Next
            SALCertstr = SALprefix + Format(SALcert, formatstr)

            '### DO PRINT
            save()

            Dim equip As New equipment
            equip.persistant = persistant
            Dim posx, posy, align As Integer
            Dim text, txtboxname, datacode As String
            Dim size As Integer
            Dim inputfile, savelocation As String

            Select Case persistant.getvalue(persistant.tbl_location, "Prov", "Code = '" + mybosstorecode + "'", 0)
                Case "AB"
                    datacode = "SALAB"
                    inputfile = persistant.installdir + "\resources\Contracts\SALAB.pdf"
                Case "BC"
                    datacode = "SALBC"
                    inputfile = persistant.installdir + "\resources\Contracts\SALBC.pdf"
                Case Else
                    datacode = "SALAB"
                    inputfile = persistant.installdir + "\resources\Contracts\SALAB.pdf"

            End Select

            savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-SAL.pdf"

            Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            '   Try
            Dim reader As PdfReader = New PdfReader(inputfile)
            Dim doc As Document = New Document(reader.GetPageSize(1))
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
            doc.Open()
            Dim cb As PdfContentByte = writer.DirectContent


            doc.NewPage()
            Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)

            cb.AddTemplate(Import, 0, 0)

            text = txtbuyer1last.Text
            If txtbuyer1first.Text <> "" Then
                text = text + ", " + txtbuyer1first.Text
            End If
            txtboxname = "txtbuyer1last"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 10
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()

            txtboxname = "effectivedateins"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Date.Today.ToShortDateString, posx, posy, 0)
            cb.EndText()

            'Personal Data
            For x As Integer = 6 To 10
                txtboxname = mydata(x).txtbox.name
                If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                    cb.EndText()
                End If
            Next

            text = ""
            If txtbuyer2last.Text <> "" Then
                text = text + txtbuyer2last.Text
            End If

            If txtbuyer2first.Text <> "" Then
                text = text + ", " + txtbuyer2first.Text
            End If
            If text <> "" Then
                txtboxname = "txtbuyer2last"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))

                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, text, posx, posy, 0)
                cb.EndText()

                For x As Integer = 6 To 10
                    txtboxname = mydata(x).txtbox.name + "2"
                    If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                        cb.BeginText()
                        cb.SetFontAndSize(bf, size)
                        cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                        cb.EndText()
                    End If
                Next

            End If

            txtboxname = "txtbday1"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtbday1.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "txtbday2"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtbday2.Text, posx, posy, 0)
            cb.EndText()


            txtboxname = "certificatenumber"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, SALCertstr, posx, posy, 0)
            cb.EndText()

            txtboxname = "financialinst"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))

            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.ListBanks.SelectedItem, posx, posy, 0)
            cb.EndText()

            txtboxname = "creditor"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, mybosstore, posx, posy, 0)
            cb.EndText()

            'Finance INfomation
            txtboxname = "financeamt"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(FandI.dollerstotal, "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "residual"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(FandI.residual, "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "interestrate"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, (FandI.Rate * 100).ToString + "%", posx, posy, 0)
            cb.EndText()
            txtboxname = "monthlypymt"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(FandI.pmt, "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "dealtype"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "Financed", posx, posy, 0)
            cb.EndText()
            txtboxname = "financeterm"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.cmbTerm.SelectedItem.ToString, posx, posy, 0)
            cb.EndText()

            txtboxname = "ammoritizationterm"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If Me.txtammover.Text <> "" Then
                cb.ShowTextAligned(align, Me.txtammover.Text, posx, posy, 0)
            Else
                cb.ShowTextAligned(align, Me.cmbammort.SelectedItem.ToString, posx, posy, 0)
            End If

            cb.EndText()

            'LIFE
            If Me.cmblins.SelectedItem <> "Declined" Then
                If Me.cmblins.SelectedItem = "Buyer" Then
                    txtboxname = "lifedebtor"
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, "x", posx, posy, 0)
                    cb.EndText()
                End If
                If Me.cmblins.SelectedItem = "Co-Buyer" Then
                    txtboxname = "lifecodebtor"
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, "x", posx, posy, 0)
                    cb.EndText()
                End If
                If Me.cmblins.SelectedItem = "Both" Then
                    txtboxname = "lifeboth"
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, "x", posx, posy, 0)
                    cb.EndText()
                End If
                txtboxname = "insfinanceamt"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(FandI.dollerstotal, "Currency"), posx, posy, 0)
                cb.EndText()
                txtboxname = "insmonthlyterm"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Me.cmbTerm.SelectedItem.ToString, posx, posy, 0)
                cb.EndText()
                txtboxname = "inslifepremium"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(FandI.lifec, "Currency"), posx, posy, 0)
                cb.EndText()
                txtboxname = "insresidualval"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(FandI.residual, "Currency"), posx, posy, 0)
                cb.EndText()
                txtboxname = "insresualvalterm"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Me.cmbTerm.SelectedItem.ToString, posx, posy, 0)
                cb.EndText()
                txtboxname = "insresidualpremuim"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(FandI.lifer, "Currency"), posx, posy, 0)
                cb.EndText()

            Else
                txtboxname = "lifedeclined"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, "x", posx, posy, 0)
                cb.EndText()
            End If

            'CI
            If Me.cmbcins.SelectedItem <> "Declined" Then
                If Me.cmbcins.SelectedItem = "Buyer" Then
                    txtboxname = "cidebtor"
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, "x", posx, posy, 0)
                    cb.EndText()
                End If
                If Me.cmbcins.SelectedItem = "Co-Buyer" Then
                    txtboxname = "cicodebtor"
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, "x", posx, posy, 0)
                    cb.EndText()
                End If
                If Me.cmbcins.SelectedItem = "Both" Then
                    txtboxname = "ciboth"
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, "x", posx, posy, 0)
                    cb.EndText()
                End If
                txtboxname = "cifinanceamt"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(FandI.dollerstotal, "Currency"), posx, posy, 0)
                cb.EndText()
                txtboxname = "cimonthlyterm"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Me.cmbTerm.SelectedItem.ToString, posx, posy, 0)
                cb.EndText()
                txtboxname = "cilifepremium"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(FandI.Criticalc, "Currency"), posx, posy, 0)
                cb.EndText()
                txtboxname = "ciresidualval"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(FandI.residual, "Currency"), posx, posy, 0)
                cb.EndText()
                txtboxname = "ciresidualvalterm"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Me.cmbTerm.SelectedItem.ToString, posx, posy, 0)
                cb.EndText()
                txtboxname = "ciresidualpremium"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(FandI.Criticalr, "Currency"), posx, posy, 0)
                cb.EndText()

            Else
                txtboxname = "cideclined"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, "x", posx, posy, 0)
                cb.EndText()
            End If

            'A AND H
            If Me.cmbains.SelectedItem <> "Declined" Then
                If Me.cmbains.SelectedItem = "Buyer" Then
                    txtboxname = "disdebtor"
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, "x", posx, posy, 0)
                    cb.EndText()
                End If
                If Me.cmbains.SelectedItem = "Co-Buyer" Then
                    txtboxname = "discodebtor"
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, "x", posx, posy, 0)
                    cb.EndText()
                End If
                If Me.cmbains.SelectedItem = "Both" Then
                    txtboxname = "disboth"
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, "x", posx, posy, 0)
                    cb.EndText()
                End If
                txtboxname = "dismonthlypayment"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(FandI.pmt, "Currency"), posx, posy, 0)
                cb.EndText()
                txtboxname = "disterm"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Me.cmbTerm.SelectedItem.ToString, posx, posy, 0)
                cb.EndText()
                txtboxname = "dispremium"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(FandI.AandH, "Currency"), posx, posy, 0)
                cb.EndText()

                Dim days, type As String
                days = ""
                type = ""

                If Me.cmbahtype.SelectedItem = "7 Day Retro" Then
                    days = "7"
                    type = "Retroactive"
                End If
                If Me.cmbahtype.SelectedItem = "14 Day Retro" Then
                    days = "14"
                    type = "Retroactive"
                End If
                If Me.cmbahtype.SelectedItem = "30 Day Retro" Then
                    days = "30"
                    type = "Retroactive"
                End If
                If Me.cmbahtype.SelectedItem = "30 Day Elim" Then
                    days = "30"
                    type = "Elimination"
                End If
                txtboxname = "diswaitingperiod"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, days, posx, posy, 0)
                cb.EndText()
                txtboxname = "distypewaitperiod"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, type, posx, posy, 0)
                cb.EndText()


            Else
                txtboxname = "disdeclined"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, "x", posx, posy, 0)
                cb.EndText()
            End If
            Dim instotal As Decimal
            instotal = FandI.AandH + FandI.Criticalc + FandI.Criticalr + FandI.lifec + FandI.lifer
            txtboxname = "totalpremium"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(instotal, "Currency"), posx, posy, 0)
            cb.EndText()

            txtboxname = "totalcost"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(instotal, "Currency"), posx, posy, 0)
            cb.EndText()

            doc.NewPage()
            Import = writer.GetImportedPage(reader, 2)
            cb.AddTemplate(Import, 0, 0)

            txtboxname = "certpg2"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, SALCertstr, posx, posy, 0)
            cb.EndText()



            doc.Close()
            System.Diagnostics.Process.Start(savelocation)
            If Me.cmbDealStatus.SelectedItem = "Pending" Or Me.cmbDealStatus.SelectedItem = "Conditions Removed" Or Me.cmbDealStatus.SelectedItem = "Quote" Then
                Me.txtinssold.Text = Format(instotal, "currency")
            End If



        Else
            'If SAL certs not active then quit and tell user
            MessageBox.Show("This feature is not yet available for your store")
        End If

    End Sub



#Region "Side Menu Buttons"


    Private Sub btnDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetails.Click
        Dim temp As Boolean
        temp = lockedmode

        If myboat > 2 Then
            Dim invedit1 As New frmeditinv
            invedit1.persistant = persistant
            If lockedmode = False Then
                invedit1.unlockserials = True
            End If
            invedit1.controlnumber = myboat
            invedit1.ShowDialog()

        End If

        If ordernumber > 0 Then
            order.persistant = persistant
            order.vieworder(ordernumber)
            order.ShowDialog()
        End If
        Me.downloaddata()
        lockedmode = temp
        Me.WritetoScreen()
    End Sub

    Private Sub btnPrintBOS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintBOS.Click
        save()

        Dim equip As New equipment
        equip.persistant = persistant
        Dim posx, posy, align As Integer
        Dim text, txtboxname As String
        Dim size As Integer
        Dim inputfile, savelocation As String

        inputfile = persistant.installdir + "\resources\bosblanks\BOS2012.pdf"

        'inputfile = ""
        'Select Case mybosstorecode
        '    Case "ATL"
        '        inputfile = persistant.installdir + "\resources\bosblanks\ATL.pdf"
        '    Case "BTH"
        '        inputfile = persistant.installdir + "\resources\bosblanks\BTH.pdf"
        '    Case "GAL"
        '        inputfile = persistant.installdir + "\resources\bosblanks\GAL.pdf"
        '    Case "OGO"
        '        inputfile = persistant.installdir + "\resources\bosblanks\OGO.pdf"
        '    Case "REN"
        '        inputfile = persistant.installdir + "\resources\bosblanks\REN.pdf"
        '    Case "EDM"
        '        inputfile = persistant.installdir + "\resources\bosblanks\EDM.pdf"
        '    Case "SYL"
        '        inputfile = persistant.installdir + "\resources\bosblanks\SYL.pdf"
        '    Case "KEL"
        '        inputfile = persistant.installdir + "\resources\bosblanks\KEL.pdf"
        'End Select

        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + ".pdf"

        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        '   Try
        Dim reader As PdfReader = New PdfReader(inputfile)
        Dim doc As Document = New Document(reader.GetPageSize(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
        doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent
        doc.NewPage()
        Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)
        cb.AddTemplate(Import, 0, 0)

        Dim logo As String = persistant.installdir & "\resources\images\" & mybosstorecode & "Logo2.png"
        Dim addr As String = persistant.installdir & "\resources\images\" & mybosstorecode & "Addr.png"
        ' add the store logo and address
        Dim im As Image
        im = Image.GetInstance(logo)
        im.ScalePercent(50)
        'im.WidthPercentage = 150
        im.SetAbsolutePosition(100, 730)
        doc.Add(im)
        im = Image.GetInstance(addr)
        im.ScalePercent(50)
        im.SetAbsolutePosition(0, 730)
        doc.Add(im)


        'align  (0 = Left, 1 = Center, 2 = Right)

        'txtboxname = "buyersname"
        posx = 23
        posy = 676
        align = 0
        size = 12
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        text = txtbuyer1last.Text & IIf(txtbuyer1first.Text <> "", ",", "")
        cb.ShowTextAligned(align, text, posx, posy, 0)
        text = txtbuyer1first.Text
        cb.ShowTextAligned(align, text, 130, posy, 0)
        text = txtbuyer2last.Text & IIf(txtbuyer2first.Text <> "", ",", "")
        cb.ShowTextAligned(align, text, 330, posy, 0)
        text = txtbuyer2first.Text
        cb.ShowTextAligned(align, text, 437, posy, 0)
        cb.EndText()


        If Me.cmbDealStatus.SelectedItem = "Void" Or Me.cmbDealStatus.SelectedItem = "Void Pending" Then
            cb.BeginText()
            cb.SetFontAndSize(bf, 120)
            cb.ShowTextAligned(1, "VOID", 315, 365, 0)
            cb.EndText()
        End If


        'Salesman
        'txtboxname = "salesman" , "bizman"
        size = 9
        align = 1
        posx = 80
        posy = 70
        text = persistant.getvalue(persistant.tbl_users, "name", "user = '" + txtsalesman.Text + "'", 0)
        If txtsalesman2.Text <> "" Then
            size = 7
            text = text + " / " + persistant.getvalue(persistant.tbl_users, "name", "user = '" + txtsalesman2.Text + "'", 0)
        End If
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, text, posx, posy, 0)
        text = ""
        If txtbizman.Text <> "" Then
            text = persistant.getvalue(persistant.tbl_users, "name", "user = '" + txtbizman.Text + "'", 0)
        End If
        cb.ShowTextAligned(align, text, 225, posy, 0)
        cb.EndText()

        size = 12
        If Me.txtdatedeled.Text <> "" Then
            cb.BeginText()
            cb.SetFontAndSize(bf, 12)
            cb.ShowTextAligned(2, Me.txtdatedeled.Text, 585, 715, 0)
            cb.EndText()
        End If

        'Personal Data
        For x As Integer = 0 To 12
            'If x >= 2 And x <= 5 Then Continue For
            txtboxname = mydata(x).txtbox.name
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOSX", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOSY", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                size = 12
                If x = 0 Then size = 16
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                cb.EndText()
            End If
        Next

        'Options and ski pkg Number
        For x As Integer = 13 To 21
            txtboxname = mydata(x).txtbox.name
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOSX", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOSY", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                size = 10
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                cb.EndText()

            End If
        Next

        'Options Pricing
        For x As Integer = 42 To 56
            If x > 41 And x < 49 Then
                If mydata(x - 12).value.ToString = "1" Or mydata(x - 12).value.ToString = "True" Then
                    'Only print checked options for sparetire -> anti theft pkg
                    txtboxname = mydata(x).txtbox.name
                    If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                        posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOSX", "Data = '" + txtboxname + "'", 0))
                        posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOSY", "Data = '" + txtboxname + "'", 0))
                        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                        size = 10
                        cb.BeginText()
                        cb.SetFontAndSize(bf, size)
                        cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                        cb.EndText()
                    End If
                End If
            Else
                'print all additional options
                txtboxname = mydata(x).txtbox.name
                If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOSX", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOSY", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    size = 10
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                    cb.EndText()
                End If
            End If
        Next
        'Option total #2
        'txtboxname = "optionstotal2"
        size = 10
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(2, mydata(69).txtbox.text, 335, 212, 0)
        cb.EndText()

        'Boat Data
        For x As Integer = 79 To 92
            'If x >= 93 And x <= 98 Then Continue For
            txtboxname = mydata(x).txtbox.name
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOSX", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOSY", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                size = 12

                If x = 80 Then
                    If txtcolor.Text.Length > 8 Then size = 10
                    If txtcolor.Text.Length > 12 Then size = 8
                    If txtcolor.Text.Length > 16 Then size = 6
                End If
                If x = 82 Or x = 86 Or x = 90 Then
                    If mydata(x).txtbox.Text.Length > 8 Then size = 10
                    If mydata(x).txtbox.Text.Length > 12 Then size = 8
                    If mydata(x).txtbox.Text.Length > 16 Then size = 6
                End If
                If x = 91 Then size = 10
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                cb.EndText()
            End If
        Next

        'TPlate ser #
        size = 12
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(0, mydata(99).txtbox.text, 515, 598, 0)
        cb.EndText()

        ''total
        ''txtboxname = mydata(75).txtbox.name
        'size = 14
        'cb.BeginText()
        'cb.SetFontAndSize(bf, size)
        'cb.ShowTextAligned(2, mydata(75).txtbox.text, 590, 205, 0)
        'cb.EndText()

        'TAX
        'txtboxname = mydata(74).txtbox.name
        Dim taxType As String = ""
        Dim taxNum As String = ""
        Dim tireTax As String = ""
        Dim tireTax2 As String = ""
        Dim pstNum As String = ""
        Dim isAB As Boolean = True
        Select Case mybosstorecode
            Case "EDM"
                taxType = "GST"
                taxNum = "#13518 6336 RT0001"
                tireTax = "ALBERTA TIRE LEVY"
                tireTax2 = "$4 PER TIRE"
            Case "SYL"
                taxType = "GST"
                taxNum = "#13805 0331 RT001"
                tireTax = "ALBERTA TIRE LEVY"
                tireTax2 = "$4 PER TIRE"
            Case "REN"
                taxType = "GST"
                taxNum = "#10447 5231 RT0001"
                tireTax = "ALBERTA TIRE LEVY"
                tireTax2 = "$4 PER TIRE"
            Case "BTH"
                isAB = False
                taxType = "GST"
                taxNum = "#10383 9999 RT0001"
                tireTax = "ENVIRONMENTAL LEVY"
                tireTax2 = "$5 PER TIRE, $5 PER BATTERY"
                pstNum = "Dealer No. 73057  PST# 1012-8397"
            Case "ATL"
                isAB = False
                taxType = "GST"
                taxNum = "#86307 1536 RT0001"
                tireTax = "ENVIRONMENTAL LEVY"
                tireTax2 = "$5 PER TIRE, $5 PER BATTERY"
                pstNum = "Dealer No. 76865  PST# 1012-5604"
            Case "KEL"
                isAB = False
                taxType = "GST"
                taxNum = "#86307 1536 RT0001"
                tireTax = "ENVIRONMENTAL LEVY"
                tireTax2 = "$5 PER TIRE, $5 PER BATTERY"
                pstNum = "Dealer No. 76865  PST# 1012-5604"
        End Select
        Dim bf2 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        If isAB Then
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf2, size)
            cb.ShowTextAligned(0, taxType, 365, 255, 0)
            cb.EndText()

            size = 8
            cb.BeginText()
            cb.SetFontAndSize(bf2, size)
            cb.ShowTextAligned(0, taxNum, 400, 252, 0)
            cb.EndText()

            'pst + gst values
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(2, Format((Val(mydata(73).value) + Val(mydata(74).value)), "currency"), 590, 255, 0)
            cb.EndText()
        Else
            'PST label
            size = 8
            cb.BeginText()
            cb.SetFontAndSize(bf2, size)
            cb.ShowTextAligned(0, "PST", 365, 262, 0)
            cb.EndText()

            'GST label
            cb.BeginText()
            cb.SetFontAndSize(bf2, size)
            cb.ShowTextAligned(0, taxType, 365, 252, 0)
            cb.EndText()

            'size = 8 gst number
            cb.BeginText()
            cb.SetFontAndSize(bf2, size)
            cb.ShowTextAligned(0, taxNum, 400, 252, 0)
            cb.EndText()

            'pst number
            cb.BeginText()
            cb.SetFontAndSize(bf2, size)
            cb.ShowTextAligned(2, pstNum, 490, 712, 0)
            cb.EndText()

            'pst + gst values
            'size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(2, Format(Val(mydata(73).value), "currency"), 590, 262, 0)
            cb.EndText()

            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(2, Format(Val(mydata(74).value), "currency"), 590, 252, 0)
            cb.EndText()

        End If

        'tire & battery levy
        size = 9
        cb.BeginText()
        cb.SetFontAndSize(bf2, size)
        cb.ShowTextAligned(0, tireTax, 364, 458, 0)
        cb.ShowTextAligned(0, tireTax2, 364, 448, 0)
        cb.EndText()



        'Right hand col (pricing)  All execpt Discount.  We have to deal with it seperatly

        'Admin fee and Finance fee
        size = 12
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(2, Format((Val(mydata(62).value) + Val(mydata(136).value)), "currency"), 590, 472, 0)
        cb.EndText()

        For x As Integer = 57 To 72
            If x = 60 Or x = 61 Or x = 62 Then Continue For
            If x < 64 Or x > 68 Then
                txtboxname = mydata(x).txtbox.name
                If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOSX", "Data = '" + mydata(x).txtbox.name + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOSY", "Data = '" + mydata(x).txtbox.name + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + mydata(x).txtbox.name + "'", 0))
                    size = 12
                    If x = 75 Then size = 14
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                    cb.EndText()
                End If
            Else
                If mydata(x - 27).value.ToString = "1" Or mydata(x - 27).value.ToString = "True" Then
                    txtboxname = mydata(x).txtbox.name
                    If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                        posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOSX", "Data = '" + mydata(x).txtbox.name + "'", 0))
                        posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOSY", "Data = '" + mydata(x).txtbox.name + "'", 0))
                        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + mydata(x).txtbox.name + "'", 0))
                        size = 12
                        If x = 75 Then size = 14
                        cb.BeginText()
                        cb.SetFontAndSize(bf, size)
                        cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                        cb.EndText()
                    End If
                End If
            End If
        Next

        'Checks -> YES / NO
        For x As Integer = 30 To 41
            txtboxname = mydata(x).txtbox.name
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "YesNo" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOSX", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOSY", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                size = 10
                If mydata(x).value.ToString = "1" Or mydata(x).value.ToString = "True" Then
                    text = "YES"
                Else
                    text = "NO"
                End If
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, text, posx, posy, 0)
                cb.EndText()
            End If
        Next
        Dim notes() As String
        notes = txtnotes1.Text.Split(vbNewLine.ToCharArray, StringSplitOptions.RemoveEmptyEntries)

        size = 6
        align = 0
        posx = 70
        If notes.Length > 0 Then
            'NOTES
            'txtboxname = "notes1"
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, notes(0), posx, 42, 0)
            cb.EndText()
        End If

        If notes.Length > 1 Then
            'txtboxname = "notes2"
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, notes(1), posx, 32, 0)
            cb.EndText()
        End If

        'payment info
        For x As Integer = 24 To 29
            txtboxname = mydata(x).txtbox.name
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOSX", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOSY", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                size = 10
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                cb.EndText()
            End If
        Next

        'deposit date and number
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(1, mydata(149).txtbox.text, 490, 128, 0)
        cb.EndText()
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(2, mydata(147).txtbox.text, 577, 128, 0)
        cb.EndText()


        'If txtowing.Text <> "$0.00" Then
        '    'txtboxname = "txtp4amount"
        '    size = 12
        '    cb.BeginText()
        '    cb.SetFontAndSize(bf, size)
        '    cb.ShowTextAligned(2, txtowing.Text, 444, 54, 0)
        '    cb.EndText()
        'End If

        For x As Integer = 76 To 78
            txtboxname = mydata(x).txtbox.name
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOSX", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOSY", "Data = '" + txtboxname + "'", 0))
                'align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                align = 2
                size = 10
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                cb.EndText()
            End If
        Next

        'deposit amount
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(2, mydata(148).txtbox.text, 435, 128, 0)
        cb.EndText()

        'Discount
        size = 12
        align = 2
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, mydata(105).txtbox.text, 490, 490, 0)
        cb.EndText()

        If mydata(61).txtbox.text <> "" Then

            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "(" + mydata(61).txtbox.text + ")", 594, 490, 0)
            cb.EndText()
        End If

        'Included features
        If Me.myboat = 2 Then
            'ordered boat
            If ordernumber > 0 Then
                order.persistant = persistant
                order.vieworder(ordernumber)
                Dim list As New List(Of String)
                Dim input As String
                input = order.equipment
                Dim result() As String
                result = input.Split(vbNewLine.ToCharArray, StringSplitOptions.RemoveEmptyEntries)
                list.Clear()
                list.AddRange(result)
                If list.Count = 1 Then
                    result = input.Split(",".ToCharArray, StringSplitOptions.RemoveEmptyEntries)
                    list.Clear()
                    list.AddRange(result)
                End If
                size = 8
                For x As Integer = 0 To list.Count - 1
                    If x < 10 Then
                        txtboxname = "equip" + (x + 1).ToString
                        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOSX", "Data = '" + txtboxname + "'", 0))
                            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOSY", "Data = '" + txtboxname + "'", 0))
                            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                            cb.BeginText()
                            cb.SetFontAndSize(bf, size)
                            cb.ShowTextAligned(align, list.Item(x).ToString, posx, posy, 0)
                            cb.EndText()
                        End If
                    End If
                Next
            End If

        Else
            If myboat > 2 Then
                'In Stock
                Dim list As New List(Of String)
                equip.short2long(invdata.getvalue("Equipment"), list)
                size = 8
                For x As Integer = 0 To list.Count - 1
                    If x < 10 Then
                        txtboxname = "equip" + (x + 1).ToString
                        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOSX", "Data = '" + txtboxname + "'", 0))
                            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOSY", "Data = '" + txtboxname + "'", 0))
                            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                            cb.BeginText()
                            cb.SetFontAndSize(bf, size)
                            cb.ShowTextAligned(align, list.Item(x).ToString, posx, posy, 0)
                            cb.EndText()
                        End If
                    End If
                Next
            End If

        End If

        cb.BeginText()
        cb.SetFontAndSize(bf, 10)
        posx = 159
        Dim isN As Boolean = IsNumeric(txttradelein.Text)
        Dim tradeLienVal As Double = 0
        If isN Then
            tradeLienVal = CDbl(txttradelein.Text)
        End If
        Dim nonTaxTotal As Double = FandI.bankfeeoverride + FandI.lifec + FandI.lifer + FandI.Criticalc + FandI.Criticalr + FandI.AandH + FandI.AccidentalPlus + tradeLienVal
        'bank Fee
        cb.ShowTextAligned(2, FormatCurrency(FandI.bankfeeoverride), posx, 313, 0)
        'life ins
        cb.ShowTextAligned(2, FormatCurrency(FandI.lifec + FandI.lifer), posx, 300, 0)
        'critical illness
        cb.ShowTextAligned(2, FormatCurrency(FandI.Criticalc + FandI.Criticalr), posx, 287, 0)
        'accidental & sick
        cb.ShowTextAligned(2, FormatCurrency(FandI.AandH), posx, 274, 0)
        'accidental plus
        cb.ShowTextAligned(2, FormatCurrency(FandI.AccidentalPlus), posx, 260, 0)
        'Unemployeement
        'cb.ShowTextAligned(2, "*", posx, 246, 0)
        'Trade Lien
        cb.ShowTextAligned(2, txttradelein.Text, posx, 232, 0)
        'total non taxable
        cb.ShowTextAligned(2, FormatCurrency(nonTaxTotal), posx, 215, 0)
        'missing values fill in
        'cb.ShowTextAligned(0, "* Cost not available", 60, 195, 0)
        cb.EndText()

        'total
        cb.BeginText()
        cb.SetFontAndSize(bf, 12)
        cb.ShowTextAligned(2, FormatCurrency(nonTaxTotal), 590, 235, 0)
        cb.SetFontAndSize(bf, 14)
        cb.ShowTextAligned(2, FormatCurrency(nonTaxTotal + mydata(75).txtbox.text - tradeLienVal), 590, 205, 0)
        cb.EndText()

        'Balance owing
        If txtowing.Text <> "$0.00" Then
            'txtboxname = "txtp4amount"
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(2, FormatCurrency(nonTaxTotal + txtowing.Text - tradeLienVal), 444, 54, 0)
            cb.EndText()
        End If

        If persistant.myuserID <> "Inventory" Then
            doc.NewPage()
            Import = writer.GetImportedPage(reader, 2)
            cb.AddTemplate(Import, 0, 0)
        End If

        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
        '  Catch ex As Exception
        'MessageBox.Show(ex.Message)
        ' MessageBox.Show("An error occured while trying to save your printable BOS.  Please make sure that you don't already have a copy of this deal open in Adobe Reader.")
        ' End Try


    End Sub

    Private Sub btnPrintCornerstone_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintCornerstone.Click
        Dim equip As New equipment
        equip.persistant = persistant
        Dim posx, posy, align As Integer
        Dim text, txtboxname, datacode As String
        Dim size As Integer
        Dim inputfile, savelocation As String
        'Check if this store has CS certs enabled
        If Val(persistant.getvalue(persistant.tbl_Certsframe, "Active", "Product = 'Cornerstone' AND Store = '" + mybosstorecode + "'", 0)) = 1 Then
            'If Yes
            Dim cssalesman As String
            cssalesman = bosdata.getvalue("Ext_warranty_SB")
            Dim CScert, digits, recordexists As New Integer
            Dim CScertstr, CSprefix, formatstr As String
            digits = Val(persistant.getvalue(persistant.tbl_Certsframe, "Digits", "Product = 'Cornerstone' AND Store = '" + mybosstorecode + "'", 0))
            CSprefix = persistant.getvalue(persistant.tbl_Certsframe, "Prefix", "Product = 'Cornerstone' AND Store = '" + mybosstorecode + "'", 0)
            'Check if this bos has a cert #
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            Try
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = "Select CSCert08 from certsused where Store = '" + mybosstorecode + "' AND BOS = '" + mybosnumber.ToString + "'"
                'If this BOS already has a cert # use that
                Try
                    CScert = Val(cmd.ExecuteScalar)

                Catch ex As Exception
                    CScert = 0
                End Try
                If CScert = 0 Then
                    'IF not use the next one for store
                    cmd.CommandText = "Select MAX(CSCert08) from certsused where Store = '" + mybosstorecode + "'"
                    CScert = Val(cmd.ExecuteScalar.ToString) + 1
                    'IF none ever used for store use the start number
                    If CScert = 0 Then
                        CScert = Val(persistant.getvalue(persistant.tbl_Certsframe, "Start", "Product = 'Cornerstone' AND Store = '" + mybosstorecode + "'", 0))
                    End If
                    'Write the number we took for this BOS to the records
                    cmd.CommandText = "Select BOS from certsused where Store = '" + mybosstorecode + "' AND BOS = '" + mybosnumber.ToString + "'"
                    recordexists = Val(cmd.ExecuteScalar)
                    If recordexists = 0 Then
                        cmd.CommandText = "Insert into certsused set CScert08 = '" + CScert.ToString + "', Store = '" + mybosstorecode + "', BOS = '" + mybosnumber.ToString + "'"
                    Else
                        cmd.CommandText = "Update certsused set CScert08 = '" + CScert.ToString + "' where Store = '" + mybosstorecode + "' and BOS = '" + mybosnumber.ToString + "'"
                    End If
                    cmd.ExecuteNonQuery()

                End If

                cmd.Dispose()
                conn.Close()
            Catch ex As MySqlException

                MessageBox.Show("Error: " + ex.Message)
            End Try
            conn.Dispose()
            formatstr = ""

            For x As Integer = 1 To digits
                formatstr = formatstr + "0"
            Next
            CScertstr = CSprefix + Format(CScert, formatstr)

            '### DO PRINT
            save()


            datacode = "CORN"

            inputfile = persistant.installdir + "\resources\Contracts\CS.pdf"
            savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "/", ""), "\", "") + "-CS.pdf"

            Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            '   Try
            Dim reader As PdfReader = New PdfReader(inputfile)
            Dim doc As Document = New Document(reader.GetPageSize(1))
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
            doc.Open()
            Dim cb As PdfContentByte = writer.DirectContent


            doc.NewPage()
            Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)

            cb.AddTemplate(Import, 0, 0)


            size = 11
            txtboxname = "cspolicynumber"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, CScertstr, posx, posy, 0)
            cb.EndText()



            'Last name, First Name & Last name, Firstname
            text = txtbuyer1last.Text
            If txtbuyer1first.Text <> "" Then
                text = text + ", " + txtbuyer1first.Text
            End If
            txtboxname = "csname"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 8
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()

            text = txtbuyer2last.Text
            If txtbuyer2first.Text <> "" Then
                text = text + ", " + txtbuyer2first.Text
            End If
            txtboxname = "csname2"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 8
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()

            txtboxname = "cstodaysdate"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If chkfakedate.Checked = True Then
                cb.ShowTextAligned(align, DateWarrantystart.Value.ToString("MM/dd/yyyy"), posx, posy, 0)
            Else
                cb.ShowTextAligned(align, Date.Today.ToString("MM/dd/yyyy"), posx, posy, 0)
            End If
            cb.EndText()

            txtboxname = "csdeldate"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, DateWarrantystart.Value.ToString("MM/dd/yyyy"), posx, posy, 0)
            cb.EndText()

            txtboxname = "csstartdate"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, DateWarrantystart.Value.ToString("MM/dd/yyyy"), posx, posy, 0)
            cb.EndText()


            'Personal Data
            For x As Integer = 6 To 10
                txtboxname = mydata(x).txtbox.name
                If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(0, mydata(x).txtbox.text, posx, posy, 0)
                    cb.EndText()
                End If
            Next

            'Finance INfomation
            txtboxname = "csdealername"
            text = ""
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, mybosstore, posx, posy, 0)
            cb.EndText()

            text = ""
            Select Case mybosstorecode
                Case "EDM"
                    text = "AB7590"
                Case "ATL"
                    text = "AB7601"
                Case "SYL"
                    text = "AB7602"
                Case "REN"
                    text = "AB7604"
                Case "BTH"
                    text = "BC7711"
                Case "GAL"
                    text = "BC7454"
                Case "OGO"
                    text = "BC7562"
                Case "KEL"
                    text = "BC7562"
            End Select

            txtboxname = "csdealernum"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()



            txtboxname = "cslienholder"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If Me.ListBanks.SelectedItem <> Nothing Then
                cb.ShowTextAligned(align, Me.ListBanks.SelectedItem, posx, posy, 0)
            Else
                cb.ShowTextAligned(align, "", posx, posy, 0)
            End If

            cb.EndText()

            txtboxname = "cssalesman"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, cssalesman, posx, posy, 0)
            cb.EndText()
            text = ""
            Select Case mybosstorecode
                Case "EDM"
                    text = "Edmonton"
                Case "ATL"
                    text = "Edmonton"
                Case "SYL"
                    text = "Sylvan Lake"
                Case "REN"
                    text = "Calgary"
                Case "BTH"
                    text = "Salmon Arm"
                Case "GAL"
                    text = "Langley"
                Case "OGO"
                    text = "Kelowna"
                Case "KEL"
                    text = "Kelowna"
            End Select

            txtboxname = "csdealercity"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()

            text = ""
            Select Case mybosstorecode
                Case "EDM"
                    text = "AB"
                Case "ATL"
                    text = "AB"
                Case "SYL"
                    text = "AB"
                Case "REN"
                    text = "AB"
                Case "BTH"
                    text = "BC"
                Case "GAL"
                    text = "BC"
                Case "OGO"
                    text = "BC"
                Case "KEL"
                    text = "BC"
            End Select

            txtboxname = "csdealerprovince"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()

            txtboxname = "csproductdescription"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_motors, "Type", "brand = '" + Me.txtmotormake.Text + "' AND model = '" + Me.txtmotormodel.Text + "'", 0), posx, posy, 0)
            cb.EndText()
            txtboxname = "csbrand"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtmotormake.Text, posx, posy, 0)
            cb.EndText()
            txtboxname = "csproductyear"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtmotoryear.Text, posx, posy, 0)
            cb.EndText()
            txtboxname = "csserialnumber"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtmotorserial.Text, posx, posy, 0)
            cb.EndText()


            txtboxname = "csmotormodel"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtmotormodel.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "cspurchaseprice"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txttotal.Text, posx, posy, 0)
            cb.EndText()




            txtboxname = "cshp/cc"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_motors, "HP", "brand = '" + Me.txtmotormake.Text + "' AND model = '" + Me.txtmotormodel.Text + "'", 0) + " hp", posx, posy, 0)
            cb.EndText()
            size = 10

            txtboxname = "csmanufacturerlength"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.CSmanwarr.Text, posx, posy, 0)
            cb.EndText()


            txtboxname = "cscontractlength"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.CSextwarr.Text, posx, posy, 0)
            cb.EndText()

            size = 8





            Dim taxpercent
            Dim tempx As Decimal
            Dim tstr As String
            tstr = Me.txtextwarranty.Text
            tstr = Replace(tstr, "$", "")
            tstr = Replace(tstr, ",", "")
            tempx = Val(tstr)
            taxpercent = 0

            If ChkGST.Checked = True Then
                taxpercent = taxpercent + GSTRate
            End If
            If chkPST.Checked = True Then
                taxpercent = taxpercent + PSTRate
            End If


            txtboxname = "cspolicyprice"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(tempx * 1, "N"), posx, posy, 0)
            cb.EndText()

            txtboxname = "cspolicygst"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(tempx * taxpercent, "N"), posx, posy, 0)
            cb.EndText()

            txtboxname = "cspolicytotal"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(tempx * (1 + taxpercent), "N"), posx, posy, 0)
            cb.EndText()

            size = 10

            If chkcsOpti.Checked = True Then
                txtboxname = "csoptimax"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, "x", posx, posy, 0)
                cb.EndText()
            End If

            If chkcstwinprop.Checked = True Then
                txtboxname = "cstwinprop"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, "x", posx, posy, 0)
                cb.EndText()
            End If

            If cmbcsfuel.SelectedItem.ToString.ToLower = "gas" Then
                txtboxname = "csgas"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, "x", posx, posy, 0)
                cb.EndText()

            Else
                txtboxname = "csdiesel"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, "x", posx, posy, 0)
                cb.EndText()
            End If

            txtboxname = "cssingle"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "x", posx, posy, 0)
            cb.EndText()

            If chkusedwarranty.Checked = True Then
                txtboxname = "csused"
            Else
                If txtboatmake.Text = "Used" Then
                    txtboxname = "csused"
                Else
                    txtboxname = "csnew"
                End If
            End If

            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "x", posx, posy, 0)
            cb.EndText()



            If chkusedwarranty.Checked = True Then
                txtboxname = "cs100dedused"
            Else
                If txtboatmake.Text = "Used" Then
                    txtboxname = "cs100dedused"
                Else
                    Select Case cmbcsdeduct.SelectedItem.ToString.ToLower
                        Case "$100"
                            txtboxname = "cs100deductible"
                        Case "$0"
                            txtboxname = "cs0deductible"
                        Case "$500 (diesel)"
                            txtboxname = "cs500deductible"
                    End Select
                End If
            End If
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "x", posx, posy, 0)
            cb.EndText()


            Select Case cmbcstype.SelectedItem.ToString.ToLower
                Case "sterndrive"
                    txtboxname = "cssterndrive"
                Case "outboard"
                    txtboxname = "csoutboard"
                Case "inboard"
                    txtboxname = "csinboard"
                Case "jet drive"
                    txtboxname = "csjet"
            End Select

            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "x", posx, posy, 0)
            cb.EndText()



            doc.NewPage()
            Import = writer.GetImportedPage(reader, 2)
            cb.AddTemplate(Import, 0, 0)
            doc.NewPage()
            Import = writer.GetImportedPage(reader, 3)
            cb.AddTemplate(Import, 0, 0)
            doc.NewPage()
            Import = writer.GetImportedPage(reader, 4)
            cb.AddTemplate(Import, 0, 0)
            doc.NewPage()
            Import = writer.GetImportedPage(reader, 5)
            cb.AddTemplate(Import, 0, 0)


            If Me.cmbGPR.SelectedItem.ToString.ToLower <> "none" Then
                inputfile = persistant.installdir + "\resources\Contracts\CSgpr.pdf"
                reader = New PdfReader(inputfile)

                doc.NewPage()
                Import = writer.GetImportedPage(reader, 1)
                cb.AddTemplate(Import, 0, 0)


                size = 12
                txtboxname = "csgprpolicynumber"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, CScertstr, posx, posy, 0)
                cb.EndText()
                size = 10

                txtboxname = "csgprtodaysdate"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Date.Today.ToString("MM/dd/yy"), posx, posy, 0)
                cb.EndText()
                size = 8

                'Last name, First Name & Last name, Firstname
                text = txtbuyer1last.Text
                If txtbuyer1first.Text <> "" Then
                    text = text + ", " + txtbuyer1first.Text
                End If
                If txtbuyer2last.Text <> "" Then
                    text = text + " & " + txtbuyer2last.Text
                End If

                If txtbuyer2first.Text <> "" Then
                    If txtbuyer2last.Text = "" Then
                        text = text + txtbuyer2first.Text
                    Else
                        text = text + ", " + txtbuyer2first.Text
                    End If
                End If

                txtboxname = "csgprname"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                size = 8
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, text, posx, posy, 0)
                cb.EndText()





                'Personal Data
                For x As Integer = 6 To 10
                    txtboxname = mydata(x).txtbox.name + "gpr"
                    If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                        cb.BeginText()
                        cb.SetFontAndSize(bf, size)
                        cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                        cb.EndText()
                    End If
                Next

                'Finance INfomation
                txtboxname = "csgprdealername"
                text = ""
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, mybosstore, posx, posy, 0)
                cb.EndText()

                text = ""
                Select Case mybosstorecode
                    Case "EDM"
                        text = "AB7590"
                    Case "ATL"
                        text = "AB7601"
                    Case "SYL"
                        text = "AB7602"
                    Case "REN"
                        text = "AB7604"
                    Case "BTH"
                        text = "BC7711"
                    Case "GAL"
                        text = "BC7454"
                    Case "OGO"
                        text = "BC7562"
                    Case "KEL"
                        text = "BC7562"
                End Select

                txtboxname = "csgprdealernum"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, text, posx, posy, 0)
                cb.EndText()

                text = ""
                Select Case mybosstorecode
                    Case "EDM"
                        text = "Edmonton"
                    Case "ATL"
                        text = "Edmonton"
                    Case "SYL"
                        text = "Sylvan Lake"
                    Case "REN"
                        text = "Calgary"
                    Case "BTH"
                        text = "Salmon Arm"
                    Case "GAL"
                        text = "Langley"
                    Case "OGO"
                        text = "Kelowna"
                    Case "KEL"
                        text = "Kelowna"
                End Select

                txtboxname = "csgprdealercity"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, text, posx, posy, 0)
                cb.EndText()

                text = ""
                Select Case mybosstorecode
                    Case "EDM"
                        text = "AB"
                    Case "ATL"
                        text = "AB"
                    Case "SYL"
                        text = "AB"
                    Case "REN"
                        text = "AB"
                    Case "BTH"
                        text = "BC"
                    Case "GAL"
                        text = "BC"
                    Case "OGO"
                        text = "BC"
                    Case "KEL"
                        text = "BC"
                End Select

                txtboxname = "csgprdealerprovince"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, text, posx, posy, 0)
                cb.EndText()

                txtboxname = "csgprproductdescription"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, cmbcstype.SelectedItem.ToString, posx, posy, 0)
                cb.EndText()

                txtboxname = "csgprbrand"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Me.txtboatmake.Text, posx, posy, 0)
                cb.EndText()

                txtboxname = "csgprproductyear"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Me.txtboatyear.Text, posx, posy, 0)
                cb.EndText()

                txtboxname = "csgprserialnumber"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Me.txtboathin.Text, posx, posy, 0)
                cb.EndText()


                txtboxname = "csgprmodel"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Me.txtboatmodel.Text, posx, posy, 0)
                cb.EndText()

                size = 10
                txtboxname = "csgprpolicyprice"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(tempx * 1, "N"), posx, posy, 0)
                cb.EndText()


                txtboxname = "csgprpolicygst"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(tempx * taxpercent, "N"), posx, posy, 0)
                cb.EndText()

                txtboxname = "csgprpolicytotal"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Format(tempx * (1 + taxpercent), "N"), posx, posy, 0)
                cb.EndText()
                Select Case cmbGPR.SelectedItem.ToString.ToLower
                    Case "$500"
                        txtboxname = "grp500"
                    Case "$1000"
                        txtboxname = "grp1000"
                    Case "$1500"
                        txtboxname = "grp1500"
                    Case "$2000"
                        txtboxname = "grp2000"
                    Case "$2500"
                        txtboxname = "grp2500"
                    Case "$3000"
                        txtboxname = "gpr3000"
                End Select
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, "x", posx, posy, 0)
                cb.EndText()


                doc.NewPage()
                Import = writer.GetImportedPage(reader, 2)
                cb.AddTemplate(Import, 0, 0)
            End If
            doc.Close()
            System.Diagnostics.Process.Start(savelocation)
        Else
            MessageBox.Show("This feature is not yet available for your store")
        End If
    End Sub

    Private Sub btnIAPWarranty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIAPWarranty.Click
        Dim equip As New equipment
        equip.persistant = persistant
        Dim posx, posy, align As Integer
        Dim text, txtboxname, datacode As String
        Dim size As Integer
        Dim inputfile, savelocation As String
        'Check if this store has CS certs enabled
        If Val(persistant.getvalue(persistant.tbl_Certsframe, "Active", "Product = 'IAP' AND Store = '" + mybosstorecode + "'", 0)) = 1 Then
            'If Yes
            Dim IAPsalesman As String
            IAPsalesman = bosdata.getvalue("Ext_warranty_SB")
            Dim IAPcert, digits, recordexists As New Integer
            Dim IAPcertstr, IAPprefix, formatstr As String
            digits = Val(persistant.getvalue(persistant.tbl_Certsframe, "Digits", "Product = 'IAP' AND Store = '" + mybosstorecode + "'", 0))
            IAPprefix = persistant.getvalue(persistant.tbl_Certsframe, "Prefix", "Product = 'IAP' AND Store = '" + mybosstorecode + "'", 0)
            'Check if this bos has a cert #
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            Try
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = "Select IAP from certsused where Store = '" + mybosstorecode + "' AND BOS = '" + mybosnumber.ToString + "'"
                'If this BOS already has a cert # use that
                Try
                    IAPcert = Val(cmd.ExecuteScalar)

                Catch ex As Exception
                    IAPcert = 0
                End Try
                If IAPcert = 0 Then
                    'IF not use the next one for store
                    cmd.CommandText = "Select MAX(IAP) from certsused where Store = '" + mybosstorecode + "'"
                    IAPcert = Val(cmd.ExecuteScalar.ToString) + 1
                    'IF none ever used for store use the start number
                    If IAPcert = 0 Then
                        IAPcert = Val(persistant.getvalue(persistant.tbl_Certsframe, "Start", "Product = 'IAP' AND Store = '" + mybosstorecode + "'", 0))
                    End If
                    'Write the number we took for this BOS to the records
                    cmd.CommandText = "Select BOS from certsused where Store = '" + mybosstorecode + "' AND BOS = '" + mybosnumber.ToString + "'"
                    recordexists = Val(cmd.ExecuteScalar)
                    If recordexists = 0 Then
                        cmd.CommandText = "Insert into certsused set IAP = '" + IAPcert.ToString + "', Store = '" + mybosstorecode + "', BOS = '" + mybosnumber.ToString + "'"
                    Else
                        cmd.CommandText = "Update certsused set IAP = '" + IAPcert.ToString + "' where Store = '" + mybosstorecode + "' and BOS = '" + mybosnumber.ToString + "'"
                    End If
                    cmd.ExecuteNonQuery()

                End If

                cmd.Dispose()
                conn.Close()
            Catch ex As MySqlException

                MessageBox.Show("Error: " + ex.Message)
            End Try
            conn.Dispose()
            formatstr = ""

            For x As Integer = 1 To digits
                formatstr = formatstr + "0"
            Next
            IAPcertstr = IAPprefix + Format(IAPcert, formatstr)

            '### DO PRINT
            save()


            datacode = "IAP"

            inputfile = persistant.installdir + "\resources\Contracts\IAP.pdf"
            savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-IAP.pdf"

            Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            '   Try
            Dim reader As PdfReader = New PdfReader(inputfile)
            Dim doc As Document = New Document(reader.GetPageSize(1))
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
            doc.Open()
            Dim cb As PdfContentByte = writer.DirectContent


            doc.NewPage()
            Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)
            cb.AddTemplate(Import, 0, 0)


            size = 13
            txtboxname = "IAPpolicy"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, IAPcertstr, posx, posy, 0)
            cb.EndText()

            size = 9
            txtboxname = "IAPnamel"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtbuyer1last.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "IAPnamef"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtbuyer1first.Text, posx, posy, 0)
            cb.EndText()


            'Personal Data
            For x As Integer = 6 To 10
                txtboxname = mydata(x).txtbox.name
                If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(0, mydata(x).txtbox.text, posx, posy, 0)
                    cb.EndText()
                End If
            Next
            size = 7
            'Boat Data
            For x As Integer = 81 To 92
                txtboxname = mydata(x).txtbox.name
                If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(0, mydata(x).txtbox.text, posx, posy, 0)
                    cb.EndText()
                End If
            Next

            txtboxname = "IAPhp"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_motors, "HP", "brand = '" + Me.txtmotormake.Text + "' AND model = '" + Me.txtmotormodel.Text + "'", 0) + " hp", posx, posy, 0)
            cb.EndText()

            txtboxname = "IAPlengthofboat"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_boats, "Length", "Brand = '" + Me.txtboatmake.Text + "' AND Model = '" + Me.txtboatmodel.Text + "'", 0) + " ft", posx, posy, 0)
            cb.EndText()

            size = 10

            txtboxname = "IAPtoday1"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Date.Today.ToString("MM/dd/yyyy"), posx, posy, 0)
            cb.EndText()

            txtboxname = "IAPtoday2"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Date.Today.ToString("MM/dd/yyyy"), posx, posy, 0)
            cb.EndText()

            size = 9

            txtboxname = "IAPleinholder"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If Me.ListBanks.SelectedItem <> Nothing Then
                cb.ShowTextAligned(align, Me.ListBanks.SelectedItem, posx, posy, 0)
            Else
                cb.ShowTextAligned(align, "", posx, posy, 0)
            End If
            cb.EndText()


            'Finance INfomation
            txtboxname = "IAPdealername"
            text = ""
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, mybosstore, posx, posy, 0)
            cb.EndText()

            text = ""
            Select Case mybosstorecode
                Case "EDM"
                    text = "MAMSW"
                Case "ATL"
                    text = "MAMSW"
                Case "SYL"
                    text = "MAMLH"
                Case "REN"
                    text = "MAMRN"
                Case "BTH"
                    text = "MAMBT"
                Case "GAL"
                    text = "MAMGL"
                Case "OGO"
                    text = "MAMAT"
                Case "KEL"
                    text = "MAMAT"
            End Select

            txtboxname = "IAPdealernumber"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()

            Dim dealerphone, dealerfax As String
            Select Case mybosstorecode
                Case "EDM"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
                Case "ATL"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
                Case "SYL"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
                Case "REN"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
                Case "BTH"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
                Case "GAL"
                    dealerphone = "(604) 514-0460"
                    dealerfax = "(604) 514-0470"
                Case "OGO"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
                Case "KEL"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
            End Select

            txtboxname = "IAPdealerphone"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, dealerphone, posx, posy, 0)
            cb.EndText()

            size = 11

            txtboxname = "IAPstartdate"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, IAPdelDATE.Value.ToString("MM        dd        yyyy"), posx, posy, 0)
            cb.EndText()

            txtboxname = "IAPpurchasedate"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, IAPdelDATE.Value.ToString("MM        dd        yyyy"), posx, posy, 0)
            cb.EndText()


            Dim tempdate As DateTime
            tempdate = IAPdelDATE.Value
            tempdate = tempdate.AddYears(CInt(IAPextWarranty.Text))

            txtboxname = "IAPexpiry"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, tempdate.ToString("MM        dd        yyyy"), posx, posy, 0)
            cb.EndText()

            txtboxname = "IAPoemlength"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, (CInt(IAPoemWarranty.Text) * 12).ToString, posx, posy, 0)
            cb.EndText()

            txtboxname = "IAPlength"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, (CInt(IAPextWarranty.Text) * 12).ToString, posx, posy, 0)
            cb.EndText()

            Dim taxpercent
            Dim tempx, tempy As Decimal
            Dim tstr As String
            tstr = Me.txtsubtotal.Text
            tstr = Replace(tstr, "$", "")
            tstr = Replace(tstr, ",", "")
            tempy = Val(tstr)

            tstr = Me.txtextwarranty.Text
            tstr = Replace(tstr, "$", "")
            tstr = Replace(tstr, ",", "")
            tempx = Val(tstr)

            tempy = tempy - tempx
            taxpercent = 0

            If ChkGST.Checked = True Then
                taxpercent = taxpercent + GSTRate
            End If
            If chkPST.Checked = True Then
                taxpercent = taxpercent + PSTRate
            End If

            txtboxname = "IAPpurchaseprice"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(tempy, "N"), posx, posy, 0)
            cb.EndText()

            txtboxname = "IAPprice"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(tempx * 1, "N"), posx, posy, 0)
            cb.EndText()

            txtboxname = "IAPhst"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(tempx * taxpercent, "N"), posx, posy, 0)
            cb.EndText()

            txtboxname = "IAPtotal"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(tempx * (1 + taxpercent), "N"), posx, posy, 0)
            cb.EndText()

            size = 12

            Select Case IAPproduct.SelectedItem
                Case "Inboard/Sterndrive/Jet"
                    If IAPused.Checked = True Then
                        txtboxname = "IAPibUSED"
                    Else
                        txtboxname = "IAPibNEW"
                    End If
                Case "Outboard"
                    If IAPused.Checked = True Then
                        txtboxname = "IAPobUSED"
                    Else
                        txtboxname = "IAPobNEW"
                    End If
                Case "Stand Alone Outboard"
                    If IAPused.Checked = True Then
                        txtboxname = "IAPstandaloneOBused"
                    Else
                        txtboxname = "IAPstandaloneOBnew"
                    End If
            End Select
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "x", posx, posy, 0)
            cb.EndText()

            If IAPgpr.Checked = True Then
                txtboxname = "IAPgpr"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, "x", posx, posy, 0)
                cb.EndText()
            End If

            If IAPaftersale.Checked = True Then
                txtboxname = "IAPaftersale"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, "x", posx, posy, 0)
                cb.EndText()
            End If

            If IAPseals.Checked = True Then
                txtboxname = "IAPseals"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, "x", posx, posy, 0)
                cb.EndText()
            End If


            Select Case IAPdeduct.SelectedItem.ToString.ToLower
                Case "$100"
                    txtboxname = "IAP100ded"
                Case "$50"
                    txtboxname = "IAP50ded"
                Case "$500 (diesel)"
                    txtboxname = "IAP500ded"
            End Select

            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "x", posx, posy, 0)
            cb.EndText()



            doc.NewPage()
            Import = writer.GetImportedPage(reader, 2)
            cb.AddTemplate(Import, 0, 0)
            doc.NewPage()
            Import = writer.GetImportedPage(reader, 3)
            cb.AddTemplate(Import, 0, 0)
            doc.NewPage()
            Import = writer.GetImportedPage(reader, 4)
            cb.AddTemplate(Import, 0, 0)
            doc.NewPage()
            Import = writer.GetImportedPage(reader, 5)
            cb.AddTemplate(Import, 0, 0)
            doc.NewPage()
            Import = writer.GetImportedPage(reader, 6)
            cb.AddTemplate(Import, 0, 0)
            doc.NewPage()
            Import = writer.GetImportedPage(reader, 7)
            cb.AddTemplate(Import, 0, 0)


            If Me.IAPgpr.Checked = True Then
                inputfile = persistant.installdir + "\resources\Contracts\IAPgpr.pdf"
                reader = New PdfReader(inputfile)
                doc.NewPage()
                Import = writer.GetImportedPage(reader, 1)
                cb.AddTemplate(Import, 0, 0)

                size = 10

                txtboxname = "IAPgprPolicy"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, IAPcertstr, posx, posy, 0)
                cb.EndText()

                txtboxname = "IAPgprPurchaseDate"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, IAPdelDATE.Value.ToString("MMMM dd, yyyy"), posx, posy, 0)
                cb.EndText()

                txtboxname = "IAPgprOEMDate"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, IAPdelDATE.Value.ToString("MMMM dd, yyyy"), posx, posy, 0)
                cb.EndText()

                txtboxname = "IAPgprUnitPurchaseDate"
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, IAPdelDATE.Value.ToString("MMMM dd, yyyy"), posx, posy, 0)
                cb.EndText()
            End If

            doc.Close()
            System.Diagnostics.Process.Start(savelocation)
        Else
            MessageBox.Show("This feature is not yet available for your store")
        End If
    End Sub

    Private Sub btnWarrantyWaiver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWarrantyWaiver.Click

        '### DO PRINT
        save()

        Dim posx, posy, align As Integer
        Dim text, txtboxname, datacode As String
        Dim size As Integer
        Dim inputfile, savelocation, dealerphone, dealerfax As String
        datacode = "WAI"
        Dim imagex As Image

        Select Case mybosstorecode
            Case "ATL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\ATLlogo.PNG")
            Case "KEL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\ATLlogo.PNG")
            Case "BTH"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\BTHlogo.PNG")
            Case "GAL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\GALlogo.PNG")
            Case "OGO"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\OGOlogo.PNG")
            Case "REN"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\RENlogo.PNG")
            Case "EDM"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\EDMlogo.PNG")
            Case "SYL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\SYLlogo.PNG")
        End Select


        inputfile = persistant.installdir + "\resources\Contracts\WaiverWar.pdf"
        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-WaiverWar.pdf"

        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        '   Try
        Dim reader As PdfReader = New PdfReader(inputfile)
        Dim doc As Document = New Document(reader.GetPageSize(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
        doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent


        doc.NewPage()
        Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)
        cb.AddTemplate(Import, 0, 0)
        size = 14
        imagex.ScalePercent(70)
        doc.Add(imagex)


        txtboxname = "WAIWdate"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(DateTime.Now, "MMMM dd, yyyy"), posx, posy, 0)
        cb.EndText()

        txtboxname = "WAIWmotor"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtmotormake.Text + " " + txtmotormodel.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WAIWmwarr"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, CSmanwarr.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WAIWewarr"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, CSextwarr.Text, posx, posy, 0)
        cb.EndText()


        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    Private Sub btnprintAT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprintAT.Click
        If txtatnum.Text.Length < 4 Then
            MessageBox.Show("You need to enter the Etch number before you can print this form")
        Else

            Dim CScertstr As String = txtatnum.Text

            '### DO PRINT
            save()

            Dim posx, posy, align As Integer
            Dim text, txtboxname, datacode As String
            Dim size As Integer
            Dim inputfile, savelocation As String
            datacode = "AT"

            inputfile = persistant.installdir + "\resources\Contracts\AT.pdf"
            savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-AT.pdf"

            Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            '   Try
            Dim reader As PdfReader = New PdfReader(inputfile)
            Dim doc As Document = New Document(reader.GetPageSize(1))
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
            doc.Open()
            Dim cb As PdfContentByte = writer.DirectContent


            doc.NewPage()
            Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)

            cb.AddTemplate(Import, 0, 0)


            size = 11
            align = 2
            txtboxname = "ATcert"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, CScertstr, posx, posy, 0)
            cb.EndText()

            'Last name, First Name & Last name, Firstname
            text = txtbuyer1last.Text
            If txtbuyer1first.Text <> "" Then
                text = text + ", " + txtbuyer1first.Text
            End If
            If txtbuyer2last.Text <> "" Then
                text = text + " & " + txtbuyer2last.Text
            End If

            If txtbuyer2first.Text <> "" Then
                If txtbuyer2last.Text = "" Then
                    text = text + txtbuyer2first.Text
                Else
                    text = text + ", " + txtbuyer2first.Text
                End If
            End If
            align = 0
            txtboxname = "txtboathin"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtboathin.Text, posx, posy, 0)
            cb.EndText()

            size = 10

            txtboxname = "buyersname"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()

            txtboxname = "ATdealer"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.mybosstore, posx, posy, 0)
            cb.EndText()

            'Personal Data
            For x As Integer = 6 To 10
                txtboxname = mydata(x).txtbox.name
                If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                    cb.EndText()
                End If
            Next

            txtboxname = "txtboatmake"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtboatmake.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "txtboatmodel"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtboatmodel.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "txtboatyear"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtboatyear.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "txtcolor"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtcolor.Text, posx, posy, 0)
            cb.EndText()


            txtboxname = "ATterm"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "60", posx, posy, 0)
            cb.EndText()


            txtboxname = "ATBen"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "$5,000", posx, posy, 0)
            cb.EndText()

            align = 1

            txtboxname = "ATEtch"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "X", posx, posy, 0)
            cb.EndText()

            Dim tempstring As String
            Dim tempx As Integer = Now.Year + 5
            tempstring = tempx.ToString + "  " + Now.Month.ToString + "  " + Now.Day.ToString

            txtboxname = "ATexp"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, tempstring, posx, posy, 0)
            cb.EndText()

            txtboxname = "PTdateY"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Now.Year.ToString, posx, posy, 0)
            cb.EndText()
            txtboxname = "PTdateM"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Now.Month.ToString, posx, posy, 0)
            cb.EndText()
            txtboxname = "PTdateD"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Now.Day.ToString, posx, posy, 0)
            cb.EndText()


            doc.Close()
            System.Diagnostics.Process.Start(savelocation)
        End If
    End Sub

    Private Sub btnProTech_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnProTech.Click

        Dim CScertstr As String = txtatnum.Text

        '### DO PRINT
        save()

        Dim posx, posy, align As Integer
        Dim text, txtboxname, datacode As String
        Dim size As Integer
        Dim inputfile, savelocation As String
        datacode = "CHEM"

        inputfile = persistant.installdir + "\resources\Contracts\PT.pdf"
        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-PT.pdf"

        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        '   Try
        Dim reader As PdfReader = New PdfReader(inputfile)
        Dim doc As Document = New Document(reader.GetPageSize(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
        doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent


        doc.NewPage()
        Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)

        cb.AddTemplate(Import, 0, 0)


        size = 11
        align = 2
        txtboxname = "PTcert"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        If txtboathin.Text = "" Then
            cb.ShowTextAligned(align, "PT-OnOrder", posx, posy, 0)
        Else
            cb.ShowTextAligned(align, Me.txtboathin.Text.ToString.Remove(0, 4), posx, posy, 0)
        End If
        cb.EndText()

        'Last name, First Name & Last name, Firstname
        text = txtbuyer1last.Text
        If txtbuyer1first.Text <> "" Then
            text = text + ", " + txtbuyer1first.Text
        End If
        If txtbuyer2last.Text <> "" Then
            text = text + " & " + txtbuyer2last.Text
        End If

        If txtbuyer2first.Text <> "" Then
            If txtbuyer2last.Text = "" Then
                text = text + txtbuyer2first.Text
            Else
                text = text + ", " + txtbuyer2first.Text
            End If
        End If
        align = 0
        txtboxname = "txtboathin"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Me.txtboathin.Text, posx, posy, 0)
        cb.EndText()

        size = 10

        txtboxname = "buyersname"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, text, posx, posy, 0)
        cb.EndText()

        txtboxname = "PTdealer"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Me.mybosstore, posx, posy, 0)
        cb.EndText()

        'Personal Data
        For x As Integer = 6 To 10
            txtboxname = mydata(x).txtbox.name
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                cb.EndText()
            End If
        Next

        txtboxname = "txtboatmake"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Me.txtboatmake.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "txtboatmodel"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Me.txtboatmodel.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "txtboatyear"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Me.txtboatyear.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "txtcolor"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Me.txtcolor.Text, posx, posy, 0)
        cb.EndText()
        align = 1
        txtboxname = "Ptpaintg"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, "X", posx, posy, 0)
        cb.EndText()

        txtboxname = "Ptvinyl"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, "X", posx, posy, 0)
        cb.EndText()

        txtboxname = "Ptmaint"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, "X", posx, posy, 0)
        cb.EndText()

        If txtboatmake.Text = "Used" Then
            txtboxname = "PTused"
        Else
            txtboxname = "Ptnew"
        End If

        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, "X", posx, posy, 0)
        cb.EndText()

        If CmbPTfabric.SelectedItem = "YES" Then
            txtboxname = "Ptfabric"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "X", posx, posy, 0)
            cb.EndText()
        End If
        If CmbPTrtb.SelectedItem = "YES" Then
            txtboxname = "Ptrtb"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "X", posx, posy, 0)
            cb.EndText()
        End If

        Dim tempstring As String
        Dim tempx As Integer = Now.Year
        tempstring = tempx.ToString + "  " + Now.Month.ToString + "  " + Now.Day.ToString

        txtboxname = "PTexp"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, tempstring, posx, posy, 0)
        cb.EndText()

        txtboxname = "PTdateY"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Now.Year.ToString, posx, posy, 0)
        cb.EndText()
        txtboxname = "PTdateM"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Now.Month.ToString, posx, posy, 0)
        cb.EndText()
        txtboxname = "PTdateD"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Now.Day.ToString, posx, posy, 0)
        cb.EndText()

        doc.NewPage()
        Import = writer.GetImportedPage(reader, 2)
        cb.AddTemplate(Import, 0, 0)


        doc.NewPage()
        Import = writer.GetImportedPage(reader, 3)
        cb.AddTemplate(Import, 0, 0)
        size = 12
        align = 0
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Me.txtboathin.Text.ToString.Remove(0, 4), 192, 620, 270)
        cb.EndText()
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Me.txtboathin.Text.ToString.Remove(0, 4), 192, 224, 270)
        cb.EndText()

        doc.Close()
        System.Diagnostics.Process.Start(savelocation)

        Dim ptcost As Decimal
        ptcost = 55
        If CmbPTreg.SelectedItem = "NO" Then
            ptcost = ptcost + 20
        Else
            ptcost = ptcost + 50
        End If
        If CmbPTrtb.SelectedItem = "YES" Then
            ptcost = ptcost + 33
        End If
        If CmbPTfabric.SelectedItem = "YES" Then
            ptcost = ptcost + 50
        End If
        If CmbPTinstalled.SelectedItem = "YES" Then
            ptcost = ptcost + 120
        End If
        If CmbPTinstalled.SelectedItem = "NO" And CmbPTreg.SelectedItem = "NO" Then ptcost = ptcost - 20

        txtPTcost.Text = ptcost.ToString
        moneychanged(txtPTcost)

    End Sub

    Private Sub btnPrintSAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintSAL.Click
        Dim equip As New equipment
        equip.persistant = persistant
        Dim posx, posy, align As Integer
        Dim txtboxname, datacode As String
        Dim size As Integer
        Dim inputfile, savelocation As String
        'Check if this store has CS certs enabled
        If Val(persistant.getvalue(persistant.tbl_Certsframe, "Active", "Product = 'SAL2012' AND Store = '" + mybosstorecode + "'", 0)) = 1 Then
            'If Yes
            Dim IAPsalesman As String
            IAPsalesman = bosdata.getvalue("Ext_warranty_SB")
            Dim IAPcert, digits, recordexists As New Integer
            Dim IAPcertstr, IAPprefix, formatstr As String
            digits = Val(persistant.getvalue(persistant.tbl_Certsframe, "Digits", "Product = 'SAL2012' AND Store = '" + mybosstorecode + "'", 0))
            IAPprefix = persistant.getvalue(persistant.tbl_Certsframe, "Prefix", "Product = 'SAL2012' AND Store = '" + mybosstorecode + "'", 0)
            'Check if this bos has a cert #
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            Try
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = "Select SAL2012 from certsused where Store = '" + mybosstorecode + "' AND BOS = '" + mybosnumber.ToString + "'"
                'If this BOS already has a cert # use that
                Try
                    IAPcert = Val(cmd.ExecuteScalar)

                Catch ex As Exception
                    IAPcert = 0
                End Try
                If IAPcert = 0 Then
                    'IF not use the next one for store
                    cmd.CommandText = "Select MAX(SAL2012) from certsused where Store = '" + mybosstorecode + "'"
                    IAPcert = Val(cmd.ExecuteScalar.ToString) + 1
                    'IF none ever used for store use the start number
                    If IAPcert = 0 Then
                        IAPcert = Val(persistant.getvalue(persistant.tbl_Certsframe, "Start", "Product = 'SAL2012' AND Store = '" + mybosstorecode + "'", 0))
                    End If
                    'Write the number we took for this BOS to the records
                    cmd.CommandText = "Select BOS from certsused where Store = '" + mybosstorecode + "' AND BOS = '" + mybosnumber.ToString + "'" ' And Product = 'IAP2012'"
                    recordexists = Val(cmd.ExecuteScalar)
                    If recordexists = 0 Then
                        cmd.CommandText = "Insert into certsused set SAL2012 = '" + IAPcert.ToString + "', Store = '" + mybosstorecode + "', BOS = '" + mybosnumber.ToString + "'"
                    Else
                        cmd.CommandText = "Update certsused set SAL2012 = '" + IAPcert.ToString + "' where Store = '" + mybosstorecode + "' and BOS = '" + mybosnumber.ToString + "'"
                    End If
                    cmd.ExecuteNonQuery()

                End If

                cmd.Dispose()
                conn.Close()
            Catch ex As MySqlException

                MessageBox.Show("Error: " + ex.Message)
            End Try
            conn.Dispose()
            formatstr = ""

            For x As Integer = 1 To digits
                formatstr = formatstr + "0"
            Next
            IAPcertstr = IAPprefix + Format(IAPcert, formatstr)

            '### DO PRINT
            save()


            datacode = "SAL"

            inputfile = persistant.installdir + "\resources\Contracts\SAL2012.pdf"
            savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-IAP.pdf"

            Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            '   Try
            Dim reader As PdfReader = New PdfReader(inputfile)
            Dim doc As Document = New Document(reader.GetPageSize(1))
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
            doc.Open()
            Dim cb As PdfContentByte = writer.DirectContent


            Dim Import As PdfImportedPage
            'do not need the first two pages.
            'doc.NewPage()
            'Import = writer.GetImportedPage(reader, 1)
            'cb.AddTemplate(Import, 0, 0)
            'doc.NewPage()
            'Import = writer.GetImportedPage(reader, 2)
            'cb.AddTemplate(Import, 0, 0)

            doc.NewPage()
            'Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)
            Import = writer.GetImportedPage(reader, 3)
            cb.AddTemplate(Import, 0, 0)


            size = 13
            'txtboxname = "IAP2012policy"
            posx = 535
            posy = 700
            align = 1
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, IAPcertstr, posx, posy, 0)
            cb.EndText()

            size = 10
            'txtboxname = "IAP2012stockNo"
            posx = 215
            posy = 700
            align = 1
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtboatunit.Text, posx, posy, 0)
            cb.EndText()

            size = 9

            Dim ds As String ' = IAPdelDATE.Value.ToString("ddMMMyyyy")
            Dim theDay As String ' = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1)
            Dim theMonth As String ' = Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1)
            Dim theYear As String ' = Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
            'If Not txtdatedeled.Text = "" Then
            '    ''txtboxname = "IAP2012approvaldate"
            '    'posx = 263
            '    'posy = 674
            '    'align = 0
            '    theMonth = CDate(txtdatedeled.Text).ToString("MMM")
            '    ds = Mid(txtdatedeled.Text, 1, 2) & theMonth & Mid(txtdatedeled.Text, 7, 4)
            '    theDay = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1)
            '    theMonth = Mid(theMonth, 1, 1) & "   " & Mid(theMonth, 2, 1) & "   " & Mid(theMonth, 3, 1)
            '    theYear = Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
            '    'ds = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1) & "    " & Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1) & _
            '    '        "     " & Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
            '    'cb.BeginText()
            '    'cb.SetFontAndSize(bf, size)
            '    'cb.ShowTextAligned(align, theDay, posx, posy, 0)
            '    'cb.EndText()
            '    'cb.BeginText()
            '    'cb.SetFontAndSize(bf, size)
            '    'cb.ShowTextAligned(align, theMonth, posx + 26, posy, 0)
            '    'cb.EndText()
            '    'cb.BeginText()
            '    'cb.SetFontAndSize(bf, size)
            '    'cb.ShowTextAligned(align, theYear, posx + 64, posy, 0)
            '    'cb.EndText()
            'Else
            '    ds = IAPdelDATE.Value.ToString("ddMMMyyyy")
            '    theDay = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1)
            '    theMonth = Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1)
            '    theYear = Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)

            'End If

            ds = IAPdelDATE.Value.ToString("ddMMMyyyy")
            theDay = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1)
            theMonth = Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1)
            theYear = Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)


            'txtboxname = "IAP2012approvaldate"
            posx = 263
            posy = 674
            align = 0
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theDay, posx, posy, 0)
            cb.EndText()
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theMonth, posx + 26, posy, 0)
            cb.EndText()
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theYear, posx + 64, posy, 0)
            cb.EndText()

            'txtboxname = "IAP2012startdate"
            posx = 263
            posy = 700
            align = 0
            'Dim ds As String = IAPdelDATE.Value.ToString("ddMMMyyyy")
            'Dim theDay As String = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1)
            'Dim theMonth As String = Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1)
            'Dim theYear As String = Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)

            'ds = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1) & "    " & Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1) & _
            '        "     " & Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theDay, posx, posy, 0)
            cb.EndText()
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theMonth, posx + 26, posy, 0)
            cb.EndText()
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theYear, posx + 64, posy, 0)
            cb.EndText()

            'txtboxname = "IAP2012firstpaymentdate"
            posx = 375
            posy = 700
            align = 0
            ds = (IAPdelDATE.Value.AddDays(30)).ToString("ddMMMyyyy")
            theDay = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1)
            theMonth = Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1)
            theYear = Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
            'ds = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1) & "    " & Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1) & _
            '        "     " & Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theDay, posx, posy, 0)
            cb.EndText()
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theMonth, posx + 26, posy, 0)
            cb.EndText()
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theYear, posx + 64, posy, 0)
            cb.EndText()

            'If Not txtdatedeled.Text = "" Then
            '    'txtboxname = "IAP2012approvaldate"
            '    posx = 263
            '    posy = 674
            '    align = 0
            '    theMonth = CDate(txtdatedeled.Text).ToString("MMM")
            '    ds = Mid(txtdatedeled.Text, 1, 2) & theMonth & Mid(txtdatedeled.Text, 7, 4)
            '    theDay = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1)
            '    theMonth = Mid(theMonth, 1, 1) & "   " & Mid(theMonth, 2, 1) & "   " & Mid(theMonth, 3, 1)
            '    theYear = Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
            '    'ds = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1) & "    " & Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1) & _
            '    '        "     " & Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
            '    cb.BeginText()
            '    cb.SetFontAndSize(bf, size)
            '    cb.ShowTextAligned(align, theDay, posx, posy, 0)
            '    cb.EndText()
            '    cb.BeginText()
            '    cb.SetFontAndSize(bf, size)
            '    cb.ShowTextAligned(align, theMonth, posx + 26, posy, 0)
            '    cb.EndText()
            '    cb.BeginText()
            '    cb.SetFontAndSize(bf, size)
            '    cb.ShowTextAligned(align, theYear, posx + 64, posy, 0)
            '    cb.EndText()
            'End If

            'txtboxname = "IAP2012expirydate"
            posx = 375
            posy = 674
            align = 0
            ds = (IAPdelDATE.Value.AddMonths(CInt(cmbTerm.SelectedItem))).ToString("ddMMMyyyy")
            theDay = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1)
            theMonth = Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1)
            theYear = Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
            'ds = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1) & "    " & Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1) & _
            '        "     " & Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theDay, posx, posy, 0)
            cb.EndText()
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theMonth, posx + 26, posy, 0)
            cb.EndText()
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theYear, posx + 64, posy, 0)
            cb.EndText()

            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theDay, posx + 112, posy, 0)
            cb.EndText()
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theMonth, posx + 138, posy, 0)
            cb.EndText()
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, theYear, posx + 176, posy, 0)
            cb.EndText()

            size = 10
            'txtboxname = "SAL2012store"
            Dim storeName As String = "Shipwreck Marine"
            Select Case mybosstorecode
                Case "EDM"
                    storeName = "Shipwreck Marine"
                Case "REN"
                    storeName = "Renfrew Marine"
                Case "BTH"
                    storeName = "Boat House Marine and Leisure"
                Case "ATL"
                    storeName = "Atlantis Marine"
                Case "KEL"
                    storeName = "Atlantis Marine"
                Case "SYL"
                    storeName = "Lighthouse Marine"
            End Select
            posx = 60
            posy = 648
            align = 0
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, storeName, posx, posy, 0)
            cb.EndText()

            'bank name
            If Me.ListBanks.SelectedItem <> Nothing Then
                'txtboxname = "SAL2012leinholder"
                posx = 350
                posy = 648
                align = 1
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, Me.ListBanks.SelectedItem, posx, posy, 0)
                cb.EndText()
            End If

            'debtor names
            'txtboxname = "SAL2012namel"
            posx = 105
            posy = 622
            align = 1
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtbuyer1last.Text, posx, posy, 0)
            cb.EndText()

            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtbuyer2last.Text, posx, posy - 52, 0)
            cb.EndText()

            'txtboxname = "SAL2012namef"
            posx = 240
            posy = 622
            align = 1
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtbuyer1first.Text, posx, posy, 0)
            cb.EndText()

            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtbuyer2first.Text, posx, posy - 52, 0)
            cb.EndText()

            ''Personal Data
            ''mydata(6) = txtaddress , mydata(7) = txtcity , mydata(8) = txtprov , mydata(9) = txtpostal , mydata(10) = txthome
            For x As Integer = 6 To 10
                txtboxname = "SAL2012" + mydata(x).txtbox.name
                If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
                    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
                    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(0, IIf(x = 10, Replace(Replace(Replace(mydata(x).txtbox.text, "(", ""), ")", ""), " ", "      "), mydata(x).txtbox.text), posx, posy, 0)
                    cb.EndText()

                    If Not txtbuyer2first.Text = "" Then
                        cb.BeginText()
                        cb.SetFontAndSize(bf, size)
                        cb.ShowTextAligned(0, IIf(x = 10, Replace(Replace(Replace(mydata(x).txtbox.text, "(", ""), ")", ""), " ", "      "), mydata(x).txtbox.text), posx, posy - 50, 0)
                        cb.EndText()
                    End If
                End If
            Next

            'birthdays
            size = 9
            If Not txtbday1.Text = "" Then
                posx = 487
                posy = 622
                align = 0
                ds = txtbday1.Text
                theDay = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1)
                If IsNumeric(Mid(ds, 3, 2)) Then
                    'month is numeric
                    theMonth = Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1)
                    theYear = Mid(ds, 5, 1) & "   " & Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1)
                Else
                    theMonth = Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1)
                    theYear = Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
                End If
                'ds = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1) & "   " & Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1) & _
                '        "   " & Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, theDay, posx, posy, 0)
                cb.EndText()
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, theMonth, posx + 26, posy, 0)
                cb.EndText()
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, theYear, posx + 64, posy, 0)
                cb.EndText()
            End If

            If Not txtbday2.Text = "" Then
                posx = 487
                posy = 570
                align = 0
                ds = txtbday2.Text
                theDay = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1)
                If IsNumeric(Mid(ds, 3, 2)) Then
                    'month is numeric
                    theMonth = Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1)
                    theYear = Mid(ds, 5, 1) & "   " & Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1)
                Else
                    theMonth = Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1)
                    theYear = Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
                End If
                'ds = Mid(ds, 1, 1) & "   " & Mid(ds, 2, 1) & "   " & Mid(ds, 3, 1) & "   " & Mid(ds, 4, 1) & "   " & Mid(ds, 5, 1) & _
                '        "   " & Mid(ds, 6, 1) & "   " & Mid(ds, 7, 1) & "   " & Mid(ds, 8, 1) & "   " & Mid(ds, 9, 1)
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, theDay, posx, posy, 0)
                cb.EndText()
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, theMonth, posx + 26, posy, 0)
                cb.EndText()
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, theYear, posx + 64, posy, 0)
                cb.EndText()
            End If


            'total financed
            size = 11
            posx = 100
            posy = 521
            align = 1
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, FormatNumber(FandI.dollerstotal, 2), posx, posy, 0)
            cb.EndText()

            'monthly payment
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, FormatNumber(FandI.pmt, 2), posx, posy - 26, 0)
            cb.EndText()

            'residual value 
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, FormatNumber(FandI.residual, 2), posx + 140, posy, 0)
            cb.EndText()

            'deal type  
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "Financed", posx + 140, posy - 26, 0)
            cb.EndText()

            'rate   
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, FandI.Rate * 100, posx + 300, posy, 0)
            cb.EndText()

            'finance months   
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, FandI.Term, posx + 300, posy - 26, 0)
            cb.EndText()

            'amortized months   
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, FandI.Ammort, posx + 430, posy - 26, 0)
            'If Me.txtammover.Text <> "" Then
            '    cb.ShowTextAligned(align, Me.txtammover.Text, posx + 430, posy - 26, 0)
            'Else
            '    cb.ShowTextAligned(align, Me.cmbammort.SelectedItem.ToString, posx + 430, posy - 26, 0)
            'End If
            cb.EndText()

            'life insurance selection
            Select Case cmblins.SelectedItem
                Case "Buyer"
                    posx = 106
                Case "Co-Buyer"
                    posx = 184
                Case "Both"
                    posx = 279
                Case "Declined"
                    posx = 398
            End Select
            posy = 457
            align = 0
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "x", posx, posy, 0)
            cb.EndText()

            If Not cmblins.SelectedItem = "Declined" Then

                'life financed amount
                posx = 220
                posy = 433
                align = 1
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                If FandI.Ammort > FandI.Term Then
                    cb.ShowTextAligned(align, FormatNumber(FandI.dollerstotal - FandI.residual, 2), posx, posy, 0)
                Else
                    cb.ShowTextAligned(align, FormatNumber(FandI.dollerstotal, 2), posx, posy, 0)
                End If
                cb.EndText()

                'life terms  
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FandI.Term, posx + 180, posy, 0)
                cb.EndText()

                'life premium  
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FormatNumber(FandI.lifec, 2), posx + 310, posy, 0)
                cb.EndText()

                'life residual amount
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FormatNumber(FandI.residual, 2), posx, posy - 26, 0)
                cb.EndText()

                'life residual terms  
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FandI.Term, posx + 180, posy - 26, 0)
                cb.EndText()

                'life residual premium  
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FormatNumber(FandI.lifer, 2), posx + 310, posy - 26, 0)
                cb.EndText()

            End If


            'critical illness selection
            Select Case cmbcins.SelectedItem
                Case "Buyer"
                    posx = 106
                Case "Co-Buyer"
                    posx = 184
                Case "Both"
                    posx = 279
                Case "Declined"
                    posx = 398
            End Select
            posy = 366
            align = 0
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "x", posx, posy, 0)
            cb.EndText()

            If Not cmbcins.SelectedItem = "Declined" Then
                'critical financed amount
                posx = 220
                posy = 342
                align = 1
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                If FandI.Ammort > FandI.Term Then
                    cb.ShowTextAligned(align, FormatNumber(FandI.dollerstotal - FandI.residual, 2), posx, posy, 0)
                Else
                    cb.ShowTextAligned(align, FormatNumber(FandI.dollerstotal, 2), posx, posy, 0)
                End If
                cb.EndText()

                'critical terms  
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FandI.Term, posx + 180, posy, 0)
                cb.EndText()

                'critical premium  
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FormatNumber(FandI.Criticalc, 2), posx + 310, posy, 0)
                cb.EndText()

                'critical residual amount
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FormatNumber(FandI.residual, 2), posx, posy - 26, 0)
                cb.EndText()

                'critical residual terms  
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FandI.Term, posx + 180, posy - 26, 0)
                cb.EndText()

                'critical residual premium  
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FormatNumber(FandI.Criticalr, 2), posx + 310, posy - 26, 0)
                cb.EndText()

            End If


            'accident and sickness selection
            Select Case cmbains.SelectedItem
                Case "Buyer"
                    posx = 106
                Case "Co-Buyer"
                    posx = 184
                Case "Both"
                    posx = 279
                Case "Declined"
                    posx = 398
            End Select
            posy = 275
            align = 0
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "x", posx, posy, 0)
            cb.EndText()

            If Not cmbains.SelectedItem = "Declined" Then
                posx = 220
                posy = 252
                align = 1
                If Not cmbahtype.Text = "" Then

                    'waiting period
                    Dim ahType() As String = Split(cmbahtype.SelectedItem, " ")
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(1, ahType(0), 80, posy, 0)
                    cb.EndText()

                    'accident and sickness waiting type
                    cb.BeginText()
                    cb.SetFontAndSize(bf, size)
                    cb.ShowTextAligned(align, ahType(2), posx, posy, 0)
                    cb.EndText()
                End If

                'accident and sickness monthly amount
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FormatNumber(FandI.pmt, 2), posx, posy - 26, 0)
                cb.EndText()

                'accident and sickness terms  
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FandI.Term, posx + 180, posy - 26, 0)
                cb.EndText()

                'accident and sickness premium  
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FormatNumber(FandI.AandH, 2), posx + 310, posy - 26, 0)
                cb.EndText()

            End If


            'accidental Plus selection
            Select Case cmbAccidentalPlus.SelectedItem
                Case "Buyer"
                    posx = 106
                Case "Co-Buyer"
                    posx = 184
                Case "Both"
                    posx = 279
                Case "Declined"
                    posx = 398
                Case Else
                    posx = 398 'when non are selected then set it to non
            End Select
            posy = 185
            align = 0
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "x", posx, posy, 0)
            cb.EndText()

            If cmbAccidentalPlus.SelectedItem = "Buyer" Or cmbAccidentalPlus.SelectedItem = "Co-Buyer" Or cmbAccidentalPlus.SelectedItem = "Both" Then
                'accidental Plus monthly amount
                posx = 220
                posy = 160
                align = 1
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FormatNumber(FandI.pmt, 2), posx + 30, posy, 0)
                cb.EndText()

                'accidental Plus terms  
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FandI.Term, posx + 180, posy, 0)
                cb.EndText()

                'accidental Plus premium 
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, FormatNumber(FandI.AccidentalPlus, 2), posx + 310, posy, 0)
                cb.EndText()

            End If

            posx = 550
            posy = 116
            align = 2
            Dim tp As Double = 0
            If Not cmblins.SelectedItem = "Declined" Then tp += FandI.lifec
            If Not cmbcins.SelectedItem = "Declined" Then tp += FandI.Criticalc
            If Not cmbains.SelectedItem = "Declined" Then tp += FandI.AandH
            If cmbAccidentalPlus.SelectedItem = "Buyer" Or cmbAccidentalPlus.SelectedItem = "Co-Buyer" Or cmbAccidentalPlus.SelectedItem = "Both" Then tp += FandI.AccidentalPlus

            Dim instotal As Decimal
            instotal = FandI.AandH + FandI.Criticalc + FandI.Criticalr + FandI.lifec + FandI.lifer + FandI.AccidentalPlus
            'txtboxname = "totalpremium"
            'posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            'posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            'align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            'cb.BeginText()
            'cb.SetFontAndSize(bf, size)
            'cb.ShowTextAligned(align, Format(instotal, "Currency"), posx, posy, 0)
            'cb.EndText()

            'txtboxname = "totalcost"
            'posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            'posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            'align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            'cb.BeginText()
            'cb.SetFontAndSize(bf, size)
            'cb.ShowTextAligned(align, Format(instotal, "Currency"), posx, posy, 0)
            'cb.EndText()




            'total premium
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(instotal, "Currency"), posx, posy, 0)
            cb.EndText()

            ''sales tax
            'Dim tax As Double = 0
            'If chkPST.Checked = True Then tax += 0.07
            'If ChkGST.Checked = True Then tax += 0.05
            'cb.BeginText()
            'cb.SetFontAndSize(bf, size)
            'cb.ShowTextAligned(align, Format(instotal * tax, "Currency"), posx, posy - 26, 0)
            'cb.EndText()

            ''total cost
            'cb.BeginText()
            'cb.SetFontAndSize(bf, size)
            'cb.ShowTextAligned(align, Format(instotal + (instotal * tax), "Currency"), posx, posy - 50, 0)
            'cb.EndText()
            'total cost
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(instotal, "Currency"), posx, posy - 50, 0)
            cb.EndText()


            'Dim tempdate As DateTime
            'tempdate = IAPdelDATE.Value
            'tempdate = tempdate.AddYears(CInt(IAPextWarranty.Text))

            'txtboxname = "IAPexpiry"
            'posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            'posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            'align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            'cb.BeginText()
            'cb.SetFontAndSize(bf, size)
            'cb.ShowTextAligned(align, tempdate.ToString("MM        dd        yyyy"), posx, posy, 0)
            'cb.EndText()

            'txtboxname = "IAPoemlength"
            'posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            'posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            'align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            'cb.BeginText()
            'cb.SetFontAndSize(bf, size)
            'cb.ShowTextAligned(align, (CInt(IAPoemWarranty.Text) * 12).ToString, posx, posy, 0)
            'cb.EndText()

            'txtboxname = "IAPlength"
            'posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            'posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            'align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            'cb.BeginText()
            'cb.SetFontAndSize(bf, size)
            'cb.ShowTextAligned(align, (CInt(IAPextWarranty.Text) * 12).ToString, posx, posy, 0)
            'cb.EndText()

            'Dim taxpercent
            'Dim tempx, tempy As Decimal
            'Dim tstr As String
            'tstr = Me.txtsubtotal.Text
            'tstr = Replace(tstr, "$", "")
            'tstr = Replace(tstr, ",", "")
            'tempy = Val(tstr)

            'tstr = Me.txtextwarranty.Text
            'tstr = Replace(tstr, "$", "")
            'tstr = Replace(tstr, ",", "")
            'tempx = Val(tstr)

            'tempy = tempy - tempx
            'taxpercent = 0

            'If ChkGST.Checked = True Then
            '    taxpercent = taxpercent + 0.05
            'End If
            'If chkPST.Checked = True Then
            '    taxpercent = taxpercent + 0.07
            'End If

            'txtboxname = "IAPpurchaseprice"
            'posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            'posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            'align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            'cb.BeginText()
            'cb.SetFontAndSize(bf, size)
            'cb.ShowTextAligned(align, Format(tempy, "N"), posx, posy, 0)
            'cb.EndText()

            'txtboxname = "IAPprice"
            'posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            'posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            'align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            'cb.BeginText()
            'cb.SetFontAndSize(bf, size)
            'cb.ShowTextAligned(align, Format(tempx * 1, "N"), posx, posy, 0)
            'cb.EndText()

            'txtboxname = "IAPhst"
            'posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            'posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            'align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            'cb.BeginText()
            'cb.SetFontAndSize(bf, size)
            'cb.ShowTextAligned(align, Format(tempx * taxpercent, "N"), posx, posy, 0)
            'cb.EndText()

            'txtboxname = "IAPtotal"
            'posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            'posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            'align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            'cb.BeginText()
            'cb.SetFontAndSize(bf, size)
            'cb.ShowTextAligned(align, Format(tempx * (1 + taxpercent), "N"), posx, posy, 0)
            'cb.EndText()

            'size = 12

            'Select Case IAPproduct.SelectedItem
            '    Case "Inboard/Sterndrive/Jet"
            '        If IAPused.Checked = True Then
            '            txtboxname = "IAPibUSED"
            '        Else
            '            txtboxname = "IAPibNEW"
            '        End If
            '    Case "Outboard"
            '        If IAPused.Checked = True Then
            '            txtboxname = "IAPobUSED"
            '        Else
            '            txtboxname = "IAPobNEW"
            '        End If
            '    Case "Stand Alone Outboard"
            '        If IAPused.Checked = True Then
            '            txtboxname = "IAPstandaloneOBused"
            '        Else
            '            txtboxname = "IAPstandaloneOBnew"
            '        End If
            'End Select
            'posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            'posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            'align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            'cb.BeginText()
            'cb.SetFontAndSize(bf, size)
            'cb.ShowTextAligned(align, "x", posx, posy, 0)
            'cb.EndText()

            'If IAPgpr.Checked = True Then
            '    txtboxname = "IAPgpr"
            '    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            '    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            '    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            '    cb.BeginText()
            '    cb.SetFontAndSize(bf, size)
            '    cb.ShowTextAligned(align, "x", posx, posy, 0)
            '    cb.EndText()
            'End If

            'If IAPaftersale.Checked = True Then
            '    txtboxname = "IAPaftersale"
            '    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            '    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            '    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            '    cb.BeginText()
            '    cb.SetFontAndSize(bf, size)
            '    cb.ShowTextAligned(align, "x", posx, posy, 0)
            '    cb.EndText()
            'End If

            'If IAPseals.Checked = True Then
            '    txtboxname = "IAPseals"
            '    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            '    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            '    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            '    cb.BeginText()
            '    cb.SetFontAndSize(bf, size)
            '    cb.ShowTextAligned(align, "x", posx, posy, 0)
            '    cb.EndText()
            'End If


            'Select Case IAPdeduct.SelectedItem.ToString.ToLower
            '    Case "$100"
            '        txtboxname = "IAP100ded"
            '    Case "$50"
            '        txtboxname = "IAP50ded"
            '    Case "$500 (diesel)"
            '        txtboxname = "IAP500ded"
            'End Select

            'posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            'posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            'align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            'cb.BeginText()
            'cb.SetFontAndSize(bf, size)
            'cb.ShowTextAligned(align, "x", posx, posy, 0)
            'cb.EndText()



            'doc.NewPage()
            'Import = writer.GetImportedPage(reader, 2)
            'cb.AddTemplate(Import, 0, 0)
            'doc.NewPage()
            'Import = writer.GetImportedPage(reader, 3)
            'cb.AddTemplate(Import, 0, 0)
            doc.NewPage()
            Import = writer.GetImportedPage(reader, 4)
            cb.AddTemplate(Import, 0, 0)

            size = 13
            align = 1
            posx = 540
            posy = 35
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, IAPcertstr, posx, posy, 0)
            cb.EndText()


            doc.NewPage()
            Import = writer.GetImportedPage(reader, 5)
            cb.AddTemplate(Import, 0, 0)

            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, IAPcertstr, posx, posy, 0)
            cb.EndText()


            doc.NewPage()
            Import = writer.GetImportedPage(reader, 6)
            cb.AddTemplate(Import, 0, 0)

            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, IAPcertstr, posx, posy, 0)
            cb.EndText()


            'doc.NewPage()
            'Import = writer.GetImportedPage(reader, 7)
            'cb.AddTemplate(Import, 0, 0)


            'If Me.IAPgpr.Checked = True Then
            '    inputfile = persistant.installdir + "\resources\Contracts\IAPgpr.pdf"
            '    reader = New PdfReader(inputfile)
            '    doc.NewPage()
            '    Import = writer.GetImportedPage(reader, 1)
            '    cb.AddTemplate(Import, 0, 0)

            '    size = 10

            '    txtboxname = "IAPgprPolicy"
            '    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            '    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            '    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            '    cb.BeginText()
            '    cb.SetFontAndSize(bf, size)
            '    cb.ShowTextAligned(align, IAPcertstr, posx, posy, 0)
            '    cb.EndText()

            '    txtboxname = "IAPgprPurchaseDate"
            '    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            '    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            '    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            '    cb.BeginText()
            '    cb.SetFontAndSize(bf, size)
            '    cb.ShowTextAligned(align, IAPdelDATE.Value.ToString("MMMM dd, yyyy"), posx, posy, 0)
            '    cb.EndText()

            '    txtboxname = "IAPgprOEMDate"
            '    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            '    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            '    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            '    cb.BeginText()
            '    cb.SetFontAndSize(bf, size)
            '    cb.ShowTextAligned(align, IAPdelDATE.Value.ToString("MMMM dd, yyyy"), posx, posy, 0)
            '    cb.EndText()

            '    txtboxname = "IAPgprUnitPurchaseDate"
            '    posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            '    posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            '    align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            '    cb.BeginText()
            '    cb.SetFontAndSize(bf, size)
            '    cb.ShowTextAligned(align, IAPdelDATE.Value.ToString("MMMM dd, yyyy"), posx, posy, 0)
            '    cb.EndText()
            'End If

            doc.Close()
            System.Diagnostics.Process.Start(savelocation)
        Else
            MessageBox.Show("This feature is not yet available for your store")
        End If
    End Sub

    Private Sub btnInsuranceWaiver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsuranceWaiver.Click
        '### DO PRINT
        save()

        Dim posx, posy, align As Integer
        Dim text, txtboxname, datacode As String
        Dim size As Integer
        Dim inputfile, savelocation As String
        datacode = "WAI"
        inputfile = persistant.installdir + "\resources\Contracts\WaiverIns.pdf"
        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-WaiverIns.pdf"

        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        '   Try
        Dim reader As PdfReader = New PdfReader(inputfile)
        Dim doc As Document = New Document(reader.GetPageSize(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
        doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent


        doc.NewPage()
        Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)

        cb.AddTemplate(Import, 0, 0)
        size = 10

        txtboxname = "WAIIdate1"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(DateTime.Now, "dd / MMMM / yyyy"), posx, posy, 0)
        cb.EndText()
        txtboxname = "WAIIdate2"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(DateTime.Now, "dd / MMMM / yyyy"), posx, posy, 0)
        cb.EndText()

        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    Private Sub btnATBprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnATBprint.Click
        '### DO PRINT
        save()

        Dim posx, posy, align As Integer
        Dim text, txtboxname, datacode As String
        Dim size As Integer
        Dim inputfile, savelocation, dealeraddress, dealercity, dealerprov, dealerpostal As String
        Select Case mybosstorecode
            Case "ATL"
                dealeraddress = "8233 - 50 St"
                dealercity = "Edmonton"
                dealerprov = "AB"
                dealerpostal = "T6B 1E7"

            Case "EDM"
                dealeraddress = "8233 - 50 St"
                dealercity = "Edmonton"
                dealerprov = "AB"
                dealerpostal = "T6B 1E7"

            Case "SYL"
                dealeraddress = "38 Industrial Drive "
                dealercity = "Sylvan Lake"
                dealerprov = "AB"
                dealerpostal = "T4S 1P4"

            Case "REN"
                dealeraddress = "804 - 41 Avenue N.E. "
                dealercity = "Calgary"
                dealerprov = "AB"
                dealerpostal = "T2E 3R2"

        End Select
        datacode = "ATB"
        inputfile = persistant.installdir + "\resources\Contracts\ATB.pdf"
        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-ATB.pdf"

        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        '   Try
        Dim reader As PdfReader = New PdfReader(inputfile)
        Dim doc As Document = New Document(reader.GetPageSize(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
        doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent


        doc.NewPage()
        Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)
        cb.AddTemplate(Import, 0, 0)
        size = 8

        txtboxname = "ATBb1lastname"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtbuyer1last.Text, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBb1firstname"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtbuyer1first.Text, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBb1address"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtaddress.Text, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBb1city"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtcity.Text, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBb1prov"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtprov.Text, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBb1postal"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtpostal.Text, posx, posy, 0)
        cb.EndText()

        If txtbuyer2last.Text <> "" Then
            txtboxname = "ATBb2lastname"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtbuyer2last.Text, posx, posy, 0)
            cb.EndText()
            txtboxname = "ATBb2firstname"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtbuyer2first.Text, posx, posy, 0)
            cb.EndText()
            txtboxname = "ATBb2address"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtaddress.Text, posx, posy, 0)
            cb.EndText()
            txtboxname = "ATBb2city"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtcity.Text, posx, posy, 0)
            cb.EndText()
            txtboxname = "ATBb2prov"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtprov.Text, posx, posy, 0)
            cb.EndText()
            txtboxname = "ATBb2postal"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtpostal.Text, posx, posy, 0)
            cb.EndText()
        End If
        txtboxname = "ATBdealername"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, mybosstore, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBdealeraddress"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, dealeraddress, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBdealercity"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, dealercity, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBdealerprov"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, dealerprov, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBdealerpostal"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, dealerpostal, posx, posy, 0)
        cb.EndText()



        size = 9

        txtboxname = "ATBunityear"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtboatyear.Text, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBunitmake"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtboatmake.Text, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBunitmodel"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtboatmodel.Text, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBunithin"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtboathin.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "ATBmotortrailer"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtmotoryear.Text + " " + txtmotormake.Text + " " + txtmotormodel.Text + " S/N: " + txtmotorserial.Text + " AND " + txttraileryear.Text + " " + txttrailermake.Text + " " + txttrailermodel.Text + " S/N: " + txttrailerserial.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "ATBnumpay"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, (FandI.Term - 1).ToString, posx, posy, 0)
        cb.EndText()

        txtboxname = "ATBpayment"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format((FandI.pmt), "Currency"), posx, posy, 0)
        cb.EndText()

        txtboxname = "ATBfreq"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, "Monthly", posx, posy, 0)
        cb.EndText()

        Dim tempdate As DateTime
        tempdate = DateTime.Now
        tempdate = tempdate.AddDays(30)

        txtboxname = "ATBfirstpayment"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(tempdate, "dd/MM/yyyy"), posx, posy, 0)
        cb.EndText()

        txtboxname = "ATBterm"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, FandI.Term.ToString, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBamort"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, FandI.Ammort.ToString, posx, posy, 0)
        cb.EndText()

        tempdate = tempdate.AddMonths(FandI.Term - 1)

        txtboxname = "ATBfinalday"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(tempdate, "dd"), posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBfinalmonth"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(tempdate, "MMMM"), posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBfinalyear"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(tempdate, "yyyy"), posx, posy, 0)
        cb.EndText()

        txtboxname = "ATBresidual"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format((FandI.pmt + FandI.residual).ToString, "Currency"), "$", ""), posx, posy, 0)


        Dim tempx, tempy, tempz, tempcp, tempot As Decimal

        tempcp = 0
        tempcp = tempcp + Val(Replace(Replace(Replace(Replace(txtpackagetotal.Text, "$", ""), ",", ""), ")", ""), "(", "-"))
        tempcp = tempcp + Val(Replace(Replace(Replace(Replace(txtoptionstotal.Text, "$", ""), ",", ""), ")", ""), "(", "-"))
        tempcp = tempcp + Val(Replace(Replace(Replace(Replace(txtgst.Text, "$", ""), ",", ""), ")", ""), "(", "-"))
        tempcp = tempcp + Val(Replace(Replace(Replace(Replace(txtpst.Text, "$", ""), ",", ""), ")", ""), "(", "-"))
        tempcp = tempcp - Val(Replace(Replace(Replace(Replace(txtextwarranty.Text, "$", ""), ",", ""), ")", ""), "(", "-"))

        txtboxname = "ATBcashprice"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format(tempcp, "Currency"), "$", ""), posx, posy, 0)
        cb.EndText()


        tempy = 0
        tempy = tempy + Val(Replace(Replace(Replace(Replace(txttrade.Text, "$", ""), ",", ""), ")", ""), "(", "-"))

        txtboxname = "ATBtradein"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format(tempy, "Currency"), "$", ""), posx, posy, 0)
        cb.EndText()

        tempx = 0
        tempx = tempx + Val(Replace(Replace(Replace(Replace(txttradelein.Text, "$", ""), ",", ""), ")", ""), "(", "-"))

        txtboxname = "ATBtradelien"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format(tempx, "Currency"), "$", ""), posx, posy, 0)
        cb.EndText()

        tempz = tempy - tempx
        txtboxname = "ATBdifference"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format(tempz, "Currency"), "$", ""), posx, posy, 0)
        cb.EndText()


        tempx = 0
        tempx = tempx + Val(Replace(Replace(Replace(Replace(txtp1amount.Text, "$", ""), ",", ""), ")", ""), "(", "-"))
        tempx = tempx + Val(Replace(Replace(Replace(Replace(txtp2amount.Text, "$", ""), ",", ""), ")", ""), "(", "-"))
        tempx = tempx + Val(Replace(Replace(Replace(Replace(txtp3amount.Text, "$", ""), ",", ""), ")", ""), "(", "-"))

        txtboxname = "ATBcashdown"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format(tempx, "Currency"), "$", ""), posx, posy, 0)
        cb.EndText()

        tempy = tempx + tempz
        txtboxname = "ATBtotaldown"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format(tempy, "Currency"), "$", ""), posx, posy, 0)
        cb.EndText()

        tempx = tempcp - tempy
        txtboxname = "ATBbalance"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format(tempx, "Currency"), "$", ""), posx, posy, 0)
        cb.EndText()

        txtboxname = "ATBfees"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format(FandI.Ammort / 12 * 2, "Currency"), "$", ""), posx, posy, 0)
        cb.EndText()

        txtboxname = "ATBlife"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format((FandI.lifec + FandI.lifer), "Currency"), "$", ""), posx, posy, 0)
        cb.EndText()
        If txtextwarranty.Text = "" Then
            txtboxname = "ATBwarr"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Replace(0, "$", ""), posx, posy, 0)
            cb.EndText()
        Else
            txtboxname = "ATBwarr"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Replace(txtextwarranty.Text, "$", ""), posx, posy, 0)
            cb.EndText()
        End If

        txtboxname = "ATBdis"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format((FandI.AandH + FandI.Criticalc + FandI.Criticalr), "Currency"), "$", ""), posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBfinancefee"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(txtfinancefee.Text, "$", ""), posx, posy, 0)
        cb.EndText()

        tempot = 0
        tempot = FandI.AandH + FandI.Criticalc + FandI.Criticalr + FandI.lifec + FandI.lifer
        tempot = tempot + Val(Replace(Replace(Replace(Replace(txtfinancefee.Text, "$", ""), ",", ""), ")", ""), "(", "-"))
        tempot = tempot + Val(Replace(Replace(Replace(Replace(txtextwarranty.Text, "$", ""), ",", ""), ")", ""), "(", "-"))
        txtboxname = "ATBtotal"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format(tempot, "currency"), "$", ""), posx, posy, 0)
        cb.EndText()

        txtboxname = "ATBfinancedammount"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format((tempot + tempx + (FandI.Ammort / 12 * 2)), "currency"), "$", ""), posx, posy, 0)
        cb.EndText()

        If FandI.Term = FandI.Ammort Then
            tempz = (Val(FormatNumber(FandI.pmt, 2)) * FandI.Term) - ((tempot + tempx + (FandI.Ammort / 12 * 2)))
        Else
            tempz = ((Val(FormatNumber(FandI.pmt, 2)) * FandI.Term) + FandI.residual) - ((tempot + tempx + (FandI.Ammort / 12 * 2)))
        End If

        txtboxname = "ATBfinancecharge"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format(tempz, "currency"), "$", ""), posx, posy, 0)
        cb.EndText()

        txtboxname = "ATBrate"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, FormatNumber((100 * FandI.Rate), 2), posx, posy, 0)
        cb.EndText()

        txtboxname = "ATBtimebalance"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format((tempz + tempot + tempx), "currency"), "$", ""), posx, posy, 0)
        cb.EndText()

        If FandI.Term = FandI.Ammort Then
            ' tempz = (FandI.pmt * FandI.Term)
        Else
            '  tempz = ((FandI.pmt * FandI.Term) + FandI.residual)
        End If
        txtboxname = "ATBtotaltimeprice"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format(tempz + tempot + tempx + tempy, "currency"), "$", ""), posx, posy, 0)
        cb.EndText()


        tempz = tempcp + tempot + (FandI.Ammort / 12 * 2)
        txtboxname = "ATBcogs"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Replace(Format(tempz, "currency"), "$", ""), posx, posy, 0)
        cb.EndText()


        txtboxname = "ATBdateday"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(DateTime.Now, "dd"), posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBdatemonth"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(DateTime.Now, "MMMM"), posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBdateyear"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(DateTime.Now, "yyyy"), posx, posy, 0)
        cb.EndText()


        txtboxname = "ATBsellersname"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, mybosstore, posx, posy, 0)
        cb.EndText()
        txtboxname = "ATBdatedbottom"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(DateTime.Now, "MMMM dd, yyyy"), posx, posy, 0)
        cb.EndText()

        doc.NewPage()
        Import = writer.GetImportedPage(reader, 2)
        cb.AddTemplate(Import, 0, 0)

        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    Private Sub btnPrintCAF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintCAF.Click

        '### DO PRINT
        save()

        Dim posx, posy, align As Integer
        Dim text, txtboxname, datacode As String
        Dim size As Integer
        Dim inputfile, savelocation As String
        datacode = "CAF"
        inputfile = persistant.installdir + "\resources\Contracts\CAF.pdf"
        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-CAF.pdf"

        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        '   Try
        Dim reader As PdfReader = New PdfReader(inputfile)
        Dim doc As Document = New Document(reader.GetPageSize(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
        doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent


        doc.NewPage()
        Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)

        cb.AddTemplate(Import, 0, 0)
        size = 8


        'Last name, First Name & Last name, Firstname
        text = txtbuyer1last.Text
        If txtbuyer1first.Text <> "" Then
            text = text + ", " + txtbuyer1first.Text
        End If
        If txtbuyer2last.Text <> "" Then
            text = text + " & " + txtbuyer2last.Text
        End If

        If txtbuyer2first.Text <> "" Then
            If txtbuyer2last.Text = "" Then
                text = text + txtbuyer2first.Text
            Else
                text = text + ", " + txtbuyer2first.Text
            End If
        End If

        txtboxname = "CAFcust"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, text, posx, posy, 0)
        cb.EndText()

        txtboxname = "CAFboat"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtboatmake.Text + " " + txtboatmodel.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "CAFdate"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(DateTime.Now, "MMMM dd, yyyy"), posx, posy, 0)
        cb.EndText()

        txtboxname = "CAFdealer"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, mybosstore, posx, posy, 0)
        cb.EndText()

        txtboxname = "CAFvin"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Me.txtboathin.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "CAFbos"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, mybosnumber.ToString, posx, posy, 0)
        cb.EndText()
        text = persistant.getvalue(persistant.tbl_users, "name", "user = '" + txtsalesman.Text + "'", 0)
        If txtsalesman2.Text <> "" Then
            text = text + " / " + persistant.getvalue(persistant.tbl_users, "name", "user = '" + txtsalesman2.Text + "'", 0)
        End If
        txtboxname = "CAFsalesman"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, text, posx, posy, 0)
        cb.EndText()

        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    Private Sub btnPCLForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPCLForm.Click
        Dim pclform As New FrmPCL
        pclform.ShowDialog()
        If pclform.DialogResult = Windows.Forms.DialogResult.OK Then
            '### DO PRINT
            save()

            Dim posx, posy, align As Integer
            Dim text, txtboxname, datacode As String
            Dim size As Integer
            Dim inputfile, savelocation As String
            datacode = "PCL"
            inputfile = persistant.installdir + "\resources\Contracts\PCL.pdf"
            savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-PCL.pdf"

            Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            Dim reader As PdfReader = New PdfReader(inputfile)
            Dim doc As Document = New Document(reader.GetPageSize(1))
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
            doc.Open()
            Dim cb As PdfContentByte = writer.DirectContent

            doc.NewPage()
            Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)
            cb.AddTemplate(Import, 0, 0)
            size = 10


            'Last name, First Name & Last name, Firstname
            text = txtbuyer1last.Text
            If txtbuyer1first.Text <> "" Then
                text = text + ", " + txtbuyer1first.Text
            End If
            If txtbuyer2last.Text <> "" Then
                text = text + " & " + txtbuyer2last.Text
            End If

            If txtbuyer2first.Text <> "" Then
                If txtbuyer2last.Text = "" Then
                    text = text + txtbuyer2first.Text
                Else
                    text = text + ", " + txtbuyer2first.Text
                End If
            End If

            txtboxname = "PCLname"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()

            txtboxname = "PCLaddress"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtaddress.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "PCLtel"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txthome.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "PCLdob"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtbday1.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "PCLcity"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtcity.Text + " - " + Me.txtprov.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "PCLcountry"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "Canada", posx, posy, 0)
            cb.EndText()

            txtboxname = "PCLpostal"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtpostal.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "PCLdate1"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(DateTime.Now, "yyyy - MM - dd"), posx, posy, 0)
            cb.EndText()

            Select Case pclform.motor
                Case "Sterndrive"
                    txtboxname = "PCLsterndrive"
                Case "Inboard"
                    txtboxname = "PCLinboard"
                Case "Outboard"
                    txtboxname = "PCLoutboard"
                Case "Jet Drive"
                    txtboxname = "PCLjet"
            End Select
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, "X", posx, posy, 0)
            cb.EndText()

            txtboxname = "PCLlength"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_boats, "Length", "Brand = '" + Me.txtboatmake.Text + "' AND Model = '" + Me.txtboatmodel.Text + "'", 0), posx, posy, 0)
            cb.EndText()

            txtboxname = "PCLhin"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Me.txtboathin.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "PCLmaterial"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_boats, "Material", "Brand = '" + Me.txtboatmake.Text + "' AND Model = '" + Me.txtboatmodel.Text + "'", 0), posx, posy, 0)
            cb.EndText()

            txtboxname = "PCLtype"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, pclform.hull, posx, posy, 0)
            cb.EndText()

            txtboxname = "PCLcolor"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtcolor.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "PCLboat"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtboatmake.Text + " " + txtboatmodel.Text, posx, posy, 0)
            cb.EndText()


            txtboxname = "PCLdate2"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(DateTime.Now, "yyyy - MM - dd"), posx, posy, 0)
            cb.EndText()

            doc.Close()
            System.Diagnostics.Process.Start(savelocation)
        End If

    End Sub

    Private Sub btnPCLAuthorization_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPCLAuthorization.Click
        '### DO PRINT
        save()

        Dim posx, posy, align As Integer
        Dim text, txtboxname, datacode As String
        Dim size As Integer
        Dim inputfile, savelocation As String
        datacode = "PCL"
        inputfile = persistant.installdir + "\resources\Contracts\PCLauth.pdf"
        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-PCLauth.pdf"

        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        Dim reader As PdfReader = New PdfReader(inputfile)
        Dim doc As Document = New Document(reader.GetPageSize(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
        doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent

        doc.NewPage()
        Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)
        cb.AddTemplate(Import, 0, 0)
        size = 14


        'Last name, First Name & Last name, Firstname
        text = txtbuyer1last.Text
        If txtbuyer1first.Text <> "" Then
            text = text + ", " + txtbuyer1first.Text
        End If
        If txtbuyer2last.Text <> "" Then
            text = text + " & " + txtbuyer2last.Text
        End If

        If txtbuyer2first.Text <> "" Then
            If txtbuyer2last.Text = "" Then
                text = text + txtbuyer2first.Text
            Else
                text = text + ", " + txtbuyer2first.Text
            End If
        End If

        txtboxname = "PCLauthname"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, text, posx, posy, 0)
        cb.EndText()

        txtboxname = "PCLauthaddress"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Me.txtaddress.Text + ", " + Me.txtcity.Text + ", " + Me.txtprov.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "PCLauthdealer"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, mybosstore, posx, posy, 0)
        cb.EndText()

        txtboxname = "PCLauthboat"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtboatyear.Text + " " + txtboatmake.Text + " " + txtboatmodel.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "PCLauthHIN"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Me.txtboathin.Text, posx, posy, 0)
        cb.EndText()

        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    Private Sub btnTrailerBOS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrailerBOS.Click
        save()

        Dim equip As New equipment
        equip.persistant = persistant
        Dim posx, posy, align As Integer
        Dim text, txtboxname As String
        Dim size As Integer
        Dim inputfile, savelocation As String

        inputfile = ""

        Select Case mybosstorecode
            Case "ATL"
                inputfile = persistant.installdir + "\resources\bosblanks\ATL.pdf"
            Case "KEL"
                inputfile = persistant.installdir + "\resources\bosblanks\KEL.pdf"
            Case "BTH"
                inputfile = persistant.installdir + "\resources\bosblanks\BTH.pdf"
            Case "GAL"
                inputfile = persistant.installdir + "\resources\bosblanks\GAL.pdf"
            Case "OGO"
                inputfile = persistant.installdir + "\resources\bosblanks\OGO.pdf"
            Case "REN"
                inputfile = persistant.installdir + "\resources\bosblanks\REN.pdf"
            Case "EDM"
                inputfile = persistant.installdir + "\resources\bosblanks\EDM.pdf"
            Case "SYL"
                inputfile = persistant.installdir + "\resources\bosblanks\SYL.pdf"
        End Select
        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-TRAILER.pdf"

        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        '   Try
        Dim reader As PdfReader = New PdfReader(inputfile)
        Dim doc As Document = New Document(reader.GetPageSize(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
        doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent
        doc.NewPage()
        Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)
        cb.AddTemplate(Import, 0, 0)

        'Last name, First Name & Last name, Firstname
        text = txtbuyer1last.Text
        If txtbuyer1first.Text <> "" Then
            text = text + ", " + txtbuyer1first.Text
        End If

        If txtbuyer2last.Text <> "" Then
            text = text + " & " + txtbuyer2last.Text
        End If

        If txtbuyer2first.Text <> "" Then
            text = text + ", " + txtbuyer2first.Text
        End If

        txtboxname = "buyersname"
        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()
        End If

        If Me.cmbDealStatus.SelectedItem = "Void" Or Me.cmbDealStatus.SelectedItem = "Void Pending" Then
            cb.BeginText()
            cb.SetFontAndSize(bf, 120)
            cb.ShowTextAligned(1, "VOID", 315, 365, 0)
            cb.EndText()
        End If


        'Salesman
        size = 10
        text = persistant.getvalue(persistant.tbl_users, "name", "user = '" + txtsalesman.Text + "'", 0)
        If txtsalesman2.Text <> "" Then
            size = 6
            text = text + " / " + persistant.getvalue(persistant.tbl_users, "name", "user = '" + txtsalesman2.Text + "'", 0)
        End If
        txtboxname = "salesman"
        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()
        End If

        If Me.txtdatedeled.Text <> "" Then
            cb.BeginText()
            cb.SetFontAndSize(bf, 12)
            cb.ShowTextAligned(2, Me.txtdatedeled.Text, 580, 713, 0)
            cb.EndText()
        End If

        'Personal Data
        For x As Integer = 0 To 12
            txtboxname = mydata(x).txtbox.name
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                size = 12
                If x = 0 Then size = 16
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, mydata(x).txtbox.text, posx, posy, 0)
                cb.EndText()
            End If
        Next


        Dim notes() As String
        notes = txtnotes1.Text.Split(vbNewLine.ToCharArray, StringSplitOptions.RemoveEmptyEntries)

        If notes.Length > 0 Then
            'NOTES
            txtboxname = "notes1"
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                size = 6
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, notes(0), posx, posy, 0)
                cb.EndText()
            End If
        End If

        If notes.Length > 1 Then
            txtboxname = "notes2"
            If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
                posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
                posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
                align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
                size = 6
                cb.BeginText()
                cb.SetFontAndSize(bf, size)
                cb.ShowTextAligned(align, notes(1), posx, posy, 0)
                cb.EndText()
            End If
        End If
        size = 12
        txtboxname = "txtboatmake"
        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txttrailermake.Text, posx, posy, 0)
            cb.EndText()
        End If

        txtboxname = "txtboatmodel"
        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txttrailermodel.Text, posx, posy, 0)
            cb.EndText()
        End If

        txtboxname = "txtboathin"
        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txttrailerserial.Text, posx, posy, 0)
            cb.EndText()
        End If

        txtboxname = "txtboatyear"
        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))

            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txttraileryear.Text, posx, posy, 0)
            cb.EndText()
        End If


        Dim value As Decimal
        value = Val(mydata(72).value) + Val(mydata(73).value) + Val(mydata(74).value)

        txtboxname = "txtboatprice"
        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(value * 0.1, "Currency"), posx, posy, 0)
            cb.EndText()
        End If

        txtboxname = "txtsubtotal"
        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(value * 0.1, "Currency"), posx, posy, 0)
            cb.EndText()
        End If

        txtboxname = "txtgst"
        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(value * 0.1 * GSTRate, "Currency"), posx, posy, 0)
            cb.EndText()
        End If

        txtboxname = "txttotal"
        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(value * 0.1 * (1 + GSTRate), "Currency"), posx, posy, 0)
            cb.EndText()
        End If

        txtboxname = "txtp1amount"
        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(txtp1amount.Text, "Currency"), posx, posy, 0)
            cb.EndText()
        End If
        txtboxname = "txtp1date"
        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtp1date.Text, posx, posy, 0)
            cb.EndText()
        End If
        txtboxname = "txtp1num"
        If persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = '" + txtboxname + "'", 0) = "Text" Then
            posx = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, mybosstorecode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtp1num.Text, posx, posy, 0)
            cb.EndText()
        End If


        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    Private Sub btnShowSoldBy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowSoldBy.Click
        Dim reporter As New frmReport
        reporter.persistant = persistant
        reporter.reporttype = 5
        reporter.bos = Me.mybosnumber
        reporter.bosstore = Me.mybosstorecode
        reporter.Show()
    End Sub

    Private Sub btnBOprofitsheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBOprofitsheet.Click
        Dim posx, posy, align As Integer
        Dim text, txtboxname, twentyHRSoldby, WinterizeSB, WBSoldby As String
        Dim size As Integer
        Dim spifft, totalt, servicet As Decimal
        Dim inputfile, savelocation As String
        Dim imagex As Image
        inputfile = persistant.installdir + "\resources\contracts\BO-Profit.pdf"
        spifft = 0
        totalt = 0

        Select Case mybosstorecode
            Case "ATL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\ATLlogo.PNG")
            Case "KEL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\ATLlogo.PNG")
            Case "BTH"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\BTHlogo.PNG")
            Case "GAL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\GALlogo.PNG")
            Case "OGO"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\OGOlogo.PNG")
            Case "REN"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\RENlogo.PNG")
            Case "EDM"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\EDMlogo.PNG")
            Case "SYL"
                imagex = Image.GetInstance(persistant.installdir + "\resources\images\SYLlogo.PNG")
        End Select
        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\BOS\" + mybosstorecode + "-" + mybosnumber.ToString + "-Profit.pdf"

        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        '   Try
        Dim reader As PdfReader = New PdfReader(inputfile)
        Dim doc As Document = New Document(reader.GetPageSize(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
        doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent
        doc.NewPage()
        Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)
        cb.AddTemplate(Import, 0, 0)
        imagex.ScalePercent(65)
        doc.Add(imagex)
        text = txtbuyer1last.Text
        If txtbuyer1first.Text <> "" Then
            text = text + ", " + txtbuyer1first.Text
        End If

        If txtbuyer2last.Text <> "" Then
            text = text + " & " + txtbuyer2last.Text
        End If

        If txtbuyer2first.Text <> "" Then
            text = text + ", " + txtbuyer2first.Text
        End If
        txtboxname = "BoprofitNAME"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        size = 10
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, text, posx, posy, 0)
        cb.EndText()
        txtboxname = "BoprofitBOS"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        size = 10
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, mybosnumber.ToString, posx, posy, 0)
        cb.EndText()

        text = persistant.getvalue(persistant.tbl_users, "name", "user = '" + txtsalesman.Text + "'", 0)
        If txtsalesman2.Text <> "" Then
            text = text + " / " + persistant.getvalue(persistant.tbl_users, "name", "user = '" + txtsalesman2.Text + "'", 0)
        End If

        txtboxname = "BoprofitSalesman"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        size = 10
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, text.ToString, posx, posy, 0)
        cb.EndText()

        text = ""
        If txtbizman.Text <> "" Then
            text = persistant.getvalue(persistant.tbl_users, "name", "user = '" + txtbizman.Text + "'", 0)
        End If
        txtboxname = "BoprofitBM"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        size = 10
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, text, posx, posy, 0)
        cb.EndText()

        'Reserve
        If txtreserve.Text <> "" Then
            txtboxname = "BoprofitRESprofit"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(CDec(txtreserve.Text), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitREScomm"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format((CDec(txtreserve.Text) * 0.21), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitREStotal"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format((CDec(txtreserve.Text) * 0.21), "Currency"), posx, posy, 0)
            cb.EndText()
            totalt = totalt + (CDec(txtreserve.Text) * 0.21)
        End If
        If txtfinancefee.Text <> "" Then
            'Fee
            txtboxname = "BoprofitFEEsold"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format((CDec(txtfinancefee.Text)), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitFEEcomm"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format((CDec(txtfinancefee.Text) * 0.21), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitFEEspiff"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If CDec(txtfinancefee.Text) > 0 Then
                cb.ShowTextAligned(align, Format((50), "Currency"), posx, posy, 0)
                spifft = spifft + 50
            Else
                cb.ShowTextAligned(align, Format(0, "Currency"), posx, posy, 0)
            End If
            cb.EndText()
            txtboxname = "BoprofitFEEtotal"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If CDec(txtfinancefee.Text) > 0 Then
                cb.ShowTextAligned(align, Format(((CDec(txtfinancefee.Text) * 0.21) - 50), "Currency"), posx, posy, 0)
                totalt = totalt + ((CDec(txtfinancefee.Text) * 0.21) - 50)
            Else
                cb.ShowTextAligned(align, Format(0, "Currency"), posx, posy, 0)
            End If
            cb.EndText()
        End If
        If txtwarrcost.Text <> "" And txtextwarranty.Text <> "" Then
            'Warranty
            txtboxname = "BoprofitWARsold"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format((CDec(txtextwarranty.Text)), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitWARcost"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format((CDec(txtwarrcost.Text)), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitWARprofit"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format((CDec(txtextwarranty.Text) - CDec(txtwarrcost.Text)), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitWARcomm"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(((CDec(txtextwarranty.Text) - CDec(txtwarrcost.Text)) * 0.21), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitWARspiff"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If (CDec(txtextwarranty.Text) - CDec(txtwarrcost.Text)) > 0 Then
                If CDec(txtextwarranty.Text) < 1700 Then
                    cb.ShowTextAligned(align, Format(60, "Currency"), posx, posy, 0)
                    spifft = spifft + 60
                End If
                If CDec(txtextwarranty.Text) >= 1700 And CDec(txtextwarranty.Text) < 2200 Then
                    cb.ShowTextAligned(align, Format(84, "Currency"), posx, posy, 0)
                    spifft = spifft + 84
                End If
                If CDec(txtextwarranty.Text) >= 2200 Then
                    cb.ShowTextAligned(align, Format(100, "Currency"), posx, posy, 0)
                    spifft = spifft + 100
                End If
            Else
                cb.ShowTextAligned(align, Format(0, "Currency"), posx, posy, 0)
            End If
            cb.EndText()
            txtboxname = "BoprofitWARtotal"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If (CDec(txtextwarranty.Text) - CDec(txtwarrcost.Text)) > 0 Then
                If CDec(txtextwarranty.Text) < 1700 Then
                    cb.ShowTextAligned(align, Format(((CDec(txtextwarranty.Text) - CDec(txtwarrcost.Text)) * 0.21 - 60), "Currency"), posx, posy, 0)
                    totalt = totalt + ((CDec(txtextwarranty.Text) - CDec(txtwarrcost.Text)) * 0.21 - 60)
                End If
                If CDec(txtextwarranty.Text) >= 1700 And CDec(txtextwarranty.Text) < 2200 Then
                    cb.ShowTextAligned(align, Format(((CDec(txtextwarranty.Text) - CDec(txtwarrcost.Text)) * 0.21 - 84), "Currency"), posx, posy, 0)
                    totalt = totalt + ((CDec(txtextwarranty.Text) - CDec(txtwarrcost.Text)) * 0.21 - 84)
                End If
                If CDec(txtextwarranty.Text) >= 2200 Then
                    cb.ShowTextAligned(align, Format(((CDec(txtextwarranty.Text) - CDec(txtwarrcost.Text)) * 0.21 - 100), "Currency"), posx, posy, 0)
                    totalt = totalt + ((CDec(txtextwarranty.Text) - CDec(txtwarrcost.Text)) * 0.21 - 100)
                End If
            Else
                cb.ShowTextAligned(align, Format(0, "Currency"), posx, posy, 0)
            End If
            cb.EndText()
        End If
        If txtprotech.Text <> "" And txtPTcost.Text <> "" Then
            'PT
            txtboxname = "BoprofitPTsold"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format((CDec(txtprotech.Text)), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitPTcost"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format((CDec(txtPTcost.Text)), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitPTprofit"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format((CDec(txtprotech.Text) - CDec(txtPTcost.Text)), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitPTcomm"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(((CDec(txtprotech.Text) - CDec(txtPTcost.Text)) * 0.21), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitPTspiff"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If (CDec(txtprotech.Text) - CDec(txtPTcost.Text)) > 0 Then
                cb.ShowTextAligned(align, Format(50, "Currency"), posx, posy, 0)
                spifft = spifft + 50
            Else
                cb.ShowTextAligned(align, Format(0, "Currency"), posx, posy, 0)
            End If
            cb.EndText()
            txtboxname = "BoprofitPTtotal"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If (CDec(txtprotech.Text) - CDec(txtPTcost.Text)) > 0 Then
                cb.ShowTextAligned(align, Format((((CDec(txtprotech.Text) - CDec(txtPTcost.Text)) * 0.21) - 50), "Currency"), posx, posy, 0)
                totalt = totalt + (((CDec(txtprotech.Text) - CDec(txtPTcost.Text)) * 0.21) - 50)
            Else
                cb.ShowTextAligned(align, Format(0, "Currency"), posx, posy, 0)
            End If
            cb.EndText()
        End If
        If txtantitheftreg.Text <> "" Then
            'AT
            txtboxname = "BoprofitETCHsold"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format((CDec(txtantitheftreg.Text)), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitETCHcost"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If CDec(txtantitheftreg.Text) > 0 Then
                cb.ShowTextAligned(align, Format(38, "Currency"), posx, posy, 0)
            Else
                cb.ShowTextAligned(align, Format(0, "Currency"), posx, posy, 0)
            End If
            cb.EndText()
            txtboxname = "BoprofitETCHprofit"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If CDec(txtantitheftreg.Text) > 0 Then
                cb.ShowTextAligned(align, Format((CDec(txtantitheftreg.Text) - 38), "Currency"), posx, posy, 0)
            Else
                cb.ShowTextAligned(align, Format(0, "Currency"), posx, posy, 0)
            End If
            cb.EndText()
            txtboxname = "BoprofitETCHcomm"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If CDec(txtantitheftreg.Text) > 0 Then
                cb.ShowTextAligned(align, Format(((CDec(txtantitheftreg.Text) - 38) * 0.21), "Currency"), posx, posy, 0)
            Else
                cb.ShowTextAligned(align, Format(0, "Currency"), posx, posy, 0)
            End If
            cb.EndText()
            txtboxname = "BoprofitETCHspiff"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If CDec(txtantitheftreg.Text) > 0 Then
                cb.ShowTextAligned(align, Format(17, "Currency"), posx, posy, 0)
                spifft = spifft + 17
            Else
                cb.ShowTextAligned(align, Format(0, "Currency"), posx, posy, 0)
            End If
            cb.EndText()
            txtboxname = "BoprofitETCHtotal"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            If CDec(txtantitheftreg.Text) > 0 Then
                cb.ShowTextAligned(align, Format((((CDec(txtantitheftreg.Text) - 38) * 0.21) - 17), "Currency"), posx, posy, 0)
                totalt = totalt + (((CDec(txtantitheftreg.Text) - 38) * 0.21) - 17)
            Else
                cb.ShowTextAligned(align, Format(0, "Currency"), posx, posy, 0)
            End If
            cb.EndText()
        End If
        If txtinssold.Text <> "" Then
            'INS
            txtboxname = "BoprofitINSsold"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format((CDec(txtinssold.Text)), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitINSprofit"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format((CDec(txtinssold.Text) / 2), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitINScomm"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(((CDec(txtinssold.Text) / 2) * 0.21), "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitINStotal"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(((CDec(txtinssold.Text) / 2) * 0.21), "Currency"), posx, posy, 0)
            totalt = totalt + ((CDec(txtinssold.Text) / 2) * 0.21)
            cb.EndText()
        End If

        'SERVICE
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "SELECT Winterize_SB from bos Where BOS_Number = '" + mybosnumber.ToString + "' AND Store = '" + mybosstorecode + "'"
            WBSoldby = cmd.ExecuteScalar.ToString
            cmd.CommandText = "SELECT 20_Hour_SB from bos Where BOS_Number = '" + mybosnumber.ToString + "' AND Store = '" + mybosstorecode + "'"
            twentyHRSoldby = cmd.ExecuteScalar.ToString
            conn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
        conn.Dispose()
        servicet = 0
        If txtwinterize.Text <> "" Then
            If WBSoldby = "Duncan" Or WBSoldby = "admin" Or WBSoldby = "Nikky" Or WBSoldby = "Wallee" Then
                servicet = servicet + CDec(txtwinterize.Text)
            End If
        End If
        If txt20hr.Text <> "" Then
            If twentyHRSoldby = "Duncan" Or twentyHRSoldby = "admin" Or twentyHRSoldby = "Nikky" Or twentyHRSoldby = "Wallee" Then
                servicet = servicet + CDec(txt20hr.Text)
            End If
        End If
        If servicet > 0 Then
            txtboxname = "BoprofitSERsold"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(servicet, "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitSERcomm"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(servicet * 0.056, "Currency"), posx, posy, 0)
            cb.EndText()
            txtboxname = "BoprofitSERtotal"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
            size = 12
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(servicet * 0.056, "Currency"), posx, posy, 0)
            cb.EndText()
            totalt = totalt + servicet * 0.056
        End If

        'TOTAL
        txtboxname = "BoprofitTOTspiff"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        size = 12
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(spifft, "Currency"), posx, posy, 0)
        cb.EndText()
        txtboxname = "BoprofitTOTtotal"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "BOprofitY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        size = 12
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(totalt, "Currency"), posx, posy, 0)
        cb.EndText()

        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        save()
    End Sub

    Private Sub btnUnlockBOS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnlockBOS.Click
        'If lockedmode = True And mydata(2).txtbox.text <> "" Then
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "UPDATE bos set locked = 0 where bos_number = '" + mybosnumber.ToString + "' and Store = '" + mybosstorecode + "'"
            cmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
        conn.Dispose()
        'End If
    End Sub

#End Region 'Side Menu Buttons

#Region "Purchase Info Tab"


    Private Sub btnPickABoat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPickABoat.Click

        Dim inv As New frmInv
        inv.persistant = persistant
        inv.selectingboat = True
        inv.mybos = Me
        inv.Show()

    End Sub

    Private Sub btnReleaseBoat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReleaseBoat.Click
        boatdropped_killWO()

        If myboat = 2 And ordernumber > 0 Then
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            cmd.CommandText = "UPDATE orders Set status = 'Canceled' WHERE ordernumber = '" + ordernumber.ToString + "'"
            Try
                conn.Open()
                cmd.Connection = conn
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()
            ordernumber = 0
            myboat = 0

        End If
        If myboat > 2 Then
            oldboat = myboat
            myboat = 0
        End If
        For x As Integer = 0 To mydatacount
            If mydata(x).dbtable = "inventory" Then
                mydata(x).txtbox.text = ""
                mydata(x).value = ""
                Me.stringboxchanged(mydata(x).txtbox, e)
            End If
        Next
        save()
        downloaddata()
        Me.txtdriveserial.Text = ""
        Me.txtdrivemodel.Text = ""

        Me.stringboxchanged(Me.txtdriveserial, e)
        Me.stringboxchanged(Me.txtdrivemodel, e)

        lockedmode = False
        WritetoScreen()

    End Sub

    Private Sub btnOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrder.Click
        If Me.txtbuyer1last.Text = "" Then
            MessageBox.Show("You must enter your customers last name before requesting an ordered boat.")
        Else
            save()
            If Me.cmbDealStatus.SelectedItem = "Quote" Then
                MessageBox.Show("You must have a deposit before you can order a boat.")
            Else
                Dim temp As Integer
                order.persistant = persistant
                temp = order.Startorder(mybosnumber, mybosstorecode, Me.txtsalesman.Text, Me.txtbuyer1last.Text)
                order.ShowDialog()
                If order.cancled = False Then
                    Me.downloaddata()
                    lockedmode = False
                    Me.WritetoScreen()
                End If
            End If
        End If

    End Sub

    Private Sub ChkGST_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPST.CheckedChanged, ChkGST.CheckedChanged
        If loaded = True Then

            updatemath()

        End If
    End Sub

    Private Sub btndonecomm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDoneComm.Click
        cmbCommissioned.SelectedItem = "YES"
        Me.Close()

    End Sub

    Private Sub chkboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkwinterize.CheckedChanged, chkstire.CheckedChanged, chksprop.CheckedChanged, chkskipkg.CheckedChanged, chksafepkg.CheckedChanged, chkrockg.CheckedChanged, chkprotech.CheckedChanged, chklockpkg.CheckedChanged, chkextwarranty.CheckedChanged, chkcover.CheckedChanged, chkantitheftreg.CheckedChanged, Chk20hr.CheckedChanged
        chkchanged(sender)
        updatemath()

    End Sub

    Private Sub moneyboxleaveAT(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtantitheftreg.Leave
        If persistant.getvalue(persistant.tbl_location, "Prov", "Code = '" + mybosstorecode + "'", 0) = "BC" Then
            Dim strtemp As String
            strtemp = Me.txtantitheftreg.Text
            strtemp = Replace(strtemp, "$", "")
            strtemp = Replace(strtemp, ",", "")
            If Val(strtemp) > 0 Then
                Me.txtantitheftreg.Text = "$399"
            Else
                Me.txtantitheftreg.Text = ""
            End If
        End If
        moneychanged(sender)
        updatemath()
    End Sub

    Private Sub moneyboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPTcost.Leave, txtreserve.Leave, txtsmallitemcom.Leave, txtBOcommadjust.Leave, txtcommchange1.Leave, txtcommchange2.Leave, txtMiscComm.Leave, txtinssold.Leave, txttradelein.Leave, txtwinterize.Leave, txttrailerprice.Leave, txttrade.Leave, txttplateprice.Leave, txttiretax.Leave, txtstireprice.Leave, txtspropprice.Leave, txtskipkgprice.Leave, txtsafepkgprice.Leave, txtrockgprice.Leave, txtprotech.Leave, txtp3amount.Leave, txtp2amount.Leave, txtp1amount.Leave, txtDepAmount.Leave, txto8price.Leave, txto7price.Leave, txto6price.Leave, txto5price.Leave, txto4price.Leave, txto3price.Leave, txto2price.Leave, txto1price.Leave, txtmotorprice.Leave, txtlockpkgprice.Leave, txtkickerprice.Leave, txtextwarranty.Leave, txtdriveprice.Leave, txtdiscountprice.Leave, txtcoverprice.Leave, txtboatprice.Leave, txtadminfee.Leave, txt20hr.Leave, txtfinancefee.Leave, txtwarrcost.Leave
        moneychanged(sender)
        If sender Is Me.txtfinancefee Then lblFFee.Text = txtfinancefee.Text
        updatemath()
    End Sub

    Private Sub decimalboxleave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcommrate1.Leave, txtcommrate2.Leave, txtcommrate3.Leave, txtcommrate4.Leave, txtcommrate5.Leave, txtcommrate6.Leave, txtcommrate7.Leave, txtcommrate8.Leave
        decimalchanged(sender)
        updatemath()

    End Sub

    Private Sub txttires_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttires.Leave, txtbatteries.Leave
        decimalchanged(sender)
        updatemath()

    End Sub


#End Region 'Purchase Info Tab

#Region "Additional Info Tab"


    Private Sub btnChangeSalesman_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeSalesman.Click
        cansave = True


        Dim pick As New frmSalesman
        pick.persistant = persistant
        pick.filter = "User <> 'root'"
        pick.ShowDialog()
        If persistant.temppass <> "None" Then
            Me.txtsalesman.Text = persistant.getvalue(persistant.tbl_users, "user", "name = '" + persistant.temppass + "'", 0)
        Else
            Me.txtsalesman.Text = ""
        End If
        stringchanged(Me.txtsalesman)
    End Sub

    Private Sub btnSplitDeal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSplitDeal.Click
        cansave = True


        Dim pick As New frmSalesman
        pick.persistant = persistant
        pick.filter = "User <> 'root'"
        pick.ShowDialog()
        If persistant.temppass <> "None" Then
            Me.txtsalesman2.Text = persistant.getvalue(persistant.tbl_users, "user", "name = '" + persistant.temppass + "'", 0)
        Else
            Me.txtsalesman2.Text = ""
        End If
        stringchanged(Me.txtsalesman2)
    End Sub

    Private Sub cmbDealStatus_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDealStatus.SelectedIndexChanged
        'If persistant.myuserLEVEL < 7 Then
        If persistant.myuserLEVEL > 8 Or persistant.mystore = "Admin" Then
            If cmbDealStatus.SelectedItem = "Quote" Then
                btnReleaseBoat.Visible = True
            Else
                btnReleaseBoat.Visible = False
            End If
        End If

    End Sub

    Private Sub btnCreateWO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateWO.Click
        If myboat = 2 Then
            MessageBox.Show("You can't create a work order for a boat that is still on order.  If the boat has arrived please contact managment")
        Else
            If txtwonumber.Text = "0" Or txtwonumber.Text = "" Then
                'NEW
                Dim CustomerNumber As Integer
                'Dim Customerdata As New CustData
                setuparray2()
                CustomerNumber = savecust()
                Dim newWO As New FrmWO
                newWO.persistant = Me.persistant
                newWO.newWOschDate = Date.Now
                newWO.Store = mybosstorecode
                newWO.BOSnumber = mybosnumber
                newWO.WONumber = 0
                newWO.CustomerNumber = CustomerNumber
                newWO.ShowDialog()
                txtwonumber.Text = newWO.WONumber.ToString
                stringchanged(txtwonumber)
                save()
                WritetoScreen()
            Else
                'Load existing
                Dim newWO As New FrmWO
                newWO.persistant = Me.persistant
                newWO.Store = mybosstorecode
                newWO.WONumber = CInt(txtwonumber.Text)
                newWO.ShowDialog()
                WritetoScreen()
            End If
        End If

    End Sub

    Private Sub btnVoid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoid.Click
        If cmbDealStatus.SelectedItem <> "Quote" Then
            boatdropped_killWO()

            Me.cmbDealStatus.SelectedItem = "Void Pending"
            save()
        Else
            MessageBox.Show("Sorry, you can't void a quote")
        End If

    End Sub

    Private Sub DateTimeWO_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimeWO.Leave
        For i As Integer = 0 To (mydata.Length - 1)
            If sender.Equals(mydata(i).txtbox) = True Then
                mydata(i).value = sender.value.ToString
                mydata(i).changed = True
            End If
        Next
    End Sub

    Private Sub btnDelivered_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelivered.Click
        Me.cmbDealStatus.SelectedItem = "Delivered"
        If MessageBox.Show("Was this boat financed?", "Financed", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            'YES
            Me.rcash.Checked = False
            Me.rfin.Checked = True
            If MessageBox.Show("What the financing insured?", "Insured", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                'INSURED
                Me.rins.Checked = True
                Me.runins.Checked = False
                Me.txthiddenfincode.Text = "3"
            Else
                'NOT INSURED
                Me.rins.Checked = False
                Me.runins.Checked = True
                Me.txthiddenfincode.Text = "2"
                Me.txtinssold.Text = "0.001"
                moneychanged(txtinssold)
            End If
        Else
            'CASH SALE
            Me.rcash.Checked = True
            Me.rfin.Checked = False
            Me.rins.Visible = False
            Me.runins.Visible = False
            Me.txtinssold.Visible = False
            Me.txthiddenfincode.Text = "1"
            Me.cmbFinanceStatus.SelectedItem = "Cash"
            Me.txtinssold.Text = "0.001"
            moneychanged(txtinssold)
            Me.txtfinancefee.Text = "0.001"
            moneychanged(txtfinancefee)
        End If
        Me.txtdatedeled.Text = DateTime.Now.ToShortDateString
    End Sub



#End Region ' Additional Info Tab

#Region "Finance Tab"


    Private Sub btnBizmanChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBizmanChange.Click
        Dim pick As New frmSalesman
        pick.persistant = persistant
        pick.filter = "usergroup IN ('4', '6', '7')"
        pick.ShowDialog()
        If persistant.temppass <> "None" Then
            Me.txtbizman.Text = persistant.getvalue(persistant.tbl_users, "user", "name = '" + persistant.temppass + "'", 0)
        Else
            Me.txtbizman.Text = ""
        End If
        stringchanged(Me.txtbizman)
    End Sub

    Private Sub btnLICopyToClip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLICopyToClip.Click
        Clipboard.SetText(Replace(FormatNumber((FandI.lifer + FandI.lifec), 2), ",", ""))
    End Sub

    Private Sub btnCICopyToClip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCICopyToClip.Click
        Clipboard.SetText(Replace(FormatNumber((FandI.Criticalc + FandI.Criticalr), 2), ",", ""))
    End Sub

    Private Sub btnASCopyToClip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnASCopyToClip.Click
        Clipboard.SetText(Replace(FormatNumber((FandI.AandH), 2), ",", ""))
    End Sub

    Private Sub btnAPCopyToClip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAPCopyToClip.Click
        Clipboard.SetText(Replace(FormatNumber((FandI.AccidentalPlus), 2), ",", ""))
    End Sub

    Private Sub btnPrintInsRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintInsRequest.Click
        Dim insform As New frmInsurance
        insform.ShowDialog()
        If insform.DialogResult = Windows.Forms.DialogResult.OK Then

            '### DO PRINT
            save()

            Dim posx, posy, align As Integer
            Dim text, txtboxname, datacode As String
            Dim size As Integer
            Dim inputfile, savelocation, dealerphone, dealerfax As String
            datacode = "INS"
            Dim imagex As Image

            Select Case mybosstorecode
                Case "ATL"
                    imagex = Image.GetInstance(persistant.installdir + "\resources\images\ATLlogo.PNG")
                Case "KEL"
                    imagex = Image.GetInstance(persistant.installdir + "\resources\images\ATLlogo.PNG")
                Case "BTH"
                    imagex = Image.GetInstance(persistant.installdir + "\resources\images\BTHlogo.PNG")
                Case "GAL"
                    imagex = Image.GetInstance(persistant.installdir + "\resources\images\GALlogo.PNG")
                Case "OGO"
                    imagex = Image.GetInstance(persistant.installdir + "\resources\images\OGOlogo.PNG")
                Case "REN"
                    imagex = Image.GetInstance(persistant.installdir + "\resources\images\RENlogo.PNG")
                Case "EDM"
                    imagex = Image.GetInstance(persistant.installdir + "\resources\images\EDMlogo.PNG")
                Case "SYL"
                    imagex = Image.GetInstance(persistant.installdir + "\resources\images\SYLlogo.PNG")
            End Select
            Select Case mybosstorecode
                Case "EDM"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
                Case "ATL"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
                Case "SYL"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
                Case "REN"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
                Case "BTH"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
                Case "GAL"
                    dealerphone = "(604) 514-0460"
                    dealerfax = "(604) 514-0470"
                Case "OGO"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
                Case "KEL"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(780) 463-9667"
            End Select

            Select Case txtbizman.Text
                Case "Duncan"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(866) 300-1264"
                Case "Wallee"
                    dealerphone = "(780) 465-5307"
                    dealerfax = "(888) 208-2341"

            End Select

            inputfile = persistant.installdir + "\resources\Contracts\Insurance.pdf"
            savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-InsReq.pdf"

            Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            '   Try
            Dim reader As PdfReader = New PdfReader(inputfile)
            Dim doc As Document = New Document(reader.GetPageSize(1))
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
            doc.Open()
            Dim cb As PdfContentByte = writer.DirectContent


            doc.NewPage()
            Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)

            cb.AddTemplate(Import, 0, 0)
            size = 12
            imagex.ScalePercent(65)
            doc.Add(imagex)

            txtboxname = "INSphone"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, dealerphone, posx, posy, 0)
            cb.EndText()


            txtboxname = "INSfax"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, dealerfax, posx, posy, 0)
            cb.EndText()


            'Last name, First Name & Last name, Firstname
            text = txtbuyer1last.Text
            If txtbuyer1first.Text <> "" Then
                text = text + ", " + txtbuyer1first.Text
            End If
            If txtbuyer2last.Text <> "" Then
                text = text + " & " + txtbuyer2last.Text
            End If

            If txtbuyer2first.Text <> "" Then
                If txtbuyer2last.Text = "" Then
                    text = text + txtbuyer2first.Text
                Else
                    text = text + ", " + txtbuyer2first.Text
                End If
            End If

            txtboxname = "INSclient"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, text, posx, posy, 0)
            cb.EndText()

            txtboxname = "INScomp"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, insform.comp, posx, posy, 0)
            cb.EndText()

            txtboxname = "INSdate"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(DateTime.Now, "MMMM dd, yyyy"), posx, posy, 0)
            cb.EndText()

            txtboxname = "INSph"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, insform.ph, posx, posy, 0)
            cb.EndText()

            txtboxname = "INSfx"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, insform.fx, posx, posy, 0)
            cb.EndText()

            txtboxname = "INSattn"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, insform.attn, posx, posy, 0)
            cb.EndText()

            txtboxname = "INSpgs"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, insform.pgs, posx, posy, 0)
            cb.EndText()

            size = 11
            txtboxname = "INSboat"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtboatmake.Text + " " + txtboatmodel.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "INSmotor"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtmotormake.Text + " " + txtmotormodel.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "INStrailer"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txttrailermake.Text + " " + txttrailermodel.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "INSboatyear"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtboatyear.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "INSmotoryear"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtmotoryear.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "INStraileryear"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txttraileryear.Text, posx, posy, 0)
            cb.EndText()


            txtboxname = "INSboatserial"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtboathin.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "INSmotorserial"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txtmotorserial.Text, posx, posy, 0)
            cb.EndText()

            txtboxname = "INStrailerserial"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, txttrailerserial.Text, posx, posy, 0)
            cb.EndText()

            Dim value As Decimal
            value = Val(mydata(72).value) + Val(mydata(73).value) + Val(mydata(74).value)


            txtboxname = "INSboatval"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(value * 0.35, "Currency"), posx, posy, 0)
            cb.EndText()

            txtboxname = "INSmotorval"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(value * 0.55, "Currency"), posx, posy, 0)
            cb.EndText()

            txtboxname = "INStrailerval"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, Format(value * 0.1, "Currency"), posx, posy, 0)
            cb.EndText()

            txtboxname = "INSlein1"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_banks, "Address1", "Bank = '" + ListBanks.SelectedItem + "'", 0), posx, posy, 0)
            cb.EndText()

            txtboxname = "INSlein2"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_banks, "Address2", "Bank = '" + ListBanks.SelectedItem + "'", 0), posx, posy, 0)
            cb.EndText()
            txtboxname = "INSlein3"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_banks, "Address3", "Bank = '" + ListBanks.SelectedItem + "'", 0), posx, posy, 0)
            cb.EndText()
            txtboxname = "INSlein4"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_banks, "Address4", "Bank = '" + ListBanks.SelectedItem + "'", 0), posx, posy, 0)
            cb.EndText()
            txtboxname = "INSlein5"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_banks, "Address5", "Bank = '" + ListBanks.SelectedItem + "'", 0), posx, posy, 0)
            cb.EndText()



            txtboxname = "INSBOname"
            posx = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "X", "Data = '" + txtboxname + "'", 0))
            posy = Val(persistant.getvalue(persistant.tbl_printdata, datacode + "Y", "Data = '" + txtboxname + "'", 0))
            align = Val(persistant.getvalue(persistant.tbl_printdata, "ALIGN", "Data = '" + txtboxname + "'", 0))
            cb.BeginText()
            cb.SetFontAndSize(bf, size)
            cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_users, "name", "user = '" + txtbizman.Text + "'", 0), posx, posy, 0)
            cb.EndText()


            doc.Close()
            System.Diagnostics.Process.Start(savelocation)
        End If

    End Sub

    Private Sub btnCashPriceCTC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCashPriceCTC.Click
        Clipboard.SetText(Replace(txtbocashprice.Text, ",", ""))
    End Sub

    Private Sub btnWarrantyCTC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWarrantyCTC.Click
        Clipboard.SetText(Replace(txtbowarranty.Text, ",", ""))
    End Sub

    Private Sub btnDownPmtCTC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownPmtCTC.Click
        Clipboard.SetText(Replace(txtbodown.Text, ",", ""))
    End Sub

    Private Sub btnTradeInCTC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTradeInCTC.Click
        Clipboard.SetText(Replace(txtbotrade.Text, ",", ""))
    End Sub

    Private Sub btnTradeLienCTC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTradeLienCTC.Click
        Clipboard.SetText(Replace(txtbolien.Text, ",", ""))
    End Sub

    Private Sub bdaycng(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbday2.Leave, txtbday1.Leave
        Dim temp As String
        temp = saldateformat(sender.Text)
        If temp <> "" Then
            sender.Text = temp
        End If
    End Sub

    Private Sub cmbAccidentalPlus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccidentalPlus.SelectedIndexChanged
        updatemath()
    End Sub

    Private Sub cmbTerm_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbammort.SelectedValueChanged, cmbTerm.SelectedValueChanged
        updatemath()
    End Sub

    Private Sub cmblins_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmblins.SelectedIndexChanged, cmbcins.SelectedIndexChanged, cmbains.SelectedIndexChanged, cmbahtype.SelectedIndexChanged
        updatemath()
    End Sub

    'Private Sub ListBanks_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBanks.SelectedIndexChanged
    '    updaterates()
    'End Sub

    Private Sub stringboxchanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfinnotes.Leave, txtatnum.Leave, txtBOcommadjustnote.Leave, _
        txtchange1note.Leave, txtchange2note.Leave, txtwork.Leave, txtnotes.Leave, txtwotext.Leave, txttraileryear.Leave, txttrailerserial.Leave, txttrailermodel.Leave, _
        txttrailermake.Leave, txttplateyear.Leave, txttplateserial.Leave, txttplatemodel.Leave, txttplatemake.Leave, txtskipkg.Leave, txtprov.Leave, txtpostal.Leave, _
        txtp3num.Leave, txtp2num.Leave, txtp1num.Leave, txtDepNum.Leave, txto8.Leave, txto7.Leave, txto6.Leave, txto5.Leave, txto4.Leave, txto3.Leave, txto2.Leave, _
        txto1.Leave, txtnotes1.Leave, txtmotoryear.Leave, txtmotorserial.Leave, txtmotormodel.Leave, txtmotormake.Leave, txtkickeryear.Leave, txtkickerserial.Leave, _
        txtkickermodel.Leave, txtkickermake.Leave, txthome.Leave, txtemail.Leave, txtdriveyear.Leave, txtdriveserial.Leave, txtdrivemodel.Leave, txtdrivemake.Leave, _
        txtcolor.Leave, txtcity.Leave, txtbuyer2last.Leave, txtbuyer2first.Leave, txtbuyer1last.Leave, txtbuyer1first.Leave, txtboatyear.Leave, txtboatmodel.Leave, _
        txtboatmake.Leave, txtboathin.Leave, txtaddress.Leave, Panel1.Leave


        If Replace(Me.txtbuyer1last.Text, " ", "") = "" Then Me.txtbuyer1last.Text = ""

        If sender.Equals(Me.txtDepNum) And txtDepNum.Text <> mydata(147).value Then
            txtDepDate.Text = Format(DateTime.Now, "dd/MM/yyyy")
            mydata(149).value = Format(DateTime.Now, "yyyy-MM-dd")
            mydata(149).changed = True
        End If
        If sender.Equals(Me.txtp1num) And txtp1num.Text <> mydata(24).value Then
            txtp1date.Text = Format(DateTime.Now, "dd/MM/yyyy")
            mydata(27).value = Format(DateTime.Now, "yyyy-MM-dd")
            mydata(27).changed = True
        End If
        If sender.Equals(Me.txtp2num) And txtp2num.Text <> mydata(25).value Then
            txtp2date.Text = Format(DateTime.Now, "dd/MM/yyyy")
            mydata(28).value = Format(DateTime.Now, "yyyy-MM-dd")
            mydata(28).changed = True
        End If
        If sender.Equals(Me.txtp3num) And txtp3num.Text <> mydata(26).value Then
            txtp3date.Text = Format(DateTime.Now, "dd/MM/yyyy")
            mydata(29).value = Format(DateTime.Now, "yyyy-MM-dd")
            mydata(29).changed = True
        End If

        If txtkickerserial.Text <> "" And txtkickerserial.Text <> "" Then
            txto8.Enabled = False
            txto8price.Enabled = False
        Else
            txto8.Enabled = True
            txto8price.Enabled = True
        End If
        stringchanged(sender)
    End Sub

    Private Sub txtBankFee_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBankFee.Leave
        Dim x As String
        Dim y As Decimal
        x = Replace(sender.text, "$", "")
        x = Replace(x, ",", "")
        y = Val(x)

        If y <> 0 Then
            txtBankFee.Text = Format(x, "currency")
            FandI.bankfeeoverride = y
        End If

        If y = 0 Then
            txtBankFee.Text = ""
            FandI.bankfeeoverride = 0
        End If
        updatemath()
    End Sub

    Private Sub Mustreg(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbPTfabric.SelectedValueChanged, CmbPTrtb.SelectedValueChanged
        If CmbPTfabric.SelectedItem = "YES" Or CmbPTrtb.SelectedItem = "YES" Then
            CmbPTreg.SelectedItem = "YES"
            CmbPTinstalled.SelectedItem = "YES"
        End If
    End Sub

    Private Sub CmbPTinstalled_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbPTinstalled.SelectedValueChanged
        If CmbPTinstalled.SelectedItem = "NO" Then
            CmbPTreg.SelectedItem = "NO"
            CmbPTrtb.SelectedItem = "NO"
            CmbPTfabric.SelectedItem = "NO"
        End If
    End Sub

    Private Sub txtrateover_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrateover.Leave, txtammover.Leave
        updatemath()
    End Sub



#End Region ' Finance Tab

#Region "Warranty Tab"


    Private Sub btnPrintReminder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintReminder.Click
        '### DO PRINT
        save()

        Dim posx, posy, align As Integer
        Dim text, txtboxname, datacode As String
        Dim size As Integer
        Dim inputfile, savelocation, dealerphone, dealerfax As String

        inputfile = persistant.installdir + "\resources\Contracts\CSreminder.pdf"
        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Bos\" + mybosstorecode + "-" + mybosnumber.ToString + "-" + Replace(Replace(txtbuyer1last.Text, "\", ""), "/", "") + "-CSreminder.pdf"

        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        Dim reader As PdfReader = New PdfReader(inputfile)
        Dim doc As Document = New Document(reader.GetPageSize(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
        doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent


        doc.NewPage()
        Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)
        cb.AddTemplate(Import, 0, 0)
        size = 45
        Dim tempdate As DateTime
        tempdate = DateWarrantystart.Value
        tempdate = tempdate.AddYears(CInt(CSextwarr.Text))

        posx = 310
        posy = 300
        align = 1
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Format(tempdate, "MMMM dd, yyyy"), posx, posy, 0)
        cb.EndText()

        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    Private Sub BtnSylWarr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSylWarr.Click
        Dim posx, posy, align As Integer
        Dim text, txtboxname, storeaddr As String
        Dim size As Integer

        Dim inputfile, savelocation As String

        inputfile = persistant.installdir + "\resources\contracts\SylvanWARR.pdf"

        Select Case mybosstorecode
            Case "ATL"
                storeaddr = "8233 - 50 St,  Edmonton,  AB,  T6B 1E7"
            Case "EDM"
                storeaddr = "8233 - 50 St,  Edmonton,  AB,  T6B 1E7"
            Case "BTH"
                storeaddr = "PO Box 568,  Salmon Arm,  BC,  V1E 4N7"
            Case "GAL"
                storeaddr = "20247 Langley Bypass,  Langley,  BC,  V3A 6K9"
            Case "OGO"
                storeaddr = "3306 HWY 97N,  Kelowna,  BC,  V1X 5C2"
            Case "KEL"
                storeaddr = "3306 HWY 97N,  Kelowna,  BC,  V1X 5C2"
            Case "REN"
                storeaddr = "804 - 41 Avenue N.E.,  Calgary,  AB,  T2E 3R2"
            Case "SYL"
                storeaddr = "38 Industrial Drive,  Sylvan Lake,  AB,  T4S 1P4"
        End Select

        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\BOS\" + mybosstorecode + "-" + mybosnumber.ToString + "-SYLWarr.pdf"

        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        '   Try
        Dim reader As PdfReader = New PdfReader(inputfile)
        Dim doc As Document = New Document(reader.GetPageSize(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
        doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent
        doc.NewPage()
        Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)
        cb.AddTemplate(Import, 0, 0)
        size = 12

        txtboxname = "WARsylModel"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtboatmake.Text + " " + txtboatmodel.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARsylHIN"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtboathin.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARsylDate"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtdatedeled.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARsylDealer"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, Me.mybosstore, posx, posy, 0)
        cb.EndText()


        txtboxname = "WARsylDealerAddress"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, storeaddr, posx, posy, 0)
        cb.EndText()

        text = txtbuyer1last.Text
        If txtbuyer1first.Text <> "" Then
            text = text + ", " + txtbuyer1first.Text
        End If

        If txtbuyer2last.Text <> "" Then
            text = text + " & " + txtbuyer2last.Text
        End If

        If txtbuyer2first.Text <> "" Then
            text = text + ", " + txtbuyer2first.Text
        End If
        txtboxname = "WARsylCustomer"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARsylCustomerAddress"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtaddress.Text + ", " + txtcity.Text + ", " + txtprov.Text + ", " + txtpostal.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARsylHP"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARsylY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, persistant.getvalue(persistant.tbl_motors, "HP", "brand = '" + Me.txtmotormake.Text + "' AND model = '" + Me.txtmotormodel.Text + "'", 0), posx, posy, 0)
        cb.EndText()

        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    Private Sub btnShorelandWarr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShorelandWarr.Click
        Dim posx, posy, align As Integer
        Dim text, txtboxname, storeaddr, storecity, storeprov, storepostal As String
        Dim size As Integer

        Dim inputfile, savelocation As String

        inputfile = persistant.installdir + "\resources\contracts\Shorelandr.pdf"

        Select Case mybosstorecode
            Case "ATL"
                storeaddr = "8233 - 50 St"
                storecity = "Edmonton"
                storeprov = "AB"
                storepostal = "T6B 1E7"
            Case "EDM"
                storeaddr = "8233 - 50 St"
                storecity = "Edmonton"
                storeprov = "AB"
                storepostal = "T6B 1E7"
            Case "BTH"
                storeaddr = "PO Box 568"
                storecity = "Salmon Arm"
                storeprov = "BC"
                storepostal = "V1E 4N7"
            Case "GAL"
                storeaddr = "20247 Langley Bypass"
                storecity = "Langley"
                storeprov = "BC"
                storepostal = "V3A 6K9"
            Case "OGO"
                storeaddr = "3306 HWY 97N"
                storecity = "Kelowna"
                storeprov = "BC"
                storepostal = "V1X 5C2"

            Case "KEL"
                storeaddr = "3306 HWY 97N"
                storecity = "Kelowna"
                storeprov = "BC"
                storepostal = "V1X 5C2"
            Case "REN"
                storeaddr = "804 - 41 Avenue N.E."
                storecity = "Calgary"
                storeprov = "AB"
                storepostal = "T2E 3R2"

            Case "SYL"
                storeaddr = "38 Industrial Drive"
                storecity = "Sylvan Lake"
                storeprov = "AB"
                storepostal = "T4S 1P4"
        End Select

        savelocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\BOS\" + mybosstorecode + "-" + mybosnumber.ToString + "-ShoreWarr.pdf"

        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        '   Try
        Dim reader As PdfReader = New PdfReader(inputfile)
        Dim doc As Document = New Document(reader.GetPageSize(1))
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savelocation, FileMode.Create))
        doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent
        doc.NewPage()
        Dim Import As PdfImportedPage = writer.GetImportedPage(reader, 1)
        cb.AddTemplate(Import, 0, 0)
        size = 12

        text = txtbuyer1last.Text
        If txtbuyer1first.Text <> "" Then
            text = text + ", " + txtbuyer1first.Text
        End If

        If txtbuyer2last.Text <> "" Then
            text = text + " & " + txtbuyer2last.Text
        End If

        If txtbuyer2first.Text <> "" Then
            text = text + ", " + txtbuyer2first.Text
        End If

        txtboxname = "WARshoreOwner"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreOwnerAddress"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtaddress.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreOwnerCity"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtcity.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreOwnerCountry"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, "Canada", posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreOwnerProv"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtprov.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreOwnerPostal"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtpostal.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreVIN"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txttrailerserial.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreModel"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txttrailermodel.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreDate"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, txtdatedeled.Text, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreDealer"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, mybosstore, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreDealerAddress"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, storeaddr, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreDealerCity"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, storecity, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreDealerCountry"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, "Canada", posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreDealerProv"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, storeprov, posx, posy, 0)
        cb.EndText()

        txtboxname = "WARshoreDealerPostal"
        posx = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrX", "Data = '" + txtboxname + "'", 0))
        posy = Val(persistant.getvalue(persistant.tbl_printdata, "WARshorelandrY", "Data = '" + txtboxname + "'", 0))
        align = Val(persistant.getvalue(persistant.tbl_printdata, "Align", "Data = '" + txtboxname + "'", 0))
        cb.BeginText()
        cb.SetFontAndSize(bf, size)
        cb.ShowTextAligned(align, storepostal, posx, posy, 0)
        cb.EndText()

        doc.Close()
        System.Diagnostics.Process.Start(savelocation)
    End Sub

    Private Sub cmbcsfuel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcsfuel.SelectedIndexChanged
        If cmbcsfuel.SelectedItem.ToString.ToLower = "diesel" Then
            cmbcsdeduct.SelectedItem = "$500 (Diesel)"
        Else
            cmbcsdeduct.SelectedItem = "$100"
        End If
    End Sub

    Private Sub IAPfuel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IAPfuel.SelectedIndexChanged
        If IAPfuel.SelectedItem.ToString.ToLower = "diesel" Then
            IAPdeduct.SelectedItem = "$500 (Diesel)"
        Else
            IAPdeduct.SelectedItem = "$100"
        End If
    End Sub


#End Region ' Warranty Tab

#Region "Custom Functions/Subs"

    Private Sub setuparray()
        Dim i As Integer

        For x As Integer = 0 To (mydata.Length - 1)
            mydata(x) = New element

        Next

        i = 0
        mydata(i).txtbox = Me.txtBOS
        mydata(i).dbfeild = "BOS_number"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 1
        mydata(i).txtbox = Me.DateSold
        mydata(i).dbfeild = "Date_sold"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 2
        mydata(i).txtbox = Me.txtbuyer1last
        mydata(i).dbfeild = "Buyer1_last"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 3
        mydata(i).txtbox = Me.txtbuyer1first
        mydata(i).dbfeild = "Buyer1_first"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 4
        mydata(i).txtbox = Me.txtbuyer2last
        mydata(i).dbfeild = "Buyer2_last"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 5
        mydata(i).txtbox = Me.txtbuyer2first
        mydata(i).dbfeild = "Buyer2_first"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 6
        mydata(i).txtbox = Me.txtaddress
        mydata(i).dbfeild = "Address"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 7
        mydata(i).txtbox = Me.txtcity
        mydata(i).dbfeild = "City"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 8
        mydata(i).txtbox = Me.txtprov
        mydata(i).dbfeild = "Prov"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 9
        mydata(i).txtbox = Me.txtpostal
        mydata(i).dbfeild = "Postal_Code"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 10
        mydata(i).txtbox = Me.txthome
        mydata(i).dbfeild = "Home_Number"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 11
        mydata(i).txtbox = Me.txtwork
        mydata(i).dbfeild = "Work_Number"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 12
        mydata(i).txtbox = Me.txtemail
        mydata(i).dbfeild = "e_mail"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 13
        mydata(i).txtbox = Me.txtskipkg
        mydata(i).dbfeild = "ski_pack_num"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 14
        mydata(i).txtbox = Me.txto1
        mydata(i).dbfeild = "o1"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 15
        mydata(i).txtbox = Me.txto2
        mydata(i).dbfeild = "o2"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 16
        mydata(i).txtbox = Me.txto3
        mydata(i).dbfeild = "o3"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 17
        mydata(i).txtbox = Me.txto4
        mydata(i).dbfeild = "o4"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 18
        mydata(i).txtbox = Me.txto5
        mydata(i).dbfeild = "o5"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 19
        mydata(i).txtbox = Me.txto6
        mydata(i).dbfeild = "o6"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 20
        mydata(i).txtbox = Me.txto7
        mydata(i).dbfeild = "o7"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 21
        mydata(i).txtbox = Me.txto8
        mydata(i).dbfeild = "o8"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 22
        mydata(i).txtbox = Me.txtnotes1
        mydata(i).dbfeild = "Conditions"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 23
        mydata(i).txtbox = Me.txtnotes
        mydata(i).dbfeild = "Notes1"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 24
        mydata(i).txtbox = Me.txtp1num
        mydata(i).dbfeild = "payment1_number"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 25
        mydata(i).txtbox = Me.txtp2num
        mydata(i).dbfeild = "payment2_number"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 26
        mydata(i).txtbox = Me.txtp3num
        mydata(i).dbfeild = "payment3_number"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 27
        mydata(i).txtbox = Me.txtp1date
        mydata(i).dbfeild = "Payment1_Date"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 28
        mydata(i).txtbox = Me.txtp2date
        mydata(i).dbfeild = "Payment2_Date"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 29
        mydata(i).txtbox = Me.txtp3date
        mydata(i).dbfeild = "Payment3_Date"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 30
        mydata(i).txtbox = Me.chkstire
        mydata(i).dbfeild = "S_tire"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = "0"

        i = 31
        mydata(i).txtbox = Me.chkcover
        mydata(i).dbfeild = "Cover"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = "0"

        i = 32
        mydata(i).txtbox = Me.chkskipkg
        mydata(i).dbfeild = "Ski_Pack"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = "0"

        i = 33
        mydata(i).txtbox = Me.chkrockg
        mydata(i).dbfeild = "Rock_Guard"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = "0"

        i = 34
        mydata(i).txtbox = Me.chksprop
        mydata(i).dbfeild = "S_prop"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = "0"

        i = 35
        mydata(i).txtbox = Me.chksafepkg
        mydata(i).dbfeild = "Safety_Pkg"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = "0"

        i = 36
        mydata(i).txtbox = Me.chklockpkg
        mydata(i).dbfeild = "Lock_Pkg"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 37
        mydata(i).txtbox = Me.chkantitheftreg
        mydata(i).dbfeild = "Antitheft"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = "0"

        i = 38
        mydata(i).txtbox = Me.chkprotech
        mydata(i).dbfeild = "Protech"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = "0"

        i = 39
        mydata(i).txtbox = Me.chkextwarranty
        mydata(i).dbfeild = "Ext_Warranty"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = "0"

        i = 40
        mydata(i).txtbox = Me.Chk20hr
        mydata(i).dbfeild = "20_Hour"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = "0"

        i = 41
        mydata(i).txtbox = Me.chkwinterize
        mydata(i).dbfeild = "Winterize"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = "0"

        i = 42
        mydata(i).txtbox = Me.txtstireprice
        mydata(i).dbfeild = "s_tire_Price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 43
        mydata(i).txtbox = Me.txtcoverprice
        mydata(i).dbfeild = "Cover_Price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 44
        mydata(i).txtbox = Me.txtskipkgprice
        mydata(i).dbfeild = "Ski_Pack_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 45
        mydata(i).txtbox = Me.txtrockgprice
        mydata(i).dbfeild = "Rock_Guard_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 46
        mydata(i).txtbox = Me.txtspropprice
        mydata(i).dbfeild = "S_prop_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 47
        mydata(i).txtbox = Me.txtsafepkgprice
        mydata(i).dbfeild = "Safety_Pkg_Price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 48
        mydata(i).txtbox = Me.txtlockpkgprice
        mydata(i).dbfeild = "lock_Pkg_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 49
        mydata(i).txtbox = Me.txto1price
        mydata(i).dbfeild = "o1_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 50
        mydata(i).txtbox = Me.txto2price
        mydata(i).dbfeild = "o2_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 51
        mydata(i).txtbox = Me.txto3price
        mydata(i).dbfeild = "o3_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 52
        mydata(i).txtbox = Me.txto4price
        mydata(i).dbfeild = "o4_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 53
        mydata(i).txtbox = Me.txto5price
        mydata(i).dbfeild = "o5_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 54
        mydata(i).txtbox = Me.txto6price
        mydata(i).dbfeild = "o6_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 55
        mydata(i).txtbox = Me.txto7price
        mydata(i).dbfeild = "o7_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 56
        mydata(i).txtbox = Me.txto8price
        mydata(i).dbfeild = "o8_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 57
        mydata(i).txtbox = Me.txtboatprice
        mydata(i).dbfeild = "Price"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 58
        mydata(i).txtbox = Me.txtmotorprice
        mydata(i).dbfeild = "Motor_Upgrade_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 59
        mydata(i).txtbox = Me.txttrailerprice
        mydata(i).dbfeild = "Trailer_price_override"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 60
        mydata(i).txtbox = Me.txtkickerprice
        mydata(i).dbfeild = "Kicker_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 61
        mydata(i).txtbox = Me.txtdiscountprice
        mydata(i).dbfeild = "Discount"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 62
        mydata(i).txtbox = Me.txtadminfee
        mydata(i).dbfeild = "Admin_fee"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 63
        mydata(i).txtbox = Me.txttiretax
        mydata(i).dbfeild = "Tire_tax"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 64
        mydata(i).txtbox = Me.txtantitheftreg
        mydata(i).dbfeild = "Antitheft_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 65
        mydata(i).txtbox = Me.txtprotech
        mydata(i).dbfeild = "protech_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 66
        mydata(i).txtbox = Me.txtextwarranty
        mydata(i).dbfeild = "ext_warranty_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 67
        mydata(i).txtbox = Me.txt20hr
        mydata(i).dbfeild = "20_hour_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 68
        mydata(i).txtbox = Me.txtwinterize
        mydata(i).dbfeild = "winterize_price"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 69
        mydata(i).txtbox = Me.txtoptionstotal
        mydata(i).dbfeild = "o_total"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 70
        mydata(i).txtbox = Me.txtpackagetotal
        mydata(i).dbfeild = "p_total"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 71
        mydata(i).txtbox = Me.txttrade
        mydata(i).dbfeild = "Trade_in"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 72
        mydata(i).txtbox = Me.txtsubtotal
        mydata(i).dbfeild = "subtotal"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 73
        mydata(i).txtbox = Me.txtpst
        mydata(i).dbfeild = "pst"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 74
        mydata(i).txtbox = Me.txtgst
        mydata(i).dbfeild = "gst"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 75
        mydata(i).txtbox = Me.txttotal
        mydata(i).dbfeild = "total"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 76
        mydata(i).txtbox = Me.txtp1amount
        mydata(i).dbfeild = "Payment1_ammount"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 77
        mydata(i).txtbox = Me.txtp2amount
        mydata(i).dbfeild = "Payment2_ammount"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 78
        mydata(i).txtbox = Me.txtp3amount
        mydata(i).dbfeild = "Payment3_ammount"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 79
        mydata(i).txtbox = Me.txtboatunit
        mydata(i).dbfeild = "boat_serial"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 80
        mydata(i).txtbox = Me.txtcolor
        mydata(i).dbfeild = "boat_color"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 81
        mydata(i).txtbox = Me.txtboatmake
        mydata(i).dbfeild = "boat_brand"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 82
        mydata(i).txtbox = Me.txtboatmodel
        mydata(i).dbfeild = "boat_model"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 83
        mydata(i).txtbox = Me.txtboathin
        mydata(i).dbfeild = "boat_hin"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 84
        mydata(i).txtbox = Me.txtboatyear
        mydata(i).dbfeild = "boat_year"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 85
        mydata(i).txtbox = Me.txtmotormake
        mydata(i).dbfeild = "motor_make_override"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 86
        mydata(i).txtbox = Me.txtmotormodel
        mydata(i).dbfeild = "motor_model_override"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 87
        mydata(i).txtbox = Me.txtmotorserial
        mydata(i).dbfeild = "motor_serial_override"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 88
        mydata(i).txtbox = Me.txtmotoryear
        mydata(i).dbfeild = "motor_year_override"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 89
        mydata(i).txtbox = Me.txttrailermake
        mydata(i).dbfeild = "trailer_make_override"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 90
        mydata(i).txtbox = Me.txttrailermodel
        mydata(i).dbfeild = "trailer_model_override"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 91
        mydata(i).txtbox = Me.txttrailerserial
        mydata(i).dbfeild = "trailer_serial_override"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 92
        mydata(i).txtbox = Me.txttraileryear
        mydata(i).dbfeild = "trailer_year_override"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 93
        mydata(i).txtbox = Me.txtdrivemake
        mydata(i).dbfeild = "none"
        mydata(i).dbtable = "none"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 94
        mydata(i).txtbox = Me.txtdrivemodel
        mydata(i).dbfeild = "drive_model"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 95
        mydata(i).txtbox = Me.txtdriveserial
        mydata(i).dbfeild = "drive_serial"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 96
        mydata(i).txtbox = Me.txtdriveyear
        mydata(i).dbfeild = "none"
        mydata(i).dbtable = "none"
        mydata(i).changed = False
        mydata(i).value = ""
        i = 97
        mydata(i).txtbox = Me.txttplatemake
        mydata(i).dbfeild = "none"
        mydata(i).dbtable = "none"
        mydata(i).changed = False
        mydata(i).value = ""
        i = 98
        mydata(i).txtbox = Me.txttplatemodel
        mydata(i).dbfeild = "none"
        mydata(i).dbtable = "none"
        mydata(i).changed = False
        mydata(i).value = ""
        i = 99
        mydata(i).txtbox = Me.txttplateserial
        mydata(i).dbfeild = "tplate_serial"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""
        i = 100
        mydata(i).txtbox = Me.txttplateyear
        mydata(i).dbfeild = "none"
        mydata(i).dbtable = "none"
        mydata(i).changed = False
        mydata(i).value = ""
        i = 101
        mydata(i).txtbox = Me.txtkickermake
        mydata(i).dbfeild = "kicker_make"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""
        i = 102
        mydata(i).txtbox = Me.txtkickermodel
        mydata(i).dbfeild = "kicker_model"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""
        i = 103
        mydata(i).txtbox = Me.txtkickerserial
        mydata(i).dbfeild = "kicker_serial"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""
        i = 104
        mydata(i).txtbox = Me.txtkickeryear
        mydata(i).dbfeild = "kicker_year"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""
        i = 105
        mydata(i).txtbox = Me.txtdiscount
        mydata(i).dbfeild = "discount_reason"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""
        'overrided boxs
        i = 106
        mydata(i).txtbox = Me.txtmotormake
        mydata(i).dbfeild = "engine_make"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 107
        mydata(i).txtbox = Me.txtmotormodel
        mydata(i).dbfeild = "engine_model"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 108
        mydata(i).txtbox = Me.txtmotorserial
        mydata(i).dbfeild = "engine_serial"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 109
        mydata(i).txtbox = Me.txtmotoryear
        mydata(i).dbfeild = "engine_year"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 110
        mydata(i).txtbox = Me.txttrailermake
        mydata(i).dbfeild = "trailer_make"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 111
        mydata(i).txtbox = Me.txttrailermodel
        mydata(i).dbfeild = "trailer_model"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 112
        mydata(i).txtbox = Me.txttrailerserial
        mydata(i).dbfeild = "trailer_serial"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 113
        mydata(i).txtbox = Me.txttraileryear
        mydata(i).dbfeild = "trailer_year"
        mydata(i).dbtable = "inventory"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 114
        mydata(i).txtbox = Me.txtsalesman
        mydata(i).dbfeild = "salesman"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 115
        mydata(i).txtbox = Me.txtsalesman2
        mydata(i).dbfeild = "salesman2"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 116
        mydata(i).txtbox = Me.txtwotext
        mydata(i).dbfeild = "WOtext"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 117
        mydata(i).txtbox = Me.txtwonumber
        mydata(i).dbfeild = "WOnum"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 118
        mydata(i).txtbox = Me.DateTimeWO
        mydata(i).dbfeild = "WOduedate"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 119
        mydata(i).txtbox = Me.txtbizman
        mydata(i).dbfeild = "bizman"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 120
        mydata(i).txtbox = Me.txtfinnotes
        mydata(i).dbfeild = "notes2"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 121
        mydata(i).txtbox = Me.txtwarrcost
        mydata(i).dbfeild = "warranty_cost"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 122
        mydata(i).txtbox = Me.txttradelein
        mydata(i).dbfeild = "tradelein"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 123
        mydata(i).txtbox = Me.txtreserve
        mydata(i).dbfeild = "RESERVE"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 124
        mydata(i).txtbox = Me.txtatnum
        mydata(i).dbfeild = "ATNumber"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 125
        mydata(i).txtbox = Me.txtPTcost
        mydata(i).dbfeild = "PTcost"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 126
        mydata(i).txtbox = Me.CSmanwarr
        mydata(i).dbfeild = "warman"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        '
        '
        '

        i = 127
        mydata(i).txtbox = Me.txtcommrate1
        mydata(i).dbfeild = "commrate1"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 128
        mydata(i).txtbox = Me.txtcommrate2
        mydata(i).dbfeild = "commrate2"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 129
        mydata(i).txtbox = Me.txtcommrate3
        mydata(i).dbfeild = "commrate3"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 130
        mydata(i).txtbox = Me.txtcommrate4
        mydata(i).dbfeild = "commrate4"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 131
        mydata(i).txtbox = Me.txtcommrate5
        mydata(i).dbfeild = "commrate5"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 132
        mydata(i).txtbox = Me.txtcommrate6
        mydata(i).dbfeild = "commrate6"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 133
        mydata(i).txtbox = Me.txtcommrate7
        mydata(i).dbfeild = "commrate7"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 134
        mydata(i).txtbox = Me.txtcommrate8
        mydata(i).dbfeild = "commrate8"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 135
        mydata(i).txtbox = Me.txtMiscComm
        mydata(i).dbfeild = "misccomm"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 136
        mydata(i).txtbox = Me.txtfinancefee
        mydata(i).dbfeild = "financefee"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 137
        mydata(i).txtbox = Me.txtcommchange1
        mydata(i).dbfeild = "commadjust1"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 138
        mydata(i).txtbox = Me.txtcommchange2
        mydata(i).dbfeild = "commadjust2"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 139
        mydata(i).txtbox = Me.txtchange1note
        mydata(i).dbfeild = "commadjust_note1"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 140
        mydata(i).txtbox = Me.txtchange2note
        mydata(i).dbfeild = "commadjust_note2"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 141
        mydata(i).txtbox = Me.txtBOcommadjust
        mydata(i).dbfeild = "BOcommadjust"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 142
        mydata(i).txtbox = Me.txtBOcommadjustnote
        mydata(i).dbfeild = "BOcommadjust_note"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 143
        mydata(i).txtbox = Me.txtsmallitemcom
        mydata(i).dbfeild = "smallitempay"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 144
        mydata(i).txtbox = Me.CSextwarr
        mydata(i).dbfeild = "warext"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 145
        mydata(i).txtbox = Me.txttires
        mydata(i).dbfeild = "tires"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 146
        mydata(i).txtbox = Me.txtbatteries
        mydata(i).dbfeild = "batteries"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 147
        mydata(i).txtbox = Me.txtDepNum
        mydata(i).dbfeild = "Deposit_Number"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 148
        mydata(i).txtbox = Me.txtDepAmount
        mydata(i).dbfeild = "Deposit_ammount"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""

        i = 149
        mydata(i).txtbox = Me.txtDepDate
        mydata(i).dbfeild = "Deposit_date"
        mydata(i).dbtable = "bos"
        mydata(i).changed = False
        mydata(i).value = ""


    End Sub

    Private Sub setuparray2()
        Dim i As Integer

        For x As Integer = 0 To (mydata2.Length - 1)
            mydata2(x) = New element

        Next
        Dim text As String
        text = txtbuyer1last.Text
        If txtbuyer1first.Text <> "" Then
            text = text + ", " + txtbuyer1first.Text
        End If
        If txtbuyer2last.Text <> "" Or txtbuyer2first.Text <> "" Then
            text = text + " & " + txtbuyer2last.Text
        End If

        If txtbuyer2first.Text <> "" Then
            If txtbuyer2last.Text = "" Then
                text = text + txtbuyer2first.Text
            Else
                text = text + ", " + txtbuyer2first.Text
            End If
        End If

        i = 0
        mydata2(i).txtbox = Me.txtwonumber
        mydata2(i).dbfeild = "Name"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = True
        mydata2(i).value = text

        i = 1
        mydata2(i).txtbox = Me.txtwonumber
        mydata2(i).dbfeild = "Address"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = True
        mydata2(i).value = txtaddress.Text


        i = 2
        mydata2(i).txtbox = Me.txtwonumber
        mydata2(i).dbfeild = "City"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = True
        mydata2(i).value = txtcity.Text

        i = 3
        mydata2(i).txtbox = Me.txtwonumber
        mydata2(i).dbfeild = "Prov"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = True
        mydata2(i).value = txtprov.Text

        i = 4
        mydata2(i).txtbox = Me.txtpostal
        mydata2(i).dbfeild = "Postal"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = True
        mydata2(i).value = txtpostal.Text

        i = 5
        mydata2(i).txtbox = Me.txtwonumber
        mydata2(i).dbfeild = "Phone1"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = True
        mydata2(i).value = txthome.Text

        i = 6
        mydata2(i).txtbox = Me.txtwonumber
        mydata2(i).dbfeild = "Phone2"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = True
        mydata2(i).value = txtwork.Text

        i = 7
        mydata2(i).txtbox = Me.txtemail
        mydata2(i).dbfeild = "Email"
        mydata2(i).dbtable = "Customer"
        mydata2(i).changed = True
        mydata2(i).value = txtemail.Text

        If myboat > 1 Then
            i = 8
            mydata2(i).txtbox = Me.txtboatmake
            mydata2(i).dbfeild = "Bmake"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txtboatmake.Text

            i = 9
            mydata2(i).txtbox = Me.txtboatmodel
            mydata2(i).dbfeild = "Bmodel"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txtboatmodel.Text

            i = 10
            mydata2(i).txtbox = Me.txtwonumber
            mydata2(i).dbfeild = "Bserial"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txtboathin.Text

            i = 11
            mydata2(i).txtbox = Me.txtmotormake
            mydata2(i).dbfeild = "Mmake"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txtmotormake.Text

            i = 12
            mydata2(i).txtbox = Me.txtmotormodel
            mydata2(i).dbfeild = "Mmodel"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txtmotormodel.Text

            i = 13
            mydata2(i).txtbox = Me.txtmotorserial
            mydata2(i).dbfeild = "Mserial"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txtmotorserial.Text

            i = 14
            mydata2(i).txtbox = Me.txttrailermake
            mydata2(i).dbfeild = "Tmake"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txttrailermake.Text

            i = 15
            mydata2(i).txtbox = Me.txttrailermodel
            mydata2(i).dbfeild = "Tmodel"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txttrailermodel.Text

            i = 16
            mydata2(i).txtbox = Me.txttrailerserial
            mydata2(i).dbfeild = "Tserial"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txttrailerserial.Text

            i = 17
            mydata2(i).txtbox = Me.txtcolor
            mydata2(i).dbfeild = "Color"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txtcolor.Text


            i = 18
            mydata2(i).txtbox = Me.txtmotoryear
            mydata2(i).dbfeild = "Year"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txtboatyear.Text

        Else
            i = 8
            mydata2(i).txtbox = Me.txtboatmake
            mydata2(i).dbfeild = "Bmake"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txtmotormake.Text

            i = 9
            mydata2(i).txtbox = Me.txtboatmodel
            mydata2(i).dbfeild = "Bmodel"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txtmotormodel.Text

            i = 10
            mydata2(i).txtbox = Me.txtwonumber
            mydata2(i).dbfeild = "Bserial"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txtmotorserial.Text

            i = 11
            mydata2(i).txtbox = Me.txtmotormake
            mydata2(i).dbfeild = "Mmake"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = ""

            i = 12
            mydata2(i).txtbox = Me.txtmotormodel
            mydata2(i).dbfeild = "Mmodel"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = ""

            i = 13
            mydata2(i).txtbox = Me.txtmotorserial
            mydata2(i).dbfeild = "Mserial"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = ""

            i = 14
            mydata2(i).txtbox = Me.txttrailermake
            mydata2(i).dbfeild = "Tmake"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = ""

            i = 15
            mydata2(i).txtbox = Me.txttrailermodel
            mydata2(i).dbfeild = "Tmodel"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = ""

            i = 16
            mydata2(i).txtbox = Me.txttrailerserial
            mydata2(i).dbfeild = "Tserial"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = ""

            i = 17
            mydata2(i).txtbox = Me.txtcolor
            mydata2(i).dbfeild = "Color"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = ""

            i = 18
            mydata2(i).txtbox = Me.txtmotoryear
            mydata2(i).dbfeild = "Year"
            mydata2(i).dbtable = "Customer"
            mydata2(i).changed = True
            mydata2(i).value = txtmotoryear.Text


        End If


    End Sub

    Private Function savecust() As Integer

        For x As Integer = 0 To mydatacount2
            mydata2(x).value = Replace(mydata2(x).value, "'", "\'")
            mydata2(x).value = Replace(mydata2(x).value, ";", "\;")
        Next

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring

        cmd.Connection = conn
        Dim cmdstring As String = "Insert Into ServiceCustomers Set "

        For x As Integer = 0 To mydatacount2
            If mydata2(x).dbtable = "Customer" And mydata2(x).changed = True Then
                cmdstring = cmdstring + mydata2(x).dbfeild + " = '" + mydata2(x).value + "', "
            End If
        Next
        cmdstring = Mid(cmdstring, 1, cmdstring.Length - 2)
        If cmdstring <> "Insert Into ServiceCustomers Se" Then
            cmd.CommandText = cmdstring
            Debug.WriteLine(cmdstring)

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Select Max(Number) from ServiceCustomers"
                savecust = cmd.ExecuteScalar
                conn.Close()
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            End Try
            conn.Dispose()
        End If

        For x As Integer = 0 To mydatacount2
            mydata2(x).value = Replace(mydata2(x).value, "\'", "'")
            mydata2(x).value = Replace(mydata2(x).value, "\;", ";")
        Next


    End Function

    Private Sub updatemath()
        Dim temptirefee As Decimal = 0
        If txtbatteries.Text <> "" Then
            Select Case persistant.getvalue(persistant.tbl_location, "Prov", "Code = '" + mybosstorecode + "'", 0)
                Case "AB"
                    temptirefee = temptirefee + (CInt(txtbatteries.Text) * 0)
                Case "BC"
                    temptirefee = temptirefee + (CInt(txtbatteries.Text) * 0)
            End Select
        End If
        If txttires.Text <> "" Then
            Select Case persistant.getvalue(persistant.tbl_location, "Prov", "Code = '" + mybosstorecode + "'", 0)
                Case "AB"
                    temptirefee = temptirefee + (CInt(txttires.Text) * 4)
                Case "BC"
                    temptirefee = temptirefee + (CInt(txttires.Text) * 5)
            End Select
        End If
        txttiretax.Text = temptirefee.ToString
        moneychanged(txttiretax)


        Dim x As Decimal
        moneychanged(Me.txttradelein)
        x = 0
        For y As Integer = 42 To 48
            If mydata(y - 12).value.ToString = "1" Or mydata(y - 12).value.ToString = "True" Then
                x = x + Val(mydata(y).value)
            End If
        Next
        For y As Integer = 49 To 56
            x = x + Val(mydata(y).value)
        Next

        Me.txtoptionstotal.Text = x
        moneychanged(Me.txtoptionstotal)
        Dim strtemp As String
        strtemp = Me.txtboatprice.Text
        strtemp = Replace(strtemp, "$", "")
        strtemp = Replace(strtemp, ",", "")

        x = Val(strtemp)

        For y As Integer = 58 To 63
            If y = 61 Then
                x = x - Val(mydata(y).value)
            Else
                x = x + Val(mydata(y).value)
            End If

        Next

        For y As Integer = 64 To 68
            If mydata(y - 27).value.ToString = "1" Or mydata(y - 27).value.ToString = "True" Then
                x = x + Val(mydata(y).value)
            End If

        Next
        Me.txtpackagetotal.Text = x
        moneychanged(Me.txtpackagetotal)

        Me.txtsubtotal.Text = Val(mydata(69).value) + Val(mydata(70).value) - Val(mydata(71).value) + Val(mydata(136).value)
        moneychanged(Me.txtsubtotal)

        If chkPST.Checked = True Then
            Me.txtpst.Text = (Val(mydata(72).value) * PSTRate)
            moneychanged(Me.txtpst)
        Else
            Me.txtpst.Text = "0"
            moneychanged(Me.txtpst)
        End If
        If ChkGST.Checked = True Then
            If Me.txtdatedeled.Text = "" Then
                Me.txtgst.Text = Val(mydata(72).value) * GSTRate
            Else
                If CDate(Me.txtdatedeled.Text) > CDate("Jan 1, 2008") Then
                    Me.txtgst.Text = Val(mydata(72).value) * GSTRate
                Else
                    Me.txtgst.Text = Val(mydata(72).value) * 0.06
                End If

            End If


            moneychanged(Me.txtgst)
        Else
            Me.txtgst.Text = "0"
            moneychanged(Me.txtgst)
        End If

        Me.txttotal.Text = Val(mydata(72).value) + Val(mydata(73).value) + Val(mydata(74).value) + Val(mydata(122).value)
        If txttotal.Text = "" Then txttotal.Text = 0
        moneychanged(Me.txttotal)

        Me.txtbocashprice.Text = FormatNumber((Val(mydata(69).value) + Val(mydata(70).value) - Val(mydata(66).value)), 2)
        Me.txtbowarranty.Text = FormatNumber(Val(mydata(66).value), 2)
        Me.txtbolien.Text = FormatNumber(Val(mydata(122).value), 2)
        Me.txtbotrade.Text = FormatNumber(Val(mydata(71).value), 2)
        Me.txtbodown.Text = FormatNumber((Val(mydata(76).value) + Val(mydata(77).value) + Val(mydata(78).value)), 2)

        'FINANCING STUFF

        Dim owing As Decimal
        owing = Val(mydata(75).value) ' total
        ' 76 = payment1, 77 = payment2, 78 = payment3
        owing = owing - (Val(mydata(76).value) + Val(mydata(77).value) + Val(mydata(78).value))
        txtowing.Text = Format(owing.ToString, "currency")

        'If FandI.bankfeeoverride = 0 Then FandI.bankfeeoverride = FandI.bankfee
        If ChkGST.Checked = True Then FandI.fee = Val(mydata(136).value) * (1 + GSTRate)
        If chkPST.Checked = True Then FandI.fee = FandI.fee + (Val(mydata(136).value) * PSTRate)
        If ListBanks.SelectedItem = "ATB Financial" Then FandI.fee = Val(mydata(136).value)

        FandI.dollerspre = Val(mydata(75).value) + FandI.fee + FandI.bankfeeoverride

        FandI.dollerspre = FandI.dollerspre - (Val(mydata(76).value) + Val(mydata(77).value) + Val(mydata(78).value))

        ' FandI.dollerspre = Val(mydata(75).value) - (Val(mydata(76).value) + Val(mydata(77).value) + Val(mydata(78).value)) + Val(mydata(122).value) + 208.95 + FandI.bankfeeoverride
        FandI.dollerstotal = FandI.dollerspre

        FandI.Term = Val(cmbTerm.SelectedItem)
        FandI.Ammort = Val(cmbammort.SelectedItem)
        If Val(Me.txtammover.Text) <> 0 Then FandI.Ammort = Val(Me.txtammover.Text)

        '  FandI.Rate = Val(cmbrate.SelectedItem) / 100
        If Val(Me.txtrateover.Text) <> 0 Then FandI.Rate = Val(Me.txtrateover.Text) / 100

        FandI.lifebuyer = 0
        If Me.cmblins.SelectedIndex = 1 Or Me.cmblins.SelectedIndex = 2 Then
            FandI.lifebuyer = 1
        End If
        If Me.cmblins.SelectedIndex = 3 Then
            FandI.lifebuyer = 2
        End If
        FandI.AandHbuyers = 0
        If Me.cmbains.SelectedIndex = 1 Or Me.cmbains.SelectedIndex = 2 Then
            FandI.AandHbuyers = 1
        End If
        If Me.cmbains.SelectedIndex = 3 Then
            FandI.AandHbuyers = 2
        End If
        FandI.criticalbuyers = 0

        If Me.cmbcins.SelectedIndex = 1 Or Me.cmbcins.SelectedIndex = 2 Then
            FandI.criticalbuyers = 1
        End If
        If Me.cmbcins.SelectedIndex = 3 Then
            FandI.criticalbuyers = 2
        End If
        FandI.AccidentalPlusBuyers = 0

        If Me.cmbAccidentalPlus.SelectedIndex = 1 Or Me.cmbAccidentalPlus.SelectedIndex = 2 Then
            FandI.AccidentalPlusBuyers = 1
        End If
        If Me.cmbAccidentalPlus.SelectedIndex = 3 Then
            FandI.AccidentalPlusBuyers = 2
        End If


        If Val(FandI.Rate) <> 0 And Val(FandI.Ammort) <> 0 And Val(FandI.Term) <> 0 And Me.cmbahtype.SelectedIndex <> -1 Then

            Dim last_dollerstotal As Decimal = 0
            Dim monthlypayments As Decimal
            While last_dollerstotal <> FandI.dollerstotal
                last_dollerstotal = FandI.dollerstotal

                FandI.pmt = FormatNumber(persistant.paymentmath.payment(FandI.Rate, FandI.Ammort, FandI.dollerstotal), 2)
                FandI.residual = FormatNumber(persistant.paymentmath.residual(FandI.Rate, FandI.Term, FandI.Ammort, FandI.dollerstotal), 2)
                FandI.residual2 = FormatNumber(persistant.paymentmath.residual(FandI.Rate, FandI.Term - 1, FandI.Ammort - 1, FandI.dollerstotal), 2)
                'Pause Here
                monthlypayments = FormatNumber(FandI.pmt * FandI.Term, 2)
                FandI.lifec = persistant.paymentmath.creditlife(monthlypayments, FandI.Term, FandI.lifebuyer, Prov)
                FandI.lifer = persistant.paymentmath.reslife(FandI.residual, FandI.Term, FandI.lifebuyer, Prov)
                FandI.Criticalc = persistant.paymentmath.creditcritical(monthlypayments, FandI.Term, FandI.criticalbuyers, Prov)
                FandI.Criticalr = persistant.paymentmath.rescritical(FandI.residual, FandI.Term, FandI.criticalbuyers, Prov)
                FandI.AandH = persistant.paymentmath.ah(monthlypayments, FandI.Term, FandI.AandHbuyers, Me.cmbahtype.SelectedItem.ToString, Prov)
                FandI.AccidentalPlus = persistant.paymentmath.ap(monthlypayments, FandI.Term, FandI.AccidentalPlusBuyers, Prov)

                FandI.dollerstotal = FandI.dollerspre + FormatNumber(FandI.AandH, 2) + FormatNumber(FandI.lifec, 2) + FormatNumber(FandI.lifer, 2) + _
                    FormatNumber(FandI.Criticalc, 2) + FormatNumber(FandI.Criticalr, 2) + FormatNumber(FandI.AccidentalPlus, 2)

                FandI.pmt = persistant.paymentmath.payment(FandI.Rate, FandI.Ammort, FandI.dollerstotal)

            End While

            txttotalfin.Text = Format(FandI.dollerstotal, "Currency")


            Me.ToolStripStatusLabel1.Text = Format(FandI.pmt.ToString, "currency")

            TextBox1.Text = (Format(FandI.lifec, "Currency"))
            TextBox2.Text = (Format(FandI.lifer, "Currency"))
            TextBox3.Text = (Format(FandI.AandH, "Currency"))
            TextBox4.Text = (Format(FandI.Criticalc, "Currency"))
            TextBox5.Text = (Format(FandI.Criticalr, "Currency"))
            TextBox7.Text = (Format(FandI.AccidentalPlus, "Currency"))

        End If
        Dim tempx As Decimal
        tempx = 0
        If (mydata(49).value <> "" And mydata(127).value <> "") Then tempx = tempx + CDec(mydata(49).value) * CDec(mydata(127).value) / 100
        If (mydata(50).value <> "" And mydata(128).value <> "") Then tempx = tempx + CDec(mydata(50).value) * CDec(mydata(128).value) / 100
        If (mydata(51).value <> "" And mydata(129).value <> "") Then tempx = tempx + CDec(mydata(51).value) * CDec(mydata(129).value) / 100
        If (mydata(52).value <> "" And mydata(130).value <> "") Then tempx = tempx + CDec(mydata(52).value) * CDec(mydata(130).value) / 100
        If (mydata(53).value <> "" And mydata(131).value <> "") Then tempx = tempx + CDec(mydata(53).value) * CDec(mydata(131).value) / 100
        If (mydata(54).value <> "" And mydata(132).value <> "") Then tempx = tempx + CDec(mydata(54).value) * CDec(mydata(132).value) / 100
        If (mydata(55).value <> "" And mydata(133).value <> "") Then tempx = tempx + CDec(mydata(55).value) * CDec(mydata(133).value) / 100
        If (mydata(56).value <> "" And mydata(134).value <> "") Then tempx = tempx + CDec(mydata(56).value) * CDec(mydata(134).value) / 100
        txtMiscComm.Text = tempx.ToString
        moneychanged(Me.txtMiscComm)

        If ChkGST.Checked = False Then
            txtRedNoGST.Visible = True
        Else
            txtRedNoGST.Visible = False
        End If

        Select Case persistant.getvalue(persistant.tbl_location, "Prov", "Code = '" + mybosstorecode + "'", 0)
            Case "BC"
                If chkPST.Checked = False Then
                    txtrednopst.Visible = True
                Else
                    txtrednopst.Visible = False
                End If
        End Select

    End Sub

    Private Function saldateformat(ByVal input As String) As String
        Dim tempdate As Date
        Try
            tempdate = CDate(input)
            saldateformat = Format(tempdate, "ddMMMyyyy")
        Catch ex As Exception
            saldateformat = ""
        End Try
    End Function

    Function linex(ByVal input As String, ByVal x As Integer) As String
        Dim leftbound, rightbound As Integer
        If howmanylines(input) = 0 Then
            linex = ""
            GoTo z
        End If
        If howmanylines(input) = 1 Then
            linex = input
            GoTo z
        End If
        If x = 0 Then
            rightbound = InStr(input, vbNewLine)
            linex = Mid(input, 1, rightbound - 1)
            GoTo z
        End If
        If x = howmanylines(input) - 1 Then
            leftbound = 1
a:
            rightbound = InStr(leftbound, input, vbNewLine)
            If rightbound <> 0 Then
                leftbound = rightbound + 1
                GoTo a
            End If
            linex = Mid(input, Len(input) - (leftbound - 1))
            GoTo z
        End If
        rightbound = 0
        leftbound = 0
        For y As Integer = 0 To x
            leftbound = rightbound
            rightbound = InStr(leftbound + 1, input, vbNewLine)
        Next
        linex = Mid(input, leftbound + 1, rightbound - (leftbound + 1))
z:

    End Function

    Function howmanylines(ByVal input As String) As Integer
        Dim count As Integer = 0
        Dim lastfound As Integer = 0

        If Replace(input, " ", "") = "" Then
            howmanylines = 0
            GoTo z
        End If

        If InStr(input, vbNewLine) = 0 Then
            howmanylines = 1
            GoTo z
        End If
        lastfound = InStr(input, vbNewLine)
        count = count + 1

a:
        If lastfound <> 0 Then
            lastfound = InStr(lastfound + 1, input, vbNewLine)
            count = count + 1
            GoTo a
        End If
        howmanylines = count

z:
    End Function

    Private Sub assumedata()
        If Me.txttplateserial.Text <> "" Then
            Me.txttplatemake.Text = Me.txtmotormake.Text
            Me.txttplatemodel.Text = "T - Plate"
            Me.txttplateyear.Text = Me.txtmotoryear.Text
        Else
            Me.txttplatemake.Text = ""
            Me.txttplatemodel.Text = ""
            Me.txttplateyear.Text = ""
        End If
        If Me.txtdrivemodel.Text <> "" Or Me.txtdriveserial.Text <> "" Then
            Me.txtdrivemake.Text = Me.txtmotormake.Text
            Me.txtdriveyear.Text = Me.txtmotoryear.Text
        Else
            Me.txtdrivemake.Text = ""
            Me.txtdriveyear.Text = ""
        End If
        stringchanged(Me.txttplatemodel)
        stringchanged(Me.txtdrivemake)
        stringchanged(Me.txtdriveyear)
        stringchanged(Me.txttplatemake)
        stringchanged(Me.txttplateyear)
    End Sub

    Private Sub logo()
        If mybosstorecode = "EDM" Then
            Me.PictureBox3.BackgroundImage = MMSOnline.My.Resources.SPWlogo
            Me.PictureBox2.BackgroundImage = MMSOnline.My.Resources.EDMadr
            localareacode = 780
        End If
        If mybosstorecode = "SYL" Then
            Me.PictureBox3.BackgroundImage = MMSOnline.My.Resources.SYLlogo
            Me.PictureBox2.BackgroundImage = MMSOnline.My.Resources.SYLadr
            localareacode = 403
        End If
        If mybosstorecode = "ATL" Then
            Me.PictureBox3.BackgroundImage = MMSOnline.My.Resources.ATLlogo
            Me.PictureBox2.BackgroundImage = MMSOnline.My.Resources.ATLadr
            localareacode = 780
        End If
        If mybosstorecode = "REN" Then
            Me.PictureBox3.BackgroundImage = MMSOnline.My.Resources.RENlogo
            Me.PictureBox2.BackgroundImage = MMSOnline.My.Resources.Renadr
            localareacode = 403
        End If
        If mybosstorecode = "GAL" Then
            Me.PictureBox2.BackgroundImage = MMSOnline.My.Resources.GALadr
            Me.PictureBox3.BackgroundImage = MMSOnline.My.Resources.GALlogo
            localareacode = 604
        End If
        If mybosstorecode = "OGO" Then
            Me.PictureBox3.BackgroundImage = MMSOnline.My.Resources.OGOlogo
            Me.PictureBox2.BackgroundImage = MMSOnline.My.Resources.Ogoadr
            localareacode = 250
        End If
        If mybosstorecode = "BTH" Then
            Me.PictureBox3.BackgroundImage = MMSOnline.My.Resources.BTHlogopng
            Me.PictureBox2.BackgroundImage = MMSOnline.My.Resources.BTHadr
            localareacode = 250
        End If
        If mybosstorecode = "KEL" Then
            Me.PictureBox3.BackgroundImage = MMSOnline.My.Resources.ATLlogo
            Me.PictureBox2.BackgroundImage = MMSOnline.My.Resources.Ogoadr
            localareacode = 250
        End If
    End Sub

    Private Sub permisions()

        btndoesdisplay(Me.btnBizmanChange, persistant.myuserLEVEL, 4)
        If persistant.myuserLEVEL < 7 Then
            Select Case Me.cmbDealStatus.SelectedItem
                Case "Pending"
                Case "Conditions Removed"
                Case "Quote"
                Case "Delivered"
                    If persistant.myuserLEVEL < 4 Then
                        lockedmode = True
                    End If
                Case "Waiting for Originals"
                    If persistant.myuserLEVEL < 4 Then
                        lockedmode = True
                    End If
                Case Else
                    lockedmode = True
            End Select
        End If
        If persistant.myuserID.ToLower <> Me.txtsalesman.Text.ToLower And persistant.myuserID.ToLower <> Me.txtsalesman2.Text.ToLower Then
            If persistant.myuserLEVEL < 6 Then
                If Me.mybosstorecode <> persistant.mystoreCODE Then
                    btndoesdisplay(Me.btnChangeSalesman, persistant.myuserLEVEL, 4)
                    btndoesdisplay(Me.btnSplitDeal, persistant.myuserLEVEL, 4)
                    btndoesdisplay(Me.btnVoid, persistant.myuserLEVEL, 4)
                    'btndoesdisplay(Me.btnReleaseBoat, persistant.myuserLEVEL, 4)
                    lockedmode = True
                Else
                    btndoesdisplay(Me.btnChangeSalesman, persistant.myuserLEVEL, 4)
                    btndoesdisplay(Me.btnSplitDeal, persistant.myuserLEVEL, 4)
                    btndoesdisplay(Me.btnVoid, persistant.myuserLEVEL, 4)
                    'btndoesdisplay(Me.btnReleaseBoat, persistant.myuserLEVEL, 4)
                    btndoesdisplay(Me.btnReleaseBoat, persistant.myuserLEVEL, 9)
                    If persistant.mystore = "Admin" Then
                        btnVoid.Enabled = True
                        btnReleaseBoat.Visible = True
                    End If
                    If persistant.myuserLEVEL < 4 Then lockedmode = True
                End If
            End If
        End If
        cmbDealStatus.Enabled = False

        If persistant.myuserLEVEL > 7 Then
            txtinssold.ReadOnly = False
            txtPTcost.ReadOnly = False
            cmbDealStatus.Enabled = True
        End If
        If persistant.myuserLEVEL > 3 Then
            If persistant.myuserLEVEL > 7 Then
                txtwarrcost.Enabled = True
                txtreserve.Enabled = True
            Else
                If Me.cmbDealStatus.SelectedItem = "Delivered" Or Me.cmbDealStatus.SelectedItem = "Pending" Or Me.cmbDealStatus.SelectedItem = "Conditions Removed" Or Me.cmbDealStatus.SelectedItem = "Waiting for Originals" Then
                    txtwarrcost.Enabled = True
                    txtreserve.Enabled = True
                End If
            End If
        End If
        If persistant.myuserLEVEL < 4 Then
            txtreserve.Visible = False
            Label80.Visible = False
            cmbDealStatus.Enabled = False
            CmbShopStatus.Enabled = False
            txtfinnotes.Visible = False
            txtwarrcost.Visible = False
            Label78.Visible = False
            GroupBox15.Visible = False
        End If
        If persistant.myuserLEVEL < 4 Then
            cmbFinanceStatus.Enabled = False
            cmbGPR.Enabled = False
            Panel3.Enabled = False
        End If
        If persistant.myuserLEVEL = 1 Then
            lockedmode = False
        End If
        If lockedmode = True Then
            Me.Panel1.Enabled = False
            Me.GroupBox1.Enabled = False
            Me.GroupBox3.Enabled = False
            Me.GroupBox5.Enabled = False
            Me.Panel3.Enabled = False
        End If

        'old values were admin,Rob,Dank
        If persistant.myuserLEVEL > 8 Then ' persistant.myuserID = "admin" Or persistant.myuserID = "Rob" Or persistant.myuserID = "Dank" Or persistant.myuserID = "Dave" Then
            Me.cmbCommissioned.Enabled = True
        End If

        'old values were admin,Wallee,Nikky,Rob,Dank,Inventory,persistant.myuserID = "Brian" Or 
        If persistant.myuserLEVEL = 6 Or persistant.myuserLEVEL > 8 Then
            Me.btnUnlockBOS.Visible = True
            Me.btnBOprofitsheet.Visible = True
        End If

        ''old values were admin,Wallee,Nikky,Duncan
        'If persistant.myuserLEVEL = 6 Or persistant.myuserLEVEL > 8 Then
        'End If

        If txtkickerserial.Text <> "" And Me.txttplateserial.Text <> "" Then
            txto8.Enabled = False
            txto8price.Enabled = False
        Else
            txto8.Enabled = True
            txto8price.Enabled = True
        End If
        If Me.cmbDealStatus.SelectedItem = "Pending" Or Me.cmbDealStatus.SelectedItem = "Conditions Removed" Then
            Me.btnDelivered.Visible = True
        End If

        If persistant.myuserLEVEL > 7 Then
            btnDoneComm.Visible = True
            txtcommrate1.Enabled = True
            txtcommrate2.Enabled = True
            txtcommrate3.Enabled = True
            txtcommrate4.Enabled = True
            txtcommrate5.Enabled = True
            txtcommrate6.Enabled = True
            txtcommrate7.Enabled = True
            txtcommrate8.Enabled = True
            txtBOcommadjust.Enabled = True
            txtBOcommadjustnote.Enabled = True
            txtcommchange1.Enabled = True
            txtcommchange2.Enabled = True
            txtchange1note.Enabled = True
            txtchange2note.Enabled = True
        End If
        If cmbDealStatus.SelectedItem = "Quote" Then
            btnCreateWO.Visible = False
            btnReleaseBoat.Visible = True
        Else
            btnCreateWO.Visible = True
            btnReleaseBoat.Visible = False
            If txtwonumber.Text = "" Then
                btnCreateWO.Text = "Create WO"
            Else
                btnCreateWO.Text = "View WO"
            End If
        End If
        If persistant.myuserLEVEL < 5 Then
            If persistant.myuserID <> "BrianM" And persistant.myuserID <> "Walter" Then
                ChkGST.Visible = False
                chkPST.Visible = False
            End If
            'If cmbDealStatus.SelectedItem = "Quote" Then
            '    btnReleaseBoat.Visible = True
            'Else
            '    btnReleaseBoat.Visible = False
            'End If
        End If



    End Sub

    Private Sub setupnewbos()
        setuparray()
        myboat = 0
        oldboat = 0
        ordernumber = 0
        dealstatus = 0
        fandistatus = 1
        shopstatus = 1
        lockedmode = False
        Me.cmbCommissioned.SelectedItem = "NO"
        mybosstore = persistant.getvalue(persistant.tbl_location, "store", "Code = '" + mybosstorecode + "'", 0)
        Dim BOSQuote As Integer = persistant.getvalue(persistant.tbl_location, "StartBOSQuoteNum", "Code = '" + mybosstorecode + "'", 0)
        'Select next bos number for store
        'Start with a quote number
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            'cmd.CommandText = "Select CASE WHEN  ISNULL(Max(bos_number)) then 500000 else Max(bos_number) + 1 END from bos where store = '" + mybosstorecode + "' and BOS_number >= 500000"
            cmd.CommandText = "Select CASE WHEN  ISNULL(Max(bos_number)) then " & BOSQuote.ToString & " else Max(bos_number) + 1 END from bos where store = '" & mybosstorecode & "' and BOS_number >= " & BOSQuote
            ' "Select Max(bos_number) from bos where store = '" + mybosstorecode + "'"
            mybosnumber = Val(cmd.ExecuteScalar.ToString)
            'mybosnumber = mybosnumber + 1
            mydata(0).value = mybosnumber
            mydata(0).txtbox.text = mybosnumber.ToString

            cmd.Dispose()
            conn.Close()
        Catch ex As Exception
            Me.Close()
            MessageBox.Show("A problem was encountered. Please try again")
        End Try
        conn.Dispose()

        beeninserted = False
        Me.txtBOS.Text = mybosnumber
        mydata(0).value = mybosnumber
        Me.txtadminfee.Text = 299
        moneychanged(Me.txtadminfee)
        Me.DateSold.Value = DateTime.Now
        mydata(1).value = DateTime.Now.ToShortDateString
        cmbahtype.SelectedItem = "7 Day Retro"
        cmbDealStatus.SelectedItem = "Pending"
        CmbShopStatus.SelectedItem = "Not Submited"
        cmbFinanceStatus.SelectedItem = "Unsubmited"
        cmbTerm.SelectedItem = "60"
        cmbammort.SelectedItem = "180"
        cmblins.SelectedItem = "Buyer"
        cmbains.SelectedItem = "Buyer"
        cmbcins.SelectedItem = "Declined"
        cmbAccidentalPlus.SelectedItem = "Declined"

        CmbPTfabric.SelectedItem = "NO"
        CmbPTinstalled.SelectedItem = "YES"
        CmbPTreg.SelectedItem = "NO"
        CmbPTrtb.SelectedItem = "NO"
        cmbGPR.SelectedItem = "NONE"
        cmbcstype.SelectedItem = "Sterndrive"
        cmbcsfuel.SelectedItem = "Gas"
        cmbcsdeduct.SelectedItem = "$100"

        'Set Salesman
        If persistant.mystoreCODE = "ADM" Then
            If persistant.temppass <> "None" Then mydata(114).value = persistant.getvalue(persistant.tbl_users, "User", "name = '" + persistant.temppass + "'", 0)
        Else
            mydata(114).value = persistant.myuserID
        End If

        Select Case persistant.getvalue(persistant.tbl_location, "Prov", "Code = '" + mybosstorecode + "'", 0)
            Case "AB"
                chkPST.Checked = False
            Case "BC"
                chkPST.Checked = True
        End Select
        txtbatteries.Text = "2"
        txttires.Text = "5"
        stringchanged(txtbatteries)
        stringchanged(txttires)
        txtnotes1.Text = persistant.getvalue(persistant.tbl_printdata, "DRAW", "Data = 'note'", 0)
        stringchanged(txtnotes1)
        'txtfinancefee.Text = 199
        moneychanged(txtfinancefee)
        CSextwarr.Text = "6"
        CSmanwarr.Text = "2"
        stringchanged(CSextwarr)
        stringchanged(CSmanwarr)

    End Sub

    Private Sub builtlists()
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_status, "code <> 1") - 1)
            cmbDealStatus.Items.Add(persistant.getvalue(persistant.tbl_status, "State", "code <> 1", x))
        Next
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_financestatus, "") - 1)
            cmbFinanceStatus.Items.Add(persistant.getvalue(persistant.tbl_financestatus, "State", "", x))
        Next
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_shopstatus, "") - 1)
            CmbShopStatus.Items.Add(persistant.getvalue(persistant.tbl_shopstatus, "State", "", x))
        Next
        For x As Integer = 0 To (persistant.howmanyrows(persistant.tbl_banks, "") - 1)
            ListBanks.Items.Add(persistant.getvalue(persistant.tbl_banks, "Bank", "", x))
        Next
        DateWarrantystart.Value = DateTime.Now

    End Sub

    Public Sub downloaddata()
        setuparray()

        bosdata.bos = mybosnumber
        bosdata.connectionString = persistant.myconnstring
        bosdata.store = mybosstorecode
        bosdata.Download()


        ordernumber = Val(bosdata.getvalue("ordernumber"))
        myboat = Val(bosdata.getvalue("Control_Number"))
        oldboat = myboat

        invdata.persistant = persistant
        invdata.Clear()
        invdata.Control_number = myboat
        invdata.readDB()
        If myboat > 2 Then
            If Val(invdata.bos_num) <> Me.mybosnumber And Val(invdata.bos_num) <> 0 And usedquoteboat <> myboat Then
                'This boat is taken by another deal, set this deal back to a quote and leave that boat as it is.  'Move this to download data.
                'Add global marker for leave deal as quote if myboat is still this boat.
                MessageBox.Show("Sorry.  The Boat you Wrote this Quote on is No Longer Available.")
                Me.cmbDealStatus.SelectedItem = "Quote"
                usedquoteboat = myboat
            End If
        End If


        For x As Integer = 0 To mydatacount
            If mydata(x).dbtable = "bos" Then mydata(x).value = bosdata.getvalue(mydata(x).dbfeild)
            If mydata(x).dbtable = "inventory" And myboat <> 0 Then mydata(x).value = invdata.getvalue(mydata(x).dbfeild)
        Next

        If Val(bosdata.getvalue("bankfeeoverride")) = 0 Then
            FandI.bankfeeoverride = 0
        Else
            FandI.bankfeeoverride = Val(bosdata.getvalue("bankfeeoverride"))
            txtBankFee.Text = Format(Val(bosdata.getvalue("bankfeeoverride")), "currency")
        End If

        If bosdata.getvalue("gpr") = "" Then
            cmbGPR.SelectedItem = "NONE"
            cmbcstype.SelectedItem = "Sterndrive"
        Else
            cmbGPR.SelectedItem = bosdata.getvalue("gpr")
        End If

        If bosdata.getvalue("PTreg") = "" Then
            CmbPTfabric.SelectedItem = "NO"
            CmbPTinstalled.SelectedItem = "YES"
            CmbPTreg.SelectedItem = "NO"
            CmbPTrtb.SelectedItem = "NO"
        Else
            CmbPTfabric.SelectedItem = bosdata.getvalue("PTfabric")
            CmbPTinstalled.SelectedItem = bosdata.getvalue("PTapplied")
            CmbPTreg.SelectedItem = bosdata.getvalue("PTreg")
            CmbPTrtb.SelectedItem = bosdata.getvalue("PTrtbs")
        End If
        If bosdata.getvalue("warproducttype") <> "" Then
            cmbcstype.SelectedItem = bosdata.getvalue("warproducttype")
        Else
            cmbcstype.SelectedItem = "Sterndrive"
        End If

        dealstatus = Val(bosdata.getvalue("Status"))
        fandistatus = Val(bosdata.getvalue("Finance_Status"))
        shopstatus = Val(bosdata.getvalue("Shop_Status"))
        txtbday1.Text = bosdata.getvalue("Bday1")
        txtbday2.Text = bosdata.getvalue("Bday2")
        If Val(bosdata.getvalue("total")) > 0 Then
            If Val(bosdata.getvalue("PST")) = 0 Then
                chkPST.Checked = False
            End If
            If Val(bosdata.getvalue("GST")) = 0 Then
                ChkGST.Checked = False
            End If
        End If


        If Val(bosdata.getvalue("locked")) = 1 Then
            lockedmode = True
        Else
            If lockedmode = False Then
                If persistant.myuserLEVEL > 6 Then
                    bosdata.locked(True, persistant.myuserID)
                End If
                If persistant.myuserLEVEL > 5 Then
                    If dealstatus < 6 Then
                        bosdata.locked(True, persistant.myuserID)
                    End If
                End If
                If persistant.myuserLEVEL > 3 Then
                    If dealstatus < 6 And Me.mybosstorecode = persistant.mystoreCODE Then
                        bosdata.locked(True, persistant.myuserID)
                    End If
                End If
                If persistant.myuserID.ToLower = Me.txtsalesman.Text.ToLower Or persistant.myuserID.ToLower = Me.txtsalesman2.Text.ToLower Then
                    If dealstatus < 4 Then
                        bosdata.locked(True, persistant.myuserID)
                    End If
                End If
            End If
        End If


        If Replace(bosdata.getvalue("Date_Delivered"), " ", "") <> "" Then
            Me.txtdatedeled.Text = CDate(bosdata.getvalue("Date_Delivered")).ToShortDateString
        End If
        If bosdata.getvalue("commisoned") = "YES" Then
            Me.cmbCommissioned.SelectedItem = "YES"
        Else
            Me.cmbCommissioned.SelectedItem = "NO"
        End If

        If Val(bosdata.getvalue("fincode")) = 1 Then
            'CASH SALE
            Me.rcash.Checked = True
            Me.rfin.Checked = False
            Me.rins.Visible = False
            Me.runins.Visible = False
            Me.txtinssold.Visible = False
        End If
        If Val(bosdata.getvalue("fincode")) = 2 Then
            'Financed not insured
            Me.rcash.Checked = False
            Me.rfin.Checked = True
            Me.rins.Checked = False
            Me.runins.Checked = True
        End If
        If Val(bosdata.getvalue("fincode")) = 3 Then
            'INSURED
            Me.rcash.Checked = False
            Me.rfin.Checked = True
            Me.rins.Checked = True
            Me.runins.Checked = False
            Me.txtinssold.Text = Format(Val(bosdata.getvalue("inssold")), "currency")
        End If
        If Val(bosdata.getvalue("fincode")) <> 0 Then
            Panel5.Visible = True
        End If
        If persistant.myuserLEVEL < 8 Then
            Me.btnReleaseBoat.Visible = False
        End If

        If Me.rfin.Checked = True Then
            Panel4.Visible = True
        Else
            Panel4.Visible = False
        End If


    End Sub

    Private Sub WritetoScreen()

        For x As Integer = 0 To 26
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
        Next
        For x As Integer = 27 To 29
            If mydata(x).value <> "" Then mydata(x).txtbox.text = Format(CDate(mydata(x).value), "dd/MM/yyyy")
        Next
        'when this runs it wipes out the tradelein value stored at 122
        'I have no idea why this is happening so I save the value
        'and reset it after
        Dim saveTradeLienVal As String = mydata(122).value
        For x As Integer = 30 To 41
            'MsgBox(x & " 122 before " & mydata(122).value)
            If mydata(x).value.ToString = "1" Or mydata(x).value.ToString = "True" Then mydata(x).txtbox.checked = True
            'MsgBox(x & " 122 after " & mydata(122).value)
        Next
        mydata(122).value = saveTradeLienVal
        For x As Integer = 42 To 78
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
            moneychanged(mydata(x).txtbox)
        Next

        For x As Integer = 79 To 84
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
            If myboat = 1 Then mydata(x).txtbox.text = ""
        Next

        For x As Integer = 93 To 147
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
            If x > 105 And x < 114 Then If myboat = 1 Then mydata(x).txtbox.text = ""
        Next
        If mydata(148).value <> "" Then
            mydata(148).txtbox.text = mydata(148).value
            moneychanged(mydata(148).txtbox)
        End If
        If mydata(149).value <> "" Then mydata(149).txtbox.text = Format(CDate(mydata(149).value), "dd/MM/yyyy")
        moneychanged(Me.txtfinancefee)
        lblFFee.Text = txtfinancefee.Text
        For x As Integer = 85 To 92
            If mydata(x).value <> "" Then mydata(x).txtbox.text = mydata(x).value
        Next

        GetNewCallData()

        If ordernumber > 0 Then
            order.persistant = persistant
            order.vieworder(ordernumber)
            Me.txtboatmake.Text = order.boatmake
            Me.txtboatmodel.Text = order.boatmodel
            Me.txtboathin.Text = ""
            Me.txtboatunit.Text = ""
            Me.txtcolor.Text = order.colour
            Me.txtboatyear.Text = order.boatyear
            Me.txtboatprice.Text = Format(order.price, "currency")
            moneychanged(Me.txtboatprice)
            Me.txtmotormake.Text = order.motormake
            Me.txtmotormodel.Text = order.motormodel
            Me.txtmotorserial.Text = ""
            Me.txtmotoryear.Text = ""
            Me.txtmotorprice.Text = ""
            Me.txttraileryear.Text = ""
            moneychanged(Me.txtmotorprice)
        End If

        Me.cmbDealStatus.SelectedItem = persistant.getvalue(persistant.tbl_status, "State", "code = '" + dealstatus.ToString + "'", 0)
        Me.cmbFinanceStatus.SelectedItem = persistant.getvalue(persistant.tbl_financestatus, "State", "code = '" + fandistatus.ToString + "'", 0)
        Me.CmbShopStatus.SelectedItem = persistant.getvalue(persistant.tbl_shopstatus, "State", "code = '" + shopstatus.ToString + "'", 0)
        If Me.beeninserted Then
            If bosdata.getvalue("bank") <> "" Then Me.ListBanks.SelectedItem = persistant.getvalue(persistant.tbl_banks, "Bank", "Code = '" + bosdata.getvalue("bank") + "'", 0)
            'If Me.ListBanks.SelectedIndex <> -1 Then updaterates()
            If bosdata.getvalue("rate") <> "" And Me.cmbrate.SelectedIndex = -1 Then txtrateover.Text = bosdata.getvalue("rate")
            If bosdata.getvalue("term") <> "" Then Me.cmbTerm.SelectedItem = bosdata.getvalue("term")
            If bosdata.getvalue("ammort") <> "" Then Me.cmbammort.SelectedItem = bosdata.getvalue("ammort")
            If bosdata.getvalue("ammort") <> "" And Me.cmbammort.SelectedIndex = -1 Then Me.txtammover.Text = bosdata.getvalue("ammort")
            If bosdata.getvalue("Life_insurance") <> "" Then Me.cmblins.SelectedItem = bosdata.getvalue("Life_insurance")
            If bosdata.getvalue("AH_Insurance") <> "" Then Me.cmbains.SelectedItem = bosdata.getvalue("AH_Insurance")
            If bosdata.getvalue("CI_Insurance") <> "" Then Me.cmbcins.SelectedItem = bosdata.getvalue("CI_Insurance")
            If bosdata.getvalue("APlus_Insurance") <> "" Then Me.cmbAccidentalPlus.SelectedItem = bosdata.getvalue("APlus_Insurance")
            If bosdata.getvalue("ahtype") <> "" Then Me.cmbahtype.SelectedItem = bosdata.getvalue("ahtype")
        End If

        If myboat > 0 Then
            Me.btnDetails.Visible = True
            Me.btnReleaseBoat.Visible = True
            Me.btnPickABoat.Visible = False
            Me.btnOrder.Visible = False
        End If
        If myboat = 1 And persistant.myuserLEVEL > 7 Then
            GroupBox17.Visible = True
        End If
        If ordernumber > 0 Then
            Me.btnDetails.Visible = True
            Me.btnReleaseBoat.Visible = True
            Me.btnPickABoat.Visible = False
            Me.btnOrder.Visible = False
            btnReleaseBoat.Text = "Cancel Order"
            btnDetails.Text = "Order Details"
        End If
        If ordernumber = 0 And myboat < 3 Then
            Me.btnDetails.Visible = False
            Me.btnReleaseBoat.Visible = False
            Me.btnPickABoat.Visible = True
            Me.btnOrder.Visible = True
        End If
        updatemath()
        assumedata()
        If txtwonumber.Text <> "0" And txtwonumber.Text <> "" Then
            txtwostore.Text = mybosstorecode
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            conn.ConnectionString = persistant.myconnstring
            Try
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = "SELECT Status from ServiceWO where Number = " + txtwonumber.Text + " and Store = '" + txtwostore.Text + "'"
                txtwostatus.Text = cmd.ExecuteScalar
                cmd.CommandText = "SELECT DateReqComp from ServiceWO where Number = " + txtwonumber.Text + " and Store = '" + txtwostore.Text + "'"
                txtwodate.Text = cmd.ExecuteScalar
                conn.Close()
            Catch ex As Exception
                MessageBox.Show("Error Connecting to Database: " & ex.Message)
            End Try
            If conn.State <> ConnectionState.Closed Then conn.Close()
            conn.Dispose()
        Else
            txtwostatus.Text = ""
            txtwodate.Text = ""
        End If
        If txtwonumber.Text = "" Then
            btnCreateWO.Text = "Create WO"
        Else
            btnCreateWO.Text = "View WO"
        End If

    End Sub

    Public Sub selectboat(ByVal boat As Integer)
        ordernumber = 0
        oldboat = myboat
        myboat = boat
        Me.cmbDealStatus.SelectedItem = "Quote"
        Dim success As Boolean = save()
        If success Then
            lockedmode = False
            WritetoScreen()
        End If

    End Sub

    Private Function soldby() As String
        Dim temp As String
        temp = ""

        For x As Integer = 42 To 68
            If mydata(x).changed = True Then
                If x = 42 Then temp = temp + "s_tire_SB = '" + Me.persistant.myuserID + "', "
                If x = 43 Then temp = temp + "Cover_SB = '" + Me.persistant.myuserID + "', "
                If x = 44 Then temp = temp + "Ski_Pack_SB = '" + Me.persistant.myuserID + "', "
                If x = 45 Then temp = temp + "Rock_Guard_SB = '" + Me.persistant.myuserID + "', "
                If x = 46 Then temp = temp + "S_prop_SB = '" + Me.persistant.myuserID + "', "
                If x = 47 Then temp = temp + "Safety_Pkg_SB = '" + Me.persistant.myuserID + "', "
                If x = 48 Then temp = temp + "lock_Pkg_SB = '" + Me.persistant.myuserID + "', "
                If x = 49 Then temp = temp + "o1_SB = '" + Me.persistant.myuserID + "', "
                If x = 50 Then temp = temp + "o2_SB = '" + Me.persistant.myuserID + "', "
                If x = 51 Then temp = temp + "o3_SB = '" + Me.persistant.myuserID + "', "
                If x = 52 Then temp = temp + "o4_SB = '" + Me.persistant.myuserID + "', "
                If x = 53 Then temp = temp + "o5_SB = '" + Me.persistant.myuserID + "', "
                If x = 54 Then temp = temp + "o6_SB = '" + Me.persistant.myuserID + "', "
                If x = 55 Then temp = temp + "o7_SB = '" + Me.persistant.myuserID + "', "
                If x = 56 Then temp = temp + "o8_SB = '" + Me.persistant.myuserID + "', "
                If x = 64 Then temp = temp + "Antitheft_SB = '" + Me.persistant.myuserID + "', "
                If x = 65 Then temp = temp + "protech_SB = '" + Me.persistant.myuserID + "', "
                If x = 66 Then temp = temp + "ext_warranty_SB = '" + Me.persistant.myuserID + "', "
                If x = 67 Then temp = temp + "20_hour_SB = '" + Me.persistant.myuserID + "', "
                If x = 68 Then temp = temp + "winterize_SB = '" + Me.persistant.myuserID + "', "
            End If
        Next
        soldby = temp

    End Function

    Private Function save() As Boolean

        'check for required fields
        ' First and last name, city, phone number and email address
        If Trim(txtbuyer1last.Text) = "" Or _
            Trim(txtbuyer1first.Text) = "" Or _
            Trim(txtcity.Text) = "" Or _
            Trim(txthome.Text) = "" Or _
            Trim(txtemail.Text) = "" Then
            MessageBox.Show("This quote cannot be saved because one of the required fields has not been filled in.")
            Return False
        End If
        If Me.cmbDealStatus.SelectedItem <> "Quote" Then
            'if it is a bos then add the required fields of work order and parts orders, all fields for customer info must be completed, except for single name
            If Trim(txtaddress.Text) = "" Or _
                Trim(txtprov.Text) = "" Or _
                Trim(txtpostal.Text) = "" Then
                MessageBox.Show("This quote cannot be saved because one of the required fields has not been filled in.")
                Return False
            End If

        End If


        Me.btnPrintBOS.Select()

        Dim templockedstate As Boolean
        templockedstate = lockedmode
        Dim tempstring As String
        tempstring = Me.ToolStripStatusLabel1.Text
        Me.ToolStripStatusLabel1.Text = "Saving..."
        Me.Refresh()
        Dim boatbosnum As String = ""

        For x As Integer = 0 To mydatacount
            mydata(x).value = Replace(mydata(x).value, "'", "\'")
            mydata(x).value = Replace(mydata(x).value, ";", "\;")
        Next

        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        conn.Open()
        cmd.Connection = conn
        Dim cmdstring, dealstatus As String

        dealstatus = persistant.getvalue(persistant.tbl_status, "code", "State = '" & Me.cmbDealStatus.SelectedItem & "'", 0)

        Dim BOSQuote As Integer
        Dim BOSNew As Integer
        If mybosstorecode = "ATL" Then
            BOSQuote = 500000
            BOSNew = 2
        Else
            BOSQuote = persistant.getvalue(persistant.tbl_location, "StartBOSQuoteNum", "Code = '" & mybosstorecode & "'", 0)
            BOSNew = persistant.getvalue(persistant.tbl_location, "StartBOSNum", "Code = '" & mybosstorecode & "'", 0)
        End If

        'Dim BOSQuote As Integer = persistant.getvalue(persistant.tbl_location, "StartBOSQuoteNum", "Code = '" & mybosstorecode & "'", 0)
        'Dim BOSNew As Integer = persistant.getvalue(persistant.tbl_location, "StartBOSNum", "Code = '" & mybosstorecode & "'", 0)
        If beeninserted Then
            cmd.CommandText = "Select Status from bos where store = '" & mybosstorecode & "' AND bos_number = '" & mybosnumber.ToString & "'"
            'Status 0 is equal to a quote
            If Val(dealstatus) = 0 Then dealstatus = cmd.ExecuteScalar

            If Val(dealstatus) = 0 Then
                'mydata76=Payment1_ammount   ,mydata77=Payment2_ammount    ,mydata78=Payment3_ammount   ,txtp1num.text is payment receipt #      And Replace(Me.txtp1num.Text, " ", "") <> ""
                If (Val(mydata(76).value) + Val(mydata(77).value) + Val(mydata(78).value) + Val(mydata(148).value)) > 0 And usedquoteboat <> myboat Then
                    Me.cmbDealStatus.SelectedItem = "Pending"
                    dealstatus = "2"
                    Me.DateSold.Value = Now
                    mydata(1).value = Now.ToShortDateString
                    mydata(1).changed = True
                    Dim newbosnum As Integer ', maxrealbos, maxquote
                    newbosnum = 0
                    cmd.CommandText = "Select CASE WHEN ISNULL(Max(bos_number)) then " & BOSNew.ToString & " else Max(bos_number) + 1 END  from bos where store = '" & mybosstorecode & "' and BOS_number < " & BOSQuote & " and BOS_number >= " & BOSNew
                    'cmd.CommandText = "Select Max(bos_number) from bos where store = '" + mybosstorecode + "' AND Status <> '0'"
                    'maxrealbos = Val(cmd.ExecuteScalar.ToString)
                    'cmd.CommandText = "Select Max(bos_number) from bos where store = '" + mybosstorecode + "'"
                    'maxquote = Val(cmd.ExecuteScalar.ToString)
                    newbosnum = Val(cmd.ExecuteScalar.ToString)
                    'newbosnum = maxrealbos + 1
                    'If newbosnum <> mybosnumber Then
                    ''cmd.CommandText = "Update bos set bos_number = '" + (maxquote + 1).ToString + "' where bos_number = '" + newbosnum.ToString + "' AND store = '" + mybosstorecode + "'"
                    'cmd.ExecuteNonQuery()
                    cmd.CommandText = "Update bos set bos_number = '" & newbosnum.ToString & "' where bos_number = '" & mybosnumber.ToString & "' AND store = '" & mybosstorecode & "'"
                    cmd.ExecuteNonQuery()
                    ' cmd.CommandText = "Update bos set bos_number = '" + mybosnumber.ToString + "' where bos_number = '" + (maxquote + 1).ToString + "' AND store = '" + mybosstorecode + "'"
                    ' cmd.ExecuteNonQuery()
                    mybosnumber = newbosnum
                    mydata(0).value = mybosnumber
                    mydata(0).txtbox.text = mybosnumber.ToString

                    'End If
                End If
            End If
            cmdstring = "UPDATE bos SET "
            'if the user is Dan or Rob then allow the date to change
            If persistant.myuserID.ToLower = "dank" Or persistant.myuserID.ToLower = "rob" Or persistant.myuserID.ToLower = "dave" Then
                mydata(1).value = DateSold.Value
                mydata(1).changed = True
            End If
        Else

            If (Val(mydata(76).value) + Val(mydata(77).value) + Val(mydata(78).value) + Val(mydata(148).value)) > 0 Then 'And Replace(Me.txtp1num.Text, " ", "") <> ""
                Me.cmbDealStatus.SelectedItem = "Pending"
                dealstatus = "2"
                Dim newbosnum As Integer ', maxrealbos
                newbosnum = 0
                cmd.CommandText = "Select CASE WHEN ISNULL(Max(bos_number)) then " & BOSNew.ToString & " else Max(bos_number) + 1 END  from bos where store = '" & mybosstorecode & "' and BOS_number < " & BOSQuote & " and BOS_number >= " & BOSNew
                'cmd.CommandText = "Select Max(bos_number) + 1 from bos where store = '" + mybosstorecode + "' and BOS_number < " & BOSQuote
                'cmd.CommandText = "Select Max(bos_number) from bos where store = '" + mybosstorecode + "' AND Status <> '0'"
                'maxrealbos = Val(cmd.ExecuteScalar.ToString)
                mybosnumber = Val(cmd.ExecuteScalar.ToString)
                'mybosnumber = maxrealbos + 1
            Else
                'Use a quote number
                cmd.CommandText = "Select CASE WHEN ISNULL(Max(bos_number)) then " & BOSQuote.ToString & " else Max(bos_number) + 1 END from bos where store = '" & mybosstorecode & "' and BOS_number >= " & BOSQuote
                'cmd.CommandText = "Select Max(bos_number) from bos where store = '" + mybosstorecode + "'"
                mybosnumber = Val(cmd.ExecuteScalar.ToString)
            End If
            cmdstring = "INSERT into bos set locked = '1', lockedby = '" & persistant.myuserID & "', "
        End If

        For x As Integer = 0 To mydatacount
            If mydata(x).dbtable = "bos" And mydata(x).changed = True Then
                If x = 1 Or x = 27 Or x = 28 Or x = 29 Or x = 118 Or x = 149 Then
                    cmdstring = cmdstring & mydata(x).dbfeild & " = '" & Format(CDate(mydata(x).value), "yyyy-MM-dd") & "', "
                Else
                    cmdstring = cmdstring & mydata(x).dbfeild & " = '" & mydata(x).value & "', "
                End If
            End If
        Next
        If ordernumber = 0 And myboat > 2 Then
            cmdstring = cmdstring & "control_number = '" & myboat.ToString & "', "
            cmdstring = cmdstring & "ordernumber = '0', "
        End If

        If ordernumber > 0 Then
            cmdstring = cmdstring & "control_number = '2', "
            cmdstring = cmdstring & "ordernumber = '" & ordernumber.ToString & "', "
        End If
        If ordernumber = 0 And myboat < 3 Then
            cmdstring = cmdstring & "ordernumber = '0', "
            cmdstring = cmdstring & "control_number = '1', "
        End If

        cmdstring = cmdstring & "Status = '" & dealstatus & "', "
        cmdstring = cmdstring & "Finance_Status = '" & persistant.getvalue(persistant.tbl_financestatus, "code", "State = '" & Me.cmbFinanceStatus.SelectedItem & "'", 0) & "', "
        cmdstring = cmdstring & "Shop_Status = '" & persistant.getvalue(persistant.tbl_shopstatus, "code", "State = '" & Me.CmbShopStatus.SelectedItem + "'", 0) & "', "
        If Me.ListBanks.SelectedIndex <> -1 Then cmdstring = cmdstring & "Bank = '" & persistant.getvalue(persistant.tbl_banks, "code", "Bank = '" & Me.ListBanks.SelectedItem & "'", 0) & "', "
        If Me.cmbTerm.SelectedIndex <> -1 Then cmdstring = cmdstring & "term = '" & cmbTerm.SelectedItem.ToString & "', "

        If Me.txtammover.Text = "" Then
            If Me.cmbammort.SelectedIndex <> -1 Then cmdstring = cmdstring & "ammort = '" & cmbammort.SelectedItem.ToString & "', "
        Else
            If Me.txtammover.Text <> "" Then cmdstring = cmdstring & "ammort = '" & Me.txtammover.Text & "', "
        End If

        If Me.txtrateover.Text = "" Then
            'If Me.cmbrate.SelectedIndex <> -1 Then cmdstring = cmdstring + "rate = '" + cmbrate.SelectedItem.ToString + "', "
        Else
            If Me.txtrateover.Text <> "" Then cmdstring = cmdstring & "rate = '" & Me.txtrateover.Text & "', "
        End If
        If Me.cmblins.SelectedIndex <> -1 Then cmdstring = cmdstring & "Life_insurance = '" & cmblins.SelectedItem.ToString & "', "
        If Me.cmbcins.SelectedIndex <> -1 Then cmdstring = cmdstring & "CI_Insurance = '" & cmbcins.SelectedItem.ToString & "', "
        If Me.cmbains.SelectedIndex <> -1 Then cmdstring = cmdstring & "AH_Insurance = '" & cmbains.SelectedItem.ToString & "', "
        If Me.cmbahtype.SelectedIndex <> -1 Then cmdstring = cmdstring & "ahtype = '" & cmbahtype.SelectedItem.ToString & "', "
        If Me.cmbAccidentalPlus.SelectedIndex <> -1 Then cmdstring = cmdstring & "APlus_Insurance = '" & cmbAccidentalPlus.SelectedItem.ToString & "', "
        If Val(Replace(Replace(Me.txtinssold.Text, "$", ""), ",", "")) <> 0 Then cmdstring = cmdstring & "inssold = '" & Val(Replace(Replace(Me.txtinssold.Text, "$", ""), ",", "")).ToString & "', "
        If Val(Me.txthiddenfincode.Text) <> 0 Then cmdstring = cmdstring & "fincode = '" & Val(Me.txthiddenfincode.Text).ToString & "', "
        cmdstring = cmdstring & "commisoned = '" & Me.cmbCommissioned.SelectedItem.ToString & "', "
        cmdstring = cmdstring & "Bday1 = '" & txtbday1.Text & "', "
        cmdstring = cmdstring & "Bday2 = '" & txtbday2.Text & "', "
        cmdstring = cmdstring & "bankfeeoverride = '" & FandI.bankfeeoverride.ToString & "', "
        cmdstring = cmdstring & soldby()
        If txtdatedeled.Text <> "" Then
            cmdstring = cmdstring & "Date_Delivered = '" & Format(CDate(Me.txtdatedeled.Text), "yyyy-MM-dd") & "', "
        End If
        cmdstring = cmdstring & "lastmodified = '" & Format(DateTime.Now, "yyyy-MM-dd H:mm:ss") & "', "
        Try
            cmdstring = cmdstring & "warproducttype = '" & cmbcstype.SelectedItem.ToString & "', "
        Catch ex As Exception
        End Try
        cmdstring = cmdstring & "PTfabric = '" & CmbPTfabric.SelectedItem.ToString & "', "
        cmdstring = cmdstring & "PTreg = '" & CmbPTreg.SelectedItem.ToString & "', "
        cmdstring = cmdstring & "PTrtbs = '" & CmbPTrtb.SelectedItem.ToString & "', "
        cmdstring = cmdstring & "PTapplied = '" & CmbPTinstalled.SelectedItem.ToString & "', "
        cmdstring = cmdstring & "gpr = '" & cmbGPR.SelectedItem.ToString & "' "
        If beeninserted = True Then
            cmdstring = cmdstring & "WHERE bos_number = '" & mybosnumber.ToString & "' AND Store = '" & mybosstorecode & "'"
        Else
            cmdstring = cmdstring & ",Store = '" & mybosstorecode & "'" & ",bos_number = '" & Me.mybosnumber.ToString & "'" & ",salesman = '" & Me.txtsalesman.Text & "', Date_sold = '" & Format(DateTime.Now, "yyyy-MM-dd") & "'"
        End If
        cmd.CommandText = cmdstring
        Debug.WriteLine(cmdstring)

        Try
            cmd.ExecuteNonQuery()
            beeninserted = True
            If myboat > 2 Then
                cmd.CommandText = "SELECT BOS_Number from inventory where control_number = '" & myboat.ToString & "'"
                boatbosnum = cmd.ExecuteScalar().ToString
            End If
            If Me.cmbDealStatus.SelectedItem = "Pending" And ordernumber > 0 Then
                cmd.CommandText = "UPDATE orders Set status = 'Requested' WHERE ordernumber = '" & ordernumber.ToString & "'"
                cmd.ExecuteNonQuery()
            End If
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
        conn.Dispose()

        For x As Integer = 0 To 118
            mydata(x).value = Replace(mydata(x).value, "\'", "'")
            mydata(x).value = Replace(mydata(x).value, "\;", ";")
        Next
        invdata.persistant = persistant

        'Update inventory DB to reflect boat availablity
        'Cases:
        '  1) Quote - Don't change inv data.  
        '  2) Pending - If boat is avaiblable then set it to used.  If boat is mine update it.  If boat is taken give message and set deal back to quote.
        '  3) Real Deal - Update state
        '  4) Void - If boat is mine update it to be void pending.  If boat is taken leave it.
        If Val(myboat) > 2 Then
            If Me.cmbDealStatus.SelectedItem = "Pending" Then
                If Val(boatbosnum) = 0 Then
                    'boat is avalible, take it
                    invdata.Clear()
                    invdata.Control_number = myboat
                    invdata.readDB()
                    invdata.bos_store = mybosstorecode
                    invdata.bos_num = Me.mybosnumber
                    invdata.state = persistant.getvalue(persistant.tbl_status, "code", "State = '" & Me.cmbDealStatus.SelectedItem & "'", 0)
                    invdata.updateDB()
                End If
                If Val(boatbosnum) = Val(mydata(0).value) Then
                    'boat is already mine, update the status just in case
                    invdata.Clear()
                    invdata.Control_number = myboat
                    invdata.readDB()
                    invdata.bos_store = mybosstorecode
                    invdata.bos_num = mydata(0).value
                    invdata.state = persistant.getvalue(persistant.tbl_status, "code", "State = '" & Me.cmbDealStatus.SelectedItem & "'", 0)
                    invdata.updateDB()
                End If

            End If
            If Me.cmbDealStatus.SelectedItem = "Void Pending" Then
                If Val(boatbosnum) = Val(mydata(0).value) Then
                    invdata.Clear()
                    invdata.Control_number = myboat
                    invdata.readDB()
                    invdata.state = 8
                    invdata.updateDB()
                    oldboat = 0
                End If
            End If
            If Me.cmbDealStatus.SelectedItem = "Void" Then
                invdata.Clear()
                invdata.Control_number = myboat
                invdata.readDB()
                If invdata.bos_num = Val(mydata(0).value) Then
                    invdata.bos_num = Nothing
                    invdata.bos_store = ""
                    invdata.state = 1
                    invdata.updateDB()
                End If
            End If
            If Me.cmbDealStatus.SelectedItem <> "Void" And Me.cmbDealStatus.SelectedItem <> "Void Pending" And Me.cmbDealStatus.SelectedItem <> "Pending" And Me.cmbDealStatus.SelectedItem <> "Quote" Then
                'boat is already mine, update the status just in case
                invdata.Clear()
                invdata.Control_number = myboat
                invdata.readDB()
                invdata.bos_store = mybosstorecode
                invdata.bos_num = mydata(0).value
                invdata.state = persistant.getvalue(persistant.tbl_status, "code", "State = '" & Me.cmbDealStatus.SelectedItem & "'", 0)
                invdata.updateDB()
            End If
        End If
        If Val(oldboat) > 2 And Val(myboat) = 0 Then
            invdata.Clear()
            invdata.Control_number = oldboat
            invdata.readDB()
            invdata.bos_num = Nothing
            invdata.bos_store = ""
            invdata.state = 1
            invdata.updateDB()
        End If
        oldboat = 0

        downloaddata()
        Me.ToolStripStatusLabel1.Text = tempstring
        Me.Refresh()
        lockedmode = templockedstate
        Return True
    End Function

    Private Sub chkchanged(ByVal sender As Object)
        For i As Integer = 0 To (mydata.Length - 1)
            If sender.Equals(mydata(i).txtbox) = True Then
                If sender.Checked = True Then
                    mydata(i).value = 1
                Else
                    mydata(i).value = 0
                End If
                mydata(i).changed = True
            End If
        Next
    End Sub

    Private Sub stringchanged(ByVal sender As Object)
        Dim data, temp As String
        For i As Integer = 0 To (mydata.Length - 1)
            If sender.Equals(mydata(i).txtbox) = True Then
                data = sender.text
                If i = 10 Or i = 11 Then
                    temp = txt2Phone(data)
                    data = temp
                    sender.text = data
                End If
                If i = 9 Then
                    temp = txt2Postal(data)
                    data = temp
                    sender.text = data
                End If
                mydata(i).value = data
                mydata(i).changed = True
            End If
        Next
    End Sub

    Private Function txt2Postal(ByVal input As String) As String
        Dim strTemp As String
        strTemp = ""
        strTemp = Replace(input, "(", "")
        strTemp = Replace(strTemp, ")", "")
        strTemp = Replace(strTemp, "-", "")
        strTemp = Replace(strTemp, " ", "")
        If strTemp <> Nothing Then
            strTemp = strTemp.ToUpper
            If strTemp.Length = 6 Then
                txt2Postal = Mid(strTemp, 1, 3) + " " + Mid(strTemp, 4, 3)
            Else
                txt2Postal = input
            End If
        Else
            txt2Postal = input
        End If



    End Function

    Private Function txt2Phone(ByVal input As String) As String
        Dim strTemp As String
        Dim strPhone As String
        Dim strExtension As String = ""
        Dim intResult As Integer
        Dim output As String
        Dim DefaultAreaCode As String
        DefaultAreaCode = localareacode.ToString
        '
        ' Remove all the grouping characters for
        ' now. We'll add them back in later.
        '
        strTemp = Replace(input, "(", "")
        strTemp = Replace(strTemp, ")", "")
        strTemp = Replace(strTemp, "-", "")
        strTemp = Replace(strTemp, " ", "")
        strTemp = Replace(strTemp, "X", "x")

        '
        ' Break up the digits into the number and
        ' the extension, if any.
        '
        intResult = InStr(1, strTemp, "x", vbTextCompare)
        If intResult > 0 Then
            strExtension = Mid(strTemp, intResult + 1)
            strPhone = Mid(strTemp, 1, intResult - 1)
        Else
            strPhone = strTemp
        End If

        If Mid(strPhone, 1, 1) = "1" Then
            strPhone = Mid(strPhone, 2)
        End If

        If Len(strPhone) <> 7 And Len(strPhone) <> 10 Then
            txt2Phone = input
            GoTo xxx
        End If

        '
        ' Prepend the default area code
        '
        If Len(strPhone) = 7 Then
            strPhone = DefaultAreaCode + strPhone
        End If

        '
        ' Build the new phone number
        '
        output = "(" + Mid(strPhone, 1, 3) & ") " + Mid(strPhone, 4, 3) + "-" + Mid(strPhone, 7, 4)

        '
        ' Add the extension, if any
        '
        If strExtension <> "" Then
            output = output & " x" & strExtension
        End If
        txt2Phone = output

xxx:

    End Function

    Private Sub decimalchanged(ByRef sender As Object)


        Dim y As Decimal
        y = Val(sender.text)

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
                    If i = 75 Then
                        sender.text = "$0.00"
                    Else
                        sender.text = ""
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub boatdropped_killWO()
        If txtwonumber.Text <> "0" And txtwonumber.Text <> "" Then
            'A WO has been created so we need to kill it and zap the customer profile
            Dim conn As New MySqlConnection
            Dim cmd As New MySqlCommand
            Dim adapter As New MySqlDataAdapter
            Dim customerprofilenumber As String
            Dim IssuesforthisWO As New DataTable
            conn.ConnectionString = persistant.myconnstring
            Try
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = "SELECT CustomerProfile from ServiceWO where Number = " + txtwonumber.Text + " and Store = '" + txtwostore.Text + "'"
                customerprofilenumber = cmd.ExecuteScalar
                cmd.CommandText = "Update ServiceWO set CustomerProfile = '1' where Number = " + txtwonumber.Text + " and Store = '" + txtwostore.Text + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Delete from ServiceCustomers where Number = '" + customerprofilenumber + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Update ServiceWO set Status = 'Void' where Number = " + txtwonumber.Text + " and Store = '" + txtwostore.Text + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "SELECT Issue from ServiceWOtoIssue where WONumber = " + txtwonumber.Text + " and WOStore = '" + txtwostore.Text + "'"
                adapter.SelectCommand = cmd
                IssuesforthisWO.Clear()
                adapter.Fill(IssuesforthisWO)
                If IssuesforthisWO.Rows.Count > 0 Then
                    For x As Integer = 0 To IssuesforthisWO.Rows.Count - 1
                        cmd.CommandText = "Update ServiceIssue set Status = 'Not Completed' where Issue = " + IssuesforthisWO.Rows(x).Item(0).ToString + " and Status <> 'Completed'"
                        cmd.ExecuteNonQuery()
                    Next
                End If
                conn.Close()
                Dim funcs As New Functions
                funcs.wodoneremovefrompriority(txtwonumber.Text, txtwostore.Text, persistant.myconnstring)

            Catch ex As Exception
                MessageBox.Show("Error Connecting to Database: " & ex.Message)
            End Try
            If conn.State <> ConnectionState.Closed Then conn.Close()
            conn.Dispose()
            txtwonumber.Text = ""
            txtwostore.Text = ""
            stringchanged(txtwonumber)
            WritetoScreen()
        End If
    End Sub



#End Region ' Custom Functions/Subs

    Private Callsdatatable As New DataTable
    Private Callsnewdata As New DataTable
    Private Callsbindingsource As New BindingSource
    Private Callsbinded As Boolean

    Private Sub RefreshCallsDataView(ByVal data As DataTable)
        Callsdatatable.Clear()
        Callsdatatable.Merge(data)
        Callsbindingsource.ResetBindings(False)

        If Callsbinded = False Then
            Callsbindingsource.DataSource = Callsdatatable
            DVCalls.DataSource = Callsbindingsource
            Callsbinded = True
        End If
        ' Me.DVCalls.Columns("Note").Width = 220
        ' Me.DVCalls.Columns("Who").Width = 40
        If DVCalls.RowCount > 0 Then
            Me.DVCalls.Columns("View").Visible = True
            Me.DVCalls.ColumnHeadersVisible = True

        Else
            Me.DVCalls.Columns("View").Visible = False
            Me.DVCalls.ColumnHeadersVisible = True

        End If
        Me.DVCalls.Columns("Number").Visible = False

        Me.DVCalls.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells) ' .DisplayedCellsExceptHeader)

    End Sub

    Private Sub GetNewCallData()
        'Just fill newdata and pass it along to refreshdata
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adapter As New MySqlDataAdapter

        'Dim SQL As String = "SELECT Number, Who, time, Note FROM ServiceCallLog where CustProfile = '" + CustomerNumber.ToString + "' ORDER BY time DESC"
        Dim SQL As String = "SELECT Number, Who, time, Note FROM ServiceCallLog where CallType = 'BOS' and ParentID = '" & mybosnumber.ToString & "'  ORDER BY time DESC"

        conn.ConnectionString = persistant.myconnstring
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = SQL
            adapter.SelectCommand = cmd
            Callsnewdata.Clear()
            adapter.Fill(Callsnewdata)
            conn.Close()
            RefreshCallsDataView(Callsnewdata)

        Catch ex As Exception
            MessageBox.Show("Error Connecting to Database: " & ex.Message)
        End Try
        If conn.State <> ConnectionState.Closed Then conn.Close()
        conn.Dispose()
    End Sub

    Private Sub btnLogACall_Click(sender As System.Object, e As System.EventArgs) Handles btnLogACall.Click
        Dim logcall As New frmCallLog
        logcall.persistant = persistant
        'logcall.CustNumber = CustomerNumber
        logcall.CallType = "BOS"
        logcall.ParentID = mybosnumber
        'logcall.isPOCall = True
        logcall.ShowDialog()
        WritetoScreen()
    End Sub

    Private Sub DVCalls_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DVCalls.CellContentClick
        Dim xRow As Integer
        Dim XNum As Integer
        If DVCalls.Columns(e.ColumnIndex).Name = "View" Then
            xRow = e.RowIndex
            XNum = Callsdatatable.Rows(xRow).Item("Number").ToString
            Dim Viewcall As New frmCallLog
            Viewcall.persistant = persistant
            Viewcall.NoteNumber = XNum
            Viewcall.Viewnote = True
            Viewcall.ShowDialog()
        End If
        WritetoScreen()
    End Sub


End Class

Public Class element
    Public txtbox As New Object
    Public dbtable As String
    Public dbfeild As String
    Public changed As Boolean
    Public value As String
End Class

Public Class financingdata
    Public pmtpreinsur As Decimal
    Public pmt As Decimal
    Public fee As Decimal
    Public residual As Decimal
    Public residual2 As Decimal
    Public bankfee As Decimal
    Public bankfeeoverride As Decimal
    Public lifec As Decimal
    Public lifer As Decimal
    Public AandH As Decimal
    Public AccidentalPlus As Decimal
    Public Criticalc As Decimal
    Public Criticalr As Decimal
    Public Rate As Decimal
    Public Term As Integer
    Public Ammort As Integer
    Public lifebuyer As Integer
    Public AandHbuyers As Integer
    Public criticalbuyers As Integer
    Public AccidentalPlusBuyers As Integer
    Public dollerspre As Decimal
    Public dollerstotal As Decimal
End Class

