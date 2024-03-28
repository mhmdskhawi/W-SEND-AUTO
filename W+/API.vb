Imports System.IO
Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json.Linq

Module API

    Function creatdevice(authKey As String, device_name As String)
        Dim apiUrl As String = $"https://wplus.my-sys.online/api/devicecreat/?name={device_name}&authkey={authKey}"
        ' HttpClient to make the HTTP request
        Dim httpClient As New HttpClient()

        Dim payload As String = "User-Agent=PostmanRuntime/7.36.1" &
                                "&Connection=keep-alive"
        Dim req As String = String.Format(payload)
        Dim content As New StringContent(req, Encoding.UTF8, "application/x-www-form-urlencoded")

        Using client As New HttpClient()
            Dim response As HttpResponseMessage = client.PostAsync(apiUrl, content).Result

            If response.IsSuccessStatusCode Then
                Dim responseContent As String = response.Content.ReadAsStringAsync().Result
                Return responseContent

            Else
                Return response.StatusCode
            End If
        End Using

    End Function
    Function chk_device(uuid As String)
        Dim apiUrl As String = $"https://wplus.my-sys.online/api/checkSession/{uuid}"
        Dim httpClient As New HttpClient()


        Dim responseTask = httpClient.GetAsync(apiUrl)


        Dim responseContent = responseTask.Result.Content.ReadAsStringAsync().Result

        ' Return the response content
        Return responseContent

    End Function
    Function destroy_device(uuid As String)
        Dim apiUrl As String = $"https://wplus.my-sys.online/api/device_destroy/{uuid}"

        Dim httpClient As New HttpClient()


        Dim responseTask = httpClient.GetAsync(apiUrl)


        Dim responseContent = responseTask.Result.Content.ReadAsStringAsync().Result

        ' Return the response content
        Return responseContent

    End Function
    Function get_devices(authKey As String) As String

        Dim apiUrl As String = "https://wplus.my-sys.online/api/gdid/?authkey=" & authKey


        Dim httpClient As New HttpClient()


        Dim responseTask = httpClient.GetAsync(apiUrl)


        Dim responseContent = responseTask.Result.Content.ReadAsStringAsync().Result

        ' Return the response content
        Return responseContent

    End Function

    Function Getqr(authKey As String, uuid As String)

        Dim apiUrl As String = $"https://wplus.my-sys.online/api/qr/?device={uuid}&authkey={authKey}"
        ' HttpClient to make the HTTP request

        Dim httpClient As New HttpClient()

        Dim payload As String = "User-Agent=PostmanRuntime/7.36.1" &
                                "&Connection=keep-alive"

        Dim req As String = String.Format(payload)

        Dim content As New StringContent(req, Encoding.UTF8, "application/x-www-form-urlencoded")

        Using client As New HttpClient()
            Dim response As HttpResponseMessage = client.PostAsync(apiUrl, content).Result

            If response.IsSuccessStatusCode Then
                Dim responseContent As String = response.Content.ReadAsStringAsync().Result
                Return responseContent

            Else
                Return response.StatusCode

                ' MsgBox($"Error: {response.StatusCode} - {response.ReasonPhrase}")
            End If
        End Using

    End Function
    Function app_creat(authKey As String, app_name As String, device_id As Integer) As String
        ' API Endpoint URL  
        Dim apiUrl As String = $"https://wplus.my-sys.online/api/app_cre/?device={device_id}&name={app_name}&authkey={authKey}"

        ' HttpClient to make the HTTP request
        Dim httpClient As New HttpClient()

        ' Send GET request asynchronously
        Dim responseTask = httpClient.GetAsync(apiUrl)

        ' Await the response and read the content
        Dim responseContent = responseTask.Result.Content.ReadAsStringAsync().Result

        ' Return the response content
        Return responseContent
    End Function


    Function get_apps(authKey As String) As String
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

    Function loginchek(authKey As String) As String
        ' API Endpoint URL
        Dim apiUrl As String = "https://wplus.my-sys.online/api/checkau/" & authKey

        ' HttpClient to make the HTTP request
        Dim httpClient As New HttpClient()

        ' Send GET request asynchronously
        Dim responseTask = httpClient.GetAsync(apiUrl)

        ' Await the response and read the content
        Dim responseContent = responseTask.Result.Content.ReadAsStringAsync().Result

        ' Return the response content
        Return responseContent

    End Function

    Function send_api(ByVal num, ByVal mes, ByVal auth, ByVal app)

        Dim url As String = "https://wplus.my-sys.online/api/create-message"

        Dim payload As String = $"appkey={app}" &
                                $"&authkey={auth}" &
                                "&to=+{0}" &
                                "&message={1}"
        Dim req As String = String.Format(payload, num, mes)
        Dim content As New StringContent(req, Encoding.UTF8, "application/x-www-form-urlencoded")

        Using client As New HttpClient()
            Dim response As HttpResponseMessage = client.PostAsync(url, content).Result

            If response.IsSuccessStatusCode Then
                '  Dim responseContent As String = response.Content.ReadAsStringAsync().Result
                '  MsgBox(responseContent)

                '   MsgBox(responseContent)
            Else
                Return 0

                'MsgBox($"Error: {response.StatusCode} - {response.ReasonPhrase}")
            End If
        End Using

    End Function

    Function send_apifile(ByVal num, ByVal mes, ByVal auth, ByVal app, ByVal filePath)

        Dim url As String = "https://wplus.my-sys.online/api/create-message"

        ' Create a new instance of MultipartFormDataContent
        Dim formData As New MultipartFormDataContent()

        ' Add text parameters
        formData.Add(New StringContent(app), "appkey")
        formData.Add(New StringContent(auth), "authkey")
        formData.Add(New StringContent(num), "to")
        formData.Add(New StringContent(mes), "message")

        ' Add file parameter
        Dim fileBytes As Byte() = File.ReadAllBytes(filePath)
        Dim fileContent As New ByteArrayContent(fileBytes)
        formData.Add(fileContent, "data", Path.GetFileName(filePath))

        ' Send the request
        SendRequest(url, formData)
    End Function

    Async Sub SendRequest(ByVal url As String, ByVal content As MultipartFormDataContent)
        Using client As New HttpClient()
            Try
                Dim response As HttpResponseMessage = Await client.PostAsync(url, content)

                If response.IsSuccessStatusCode Then
                    '    Dim responseContent As String = Await response.Content.ReadAsStringAsync()
                    '  MsgBox(responseContent)
                Else
                    '  MsgBox($"Error: {response.StatusCode} - {response.ReasonPhrase}")
                End If
            Catch ex As Exception
                '   MsgBox($"Exception: {ex.Message}")
            End Try
        End Using
    End Sub
End Module
