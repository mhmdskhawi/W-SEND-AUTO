Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports System.Windows.Forms

Public Class API_W
    Dim authke As String = Login.TextBox1.Text
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call API.creatdevice(authke, "dgsdfg")

        reconncet()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        reconncet()
    End Sub
    Sub reconncet()
        For Each TABp In TabControl1.TabPages
            TabControl1.TabPages.Remove(TABp)
        Next


        getdev()
    End Sub
    Sub getdev()

        Dim objects As List(Of JObject) = JsonConvert.DeserializeObject(Of List(Of JObject))(API.get_devices(authke))

        For Each obj As JObject In objects


            If obj("status").ToString <> "1" Then


                TabControl1.TabPages.Add(TabControl1.TabCount + 1, obj("id").ToString())
                add_pic(obj("uuid").ToString())
            Else

                TabControl1.TabPages.Add(TabControl1.TabCount + 1, "Connected" & "|" & obj("id").ToString())
                add_pic_con(obj("uuid").ToString())
            End If

            TabControl1.TabPages.Item(TabControl1.TabCount - 1).Tag = obj("uuid").ToString()
            Call API.chk_device(obj("uuid").ToString)
        Next



    End Sub
    Sub add_pic(uuid As String)
        ' Create a new PictureBox
        Dim pictureBox As New PictureBox()
        ' Set properties for the PictureBox
        pictureBox.Location = New Point(10, 10)
        pictureBox.Size = New Size(330, 330)
        ' Set the path to your image
        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage
A1:

        Dim res = API.Getqr(authke, uuid)
        If res.ToString.Contains("qr") Then
            Dim jsonResponse As JObject = JObject.Parse(res)
            Dim imageBytes As Byte() = Convert.FromBase64String(jsonResponse("qr").ToString().Replace("data:image/png;base64,", ""))

            Using stream As New MemoryStream(imageBytes)
                ' Create an Image from the MemoryStream
                Dim image As Image = Image.FromStream(stream)
                pictureBox.Image = image

            End Using
        Else

            GoTo A1
        End If

        ' Add the PictureBox to the TabPage
        TabControl1.TabPages.Item(TabControl1.TabCount - 1).Controls.Add(pictureBox)
    End Sub
    Sub add_pic_con(uuid As String)
        ' Create a new PictureBox
        Dim pictureBox As New PictureBox()
        ' Set properties for the PictureBox
        pictureBox.Location = New Point(10, 10)
        pictureBox.Size = New Size(330, 330)
        ' Set the path to your image
        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage
        pictureBox.Image = My.Resources.whatsapp_logo_background_29


        ' Add the PictureBox to the TabPage
        TabControl1.TabPages.Item(TabControl1.TabCount - 1).Controls.Add(pictureBox)
    End Sub
    Private Sub API_W_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getdev()
        Button4.Enabled = False
        GroupBox3.Enabled = False
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        getkey()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim result As DialogResult = MessageBox.Show("Do you want to Delete Device ?", "Confirmation", MessageBoxButtons.YesNo)

        ' Check the user's choice
        If result = DialogResult.Yes Then
            Call API.destroy_device(TabControl1.TabPages.Item(TabControl1.SelectedIndex).Tag)
            reconncet()
        Else

        End If



    End Sub



    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        MsgBox(API.app_creat(authke, "Applecation", TabControl1.TabPages.Item(TabControl1.SelectedIndex).Text.Replace("Connected|", "")))
        getkey()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        On Error Resume Next

        getkey()

        Clipboard.SetText(appkeyy.Text)
    End Sub
    Sub getkey()
        On Error Resume Next
        GroupBox3.Enabled = False

        If TabControl1.TabPages.Item(TabControl1.SelectedIndex).Text.Contains("Connected") Then
            Button4.Enabled = True
            Button4.Text = "Create"
        Else
            Button4.Enabled = False
            Button4.Text = "Scan Qr .. "
        End If

        Dim objects As List(Of JObject) = JsonConvert.DeserializeObject(Of List(Of JObject))(API.get_apps(authke))
        appkeyy.Text = "."
        For Each obj As JObject In objects
            If TabControl1.TabPages.Item(TabControl1.SelectedIndex).Text.Replace("Connected|", "") = obj("device_id").ToString() Then
                appkeyy.Text = obj("key").ToString()
                GroupBox3.Enabled = True

                Button4.Enabled = False
                Button4.Text = "You Have One"




            End If


        Next


    End Sub


    Private Sub appkeyy_DoubleClick(sender As Object, e As EventArgs) Handles appkeyy.DoubleClick
        TextBox1.Text = appkeyy.Text
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        send_api(TextBox2.Text, "Test", authke, appkeyy.Text)
    End Sub

    Private Sub API_W_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub
End Class