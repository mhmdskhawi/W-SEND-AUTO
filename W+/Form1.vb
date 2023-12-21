Imports System.Threading ' Add this import for Thread.Sleep

Imports System.Net.Http
Imports System.Text
Imports System.IO

Public Class Form1
    Dim filter = 0
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CloseProcessByName("WhatsApp")
        Try
            Dim text = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "\PHONE.txt").Split(vbCrLf)

            For Each phon In text

                ListBox1.Items.Add(phon)

            Next
            cc.Text = ListBox1.Items.Count

        Catch ex As Exception

        End Try

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



        System.Diagnostics.Process.Start(WhatsAppURL)
        AppActivate("WhatsApp")


        Thread.Sleep(3000)
        SendKeys.SendWait("{ENTER}")
        SendKeys.SendWait("{ENTER}")

        AppActivate("WhatsApp")
        Thread.Sleep(500)

        FORNEXT("+{TAB}", 4) ' 4
        SendKeys.SendWait(msg)
        SendKeys.SendWait("{ENTER}")


        Thread.Sleep(500)
        SendKeys.SendWait("{ENTER}")
        If pdf = "" Then
            GoTo A1
        End If

        FORNEXT("+{TAB}", 0)

        Thread.Sleep(500)
        SendKeys.SendWait("{ENTER}")
        Thread.Sleep(500)


        If filter = "jpg" Or filter = "jpeg" Or filter = "png" Or filter = "gif" Or filter = "bmp" Then ' filter = jpg jpeg png gif bmp

        Else
            SendKeys.SendWait("{DOWN}")
            Thread.Sleep(500)

            SendKeys.SendWait("{DOWN}")
            Thread.Sleep(500)
        End If

        SendKeys.SendWait("{ENTER}")
        Thread.Sleep(1500)
        SendKeys.SendWait(pdf)
        Thread.Sleep(2000)
        SendKeys.SendWait("{ENTER}")
        Thread.Sleep(1500)
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


        For i = 0 To ListBox1.Items.Count - 1
            Dim phon = ListBox1.Items.Item(i)
            sendapp(phon, MESS.Text, PDFPATH.Text)
            ListBox1.Items.Item(i) = phon & "-> DONE"

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

    Private Sub PDFPATH_TextChanged(sender As Object, e As EventArgs) Handles PDFPATH.TextChanged

    End Sub

    Private Sub PDFPATH_DoubleClick(sender As Object, e As EventArgs) Handles PDFPATH.DoubleClick
        ' Create an instance of OpenFileDialog
        Dim openFileDialog1 As New OpenFileDialog()

        ' Set the initial directory (optional)
        ' openFileDialog1.InitialDirectory = "C:\"

        ' Set the title of the dialog
        openFileDialog1.Title = "Select a File"

        ' Filter files by extension   
        openFileDialog1.Filter = "PDF Files|*.pdf|Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*"

        ' Allow selecting multiple files (optional)
        openFileDialog1.Multiselect = False

        ' Show the dialog and check if the user clicked OK
        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Get the selected file path
            Dim selectedFilePath As String = openFileDialog1.FileName
            PDFPATH.Text = selectedFilePath
            filter = (selectedFilePath.Substring(selectedFilePath.Length - 3, 3))
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        Dim sReturn As String
        sReturn = InputBox("Phone Number ( 01069124709 ) : ")
        ListBox1.Items.Add(sReturn)
        cc.Text = ListBox1.Items.Count

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Create an instance of OpenFileDialog
        Dim openFileDialog1 As New OpenFileDialog()

        ' Set the initial directory (optional)
        ' openFileDialog1.InitialDirectory = "C:\"

        ' Set the title of the dialog
        openFileDialog1.Title = "Select a File"

        ' Filter files by extension
        openFileDialog1.Filter = "Text Files|*.txt|All Files|*.*"

        ' Allow selecting multiple files (optional)
        openFileDialog1.Multiselect = False

        ' Show the dialog and check if the user clicked OK
        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Get the selected file path
            Dim selectedFilePath As String = openFileDialog1.FileName


            CloseProcessByName("WhatsApp")
            Dim text = My.Computer.FileSystem.ReadAllText(selectedFilePath).Split(vbCrLf)

            For Each phon In text
                ListBox1.Items.Add(phon)
            Next
            cc.Text = ListBox1.Items.Count

        End If
    End Sub
    Function send_api(ByVal num, ByVal mes)

        Dim url As String = "http://127.0.0.1/api/create-message"

        Dim payload As String = "appkey=e9aa67a4-bb3c-4c89-88f1-a03af52117fb" &
                                "&authkey=4gXAPSaPdXMohzlRHdcAAJHdEy7y4MRUH5KyQznJjOXzJ3FZwl" &
                                "&to=+{0}" &
                                "&message={1}"
        Dim req As String = String.Format(payload, num, mes)
        Dim content As New StringContent(req, Encoding.UTF8, "application/x-www-form-urlencoded")

        Using client As New HttpClient()
            Dim response As HttpResponseMessage = client.PostAsync(url, content).Result

            If response.IsSuccessStatusCode Then
                Dim responseContent As String = response.Content.ReadAsStringAsync().Result
                Return 1
                '   MsgBox(responseContent)
            Else
                Return 0

                MsgBox($"Error: {response.StatusCode} - {response.ReasonPhrase}")
            End If
        End Using

    End Function

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        For i = 0 To ListBox1.Items.Count - 1
            Dim phon = ListBox1.Items.Item(i)
            If send_api(phon, MESS.Text.Replace("{0Num}", "")) = 1 Then
                ListBox1.Items.Item(i) = phon & "-> DONE"
                Thread.Sleep("2000")
            Else
                ListBox1.Items.Item(i) = phon & "-> Error"
            End If


        Next
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim numbers As New List(Of String) ' Assuming you have a list of numbers

        For i = 0 To ListBox1.Items.Count - 1
            Dim phon = ListBox1.Items.Item(i)

            numbers.Add(phon)
        Next


        ' Set the number of numbers per file
        Dim numbersPerFile As Integer = 400

        ' Calculate the number of files needed
        Dim totalNumbers As Integer = numbers.Count
        Dim numberOfFiles As Integer = CInt(Math.Ceiling(totalNumbers / numbersPerFile))

        ' Create and write to files
        For i As Integer = 0 To numberOfFiles - 1
            ' Calculate the range for the current file
            Dim startIdx As Integer = i * numbersPerFile
            Dim endIdx As Integer = Math.Min((i + 1) * numbersPerFile, totalNumbers)

            ' Get the numbers for the current file
            Dim numbersForFile As List(Of String) = numbers.GetRange(startIdx, endIdx - startIdx)

            ' Create and write to the file
            Dim fileName As String = $"output_file_{i + 1}.txt"
            File.WriteAllLines(fileName, numbersForFile)
        Next

        Console.WriteLine("Files created successfully.")


    End Sub
End Class
