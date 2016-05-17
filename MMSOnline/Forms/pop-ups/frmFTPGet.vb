Imports EnterpriseDT.Net.Ftp
Imports System.IO

Public Class frmFTPGet
    Delegate Sub UpdateProgressBarDelegate(ByVal byteCount As Long)
    Private fsize As Long
    Public serveraddress As String


    Private Sub download()
        Me.Refresh()

        Try

            FtpConnection1.ServerAddress = (serveraddress)
            FtpConnection1.UserName = ("download")
            FtpConnection1.Password = ("mmsonline")
            FtpConnection1.ConnectMode = FTPConnectMode.PASV
            FtpConnection1.Connect()

        Catch ex As FormatException
            If FtpConnection1.IsConnected = True Then
                FtpConnection1.Close()
            End If
            Debug.WriteLine(ex.Message)
            Debug.WriteLine("Connect Failed")
            Exit Sub
            'GoTo notconnected
        End Try

        If FtpConnection1.IsConnected = True Then
            Debug.WriteLine("connected")
        End If


        ' Change to Temp folder, download the users file and change the extension to .txt 
        Try
            'make sure the download directory exists
            If Not Directory.Exists("C:\Temp") Then
                Directory.CreateDirectory("C:\Temp")
            End If
            If Not Directory.Exists("C:\Temp\MMS") Then
                Directory.CreateDirectory("C:\Temp\MMS")
            End If
            Dim file As String = "Setup.msi"
            'Dim dlpath As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Setup.msi"

            Dim dlpath As String = "C:\Temp\MMS\Setup.msi"
            fsize = FtpConnection1.GetSize(file)
            FtpConnection1.TransferType = FTPTransferType.BINARY
            FtpConnection1.DeleteOnFailure = True
            FtpConnection1.ConnectMode = FTPConnectMode.PASV
            Dim stream As New System.IO.FileStream(dlpath, IO.FileMode.Create)
            FtpConnection1.DownloadStream(stream, file)

            ProgressBar1.Value = 0
            file = "setup.exe"
            'dlpath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Setup.msi"
            dlpath = "C:\Temp\MMS\Setup.exe"
            fsize = FtpConnection1.GetSize(file)
            Dim stream2 As New System.IO.FileStream(dlpath, IO.FileMode.Create)
            FtpConnection1.DownloadStream(stream2, file)

            FtpConnection1.Close()

            System.Diagnostics.Process.Start(dlpath)
            Me.Close()
            ' Close the connection 
        Catch ex As FTPException
            If FtpConnection1.IsConnected = True Then
                FtpConnection1.Close()
            End If
            Debug.WriteLine(ex.Message)
            Debug.WriteLine("Didn't download")
        End Try


        '' Change to Temp folder, download the users file and change the extension to .txt 
        'Try
        '    Dim file As String = "Setup.exe"
        '    Dim dlpath As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MMS\Setup.exe"
        '    fsize = FtpConnection1.GetSize(file)
        '    FtpConnection1.TransferType = FTPTransferType.BINARY
        '    FtpConnection1.DeleteOnFailure = True
        '    FtpConnection1.ConnectMode = FTPConnectMode.PASV
        '    Dim stream As New System.IO.FileStream(dlpath, IO.FileMode.Create)
        '    FtpConnection1.DownloadStream(stream, file)
        '    FtpConnection1.Close()
        '    System.Diagnostics.Process.Start(dlpath)
        '    Me.Close()
        '    ' Close the connection 
        'Catch ex As FTPException
        '    If FtpConnection1.IsConnected = True Then
        '        FtpConnection1.Close()
        '    End If
        '    Debug.WriteLine(ex.Message)
        '    Debug.WriteLine("Didn't download")
        'End Try
        'notconnected:
    End Sub

    Private Sub FtpConnection1_BytesTransferred(ByVal sender As Object, ByVal e As EnterpriseDT.Net.Ftp.BytesTransferredEventArgs) Handles FtpConnection1.BytesTransferred
        Dim arguments As Long
        arguments = (e.ByteCount / fsize) * 100
        ProgressBar1.Invoke(New UpdateProgressBarDelegate(AddressOf UpdateProgressBar), arguments)
    End Sub

    Private Sub UpdateProgressBar(ByVal ByteCount As Long)
        ProgressBar1.Value = ByteCount
    End Sub


    Private Sub frmFTPGet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Refresh()


    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        download()


    End Sub
End Class