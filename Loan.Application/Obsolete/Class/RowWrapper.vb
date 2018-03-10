Imports System.ComponentModel
Imports System.Collections.Generic

<TypeConverter(GetType(RowWrapper.RowWrapperConverter))> _
Public Class RowWrapper
    Private ReadOnly m_exclude As New List(Of String)()

    Public ReadOnly Property Exclude() As List(Of String)
        Get
            Return m_exclude
        End Get
    End Property

    Private ReadOnly rowView As DataRowView

    Public Sub New(ByVal row As DataRow)
        Dim view As New DataView(row.Table)
        For Each tmp As DataRowView In view
            If tmp.Row.Equals(row) Then
                rowView = tmp
                Exit For
            End If
        Next
    End Sub

    Private Shared Function GetRowView(ByVal component As Object) As DataRowView
        Return DirectCast(component, RowWrapper).rowView
    End Function

    Public Class RowWrapperConverter
        Inherits TypeConverter

        Public Overrides Function GetPropertiesSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overrides Function GetProperties(ByVal context As ITypeDescriptorContext, ByVal value As Object, ByVal attributes As Attribute()) As PropertyDescriptorCollection
            Dim rw As RowWrapper = DirectCast(value, RowWrapper)
            Dim props As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetRowView(value), attributes)
            Dim result As New List(Of PropertyDescriptor)(props.Count)
            For Each prop As PropertyDescriptor In props
                If rw.Exclude.Contains(prop.Name) Then
                    Continue For
                End If
                result.Add(New RowWrapperDescriptor(prop))
            Next
            Return New PropertyDescriptorCollection(result.ToArray())
        End Function
    End Class

    Private Class RowWrapperDescriptor
        Inherits PropertyDescriptor

        ReadOnly innerProp As PropertyDescriptor

        Private Shared Function GetAttribs(ByVal value As AttributeCollection) As Attribute()
            If value Is Nothing Then
                Return Nothing
            End If
            Dim result As Attribute() = New Attribute(value.Count - 1) {}
            value.CopyTo(result, 0)
            Return result
        End Function

        Public Sub New(ByVal innerProperty As PropertyDescriptor)
            MyBase.New(innerProperty.Name, GetAttribs(innerProperty.Attributes))
            Me.innerProp = innerProperty
        End Sub

        Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
            Return innerProp.ShouldSerializeValue(GetRowView(component))
        End Function

        Public Overrides Sub ResetValue(ByVal component As Object)
            innerProp.ResetValue(GetRowView(component))
        End Sub

        Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
            Return innerProp.CanResetValue(GetRowView(component))
        End Function

        Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
            innerProp.SetValue(GetRowView(component), value)
        End Sub

        Public Overrides Function GetValue(ByVal component As Object) As Object
            Return innerProp.GetValue(GetRowView(component))
        End Function

        Public Overrides ReadOnly Property PropertyType() As Type
            Get
                Return innerProp.PropertyType
            End Get
        End Property

        Public Overrides ReadOnly Property ComponentType() As Type
            Get
                Return GetType(RowWrapper)
            End Get
        End Property

        Public Overrides ReadOnly Property IsReadOnly() As Boolean
            Get
                Return innerProp.IsReadOnly
            End Get
        End Property
    End Class
End Class