Imports System.Data.SqlClient
Imports System.IO
Imports TRAINEE_SYSTEM

Public Class Form4

    Dim liste() As Char = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}
    Dim _Administration As New Administration
    Dim cn_serveurCentrale As SqlConnection

    Dim _lstRubriqueArticle As New List(Of Administration)

    Private Shared _instance As Form4 = Nothing
    Private _name As String
    Private _dtagridArticles As DataGridView
    Dim chargement As Boolean = False
    Dim _dupliquer As Boolean

    Public Sub setField(ByRef modele As Administration)


        If modele.Id > 0 Then

            Me.Text = "Article : [" & Textname.Text & "] "

            Textname.Text = modele.name
            Textpassword.Text = modele.password
            Textemail.Text = modele.Email
            Texttelephone.Text = modele.telephone



            'loadGrid(modele.code)

            Textname.Enabled = False

        Else
        End If

    End Sub

    'Dim Con As New SqlConnection("Data Source=GRACENGUEDIA;Initial Catalog=TMS;User ID=matriix;Password=***********")


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        ' Dim name As String = Textname.Text
        ' Dim pword As String = Textpassword.Text
        'Dim email As String = Textemail.Text
        ' Dim telephone As Integer = Texttelephone.Text
        ' Dim Con As New SqlConnection("Data Source=GRACENGUEDIA;Initial Catalog=TMS;User ID=matriix;Password=***********")
        ' Con.Open()
        ' Dim command As New SqlCommand(" Login in to ADMINISTRATION values('" & name & "','" & pword & "','" & email & "','" & telephone & "',) ", Con)

        If Textname.Text = "" Then
            MessageBox.Show("please enter your name")
            Textname.Focus()
        ElseIf Textpassword.Text = "" Then
            MessageBox.Show("please enter your password")
            Textpassword.Focus()
        ElseIf Textemail.Text = "" Then
            MessageBox.Show("please enter your email")
            Textemail.Focus()
        ElseIf Texttelephone.Text = "" Then
            MessageBox.Show("please enter your telephone number")
            Texttelephone.Focus()


        End If

        Try
            _Administration.Name = Textname.Text.Trim
            _Administration.Password = Textpassword.Text.Trim
            _Administration.Email = Textemail.Text.Trim
            _Administration.Telephone = Texttelephone.Text.Trim
            Try
                Dim connexionString As String = File.ReadAllLines("config.ini")(0)
                LUNA.LunaContext.DateFormat = File.ReadAllLines("config.ini")(2)
                Dim cn As New SqlConnection(connexionString)
                LUNA.LunaContext.Connection = cn
                cn.Open()
                Save(_Administration)
                cn.Close()
                'Liste_Administration.table.Rows.Add(textname.Text, texttsurname.Text, textemail.Text, texttelephone.text, textlogin.text,textpassword.text,_Article.id)
                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            End Try
            'If _Administration.Save() < 1 Then
            '    MsgBox("Echec de l'opération de validation!", 1)
            '    Return
            'Else
            '    Me.Close()

            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try



        MessageBox.Show("sucessfully inserted.")

    End Sub


End Class