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

Partial Public Class Stagiaire
    Inherits LUNA.LunaBaseClassEntity
    '******IMPORTANT: Don't write your code here. Write your code in the Class object that use this Partial Class.
    '******So you can replace DAOClass and EntityClass without lost your code

    Public Sub New()

    End Sub

#Region "Database Field Map"

    Protected _STAGIAIRE_ID As Integer = 0

    <XmlElementAttribute("STAGIAIRE_ID")>
    Public Property STAGIAIRE_ID() As Integer
        Get
            Return _STAGIAIRE_ID
        End Get
        Set(ByVal value As Integer)
            If _STAGIAIRE_ID <> value Then
                IsChanged = True
                _STAGIAIRE_ID = value
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

    Protected _GENDER As String = ""

    <XmlElementAttribute("GENDER")>
    Public Property GENDER() As String
        Get
            Return _GENDER
        End Get
        Set(ByVal value As String)
            If _GENDER <> value Then
                IsChanged = True
                _GENDER = value
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

    Protected _BEGINNING_DATE As DateTime = Nothing

    <XmlElementAttribute("BEGINNING_DATE")>
    Public Property BEGINNING_DATE() As DateTime
        Get
            Return _BEGINNING_DATE
        End Get
        Set(ByVal value As DateTime)
            If _BEGINNING_DATE <> value Then
                IsChanged = True
                _BEGINNING_DATE = value
            End If
        End Set
    End Property

    Protected _ENDING_DATE As DateTime = Nothing

    <XmlElementAttribute("ENDING_DATE")>
    Public Property ENDING_DATE() As DateTime
        Get
            Return _ENDING_DATE
        End Get
        Set(ByVal value As DateTime)
            If _ENDING_DATE <> value Then
                IsChanged = True
                _ENDING_DATE = value
            End If
        End Set
    End Property
#End Region

#Region "Method"
    ''' <summary>
    '''This method read an Stagiaire from DB.
    ''' </summary>
    ''' <returns>
    '''Return 0 if all ok, 1 if error
    ''' </returns>
    Public Overridable Function Read(Id As Integer) As Integer
        'Return 0 if all ok
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New StagiaireDAO
            Dim int As Stagiaire = Mgr.Read(Id)
            _STAGIAIRE_ID = int.STAGIAIRE_ID
            _NAME = int.NAME
            _SURNAME = int.SURNAME
            _DEPARTMENT = int.DEPARTMENT
            _GENDER = int.GENDER
            _TELEPHONE = int.TELEPHONE
            _EMAIL = int.EMAIL
            _BEGINNING_DATE = int.BEGINNING_DATE
            _ENDING_DATE = int.ENDING_DATE
            Mgr.Dispose()
        Catch ex As Exception
            ManageError(ex)
            Ris = 1
        End Try
        Return Ris
    End Function

    ''' <summary>
    '''This method save an Stagiaire on DB.
    ''' </summary>
    ''' <returns>
    '''Return Id insert in DB if all ok, 0 if error
    ''' </returns>
    Public Overridable Function Save() As Integer
        'Return the id Inserted
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New StagiaireDAO
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
        If _DEPARTMENT.Length > 50 Then Ris = False
        If _GENDER.Length > 50 Then Ris = False
        If _EMAIL.Length > 50 Then Ris = False
        Return Ris
    End Function

#End Region

#Region "Embedded Class"

#End Region

End Class

''' <summary>
'''This class manage persistency on db of Stagiaire object
''' </summary>
''' <remarks>
'''
''' </remarks>
Partial Public Class StagiaireDAO
    Inherits LUNA.LunaBaseClassDAO(Of Stagiaire)

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
    '''Read from DB table Stagiaire
    ''' </summary>
    ''' <returns>
    '''Return an Stagiaire object
    ''' </returns>
    Public Overrides Function Read(Id As Integer) As Stagiaire
        Dim cls As New Stagiaire

        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = "SELECT * FROM Stagiaire where STAGIAIRE_ID = " & Id
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            myReader.Read()
            If myReader.HasRows Then
                cls.STAGIAIRE_ID = myReader("STAGIAIRE_ID")
                If Not myReader("NAME") Is DBNull.Value Then
                    cls.NAME = myReader("NAME")
                End If
                If Not myReader("SURNAME") Is DBNull.Value Then
                    cls.SURNAME = myReader("SURNAME")
                End If
                If Not myReader("DEPARTMENT") Is DBNull.Value Then
                    cls.DEPARTMENT = myReader("DEPARTMENT")
                End If
                If Not myReader("GENDER") Is DBNull.Value Then
                    cls.GENDER = myReader("GENDER")
                End If
                If Not myReader("TELEPHONE") Is DBNull.Value Then
                    cls.TELEPHONE = myReader("TELEPHONE")
                End If
                If Not myReader("EMAIL") Is DBNull.Value Then
                    cls.EMAIL = myReader("EMAIL")
                End If
                If Not myReader("BEGINNING_DATE") Is DBNull.Value Then
                    cls.BEGINNING_DATE = myReader("BEGINNING_DATE")
                End If
                If Not myReader("ENDING_DATE") Is DBNull.Value Then
                    cls.ENDING_DATE = myReader("ENDING_DATE")
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
    '''Save on DB table Stagiaire
    ''' </summary>
    ''' <returns>
    '''Return ID insert in DB
    ''' </returns>
    Public Overrides Function Save(ByRef cls As Stagiaire) As Integer

        Dim Ris As Integer = 0 'in Ris return Insert Id

        If cls.IsValid Then
            If cls.IsChanged Then
                Dim DbCommand As SqlCommand = New SqlCommand()
                Try
                    Dim sql As String
                    DbCommand.Connection = _cn
                    If Not DbTransaction Is Nothing Then DbCommand.Transaction = DbTransaction
                    If cls.STAGIAIRE_ID = 0 Then
                        sql = "INSERT INTO Stagiaire ("
                        sql &= "NAME,"
                        sql &= "SURNAME,"
                        sql &= "DEPARTMENT,"
                        sql &= "GENDER,"
                        sql &= "TELEPHONE,"
                        sql &= "EMAIL,"
                        sql &= "BEGINNING_DATE,"
                        sql &= "ENDING_DATE"
                        sql &= ") VALUES ("
                        sql &= "@NAME,"
                        sql &= "@SURNAME,"
                        sql &= "@DEPARTMENT,"
                        sql &= "@GENDER,"
                        sql &= "@TELEPHONE,"
                        sql &= "@EMAIL,"
                        sql &= "@BEGINNING_DATE,"
                        sql &= "@ENDING_DATE"
                        sql &= ")"
                    Else
                        sql = "UPDATE Stagiaire SET "
                        sql &= "NAME = @NAME,"
                        sql &= "SURNAME = @SURNAME,"
                        sql &= "DEPARTMENT = @DEPARTMENT,"
                        sql &= "GENDER = @GENDER,"
                        sql &= "TELEPHONE = @TELEPHONE,"
                        sql &= "EMAIL = @EMAIL,"
                        sql &= "BEGINNING_DATE = @BEGINNING_DATE,"
                        sql &= "ENDING_DATE = @ENDING_DATE"
                        sql &= " WHERE STAGIAIRE_ID= " & cls.STAGIAIRE_ID
                    End If
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@NAME", cls.NAME))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@SURNAME", cls.SURNAME))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@DEPARTMENT", cls.DEPARTMENT))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@GENDER", cls.GENDER))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@TELEPHONE", cls.TELEPHONE))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@EMAIL", cls.EMAIL))
                    If cls.BEGINNING_DATE <> Date.MinValue Then
                        Dim DataPar As New SqlClient.SqlParameter("@BEGINNING_DATE", OleDbType.Date)
                        DataPar.Value = cls.BEGINNING_DATE
                        DbCommand.Parameters.Add(DataPar)
                    Else
                        DbCommand.Parameters.Add(New SqlClient.SqlParameter("@BEGINNING_DATE", DBNull.Value))
                    End If
                    If cls.ENDING_DATE <> Date.MinValue Then
                        Dim DataPar As New SqlClient.SqlParameter("@ENDING_DATE", OleDbType.Date)
                        DataPar.Value = cls.ENDING_DATE
                        DbCommand.Parameters.Add(DataPar)
                    Else
                        DbCommand.Parameters.Add(New SqlClient.SqlParameter("@ENDING_DATE", DBNull.Value))
                    End If
                    DbCommand.CommandType = CommandType.Text
                    DbCommand.CommandText = sql
                    DbCommand.ExecuteNonQuery()

                    If cls.STAGIAIRE_ID = 0 Then
                        Dim IdInserito As Integer = 0
                        sql = "select @@identity"
                        DbCommand.CommandText = sql
                        IdInserito = DbCommand.ExecuteScalar()
                        cls.STAGIAIRE_ID = IdInserito
                        Ris = IdInserito
                    Else
                        Ris = cls.STAGIAIRE_ID
                    End If

                    DbCommand.Dispose()

                Catch ex As Exception
                    ManageError(ex)
                End Try
            Else
                Ris = cls.STAGIAIRE_ID
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
            'Dim Sql As String = "UPDATE Stagiaire SET DELETED=True "
            Dim Sql As String = "DELETE FROM Stagiaire"
            Sql &= " Where STAGIAIRE_ID = " & Id

            UpdateCommand.CommandText = Sql
            If Not DbTransaction Is Nothing Then UpdateCommand.Transaction = DbTransaction
            UpdateCommand.ExecuteNonQuery()
            UpdateCommand.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
    End Sub

    ''' <summary>
    '''Delete from DB table Stagiaire. Accept id of object to delete.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(Id As Integer)

        DestroyPermanently(Id)

    End Sub

    ''' <summary>
    '''Delete from DB table Stagiaire. Accept object to delete and optional a List to remove the object from.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(ByRef obj As Stagiaire, Optional ByRef ListaObj As List(Of Stagiaire) = Nothing)

        DestroyPermanently(obj.STAGIAIRE_ID)
        If Not ListaObj Is Nothing Then ListaObj.Remove(obj)

    End Sub

    Public Overloads Function Find(ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Stagiaire)
        Return FindReal(0, OrderBy, Parameter)
    End Function

    Public Overloads Function Find(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Stagiaire)
        Return FindReal(Top, OrderBy, Parameter)
    End Function

    Public Overrides Function Find(ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Stagiaire)
        Return FindReal(0, "", Parameter)
    End Function

    Private Function FindReal(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Stagiaire)
        Dim Ls As New List(Of Stagiaire)
        Try

            Dim sql As String = ""
            sql ="SELECT " & IIf(Top, Top, "") & " STAGIAIRE_ID," & _
	"NAME," & _
	"SURNAME," & _
	"DEPARTMENT," & _
	"GENDER," & _
	"TELEPHONE," & _
	"EMAIL," & _
	"BEGINNING_DATE," & _
	"ENDING_DATE"
sql &=" from Stagiaire" 
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

Public Overrides Function GetAll(Optional OrderByField as string = "", Optional ByVal AddEmptyItem As Boolean = False) as iEnumerable(Of Stagiaire)
Dim Ls As New List(Of Stagiaire)
Try

Dim sql As String = ""
sql = "SELECT STAGIAIRE_ID," &
    "NAME," &
    "SURNAME," &
    "DEPARTMENT," &
    "GENDER," &
    "TELEPHONE," &
    "EMAIL," &
    "BEGINNING_DATE," &
    "ENDING_DATE"
            sql &= " from Stagiaire"
            If OrderByField.Length Then
                sql &= " ORDER BY " & OrderByField
            End If

            Ls = GetData(sql, AddEmptyItem)

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
    Private Function GetData(sql As String, Optional ByVal AddEmptyItem As Boolean = False) As iEnumerable(Of Stagiaire)
        Dim Ls As New List(Of Stagiaire)
        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = sql
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            If AddEmptyItem Then Ls.Add(New Stagiaire() With {.STAGIAIRE_ID = 0, .NAME = "", .SURNAME = "", .DEPARTMENT = "", .GENDER = "", .TELEPHONE = 0, .EMAIL = "", .BEGINNING_DATE = Nothing, .ENDING_DATE = Nothing})
            While myReader.Read
                Dim classe As New Stagiaire
                If Not myReader("STAGIAIRE_ID") Is DBNull.Value Then classe.STAGIAIRE_ID = myReader("STAGIAIRE_ID")
                If Not myReader("NAME") Is DBNull.Value Then classe.NAME = myReader("NAME")
                If Not myReader("SURNAME") Is DBNull.Value Then classe.SURNAME = myReader("SURNAME")
                If Not myReader("DEPARTMENT") Is DBNull.Value Then classe.DEPARTMENT = myReader("DEPARTMENT")
                If Not myReader("GENDER") Is DBNull.Value Then classe.GENDER = myReader("GENDER")
                If Not myReader("TELEPHONE") Is DBNull.Value Then classe.TELEPHONE = myReader("TELEPHONE")
                If Not myReader("EMAIL") Is DBNull.Value Then classe.EMAIL = myReader("EMAIL")
                If Not myReader("BEGINNING_DATE") Is DBNull.Value Then classe.BEGINNING_DATE = myReader("BEGINNING_DATE")
                If Not myReader("ENDING_DATE") Is DBNull.Value Then classe.ENDING_DATE = myReader("ENDING_DATE")
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
'''This class manage persistency on db of Stagiaire object
''' </summary>
''' <remarks>
'''
''' </remarks>
Partial Public Class StagiaireDAO
    Inherits LUNA.LunaBaseClassDAO(Of Stagiaire)

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
    '''Read from DB table Stagiaire
    ''' </summary>
    ''' <returns>
    '''Return an Stagiaire object
    ''' </returns>
    Public Overrides Function Read(Id As Integer) As Stagiaire
        Dim cls As New Stagiaire

        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = "SELECT * FROM Stagiaire where STAGIAIRE_ID = " & Id
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            myReader.Read()
            If myReader.HasRows Then
                cls.STAGIAIRE_ID = myReader("STAGIAIRE_ID")
                If Not myReader("NAME") Is DBNull.Value Then
                    cls.NAME = myReader("NAME")
                End If
                If Not myReader("SURNAME") Is DBNull.Value Then
                    cls.SURNAME = myReader("SURNAME")
                End If
                If Not myReader("DEPARTMENT") Is DBNull.Value Then
                    cls.DEPARTMENT = myReader("DEPARTMENT")
                End If
                If Not myReader("GENDER") Is DBNull.Value Then
                    cls.GENDER = myReader("GENDER")
                End If
                If Not myReader("TELEPHONE") Is DBNull.Value Then
                    cls.TELEPHONE = myReader("TELEPHONE")
                End If
                If Not myReader("EMAIL") Is DBNull.Value Then
                    cls.EMAIL = myReader("EMAIL")
                End If
                If Not myReader("BEGINNING_DATE") Is DBNull.Value Then
                    cls.BEGINNING_DATE = myReader("BEGINNING_DATE")
                End If
                If Not myReader("ENDING_DATE") Is DBNull.Value Then
                    cls.ENDING_DATE = myReader("ENDING_DATE")
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
    '''Save on DB table Stagiaire
    ''' </summary>
    ''' <returns>
    '''Return ID insert in DB
    ''' </returns>
    Public Overrides Function Save(ByRef cls As Stagiaire) As Integer

        Dim Ris As Integer = 0 'in Ris return Insert Id

        If cls.IsValid Then
            If cls.IsChanged Then
                Dim DbCommand As SqlCommand = New SqlCommand()
                Try
                    Dim sql As String
                    DbCommand.Connection = _cn
                    If Not DbTransaction Is Nothing Then DbCommand.Transaction = DbTransaction
                    If cls.STAGIAIRE_ID = 0 Then
                        sql = "INSERT INTO Stagiaire ("
                        sql &= "NAME,"
                        sql &= "SURNAME,"
                        sql &= "DEPARTMENT,"
                        sql &= "GENDER,"
                        sql &= "TELEPHONE,"
                        sql &= "EMAIL,"
                        sql &= "BEGINNING_DATE,"
                        sql &= "ENDING_DATE"
                        sql &= ") VALUES ("
                        sql &= "@NAME,"
                        sql &= "@SURNAME,"
                        sql &= "@DEPARTMENT,"
                        sql &= "@GENDER,"
                        sql &= "@TELEPHONE,"
                        sql &= "@EMAIL,"
                        sql &= "@BEGINNING_DATE,"
                        sql &= "@ENDING_DATE"
                        sql &= ")"
                    Else
                        sql = "UPDATE Stagiaire SET "
                        sql &= "NAME = @NAME,"
                        sql &= "SURNAME = @SURNAME,"
                        sql &= "DEPARTMENT = @DEPARTMENT,"
                        sql &= "GENDER = @GENDER,"
                        sql &= "TELEPHONE = @TELEPHONE,"
                        sql &= "EMAIL = @EMAIL,"
                        sql &= "BEGINNING_DATE = @BEGINNING_DATE,"
                        sql &= "ENDING_DATE = @ENDING_DATE"
                        sql &= " WHERE STAGIAIRE_ID= " & cls.STAGIAIRE_ID
                    End If
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@NAME", cls.NAME))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@SURNAME", cls.SURNAME))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@DEPARTMENT", cls.DEPARTMENT))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@GENDER", cls.GENDER))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@TELEPHONE", cls.TELEPHONE))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@EMAIL", cls.EMAIL))
                    If cls.BEGINNING_DATE <> Date.MinValue Then
                        Dim DataPar As New SqlClient.SqlParameter("@BEGINNING_DATE", OleDbType.Date)
                        DataPar.Value = cls.BEGINNING_DATE
                        DbCommand.Parameters.Add(DataPar)
                    Else
                        DbCommand.Parameters.Add(New SqlClient.SqlParameter("@BEGINNING_DATE", DBNull.Value))
                    End If
                    If cls.ENDING_DATE <> Date.MinValue Then
                        Dim DataPar As New SqlClient.SqlParameter("@ENDING_DATE", OleDbType.Date)
                        DataPar.Value = cls.ENDING_DATE
                        DbCommand.Parameters.Add(DataPar)
                    Else
                        DbCommand.Parameters.Add(New SqlClient.SqlParameter("@ENDING_DATE", DBNull.Value))
                    End If
                    DbCommand.CommandType = CommandType.Text
                    DbCommand.CommandText = sql
                    DbCommand.ExecuteNonQuery()

                    If cls.STAGIAIRE_ID = 0 Then
                        Dim IdInserito As Integer = 0
                        sql = "select @@identity"
                        DbCommand.CommandText = sql
                        IdInserito = DbCommand.ExecuteScalar()
                        cls.STAGIAIRE_ID = IdInserito
                        Ris = IdInserito
                    Else
                        Ris = cls.STAGIAIRE_ID
                    End If

                    DbCommand.Dispose()

                Catch ex As Exception
                    ManageError(ex)
                End Try
            Else
                Ris = cls.STAGIAIRE_ID
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
            'Dim Sql As String = "UPDATE Stagiaire SET DELETED=True "
            Dim Sql As String = "DELETE FROM Stagiaire"
            Sql &= " Where STAGIAIRE_ID = " & Id

            UpdateCommand.CommandText = Sql
            If Not DbTransaction Is Nothing Then UpdateCommand.Transaction = DbTransaction
            UpdateCommand.ExecuteNonQuery()
            UpdateCommand.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
    End Sub

    ''' <summary>
    '''Delete from DB table Stagiaire. Accept id of object to delete.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(Id As Integer)

        DestroyPermanently(Id)

    End Sub

    ''' <summary>
    '''Delete from DB table Stagiaire. Accept object to delete and optional a List to remove the object from.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(ByRef obj As Stagiaire, Optional ByRef ListaObj As List(Of Stagiaire) = Nothing)

        DestroyPermanently(obj.STAGIAIRE_ID)
        If Not ListaObj Is Nothing Then ListaObj.Remove(obj)

    End Sub

    Public Overloads Function Find(ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Stagiaire)
        Return FindReal(0, OrderBy, Parameter)
    End Function

    Public Overloads Function Find(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Stagiaire)
        Return FindReal(Top, OrderBy, Parameter)
    End Function

    Public Overrides Function Find(ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Stagiaire)
        Return FindReal(0, "", Parameter)
    End Function

    Private Function FindReal(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Stagiaire)
        Dim Ls As New List(Of Stagiaire)
        Try

            Dim sql As String = ""
sql ="SELECT " & IIf(Top, Top, "") & " STAGIAIRE_ID," & _
	"NAME," & _
	"SURNAME," & _
	"DEPARTMENT," & _
	"GENDER," & _
	"TELEPHONE," & _
	"EMAIL," & _
	"BEGINNING_DATE," & _
	"ENDING_DATE"
sql &=" from Stagiaire" 
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

Public Overrides Function GetAll(Optional OrderByField as string = "", Optional ByVal AddEmptyItem As Boolean = False) as iEnumerable(Of Stagiaire)
Dim Ls As New List(Of Stagiaire)
Try

            Dim sql As String = ""
            sql = "SELECT STAGIAIRE_ID," &
    "NAME," &
    "SURNAME," &
    "DEPARTMENT," &
    "GENDER," &
    "TELEPHONE," &
    "EMAIL," &
    "BEGINNING_DATE," &
    "ENDING_DATE"
            sql &= " from Stagiaire"
            If OrderByField.Length Then
                sql &= " ORDER BY " & OrderByField
            End If

            Ls = GetData(sql, AddEmptyItem)

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
    Private Function GetData(sql As String, Optional ByVal AddEmptyItem As Boolean = False) As iEnumerable(Of Stagiaire)
        Dim Ls As New List(Of Stagiaire)
        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = sql
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            If AddEmptyItem Then Ls.Add(New Stagiaire() With {.STAGIAIRE_ID = 0, .NAME = "", .SURNAME = "", .DEPARTMENT = "", .GENDER = "", .TELEPHONE = 0, .EMAIL = "", .BEGINNING_DATE = Nothing, .ENDING_DATE = Nothing})
            While myReader.Read
                Dim classe As New Stagiaire
                If Not myReader("STAGIAIRE_ID") Is DBNull.Value Then classe.STAGIAIRE_ID = myReader("STAGIAIRE_ID")
                If Not myReader("NAME") Is DBNull.Value Then classe.NAME = myReader("NAME")
                If Not myReader("SURNAME") Is DBNull.Value Then classe.SURNAME = myReader("SURNAME")
                If Not myReader("DEPARTMENT") Is DBNull.Value Then classe.DEPARTMENT = myReader("DEPARTMENT")
                If Not myReader("GENDER") Is DBNull.Value Then classe.GENDER = myReader("GENDER")
                If Not myReader("TELEPHONE") Is DBNull.Value Then classe.TELEPHONE = myReader("TELEPHONE")
                If Not myReader("EMAIL") Is DBNull.Value Then classe.EMAIL = myReader("EMAIL")
                If Not myReader("BEGINNING_DATE") Is DBNull.Value Then classe.BEGINNING_DATE = myReader("BEGINNING_DATE")
                If Not myReader("ENDING_DATE") Is DBNull.Value Then classe.ENDING_DATE = myReader("ENDING_DATE")
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


