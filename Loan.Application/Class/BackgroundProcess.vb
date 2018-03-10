Imports System.ComponentModel

Public Class BackgroundProcess
    Dim WithEvents bgw As BackgroundWorker
    Dim callingForm As Windows.Forms.Form
    Dim db As Data.Database
    Dim asyncProcess As Infrastructure.Enumerations.System.AsyncProcess

    Public Sub New(ByVal form As Windows.Forms.Form, ByVal process As Infrastructure.Enumerations.System.AsyncProcess)
        callingForm = form
        asyncProcess = process
        bgw = New BackgroundWorker()
    End Sub

    Public Sub New(ByVal form As Windows.Forms.Form, ByVal process As Infrastructure.Enumerations.System.AsyncProcess, ByVal database As Data.Database)
        callingForm = form
        asyncProcess = process
        db = database
        bgw = New BackgroundWorker()
    End Sub

    Private Sub bgw_DoWork(ByVal sender As System.Object, ByVal e As DoWorkEventArgs) Handles bgw.DoWork
        Select Case asyncProcess
            Case Infrastructure.Enumerations.System.AsyncProcess.Close
                If Business.Rules.Administration.IsSystemClosed() Then
                    Common.PopupInformation("Closed successfully completed.")
                Else
                    Common.PopupExclamation("Close failed. Please contact developer.")
                End If
            Case Infrastructure.Enumerations.System.AsyncProcess.EndOfDay
                If Business.Rules.Administration.IsEndOfDayExecuted Then
                    Common.PopupInformation("EOD successfully completed.")
                    CType(callingForm, Master).Close()
                Else
                    Common.PopupExclamation("EOD failed.")
                    CType(callingForm, Master).SetMenuAfterClosing.Invoke()
                End If
            Case Infrastructure.Enumerations.System.AsyncProcess.Backup
                If Business.Rules.Administration.IsDbBackedUp() Then
                    Common.PopupInformation("System backup successful.")
                Else
                    Common.PopupExclamation("System backup failed.")
                End If
            Case Infrastructure.Enumerations.System.AsyncProcess.Restore
                If Business.Rules.Administration.IsDbRestored() Then
                    Common.PopupInformation("System restore successful. Loan application shutting down, please reopen.")
                    End
                Else
                    Common.PopupExclamation("System restore failed.")
                End If
            Case Infrastructure.Enumerations.System.AsyncProcess.Payroll
                Try
                    Common.PopupInformation("Starting to post payroll deductions. Press any key to continue.")
                    db.BeginTransaction()
                    db.ExecuteNonQuery("dbo.s3p_Payroll_Process", CommandType.StoredProcedure)
                    db.CommitTransaction()
                    Common.ProcessPayrollRecomputeBalance()
                    Common.PopupInformation("Posting of Payroll Deductions Finished.")
                Catch ex As Exception
                    db.RollbackTransaction()
                    Common.PopupExclamation(ex.Message)
                End Try
            Case Infrastructure.Enumerations.System.AsyncProcess.OffCycle
                Try
                    Common.PopupInformation("Starting to post other deductions. Press any key to continue.")
                    db.BeginTransaction()
                    db.ExecuteNonQuery("dbo.s3p_Payroll_Others_Process", CommandType.StoredProcedure)
                    db.CommitTransaction()
                    Common.ProcessOffCycleRecomputeBalance()
                    Common.PopupInformation("Posting of Others Deductions Finished.")
                Catch ex As Exception
                    db.RollbackTransaction()
                    Common.PopupExclamation(ex.Message)
                End Try
        End Select
    End Sub

    Private Sub bgw_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As RunWorkerCompletedEventArgs) Handles bgw.RunWorkerCompleted
        Select Case asyncProcess
            Case Infrastructure.Enumerations.System.AsyncProcess.Close
                CType(callingForm, Master).SetMenuAfterClosing.Invoke()
            Case Infrastructure.Enumerations.System.AsyncProcess.EndOfDay
                CType(callingForm, Master).SetMenuAfterOpening.Invoke()
            Case Infrastructure.Enumerations.System.AsyncProcess.Backup
                CType(callingForm, Master).SetMenu.Invoke(True)
            Case Infrastructure.Enumerations.System.AsyncProcess.Restore
                CType(callingForm, Master).SetMenu.Invoke(True)
            Case Infrastructure.Enumerations.System.AsyncProcess.Payroll
                CType(callingForm, Master).SetMenu.Invoke(True)
            Case Infrastructure.Enumerations.System.AsyncProcess.OffCycle
                CType(callingForm, Master).SetMenu.Invoke(True)
        End Select
    End Sub

    Public Sub RunWorkerAsync()
        bgw.RunWorkerAsync()
    End Sub

End Class
