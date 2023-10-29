Imports System.Threading ' Add this import for Thread.Sleep


Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CloseProcessByName("WhatsApp")
        Dim text = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "\PHONE.txt").Split(vbCrLf)

        For Each phon In text

            ListBox1.Items.Add(phon)

        Next
    End Sub
    Sub sends(ByVal Phone, ByVal msg, ByVal pdf, ByVal c)


        On Error Resume Next ' Error handling is different in VB.NET, but you can still use Try...Catch
        Thread.Sleep(2000)


        ' Construct the WhatsApp URL
        Dim WhatsAppURL As String = "https://web.whatsapp.com/send/?phone=%2B2" & Phone & "&text=*" & msg & "*&type=phone_number&app_absent=0"
        ' WhatsAppURL = "WHATSAPP://SEND?phone=%2B2" & Phone & "&text=*" & msg & "*&type=phone_number&app_absent=0"

        ' Open the WhatsApp URL in the default web browser WHATSAPP://SEND?phone=+2" & Me.Text0
        System.Diagnostics.Process.Start(WhatsAppURL)

        ' Wait for 15 seconds
        Thread.Sleep(7000)
        SendKeys.SendWait("{ENTER}")
        If c = 1 Then
            If pdf = "" Then
                FORNEXT("{TAB}", 17)
                ' Simulate pressing Enter key twice
                SendKeys.SendWait("{ENTER}")

            Else


                FORNEXT("{TAB}", 16)

            End If

        Else
            If pdf = "" Then
                FORNEXT("{TAB}", 17)
                ' Simulate pressing Enter key twice
                SendKeys.SendWait("{ENTER}")

            Else


                FORNEXT("{TAB}", 14)

            End If
        End If


        Thread.Sleep(1000)
        '   FORNEXT("+{TAB}", 0)
        SendKeys.SendWait("{ENTER}")
        SendKeys.SendWait("{DOWN}")
        Thread.Sleep(500)
        SendKeys.SendWait("{ENTER}")
        Thread.Sleep(500)



        Thread.Sleep(500)
        SendKeys.SendWait(pdf)
        Thread.Sleep(1000)
        SendKeys.SendWait("{ENTER}")

        Thread.Sleep(1000)
        SendKeys.SendWait("{ENTER}")
        SendKeys.SendWait("{ENTER}")


A1:
        SendKeys.SendWait("{ENTER}")
        SendKeys.SendWait("{ENTER}")
        SendKeys.SendWait("{ENTER}")

        Thread.Sleep(2000)

    End Sub
    Sub sendapp(ByVal Phone, ByVal msg, ByVal pdf)

        On Error Resume Next ' Error handling is different in VB.NET, but you can still use Try...Catch
        CloseProcessByName("WhatsApp")

        Thread.Sleep(3000)


        Dim WhatsAppURL As String = "WHATSAPP://SEND?phone=%2B2" & Phone & "&text=*" & msg & "*&type=phone_number&app_absent=0"


        ' Open the WhatsApp URL in the default web browser WHATSAPP://SEND?phone=+2" & Me.Text0
        System.Diagnostics.Process.Start(WhatsAppURL)
        AppActivate("WhatsApp")


        Thread.Sleep(3000)
        SendKeys.SendWait("{ENTER}")
        SendKeys.SendWait("{ENTER}")

        AppActivate("WhatsApp")
        Thread.Sleep(500)

        FORNEXT("+{TAB}", 4)

        Thread.Sleep(500)
        SendKeys.SendWait("{ENTER}")
        If pdf = "" Then
            GoTo A1
        End If

        FORNEXT("+{TAB}", 0)

        Thread.Sleep(500)
        SendKeys.SendWait("{ENTER}")
        Thread.Sleep(500)
        SendKeys.SendWait("{DOWN}")
        Thread.Sleep(500)
        SendKeys.SendWait("{ENTER}")
        Thread.Sleep(500)
        SendKeys.SendWait(pdf)
        SendKeys.SendWait("{ENTER}")
        Thread.Sleep(1000)
        SendKeys.SendWait("{ENTER}")
        SendKeys.SendWait("{ENTER}")
A1:

        SendKeys.SendWait("{ENTER}")

        Thread.Sleep(2000)



    End Sub
    Sub FORNEXT(ByVal AC, ByVal tic)

        For x = 0 To tic
            SendKeys.SendWait(AC)
            Thread.Sleep(100)
        Next

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        For Each phon In ListBox1.Items

            sendapp(phon, MESS.Text, PDFPATH.Text)

        Next
    End Sub



    Sub CloseProcessByName(processName As String)
        ' Get all running processes with the specified name
        Dim processes() As Process = Process.GetProcessesByName(processName)

        ' Iterate through each process and close it
        For Each process As Process In processes

            process.Kill()

        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CloseProcessByName("WhatsApp")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim c = 1
        For Each phon In ListBox1.Items
            sends(phon, MESS.Text, PDFPATH.Text, c)
            c += 1
        Next

    End Sub
End Class
