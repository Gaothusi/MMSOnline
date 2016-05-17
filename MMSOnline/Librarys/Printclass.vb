Imports System.Windows.Forms
Public Class PrintFactory
#Region "Instance"

#Region "Declerations"

    Private WithEvents _printDocument As Printing.PrintDocument = New Printing.PrintDocument()
    Private _dgv As DataGridView
    Private _stringFormat As StringFormat
    Private _stringFormatComboBox As StringFormat
    Private _button As Button
    Private _checkBox As CheckBox
    Private _comboBox As ComboBox
    Private _totalWidth As Int16
    Private _rowPos As Int16
    Private _newPage As Boolean
    Private _pageNumber As Int16
    Private _userName As String
    Private _headerText As String
    Private _blnGotNumPages As Boolean
    Private _strNumOfPages As String
    Private _orientation As String

#End Region

#Region "Properties"

    Private ReadOnly Property DGV() As DataGridView
        Get
            Return _dgv
        End Get
    End Property

    Private Property HeaderText() As String
        Get
            Return _headerText
        End Get

        Set(ByVal value As String)
            _headerText = value
        End Set

    End Property

    Private Property UserName() As String

        Get
            Return _userName
        End Get

        Set(ByVal value As String)
            _userName = value
        End Set
    End Property

    Private Property StringFormat() As StringFormat
        Get
            Return _stringFormat
        End Get

        Set(ByVal value As StringFormat)
            _stringFormat = value
        End Set
    End Property

    Private Property StringFormatComboBox() As StringFormat
        Get
            Return _stringFormatComboBox
        End Get

        Set(ByVal value As StringFormat)
            _stringFormatComboBox = value
        End Set
    End Property

    Private Property Button() As Button

        Get
            Return _button
        End Get

        Set(ByVal value As Button)
            _button = value
        End Set
    End Property

    Private Property CheckBox() As CheckBox
        Get
            Return _checkBox
        End Get

        Set(ByVal value As CheckBox)
            _checkBox = value
        End Set
    End Property

    Private Property ComboBox() As ComboBox
        Get
            Return _comboBox
        End Get

        Set(ByVal value As ComboBox)
            _comboBox = value
        End Set
    End Property

    Private Property TotalWidth() As Int16
        Get
            Return _totalWidth
        End Get

        Set(ByVal value As Int16)
            _totalWidth = value
        End Set
    End Property

    Private Property RowPos() As Int16
        Get
            Return _rowPos
        End Get

        Set(ByVal value As Int16)
            _rowPos = value
        End Set
    End Property

    Private Property NewPage() As Boolean
        Get
            Return _newPage
        End Get

        Set(ByVal value As Boolean)
            _newPage = value
        End Set
    End Property

    Private Property PageNumber() As Int16
        Get
            Return _pageNumber
        End Get

        Set(ByVal value As Int16)
            _pageNumber = value
        End Set
    End Property
    Private Property strNumOfPages() As String
        Get
            Return _strNumOfPages
        End Get

        Set(ByVal value As String)
            _strNumOfPages = value
        End Set
    End Property
    Private Property blnGotNumPages() As Boolean
        Get
            Return _blnGotNumPages
        End Get

        Set(ByVal value As Boolean)
            _blnGotNumPages = value
        End Set
    End Property
    Private Property orientation() As String

        Get
            Return _orientation
        End Get

        Set(ByVal value As String)
            _orientation = value
        End Set
    End Property
#End Region

    Private Sub New(ByVal userName As String, ByVal headerText As String)
        Me._userName = userName
        Me._headerText = headerText
    End Sub

    Private Sub _printDocument_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles _printDocument.PrintPage
        EndPrint(Me, e)
    End Sub

#End Region

#Region "Statics"

    Public Shared Function Print(ByVal dgv As DataGridView, ByVal userName As String, ByVal headerText As String, ByVal Orientation As String) As Boolean
        Validate(dgv, userName, headerText)
        Dim tempPrintFactory As PrintFactory
        tempPrintFactory = BeginPrint(dgv, userName, headerText)
        If orientation = "L" Then
            tempPrintFactory._printDocument.DefaultPageSettings.Landscape = True
        End If
        tempPrintFactory._printDocument.Print()
    End Function

    Public Shared Function Preview(ByVal dgv As DataGridView, ByVal userName As String, ByVal headerText As String, ByVal Orientation As String) As Boolean
        Validate(dgv, userName, headerText)
        Dim tempPrintFactory As PrintFactory
        tempPrintFactory = BeginPrint(dgv, userName, headerText)
        If Orientation = "L" Then
            tempPrintFactory._printDocument.DefaultPageSettings.Landscape = True
        End If
        Dim tempPrintDiag As New PrintPreviewDialog()
        tempPrintDiag.Document = tempPrintFactory._printDocument
        tempPrintDiag.ShowDialog()
    End Function

    Private Shared Function BeginPrint(ByVal dgv As DataGridView, ByVal userName As String, ByVal headerText As String) As PrintFactory
        Dim printEntity As PrintFactory = New PrintFactory(userName, headerText)
        printEntity._dgv = dgv
        printEntity.StringFormat = New StringFormat
        printEntity.StringFormat.Alignment = StringAlignment.Near
        printEntity.StringFormat.LineAlignment = StringAlignment.Center
        printEntity.StringFormat.Trimming = StringTrimming.EllipsisCharacter
        printEntity.StringFormatComboBox = New StringFormat
        printEntity.StringFormatComboBox.LineAlignment = StringAlignment.Center
        printEntity.StringFormatComboBox.FormatFlags = StringFormatFlags.NoWrap
        printEntity.StringFormatComboBox.Trimming = StringTrimming.EllipsisCharacter
        printEntity.Button = New Button
        printEntity.CheckBox = New CheckBox
        printEntity.ComboBox = New ComboBox
        printEntity.TotalWidth = 0

        For Each oColumn As DataGridViewColumn In dgv.Columns
            printEntity.TotalWidth += oColumn.Width
        Next
        printEntity.PageNumber = 1
        printEntity.NewPage = True
        printEntity.RowPos = 0
        Return printEntity
    End Function

    Private Shared Function EndPrint(ByVal printEntity As PrintFactory, ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Boolean
        Static oColumnLefts As New ArrayList
        Static oColumnWidths As New ArrayList
        Static oColumnTypes As New ArrayList
        Static nHeight As Int16

        Dim nWidth, i, nRowsPerPage As Int16
        Dim blnGotNumPages As Boolean
        Dim strNumOfPages As String

        Dim nTop As Int16 = e.MarginBounds.Top
        Dim nLeft As Int16 = e.MarginBounds.Left

        If printEntity.PageNumber = 1 Then

            For Each oColumn As DataGridViewColumn In printEntity.DGV.Columns
                If oColumn.Visible = True Then
                    nWidth = CType(Math.Floor(oColumn.Width / printEntity.TotalWidth * printEntity.TotalWidth * (e.MarginBounds.Width / printEntity.TotalWidth)), Int16)
                    nHeight = e.Graphics.MeasureString(oColumn.HeaderText, oColumn.InheritedStyle.Font, nWidth).Height + 11
                    oColumnLefts.Add(nLeft)
                    oColumnWidths.Add(nWidth)
                    oColumnTypes.Add(oColumn.GetType)
                    nLeft += nWidth
                End If
            Next
        End If

        Do While printEntity.RowPos < printEntity.DGV.Rows.Count
            Dim oRow As DataGridViewRow = printEntity.DGV.Rows(printEntity.RowPos)

            If nTop + nHeight >= e.MarginBounds.Height + e.MarginBounds.Top Then
                DrawFooter(blnGotNumPages, strNumOfPages, printEntity.DGV, printEntity, nRowsPerPage, e)
                printEntity.NewPage = True
                printEntity.PageNumber += 1
                e.HasMorePages = True
                Exit Function
            Else

                If printEntity.NewPage Then

                    ' Draw Header 
                    e.Graphics.DrawString(printEntity.HeaderText, New Font(printEntity.DGV.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top - e.Graphics.MeasureString(printEntity.HeaderText, New Font(printEntity.DGV.Font, FontStyle.Bold), e.MarginBounds.Width).Height - 13)
                    ' Draw Columns 
                    nTop = e.MarginBounds.Top
                    i = 0
                    For Each oColumn As DataGridViewColumn In printEntity.DGV.Columns

                        If oColumn.Visible = True Then
                            e.Graphics.FillRectangle(New SolidBrush(Drawing.Color.LightGray), New Rectangle(oColumnLefts(i), nTop, oColumnWidths(i), nHeight))
                            e.Graphics.DrawRectangle(Pens.Black, New Rectangle(oColumnLefts(i), nTop, oColumnWidths(i), nHeight))
                            e.Graphics.DrawString(oColumn.HeaderText, oColumn.InheritedStyle.Font, New SolidBrush(oColumn.InheritedStyle.ForeColor), New RectangleF(oColumnLefts(i), nTop, oColumnWidths(i), nHeight), printEntity.StringFormat)
                            i += 1
                        End If

                    Next

                    printEntity.NewPage = False
                End If

                nTop += nHeight
                i = 0

                For Each oCell As DataGridViewCell In oRow.Cells
                    If oCell.Visible = True Then
                        If oColumnTypes(i) Is GetType(DataGridViewTextBoxColumn) OrElse oColumnTypes(i) Is GetType(DataGridViewLinkColumn) Then
                            e.Graphics.DrawString(oCell.Value.ToString, oCell.InheritedStyle.Font, New SolidBrush(oCell.InheritedStyle.ForeColor), New RectangleF(oColumnLefts(i), nTop, oColumnWidths(i), nHeight), printEntity.StringFormat)
                        ElseIf oColumnTypes(i) Is GetType(DataGridViewButtonColumn) Then
                            printEntity.Button.Text = oCell.Value.ToString
                            printEntity.Button.Size = New Size(oColumnWidths(i), nHeight)
                            Dim oBitmap As New Bitmap(printEntity.Button.Width, printEntity.Button.Height)
                            printEntity.Button.DrawToBitmap(oBitmap, New Rectangle(0, 0, oBitmap.Width, oBitmap.Height))
                            e.Graphics.DrawImage(oBitmap, New Point(oColumnLefts(i), nTop))
                        ElseIf oColumnTypes(i) Is GetType(DataGridViewCheckBoxColumn) Then
                            printEntity.CheckBox.Size = New Size(14, 14)
                            printEntity.CheckBox.Checked = CType(oCell.Value, Boolean)
                            Dim oBitmap As New Bitmap(oColumnWidths(i), nHeight)
                            Dim oTempGraphics As Graphics = Graphics.FromImage(oBitmap)
                            oTempGraphics.FillRectangle(Brushes.White, New Rectangle(0, 0, oBitmap.Width, oBitmap.Height))
                            printEntity.CheckBox.DrawToBitmap(oBitmap, New Rectangle(CType((oBitmap.Width - printEntity.CheckBox.Width) / 2, Int32), CType((oBitmap.Height - printEntity.CheckBox.Height) / 2, Int32), printEntity.CheckBox.Width, printEntity.CheckBox.Height))
                            e.Graphics.DrawImage(oBitmap, New Point(oColumnLefts(i), nTop))
                        ElseIf oColumnTypes(i) Is GetType(DataGridViewComboBoxColumn) Then
                            printEntity.ComboBox.Size = New Size(oColumnWidths(i), nHeight)
                            Dim oBitmap As New Bitmap(printEntity.ComboBox.Width, printEntity.ComboBox.Height)
                            printEntity.ComboBox.DrawToBitmap(oBitmap, New Rectangle(0, 0, oBitmap.Width, oBitmap.Height))
                            e.Graphics.DrawImage(oBitmap, New Point(oColumnLefts(i), nTop))
                            e.Graphics.DrawString(oCell.Value.ToString, oCell.InheritedStyle.Font, New SolidBrush(oCell.InheritedStyle.ForeColor), New RectangleF(oColumnLefts(i) + 1, nTop, oColumnWidths(i) - 16, nHeight), printEntity.StringFormatComboBox)
                        ElseIf oColumnTypes(i) Is GetType(DataGridViewImageColumn) Then
                            Dim oCellSize As Rectangle = New Rectangle(oColumnLefts(i), nTop, oColumnWidths(i), nHeight)
                            Dim oImageSize As Size = CType(oCell.Value, Image).Size

                            e.Graphics.DrawImage(oCell.Value, New Rectangle(oColumnLefts(i) + CType(((oCellSize.Width - oImageSize.Width) / 2), Int32), nTop + CType(((oCellSize.Height - oImageSize.Height) / 2), Int32), CType(oCell.Value, Image).Width, CType(oCell.Value, Image).Height))
                        End If
                        e.Graphics.DrawRectangle(Pens.Black, New Rectangle(oColumnLefts(i), nTop, oColumnWidths(i), nHeight))
                        i += 1
                    End If
                Next
            End If

            printEntity.RowPos += 1
            nRowsPerPage += 1

        Loop

        DrawFooter(blnGotNumPages, strNumOfPages, printEntity.DGV, printEntity, nRowsPerPage, e)
        e.HasMorePages = False
    End Function
    Private Shared Function DrawFooter(ByVal blnGotNumPages As Boolean, ByVal strNumOfPages As String, ByVal dgv As DataGridView, ByVal printEntity As PrintFactory, ByVal RowsPerPage As Int32, ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Boolean
        Dim sPageNo As String

        If printEntity.blnGotNumPages = False Then
            printEntity.blnGotNumPages = True
            printEntity.strNumOfPages = Math.Ceiling(dgv.Rows.Count / RowsPerPage).ToString
            sPageNo = printEntity.PageNumber.ToString + " of " + Math.Ceiling(dgv.Rows.Count / RowsPerPage).ToString
        Else
            sPageNo = printEntity.PageNumber.ToString + " of " + printEntity.strNumOfPages
        End If

        ' Right Align - User Name 
        e.Graphics.DrawString(printEntity.UserName, dgv.Font, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(printEntity.UserName, dgv.Font, e.MarginBounds.Width).Width), e.MarginBounds.Top + e.MarginBounds.Height + 7)
        ' Left Align - Date/Time 
        e.Graphics.DrawString(Now.ToLongDateString + " " + Now.ToShortTimeString, dgv.Font, Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top + e.MarginBounds.Height + 7)
        ' Center - Page No. Info 
        e.Graphics.DrawString(sPageNo, dgv.Font, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(sPageNo, dgv.Font, e.MarginBounds.Width).Width) / 2, e.MarginBounds.Top + e.MarginBounds.Height + 31)
    End Function

    Private Shared Sub Validate(ByVal dgv As DataGridView, ByVal userName As String, ByVal headerText As String)
        If dgv Is Nothing Then
            Throw New ArgumentNullException("dgv")
        End If
        If String.IsNullOrEmpty(userName) = True Then
            Throw New ArgumentNullException("userName")
        End If
        If String.IsNullOrEmpty(headerText) = True Then
            Throw New ArgumentNullException("headerText")
        End If
    End Sub
#End Region
End Class
