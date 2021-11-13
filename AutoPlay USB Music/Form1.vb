Imports System.IO
Imports Microsoft.Win32

Public Class home

    Dim myd As DriveInfo
    Dim chk As String = "b"
    Dim keyboard_used As Boolean = False
    Dim mouse_used As Boolean = False
    Private Declare Function BlockInput Lib "user32" (ByVal fBlock As Long) As Long
    Dim localByName As Process() = Process.GetProcessesByName("Music.UI")
    Dim applicationName As String = Application.ProductName
    Dim applicationPath As String = Application.ExecutablePath
    Private Sub home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        andi.Start()
        ' rtt.Visible = False
        '  Label3.Visible = False
        '  Button1.Visible = False
        '  checks()
        Dim regKey As Microsoft.Win32.RegistryKey
        regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
        regKey.SetValue(applicationName, """" & applicationPath & """")
        regKey.Close()


    End Sub

    Sub checks()
        For Each myd In DriveInfo.GetDrives
            If myd.IsReady Then
                If myd.DriveType = IO.DriveType.Removable Then
                    paths.Text = myd.Name & "Music\"
                    ListBox1.Items.Add("Drive " & myd.Name & "  is ready")
                        ListBox1.Items.Add("AvailableFreeSpace: " & myd.AvailableFreeSpace)
                        ListBox1.Items.Add("DriveFormat: " & myd.DriveFormat)
                        ListBox1.Items.Add("DriveType: " & myd.DriveType)
                        ListBox1.Items.Add("DriveType: " & myd.DriveType.ToString)


                End If
                End If

        Next
    End Sub
    Private Sub tm1_Tick(sender As Object, e As EventArgs) Handles tm1.Tick
        Try
            ListBox1.Items.Clear()
            For Each myd In DriveInfo.GetDrives
                If myd.IsReady Then
                    If myd.DriveType = IO.DriveType.Removable Then
                        chk = "a"
                        If chk = "a" Then
                            paths.Text = myd.Name & "Music\"
                            Dim dir As New IO.DirectoryInfo(paths.Text)
                            If dir.Exists Then
                                'Do whatever

                                ListBox1.Items.Add("Drive " & myd.Name & "  is ready")
                                ListBox1.Items.Add("AvailableFreeSpace: " & myd.AvailableFreeSpace)
                                ListBox1.Items.Add("DriveFormat: " & myd.DriveFormat)
                                ListBox1.Items.Add("DriveType: " & myd.DriveType)
                                ListBox1.Items.Add("DriveType: " & myd.DriveType.ToString)


                                ' tm3.Start()

                                keyboard_used = False
                                mouse_used = False
                                BlockInput(True)
                                Threading.Thread.Sleep(1000) 'blocks for 1 second
                                BlockInput(False)
                                '   Call ShowDesktop()
                                Process.Start("explorer.exe", paths.Text)
                                tm4.Start()
                                ' SendKeys.Send("^(a)")
                                Me.Hide()

                                ' My.Computer.Keyboard.SendKeys("Hello" + "{ENTER}")
                                tm1.Stop()
                            Else

                            End If
                        Else
                                tm1.Stop()
                                Me.Hide()
                            End If

                        End If

                    End If

            Next

        Catch
            End
        End Try
    End Sub
    Public Sub ShowDesktop()
        keybd_event(VK_LWIN, 0, 0, 0)
        keybd_event(77, 0, 0, 0)
        keybd_event(VK_LWIN, 0, KEYEVENTF_KEYUP, 0)
    End Sub

    Private Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte,
ByVal dwFlags As Long, ByVal dwExtraInfo As Long)
    Private Const KEYEVENTF_KEYUP = &H2
    Private Const VK_LWIN = &H5B


    Private Sub tm2_Tick(sender As Object, e As EventArgs) Handles tm2.Tick
        ListBox1.Items.Clear()
        checks()

        If IO.Directory.Exists(paths.Text) Then

            If chk = "a" Then
            Else
                andi.Start()
                tm1.Start()
            End If

        Else

            ListBox2.Items.Clear()
            tm1.Stop()
            tm3.Stop()
            tm4.Stop()
        End If
    End Sub


    Private Sub tm3_Tick(sender As Object, e As EventArgs) Handles tm3.Tick
        Try

            ListBox2.SelectedIndex = ListBox2.SelectedIndex + 1
            'Dim startInfo As New ProcessStartInfo(paths.Text & ListBox2.Text)
            ' startInfo.WindowStyle = ProcessWindowStyle.Hidden
            Process.Start(paths.Text & ListBox2.Text)
        Catch
            tm3.Stop()
        End Try
    End Sub

    Private Sub tm4_Tick(sender As Object, e As EventArgs) Handles tm4.Tick
        Try
            If localByName.Length > 0 Then
                SendKeys.Send("%{F4}")
            Else
                SendKeys.Send("^(a)")
                SendKeys.Send("{ENTER}")
                SendKeys.Send("%{F4}")
                ' Dim newProcess As Process

                '   newProcess = Process.Start("explorer.exe ", "F:\Music")
                '   newProcess.CloseMainWindow()
                '   newProcess.Close()
                keyboard_used = True
                mouse_used = True
                chk = "a"

                End
            End If
        Catch
            End
        End Try
    End Sub

    Private Sub andi_Tick(sender As Object, e As EventArgs) Handles andi.Tick
        Me.Hide()
        andi.Stop()
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        chk = "b"
        tm1.Start()
        ListBox2.Items.Clear()
        tm2.Start()
        '   tm5.Stop()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub
End Class
