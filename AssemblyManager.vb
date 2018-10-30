Imports System.Reflection

Public Class AssemblyManager
    Public m_Assembly As Assembly
    Public ReadOnly m_Classes As Type()

    Sub New(ByVal Path As String)
        m_Assembly = Assembly.LoadFrom(Path)
    End Sub

    Public Function GetListOfClasses() As List(Of Type)
        Dim l As New List(Of Type)
        For Each t In m_Assembly.GetTypes()
            If t.IsClass Then
                l.Add(t)
            End If
        Next
        Return l
    End Function
    Public Function GetClassMethods(ByVal ClassName As Type) As MethodInfo()
        Return ClassName.GetMethods()
    End Function
    Public Function GetClassMembers(ByVal ClassName As Type) As FieldInfo()
        Return ClassName.GetFields
    End Function
    Public Function GetClassProperties(ByVal ClassName As Type) As PropertyInfo()
        Return ClassName.GetProperties()
    End Function
    Public Function GetMethodBody(ByVal Method As MethodInfo) As MethodBody
        Return Method.GetMethodBody()
    End Function
    Public Function GetMethodParameters(ByVal Method As MethodInfo) As ParameterInfo()
        Return Method.GetParameters
    End Function

End Class
