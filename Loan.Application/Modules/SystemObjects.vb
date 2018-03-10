Imports System.Collections.Generic
Imports System.ComponentModel

Module SystemObjects
    Public sysuser As String = String.Empty
    Public sysuserlevel As Infrastructure.Enumerations.System.AccessLevels = Infrastructure.Enumerations.System.AccessLevels.None
    Public sysdate As New Date(1985, 9, 27)
    Public admdate As New Date(1983, 7, 30)
    Public memberList As List(Of Business.Objects.MemberList)
    Public loanTypeDetails As List(Of Business.Objects.LoanTypeDetail)
    Public loanControl As Business.Objects.Control
    Public loanRates As DataTable
End Module
