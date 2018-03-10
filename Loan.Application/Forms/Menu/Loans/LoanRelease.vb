Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports Loan.Application.Infrastructure
Imports Loan.Application.Infrastructure.Enumerations

Public Class LoanRelease
    Dim selectedKbciNo As String = String.Empty
    Dim selectedLoanType As String = String.Empty

    Dim member As Business.Objects.Member
    Dim loan As Business.Objects.Loan
    Dim loanInput As Business.Objects.LoanInput

    Dim collateralSearched As List(Of Business.Objects.Collaterals)
    Dim collateralSelected As List(Of Business.Objects.Collaterals)
    Dim collateralBindings As BindingSource

    Dim deductionSearched As List(Of Business.Objects.Deductions)
    Dim deductionSelected As List(Of Business.Objects.Deductions)
    Dim deductionForced As List(Of Business.Objects.Deductions)
    Dim deductionBindings As BindingSource

    Dim charges As List(Of Business.Objects.Charge)
    Dim chargeBindings As BindingSource


#Region "Global"
#End Region

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ResetObjects()
    End Sub

    Private Sub ResetObjects()
        selectedKbciNo = String.Empty
        selectedLoanType = String.Empty

        member = New Business.Objects.Member
        loan = New Business.Objects.Loan
        loanInput = New Business.Objects.LoanInput()
        pgDetails.SelectedObject = loanInput
        pgDetails.Refresh()

        collateralSearched = New List(Of Business.Objects.Collaterals)
        collateralSelected = New List(Of Business.Objects.Collaterals)
        collateralBindings = New BindingSource

        deductionSearched = New List(Of Business.Objects.Deductions)
        deductionSelected = New List(Of Business.Objects.Deductions)
        deductionForced = New List(Of Business.Objects.Deductions)
        deductionBindings = New BindingSource

        charges = New List(Of Business.Objects.Charge)
        chargeBindings = New BindingSource
    End Sub

    Private Sub LoanMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadDefaults()
        AddHandlers()
        AddEnterNavigator()
    End Sub

    Private Sub tcMain_Deselecting(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles tcMain.Deselecting
        If e.TabPage.Name = TabPages.LoansApplication.tpDetails.ToString() Then
            If loanInput.KBCI_NO Is Nothing OrElse loanInput.KBCI_NO.Trim().Length() <= 0 Then
                Common.PopupError("KBCI number is required.")
                e.Cancel = True
            Else
                LoadCollateralLists()
                LoadDeductionLists()
                LoadCharges()
                GetMember()
                UpdateLoaneeAndLoanType()
            End If
        End If
    End Sub

    Private Sub tcMain_Selecting(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles tcMain.Selecting
        If e.TabPage.Name = TabPages.LoansApplication.tpDetails.ToString() Then
            SaveToolStripMenuItem.Enabled = False
        ElseIf e.TabPage.Name = TabPages.LoansApplication.tpCollaterals.ToString() Then
            SaveToolStripMenuItem.Enabled = False
            ucCollaterals.dgvSearch.Focus()
        ElseIf e.TabPage.Name = TabPages.LoansApplication.tpDeductions.ToString() Then
            SaveToolStripMenuItem.Enabled = False
            ucDeductions.dgvSearch.Focus()
        ElseIf e.TabPage.Name = TabPages.LoansApplication.tpCharges.ToString() Then
            SaveToolStripMenuItem.Enabled = False
            dgvCharges.Focus()
        ElseIf e.TabPage.Name = TabPages.LoansApplication.tpNetProceeds.ToString() Then
            SaveToolStripMenuItem.Enabled = True
            LoadNetProceeds()
        End If
    End Sub

    Private Sub AddHandlers()
        AddHandler ucCollaterals.btnSearch.Click, AddressOf ucCollaterals_btnSearch_Click
        AddHandler ucCollaterals.dgvSearch.KeyDown, AddressOf ucCollaterals_dgvSearchResult_KeyDown
        AddHandler ucCollaterals.dgvSelect.KeyDown, AddressOf ucCollaterals_dgvSelect_KeyDown

        AddHandler ucDeductions.btnSearch.Click, AddressOf ucDeductions_btnSearch_Click
        AddHandler ucDeductions.dgvSearch.KeyDown, AddressOf ucDeductions_dgvSearchResult_KeyDown
        AddHandler ucDeductions.dgvSelect.KeyDown, AddressOf ucDeductions_dgvSelect_KeyDown
    End Sub

    Private Sub AddEnterNavigator()
        Infrastructure.Controls.PropertyGrid.EnterNavigator.Add(pgDetails)
    End Sub

    Private Sub LoadDefaults()
        pgDetails.Focus()
        loanInput.MOD_PAY = DropDownItems.PayMode.Payroll
        loanInput.LED_TYPE = DropDownItems.LedgerType.DiminishingPrincipal
        loanInput.FREQ = DropDownItems.Frequency.Monthly
        loanInput.LRI_IND = False
        pgDetails.SelectedObject = loanInput
        SetRates()
        SetMiscellaneousLiabilities()
    End Sub

    Private Sub LoadSearchList(Of T)(ByVal type As Sql.Query, ByRef list As List(Of T), ByVal grid As DataGridView, ByVal ParamArray parameters() As Object)
        list = Common.GetObjectList(Of T)(type, parameters)
        grid.DataSource = list
    End Sub

    Private Sub LoadSelectList(Of T)(ByRef bindingSource As BindingSource, ByRef list As List(Of T), ByVal grid As DataGridView)
        list = New List(Of T)
        bindingSource.DataSource = list
        grid.DataSource = bindingSource
    End Sub

    Private Sub UpdateLoaneeAndLoanType()
        selectedKbciNo = loanInput.KBCI_NO
        selectedLoanType = loanInput.LOAN_TYPE.ToString()
    End Sub

    Private Function IsListEmpty(Of T)(ByVal list As List(Of T), ByVal message As String) As Boolean
        If list.Count <= 0 Then
            MessageBox.Show(message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return True
        End If
        Return False
    End Function

    Private Function IsListFull(Of T)(ByVal list As List(Of T), ByVal maxCount As Integer, ByVal message As String) As Boolean
        If list.Count > maxCount Then
            MessageBox.Show(message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return True
        End If
        Return False
    End Function

    Private Function IsLoaneeAndLoanTypeModified() As Boolean
        Return loanInput.KBCI_NO IsNot Nothing AndAlso (selectedKbciNo <> loanInput.KBCI_NO OrElse selectedLoanType <> loanInput.LOAN_TYPE.ToString())
    End Function

#Region "Details"
#End Region

    Private Sub pgDetails_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles pgDetails.PropertyValueChanged
        loanInput = pgDetails.SelectedObject

        Select Case e.ChangedItem.PropertyDescriptor.Name
            Case Helpers.Member.Name(Of Business.Objects.LoanInput)(Function(x) x.KBCI_NO)
                LoadDefaults()
                SetMemberName(loanInput.KBCI_NO, loanInput.FULL_NAME)
                pgDetails.Refresh()
            Case Helpers.Member.Name(Of Business.Objects.LoanInput)(Function(x) x.COMAKER1)
                SetMemberName(loanInput.COMAKER1, loanInput.COMAKER_NAME1)
                pgDetails.Refresh()
            Case Helpers.Member.Name(Of Business.Objects.LoanInput)(Function(x) x.COMAKER2)
                SetMemberName(loanInput.COMAKER2, loanInput.COMAKER_NAME2)
                pgDetails.Refresh()
            Case Helpers.Member.Name(Of Business.Objects.LoanInput)(Function(x) x.LOAN_TYPE)
                SetFrequency()
                SetRates()
                SetMiscellaneousLiabilities()
                SetPaymentMode()
                pgDetails.Refresh()
            Case Helpers.Member.Name(Of Business.Objects.LoanInput)(Function(x) x.TERM)
                SetRates()
                pgDetails.Refresh()
            Case Helpers.Member.Name(Of Business.Objects.LoanInput)(Function(x) x.MISC)
                SetMiscellaneousLiabilities()
                pgDetails.Refresh()
            Case Helpers.Member.Name(Of Business.Objects.LoanInput)(Function(x) x.CHKNO_BANK)
                SetBank()
            Case Helpers.Member.Name(Of Business.Objects.LoanInput)(Function(x) x.CHKNO)
                SetCheckNo()
        End Select
    End Sub

    Private Sub SetFrequency()
        Select Case loanInput.LOAN_TYPE
            Case DropDownItems.LoanType.STL
                loanInput.FREQ = DropDownItems.Frequency.Daily
                loanInput.TERM = 30
            Case DropDownItems.LoanType.FAL
                loanInput.FREQ = DropDownItems.Frequency.Monthly
                loanInput.TERM = 36
            Case DropDownItems.LoanType.CML, DropDownItems.LoanType.EML
                loanInput.FREQ = DropDownItems.Frequency.Monthly
                loanInput.TERM = 24
            Case DropDownItems.LoanType.RGL, DropDownItems.LoanType.RSL, DropDownItems.LoanType.SPL
                loanInput.FREQ = DropDownItems.Frequency.Monthly
                loanInput.TERM = 120
            Case DropDownItems.LoanType.SML
                loanInput.FREQ = DropDownItems.Frequency.Monthly
                loanInput.TERM = 24
            Case DropDownItems.LoanType.BFL
                loanInput.FREQ = DropDownItems.Frequency.Monthly
                loanInput.TERM = 1
            Case Else
                loanInput.FREQ = DropDownItems.Frequency.Monthly
                loanInput.TERM = 12
        End Select
    End Sub

    Private Sub SetMemberName(ByRef memberNumber As String, ByRef memberName As String)
        Dim memberDetails() As String
        Dim dr As DataRow
        Dim dt As DataTable

        If memberNumber Is Nothing Then
            memberName = Nothing
        ElseIf memberNumber.Contains("|") Then
            memberDetails = memberNumber.Split("|")
            memberNumber = memberDetails(0)
            memberName = memberDetails(1)
        ElseIf IsNumeric(memberNumber) Then
            dt = Common.GetDetails(Infrastructure.Enumerations.Sql.Query.MemberList, memberNumber)
            If dt.Rows.Count > 0 Then
                dr = dt.Rows(0)
                memberName = dr.Item(Helpers.Member.Name(Of Business.Objects.LoanInput)(Function(x) x.FULL_NAME))
            Else
                Common.PopupError("Member not found.")
                memberNumber = Nothing
                memberName = Nothing
            End If
        Else
            memberNumber = Nothing
            memberName = Nothing
        End If
    End Sub

    Private Sub SetMiscellaneousLiabilities()
        If loanInput.LOAN_TYPE = DropDownItems.LoanType.STL Then
            loanInput.MISC = False
        End If
    End Sub

    Private Sub SetRates()
        Dim dRate As Double

        With loanInput
            Select Case .LOAN_TYPE
                Case DropDownItems.LoanType.SML
                Case DropDownItems.LoanType.BFL
                    .RATE = 0
                Case Else
                    dRate = Common.GetLoanRate(.LOAN_TYPE.ToString(), .TERM)
                    If dRate = 0 Then
                        Select Case .TERM
                            Case 12
                                .RATE = 9
                            Case 24
                                .RATE = 10
                            Case 30
                                .RATE = 17
                            Case 36
                                .RATE = 11
                            Case 48
                                .RATE = 12
                            Case 60
                                .RATE = 13
                            Case 120
                                .RATE = 17
                            Case Else
                                .RATE = 17
                        End Select
                    Else
                        .RATE = dRate
                    End If
            End Select
        End With
    End Sub

    Private Sub SetPaymentMode()
        If loanInput.LOAN_TYPE = DropDownItems.LoanType.BFL Then
            loanInput.MOD_PAY = DropDownItems.PayMode.Offcycle
        ElseIf loanInput.LOAN_TYPE = DropDownItems.LoanType.STL Then
            loanInput.MOD_PAY = DropDownItems.PayMode.DebitMemo
        Else
            loanInput.MOD_PAY = DropDownItems.PayMode.Payroll
        End If
    End Sub

    Private Sub SetBank()
        loanInput.CHKNO_BANK = loanInput.CHKNO_BANK.ToUpper()
    End Sub

    Private Sub SetCheckNo()
        loanInput.CHKNO = loanInput.CHKNO.ToUpper()
    End Sub

#Region "Collaterals"
#End Region

    Private Sub ucCollaterals_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ucCollaterals.Load

    End Sub

    Private Sub ucCollaterals_btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim list As Business.Objects.MemberList = Common.GetMemberList()
        If list IsNot Nothing Then
            ucCollaterals.lblSearch.Text = String.Format("Owner: {0}", list.FULL_NAME)
            LoadCollateralSearchList(list.KBCI_NO)
        End If
    End Sub

    Private Sub ucCollaterals_dgvSelect_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode <> Keys.Delete Then Exit Sub
        If IsCollateralSelectListEmpty() Then Exit Sub
        collateralBindings.RemoveAt(ucCollaterals.dgvSelect.CurrentCell.RowIndex)
    End Sub

    Private Sub ucCollaterals_dgvSearchResult_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode <> Keys.Enter Then Exit Sub
        If IsCollateralSelectListFull() Then Exit Sub
        If IsCollateralSearchListEmpty() Then Exit Sub
        AddCollateral()
        e.Handled = True
    End Sub

    Private Sub AddCollateral()
        With ucCollaterals
            Dim selected As Business.Objects.Collaterals = collateralSearched(.dgvSearch.CurrentCell.RowIndex)
            Dim exist As Boolean = collateralSelected.Any(Function(x) x.CTD_NO = selected.CTD_NO)
            If exist Then
                MessageBox.Show(String.Format("{0} is already in the collateral list.", selected.CTD_NO, "", MessageBoxButtons.OK, MessageBoxIcon.Error))
            Else
                collateralBindings.Add(selected)
                ucCollaterals.dgvSearch.Focus()
            End If
        End With
    End Sub

    Private Sub LoadCollateralLists()
        If IsToLoadCollateralLists() Then
            LoadCollateralSearchList(loanInput.KBCI_NO)
            LoadCollateralSelectList()
        End If
    End Sub

    Private Sub LoadCollateralSearchList(ByVal kbciNo As String)
        LoadSearchList(Of Business.Objects.Collaterals)(Sql.Query.Collaterals, collateralSearched, ucCollaterals.dgvSearch, kbciNo)
    End Sub

    Private Sub LoadCollateralSelectList()
        LoadSelectList(collateralBindings, collateralSelected, ucCollaterals.dgvSelect)
    End Sub

    Private Function IsCollateralSelectListFull() As Boolean
        Const collateralSelectedMaxCount As Integer = 5
        Return IsListFull(collateralSelected, collateralSelectedMaxCount, "Collateral select list is full.")
    End Function

    Private Function IsCollateralSelectListEmpty() As Boolean
        Return IsListEmpty(collateralSelected, "Collateral select list is empty.")
    End Function

    Private Function IsCollateralSearchListEmpty() As Boolean
        Return IsListEmpty(collateralSearched, "Collateral search list is empty.")
    End Function

    Private Function IsToLoadCollateralLists() As Boolean
        Select Case True
            Case loanInput.LOAN_TYPE <> DropDownItems.LoanType.STL
                IsToLoadCollateralLists = False
            Case IsLoaneeAndLoanTypeModified()
                ucCollaterals.lblSearch.Text = String.Format("Owner: {0}", loanInput.FULL_NAME)
                IsToLoadCollateralLists = True
            Case Else
                IsToLoadCollateralLists = False
        End Select
    End Function

#Region "Deductions"
#End Region

    Private Sub ucDeductions_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucDeductions.Load

    End Sub

    Private Sub ucDeductions_btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim list As Business.Objects.MemberList = Common.GetMemberList()
        If list IsNot Nothing Then
            ucDeductions.lblSearch.Text = String.Format("Owner: {0}", list.FULL_NAME)
            LoadDeductionSearchList(list.KBCI_NO, "%", "False")
        End If
    End Sub

    Private Sub ucDeductions_dgvSelect_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode <> Keys.Delete Then Exit Sub
        If IsDeductionSelectListEmpty() Then Exit Sub
        If IsLoanForced(deductionSelected(ucDeductions.dgvSelect.CurrentCell.RowIndex).PN_NO) Then Exit Sub
        deductionBindings.RemoveAt(ucDeductions.dgvSelect.CurrentCell.RowIndex)
    End Sub

    Private Sub ucDeductions_dgvSearchResult_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode <> Keys.Enter Then Exit Sub
        If IsDeductionSearchListEmpty() Then Exit Sub
        AddDeduction()
        e.Handled = True
    End Sub

    Private Sub AddDeduction()
        With ucDeductions
            Dim deduction As Business.Objects.Deductions = deductionSearched(.dgvSearch.CurrentCell.RowIndex)
            Dim lri As Business.Objects.LoanReleaseInsurance = Common.GetLoanReleaseInsurance(deduction.PN_NO)
            Dim exist As Boolean = deductionSelected.Any(Function(x) x.PN_NO = deduction.PN_NO)

            If exist Then
                MessageBox.Show(String.Format("{0} is already in the deduction list.", deduction.PN_NO, "", MessageBoxButtons.OK, MessageBoxIcon.Error))
            Else
                Select Case Common.PopupOptions("Full balance", "Arrear", "Other Amount", "LRI Due", "Cancel", "")
                    Case 1
                        If Not IsDeductFullBalance(deduction, lri) Then Exit Sub
                    Case 2
                        If Not IsDeductArrear(deduction, lri) Then Exit Sub
                    Case 3
                        If Not IsDeductOtherAmount(deduction, lri) Then Exit Sub
                    Case 4
                        If Not IsDeductLoanReleaseInsurance(deduction, lri) Then Exit Sub
                    Case 5
                        Exit Sub
                End Select
                'deduction.PN_TAG = True
                deductionBindings.Add(deduction)
                ucDeductions.dgvSearch.Focus()
            End If
        End With
    End Sub

    Private Sub LoadDeductionLists()
        If IsToLoadDeductionLists() Then
            LoadDeductionSearchList(loanInput.KBCI_NO, loanInput.LOAN_TYPE.ToString(), loanInput.xrenewSTL)
            LoadDeductionForcedList()
            LoadDeductionSelectList()
        End If
    End Sub

    Private Sub LoadDeductionSearchList(ByVal kbciNo As String, ByVal loanType As String, ByVal renew As String)
        LoadSearchList(Of Business.Objects.Deductions)(Sql.Query.Deductions, deductionSearched, ucDeductions.dgvSearch, kbciNo, loanType, renew, sysuser)
    End Sub

    Private Sub LoadDeductionForcedList()
        deductionForced = New List(Of Business.Objects.Deductions)
        deductionForced.AddRange(deductionSearched.Where(Function(x) x.PN_TAG).AsEnumerable())
        For Each deduction As Business.Objects.Deductions In deductionForced
            deduction.PN_TAG = True
            deduction.PAY_AMT = deduction.OUTSBAL
            deduction.PAY_TAG = DropDownItems.PayTag.Full
        Next
    End Sub

    Private Sub LoadDeductionSelectList()
        LoadSelectList(deductionBindings, deductionSelected, ucDeductions.dgvSelect)
        If deductionForced.Count > 0 Then
            For Each deduction As Business.Objects.Deductions In deductionForced
                deductionBindings.Add(deduction)
            Next
            deductionBindings.DataSource = deductionSelected
        End If
    End Sub

    Private Function IsDeductArrear(ByVal deduction As Business.Objects.Deductions, ByVal lri As Business.Objects.LoanReleaseInsurance) As Boolean
        If deduction.ARREARS <= 0 Then Common.PopupError("Loan has no arrears.") : Return False

        deduction.LRI_DUE = lri.LRI_DUE_C
        deduction.PAY_AMT = deduction.ARREARS
        deduction.PAY_TAG = DropDownItems.PayTag.Arrear
        Return True
    End Function

    Private Function IsDeductFullBalance(ByVal deduction As Business.Objects.Deductions, ByVal lri As Business.Objects.LoanReleaseInsurance) As Boolean
        If lri Is Nothing Then
            deduction.LRI_DUE = 0
        Else
            deduction.LRI_DUE = lri.LRI_DUE_C
        End If

        deduction.PAY_AMT = deduction.OUTSBAL
        deduction.PAY_TAG = DropDownItems.PayTag.Full
        Return True
    End Function

    Private Function IsDeductLoanReleaseInsurance(ByVal deduction As Business.Objects.Deductions, ByVal lri As Business.Objects.LoanReleaseInsurance) As Boolean
        If lri.LRI_DUE_P <= 0 Then Common.PopupError("Loan has no LRI due.") : Return False

        deduction.LRI_DUE = lri.LRI_DUE_P
        deduction.PAY_AMT = deduction.LRI_DUE
        deduction.PAY_TAG = DropDownItems.PayTag.LRI
        Return True
    End Function

    Private Function IsDeductOtherAmount(ByVal deduction As Business.Objects.Deductions, ByVal lri As Business.Objects.LoanReleaseInsurance) As Boolean
        Dim popup As New Business.Popups.InputAmount()
        If Common.OpenPopup(popup, "Enter amount.") <> Popups.PopupResponses.Ok Then Return False

        deduction.LRI_DUE = lri.LRI_DUE_C
        deduction.PAY_AMT = popup.Amount()
        deduction.PAY_TAG = DropDownItems.PayTag.OtherAmount
        Return True
    End Function

    Private Function IsDeductionSearchListEmpty() As Boolean
        Return IsListEmpty(deductionSearched, "The deduction search list is empty.")
    End Function

    Private Function IsDeductionSelectListEmpty() As Boolean
        Return IsListEmpty(deductionSelected, "The deduction select list is empty.")
    End Function

    Private Function IsLoanForced(ByVal pnNo As String) As Boolean
        IsLoanForced = deductionForced.Any(Function(x) x.PN_NO = pnNo)
        If IsLoanForced Then MessageBox.Show(String.Format("{0} is required for renewal.", pnNo), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Function

    Private Function IsToLoadDeductionLists() As Boolean
        IsToLoadDeductionLists = False

        Select Case True
            Case IsLoaneeAndLoanTypeModified()
                ucDeductions.lblSearch.Text = String.Format("Owner: {0}", loanInput.FULL_NAME)
                IsToLoadDeductionLists = True
            Case Else
                IsToLoadDeductionLists = False
        End Select
    End Function

#Region "Charges"
#End Region

    Private Sub dgvCharges_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCharges.CellValueChanged
        If dgvCharges.CurrentCell IsNot Nothing AndAlso dgvCharges.Columns(dgvCharges.CurrentCell.ColumnIndex).Name IsNot Nothing Then
            Dim charge As Business.Objects.Charge = charges(dgvCharges.CurrentCell.RowIndex)
            Select Case dgvCharges.Columns(dgvCharges.CurrentCell.ColumnIndex).Name
                Case Helpers.Member.Name(Of Business.Objects.Charge)(Function(x) x.ODOX_TYPE), Helpers.Member.Name(Of Business.Objects.Charge)(Function(x) x.AMOUNT)
                    SetChargeAmounts(charge)
                    'Case Helpers.Member.Name(Of Business.Objects.Charge)(Function(x) x.REMARKS)
                    '    SetChargeRemarks(charge)
            End Select
            SetChargeRemarks(charge)
            SetChargeFields(charge)
        End If
    End Sub

    Private Sub dgvCharges_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvCharges.KeyDown
        If e.KeyCode = Keys.Enter Then
            Select Case dgvCharges.Columns(dgvCharges.CurrentCell.ColumnIndex).Name
                Case Helpers.Member.Name(Of Business.Objects.Charge)(Function(x) x.OPN_NO)
                    If Common.PopupQuestion("Change PN_NO?") = Windows.Forms.DialogResult.Yes Then
                        dgvCharges.CurrentCell.Value = Helpers.Queries.GetLoan().PN_NO
                    End If
                    e.Handled = True
            End Select
        End If
    End Sub

    Private Sub LoadCharges()
        If IsToLoadChargesList() Then
            If dgvCharges.Columns.Count <= 0 Then SetChargeGridColumns(dgvCharges)
            LoadChargeList()
        End If
    End Sub

    Private Sub LoadChargeList()
        LoadSelectList(Of Business.Objects.Charge)(chargeBindings, charges, dgvCharges)
    End Sub

    Private Sub GetCheckDetails(ByVal charge As Business.Objects.Charge)
        Dim popup As New Business.Popups.BankAndCheckNo()
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        charge.OBANK = popup.Bank
        charge.OCHKNO = popup.CheckNo
    End Sub

    Private Function GetSavingsDetails(ByVal charge As Business.Objects.Charge) As Boolean
        Dim savingsDeposit As Business.Objects.SavingsDepositMaster
        GetMember()
        savingsDeposit = Common.GetSavingsAccount(member.FEBTC_SA, charge.ODR, charge.OCR > 0)
        If savingsDeposit Is Nothing Then Return False
        charge.OACCTNM = savingsDeposit.ACCTNAME
        charge.OACCTNO = savingsDeposit.ACCTNO
        Return True
    End Function

    Private Sub SetChargeAmounts(ByVal charge As Business.Objects.Charge)
        Select Case charge.ODOX_TYPE
            Case DropDownItems.DoubleEntry.DM
                charge.ODR = charge.AMOUNT
                charge.OCR = 0
            Case DropDownItems.DoubleEntry.CM
                charge.ODR = 0
                charge.OCR = charge.AMOUNT
        End Select
    End Sub

    Private Sub SetChargeGridColumns(ByVal dataGridView As DataGridView)
        Dim cbo As DataGridViewComboBoxColumn
        Dim txt As DataGridViewTextBoxColumn

        For Each prop As System.Reflection.PropertyInfo In GetType(Business.Objects.Charge).GetProperties()
            Select Case True
                Case prop.PropertyType.IsEnum
                    cbo = New DataGridViewComboBoxColumn()
                    cbo.DataSource = [Enum].GetValues(prop.PropertyType)
                    dataGridView.Columns.Add(cbo)
                Case Else
                    txt = New DataGridViewTextBoxColumn()
                    dataGridView.Columns.Add(txt)
            End Select

            With dataGridView.Columns(dataGridView.Columns.Count - 1)
                Select Case prop.Name
                    Case Helpers.Member.Name(Of Business.Objects.Charge)(Function(x) x.OPN_NO)
                        .Visible = True
                        .ReadOnly = True
                    Case Helpers.Member.Name(Of Business.Objects.Charge)(Function(x) x.ODOX_TYPE)
                        .Visible = True
                    Case Helpers.Member.Name(Of Business.Objects.Charge)(Function(x) x.AMOUNT)
                        .Visible = True
                    Case Helpers.Member.Name(Of Business.Objects.Charge)(Function(x) x.REMARKS)
                        .Visible = True
                    Case Else
                        .Visible = False
                End Select
                .Name = prop.Name
                .DataPropertyName = prop.Name
                If .Visible Then .HeaderText = DirectCast(Attribute.GetCustomAttribute(prop, GetType(DisplayNameAttribute)), DisplayNameAttribute).DisplayName
            End With
        Next

        dataGridView.Columns(dataGridView.Columns.Count - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Private Sub SetChargeRemarks(ByVal charge As Business.Objects.Charge)
        charge.OBANK = String.Empty
        charge.OCHKNO = String.Empty
        charge.OACCTNM = String.Empty
        charge.OACCTNO = String.Empty
        charge.OREF = String.Empty

        Select Case charge.REMARKS
            Case DropDownItems.ChargeRemarks.Check
                GetCheckDetails(charge)
            Case DropDownItems.ChargeRemarks.Savings
                If Not GetSavingsDetails(charge) Then
                    charge.REMARKS = DropDownItems.ChargeRemarks.Cash
                    charge.OREF = String.Empty
                    charge.ORMK = String.Empty
                End If
        End Select
    End Sub

    Private Sub SetChargeFields(ByVal charge As Business.Objects.Charge)
        Select Case charge.REMARKS
            Case DropDownItems.ChargeRemarks.Check
                charge.OREF = "COH-CHECK"
                charge.ORMK = "COH-CHECK"
            Case DropDownItems.ChargeRemarks.AP
                charge.OREF = "A/P"
                charge.ORMK = "A/P"
            Case DropDownItems.ChargeRemarks.AR
                charge.OREF = "A/R"
                charge.ORMK = "A/R"
            Case DropDownItems.ChargeRemarks.FixedDeposit
                charge.OREF = "FIXED DEP"
                charge.ORMK = "FIXED DEP"
            Case DropDownItems.ChargeRemarks.ServiceCharge
                charge.OREF = "S-CHARGE"
                charge.ORMK = "S-CHARGE"
            Case Else
                charge.OREF = charge.REMARKS.ToString().ToUpper()
                charge.ORMK = charge.REMARKS.ToString().ToUpper()
        End Select

        If charge.REMARKS = DropDownItems.ChargeRemarks.Savings Then
            charge.ORMK = String.Format("INIT-{0}-{1}", charge.ORMK, charge.OACCTNO)
        Else
            charge.ORMK = String.Format("INIT-{0}", charge.ORMK)
        End If

        If charge.ODOX_TYPE = DropDownItems.DoubleEntry.CM Then
            charge.OCR = charge.AMOUNT
            charge.ODR = 0
        Else
            charge.OCR = 0
            charge.ODR = charge.AMOUNT
        End If

        charge.OLOAN_TYPE = loanInput.LOAN_TYPE
        charge.OACCT_TYPE = charge.REMARKS
        charge.OACCT_CODE = DropDownItems.AccountCode.OTH
    End Sub

    Private Function IsToLoadChargesList() As Boolean
        Select Case True
            Case IsLoaneeAndLoanTypeModified()
                IsToLoadChargesList = True
            Case Else
                IsToLoadChargesList = False
        End Select
    End Function

#Region "Net Proceeds"
#End Region

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        DoNetProceeds(DropDownItems.NetProceedsMode.F)
    End Sub

    Private Sub LoadNetProceeds()
        DoNetProceeds(DropDownItems.NetProceedsMode.C)
    End Sub

    Private Sub DoNetProceeds(ByVal mode As DropDownItems.NetProceedsMode)
        GetLoanTypes()
        If IsValidDetails() Then
            SetLoan()
            If IsValidLoanType() AndAlso IsValidLoanAmount() AndAlso IsValidTerm() AndAlso IsValidFrequency() AndAlso IsValidInterest() Then
                Dim proceeds As New Business.Rules.Loans.NetProceeds(mode, loan, loanInput, member, deductionSelected, charges)
                Dim list As List(Of Business.Objects.NetProceeds)
                Select Case mode
                    Case DropDownItems.NetProceedsMode.C
                        list = proceeds.GetNetProceeds()
                        dgvNetProceeds.DataSource = list
                        Common.SetDataGridView(dgvNetProceeds)
                    Case DropDownItems.NetProceedsMode.F
                        proceeds.GetNetProceeds()
                        loan.CHKNO_AMT = proceeds.Proceeds()
                        If loan.CHKNO_AMT < 0 Then
                            Common.PopupError("Net proceeds is either 0 or negative.")
                        ElseIf loan.LOAN_TYPE = DropDownItems.LoanType.SML AndAlso loan.CHKNO_AMT <> loan.PRINCIPAL Then
                            Common.PopupError("Net proceeds should be equal to loan amount for SML.")
                        Else
                            If proceeds.SetNetProceeds() Then
                                tcMain.SelectedIndex = 0
                                ResetObjects()
                            End If
                        End If
                End Select
            End If
        End If
    End Sub

    Private Sub GetLoanTypes()
        If loanTypeDetails Is Nothing Then
            loanTypeDetails = Common.GetObjectList(Of Business.Objects.LoanTypeDetail)()
        End If
    End Sub

    Private Sub GetMember()
        If member Is Nothing OrElse member.KBCI_NO <> loanInput.KBCI_NO Then
            member = Common.GetMember(loanInput.KBCI_NO)
        End If
    End Sub

    Private Sub SetLoan()
        loan = New Business.Objects.Loan

        loan.KBCI_NO = loanInput.KBCI_NO
        loan.APP_DATE = sysdate
        loan.CHKNO_DATE = sysdate
        loan.USER = sysuser
        loan.LOAN_STAT = DropDownItems.LoanStatus.Released
        loan.MOD_PAY = loanInput.MOD_PAY

        loan.DATE_GRANT = sysdate
        loan.CHKNO_RELS = sysuser
        loan.ADD_DATE = sysdate

        loan.LOAN_TYPE = loanInput.LOAN_TYPE
        loan.LED_TYPE = loanInput.LED_TYPE
        loan.PRINCIPAL = loanInput.PRINCIPAL
        loan.TERM = loanInput.TERM
        loan.FREQ = loanInput.FREQ
        loan.RATE = loanInput.RATE
        loan.LRI_IND = loanInput.LRI_IND
        loan.CHKNO_BANK = loanInput.CHKNO_BANK
        loan.CHKNO = loanInput.CHKNO
        loan.COLLATERAL = GetCollateral()

        loanInput.xrenew = False
        loanInput.xrenew2 = False
        loanInput.xrenewSTL = False
        loanInput.xplnamt = 0
        loanInput.xtotamt = 0
        loanInput.reyt = 0
        loanInput.month = 0
        loanInput.mterm = 0
        loanInput.resigned = False
    End Sub

    Private Sub SetFrequencyAndTerm()
        Select Case loan.FREQ
            Case DropDownItems.Frequency.Daily
                loanInput.reyt = 360
                loanInput.month = 1
            Case DropDownItems.Frequency.Monthly
                loanInput.reyt = 12
                loanInput.month = 1
            Case DropDownItems.Frequency.Quarterly
                loanInput.reyt = 4
                loanInput.month = 3
            Case DropDownItems.Frequency.SemiAnnual
                loanInput.reyt = 2
                loanInput.month = 6
            Case DropDownItems.Frequency.Annual
                loanInput.reyt = 1
                loanInput.month = 12
        End Select

        loanInput.mterm = loan.TERM / loanInput.month
    End Sub

    Private Function IsValidDetails() As Boolean
        Dim errorMessage As New StringBuilder
        Dim loanTypeDetail As Business.Objects.LoanTypeDetail = Common.GetLoanTypeDetail(loanInput.LOAN_TYPE)

        If loanInput.CHKNO_BANK Is Nothing OrElse loanInput.CHKNO_BANK.Trim().Length = 0 Then
            errorMessage.AppendLine("Bank is required.")
        End If

        If loanInput.CHKNO Is Nothing OrElse loanInput.CHKNO.Trim().Length = 0 Then
            errorMessage.AppendLine("Check number is required.")
        End If

        If loanInput.KBCI_NO Is Nothing OrElse loanInput.KBCI_NO.Trim().Length = 0 Then
            errorMessage.AppendLine("KBCI number is required.")
        End If

        If loanInput.PRINCIPAL < loanTypeDetail.MIN OrElse loanInput.PRINCIPAL > loanTypeDetail.MAX Then
            errorMessage.AppendLine(String.Format("Amount range for {0} should be {1} - {2}", loanTypeDetail.LOAN_TYPE, loanTypeDetail.MIN.ToString("#,##0.00"), loanTypeDetail.MAX.ToString("#,##0.00")))
        End If

        If loanInput.COMAKER1 IsNot Nothing AndAlso loanInput.COMAKER1 = loanInput.KBCI_NO Then
            errorMessage.AppendLine("Co-maker 1 and the borrower is the same.")
        End If

        If loanInput.COMAKER2 IsNot Nothing AndAlso loanInput.COMAKER2 = loanInput.KBCI_NO Then
            errorMessage.AppendLine("Co-maker 2 and the borrower is the same.")
        End If

        If loanInput.COMAKER1 IsNot Nothing AndAlso loanInput.COMAKER2 IsNot Nothing AndAlso loanInput.COMAKER1 = loanInput.KBCI_NO = loanInput.COMAKER2 Then
            errorMessage.AppendLine("Co-makers are the same")
        End If

        If errorMessage.Length() <= 0 Then
            GetMember()
            If member Is Nothing Then
                errorMessage.AppendLine("KBCI number is invalid.")
            End If

            If member.MEM_STAT = DropDownItems.MembershipStatus.Retired Then
                If loanInput.LOAN_TYPE = DropDownItems.LoanType.STL AndAlso Common.GetDetails(Sql.Query.Collaterals, loanInput.KBCI_NO).Rows.Count > 0 Then
                    loanInput.resigned = True
                Else
                    errorMessage.AppendLine("Member is already resigned. Only STL is available for members with CTD.")
                End If
            End If

            If DateDiff(DateInterval.Month, member.MEM_DATE, sysdate) < 6 Then
                If Common.PopupQuestion("Borrower membership term left is less than 6 months. Proceed?") = Windows.Forms.DialogResult.No Then
                    errorMessage.AppendLine("Borrower membership term left is less than 6 months.")
                End If
            End If
        End If

        If errorMessage.Length() > 0 Then
            Common.PopupError(errorMessage.ToString())
            Return False
        End If

        Return True
    End Function

    Private Function IsValidLoanAmount() As Boolean
        Common.GetLatestControl()
        If loan.PRINCIPAL + loanInput.xtotamt > loanControl.CEILING Then
            Common.PopupError("You are already over the prescribed Aggregate Ceiling.")
            Return False
        End If

        If loan.PRINCIPAL <= 0 Then
            Common.PopupError("Loan amount must be greater than zero.")
            Return False
        End If

        Return True
    End Function

    Private Function IsValidLoanType() As Boolean
        Dim list As List(Of Business.Objects.Loan) = Common.GetObjectList(Of Business.Objects.Loan)(Sql.Query.MemberLoans, loan.KBCI_NO)

        If loanInput.resigned And loan.LOAN_TYPE <> DropDownItems.LoanType.STL Then
            Common.PopupError("Only STL is available for resigned members.")
            Return False
        End If

        If list.Any(Function(x) x.LOAN_TYPE = DropDownItems.LoanType.RSL AndAlso x.LOAN_STAT = DropDownItems.LoanStatus.Released) Then
            Common.PopupError("Borrower already has an existing Restructured Loan.")
            Return False
        End If

        loan.RENEW = False

        If loan.LOAN_TYPE <> DropDownItems.LoanType.STL AndAlso loan.LOAN_TYPE <> DropDownItems.LoanType.SML Then
            'added, not in original
            loan.L_EXT = False

            If list.Any(Function(x) x.LOAN_TYPE = loan.LOAN_TYPE AndAlso x.LOAN_STAT = DropDownItems.LoanStatus.Released) Then
                Common.PopupInformation("Borrower has existing loan of the same type.")
                loan.RENEW = True
                loanInput.xplnamt = list.Where(Function(x) x.LOAN_TYPE = loan.LOAN_TYPE AndAlso x.LOAN_STAT = DropDownItems.LoanStatus.Released).Select(Function(x) x.PRINCIPAL + x.ACCU_PAYP + x.ARREAR_P + x.ARREAR_I + x.ARREAR_OTH + x.P_BAL + x.I_BAL + x.O_BAL).Sum()
                loanInput.xtotamt = list.Where(Function(x) x.LOAN_TYPE <> DropDownItems.LoanType.STL AndAlso x.LOAN_STAT = DropDownItems.LoanStatus.Released).Select(Function(x) x.PRINCIPAL).Sum()
            End If
        Else
            loanInput.xrenew = False
            loan.L_EXT = False
            'If loan.LOAN_TYPE = DropDownItems.LoanType.SML OrElse Common.PopupQuestion("Renewal of existing " & loan.LOAN_TYPE.ToString() & "?") = MsgBoxResult.Yes Then
            If Common.PopupQuestion("Renewal of existing " & loan.LOAN_TYPE.ToString() & "?") = MsgBoxResult.Yes Then
                loanInput.xrenewSTL = False
                loanInput.xrenew2 = True
                loan.L_EXT = True
            Else
                If member.MEM_STAT <> DropDownItems.MembershipStatus.Staff AndAlso Common.PopupQuestion("Would you like to exempt Share Capital?") = Windows.Forms.DialogResult.Yes Then
                    loanInput.xrenew2 = True
                End If
            End If
        End If

        Return True
    End Function

    Private Function IsValidFrequency() As Boolean
        If loan.LED_TYPE = DropDownItems.LedgerType.OneTimeInterest AndAlso loan.FREQ <> DropDownItems.Frequency.Monthly Then
            Common.PopupError("For 'One Time Interest', frequency should be monthly.")
            Return False
        End If

        SetFrequencyAndTerm()

        Select Case loan.FREQ
            Case DropDownItems.Frequency.Quarterly, DropDownItems.Frequency.SemiAnnual, DropDownItems.Frequency.Annual
                Return IsValidFrequencyAndTerm()
            Case DropDownItems.Frequency.Daily
                If loan.TERM > 30 Then
                    Common.PopupError("Term should not be greater than 30 days.")
                    Return False
                End If
        End Select

        Return True
    End Function

    Private Function IsValidFrequencyAndTerm() As Boolean
        Select Case True
            Case loan.TERM < loanInput.month
                Common.PopupError(String.Format("Term should be at least {0} months.", loanInput.month))
                Return False
            Case loan.TERM Mod loanInput.month > 0
                Common.PopupError(String.Format("Term should be in multiples of {0} months.", loanInput.month))
                Return False
        End Select

        Return True
    End Function

    Private Function IsValidTerm() As Boolean
        If loan.LED_TYPE = DropDownItems.LedgerType.OneTimeInterest AndAlso loan.TERM Mod 12 > 0 Then
            MsgBox("For One Time Deduction, Loan Term should always fall in a full year.", MsgBoxStyle.Exclamation)
            Return False
        ElseIf loan.LOAN_TYPE = DropDownItems.LoanType.SML Then
            Select Case True
                Case loan.PRINCIPAL <= 50000 And loan.TERM > 12
                    MsgBox("For SML loan of up to 50,000, max term is 12.", MsgBoxStyle.Information)
                Case loan.PRINCIPAL >= 110000 And loan.TERM < 12
                    MsgBox("For SML loan of more than 110,000, min term is 12.", MsgBoxStyle.Information)
            End Select
        End If
        Return True
    End Function

    Private Function IsValidInterest() As Boolean
        Dim payStart As Date
        Dim interest As Decimal
        SetFrequencyAndTerm()

        If loan.LOAN_TYPE = DropDownItems.LoanType.BFL Then
            loan.PAY_START = loanControl.BFL_DATE_DUE
        ElseIf loan.MOD_PAY = DropDownItems.PayMode.Payroll Then
            If sysdate.Day <= 15 Then
                payStart = DateAdd(DateInterval.Month, 1, sysdate)
            Else
                payStart = DateAdd(DateInterval.Month, 2, sysdate)
            End If

            payStart = New Date(payStart.Year, payStart.Month, loanControl.PAY_DAY.Day)

            loan.PAY_START = payStart
        Else
            loan.PAY_START = DateAdd(DateInterval.Month, 1, loan.CHKNO_DATE)

            If loan.FREQ = DropDownItems.Frequency.Daily Then
                loan.PAY_START = DateAdd(DateInterval.Day, loan.TERM, loan.CHKNO_DATE)
            End If

            If (loan.TERM = 1 AndAlso loan.FREQ = DropDownItems.Frequency.Monthly) OrElse loan.FREQ = DropDownItems.Frequency.Daily Then
                Select Case True
                    Case loan.PAY_START.DayOfWeek = 1
                        loan.PAY_START = loan.PAY_START.AddDays(1)
                    Case loan.PAY_START.DayOfWeek = 7
                        loan.PAY_START = loan.PAY_START.AddDays(2)
                End Select
            End If
        End If

        If loan.LOAN_TYPE = DropDownItems.LoanType.BFL Then
            loan.DATE_DUE = loanControl.BFL_DATE_DUE
        ElseIf loan.LOAN_TYPE = DropDownItems.LoanType.STL AndAlso ((loan.FREQ = DropDownItems.Frequency.Daily AndAlso loan.TERM >= 30) Or (loan.FREQ = DropDownItems.Frequency.Monthly And loan.TERM = 1)) Then
            loan.DATE_DUE = loan.PAY_START
            Select Case loan.DATE_DUE.DayOfWeek
                Case DayOfWeek.Saturday
                    loan.TERM += 2
                    loan.DATE_DUE = loan.DATE_DUE.AddDays(2)
                Case DayOfWeek.Sunday
                    loan.TERM += 1
                    loan.DATE_DUE = loan.DATE_DUE.AddDays(1)
            End Select
        Else
            loan.DATE_DUE = Common.GoDue(loan.PAY_START, loan.TERM, loan.FREQ)
        End If

        If loan.FREQ = DropDownItems.Frequency.Daily Then
            If loan.TERM < DateDiff(DateInterval.Day, loan.CHKNO_DATE, loan.DATE_DUE) Then
                loan.TERM = DateDiff(DateInterval.Day, loan.CHKNO_DATE, loan.DATE_DUE)
            End If
        End If

        If loan.LED_TYPE <> DropDownItems.LedgerType.OneTimeInterest Then
            loan.AMORT_AMT = Common.Payment(loan.PRINCIPAL, loan.RATE / (100 * loanInput.reyt), loanInput.mterm)

            If loan.FREQ = DropDownItems.Frequency.Monthly AndAlso loan.TERM = 1 Then
                interest = (loan.PRINCIPAL * loan.RATE * DateDiff(DateInterval.Day, loan.CHKNO_DATE, loan.DATE_DUE)) / 36000
                loan.AMORT_AMT = loan.PRINCIPAL + interest
            End If

            If loan.FREQ = DropDownItems.Frequency.Daily Then
                interest = (loan.PRINCIPAL * loan.RATE * loan.TERM) / 36000
                loan.AMORT_AMT = loan.PRINCIPAL + interest
            End If
        End If

        Return True
    End Function

    Private Function GetCollateral() As String
        Dim text As String = String.Empty

        If loanInput.LOAN_TYPE = DropDownItems.LoanType.STL Then
            If collateralSelected.Count > 0 Then
                Dim count As Integer = 0
                text = "CTD#"

                For Each collateral As Business.Objects.Collaterals In collateralSelected
                    count += 1
                    text += String.Format("{0}{1}", IIf(count = 1, " ", "/"), collateral.CTD_NO)
                Next
            ElseIf loanInput.COLLATERAL IsNot Nothing AndAlso loanInput.COLLATERAL.StartsWith("CTD#") Then
                text = loanInput.COLLATERAL
            End If
        Else
            text = loanInput.COLLATERAL
        End If

        Return text
    End Function

End Class