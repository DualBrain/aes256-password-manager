Imports PassCrypt.Crypto
Imports System.IO

Public Class Form1


    Dim istart As Integer
    Dim ilen As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            RichTextBox1.Text = ""
            Dim strFile As String = "DB.crypt"
            Dim sr As New IO.StreamReader(strFile)
            Dim testocriptato As String
            testocriptato = sr.ReadToEnd()
            sr.Close()
            Dim aes As AES256 = New AES256(TextBox1.Text)
            RichTextBox1.Text = aes.Decrypt(testocriptato)

            If RichTextBox1.Text <> "Chiave errata" Then
                Cerca.Enabled = True
                CercaTesto.ReadOnly = False
                Precedente.Enabled = True
                Successivo.Enabled = True
                BloccaProfilo.Enabled = True
                HelpToolStripMenuItem.Enabled = True
                TextBox1.Text = "Inserisci password"

                Label1.Visible = False
                TextBox1.Visible = False
                Button1.Visible = False
            End If

        Catch
            RichTextBox1.Text = "Database non ancora generato. Clicca sull'icona in alto a sinistra per aprire la pagina di gestione del database e crearne uno."
        End Try

    End Sub

    Private Sub TextBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Click
        If TextBox1.Text = "Inserisci password" Then
            TextBox1.Text = ""
        Else
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Form2.Show()
    End Sub

    Private Sub CreaProfiloToolStripMenuItem_Click(sender As Object, e As EventArgs) 
        Form2.Show()
    End Sub

    Private Sub EsciToolStripMenuItem_Click(sender As Object, e As EventArgs) 
        Me.Close()
    End Sub


    Private Sub LicenzaToolStripMenuItem_Click(sender As Object, e As EventArgs) 
        Licenza.Show()
    End Sub

    Private Sub BloccaToolStripMenuItem_Click(sender As Object, e As EventArgs) 
        RichTextBox1.Text = "Programma bloccato, reinserisci la password per decriptare le informazioni"
        Cerca.Enabled = False
        Precedente.Enabled = False
        Successivo.Enabled = False
        CercaTesto.ReadOnly = True
        BloccaProfilo.Enabled = False
        HelpToolStripMenuItem.Enabled = False
        Button1.Enabled = True

        Label1.Visible = True
        TextBox1.Visible = True
        Button1.Visible = True
    End Sub

    Private Sub Cerca_Click(sender As Object, e As EventArgs) Handles Cerca.Click
        istart = InStr(RichTextBox1.Text, CercaTesto.Text, CompareMethod.Text)
        If istart = 0 Then
            MsgBox("nessuna corrispondenza")
            Exit Sub
        End If
        ilen = CercaTesto.TextLength
        RichTextBox1.Focus()
        RichTextBox1.SelectionStart = istart - 1
        RichTextBox1.SelectionLength = ilen
    End Sub

    Private Sub CercaTesto_Click(sender As Object, e As EventArgs) Handles CercaTesto.Click
        CercaTesto.Text = ""
        Me.AcceptButton = CercaNascosto

    End Sub

    Private Sub CercaNascosto_Click(sender As Object, e As EventArgs) Handles CercaNascosto.Click
        istart = InStr(RichTextBox1.Text, CercaTesto.Text, CompareMethod.Text)
        If istart = 0 Then
            MsgBox("Nessuna corrispondenza")
            Exit Sub
        End If
        ilen = CercaTesto.TextLength
        RichTextBox1.Focus()
        RichTextBox1.SelectionStart = istart - 1
        RichTextBox1.SelectionLength = ilen
    End Sub

    Private Sub Precedente_Click(sender As Object, e As EventArgs) Handles Precedente.Click
        istart = InStrRev(RichTextBox1.Text, CercaTesto.Text, istart - 1, CompareMethod.Text)
        If istart = 0 Then
            MsgBox("Nessuna corrispondenza")
            Exit Sub
        End If
        ilen = CercaTesto.TextLength
        RichTextBox1.Focus()
        RichTextBox1.SelectionStart = istart - 1
        RichTextBox1.SelectionLength = ilen
    End Sub

    Private Sub Successivo_Click(sender As Object, e As EventArgs) Handles Successivo.Click
        istart = InStr(istart + ilen - 1, RichTextBox1.Text, CercaTesto.Text, CompareMethod.Text)
        If istart = 0 Then
            MsgBox("Nessuna corrispondenza")
            Exit Sub
        End If
        ilen = CercaTesto.TextLength
        RichTextBox1.Focus()
        RichTextBox1.SelectionStart = istart - 1
        RichTextBox1.SelectionLength = ilen
    End Sub

    Private Sub BloccaProfilo_Click(sender As Object, e As EventArgs) Handles BloccaProfilo.Click
        RichTextBox1.Text = "Programma bloccato, reinserisci la password per decriptare le informazioni"
        Cerca.Enabled = False
        Precedente.Enabled = False
        Successivo.Enabled = False
        CercaTesto.ReadOnly = True
        BloccaProfilo.Enabled = False
        Button1.Enabled = True

        Label1.Visible = True
        TextBox1.Visible = True
        Button1.Visible = True
        HelpToolStripMenuItem.Enabled = False
    End Sub

    Private Sub Aggiungi_Click(sender As Object, e As EventArgs) Handles Aggiungi.Click
        Form2.Show()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) 
        About.Show()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If File.Exists("DB.crypt") = False Then
            NewToolStripMenuItem.Enabled = True
            OpenToolStripMenuItem.Enabled = False
            RichTextBox1.Text = "Sembra sia la prima volta che accedi, in caso contrario il programma non riesce a recuperare il tuo database. Per crearne uno nuovo clicca sull'icona in alto a sinistra che ti porterà nella pagina di gestione e creazione del database."
        End If
    End Sub

    Private Sub Feedback_Click(sender As Object, e As EventArgs) Handles Feedback.Click
        Dim url As String = "mailto:cttynul@gmail.com"
        Process.Start(url)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        About.Show()
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Form2.Show()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Form2.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        istart = InStr(RichTextBox1.Text, CercaTesto.Text, CompareMethod.Text)
        If istart = 0 Then
            MsgBox("Nessuna corrispondenza! Inserisci il testo da cercare all'interno della search bar presente nel menu principale.")
            Exit Sub
        End If
        ilen = CercaTesto.TextLength
        RichTextBox1.Focus()
        RichTextBox1.SelectionStart = istart - 1
        RichTextBox1.SelectionLength = ilen
    End Sub

    Private Sub LicenzaToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles LicenzaToolStripMenuItem.Click
        Licenza.Show()
    End Sub

    Private Sub AboutToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem2.Click
        About.Show()
    End Sub

    Private Sub InviaUnFeedbackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InviaUnFeedbackToolStripMenuItem.Click
        Dim url As String = "mailto:cttynul@gmail.com"
        Process.Start(url)
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        RichTextBox1.Text = "Programma bloccato, reinserisci la password per decriptare le informazioni"
        Cerca.Enabled = False
        Precedente.Enabled = False
        Successivo.Enabled = False
        CercaTesto.ReadOnly = True
        BloccaProfilo.Enabled = False
        Button1.Enabled = True

        Label1.Visible = True
        TextBox1.Visible = True
        Button1.Visible = True
        HelpToolStripMenuItem.Enabled = False
    End Sub
End Class
