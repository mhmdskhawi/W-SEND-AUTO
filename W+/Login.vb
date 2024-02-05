Imports Newtonsoft.Json.Linq

Public Class Login
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim xdata = loginchek(TextBox1.Text)

        If xdata = "" Or TextBox1.Text = "" Then

        Else
            Dim jsonResponse As JObject = JObject.Parse(xdata)
            If jsonResponse("status").ToString() = "1" Then
                API_W.Show()
                Me.Hide()
            Else
                MsgBox("NOT ALLOWED")
            End If



        End If

    End Sub
End Class