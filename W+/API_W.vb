Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Public Class API_W
    Dim authke As String = "mXHQwqbHQBKr9owh3KKy5k1iUDtt5XNgVxt8xt2Kmd3dYJMLj0"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox(API.creatdevice(authke, "dgsdfg"))

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
        Dim x
        x = API.get_devices(authke)
        Dim objects As List(Of JObject) = JsonConvert.DeserializeObject(Of List(Of JObject))(x)

        For Each obj As JObject In objects
            If obj("status").ToString <> "1" Then


                TabControl1.TabPages.Add(TabControl1.TabCount + 1, obj("id").ToString())
                add_pic(obj("uuid").ToString())
            Else

                TabControl1.TabPages.Add(TabControl1.TabCount + 1, "Connected" & "|" & obj("id").ToString())
                add_pic_con(obj("uuid").ToString())
            End If
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
    End Sub
End Class