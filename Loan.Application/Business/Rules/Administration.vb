Imports System.Data.SqlClient
Imports Loan.Application.Infrastructure.Enumerations.System

Namespace Business.Rules
    Public Class Administration
        Public Shared Function IsUserAuthenticated(ByVal userName As String, ByVal userPass As String, Optional ByVal override As Infrastructure.Enumerations.System.AccessLevels = Infrastructure.Enumerations.System.AccessLevels.None) As AuthenticationResponses
            Dim user As Business.Objects.User
            userPass = Loan.Application.Infrastructure.Data.Database.GetEncryptedText(userPass)
            user = Common.GetObject(Of Business.Objects.User)(userName, userPass)

            If sysuser = String.Empty Then
                If user IsNot Nothing Then
                    SetUserInfo(user)
                    Return AuthenticationResponses.Granted
                Else
                    Return AuthenticationResponses.InvalidCredentials
                End If
            ElseIf sysuserlevel < override Then
                If user IsNot Nothing Then
                    If user.LEVEL < override Then
                        Return AuthenticationResponses.Denied
                    Else
                        Return AuthenticationResponses.Overriden
                    End If
                Else
                    Return AuthenticationResponses.InvalidCredentials
                End If
            ElseIf sysuserlevel >= override Then
                Return AuthenticationResponses.Granted
            Else
                Return AuthenticationResponses.Denied
            End If
        End Function

        Private Shared Sub SetUserInfo(ByVal user As Business.Objects.User)
            If sysuser = String.Empty Then
                sysuser = user.USERNAME
                sysuserlevel = user.LEVEL
            End If
        End Sub

        Public Shared Function IsAdminDateUpdated(ByVal adminDate As Date) As Boolean
            Dim db As New Data.Database()

            Try
                db.ExecuteNonQuerySp(Infrastructure.Enumerations.Sql.NonQuery.Do_Admin_AdminDate, adminDate)
                Return True
            Catch ex As Exception
                Common.PopupExclamation(ex.Message)
                Return False
            End Try
        End Function

        Public Shared Function IsReportTagged(ByVal reportNumber As Integer) As Boolean
            Dim db As New Data.Database()

            Try
                db.ExecuteNonQuerySp(Infrastructure.Enumerations.Sql.NonQuery.Do_Admin_TagReport, reportNumber)
                Return True
            Catch ex As Exception
                Common.PopupExclamation(ex.Message)
                Return False
            End Try
        End Function

        Public Shared Function IsEndOfDayExecuted() As Boolean
            Dim db As New Data.Database()

            Try
                db.BeginTransaction()
                db.ExecuteNonQuerySp(Infrastructure.Enumerations.Sql.NonQuery.Do_Admin_EndOfDay, sysuser)
                db.CommitTransaction()
                Return True
            Catch ex As Exception
                db.RollbackTransaction()
                Common.PopupExclamation(ex.Message)
                Return False
            End Try
        End Function

        Public Shared Function IsInterestExtracted(ByVal dateFrom As DateTime, ByVal dateTo As DateTime) As Boolean
            Dim db As New Data.Database()

            Try
                db.BeginTransaction()
                db.ExecuteNonQuerySp(Infrastructure.Enumerations.Sql.NonQuery.Do_Admin_ExtractInterest, dateFrom, dateTo)
                db.CommitTransaction()
                Return True
            Catch ex As Exception
                db.RollbackTransaction()
                Common.PopupExclamation(ex.Message)
                Return False
            End Try
        End Function

        Public Shared Function IsArchived(ByVal archiveDate As String) As Boolean
            Dim db As New Data.Database()

            Try
                db.BeginTransaction()
                db.ExecuteNonQuerySp(Infrastructure.Enumerations.Sql.NonQuery.Do_Admin_Archive, archiveDate)
                db.CommitTransaction()
                Return True
            Catch ex As Exception
                db.RollbackTransaction()
                Common.PopupExclamation(ex.Message)
                Return False
            End Try
        End Function

        Public Shared Function IsSystemOpened() As Boolean
            Dim db As New Data.Database()

            Try
                db.ExecuteNonQuerySp(Infrastructure.Enumerations.Sql.NonQuery.Do_Admin_Open)
                Return True
            Catch ex As Exception
                Common.PopupExclamation(ex.Message)
                Return False
            End Try
        End Function

        Public Shared Function IsSystemClosed() As Boolean
            Dim db As New Data.Database()

            Try
                db.BeginTransaction()
                db.ExecuteNonQuerySp(Infrastructure.Enumerations.Sql.NonQuery.Do_Admin_Close, sysuser)
                db.CommitTransaction()
                Return True
            Catch ex As Exception
                db.RollbackTransaction()
                Common.PopupExclamation(ex.Message)
                Return False
            End Try
        End Function

        Public Shared Function IsDailyReversionProcessed() As Boolean
            Dim db As New Data.Database()

            Try
                db.BeginTransaction()
                db.ExecuteNonQuerySp(Infrastructure.Enumerations.Sql.NonQuery.Do_Admin_DailyReversion)
                db.CommitTransaction()
                Return True
            Catch ex As Exception
                db.RollbackTransaction()
                Common.PopupExclamation(ex.Message)
                Return False
            End Try
        End Function

        Public Shared Function IsDbBackedUp() As Boolean
            Dim popup As New Business.Popups.InputText
            Dim backupFolder As String = Configuration.ConfigurationManager.AppSettings.Get("Folder.Backup")
            Dim defaultFile As String = backupFolder & "\" & "KBCI.bak"
            Dim zipExec As String = Configuration.ConfigurationManager.AppSettings.Get("File.Zip")
            Dim zipFile As String
            
            If Common.OpenPopup(popup, "Backup Filename") = Infrastructure.Enumerations.Popups.PopupResponses.Ok Then
                If Not popup.Text.EndsWith(".zip") Then popup.Text &= ".zip"
                zipFile = backupFolder & "\" & popup.Text.Replace(" ", "_")

                Dim db As New Loan.Application.Infrastructure.Data.Database()
                Try
                    db.ExecuteNonQuery(String.Format("exec xp_cmdshell 'if exist {0} del {0}'", defaultFile), CommandType.Text)
                    db.ExecuteNonQuery(String.Format("exec xp_cmdshell 'if exist {0} del {0}'", zipFile), CommandType.Text)
                    db.ExecuteNonQuery("USE MASTER ; BACKUP DATABASE KBCI TO KBCI_DEVICE", CommandType.Text)
                    db.ExecuteNonQuery(String.Format("exec xp_cmdshell '{0} u -tzip {1} {2}'", zipExec, zipFile, defaultFile), CommandType.Text)
                    db.ExecuteNonQuery(String.Format("exec xp_cmdshell 'if exist {0} del {0}'", defaultFile), CommandType.Text)
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End If
        End Function

        Public Shared Function IsDbRestored() As Boolean
            Dim commandText As New System.Text.StringBuilder()
            Dim db As New Loan.Application.Infrastructure.Data.Database()
            Dim dt As New DataTable()
            Dim form As New Loan.Application.Infrastructure.Forms.Popups.PopupDataGridOptions()
            Dim zipExec As String = Configuration.ConfigurationManager.AppSettings.Get("File.Zip")
            Dim backupFolder As String = Configuration.ConfigurationManager.AppSettings.Get("Folder.Backup")
            Dim backupDrive As String = backupFolder.Substring(0, 2)
            Dim backupFile As String
            Dim defaultFile As String = backupFolder & "\" & "KBCI.bak"

            commandText.AppendLine("use master")
            commandText.AppendLine("declare @temp table ([BACKUP] varchar(200))")
            commandText.AppendLine("insert @temp exec xp_cmdshell 'dir """ & backupFolder & """\*.zip /o-d/b/a:-d'")
            commandText.AppendLine("select * from @temp where [BACKUP] is not null")

            dt = db.ExecuteQuery(commandText.ToString(), CommandType.Text).Tables(0)
            form.GetDataGrid().DataSource = dt
            form.ShowDialog()

            If Not form.IsCanceled Then
                If Common.PopupQuestion("System restore initializing. Press 'Yes' to continue") = Windows.Forms.DialogResult.Yes Then
                    backupFile = CType(form.GetDataGrid().DataSource, DataTable).Rows(form.GetDataGrid().CurrentCell.RowIndex).Item("BACKUP")
                    db.ExecuteNonQuery(String.Format("exec xp_cmdshell 'if exist {0} del {0}'", defaultFile), CommandType.Text)
                    db.ExecuteNonQuery(String.Format("exec xp_cmdshell '{0} && cd {1} && {2} x {3}'", backupDrive, backupFolder, zipExec, backupFile), CommandType.Text)
                    If ExecuteDbRestore() Then
                        db.ExecuteNonQuery(String.Format("exec xp_cmdshell 'if exist {0} del {0}'", defaultFile), CommandType.Text)
                        Return True
                    End If
                End If
            End If

            Return False
        End Function

        Private Shared Function ExecuteDbRestore() As Boolean
            Dim connectionString As String = Data.Database.GetConnectionString(True)

            Using connection As SqlConnection = New SqlConnection(connectionString)
                Dim command As SqlCommand = New SqlCommand(GetCloseConnectionScript(), connection)
                connection.Open()

                Try
                    command.CommandType = CommandType.Text
                    command.CommandTimeout = 99999
                    command.ExecuteNonQuery()

                    command = New SqlCommand("RESTORE DATABASE KBCI FROM KBCI_DEVICE WITH REPLACE", connection)
                    command.CommandType = CommandType.Text
                    command.CommandTimeout = 99999
                    command.ExecuteNonQuery()

                    command = New SqlCommand("USE KBCI; exec sp_change_users_login 'auto_fix', 'KBCIuser'", connection)
                    command.CommandType = CommandType.Text
                    command.CommandTimeout = 99999
                    command.ExecuteNonQuery()

                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    connection.Close()
                End Try
            End Using
        End Function

        Private Shared Function GetCloseConnectionScript() As String
            Dim sb As New System.Text.StringBuilder()
            sb.AppendLine("DECLARE @SPID INT;")
            sb.AppendLine("DECLARE @SQL NVARCHAR(20);")
            sb.AppendLine("DECLARE KILLER CURSOR FOR")
            sb.AppendLine("SELECT SPID")
            sb.AppendLine("FROM sys.sysprocesses")
            sb.AppendLine("WHERE DB_NAME(DBID) = 'KBCI' AND")
            sb.AppendLine(" SPID >= 50 AND")
            sb.AppendLine(" SPID != @@SPID;")
            sb.AppendLine("OPEN KILLER;")
            sb.AppendLine("FETCH KILLER INTO @SPID;")
            sb.AppendLine("WHILE @@FETCH_STATUS = 0")
            sb.AppendLine("BEGIN")
            sb.AppendLine(" SET @SQL = 'KILL ' + CONVERT(VARCHAR, @SPID);")
            sb.AppendLine(" EXEC SP_EXECUTESQL @STATEMENT = @SQL;")
            sb.AppendLine(" FETCH KILLER INTO @SPID;")
            sb.AppendLine("END")
            sb.AppendLine("CLOSE KILLER;")
            sb.AppendLine("DEALLOCATE KILLER;")
            Return sb.ToString()
        End Function

    End Class
End Namespace