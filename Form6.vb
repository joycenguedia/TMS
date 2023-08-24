Imports System.Data.SqlClient
Imports System.IO
Imports TRAINEE_SYSTEM

Public Class Form6

    Dim liste() As Char = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}
    Dim _ENCADREUR As New ENCADREUR
    Dim cn_serveurCentrale As SqlConnection

    Dim _lstRubriqueArticle As New List(Of ENCADREUR)

    Private Shared _instance As Form4 = Nothing
    Private _name As String
    Private _dtagridArticles As DataGridView
    Dim chargement As Boolean = False
    Dim _dupliquer As Boolean


    Public Sub setField(ByRef modele As ENCADREUR)


        If modele.Id > 0 Then

            Me.Text = "Article : [" & Textname.Text & "] " & Textsurname.Text

            Textname.Text = modele.name
            Textsurname.Text = modele.Surname
            TextPassword.Text = modele.password
            Textdepartment.Text = modele.department
            Texttelephone.Text = modele.telephone
            Textemail.Text = modele.Email


            'loadGrid(modele.code)

            Textname.Enabled = False

        Else
        End If


    End Sub


    'Dim Con As New SqlConnection("Data Source=GRACENGUEDIA;Initial Catalog=TMS;User ID=matriix;Password=***********")
    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click





        ' Dim name As String = Textname.Text
        ' Dim sname As String = Textsurname.Text
        'Dim email As String = Textemail.Text
        ' Dim telephone As Integer = Texttelephone.Text
        ' Dim login As String = TextLogin.Text
        ' Dim pword As String = Textpassword.Text
        ' Dim Con As New SqlConnection("Data Source=GRACENGUEDIA;Initial Catalog=TMS;User ID=matriix;Password=***********")
        ' Con.Open()
        ' Dim command As New SqlCommand(" Login in to ADMINISTRATION values('" & name & "','" & sname & "','" & password & "','" & department & "''" & telephone & "''" & email & "') ", Con)
        If Textname.Text = "" Then
            MessageBox.Show("please enter your name")
            Textname.Focus()
        ElseIf Textsurname.Text = "" Then
            MessageBox.Show("please enter your surname")
            Textsurname.Focus()
        ElseIf Textpassword.Text = "" Then
            MessageBox.Show("please enter your password")
            Textpassword.Focus()
        ElseIf Textdepartment.Text = "" Then
            MessageBox.Show("please enter your department")
            Textdepartment.Focus()
        ElseIf Texttelephone.Text = "" Then
            MessageBox.Show("please enter your telephone")
            Texttelephone.Focus()
        ElseIf Textemail.Text = "" Then
            MessageBox.Show("please enter your email")
            Textemail.Focus()



        End If

        Try
            _ENCADREUR.Name = Textname.Text.Trim
            _ENCADREUR.Surname = Textsurname.Text.Trim
            _ENCADREUR.Password = Textpassword.Text.Trim
            _ENCADREUR.DEPARTMENT = Textdepartment.Text.Trim
            _ENCADREUR.Telephone = Texttelephone.Text.Trim
            _ENCADREUR.Email = Textemail.Text.Trim



            Try
                Dim connexionString As String = File.ReadAllLines("config.ini")(0)
                LUNA.LunaContext.DateFormat = File.ReadAllLines("config.ini")(2)
                Dim cn As New SqlConnection(connexionString)
                LUNA.LunaContext.Connection = cn
                cn.Open()
                Save(_ENCADREUR)
                cn.Close()
                'Liste_Administration.table.Rows.Add(textname.Text, texttsurname.Text, textPASSWORD.Text, textdepartment.text, texttelephone.text,textemail.text,_Article.id)
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