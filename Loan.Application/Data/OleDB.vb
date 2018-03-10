Imports System.Data.OleDb

Namespace Data
    Public Class OleDB
        Public Shared Function ImportDbf(ByVal fullPath As String) As DataTable
            Dim file As String = Mid(fullPath, InStrRev(fullPath, "\") + 1, Len(fullPath) - InStrRev(fullPath, "\"))
            Dim path As String = Mid(fullPath, 1, InStrRev(fullPath, "\") - 1)
            Dim dt As New DataTable

            Using oleConnection As New OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=dBASE IV;User ID=Admin;Password=;", path))
                Using oleCommand As New OleDbCommand(String.Format("select * from {0}", file), oleConnection)
                    Using oleDataAdapter As New OleDbDataAdapter(oleCommand)
                        oleDataAdapter.Fill(dt)
                    End Using
                End Using
            End Using

            Return dt
        End Function
    End Class
End Namespace