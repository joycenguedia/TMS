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

Partial Public Class Course
    Inherits LUNA.LunaBaseClassEntity
    '******IMPORTANT: Don't write your code here. Write your code in the Class object that use this Partial Class.
    '******So you can replace DAOClass and EntityClass without lost your code

    Public Sub New()

    End Sub

#Region "Database Field Map"

    Protected _COURSE_ID As Integer = 0

    <XmlElementAttribute("COURSE_ID")>
    Public Property COURSE_ID() As Integer
        Get
            Return _COURSE_ID
        End Get
        Set(ByVal value As Integer)
            If _COURSE_ID <> value Then
                IsChanged = True
                _COURSE_ID = value
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

    Protected _DESCRIPTION As String = ""

    <XmlElementAttribute("DESCRIPTION")>
    Public Property DESCRIPTION() As String
        Get
            Return _DESCRIPTION
        End Get
        Set(ByVal value As String)
            If _DESCRIPTION <> value Then
                IsChanged = True
                _DESCRIPTION = value
            End If
        End Set
    End Property

    Protected _DURATION As Integer = 0

    <XmlElementAttribute("DURATION")>
    Public Property DURATION() As Integer
        Get
            Return _DURATION
        End Get
        Set(ByVal value As Integer)
            If _DURATION <> value Then
                IsChanged = True
                _DURATION = value
            End If
        End Set
    End Property
#End Region

#Region "Method"
    ''' <summary>
    '''This method read an Course from DB.
    ''' </summary>
    ''' <returns>
    '''Return 0 if all ok, 1 if error
    ''' </returns>
    Public Overridable Function Read(Id As Integer) As Integer
        'Return 0 if all ok
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New CourseDAO
            Dim int As Course = Mgr.Read(Id)
            _COURSE_ID = int.COURSE_ID
            _NAME = int.NAME
            _DESCRIPTION = int.DESCRIPTION
            _DURATION = int.DURATION
            Mgr.Dispose()
        Catch ex As Exception
            ManageError(ex)
            Ris = 1
        End Try
        Return Ris
    End Function

    ''' <summary>
    '''This method save an Course on DB.
    ''' </summary>
    ''' <returns>
    '''Return Id insert in DB if all ok, 0 if error
    ''' </returns>
    Public Overridable Function Save() As Integer
        'Return the id Inserted
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New CourseDAO
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
        If _DESCRIPTION.Length > 50 Then Ris = False
        Return Ris
    End Function

#End Region

#Region "Embedded Class"

#End Region

End Class

''' <summary>
'''This class manage persistency on db of Course object
''' </summary>
''' <remarks>
'''
''' </remarks>
Partial Public Class CourseDAO
    Inherits LUNA.LunaBaseClassDAO(Of Course)

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
    '''Read from DB table Course
    ''' </summary>
    ''' <returns>
    '''Return an Course object
    ''' </returns>
    Public Overrides Function Read(Id As Integer) As Course
        Dim cls As New Course

        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = "SELECT * FROM Course where COURSE_ID = " & Id
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            myReader.Read()
            If myReader.HasRows Then
                cls.COURSE_ID = myReader("COURSE_ID")
                If Not myReader("NAME") Is DBNull.Value Then
                    cls.NAME = myReader("NAME")
                End If
                If Not myReader("DESCRIPTION") Is DBNull.Value Then
                    cls.DESCRIPTION = myReader("DESCRIPTION")
                End If
                If Not myReader("DURATION") Is DBNull.Value Then
                    cls.DURATION = myReader("DURATION")
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
    '''Save on DB table Course
    ''' </summary>
    ''' <returns>
    '''Return ID insert in DB
    ''' </returns>
    Public Overrides Function Save(ByRef cls As Course) As Integer

        Dim Ris As Integer = 0 'in Ris return Insert Id

        If cls.IsValid Then
            If cls.IsChanged Then
                Dim DbCommand As SqlCommand = New SqlCommand()
                Try
                    Dim sql As String
                    DbCommand.Connection = _cn
                    If Not DbTransaction Is Nothing Then DbCommand.Transaction = DbTransaction
                    If cls.COURSE_ID = 0 Then
                        sql = "INSERT INTO Course ("
                        sql &= "NAME,"
                        sql &= "DESCRIPTION,"
                        sql &= "DURATION"
                        sql &= ") VALUES ("
                        sql &= "@NAME,"
                        sql &= "@DESCRIPTION,"
                        sql &= "@DURATION"
                        sql &= ")"
                    Else
                        sql = "UPDATE Course SET "
                        sql &= "NAME = @NAME,"
                        sql &= "DESCRIPTION = @DESCRIPTION,"
                        sql &= "DURATION = @DURATION"
                        sql &= " WHERE COURSE_ID= " & cls.COURSE_ID
                    End If
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@NAME", cls.NAME))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@DESCRIPTION", cls.DESCRIPTION))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@DURATION", cls.DURATION))
                    DbCommand.CommandType = CommandType.Text
                    DbCommand.CommandText = sql
                    DbCommand.ExecuteNonQuery()

                    If cls.COURSE_ID = 0 Then
                        Dim IdInserito As Integer = 0
                        sql = "select @@identity"
                        DbCommand.CommandText = sql
                        IdInserito = DbCommand.ExecuteScalar()
                        cls.COURSE_ID = IdInserito
                        Ris = IdInserito
                    Else
                        Ris = cls.COURSE_ID
                    End If

                    DbCommand.Dispose()

                Catch ex As Exception
                    ManageError(ex)
                End Try
            Else
                Ris = cls.COURSE_ID
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
            'Dim Sql As String = "UPDATE Course SET DELETED=True "
            Dim Sql As String = "DELETE FROM Course"
            Sql &= " Where COURSE_ID = " & Id

            UpdateCommand.CommandText = Sql
            If Not DbTransaction Is Nothing Then UpdateCommand.Transaction = DbTransaction
            UpdateCommand.ExecuteNonQuery()
            UpdateCommand.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
    End Sub

    ''' <summary>
    '''Delete from DB table Course. Accept id of object to delete.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(Id As Integer)

        DestroyPermanently(Id)

    End Sub

    ''' <summary>
    '''Delete from DB table Course. Accept object to delete and optional a List to remove the object from.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(ByRef obj As Course, Optional ByRef ListaObj As List(Of Course) = Nothing)

        DestroyPermanently(obj.COURSE_ID)
        If Not ListaObj Is Nothing Then ListaObj.Remove(obj)

    End Sub

    Public Overloads Function Find(ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Course)
        Return FindReal(0, OrderBy, Parameter)
    End Function

    Public Overloads Function Find(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Course)
        Return FindReal(Top, OrderBy, Parameter)
    End Function

    Public Overrides Function Find(ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Course)
        Return FindReal(0, "", Parameter)
    End Function

    Private Function FindReal(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Course)
        Dim Ls As New List(Of Course)
        Try

            Dim sql As String = ""
sql ="SELECT " & IIf(Top, Top, "") & " COURSE_ID," & _
	"NAME," & _
	"DESCRIPTION," & _
	"DURATION"
sql &=" from Course" 
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

Public Overrides Function GetAll(Optional OrderByField as string = "", Optional ByVal AddEmptyItem As Boolean = False) as iEnumerable(Of Course)
Dim Ls As New List(Of Course)
Try

            Dim sql As String = ""
            sql = "SELECT COURSE_ID," &
    "NAME," &
    "DESCRIPTION," &
    "DURATION"
            sql &= " from Course"
            If OrderByField.Length Then
                sql &= " ORDER BY " & OrderByField
            End If

            Ls = GetData(sql, AddEmptyItem)

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
    Private Function GetData(sql As String, Optional ByVal AddEmptyItem As Boolean = False) As iEnumerable(Of Course)
        Dim Ls As New List(Of Course)
        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = sql
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            If AddEmptyItem Then Ls.Add(New Course() With {.COURSE_ID = 0, .NAME = "", .DESCRIPTION = "", .DURATION = 0})
            While myReader.Read
                Dim classe As New Course
                If Not myReader("COURSE_ID") Is DBNull.Value Then classe.COURSE_ID = myReader("COURSE_ID")
                If Not myReader("NAME") Is DBNull.Value Then classe.NAME = myReader("NAME")
                If Not myReader("DESCRIPTION") Is DBNull.Value Then classe.DESCRIPTION = myReader("DESCRIPTION")
                If Not myReader("DURATION") Is DBNull.Value Then classe.DURATION = myReader("DURATION")
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
'''This class manage persistency on db of Course object
''' </summary>
''' <remarks>
'''
''' </remarks>
Partial Public Class CourseDAO
    Inherits LUNA.LunaBaseClassDAO(Of Course)

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
    '''Read from DB table Course
    ''' </summary>
    ''' <returns>
    '''Return an Course object
    ''' </returns>
    Public Overrides Function Read(Id As Integer) As Course
        Dim cls As New Course

        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = "SELECT * FROM Course where COURSE_ID = " & Id
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            myReader.Read()
            If myReader.HasRows Then
                cls.COURSE_ID = myReader("COURSE_ID")
                If Not myReader("NAME") Is DBNull.Value Then
                    cls.NAME = myReader("NAME")
                End If
                If Not myReader("DESCRIPTION") Is DBNull.Value Then
                    cls.DESCRIPTION = myReader("DESCRIPTION")
                End If
                If Not myReader("DURATION") Is DBNull.Value Then
                    cls.DURATION = myReader("DURATION")
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
    '''Save on DB table Course
    ''' </summary>
    ''' <returns>
    '''Return ID insert in DB
    ''' </returns>
    Public Overrides Function Save(ByRef cls As Course) As Integer

        Dim Ris As Integer = 0 'in Ris return Insert Id

        If cls.IsValid Then
            If cls.IsChanged Then
                Dim DbCommand As SqlCommand = New SqlCommand()
                Try
                    Dim sql As String
                    DbCommand.Connection = _cn
                    If Not DbTransaction Is Nothing Then DbCommand.Transaction = DbTransaction
                    If cls.COURSE_ID = 0 Then
                        sql = "INSERT INTO Course ("
                        sql &= "NAME,"
                        sql &= "DESCRIPTION,"
                        sql &= "DURATION"
                        sql &= ") VALUES ("
                        sql &= "@NAME,"
                        sql &= "@DESCRIPTION,"
                        sql &= "@DURATION"
                        sql &= ")"
                    Else
                        sql = "UPDATE Course SET "
                        sql &= "NAME = @NAME,"
                        sql &= "DESCRIPTION = @DESCRIPTION,"
                        sql &= "DURATION = @DURATION"
                        sql &= " WHERE COURSE_ID= " & cls.COURSE_ID
                    End If
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@NAME", cls.NAME))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@DESCRIPTION", cls.DESCRIPTION))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@DURATION", cls.DURATION))
                    DbCommand.CommandType = CommandType.Text
                    DbCommand.CommandText = sql
                    DbCommand.ExecuteNonQuery()

                    If cls.COURSE_ID = 0 Then
                        Dim IdInserito As Integer = 0
                        sql = "select @@identity"
                        DbCommand.CommandText = sql
                        IdInserito = DbCommand.ExecuteScalar()
                        cls.COURSE_ID = IdInserito
                        Ris = IdInserito
                    Else
                        Ris = cls.COURSE_ID
                    End If

                    DbCommand.Dispose()

                Catch ex As Exception
                    ManageError(ex)
                End Try
            Else
                Ris = cls.COURSE_ID
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
            'Dim Sql As String = "UPDATE Course SET DELETED=True "
            Dim Sql As String = "DELETE FROM Course"
            Sql &= " Where COURSE_ID = " & Id

            UpdateCommand.CommandText = Sql
            If Not DbTransaction Is Nothing Then UpdateCommand.Transaction = DbTransaction
            UpdateCommand.ExecuteNonQuery()
            UpdateCommand.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
    End Sub

    ''' <summary>
    '''Delete from DB table Course. Accept id of object to delete.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(Id As Integer)

        DestroyPermanently(Id)

    End Sub

    ''' <summary>
    '''Delete from DB table Course. Accept object to delete and optional a List to remove the object from.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(ByRef obj As Course, Optional ByRef ListaObj As List(Of Course) = Nothing)

        DestroyPermanently(obj.COURSE_ID)
        If Not ListaObj Is Nothing Then ListaObj.Remove(obj)

    End Sub

    Public Overloads Function Find(ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Course)
        Return FindReal(0, OrderBy, Parameter)
    End Function

    Public Overloads Function Find(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Course)
        Return FindReal(Top, OrderBy, Parameter)
    End Function

    Public Overrides Function Find(ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Course)
        Return FindReal(0, "", Parameter)
    End Function

    Private Function FindReal(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Course)
        Dim Ls As New List(Of Course)
        Try

            Dim sql As String = ""
sql ="SELECT " & IIf(Top, Top, "") & " COURSE_ID," & _
	"NAME," & _
	"DESCRIPTION," & _
	"DURATION"
sql &=" from Course" 
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

Public Overrides Function GetAll(Optional OrderByField as string = "", Optional ByVal AddEmptyItem As Boolean = False) as iEnumerable(Of Course)
Dim Ls As New List(Of Course)
Try

            Dim sql As String = ""
            sql = "SELECT COURSE_ID," &
    "NAME," &
    "DESCRIPTION," &
    "DURATION"
            sql &= " from Course"
            If OrderByField.Length Then
                sql &= " ORDER BY " & OrderByField
            End If

            Ls = GetData(sql, AddEmptyItem)

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
    Private Function GetData(sql As String, Optional ByVal AddEmptyItem As Boolean = False) As iEnumerable(Of Course)
        Dim Ls As New List(Of Course)
        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = sql
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            If AddEmptyItem Then Ls.Add(New Course() With {.COURSE_ID = 0, .NAME = "", .DESCRIPTION = "", .DURATION = 0})
            While myReader.Read
                Dim classe As New Course
                If Not myReader("COURSE_ID") Is DBNull.Value Then classe.COURSE_ID = myReader("COURSE_ID")
                If Not myReader("NAME") Is DBNull.Value Then classe.NAME = myReader("NAME")
                If Not myReader("DESCRIPTION") Is DBNull.Value Then classe.DESCRIPTION = myReader("DESCRIPTION")
                If Not myReader("DURATION") Is DBNull.Value Then classe.DURATION = myReader("DURATION")
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


