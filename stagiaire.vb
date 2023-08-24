#Region "Author"
'Class created with Luna 3.4.6.11
'Author: Diego Lunadei
'Date: 8/22/2023
#End Region

''' <summary>
'''Entity Class for table Stagiaire
''' </summary>
''' <remarks>
'''Write your custom method and property here
''' </remarks>
Public Class Stagiaire

#Region "Logic Field"

#End Region

#Region "Method"

    Protected Friend Function IsValid() As Boolean
        'RETURN TRUE IF THE OBJECT IS READY FOR SAVE
        'RETURN FALSE IF LOGIC CONTROL FAIL
        'INTERNALISVALID FUNCTION MADE SIMPLE DB CONTROL
        Dim Ris As Boolean = InternalIsValid
        'PUT YOUR LOGIC VALIDATION CODE HERE
        Return Ris
    End Function

#End Region

End Class

''' <summary>
'''DAO Class for table Stagiaire
''' </summary>
''' <remarks>
'''Write your DATABASE custom method here
''' </remarks>
Public Class StagiaireDAO

End Class

