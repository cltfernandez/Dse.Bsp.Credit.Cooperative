Namespace Business.Objects

#Region "Charge"

    Public Class Charge
        Inherits Infrastructure.Business.Objects.Charge
    End Class

#End Region

#Region "Collaterals"

    Public Class Collaterals
        Inherits Infrastructure.Business.Objects.Collaterals
    End Class

#End Region

#Region "Comaker"

    Public Class Comaker
        Inherits Infrastructure.Business.Objects.Comaker
    End Class

#End Region

#Region "Control"
    Public Class Control
        Inherits Infrastructure.Business.Objects.Control
    End Class
#End Region

#Region "Deductions"

    Public Class Deductions
        Inherits Infrastructure.Business.Objects.Deductions
    End Class

#End Region

#Region "FixedDeposit"

    Public Class FixedDeposit
        Inherits Infrastructure.Business.Objects.FixedDeposit
    End Class

#End Region

#Region "Ledger"

    Public Class Ledger
        Inherits Infrastructure.Business.Objects.Ledger
    End Class

#End Region

#Region "Loan"

    Public Class Loan
        Inherits Infrastructure.Business.Objects.Loan
    End Class

#End Region

#Region "LoanHold"

    Public Class LoanHold
        Inherits Infrastructure.Business.Objects.LoanHold
    End Class

#End Region

#Region "LoanInput"

    Public Class LoanInput
        Inherits Infrastructure.Business.Objects.LoanInput
    End Class

#End Region

#Region "LoanList"

    Public Class LoanList
        Inherits Infrastructure.Business.Objects.LoanList
    End Class

#End Region

#Region "LoanReleaseInsurance"

    Public Class LoanReleaseInsurance
        Inherits Infrastructure.Business.Objects.LoanReleaseInsurance
    End Class

#End Region

#Region "LoanTypeDetail"
    Public Class LoanTypeDetail
        Inherits Infrastructure.Business.Objects.LoanTypeDetail
    End Class
#End Region

#Region "Member"

    Public Class Member
        Inherits Infrastructure.Business.Objects.Member
    End Class

#End Region

#Region "MemberList"

    Public Class MemberList
        Inherits Infrastructure.Business.Objects.MemberList
    End Class

#End Region

#Region "MonthlyDeduction"

    Public Class MonthlyDeduction
        Inherits Infrastructure.Business.Objects.MonthlyDeduction
    End Class

    Public Class OffCycleDeduction
        Inherits Infrastructure.Business.Objects.OffCycleDeduction
    End Class

#End Region

#Region "NetProceeds"
    Public Class NetProceeds
        Inherits Infrastructure.Business.Objects.NetProceeds

        Public Sub New(ByVal account As String, ByVal debit As Decimal, ByVal credit As Decimal)
            MyBase.New(account, debit, credit)
        End Sub
    End Class
#End Region

#Region "RLoanReleaseInsurance"

    Public Class RLoanReleaseInsurance
        Inherits Infrastructure.Business.Objects.RLoanReleaseInsurance
    End Class

#End Region

#Region "SavingsDeposit"

    Public Class SavingsDeposit
        Inherits Infrastructure.Business.Objects.SavingsDeposit
    End Class

#End Region

#Region "SavingsControl"

    Public Class SavingsControl
        Inherits Infrastructure.Business.Objects.SavingsControl
    End Class

#End Region

#Region "SavingsDepositMaster"

    Public Class SavingsDepositMaster
        Inherits Infrastructure.Business.Objects.SavingsDepositMaster
    End Class

#End Region

#Region "User"
    Public Class User
        Inherits Infrastructure.Business.Objects.User
    End Class
#End Region

End Namespace