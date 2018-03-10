Namespace Business.Popups

    Public Class ControlDate
        Inherits Infrastructure.Business.Popups.ControlDate

        Public Sub New()
            MyBase.AdminDate = admdate
            MyBase.SystemDate = sysdate
        End Sub

    End Class

    Public Class InputText
        Inherits Infrastructure.Business.Popups.InputText
    End Class

    Public Class InputInt
        Inherits Infrastructure.Business.Popups.InputInt
    End Class

    Public Class InputAmount
        Inherits Infrastructure.Business.Popups.InputAmount
    End Class

    Public Class InputDate
        Inherits Infrastructure.Business.Popups.InputDate

        Public Sub New()
            MyBase.Date = sysdate
        End Sub
    End Class

    Public Class DateRange
        Inherits Infrastructure.Business.Popups.DateRange

        Public Sub New()
            MyBase.DateFrom = sysdate
            MyBase.DateTo = sysdate
        End Sub
    End Class

    Public Class PnNoRange
        Inherits Infrastructure.Business.Popups.PnNoRange
    End Class

    Public Class PnNoAndDateRange
        Inherits Infrastructure.Business.Popups.PnNoAndDateRange

        Public Sub New()
            MyBase.DateFrom = sysdate
            MyBase.DateTo = sysdate
        End Sub
    End Class

    Public Class KbciNoRange
        Inherits Infrastructure.Business.Popups.KbciNoRange
    End Class

    Public Class KbciNoAndDate
        Inherits Infrastructure.Business.Popups.KbciNoAndDate

        Public Sub New()
            MyBase.Date = sysdate
        End Sub
    End Class

    Public Class KbciNoAndDateRange
        Inherits Infrastructure.Business.Popups.KbciNoAndDateRange

        Public Sub New()
            MyBase.DateFrom = sysdate
            MyBase.DateTo = sysdate
        End Sub
    End Class

    Public Class BankAndCheckNo
        Inherits Infrastructure.Business.Popups.BankAndCheckNo
    End Class

    Public Class LoanTypeAndDate
        Inherits Infrastructure.Business.Popups.LoanTypeAndDate

        Public Sub New()
            MyBase.Date = sysdate
        End Sub
    End Class

    Public Class AsOfDateAndDateRange
        Inherits Infrastructure.Business.Popups.AsOfDateAndDateRange
    End Class

    Public Class IntRange
        Inherits Infrastructure.Business.Popups.IntRange
    End Class

    Public Class PayrollTextAndAmount
        Inherits Infrastructure.Business.Popups.PayrollTextAndAmount
    End Class

    Public Class MonthlyRunup
        Inherits Infrastructure.Business.Popups.MonthlyRunup
    End Class

End Namespace