#Region "Author"
'Class created with Luna 3.4.6.11
'Author: Diego Lunadei
'Date: 8/22/2023
#End Region

Imports System
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class Encadreur
    Inherits LUNA.LunaBaseClassEntity
    '******IMPORTANT: Don't write your code here. Write your code in the Class object that use this Partial Class.
    '******So you can replace DAOClass and EntityClass without lost your code

    Public Sub New()

    End Sub

#Region "Database Field Map"

    Protected _ENCADREUR_ID As Integer = 0

    <XmlElementAttribute("ENCADREUR_ID")>
    Public Property ENCADREUR_ID() As Integer
        Get
            Return _ENCADREUR_ID
        End Get
        Set(ByVal value As Integer)
            If _ENCADREUR_ID <> value Then
                IsChanged = True
                _ENCADREUR_ID = value
            End If
        End Set
    End Property

    Protected _NAME As String = ""

    <XmlElementAttribute("NAME")>
    Public Property NAME() As String
        Get
            Return _NAME
        End Get
        Set(ByVal value As String)
            If _NAME <> value Then
                IsChanged = True
                _NAME = value
            End If
        End Set
    End Property

    Protected _SURNAME As String = ""

    <XmlElementAttribute("SURNAME")>
    Public Property SURNAME() As String
        Get
            Return _SURNAME
        End Get
        Set(ByVal value As String)
            If _SURNAME <> value Then
                IsChanged = True
                _SURNAME = value
            End If
        End Set
    End Property

    Protected _PASSWORD As String = ""

    <XmlElementAttribute("PASSWORD")>
    Public Property PASSWORD() As String
        Get
            Return _PASSWORD
        End Get
        Set(ByVal value As String)
            If _PASSWORD <> value Then
                IsChanged = True
                _PASSWORD = value
            End If
        End Set
    End Property

    Protected _DEPARTMENT As String = ""

    <XmlElementAttribute("DEPARTMENT")>
    Public Property DEPARTMENT() As String
        Get
            Return _DEPARTMENT
        End Get
        Set(ByVal value As String)
            If _DEPARTMENT <> value Then
                IsChanged = True
                _DEPARTMENT = value
            End If
        End Set
    End Property

    Protected _TELEPHONE As Integer = 0

    <XmlElementAttribute("TELEPHONE")>
    Public Property TELEPHONE() As Integer
        Get
            Return _TELEPHONE
        End Get
        Set(ByVal value As Integer)
            If _TELEPHONE <> value Then
                IsChanged = True
                _TELEPHONE = value
            End If
        End Set
    End Property

    Protected _EMAIL As String = ""

    <XmlElementAttribute("EMAIL")>
    Public Property EMAIL() As String
        Get
            Return _EMAIL
        End Get
        Set(ByVal value As String)
            If _EMAIL <> value Then
                IsChanged = True
                _EMAIL = value
            End If
        End Set
    End Property
#End Region

#Region "Method"
    ''' <summary>
    '''This method read an Encadreur from DB.
    ''' </summary>
    ''' <returns>
    '''Return 0 if all ok, 1 if error
    ''' </returns>
    Public Overridable Function Read(Id As Integer) As Integer
        'Return 0 if all ok
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New EncadreurDAO
            Dim int As Encadreur = Mgr.Read(Id)
            _ENCADREUR_ID = int.ENCADREUR_ID
            _NAME = int.NAME
            _SURNAME = int.SURNAME
            _PASSWORD = int.PASSWORD
            _DEPARTMENT = int.DEPARTMENT
            _TELEPHONE = int.TELEPHONE
            _EMAIL = int.EMAIL
            Mgr.Dispose()
        Catch ex As Exception
            ManageError(ex)
            Ris = 1
        End Try
        Return Ris
    End Function

    ''' <summary>
    '''This method save an Encadreur on DB.
    ''' </summary>
    ''' <returns>
    '''Return Id insert in DB if all ok, 0 if error
    ''' </returns>
    Public Overridable Function Save() As Integer
        'Return the id Inserted
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New EncadreurDAO
            Ris = Mgr.Save(Me)
            Mgr.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ris
    End Function

    Private Function InternalIsValid() As Boolean
        Dim Ris As Boolean = True
        If _NAME.Length > 50 Then Ris = False
        If _SURNAME.Length > 50 Then Ris = False
        If _PASSWORD.Length > 50 Then Ris = False
        If _DEPARTMENT.Length > 50 Then Ris = False
        If _EMAIL.Length > 50 Then Ris = False
        Return Ris
    End Function

#End Region

#Region "Embedded Class"

#End Region

End Class

''' <summary>
'''This class manage persistency on db of Encadreur object
''' </summary>
''' <remarks>
'''
''' </remarks>
Partial Public Class EncadreurDAO
    Inherits LUNA.LunaBaseClassDAO(Of Encadreur)

    ''' <summary>
    '''New() create an istance of this class. Use default DB Connection
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    '''New() create an istance of this class and specify a DB connection
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Sub New(ByVal Connection As Data.SqlClient.SqlConnection)
        MyBase.New(Connection)
    End Sub

    ''' <summary>
    '''New() create an istance of this class and specify a DB connectionstring
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Sub New(ByVal ConnectionString As String)
        MyBase.New(ConnectionString)
    End Sub

    ''' <summary>
    '''Read from DB table Encadreur
    ''' </summary>
    ''' <returns>
    '''Return an Encadreur object
    ''' </returns>
    Public Overrides Function Read(Id As Integer) As Encadreur
        Dim cls As New Encadreur

        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = "SELECT * FROM Encadreur where ENCADREUR_ID = " & Id
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            myReader.Read()
            If myReader.HasRows Then
                cls.ENCADREUR_ID = myReader("ENCADREUR_ID")
                If Not myReader("NAME") Is DBNull.Value Then
                    cls.NAME = myReader("NAME")
                End If
                If Not myReader("SURNAME") Is DBNull.Value Then
                    cls.SURNAME = myReader("SURNAME")
                End If
                If Not myReader("PASSWORD") Is DBNull.Value Then
                    cls.PASSWORD = myReader("PASSWORD")
                End If
                If Not myReader("DEPARTMENT") Is DBNull.Value Then
                    cls.DEPARTMENT = myReader("DEPARTMENT")
                End If
                If Not myReader("TELEPHONE") Is DBNull.Value Then
                    cls.TELEPHONE = myReader("TELEPHONE")
                End If
                If Not myReader("EMAIL") Is DBNull.Value Then
                    cls.EMAIL = myReader("EMAIL")
                End If
            End If
            myReader.Close()
            myCommand.Dispose()

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return cls
    End Function

    ''' <summary>
    '''Save on DB table Encadreur
    ''' </summary>
    ''' <returns>
    '''Return ID insert in DB
    ''' </returns>
    Public Overrides Function Save(ByRef cls As Encadreur) As Integer

        Dim Ris As Integer = 0 'in Ris return Insert Id

        If cls.IsValid Then
            If cls.IsChanged Then
                Dim DbCommand As SqlCommand = New SqlCommand()
                Try
                    Dim sql As String
                    DbCommand.Connection = _cn
                    If Not DbTransaction Is Nothing Then DbCommand.Transaction = DbTransaction
                    If cls.ENCADREUR_ID = 0 Then
                        sql = "INSERT INTO Encadreur ("
                        sql &= "NAME,"
                        sql &= "SURNAME,"
                        sql &= "PASSWORD,"
                        sql &= "DEPARTMENT,"
                        sql &= "TELEPHONE,"
                        sql &= "EMAIL"
                        sql &= ") VALUES ("
                        sql &= "@NAME,"
                        sql &= "@SURNAME,"
                        sql &= "@PASSWORD,"
                        sql &= "@DEPARTMENT,"
                        sql &= "@TELEPHONE,"
                        sql &= "@EMAIL"
                        sql &= ")"
                    Else
                        sql = "UPDATE Encadreur SET "
                        sql &= "NAME = @NAME,"
                        sql &= "SURNAME = @SURNAME,"
                        sql &= "PASSWORD = @PASSWORD,"
                        sql &= "DEPARTMENT = @DEPARTMENT,"
                        sql &= "TELEPHONE = @TELEPHONE,"
                        sql &= "EMAIL = @EMAIL"
                        sql &= " WHERE ENCADREUR_ID= " & cls.ENCADREUR_ID
                    End If
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@NAME", cls.NAME))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@SURNAME", cls.SURNAME))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@PASSWORD", cls.PASSWORD))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@DEPARTMENT", cls.DEPARTMENT))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@TELEPHONE", cls.TELEPHONE))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@EMAIL", cls.EMAIL))
                    DbCommand.CommandType = CommandType.Text
                    DbCommand.CommandText = sql
                    DbCommand.ExecuteNonQuery()

                    If cls.ENCADREUR_ID = 0 Then
                        Dim IdInserito As Integer = 0
                        sql = "select @@identity"
                        DbCommand.CommandText = sql
                        IdInserito = DbCommand.ExecuteScalar()
                        cls.ENCADREUR_ID = IdInserito
                        Ris = IdInserito
                    Else
                        Ris = cls.ENCADREUR_ID
                    End If

                    DbCommand.Dispose()

                Catch ex As Exception
                    ManageError(ex)
                End Try
            Else
                Ris = cls.ENCADREUR_ID
            End If

        Else
            Err.Raise(602, "Object data is not valid")
        End If
        Return Ris
    End Function

    Private Sub DestroyPermanently(Id As Integer)
        Try

            Dim UpdateCommand As SqlCommand = New SqlCommand()
            UpdateCommand.Connection = _cn

            '******IMPORTANT: You can use this commented instruction to make a logical delete .
            '******Replace DELETED Field with your logic deleted field name.
            'Dim Sql As String = "UPDATE Encadreur SET DELETED=True "
            Dim Sql As String = "DELETE FROM Encadreur"
            Sql &= " Where ENCADREUR_ID = " & Id

            UpdateCommand.CommandText = Sql
            If Not DbTransaction Is Nothing Then UpdateCommand.Transaction = DbTransaction
            UpdateCommand.ExecuteNonQuery()
            UpdateCommand.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
    End Sub

    ''' <summary>
    '''Delete from DB table Encadreur. Accept id of object to delete.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(Id As Integer)

        DestroyPermanently(Id)

    End Sub

    ''' <summary>
    '''Delete from DB table Encadreur. Accept object to delete and optional a List to remove the object from.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(ByRef obj As Encadreur, Optional ByRef ListaObj As List(Of Encadreur) = Nothing)

        DestroyPermanently(obj.ENCADREUR_ID)
        If Not ListaObj Is Nothing Then ListaObj.Remove(obj)

    End Sub

    Public Overloads Function Find(ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Encadreur)
        Return FindReal(0, OrderBy, Parameter)
    End Function

    Public Overloads Function Find(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Encadreur)
        Return FindReal(Top, OrderBy, Parameter)
    End Function

    Public Overrides Function Find(ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Encadreur)
        Return FindReal(0, "", Parameter)
    End Function

    Private Function FindReal(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Encadreur)
        Dim Ls As New List(Of Encadreur)
        Try

            Dim sql As String = ""
sql ="SELECT " & IIf(Top, Top, "") & " ENCADREUR_ID," & _
	"NAME," & _
	"SURNAME," & _
	"PASSWORD," & _
	"DEPARTMENT," & _
	"TELEPHONE," & _
	"EMAIL"
sql &=" from Encadreur" 
For Each Par As LUNA.LunaSearchParameter In Parameter
	If Not Par Is Nothing Then
		If Sql.IndexOf("WHERE") = -1 Then Sql &= " WHERE " Else Sql &=  " " & Par.LogicOperatorStr & " "
		Sql &= Par.FieldName & " " & Par.SqlOperator & " " & Ap(Par.Value)
	End if
Next

If OrderBy.Length Then Sql &= " ORDER BY " & OrderBy

Ls = GetData(Sql)

Catch ex As Exception
	ManageError(ex)
End Try
Return Ls
End Function

Public Overrides Function GetAll(Optional OrderByField as string = "", Optional ByVal AddEmptyItem As Boolean = False) as iEnumerable(Of Encadreur)
Dim Ls As New List(Of Encadreur)
Try

            Dim sql As String = ""
            sql = "SELECT ENCADREUR_ID," &
    "NAME," &
    "SURNAME," &
    "PASSWORD," &
    "DEPARTMENT," &
    "TELEPHONE," &
    "EMAIL"
            sql &= " from Encadreur"
            If OrderByField.Length Then
                sql &= " ORDER BY " & OrderByField
            End If

            Ls = GetData(sql, AddEmptyItem)

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
    Private Function GetData(sql As String, Optional ByVal AddEmptyItem As Boolean = False) As iEnumerable(Of Encadreur)
        Dim Ls As New List(Of Encadreur)
        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = sql
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            If AddEmptyItem Then Ls.Add(New Encadreur() With {.ENCADREUR_ID = 0, .NAME = "", .SURNAME = "", .PASSWORD = "", .DEPARTMENT = "", .TELEPHONE = 0, .EMAIL = ""})
            While myReader.Read
                Dim classe As New Encadreur
                If Not myReader("ENCADREUR_ID") Is DBNull.Value Then classe.ENCADREUR_ID = myReader("ENCADREUR_ID")
                If Not myReader("NAME") Is DBNull.Value Then classe.NAME = myReader("NAME")
                If Not myReader("SURNAME") Is DBNull.Value Then classe.SURNAME = myReader("SURNAME")
                If Not myReader("PASSWORD") Is DBNull.Value Then classe.PASSWORD = myReader("PASSWORD")
                If Not myReader("DEPARTMENT") Is DBNull.Value Then classe.DEPARTMENT = myReader("DEPARTMENT")
                If Not myReader("TELEPHONE") Is DBNull.Value Then classe.TELEPHONE = myReader("TELEPHONE")
                If Not myReader("EMAIL") Is DBNull.Value Then classe.EMAIL = myReader("EMAIL")
                Ls.Add(classe)
            End While
            myReader.Close()
            myCommand.Dispose()

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
End Class


''' <summary>
'''This class manage persistency on db of Encadreur object
''' </summary>
''' <remarks>
'''
''' </remarks>
Partial Public Class EncadreurDAO
    Inherits LUNA.LunaBaseClassDAO(Of Encadreur)

    ''' <summary>
    '''New() create an istance of this class. Use default DB Connection
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    '''New() create an istance of this class and specify a DB connection
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Sub New(ByVal Connection As Data.SqlClient.SqlConnection)
        MyBase.New(Connection)
    End Sub

    ''' <summary>
    '''New() create an istance of this class and specify a DB connectionstring
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Sub New(ByVal ConnectionString As String)
        MyBase.New(ConnectionString)
    End Sub

    ''' <summary>
    '''Read from DB table Encadreur
    ''' </summary>
    ''' <returns>
    '''Return an Encadreur object
    ''' </returns>
    Public Overrides Function Read(Id As Integer) As Encadreur
        Dim cls As New Encadreur

        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = "SELECT * FROM Encadreur where ENCADREUR_ID = " & Id
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            myReader.Read()
            If myReader.HasRows Then
                cls.ENCADREUR_ID = myReader("ENCADREUR_ID")
                If Not myReader("NAME") Is DBNull.Value Then
                    cls.NAME = myReader("NAME")
                End If
                If Not myReader("SURNAME") Is DBNull.Value Then
                    cls.SURNAME = myReader("SURNAME")
                End If
                If Not myReader("PASSWORD") Is DBNull.Value Then
                    cls.PASSWORD = myReader("PASSWORD")
                End If
                If Not myReader("DEPARTMENT") Is DBNull.Value Then
                    cls.DEPARTMENT = myReader("DEPARTMENT")
                End If
                If Not myReader("TELEPHONE") Is DBNull.Value Then
                    cls.TELEPHONE = myReader("TELEPHONE")
                End If
                If Not myReader("EMAIL") Is DBNull.Value Then
                    cls.EMAIL = myReader("EMAIL")
                End If
            End If
            myReader.Close()
            myCommand.Dispose()

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return cls
    End Function

    ''' <summary>
    '''Save on DB table Encadreur
    ''' </summary>
    ''' <returns>
    '''Return ID insert in DB
    ''' </returns>
    Public Overrides Function Save(ByRef cls As Encadreur) As Integer

        Dim Ris As Integer = 0 'in Ris return Insert Id

        If cls.IsValid Then
            If cls.IsChanged Then
                Dim DbCommand As SqlCommand = New SqlCommand()
                Try
                    Dim sql As String
                    DbCommand.Connection = _cn
                    If Not DbTransaction Is Nothing Then DbCommand.Transaction = DbTransaction
                    If cls.ENCADREUR_ID = 0 Then
                        sql = "INSERT INTO Encadreur ("
                        sql &= "NAME,"
                        sql &= "SURNAME,"
                        sql &= "PASSWORD,"
                        sql &= "DEPARTMENT,"
                        sql &= "TELEPHONE,"
                        sql &= "EMAIL"
                        sql &= ") VALUES ("
                        sql &= "@NAME,"
                        sql &= "@SURNAME,"
                        sql &= "@PASSWORD,"
                        sql &= "@DEPARTMENT,"
                        sql &= "@TELEPHONE,"
                        sql &= "@EMAIL"
                        sql &= ")"
                    Else
                        sql = "UPDATE Encadreur SET "
                        sql &= "NAME = @NAME,"
                        sql &= "SURNAME = @SURNAME,"
                        sql &= "PASSWORD = @PASSWORD,"
                        sql &= "DEPARTMENT = @DEPARTMENT,"
                        sql &= "TELEPHONE = @TELEPHONE,"
                        sql &= "EMAIL = @EMAIL"
                        sql &= " WHERE ENCADREUR_ID= " & cls.ENCADREUR_ID
                    End If
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@NAME", cls.NAME))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@SURNAME", cls.SURNAME))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@PASSWORD", cls.PASSWORD))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@DEPARTMENT", cls.DEPARTMENT))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@TELEPHONE", cls.TELEPHONE))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@EMAIL", cls.EMAIL))
                    DbCommand.CommandType = CommandType.Text
                    DbCommand.CommandText = sql
                    DbCommand.ExecuteNonQuery()

                    If cls.ENCADREUR_ID = 0 Then
                        Dim IdInserito As Integer = 0
                        sql = "select @@identity"
                        DbCommand.CommandText = sql
                        IdInserito = DbCommand.ExecuteScalar()
                        cls.ENCADREUR_ID = IdInserito
                        Ris = IdInserito
                    Else
                        Ris = cls.ENCADREUR_ID
                    End If

                    DbCommand.Dispose()

                Catch ex As Exception
                    ManageError(ex)
                End Try
            Else
                Ris = cls.ENCADREUR_ID
            End If

        Else
            Err.Raise(602, "Object data is not valid")
        End If
        Return Ris
    End Function

    Private Sub DestroyPermanently(Id As Integer)
        Try

            Dim UpdateCommand As SqlCommand = New SqlCommand()
            UpdateCommand.Connection = _cn

            '******IMPORTANT: You can use this commented instruction to make a logical delete .
            '******Replace DELETED Field with your logic deleted field name.
            'Dim Sql As String = "UPDATE Encadreur SET DELETED=True "
            Dim Sql As String = "DELETE FROM Encadreur"
            Sql &= " Where ENCADREUR_ID = " & Id

            UpdateCommand.CommandText = Sql
            If Not DbTransaction Is Nothing Then UpdateCommand.Transaction = DbTransaction
            UpdateCommand.ExecuteNonQuery()
            UpdateCommand.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
    End Sub

    ''' <summary>
    '''Delete from DB table Encadreur. Accept id of object to delete.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(Id As Integer)

        DestroyPermanently(Id)

    End Sub

    ''' <summary>
    '''Delete from DB table Encadreur. Accept object to delete and optional a List to remove the object from.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(ByRef obj As Encadreur, Optional ByRef ListaObj As List(Of Encadreur) = Nothing)

        DestroyPermanently(obj.ENCADREUR_ID)
        If Not ListaObj Is Nothing Then ListaObj.Remove(obj)

    End Sub

    Public Overloads Function Find(ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Encadreur)
        Return FindReal(0, OrderBy, Parameter)
    End Function

    Public Overloads Function Find(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Encadreur)
        Return FindReal(Top, OrderBy, Parameter)
    End Function

    Public Overrides Function Find(ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Encadreur)
        Return FindReal(0, "", Parameter)
    End Function

    Private Function FindReal(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Encadreur)
        Dim Ls As New List(Of Encadreur)
        Try

            Dim sql As String = ""
sql ="SELECT " & IIf(Top, Top, "") & " ENCADREUR_ID," & _
	"NAME," & _
	"SURNAME," & _
	"PASSWORD," & _
	"DEPARTMENT," & _
	"TELEPHONE," & _
	"EMAIL"
sql &=" from Encadreur" 
For Each Par As LUNA.LunaSearchParameter In Parameter
	If Not Par Is Nothing Then
		If Sql.IndexOf("WHERE") = -1 Then Sql &= " WHERE " Else Sql &=  " " & Par.LogicOperatorStr & " "
		Sql &= Par.FieldName & " " & Par.SqlOperator & " " & Ap(Par.Value)
	End if
Next

If OrderBy.Length Then Sql &= " ORDER BY " & OrderBy

Ls = GetData(Sql)

Catch ex As Exception
	ManageError(ex)
End Try
Return Ls
End Function

Public Overrides Function GetAll(Optional OrderByField as string = "", Optional ByVal AddEmptyItem As Boolean = False) as iEnumerable(Of Encadreur)
Dim Ls As New List(Of Encadreur)
Try

            Dim sql As String = ""
            sql = "SELECT ENCADREUR_ID," &
    "NAME," &
    "SURNAME," &
    "PASSWORD," &
    "DEPARTMENT," &
    "TELEPHONE," &
    "EMAIL"
            sql &= " from Encadreur"
            If OrderByField.Length Then
                sql &= " ORDER BY " & OrderByField
            End If

            Ls = GetData(sql, AddEmptyItem)

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
    Private Function GetData(sql As String, Optional ByVal AddEmptyItem As Boolean = False) As iEnumerable(Of Encadreur)
        Dim Ls As New List(Of Encadreur)
        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = sql
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            If AddEmptyItem Then Ls.Add(New Encadreur() With {.ENCADREUR_ID = 0, .NAME = "", .SURNAME = "", .PASSWORD = "", .DEPARTMENT = "", .TELEPHONE = 0, .EMAIL = ""})
            While myReader.Read
                Dim classe As New Encadreur
                If Not myReader("ENCADREUR_ID") Is DBNull.Value Then classe.ENCADREUR_ID = myReader("ENCADREUR_ID")
                If Not myReader("NAME") Is DBNull.Value Then classe.NAME = myReader("NAME")
                If Not myReader("SURNAME") Is DBNull.Value Then classe.SURNAME = myReader("SURNAME")
                If Not myReader("PASSWORD") Is DBNull.Value Then classe.PASSWORD = myReader("PASSWORD")
                If Not myReader("DEPARTMENT") Is DBNull.Value Then classe.DEPARTMENT = myReader("DEPARTMENT")
                If Not myReader("TELEPHONE") Is DBNull.Value Then classe.TELEPHONE = myReader("TELEPHONE")
                If Not myReader("EMAIL") Is DBNull.Value Then classe.EMAIL = myReader("EMAIL")
                Ls.Add(classe)
            End While
            myReader.Close()
            myCommand.Dispose()

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
End Class


