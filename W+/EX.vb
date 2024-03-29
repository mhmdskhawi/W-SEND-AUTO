Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class EX
    Dim price = 0
    Dim deviceId As String
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            price += 10
        Else
            price -= 10
        End If
        T.Text = price
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            price += 50
        Else
            price -= 50
        End If
        T.Text = price
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            price += 26
        Else
            price -= 26
        End If
        T.Text = price
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            price += 33
        Else
            price -= 33
        End If
        T.Text = price
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked Then
            price += 90
        Else
            price -= 90
        End If
        T.Text = price
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked Then
            price += 153
        Else
            price -= 153
        End If
        T.Text = price
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        On Error Resume Next
        TextBox6.Text = price - TextBox5.Text
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim fin = String.Format(RichTextBox1.Text, N.Text, D.Value.ToString, T.Text, TextBox5.Text, TextBox6.Text)
        If pdf.Checked Then
            ' Set the file dialog properties
            OpenFileDialog1.InitialDirectory = "C:\" ' Set initial directory if needed
            OpenFileDialog1.Filter = "PDF Files (*.pdf)|*.pdf|Document Files (*.docx, *.xlsx, *.csv)|*.docx;*.xlsx;*.csv" ' Filter to specific file types
            OpenFileDialog1.FilterIndex = 1 ' Index of the filter to be selected by default

            ' Show the file dialog and check if the user selected a file
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                Dim selectedFilePath As String = OpenFileDialog1.FileName

                send_apifile(PH.Text, fin, auth.Text, app.Text, selectedFilePath)


            End If



        Else
            send_api(PH.Text, fin, auth.Text, app.Text)
        End If


    End Sub

    Private Sub pdf_CheckedChanged(sender As Object, e As EventArgs) Handles pdf.CheckedChanged

    End Sub

    Private Sub EX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        auth.Text = Login.TextBox1.Text
        Dim rep = get_devices(auth.Text)
        If rep.Length < 5 Then

            creatdevice(auth.Text, "vb.net")
        Else


        End If



        Dim array As JArray = JArray.Parse(API.get_devices(auth.Text))

        For Each item As JObject In array
            deviceId = DirectCast(item("Device id"), JValue).Value.ToString()
            app.Text = DirectCast(item("App key"), JValue).Value.ToString()

            Dim x As Image = Getqrimage(deviceId)
            If x IsNot Nothing Then
                PictureBox1.Image = x
            Else
                PictureBox1.Image = My.Resources.whatsapp_logo_background_29
                Timer1.Stop()
            End If

        Next


        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim x As Image = Getqrimage(deviceId)
        If x IsNot Nothing Then
            PictureBox1.Image = x
        Else
            PictureBox1.Image = My.Resources.whatsapp_logo_background_29
            Timer1.Stop()
        End If

    End Sub



    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick

        Timer1.Start()
    End Sub
End Class

