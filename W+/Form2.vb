﻿Imports System.IO
Imports System.Net.Http
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Qr
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        lunch()
    End Sub
    Sub lunch()
        Dim authKey As String = "mXHQwqbHQBKr9owh3KKy5k1iUDtt5XNgVxt8xt2Kmd3dYJMLj0"
        Dim gdidResponse As String = GetGdid(authKey)



        Dim objects As List(Of JObject) = JsonConvert.DeserializeObject(Of List(Of JObject))(gdidResponse)


        For Each obj As JObject In objects
            If obj("qr").ToString <> "" Then
                Try
                    Dim qrResponse As JObject = Getqr(authKey, obj("uuid").ToString)

                    ' Output the response content
                    Dim imageBytes As Byte() = Convert.FromBase64String(qrResponse("qr").ToString().Replace("data:image/png;base64,", ""))
                    ' Create a MemoryStream from the byte array
                    Using stream As New MemoryStream(imageBytes)
                        ' Create an Image from the MemoryStream
                        Dim image As Image = Image.FromStream(stream)
                        PictureBox1.Image = image
                    End Using
                Catch ex As Exception

                End Try


            End If
        Next


        TextBox1.Text = getdeviceid("mXHQwqbHQBKr9owh3KKy5k1iUDtt5XNgVxt8xt2Kmd3dYJMLj0")

    End Sub
    Function GetGdid(authKey As String) As String
        ' API Endpoint URL
        Dim apiUrl As String = "https://wplus.my-sys.online/api/gdid/?authkey=" & authKey

        ' HttpClient to make the HTTP request
        Dim httpClient As New HttpClient()

        ' Send GET request asynchronously
        Dim responseTask = httpClient.GetAsync(apiUrl)

        ' Await the response and read the content
        Dim responseContent = responseTask.Result.Content.ReadAsStringAsync().Result

        ' Return the response content
        Return responseContent
    End Function
    Function getdeviceid(authKey As String) As String
        ' API Endpoint URL
        Dim apiUrl As String = "https://wplus.my-sys.online/api/app/?authkey=" & authKey

        ' HttpClient to make the HTTP request
        Dim httpClient As New HttpClient()

        ' Send GET request asynchronously
        Dim responseTask = httpClient.GetAsync(apiUrl)

        ' Await the response and read the content
        Dim responseContent = responseTask.Result.Content.ReadAsStringAsync().Result

        ' Return the response content
        Return responseContent
    End Function
    ' Function to make the API request
    Function Getqr(authKey As String, uuid As String)
        Dim apiUrl As String = $"https://wplus.my-sys.online/api/qr/?device={uuid}&authkey={authKey}"
        ' HttpClient to make the HTTP request
        Dim httpClient As New HttpClient()

        ' Send GET request asynchronously
        Dim responseTask = httpClient.GetAsync(apiUrl)

        ' Await the response and read the content
        Dim responseContent = responseTask.Result.Content.ReadAsStringAsync().Result
        Dim jsonResponse As JObject = Nothing

        jsonResponse = JObject.Parse(responseContent)
        ' Return the response content
        Return jsonResponse
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        ' Test values for authKey and uuid
        Dim authKey As String = "mXHQwqbHQBKr9owh3KKy5k1iUDtt5XNgVxt8xt2Kmd3dYJMLj0"
        Dim uuid As String = "12564013-e55b-46bf-b675-78ff484e58a7"

        ' Call the Getqr function to make the API request
        Dim qrResponse As String = Getqr(authKey, uuid)


    End Sub

    Private Sub Qr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lunch()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs)

    End Sub
End Class