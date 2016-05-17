Imports MySql.Data.MySqlClient

Public Class frmViewPic

    Public persistant As PreFetch
    Public picID As Integer
    Public picName As String

    Private Sub frmViewPic_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'Read the file and show it in the picturebox
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        conn.Open()
        cmd.Connection = conn

        Dim adapter As New MySqlDataAdapter
        adapter.SelectCommand = New MySqlCommand("SELECT Pic FROM ServicePics Where PicID = " & picID, conn)

        Dim Data As New DataTable
        Dim commandbuild As New MySql.Data.MySqlClient.MySqlCommandBuilder(adapter)
        adapter.Fill(Data)

        Dim lb() As Byte = Data.Rows(Data.Rows.Count - 1).Item("Pic")
        Dim lstr As New System.IO.MemoryStream(lb)
        PictureBox1.Image = Image.FromStream(lstr)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        lstr.Close()

        conn.Close()
        conn.Dispose()


    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        'Dim fp As String = SaveFileDialog1.FileName
        Dim sfd As New SaveFileDialog
        sfd.Title = "Save your Image picture as...."
        sfd.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyPictures
        sfd.FileName = picName
        Dim fileParts() As String = Split(picName, ".")
        Dim MyImageFormat As System.Drawing.Imaging.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg ' set default to jpeg
        Select Case fileParts(1).ToLower
            Case "png"
                sfd.Filter = "Png Files (*.png)|*.png"
                MyImageFormat = System.Drawing.Imaging.ImageFormat.Png
            Case "jpg"
                sfd.Filter = "Jpeg Files (*.jpg)|*.jpg"
                MyImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
        End Select

        'sfd.Filter = "Png Files (*.png)|*.png"


        Dim result As DialogResult = sfd.ShowDialog
        If result = Windows.Forms.DialogResult.OK Then
            If sfd.FileName <> String.Empty Then
                Dim bm As New Bitmap(PictureBox1.Image)
                bm.Save(sfd.FileName)
                'PictureBox1.Image.Save(sfd.FileName, MyImageFormat)
            End If
        End If

    End Sub
End Class