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

Partial Public Class Administration
    Inherits LUNA.LunaBaseClassEntity
    '******IMPORTANT: Don't write your code here. Write your code in the Class object that use this Partial Class.
    '******So you can replace DAOClass and EntityClass without lost your code

    Public Sub New()

    End Sub

#Region "Database Field Map"

    Protected _ADMINISTRATION_Id As Integer = 0

    <XmlElementAttribute("ADMINISTRATION_Id")>
    Public Property ADMINISTRATION_Id() As Integer
        Get
            Return _ADMINISTRATION_Id
        End Get
        Set(ByVal value As Integer)
            If _ADMINISTRATION_Id <> value Then
                IsChanged = True
                _ADMINISTRATION_Id = value
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
#End Region

#Region "Method"
    ''' <summary>
    '''This method read an Administration from DB.
    ''' </summary>
    ''' <returns>
    '''Return 0 if all ok, 1 if error
    ''' </returns>
    Public Overridable Function Read(Id As Integer) As Integer
        'Return 0 if all ok
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New AdministrationDAO
            Dim int As Administration = Mgr.Read(Id)
            _ADMINISTRATION_Id = int.ADMINISTRATION_Id
            _NAME = int.NAME
            _PASSWORD = int.PASSWORD
            _EMAIL = int.EMAIL
            _TELEPHONE = int.TELEPHONE
            Mgr.Dispose()
        Catch ex As Exception
            ManageError(ex)
            Ris = 1
        End Try
        Return Ris
    End Function

    ''' <summary>
    '''This method save an Administration on DB.
    ''' </summary>
    ''' <returns>
    '''Return Id insert in DB if all ok, 0 if error
    ''' </returns>
    Public Overridable Function Save() As Integer
        'Return the id Inserted
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New AdministrationDAO
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
        If _PASSWORD.Length > 50 Then Ris = False
        If _EMAIL.Length > 50 Then Ris = False
        Return Ris
    End Function

#End Region

#Region "Embedded Class"

#End Region

End Class

''' <summary>
'''This class manage persistency on db of Administration object
''' </summary>
''' <remarks>
'''
''' </remarks>
Partial Public Class AdministrationDAO
    Inherits LUNA.LunaBaseClassDAO(Of Administration)

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
    '''Read from DB table Administration
    ''' </summary>
    ''' <returns>
    '''Return an Administration object
    ''' </returns>
    Public Overrides Function Read(Id As Integer) As Administration
        Dim cls As New Administration

        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = "SELECT * FROM Administration where ADMINISTRATION_Id = " & Id
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            myReader.Read()
            If myReader.HasRows Then
                cls.ADMINISTRATION_Id = myReader("ADMINISTRATION_Id")
                If Not myReader("NAME") Is DBNull.Value Then
                    cls.NAME = myReader("NAME")
                End If
                If Not myReader("PASSWORD") Is DBNull.Value Then
                    cls.PASSWORD = myReader("PASSWORD")
                End If
                If Not myReader("EMAIL") Is DBNull.Value Then
                    cls.EMAIL = myReader("EMAIL")
                End If
                If Not myReader("TELEPHONE") Is DBNull.Value Then
                    cls.TELEPHONE = myReader("TELEPHONE")
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
    '''Save on DB table Administration
    ''' </summary>
    ''' <returns>
    '''Return ID insert in DB
    ''' </returns>
    Public Overrides Function Save(ByRef cls As Administration) As Integer

        Dim Ris As Integer = 0 'in Ris return Insert Id

        If cls.IsValid Then
            If cls.IsChanged Then
                Dim DbCommand As SqlCommand = New SqlCommand()
                Try
                    Dim sql As String
                    DbCommand.Connection = _cn
                    If Not DbTransaction Is Nothing Then DbCommand.Transaction = DbTransaction
                    If cls.ADMINISTRATION_Id = 0 Then
                        sql = "INSERT INTO Administration ("
                        sql &= "NAME,"
                        sql &= "PASSWORD,"
                        sql &= "EMAIL,"
                        sql &= "TELEPHONE"
                        sql &= ") VALUES ("
                        sql &= "@NAME,"
                        sql &= "@PASSWORD,"
                        sql &= "@EMAIL,"
                        sql &= "@TELEPHONE"
                        sql &= ")"
                    Else
                        sql = "UPDATE Administration SET "
                        sql &= "NAME = @NAME,"
                        sql &= "PASSWORD = @PASSWORD,"
                        sql &= "EMAIL = @EMAIL,"
                        sql &= "TELEPHONE = @TELEPHONE"
                        sql &= " WHERE ADMINISTRATION_Id= " & cls.ADMINISTRATION_Id
                    End If
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@NAME", cls.NAME))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@PASSWORD", cls.PASSWORD))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@EMAIL", cls.EMAIL))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@TELEPHONE", cls.TELEPHONE))
                    DbCommand.CommandType = CommandType.Text
                    DbCommand.CommandText = sql
                    DbCommand.ExecuteNonQuery()

                    If cls.ADMINISTRATION_Id = 0 Then
                        Dim IdInserito As Integer = 0
                        sql = "select @@identity"
                        DbCommand.CommandText = sql
                        IdInserito = DbCommand.ExecuteScalar()
                        cls.ADMINISTRATION_Id = IdInserito
                        Ris = IdInserito
                    Else
                        Ris = cls.ADMINISTRATION_Id
                    End If

                    DbCommand.Dispose()

                Catch ex As Exception
                    ManageError(ex)
                End Try
            Else
                Ris = cls.ADMINISTRATION_Id
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
            'Dim Sql As String = "UPDATE Administration SET DELETED=True "
            Dim Sql As String = "DELETE FROM Administration"
            Sql &= " Where ADMINISTRATION_Id = " & Id

            UpdateCommand.CommandText = Sql
            If Not DbTransaction Is Nothing Then UpdateCommand.Transaction = DbTransaction
            UpdateCommand.ExecuteNonQuery()
            UpdateCommand.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
    End Sub

    ''' <summary>
    '''Delete from DB table Administration. Accept id of object to delete.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(Id As Integer)

        DestroyPermanently(Id)

    End Sub

    ''' <summary>
    '''Delete from DB table Administration. Accept object to delete and optional a List to remove the object from.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(ByRef obj As Administration, Optional ByRef ListaObj As List(Of Administration) = Nothing)

        DestroyPermanently(obj.ADMINISTRATION_Id)
        If Not ListaObj Is Nothing Then ListaObj.Remove(obj)

    End Sub

    Public Overloads Function Find(ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Administration)
        Return FindReal(0, OrderBy, Parameter)
    End Function

    Public Overloads Function Find(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Administration)
        Return FindReal(Top, OrderBy, Parameter)
    End Function

    Public Overrides Function Find(ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Administration)
        Return FindReal(0, "", Parameter)
    End Function

    Private Function FindReal(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Administration)
        Dim Ls As New List(Of Administration)
        Try

            Dim sql As String = ""
sql ="SELECT " & IIf(Top, Top, "") & " ADMINISTRATION_Id," & _
	"NAME," & _
	"PASSWORD," & _
	"EMAIL," & _
	"TELEPHONE"
sql &=" from Administration" 
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

Public Overrides Function GetAll(Optional OrderByField as string = "", Optional ByVal AddEmptyItem As Boolean = False) as iEnumerable(Of Administration)
Dim Ls As New List(Of Administration)
Try

            Dim sql As String = ""
            sql = "SELECT ADMINISTRATION_Id," &
    "NAME," &
    "PASSWORD," &
    "EMAIL," &
    "TELEPHONE"
            sql &= " from Administration"
            If OrderByField.Length Then
                sql &= " ORDER BY " & OrderByField
            End If

            Ls = GetData(sql, AddEmptyItem)

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
    Private Function GetData(sql As String, Optional ByVal AddEmptyItem As Boolean = False) As iEnumerable(Of Administration)
        Dim Ls As New List(Of Administration)
        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = sql
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            If AddEmptyItem Then Ls.Add(New Administration() With {.ADMINISTRATION_Id = 0, .NAME = "", .PASSWORD = "", .EMAIL = "", .TELEPHONE = 0})
            While myReader.Read
                Dim classe As New Administration
                If Not myReader("ADMINISTRATION_Id") Is DBNull.Value Then classe.ADMINISTRATION_Id = myReader("ADMINISTRATION_Id")
                If Not myReader("NAME") Is DBNull.Value Then classe.NAME = myReader("NAME")
                If Not myReader("PASSWORD") Is DBNull.Value Then classe.PASSWORD = myReader("PASSWORD")
                If Not myReader("EMAIL") Is DBNull.Value Then classe.EMAIL = myReader("EMAIL")
                If Not myReader("TELEPHONE") Is DBNull.Value Then classe.TELEPHONE = myReader("TELEPHONE")
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
'''This class manage persistency on db of Administration object
''' </summary>
''' <remarks>
'''
''' </remarks>
Partial Public Class AdministrationDAO
    Inherits LUNA.LunaBaseClassDAO(Of Administration)

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
    '''Read from DB table Administration
    ''' </summary>
    ''' <returns>
    '''Return an Administration object
    ''' </returns>
    Public Overrides Function Read(Id As Integer) As Administration
        Dim cls As New Administration

        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = "SELECT * FROM Administration where ADMINISTRATION_Id = " & Id
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            myReader.Read()
            If myReader.HasRows Then
                cls.ADMINISTRATION_Id = myReader("ADMINISTRATION_Id")
                If Not myReader("NAME") Is DBNull.Value Then
                    cls.NAME = myReader("NAME")
                End If
                If Not myReader("PASSWORD") Is DBNull.Value Then
                    cls.PASSWORD = myReader("PASSWORD")
                End If
                If Not myReader("EMAIL") Is DBNull.Value Then
                    cls.EMAIL = myReader("EMAIL")
                End If
                If Not myReader("TELEPHONE") Is DBNull.Value Then
                    cls.TELEPHONE = myReader("TELEPHONE")
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
    '''Save on DB table Administration
    ''' </summary>
    ''' <returns>
    '''Return ID insert in DB
    ''' </returns>
    Public Overrides Function Save(ByRef cls As Administration) As Integer

        Dim Ris As Integer = 0 'in Ris return Insert Id

        If cls.IsValid Then
            If cls.IsChanged Then
                Dim DbCommand As SqlCommand = New SqlCommand()
                Try
                    Dim sql As String
                    DbCommand.Connection = _cn
                    If Not DbTransaction Is Nothing Then DbCommand.Transaction = DbTransaction
                    If cls.ADMINISTRATION_Id = 0 Then
                        sql = "INSERT INTO Administration ("
                        sql &= "NAME,"
                        sql &= "PASSWORD,"
                        sql &= "EMAIL,"
                        sql &= "TELEPHONE"
                        sql &= ") VALUES ("
                        sql &= "@NAME,"
                        sql &= "@PASSWORD,"
                        sql &= "@EMAIL,"
                        sql &= "@TELEPHONE"
                        sql &= ")"
                    Else
                        sql = "UPDATE Administration SET "
                        sql &= "NAME = @NAME,"
                        sql &= "PASSWORD = @PASSWORD,"
                        sql &= "EMAIL = @EMAIL,"
                        sql &= "TELEPHONE = @TELEPHONE"
                        sql &= " WHERE ADMINISTRATION_Id= " & cls.ADMINISTRATION_Id
                    End If
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@NAME", cls.NAME))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@PASSWORD", cls.PASSWORD))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@EMAIL", cls.EMAIL))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@TELEPHONE", cls.TELEPHONE))
                    DbCommand.CommandType = CommandType.Text
                    DbCommand.CommandText = sql
                    DbCommand.ExecuteNonQuery()

                    If cls.ADMINISTRATION_Id = 0 Then
                        Dim IdInserito As Integer = 0
                        sql = "select @@identity"
                        DbCommand.CommandText = sql
                        IdInserito = DbCommand.ExecuteScalar()
                        cls.ADMINISTRATION_Id = IdInserito
                        Ris = IdInserito
                    Else
                        Ris = cls.ADMINISTRATION_Id
                    End If

                    DbCommand.Dispose()

                Catch ex As Exception
                    ManageError(ex)
                End Try
            Else
                Ris = cls.ADMINISTRATION_Id
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
            'Dim Sql As String = "UPDATE Administration SET DELETED=True "
            Dim Sql As String = "DELETE FROM Administration"
            Sql &= " Where ADMINISTRATION_Id = " & Id

            UpdateCommand.CommandText = Sql
            If Not DbTransaction Is Nothing Then UpdateCommand.Transaction = DbTransaction
            UpdateCommand.ExecuteNonQuery()
            UpdateCommand.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
    End Sub

    ''' <summary>
    '''Delete from DB table Administration. Accept id of object to delete.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(Id As Integer)

        DestroyPermanently(Id)

    End Sub

    ''' <summary>
    '''Delete from DB table Administration. Accept object to delete and optional a List to remove the object from.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(ByRef obj As Administration, Optional ByRef ListaObj As List(Of Administration) = Nothing)

        DestroyPermanently(obj.ADMINISTRATION_Id)
        If Not ListaObj Is Nothing Then ListaObj.Remove(obj)

    End Sub

    Public Overloads Function Find(ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Administration)
        Return FindReal(0, OrderBy, Parameter)
    End Function

    Public Overloads Function Find(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Administration)
        Return FindReal(Top, OrderBy, Parameter)
    End Function

    Public Overrides Function Find(ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Administration)
        Return FindReal(0, "", Parameter)
    End Function

    Private Function FindReal(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Administration)
        Dim Ls As New List(Of Administration)
        Try

            Dim sql As String = ""
sql ="SELECT " & IIf(Top, Top, "") & " ADMINISTRATION_Id," & _
	"NAME," & _
	"PASSWORD," & _
	"EMAIL," & _
	"TELEPHONE"
sql &=" from Administration" 
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

Public Overrides Function GetAll(Optional OrderByField as string = "", Optional ByVal AddEmptyItem As Boolean = False) as iEnumerable(Of Administration)
Dim Ls As New List(Of Administration)
Try

            Dim sql As String = ""
            sql = "SELECT ADMINISTRATION_Id," &
    "NAME," &
    "PASSWORD," &
    "EMAIL," &
    "TELEPHONE"
            sql &= " from Administration"
            If OrderByField.Length Then
                sql &= " ORDER BY " & OrderByField
            End If

            Ls = GetData(sql, AddEmptyItem)

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
    Private Function GetData(sql As String, Optional ByVal AddEmptyItem As Boolean = False) As iEnumerable(Of Administration)
        Dim Ls As New List(Of Administration)
        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = sql
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            If AddEmptyItem Then Ls.Add(New Administration() With {.ADMINISTRATION_Id = 0, .NAME = "", .PASSWORD = "", .EMAIL = "", .TELEPHONE = 0})
            While myReader.Read
                Dim classe As New Administration
                If Not myReader("ADMINISTRATION_Id") Is DBNull.Value Then classe.ADMINISTRATION_Id = myReader("ADMINISTRATION_Id")
                If Not myReader("NAME") Is DBNull.Value Then classe.NAME = myReader("NAME")
                If Not myReader("PASSWORD") Is DBNull.Value Then classe.PASSWORD = myReader("PASSWORD")
                If Not myReader("EMAIL") Is DBNull.Value Then classe.EMAIL = myReader("EMAIL")
                If Not myReader("TELEPHONE") Is DBNull.Value Then classe.TELEPHONE = myReader("TELEPHONE")
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
'''This class manage persistency on db of Administration object
''' </summary>
''' <remarks>
'''
''' </remarks>
Partial Public Class AdministrationDAO
    Inherits LUNA.LunaBaseClassDAO(Of Administration)

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
    '''Read from DB table Administration
    ''' </summary>
    ''' <returns>
    '''Return an Administration object
    ''' </returns>
    Public Overrides Function Read(Id As Integer) As Administration
        Dim cls As New Administration

        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = "SELECT * FROM Administration where ADMINISTRATION_Id = " & Id
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            myReader.Read()
            If myReader.HasRows Then
                cls.ADMINISTRATION_Id = myReader("ADMINISTRATION_Id")
                If Not myReader("NAME") Is DBNull.Value Then
                    cls.NAME = myReader("NAME")
                End If
                If Not myReader("PASSWORD") Is DBNull.Value Then
                    cls.PASSWORD = myReader("PASSWORD")
                End If
                If Not myReader("EMAIL") Is DBNull.Value Then
                    cls.EMAIL = myReader("EMAIL")
                End If
                If Not myReader("TELEPHONE") Is DBNull.Value Then
                    cls.TELEPHONE = myReader("TELEPHONE")
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
    '''Save on DB table Administration
    ''' </summary>
    ''' <returns>
    '''Return ID insert in DB
    ''' </returns>
    Public Overrides Function Save(ByRef cls As Administration) As Integer

        Dim Ris As Integer = 0 'in Ris return Insert Id

        If cls.IsValid Then
            If cls.IsChanged Then
                Dim DbCommand As SqlCommand = New SqlCommand()
                Try
                    Dim sql As String
                    DbCommand.Connection = _cn
                    If Not DbTransaction Is Nothing Then DbCommand.Transaction = DbTransaction
                    If cls.ADMINISTRATION_Id = 0 Then
                        sql = "INSERT INTO Administration ("
                        sql &= "NAME,"
                        sql &= "PASSWORD,"
                        sql &= "EMAIL,"
                        sql &= "TELEPHONE"
                        sql &= ") VALUES ("
                        sql &= "@NAME,"
                        sql &= "@PASSWORD,"
                        sql &= "@EMAIL,"
                        sql &= "@TELEPHONE"
                        sql &= ")"
                    Else
                        sql = "UPDATE Administration SET "
                        sql &= "NAME = @NAME,"
                        sql &= "PASSWORD = @PASSWORD,"
                        sql &= "EMAIL = @EMAIL,"
                        sql &= "TELEPHONE = @TELEPHONE"
                        sql &= " WHERE ADMINISTRATION_Id= " & cls.ADMINISTRATION_Id
                    End If
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@NAME", cls.NAME))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@PASSWORD", cls.PASSWORD))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@EMAIL", cls.EMAIL))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@TELEPHONE", cls.TELEPHONE))
                    DbCommand.CommandType = CommandType.Text
                    DbCommand.CommandText = sql
                    DbCommand.ExecuteNonQuery()

                    If cls.ADMINISTRATION_Id = 0 Then
                        Dim IdInserito As Integer = 0
                        sql = "select @@identity"
                        DbCommand.CommandText = sql
                        IdInserito = DbCommand.ExecuteScalar()
                        cls.ADMINISTRATION_Id = IdInserito
                        Ris = IdInserito
                    Else
                        Ris = cls.ADMINISTRATION_Id
                    End If

                    DbCommand.Dispose()

                Catch ex As Exception
                    ManageError(ex)
                End Try
            Else
                Ris = cls.ADMINISTRATION_Id
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
            'Dim Sql As String = "UPDATE Administration SET DELETED=True "
            Dim Sql As String = "DELETE FROM Administration"
            Sql &= " Where ADMINISTRATION_Id = " & Id

            UpdateCommand.CommandText = Sql
            If Not DbTransaction Is Nothing Then UpdateCommand.Transaction = DbTransaction
            UpdateCommand.ExecuteNonQuery()
            UpdateCommand.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
    End Sub

    ''' <summary>
    '''Delete from DB table Administration. Accept id of object to delete.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(Id As Integer)

        DestroyPermanently(Id)

    End Sub

    ''' <summary>
    '''Delete from DB table Administration. Accept object to delete and optional a List to remove the object from.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(ByRef obj As Administration, Optional ByRef ListaObj As List(Of Administration) = Nothing)

        DestroyPermanently(obj.ADMINISTRATION_Id)
        If Not ListaObj Is Nothing Then ListaObj.Remove(obj)

    End Sub

    Public Overloads Function Find(ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Administration)
        Return FindReal(0, OrderBy, Parameter)
    End Function

    Public Overloads Function Find(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Administration)
        Return FindReal(Top, OrderBy, Parameter)
    End Function

    Public Overrides Function Find(ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Administration)
        Return FindReal(0, "", Parameter)
    End Function

    Private Function FindReal(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As iEnumerable(Of Administration)
        Dim Ls As New List(Of Administration)
        Try

            Dim sql As String = ""
sql ="SELECT " & IIf(Top, Top, "") & " ADMINISTRATION_Id," & _
	"NAME," & _
	"PASSWORD," & _
	"EMAIL," & _
	"TELEPHONE"
sql &=" from Administration" 
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

Public Overrides Function GetAll(Optional OrderByField as string = "", Optional ByVal AddEmptyItem As Boolean = False) as iEnumerable(Of Administration)
Dim Ls As New List(Of Administration)
Try

            Dim sql As String = ""
            sql = "SELECT ADMINISTRATION_Id," &
    "NAME," &
    "PASSWORD," &
    "EMAIL," &
    "TELEPHONE"
            sql &= " from Administration"
            If OrderByField.Length Then
                sql &= " ORDER BY " & OrderByField
            End If

            Ls = GetData(sql, AddEmptyItem)

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
    Private Function GetData(sql As String, Optional ByVal AddEmptyItem As Boolean = False) As iEnumerable(Of Administration)
        Dim Ls As New List(Of Administration)
        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = sql
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            If AddEmptyItem Then Ls.Add(New Administration() With {.ADMINISTRATION_Id = 0, .NAME = "", .PASSWORD = "", .EMAIL = "", .TELEPHONE = 0})
            While myReader.Read
                Dim classe As New Administration
                If Not myReader("ADMINISTRATION_Id") Is DBNull.Value Then classe.ADMINISTRATION_Id = myReader("ADMINISTRATION_Id")
                If Not myReader("NAME") Is DBNull.Value Then classe.NAME = myReader("NAME")
                If Not myReader("PASSWORD") Is DBNull.Value Then classe.PASSWORD = myReader("PASSWORD")
                If Not myReader("EMAIL") Is DBNull.Value Then classe.EMAIL = myReader("EMAIL")
                If Not myReader("TELEPHONE") Is DBNull.Value Then classe.TELEPHONE = myReader("TELEPHONE")
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


