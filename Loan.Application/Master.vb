Imports System.Collections.Generic
Imports System.Configuration
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System.Text
Imports Loan.Application.Infrastructure.Forms.Popups
Imports Loan.Application.Infrastructure.Enumerations
Imports Loan.Application.Report

Public Class Master
    Inherits Loan.Application.Infrastructure.Forms.Windows.BaseForm

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        SetMenu = AddressOf SetMenuMethod
        SetMenuAfterClosing = AddressOf SetMenuAfterClosingMethod
        SetMenuAfterOpening = AddressOf SetMenuAfterOpeningMethod
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents mnuL As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLN As System.Windows.Forms.MenuItem
    Friend WithEvents mnuO As System.Windows.Forms.MenuItem
    Friend WithEvents mnuP As System.Windows.Forms.MenuItem
    Friend WithEvents mnuM As System.Windows.Forms.MenuItem
    Friend WithEvents mnuA As System.Windows.Forms.MenuItem
    Friend WithEvents mnuC As System.Windows.Forms.MenuItem
    Friend WithEvents mnuExit As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLP As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLA As System.Windows.Forms.MenuItem
    Friend WithEvents parentMenu As System.Windows.Forms.MainMenu
    Friend WithEvents mnuLS As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLV As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLR As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLF As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem18 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem19 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem21 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem22 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLE As System.Windows.Forms.MenuItem
    Friend WithEvents mnuON As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOR As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem33 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem34 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOL As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPG As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPP As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPR As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPA As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPD As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem43 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem44 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuML As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMM As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMP As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMC As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMO As System.Windows.Forms.MenuItem
    Friend WithEvents mnuME As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMW As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMA As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem56 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAR As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAE As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAA As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAI As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRD As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRP As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRA As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRR As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRF As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRV As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRN As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRS As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRM As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRL As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRC As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRE As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRT As System.Windows.Forms.MenuItem
    Friend WithEvents mnuORM As System.Windows.Forms.MenuItem
    Friend WithEvents mnuORD As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOCC As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOCD As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOCE As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOCF As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOCG As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOCH As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOCI As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOCJ As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOCK As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOCL As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOCM As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOCN As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem94 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem95 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPRD As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPRN As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLRU As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPRV As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARD As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARDD As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARDR As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARDF As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARDV As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARM As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARME As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARMC As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARMP As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARMT As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAIE As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAII As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem115 As System.Windows.Forms.MenuItem
    Friend WithEvents opd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents parentStatus As System.Windows.Forms.StatusStrip
    Friend WithEvents tsslSYSDATE As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnuPV As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPO As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPOR As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPOV As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARA As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARAM As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARAR As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARAU As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARAP As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMB As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMR As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARAT As System.Windows.Forms.MenuItem
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAIP As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAF As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAFP As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAIT As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAFT As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARAA As System.Windows.Forms.MenuItem
    Friend WithEvents fbd As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents mnuPGP As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPGV As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPGC As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPGX As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPGF As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPGT As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPGO As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPGN As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMD As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAIB As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARMB As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARMO As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem116 As System.Windows.Forms.MenuItem

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Master))
        Me.parentMenu = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuL = New System.Windows.Forms.MenuItem
        Me.mnuLN = New System.Windows.Forms.MenuItem
        Me.MenuItem18 = New System.Windows.Forms.MenuItem
        Me.mnuLP = New System.Windows.Forms.MenuItem
        Me.MenuItem19 = New System.Windows.Forms.MenuItem
        Me.mnuLA = New System.Windows.Forms.MenuItem
        Me.mnuLE = New System.Windows.Forms.MenuItem
        Me.mnuLS = New System.Windows.Forms.MenuItem
        Me.mnuLV = New System.Windows.Forms.MenuItem
        Me.MenuItem22 = New System.Windows.Forms.MenuItem
        Me.mnuLR = New System.Windows.Forms.MenuItem
        Me.mnuLRA = New System.Windows.Forms.MenuItem
        Me.mnuLRD = New System.Windows.Forms.MenuItem
        Me.mnuLRF = New System.Windows.Forms.MenuItem
        Me.mnuLRE = New System.Windows.Forms.MenuItem
        Me.mnuLRU = New System.Windows.Forms.MenuItem
        Me.mnuLRL = New System.Windows.Forms.MenuItem
        Me.mnuLRP = New System.Windows.Forms.MenuItem
        Me.mnuLRV = New System.Windows.Forms.MenuItem
        Me.mnuLRC = New System.Windows.Forms.MenuItem
        Me.mnuLRM = New System.Windows.Forms.MenuItem
        Me.mnuLRR = New System.Windows.Forms.MenuItem
        Me.mnuLRS = New System.Windows.Forms.MenuItem
        Me.mnuLRN = New System.Windows.Forms.MenuItem
        Me.mnuLRT = New System.Windows.Forms.MenuItem
        Me.MenuItem21 = New System.Windows.Forms.MenuItem
        Me.mnuLF = New System.Windows.Forms.MenuItem
        Me.mnuO = New System.Windows.Forms.MenuItem
        Me.mnuON = New System.Windows.Forms.MenuItem
        Me.MenuItem33 = New System.Windows.Forms.MenuItem
        Me.mnuOR = New System.Windows.Forms.MenuItem
        Me.mnuORM = New System.Windows.Forms.MenuItem
        Me.mnuORD = New System.Windows.Forms.MenuItem
        Me.mnuOCC = New System.Windows.Forms.MenuItem
        Me.mnuOCD = New System.Windows.Forms.MenuItem
        Me.mnuOCE = New System.Windows.Forms.MenuItem
        Me.MenuItem94 = New System.Windows.Forms.MenuItem
        Me.mnuOCF = New System.Windows.Forms.MenuItem
        Me.MenuItem95 = New System.Windows.Forms.MenuItem
        Me.mnuOCG = New System.Windows.Forms.MenuItem
        Me.mnuOCH = New System.Windows.Forms.MenuItem
        Me.mnuOCI = New System.Windows.Forms.MenuItem
        Me.mnuOCJ = New System.Windows.Forms.MenuItem
        Me.mnuOCK = New System.Windows.Forms.MenuItem
        Me.mnuOCL = New System.Windows.Forms.MenuItem
        Me.mnuOCM = New System.Windows.Forms.MenuItem
        Me.mnuOCN = New System.Windows.Forms.MenuItem
        Me.MenuItem34 = New System.Windows.Forms.MenuItem
        Me.mnuOL = New System.Windows.Forms.MenuItem
        Me.mnuP = New System.Windows.Forms.MenuItem
        Me.mnuPG = New System.Windows.Forms.MenuItem
        Me.mnuPGP = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.mnuPGV = New System.Windows.Forms.MenuItem
        Me.mnuPGC = New System.Windows.Forms.MenuItem
        Me.mnuPGX = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.mnuPGF = New System.Windows.Forms.MenuItem
        Me.mnuPGO = New System.Windows.Forms.MenuItem
        Me.mnuPGT = New System.Windows.Forms.MenuItem
        Me.MenuItem7 = New System.Windows.Forms.MenuItem
        Me.mnuPGN = New System.Windows.Forms.MenuItem
        Me.mnuPP = New System.Windows.Forms.MenuItem
        Me.mnuPR = New System.Windows.Forms.MenuItem
        Me.mnuPRD = New System.Windows.Forms.MenuItem
        Me.mnuPRN = New System.Windows.Forms.MenuItem
        Me.mnuPRV = New System.Windows.Forms.MenuItem
        Me.MenuItem43 = New System.Windows.Forms.MenuItem
        Me.mnuPO = New System.Windows.Forms.MenuItem
        Me.mnuPD = New System.Windows.Forms.MenuItem
        Me.mnuPOR = New System.Windows.Forms.MenuItem
        Me.mnuPOV = New System.Windows.Forms.MenuItem
        Me.MenuItem44 = New System.Windows.Forms.MenuItem
        Me.mnuPA = New System.Windows.Forms.MenuItem
        Me.mnuPV = New System.Windows.Forms.MenuItem
        Me.mnuM = New System.Windows.Forms.MenuItem
        Me.mnuML = New System.Windows.Forms.MenuItem
        Me.mnuMM = New System.Windows.Forms.MenuItem
        Me.mnuMP = New System.Windows.Forms.MenuItem
        Me.mnuMC = New System.Windows.Forms.MenuItem
        Me.mnuMO = New System.Windows.Forms.MenuItem
        Me.mnuME = New System.Windows.Forms.MenuItem
        Me.mnuMW = New System.Windows.Forms.MenuItem
        Me.mnuMD = New System.Windows.Forms.MenuItem
        Me.MenuItem56 = New System.Windows.Forms.MenuItem
        Me.mnuMB = New System.Windows.Forms.MenuItem
        Me.mnuMR = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.mnuMA = New System.Windows.Forms.MenuItem
        Me.mnuA = New System.Windows.Forms.MenuItem
        Me.mnuAR = New System.Windows.Forms.MenuItem
        Me.mnuARD = New System.Windows.Forms.MenuItem
        Me.mnuARDD = New System.Windows.Forms.MenuItem
        Me.mnuARDR = New System.Windows.Forms.MenuItem
        Me.mnuARDF = New System.Windows.Forms.MenuItem
        Me.mnuARDV = New System.Windows.Forms.MenuItem
        Me.mnuARM = New System.Windows.Forms.MenuItem
        Me.mnuARME = New System.Windows.Forms.MenuItem
        Me.MenuItem115 = New System.Windows.Forms.MenuItem
        Me.mnuARMC = New System.Windows.Forms.MenuItem
        Me.mnuARMP = New System.Windows.Forms.MenuItem
        Me.mnuARMB = New System.Windows.Forms.MenuItem
        Me.MenuItem116 = New System.Windows.Forms.MenuItem
        Me.mnuARMT = New System.Windows.Forms.MenuItem
        Me.mnuARA = New System.Windows.Forms.MenuItem
        Me.mnuARAA = New System.Windows.Forms.MenuItem
        Me.mnuARAM = New System.Windows.Forms.MenuItem
        Me.mnuARAP = New System.Windows.Forms.MenuItem
        Me.mnuARAU = New System.Windows.Forms.MenuItem
        Me.mnuARAR = New System.Windows.Forms.MenuItem
        Me.mnuARAT = New System.Windows.Forms.MenuItem
        Me.mnuAI = New System.Windows.Forms.MenuItem
        Me.mnuAIE = New System.Windows.Forms.MenuItem
        Me.mnuAII = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mnuAIP = New System.Windows.Forms.MenuItem
        Me.mnuAIB = New System.Windows.Forms.MenuItem
        Me.mnuAIT = New System.Windows.Forms.MenuItem
        Me.mnuAF = New System.Windows.Forms.MenuItem
        Me.mnuAFP = New System.Windows.Forms.MenuItem
        Me.mnuAFT = New System.Windows.Forms.MenuItem
        Me.mnuAE = New System.Windows.Forms.MenuItem
        Me.mnuAA = New System.Windows.Forms.MenuItem
        Me.mnuC = New System.Windows.Forms.MenuItem
        Me.mnuExit = New System.Windows.Forms.MenuItem
        Me.opd = New System.Windows.Forms.OpenFileDialog
        Me.parentStatus = New System.Windows.Forms.StatusStrip
        Me.tsslSYSDATE = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.fbd = New System.Windows.Forms.FolderBrowserDialog
        Me.mnuARMO = New System.Windows.Forms.MenuItem
        Me.parentStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'parentMenu
        '
        Me.parentMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuL, Me.mnuO, Me.mnuP, Me.mnuM, Me.mnuA, Me.mnuC, Me.mnuExit})
        '
        'mnuL
        '
        Me.mnuL.Index = 0
        Me.mnuL.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuLN, Me.MenuItem18, Me.mnuLP, Me.MenuItem19, Me.mnuLA, Me.mnuLE, Me.mnuLS, Me.mnuLV, Me.MenuItem22, Me.mnuLR, Me.MenuItem21, Me.mnuLF})
        Me.mnuL.MergeType = System.Windows.Forms.MenuMerge.Remove
        Me.mnuL.Text = "&Loans"
        '
        'mnuLN
        '
        Me.mnuLN.Index = 0
        Me.mnuLN.MdiList = True
        Me.mnuLN.Text = "&New Loans"
        '
        'MenuItem18
        '
        Me.MenuItem18.Index = 1
        Me.MenuItem18.Text = "-"
        '
        'mnuLP
        '
        Me.mnuLP.Index = 2
        Me.mnuLP.MdiList = True
        Me.mnuLP.Text = "Loans &Payment"
        '
        'MenuItem19
        '
        Me.MenuItem19.Index = 3
        Me.MenuItem19.Text = "-"
        '
        'mnuLA
        '
        Me.mnuLA.Index = 4
        Me.mnuLA.MdiList = True
        Me.mnuLA.Text = "Loan &Adjusting Entries"
        '
        'mnuLE
        '
        Me.mnuLE.Index = 5
        Me.mnuLE.Text = "Loan R&epacking"
        Me.mnuLE.Visible = False
        '
        'mnuLS
        '
        Me.mnuLS.Index = 6
        Me.mnuLS.Text = "Loan Re&structuring"
        '
        'mnuLV
        '
        Me.mnuLV.Index = 7
        Me.mnuLV.MdiList = True
        Me.mnuLV.Text = "Loan Re&version"
        '
        'MenuItem22
        '
        Me.MenuItem22.Index = 8
        Me.MenuItem22.Text = "-"
        '
        'mnuLR
        '
        Me.mnuLR.Index = 9
        Me.mnuLR.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuLRA, Me.mnuLRD, Me.mnuLRF, Me.mnuLRE, Me.mnuLRU, Me.mnuLRL, Me.mnuLRP, Me.mnuLRV, Me.mnuLRC, Me.mnuLRM, Me.mnuLRR, Me.mnuLRS, Me.mnuLRN, Me.mnuLRT})
        Me.mnuLR.Text = "&Report Options"
        '
        'mnuLRA
        '
        Me.mnuLRA.Index = 0
        Me.mnuLRA.Text = "&Amortization Schedule"
        '
        'mnuLRD
        '
        Me.mnuLRD.Index = 1
        Me.mnuLRD.Text = "&Due Payment Order"
        '
        'mnuLRF
        '
        Me.mnuLRF.Index = 2
        Me.mnuLRF.Text = "&Fully Paid Loans"
        '
        'mnuLRE
        '
        Me.mnuLRE.Index = 3
        Me.mnuLRE.Text = "Loan Arr&ears"
        '
        'mnuLRU
        '
        Me.mnuLRU.Index = 4
        Me.mnuLRU.Text = "Loan D&ue"
        '
        'mnuLRL
        '
        Me.mnuLRL.Index = 5
        Me.mnuLRL.Text = "Loan &Ledger"
        '
        'mnuLRP
        '
        Me.mnuLRP.Index = 6
        Me.mnuLRP.Text = "Loan &Payments"
        '
        'mnuLRV
        '
        Me.mnuLRV.Index = 7
        Me.mnuLRV.Text = "Loan Release &Voucher"
        '
        'mnuLRC
        '
        Me.mnuLRC.Index = 8
        Me.mnuLRC.Text = "Loan Transaction S&chedule"
        '
        'mnuLRM
        '
        Me.mnuLRM.Index = 9
        Me.mnuLRM.Text = "Pre-Ter&minated Loans"
        '
        'mnuLRR
        '
        Me.mnuLRR.Index = 10
        Me.mnuLRR.Text = "&Released Loans"
        '
        'mnuLRS
        '
        Me.mnuLRS.Index = 11
        Me.mnuLRS.Text = "Re&structured Loans"
        '
        'mnuLRN
        '
        Me.mnuLRN.Index = 12
        Me.mnuLRN.Text = "SA Tra&nsactions"
        '
        'mnuLRT
        '
        Me.mnuLRT.Index = 13
        Me.mnuLRT.Text = "Schedule for S&taff"
        '
        'MenuItem21
        '
        Me.MenuItem21.Index = 10
        Me.MenuItem21.Text = "-"
        '
        'mnuLF
        '
        Me.mnuLF.Index = 11
        Me.mnuLF.Text = "Sta&ff Loan Payments"
        '
        'mnuO
        '
        Me.mnuO.Index = 1
        Me.mnuO.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuON, Me.MenuItem33, Me.mnuOR, Me.MenuItem34, Me.mnuOL})
        Me.mnuO.MergeType = System.Windows.Forms.MenuMerge.Remove
        Me.mnuO.Text = "&Others"
        '
        'mnuON
        '
        Me.mnuON.Index = 0
        Me.mnuON.Text = "&New Members"
        '
        'MenuItem33
        '
        Me.MenuItem33.Index = 1
        Me.MenuItem33.Text = "-"
        '
        'mnuOR
        '
        Me.mnuOR.Index = 2
        Me.mnuOR.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuORM, Me.mnuORD, Me.mnuOCC, Me.mnuOCD, Me.mnuOCE, Me.MenuItem94, Me.mnuOCF, Me.MenuItem95, Me.mnuOCG, Me.mnuOCH, Me.mnuOCI, Me.mnuOCJ, Me.mnuOCK, Me.mnuOCL, Me.mnuOCM, Me.mnuOCN})
        Me.mnuOR.Text = "&Report Options"
        '
        'mnuORM
        '
        Me.mnuORM.Index = 0
        Me.mnuORM.Text = "&Members Listing"
        '
        'mnuORD
        '
        Me.mnuORD.Index = 1
        Me.mnuORD.Text = "Cash &Dividend Register"
        '
        'mnuOCC
        '
        Me.mnuOCC.Index = 2
        Me.mnuOCC.Text = "&C - Print Pat. Ref. Register"
        Me.mnuOCC.Visible = False
        '
        'mnuOCD
        '
        Me.mnuOCD.Index = 3
        Me.mnuOCD.Text = "&D - Print Div/Ref Slips"
        Me.mnuOCD.Visible = False
        '
        'mnuOCE
        '
        Me.mnuOCE.Index = 4
        Me.mnuOCE.Text = "&E - Print Div/Ref Registers"
        Me.mnuOCE.Visible = False
        '
        'MenuItem94
        '
        Me.MenuItem94.Index = 5
        Me.MenuItem94.Text = "-"
        Me.MenuItem94.Visible = False
        '
        'mnuOCF
        '
        Me.mnuOCF.Index = 6
        Me.mnuOCF.Text = "&F - Print Registers for KBCI No"
        Me.mnuOCF.Visible = False
        '
        'MenuItem95
        '
        Me.MenuItem95.Index = 7
        Me.MenuItem95.Text = "-"
        Me.MenuItem95.Visible = False
        '
        'mnuOCG
        '
        Me.mnuOCG.Index = 8
        Me.mnuOCG.Text = "&G - Print FD Certificate"
        Me.mnuOCG.Visible = False
        '
        'mnuOCH
        '
        Me.mnuOCH.Index = 9
        Me.mnuOCH.Text = "&H - Print FD Ledger"
        Me.mnuOCH.Visible = False
        '
        'mnuOCI
        '
        Me.mnuOCI.Index = 10
        Me.mnuOCI.Text = "&I - Print FD Listing"
        Me.mnuOCI.Visible = False
        '
        'mnuOCJ
        '
        Me.mnuOCJ.Index = 11
        Me.mnuOCJ.Text = "&J - Print FD Schedule"
        Me.mnuOCJ.Visible = False
        '
        'mnuOCK
        '
        Me.mnuOCK.Index = 12
        Me.mnuOCK.Text = "&K - Print FD Register"
        Me.mnuOCK.Visible = False
        '
        'mnuOCL
        '
        Me.mnuOCL.Index = 13
        Me.mnuOCL.Text = "&L - Print FD Cert. Register"
        Me.mnuOCL.Visible = False
        '
        'mnuOCM
        '
        Me.mnuOCM.Index = 14
        Me.mnuOCM.Text = "&M - FDs for Issuance of Cert"
        Me.mnuOCM.Visible = False
        '
        'mnuOCN
        '
        Me.mnuOCN.Index = 15
        Me.mnuOCN.Text = "&N - List of Resigned Mem/Inv"
        Me.mnuOCN.Visible = False
        '
        'MenuItem34
        '
        Me.MenuItem34.Index = 3
        Me.MenuItem34.Text = "-"
        '
        'mnuOL
        '
        Me.mnuOL.Index = 4
        Me.mnuOL.Text = "&LRI Maintenance"
        '
        'mnuP
        '
        Me.mnuP.Index = 2
        Me.mnuP.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuPG, Me.mnuPP, Me.mnuPR, Me.MenuItem43, Me.mnuPO, Me.mnuPD, Me.MenuItem44, Me.mnuPA, Me.mnuPV})
        Me.mnuP.MergeType = System.Windows.Forms.MenuMerge.Remove
        Me.mnuP.Text = "&Payroll"
        '
        'mnuPG
        '
        Me.mnuPG.Index = 0
        Me.mnuPG.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuPGP, Me.MenuItem4, Me.mnuPGV, Me.mnuPGC, Me.mnuPGX, Me.MenuItem5, Me.mnuPGF, Me.mnuPGO, Me.mnuPGT, Me.MenuItem7, Me.mnuPGN})
        Me.mnuPG.Text = "&Generate Payroll Diskette"
        '
        'mnuPGP
        '
        Me.mnuPGP.Index = 0
        Me.mnuPGP.Text = "&Process New Advice"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 1
        Me.MenuItem4.Text = "-"
        '
        'mnuPGV
        '
        Me.mnuPGV.Index = 2
        Me.mnuPGV.Text = "&View Advice"
        '
        'mnuPGC
        '
        Me.mnuPGC.Index = 3
        Me.mnuPGC.Text = "Generate &Consolidated"
        '
        'mnuPGX
        '
        Me.mnuPGX.Index = 4
        Me.mnuPGX.Text = "Generate E&XTKBC"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 5
        Me.MenuItem5.Text = "-"
        '
        'mnuPGF
        '
        Me.mnuPGF.Index = 6
        Me.mnuPGF.Text = "View O&ffcycle Advice"
        '
        'mnuPGO
        '
        Me.mnuPGO.Index = 7
        Me.mnuPGO.Text = "Generate Offcycle C&onsolidated"
        '
        'mnuPGT
        '
        Me.mnuPGT.Index = 8
        Me.mnuPGT.Text = "Generate Offcycle EX&TKBC"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 9
        Me.MenuItem7.Text = "-"
        '
        'mnuPGN
        '
        Me.mnuPGN.Index = 10
        Me.mnuPGN.Text = "Generate Offcycle Co&nsolidated EXTKBC"
        '
        'mnuPP
        '
        Me.mnuPP.Index = 1
        Me.mnuPP.Text = "&Process Payroll"
        '
        'mnuPR
        '
        Me.mnuPR.Index = 2
        Me.mnuPR.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuPRD, Me.mnuPRN, Me.mnuPRV})
        Me.mnuPR.Text = "Payroll Deduction &Report"
        '
        'mnuPRD
        '
        Me.mnuPRD.Index = 0
        Me.mnuPRD.Text = "Payroll &Deduction Register"
        '
        'mnuPRN
        '
        Me.mnuPRN.Index = 1
        Me.mnuPRN.Text = "&No Deduction Register"
        '
        'mnuPRV
        '
        Me.mnuPRV.Index = 2
        Me.mnuPRV.Text = "Payroll &Voucher"
        '
        'MenuItem43
        '
        Me.MenuItem43.Index = 3
        Me.MenuItem43.Text = "-"
        '
        'mnuPO
        '
        Me.mnuPO.Index = 4
        Me.mnuPO.Text = "Process &Other Deductions"
        '
        'mnuPD
        '
        Me.mnuPD.Index = 5
        Me.mnuPD.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuPOR, Me.mnuPOV})
        Me.mnuPD.Text = "Other &Deduction Report"
        '
        'mnuPOR
        '
        Me.mnuPOR.Index = 0
        Me.mnuPOR.Text = "Payroll Deduction &Register"
        '
        'mnuPOV
        '
        Me.mnuPOV.Index = 1
        Me.mnuPOV.Text = "Payroll Deduction &Voucher"
        '
        'MenuItem44
        '
        Me.MenuItem44.Index = 6
        Me.MenuItem44.Text = "-"
        '
        'mnuPA
        '
        Me.mnuPA.Index = 7
        Me.mnuPA.Text = "&Advance Payments Maintenance"
        '
        'mnuPV
        '
        Me.mnuPV.Index = 8
        Me.mnuPV.Text = "Ad&vance Payment Register"
        '
        'mnuM
        '
        Me.mnuM.Index = 3
        Me.mnuM.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuML, Me.mnuMM, Me.mnuMP, Me.mnuMC, Me.mnuMO, Me.mnuME, Me.mnuMW, Me.mnuMD, Me.MenuItem56, Me.mnuMB, Me.mnuMR, Me.MenuItem3, Me.mnuMA})
        Me.mnuM.MergeType = System.Windows.Forms.MenuMerge.Remove
        Me.mnuM.Text = "&Maintenance"
        '
        'mnuML
        '
        Me.mnuML.Index = 0
        Me.mnuML.Text = "&Loans"
        '
        'mnuMM
        '
        Me.mnuMM.Index = 1
        Me.mnuMM.Text = "&Members"
        '
        'mnuMP
        '
        Me.mnuMP.Index = 2
        Me.mnuMP.Text = "&Parameters"
        '
        'mnuMC
        '
        Me.mnuMC.Index = 3
        Me.mnuMC.Text = "&Control"
        '
        'mnuMO
        '
        Me.mnuMO.Index = 4
        Me.mnuMO.Text = "&OR Payments"
        '
        'mnuME
        '
        Me.mnuME.Index = 5
        Me.mnuME.Text = "L&edger"
        '
        'mnuMW
        '
        Me.mnuMW.Index = 6
        Me.mnuMW.Text = "Pass&words"
        '
        'mnuMD
        '
        Me.mnuMD.Index = 7
        Me.mnuMD.Text = "Payroll &Deductions"
        '
        'MenuItem56
        '
        Me.MenuItem56.Index = 8
        Me.MenuItem56.Text = "-"
        '
        'mnuMB
        '
        Me.mnuMB.Index = 9
        Me.mnuMB.Text = "&Backup Database"
        '
        'mnuMR
        '
        Me.mnuMR.Index = 10
        Me.mnuMR.Text = "&Restore Database"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 11
        Me.MenuItem3.Text = "-"
        '
        'mnuMA
        '
        Me.mnuMA.Index = 12
        Me.mnuMA.Text = "&Account Monitoring"
        '
        'mnuA
        '
        Me.mnuA.Index = 4
        Me.mnuA.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAR, Me.mnuAI, Me.mnuAF, Me.mnuAE, Me.mnuAA})
        Me.mnuA.MergeType = System.Windows.Forms.MenuMerge.Remove
        Me.mnuA.Text = "&Admin"
        '
        'mnuAR
        '
        Me.mnuAR.Index = 0
        Me.mnuAR.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuARD, Me.mnuARM, Me.mnuARA})
        Me.mnuAR.Text = "&Reports"
        '
        'mnuARD
        '
        Me.mnuARD.Index = 0
        Me.mnuARD.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuARDD, Me.mnuARDR, Me.mnuARDF, Me.mnuARDV})
        Me.mnuARD.Text = "End of &Day Reports"
        '
        'mnuARDD
        '
        Me.mnuARDD.Index = 0
        Me.mnuARDD.Text = "&Daily Transaction Register"
        '
        'mnuARDR
        '
        Me.mnuARDR.Index = 1
        Me.mnuARDR.Text = "List of &Released Loans"
        '
        'mnuARDF
        '
        Me.mnuARDF.Index = 2
        Me.mnuARDF.Text = "List of &Fully Paid Loans"
        '
        'mnuARDV
        '
        Me.mnuARDV.Index = 3
        Me.mnuARDV.Text = "Re&versed Transaction Register"
        '
        'mnuARM
        '
        Me.mnuARM.Index = 1
        Me.mnuARM.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuARME, Me.MenuItem115, Me.mnuARMC, Me.mnuARMP, Me.mnuARMB, Me.mnuARMO, Me.MenuItem116, Me.mnuARMT})
        Me.mnuARM.Text = "End of &Month Reports"
        '
        'mnuARME
        '
        Me.mnuARME.Enabled = False
        Me.mnuARME.Index = 0
        Me.mnuARME.Text = "&Extract of Monthly Runup"
        '
        'MenuItem115
        '
        Me.MenuItem115.Index = 1
        Me.MenuItem115.Text = "-"
        '
        'mnuARMC
        '
        Me.mnuARMC.Index = 2
        Me.mnuARMC.Text = "Monthly Runup - &Current"
        '
        'mnuARMP
        '
        Me.mnuARMP.Index = 3
        Me.mnuARMP.Text = "Monthly Runup - &Past Due"
        '
        'mnuARMB
        '
        Me.mnuARMB.Index = 4
        Me.mnuARMB.Text = "Monthly Runup - &Breakdown"
        '
        'MenuItem116
        '
        Me.MenuItem116.Index = 6
        Me.MenuItem116.Text = "-"
        '
        'mnuARMT
        '
        Me.mnuARMT.Enabled = False
        Me.mnuARMT.Index = 7
        Me.mnuARMT.Text = "Monthly &Transaction"
        '
        'mnuARA
        '
        Me.mnuARA.Index = 2
        Me.mnuARA.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuARAA, Me.mnuARAM, Me.mnuARAP, Me.mnuARAU, Me.mnuARAR, Me.mnuARAT})
        Me.mnuARA.Text = "&Adhoc"
        '
        'mnuARAA
        '
        Me.mnuARAA.Index = 0
        Me.mnuARAA.Text = "&Arrears"
        '
        'mnuARAM
        '
        Me.mnuARAM.Index = 1
        Me.mnuARAM.Text = "&Miscellaneous Liabilities"
        '
        'mnuARAP
        '
        Me.mnuARAP.Index = 2
        Me.mnuARAP.Text = "Missed &Payments"
        '
        'mnuARAU
        '
        Me.mnuARAU.Index = 3
        Me.mnuARAU.Text = "R&unup"
        '
        'mnuARAR
        '
        Me.mnuARAR.Index = 4
        Me.mnuARAR.Text = "&Refund/Remit"
        '
        'mnuARAT
        '
        Me.mnuARAT.Index = 5
        Me.mnuARAT.Text = "&Total Exposure"
        '
        'mnuAI
        '
        Me.mnuAI.Index = 1
        Me.mnuAI.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAIE, Me.mnuAII, Me.MenuItem1, Me.mnuAIP, Me.mnuAIB, Me.mnuAIT})
        Me.mnuAI.Text = "&Interest Reports"
        '
        'mnuAIE
        '
        Me.mnuAIE.Index = 0
        Me.mnuAIE.Text = "&Extract Interest"
        '
        'mnuAII
        '
        Me.mnuAII.Index = 1
        Me.mnuAII.Text = "Print &Interest Reports"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 2
        Me.MenuItem1.Text = "-"
        '
        'mnuAIP
        '
        Me.mnuAIP.Index = 3
        Me.mnuAIP.Text = "Staff &PFL Interest Report"
        '
        'mnuAIB
        '
        Me.mnuAIB.Index = 4
        Me.mnuAIB.Text = "Staff PFL &Balance Report"
        '
        'mnuAIT
        '
        Me.mnuAIT.Index = 5
        Me.mnuAIT.Text = "&Total Interest Paid"
        '
        'mnuAF
        '
        Me.mnuAF.Index = 2
        Me.mnuAF.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAFP, Me.mnuAFT})
        Me.mnuAF.Text = "&Foxpro Reports"
        '
        'mnuAFP
        '
        Me.mnuAFP.Index = 0
        Me.mnuAFP.Text = "Staff &PFL Interest Report"
        '
        'mnuAFT
        '
        Me.mnuAFT.Index = 1
        Me.mnuAFT.Text = "&Total Interest Paid"
        '
        'mnuAE
        '
        Me.mnuAE.Index = 3
        Me.mnuAE.Text = "&Execute End of Day"
        '
        'mnuAA
        '
        Me.mnuAA.Index = 4
        Me.mnuAA.Text = "&Archive Loan Records"
        Me.mnuAA.Visible = False
        '
        'mnuC
        '
        Me.mnuC.Index = 5
        Me.mnuC.MergeType = System.Windows.Forms.MenuMerge.Remove
        Me.mnuC.Text = "&Close"
        '
        'mnuExit
        '
        Me.mnuExit.Index = 6
        Me.mnuExit.MergeType = System.Windows.Forms.MenuMerge.Remove
        Me.mnuExit.Text = "E&xit"
        '
        'parentStatus
        '
        Me.parentStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslSYSDATE, Me.ToolStripStatusLabel1})
        Me.parentStatus.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.parentStatus.Location = New System.Drawing.Point(0, 48)
        Me.parentStatus.Name = "parentStatus"
        Me.parentStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.parentStatus.Size = New System.Drawing.Size(521, 22)
        Me.parentStatus.TabIndex = 0
        Me.parentStatus.Text = "StatusStrip1"
        '
        'tsslSYSDATE
        '
        Me.tsslSYSDATE.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslSYSDATE.Name = "tsslSYSDATE"
        Me.tsslSYSDATE.Size = New System.Drawing.Size(55, 17)
        Me.tsslSYSDATE.Text = "SYSDATE"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(61, 17)
        Me.ToolStripStatusLabel1.Text = "SYSDATE"
        '
        'mnuARMO
        '
        Me.mnuARMO.Index = 5
        Me.mnuARMO.Text = "Monthly Runup - C&onsolidation"
        '
        'Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackgroundImage = Global.Loan.Application.My.Resources.Resources.wmark2
        Me.ClientSize = New System.Drawing.Size(521, 70)
        Me.Controls.Add(Me.parentStatus)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.Menu = Me.parentMenu
        Me.Name = "Master"
        Me.Text = "KBCI Loan System"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.parentStatus.ResumeLayout(False)
        Me.parentStatus.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Variables"

    Dim intPrint As Integer
    Dim intSort As Integer
    Dim intContent As Integer
    Dim intFilter As Integer

    Dim intOption1 As Integer
    Dim intOption2 As Integer

    Dim strKBCI As String
    Dim strKBCIStart As String
    Dim strKBCIEnd As String

    Dim strDateStart As String
    Dim strDateEnd As String

    Dim strPN As String
    Dim strPNStart As String
    Dim strPNEnd As String

    Dim strInput As String

    Delegate Sub SetMenuDelegate(ByVal enable As Boolean)
    Delegate Sub SetMenuAfterClosingDelegate()
    Delegate Sub SetMenuAfterOpeningDelegate()

    Public SetMenu As SetMenuDelegate
    Public SetMenuAfterClosing As SetMenuAfterClosingDelegate
    Public SetMenuAfterOpening As SetMenuAfterOpeningDelegate
#End Region

#Region "Options"

    Private Sub PNOptions()
        strInput = InputBox("Selective Printing Per PN Num")
    End Sub

    Private Sub PrinterOptions()
        intPrint = Common.PopupOptions("Output to Printer", "Output to File", "Cancel")
    End Sub

    Private Sub SortOptions()
        intContent = Common.PopupOptions("Arrange by KBCI num", "Arrange by Region", "Sort Options:")
    End Sub

    Private Sub ContentOptions()
        intContent = Common.PopupOptions("Summary", "Details", "Cancel")
    End Sub

    Private Sub FilterOptions()
        intSort = Common.PopupOptions("Active Members", "Resigned Members", "KBCI Staff", "Cancel")
    End Sub

#End Region

#Region "Menu"

#Region "Loans"

    Private Sub mnuLN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLN.Click
        If Common.PopupQuestion("Use New?") = Windows.Forms.DialogResult.Yes Then
            Dim form As New LoanRelease()
            form.Show()
        Else
            Dim form As New LoanApplicationOld()
            form.Show()
        End If
    End Sub

    Private Sub mnuLP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLP.Click
        Dim form As New LoanPaymentOld
        form.Show()
        form = Nothing
    End Sub

    Private Sub mnuLA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLA.Click
        Dim form As New LoanAdjustingEntries
        form.Show()
        form = Nothing
    End Sub

    Private Sub mnuLS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLS.Click
        Dim form As New LoanRestructuring
        form.Show()
        form = Nothing
    End Sub

    Private Sub mnuLV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLV.Click
        Dim form As New LoanReversion
        form.Show()
        form = Nothing
    End Sub

    Private Sub mnuLRD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRD.Click
        Dim loan As Business.Objects.Loan = Common.GetLoan()
        If loan IsNot Nothing Then
            Common.OpenReport(Of PaymentOrder.Loans)(loan.PN_NO)
        End If
    End Sub

    Private Sub mnuLRP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRP.Click
        Dim loan As Business.Objects.Loan = Common.GetLoan()
        If loan IsNot Nothing Then
            Common.OpenReport(Of Loans.PaymentRegister)(loan.PN_NO)
        End If
    End Sub

    Private Sub mnuLRA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRA.Click
        Dim loan As Business.Objects.Loan = Common.GetLoan()
        If loan IsNot Nothing Then
            Common.OpenReport(Of Loans.AmortizationSchedule)(loan.PN_NO)
        End If
    End Sub

    Private Sub mnuLRR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRR.Click
        Dim popup As New Business.Popups.DateRange()
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Loans.ReleasedLoans)(popup.DateFrom, popup.DateTo)
    End Sub

    Private Sub mnuLRF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRF.Click
        Dim popup As New Business.Popups.DateRange()
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Dim intX As Integer = Common.PopupOptions("All fully paid loans", "Loans for stoppage", "Cancel", "")
        Common.OpenReport(Of Loans.FullyPaidLoans)(popup.DateFrom, popup.DateTo, IIf(intX = 1, True, False))
    End Sub

    Private Sub mnuLRV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRV.Click
        Select Case Common.PopupOptions("Release", "Payments", "Cancel", "")
            Case 1
                Dim popup As New Business.Popups.PnNoRange()
                If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
                Common.OpenReport(Of Voucher.Release)(popup.TextFrom, sysuser, IIf(popup.TextTo.Length = 0, popup.TextFrom, popup.TextTo))
            Case 2
                Dim loan As Business.Objects.Loan = Common.GetLoan()
                If loan Is Nothing Then Exit Sub
                Common.OpenReport(Of Loans.CashDisbursementOrder)(loan.PN_NO, sysuser)
        End Select
    End Sub

    Private Sub mnuLRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRN.Click
        Dim popup As New Business.Popups.DateRange()
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Loans.SavingsAccountTransactionRegister)(popup.DateFrom, popup.DateTo)
    End Sub

    Private Sub mnuLRS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRS.Click
        Dim popup As New Business.Popups.DateRange()
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Loans.RestructuredLoans)(popup.DateFrom, popup.DateTo)
    End Sub

    Private Sub mnuLRM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRM.Click
        Dim popup As New Business.Popups.DateRange()
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Loans.PreterminatedLoans)(popup.DateFrom, popup.DateTo)
    End Sub

    Private Sub mnuLRL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRL.Click
        Dim popup As New Business.Popups.PnNoAndDateRange()
        popup.DateFrom = New Date(1900, 1, 1)
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Loans.SubsidiaryLoanLedger)(popup.PnNo, popup.DateFrom, popup.DateTo)
    End Sub

    Private Sub mnuLRC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRC.Click
        Common.OpenReport(Of Loans.TransactionSchedule)()
    End Sub

    Private Sub mnuLRE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRE.Click
        Dim popup As New Business.Popups.DateRange
        Dim popup2 As New Business.Popups.InputDate

        popup.DateFrom = New Date(1900, 1, 1)
        popup2.Date = sysdate

        Select Case Common.PopupOptions("All", "Selective", "History", "Cancel", "")
            Case 1
                popup.DateTo = Date.MaxValue
                Common.OpenReport(Of Loans.LoanArrears)(popup.DateFrom, popup.DateTo)
            Case 2
                If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
                Common.OpenReport(Of Loans.LoanArrears)(popup.DateFrom, popup.DateTo)
            Case 3
                If Common.OpenPopup(popup2) <> Popups.PopupResponses.Ok Then Exit Sub
                Common.OpenReport(Of Loans.LoanArrears)(popup2.Date)
            Case 4
                Exit Sub
        End Select

    End Sub

    Private Sub mnuLRU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRU.Click
        Dim popup As New Business.Popups.DateRange()
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Loans.LoansDue)(popup.DateFrom, popup.DateTo, sysdate, sysuser)
    End Sub

    Private Sub mnuLRT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLRT.Click
        Common.OpenReport(Of Loans.KbciDeductionRegister)()
    End Sub

    Private Sub mnuLF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLF.Click
        Dim form As New LoanStaffPayment
        form.Show()
        form = Nothing
    End Sub

#End Region

#Region "Others"

    Private Sub mnuOA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuON.Click
        Dim form As New MemberForm
        form.Show()
        form = Nothing
    End Sub

    Private Sub mnuOCA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuORM.Click
        Common.OpenReport(Of Report.Members.List)()
    End Sub

    Private Sub mnuOCB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuORD.Click
        Dim region As String = String.Empty
        Dim popup As New Business.Popups.InputText
        Dim order As Int16 = Common.PopupOptions("KBCI No", "Region", "Name", "Cancel", "Select Order Method")
        Select Case order
            Case 2
                If Common.OpenPopup(popup, "Enter Region") <> Popups.PopupResponses.Ok Then Exit Sub
            Case 4
                Exit Sub
        End Select
        Common.OpenReport(Of Report.CashDividend.Register)(order, popup.Text)
    End Sub

    Private Sub mnuOD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOL.Click
        Dim form As New LriMaintenance
        form.Show()
    End Sub

#End Region

#Region "Payroll"

    Private Function IsDuplicateUpload(ByVal deductionTable As String, ByVal externalFile As String) As Boolean
        Dim db As New Data.Database
        Dim count As Integer = db.ExecuteQuery(String.Format("select distinct [DATE] from {0} where [DATE] = (select max(DATE7) from {1})", deductionTable, externalFile), CommandType.Text).Tables(0).Rows.Count
        Return count > 0
    End Function

    Private Function IsValidPayrollDay() As Boolean
        Common.GetLatestControl()

        If loanControl.PROC.Month = sysdate.Month AndAlso loanControl.PROC.Year = sysdate.Year Then
            Common.PopupError(String.Format("Payroll deduction for {0} was already processed", loanControl.PROC.ToString("MMMM")))
            Return False
        End If

        If sysdate.Day <> 7 Then
            MessageBox.Show("Payroll processing is only allowed on the 7th day of the month.", "Payroll Processing", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        Return True
    End Function

    Private Function IsPayrollWarningOk(ByVal message As String) As Boolean
        Dim sb As New StringBuilder
        sb.AppendFormat("This module will process '{0}' for the month of {1}.{2}", message, Format(sysdate, "MMMM"), vbCrLf)
        sb.AppendLine("Would you like to proceed?")
        Return Common.PopupQuestion(sb.ToString()) = Windows.Forms.DialogResult.Yes
    End Function

    Private Function GetPayrollData(ByVal fileName As String) As DataTable
        opd.FileName = fileName
        opd.Filter = "Database Files (*.dbf)|*.dbf"
        opd.Multiselect = False
        opd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}"
        opd.Title = "Select deduction file."

        If opd.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Return Nothing
        If Not System.IO.File.Exists(opd.FileName) Then
            MessageBox.Show("File not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return Nothing
        End If

        Return Data.OleDB.ImportDbf(opd.FileName)
    End Function

    Private Function ImportPayrollData(ByVal table As String, ByVal db As Data.Database, ByVal dt As DataTable) As Double
        Dim amount As Double = 0
        Dim date7s As String
        Dim date7 As Date

        db.ExecuteNonQuery(String.Format("truncate table dbo.{0}", table), CommandType.Text)

        For Each dr As DataRow In dt.Rows
            If IsDate(dr.Item("DATE7")) Then
                date7 = dr.Item("DATE7")
            ElseIf IsNumeric(dr.Item("DATE7")) AndAlso CDbl(dr.Item("DATE7")) >= 20000000 Then
                date7s = dr.Item("DATE7").ToString()
                date7 = New Date(date7s.Substring(0, 4), date7s.Substring(4, 2), date7s.Substring(6, 2))
            Else
                date7 = sysdate
            End If

            db.AddParameter("@EMPNO1", dr.Item("EMPNO1"))
            db.AddParameter("@ACTYPE", dr.Item("ACTYPE"))
            db.AddParameter("@ACTCD1", dr.Item("ACTCD1"))
            db.AddParameter("@ACTCD2", dr.Item("ACTCD2"))
            db.AddParameter("@DATE7", date7)
            db.AddParameter("@AMT7C", dr.Item("AMT7C"))
            db.AddParameter("@CODE5", dr.Item("CODE5"))

            If table = "EXTKBC" Then
                db.ExecuteNonQuery("dbo.s3p_Payroll_Process_Insert", CommandType.StoredProcedure)
            Else
                db.ExecuteNonQuery("dbo.s3p_Payroll_Others_Process_Insert", CommandType.StoredProcedure)
            End If

            amount += CDbl(dr.Item("AMT7C"))
        Next

        Return amount
    End Function

    Private Sub mnuPGP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPGP.Click
        If Not Common.IsAuthenticated(Infrastructure.Enumerations.System.AccessLevels.Level3) Then
            Common.PopupExclamation("Unathorized execution.")
            Return
        End If

        Business.Rules.Payroll.IsAsOfPayrollGenerated()
    End Sub

    Private Sub mnuPGV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPGV.Click
        If Not Common.IsAuthenticated(Infrastructure.Enumerations.System.AccessLevels.Level3) Then
            Common.PopupExclamation("Unathorized execution.")
            Return
        End If
        'Common.OpenReport(Of Report.Payroll.Stop)()
        Common.OpenReport(Of Report.Payroll.Advice)(False)
    End Sub

    Private Sub mnuPGC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPGC.Click
        If Not Common.IsAuthenticated(Infrastructure.Enumerations.System.AccessLevels.Level3) Then
            Common.PopupExclamation("Unathorized execution.")
            Return
        End If

        If Common.PopupQuestion("This process will overwrite deduction tables" & vbCrLf & "with advice details. Proceed?") = Windows.Forms.DialogResult.No Then Return

        Dim sAdviceFolder As String = GetAdviceWorkingFolder()
        If Directory.Exists(sAdviceFolder) Then fbd.SelectedPath = sAdviceFolder
        'fbd.ShowDialog()
        If System.IO.Directory.Exists(fbd.SelectedPath) AndAlso Business.Rules.Payroll.IsAdviceGenerated(fbd) Then
            Common.PopupInformation("Advice files have been created.")
        End If
    End Sub

    Private Sub mnuPGX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPGX.Click
        If Not Common.IsAuthenticated(Infrastructure.Enumerations.System.AccessLevels.Level3) Then
            Common.PopupExclamation("Unathorized execution.")
            Return
        End If

        If Common.PopupQuestion("This process will overwrite deduction tables" & vbCrLf & "with breakdown details. Proceed?") = Windows.Forms.DialogResult.No Then Return

        Dim sAdviceFolder As String = GetAdviceWorkingFolder()
        opd.Multiselect = False

        If Directory.Exists(sAdviceFolder) Then fbd.SelectedPath = sAdviceFolder

        'If fbd.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Return
        If Not System.IO.Directory.Exists(fbd.SelectedPath) Then
            Common.PopupExclamation("Invalid path")
            Return
        End If

        opd.InitialDirectory = fbd.SelectedPath

        If Business.Rules.Payroll.IsExtkbcGenerated(fbd, opd) Then
            Common.PopupInformation("Extkbc has been created.")
        End If
    End Sub

    Private Sub mnuPGF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPGF.Click
        If Not Common.IsAuthenticated(Infrastructure.Enumerations.System.AccessLevels.Level3) Then
            Common.PopupExclamation("Unathorized execution.")
            Return
        End If

        If Common.PopupQuestion("This process will delete fully paid deductions" & vbCrLf & "for the next offcycle round. Proceed?") = Windows.Forms.DialogResult.No Then Return

        Common.OpenReport(Of Report.Payroll.Advice)(True)
    End Sub

    Private Sub mnuPGO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPGO.Click
        If Not Common.IsAuthenticated(Infrastructure.Enumerations.System.AccessLevels.Level3) Then
            Common.PopupExclamation("Unathorized execution.")
            Return
        End If

        If Common.PopupQuestion("This process will overwrite deduction tables" & vbCrLf & "with advice details. Proceed?") = Windows.Forms.DialogResult.No Then Return

        Dim sAdviceFolder As String = GetAdviceOffcycleWorkingFolder()
        If Directory.Exists(sAdviceFolder) Then fbd.SelectedPath = sAdviceFolder
        'fbd.ShowDialog()
        If System.IO.Directory.Exists(fbd.SelectedPath) AndAlso Business.Rules.Payroll.IsAdviceGenerated(fbd) Then
            Common.PopupInformation("Advice files have been created.")
        End If
    End Sub

    Private Sub mnuPGT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPGT.Click
        If Not Common.IsAuthenticated(Infrastructure.Enumerations.System.AccessLevels.Level3) Then
            Common.PopupExclamation("Unathorized execution.")
            Return
        End If

        If Common.PopupQuestion("This process will overwrite deduction tables" & vbCrLf & "with breakdown details. Proceed?") = Windows.Forms.DialogResult.No Then Return

        Dim sAdviceFolder As String = GetAdviceOffcycleWorkingFolder()
        opd.Multiselect = False

        If Directory.Exists(sAdviceFolder) Then fbd.SelectedPath = sAdviceFolder

        'If fbd.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Return
        If Not System.IO.Directory.Exists(fbd.SelectedPath) Then
            Common.PopupExclamation("Invalid path")
            Return
        End If

        opd.InitialDirectory = fbd.SelectedPath

        If Business.Rules.Payroll.IsExtkbcGenerated(fbd, opd) Then
            Common.PopupInformation("Extkbc has been created.")
        End If
    End Sub

    Private Sub mnuPGN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPGN.Click
        If Not Common.IsAuthenticated(Infrastructure.Enumerations.System.AccessLevels.Level3) Then
            Common.PopupExclamation("Unathorized execution.")
            Return
        End If

        Dim sAdviceFolder As String = GetAdviceOffcycleWorkingFolder()
        opd.Multiselect = False

        If Directory.Exists(sAdviceFolder) Then fbd.SelectedPath = sAdviceFolder

        If fbd.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Return
        If Not System.IO.Directory.Exists(fbd.SelectedPath) Then
            Common.PopupExclamation("Invalid path")
            Return
        End If

        opd.InitialDirectory = fbd.SelectedPath

        If Business.Rules.Payroll.IsExtkbcGenerated(fbd, opd, True) Then
            Common.PopupInformation("Extkbc has been consolidated.")
        End If
    End Sub

    Private Sub mnuPB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPP.Click
        Dim popup As New Business.Popups.PayrollTextAndAmount()
        Dim amount As Double
        Dim remitAmount As Double = 0
        Dim remitSource As String = String.Empty
        Dim db As New Data.Database()
        Dim bgp As BackgroundProcess
        Const externalFile As String = "EXTKBC"

        If Not IsValidPayrollDay() Then Exit Sub
        If Not IsPayrollWarningOk("Payroll Deductions") Then Exit Sub
        If Common.OpenPopup(popup, "Remittance") <> Popups.PopupResponses.Ok Then Exit Sub
        remitSource = popup.Text
        remitAmount = popup.Amount

        Dim dt As DataTable = GetPayrollData(externalFile + ".dbf")
        If dt Is Nothing Then Exit Sub
        amount = ImportPayrollData(externalFile, db, dt)

        If IsDuplicateUpload("MO_DEDNH", externalFile) OrElse IsDuplicateUpload("MO_DEDNO", externalFile) Then
            Common.PopupExclamation("Transaction cancelled. Check payroll date!")
            Exit Sub
        End If

        If Math.Round(remitAmount, 2) <> Math.Round(amount, 2) Then
            Common.PopupExclamation("Transaction cancelled. Check remittance amount!")
            Exit Sub
        End If

        SetMenu(False)
        db.AddParameter("@MY_USER", sysuser)
        db.AddParameter("@SOD_TXT", remitSource)
        db.AddParameter("@SOD_TOT", remitAmount)
        bgp = New BackgroundProcess(Me, Infrastructure.Enumerations.System.AsyncProcess.Payroll, db)
        bgp.RunWorkerAsync()
    End Sub

    Private Sub mnuPCA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPRD.Click
        ViewPayrollDeductionRegister()
    End Sub

    Private Sub mnuPCB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPRN.Click
        Dim payrollDate As New Date(1900, 1, 1)

        Select Case Common.PopupOptions("Current", "History", "Cancel", "")
            Case 2
                Dim popup As New Business.Popups.InputDate()
                If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
                payrollDate = popup.Date
            Case 3
                Exit Sub
        End Select

        Common.OpenReport(Of Report.Payroll.NoDeductionRegister)(payrollDate)
    End Sub

    Private Sub mnuPCD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPRV.Click
        ViewPayrollDeductionVoucher()
    End Sub

    Private Sub mnuPCE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPV.Click
        Dim db As New Data.Database()
        Dim popup As New Business.Popups.DateRange
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub

        Dim x1 As Integer

        Select Case Common.PopupOptions("Advance", "Extract", "Cancel", "")
            Case 1
                x1 = 1
            Case 2
                x1 = 2
            Case 3
                Exit Sub
        End Select

        Common.OpenReport(Of Report.Payroll.AdvancePayments)(popup.DateFrom, popup.DateTo, x1)
    End Sub

    Private Sub mnuPD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPA.Click
        Dim form As New PayrollAdvancePaymentsMaintenance
        form.Show()
        form = Nothing
    End Sub

    Private Sub mnuPEA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPO.Click
        Dim popup As New Business.Popups.PayrollTextAndAmount()
        Dim amount As Double
        Dim remitAmount As Double = 0
        Dim remitSource As String = String.Empty
        Dim db As New Data.Database()
        Dim bgp As BackgroundProcess
        Const externalFile As String = "EXTKBC2"
        Const importTable As String = "EXTKBCO"

        If Not IsPayrollWarningOk("Other Deductions") Then Exit Sub
        If Common.OpenPopup(popup, "Remittance") <> Popups.PopupResponses.Ok Then Exit Sub
        remitSource = popup.Text
        remitAmount = popup.Amount

        Dim dt As DataTable = GetPayrollData(externalFile + ".dbf")
        If dt Is Nothing Then Exit Sub
        amount = ImportPayrollData(importTable, db, dt)

        If IsDuplicateUpload("MO_DEDNH", importTable) OrElse IsDuplicateUpload("MO_DEDNO", importTable) Then
            Common.PopupExclamation("Transaction cancelled. Check payroll date!")
            Exit Sub
        End If

        If Math.Round(remitAmount, 2) <> Math.Round(amount, 2) Then
            Common.PopupExclamation("Transaction cancelled. Check remittance amount!")
            Exit Sub
        End If

        SetMenu(False)
        db.AddParameter("@MY_USER", sysuser)
        db.AddParameter("@SOD_TXT", remitSource)
        db.AddParameter("@SOD_TOT", remitAmount)
        bgp = New BackgroundProcess(Me, Infrastructure.Enumerations.System.AsyncProcess.OffCycle, db)
        bgp.RunWorkerAsync()
    End Sub

    Private Sub mnuPEB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPOR.Click
        ViewPayrollDeductionRegister(True)
    End Sub

    Private Sub mnuPEC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPOV.Click
        ViewPayrollDeductionVoucher(True)
    End Sub

    Private Function GetAdviceWorkingFolder() As String
        Dim sPath As String

        sPath = ConfigurationManager.AppSettings.Get("Folder.Advice.Prod")
        If Directory.Exists(sPath) Then Return sPath

        sPath = ConfigurationManager.AppSettings.Get("Folder.Advice.Test")
        If Directory.Exists(sPath) Then Return sPath

        Common.PopupExclamation("Default payroll folder does not exist.")
        Return String.Empty
    End Function

    Private Function GetAdviceOffcycleWorkingFolder() As String
        Dim sPath As String

        sPath = ConfigurationManager.AppSettings.Get("Folder.Advice.Offcycle.Prod")
        If Directory.Exists(sPath) Then Return sPath

        sPath = ConfigurationManager.AppSettings.Get("Folder.Advice.Offcycle.Test")
        If Directory.Exists(sPath) Then Return sPath

        Common.PopupExclamation("Default offcycle folder does not exist.")
        Return String.Empty
    End Function

#End Region

#Region "Maintenance"

    Private Sub mnuMA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuML.Click
        Common.ShowTableEditor(Common.MaintainanceTable.Loans, Common.MaintenanceFilter.PN_NO)
    End Sub

    Private Sub mnuMM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMM.Click
        Common.ShowTableEditor(Common.MaintainanceTable.Members, Common.MaintenanceFilter.KBCI_NO)
    End Sub

    Private Sub mnuMP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMP.Click
        Common.ShowTableEditor(Common.MaintainanceTable.Param)
    End Sub

    Private Sub mnuMC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMC.Click
        Common.ShowTableEditor(Common.MaintainanceTable.Ctrl)
    End Sub

    Private Sub mnuME_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMO.Click
        Common.ShowTableEditor(Common.MaintainanceTable.Payhist, Common.MaintenanceFilter.PN_NO)
    End Sub

    Private Sub mnuMF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuME.Click
        Common.ShowTableEditor(Common.MaintainanceTable.Ledger, Common.MaintenanceFilter.PN_NO)
    End Sub

    Private Sub mnuMW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMW.Click
        Dim form As New PopupPasswordChange()
        form.ShowDialog()
    End Sub

    Private Sub mnuMD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMD.Click
        Common.ShowTableEditor(Common.MaintainanceTable.Mo_Dedn_Detl)
    End Sub

    Private Sub mnuMB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMB.Click
        SetMenu(False)
        Common.RunAsyncProcess(Me, Infrastructure.Enumerations.System.AsyncProcess.Backup)
    End Sub

    Private Sub mnuMR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMR.Click
        SetMenu(False)
        Common.RunAsyncProcess(Me, Infrastructure.Enumerations.System.AsyncProcess.Restore)
    End Sub

    Private Sub mnuML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMA.Click
        Dim form As New LoanMonitoring
        form.Show()
        form = Nothing
    End Sub

#End Region

#Region "Admin"

    Private Sub mnuAAAA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARDD.Click
        Dim loanType As String = "%"
        If Common.PopupQuestion("Print Daily Transaction Register?") = Windows.Forms.DialogResult.Yes Then
            Select Case Common.PopupOptions("All", "Details", "Summary", "")
                Case 1
                    If Business.Rules.Administration.IsReportTagged(1) And Business.Rules.Administration.IsReportTagged(2) Then Common.OpenReport(Of Admin.DailyTransactionRegister)()
                Case 2
                    If MsgBox("Selective?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        loanType = selectLoanType()
                        If loanType.Trim().Length = 0 Then Exit Sub
                    End If
                    If Business.Rules.Administration.IsReportTagged(1) And Business.Rules.Administration.IsReportTagged(2) Then Common.OpenReport(Of Admin.DailyTransactionRegisterDetails)(loanType)
                Case 3
                    If Business.Rules.Administration.IsReportTagged(1) And Business.Rules.Administration.IsReportTagged(2) Then Common.OpenReport(Of Admin.DailyTransactionRegisterSummary)()
            End Select
        End If
    End Sub

    Private Sub mnuAAAB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARDR.Click
        If Common.PopupQuestion("Print List of Released Loans?") = Windows.Forms.DialogResult.Yes Then
            If Business.Rules.Administration.IsReportTagged(3) Then Common.OpenReport(Of Admin.ReleasedLoans)()
        End If
    End Sub

    Private Sub mnuAAAC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARDF.Click
        If Common.PopupQuestion("Print List of Fully Paid Loans?") = Windows.Forms.DialogResult.Yes Then
            If Business.Rules.Administration.IsReportTagged(4) Then Common.OpenReport(Of Admin.FullyPaidLoans)()
        End If
    End Sub

    Private Sub mnuAAAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARDV.Click
        If Business.Rules.Administration.IsDailyReversionProcessed() Then
            If Common.PopupQuestion("Print Reversed Transaction Register?") = Windows.Forms.DialogResult.Yes Then
                Select Case Common.PopupOptions("Details", "Summary", "")
                    Case 1
                        If Business.Rules.Administration.IsReportTagged(5) Then Common.OpenReport(Of Admin.ReversedTransactionRegisterDetails)()
                    Case 2
                        If Business.Rules.Administration.IsReportTagged(5) Then Common.OpenReport(Of Admin.ReversedTransactionRegisterSummary)()
                End Select
            End If
            If MsgBox("Print Reversed Transaction Register?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            End If
        End If
    End Sub

    Private Sub mnuAABA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARME.Click
        MsgBox("Extract transactions for a given period?", MsgBoxStyle.YesNo)
    End Sub

    Private Sub mnuAABB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARMC.Click
        If Common.PopupQuestion("Print Monthly Runup?") = Windows.Forms.DialogResult.Yes Then
            Dim popup As New Business.Popups.MonthlyRunup()
            popup.Date = sysdate
            If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
            Common.OpenReport(Of Admin.MonthlyRunup)(popup.Date, "0", popup.ShowSubTotal)
        End If
    End Sub

    Private Sub mnuAABC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARMP.Click
        If Common.PopupQuestion("Print Monthly Runup (Past Due)?") = Windows.Forms.DialogResult.Yes Then
            Dim popup As New Business.Popups.MonthlyRunup()
            popup.Date = sysdate
            If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
            Common.OpenReport(Of Admin.MonthlyRunup)(popup.Date, "1", popup.ShowSubTotal)
        End If
    End Sub

    Private Sub mnuARMB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARMB.Click
        Dim popup As New Business.Popups.InputDate
        popup.Date = New Date(sysdate.Year, sysdate.Month, 15)
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Admin.MonthlyRunupBreakdown)(popup.Date)
    End Sub

    Private Sub mnuARMO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARMO.Click
        Dim popup As New Business.Popups.InputDate
        popup.Date = New Date(sysdate.Year, sysdate.Month, 15)
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Admin.MonthlyRunupConsolidation)(popup.Date)
    End Sub

    Private Sub mnuAABD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARMT.Click
        PrinterOptions()
        strDateStart = sysdate
        strDateEnd = sysdate
        MsgBox("Print transactions as of " & strDateStart & " to " & strDateEnd & "?", MsgBoxStyle.YesNo)
        intOption1 = Common.PopupOptions("All Loan Types", "Per Loan Type", "Options:")
        If intOption1 = 2 Then
            strInput = InputBox("Enter Loan Type")
        End If
    End Sub

    Private Sub mnuAB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAE.Click
        Dim strDTR As String
        Dim strDSR As String
        Dim strRLR As String
        Dim strFPLR As String
        Dim strRTR As String
        Dim strTTR As String

        Dim dr As DataRow
        Dim boComplete As Boolean = True

        dr = Common.GetDetailsOld("CTRL", "").Rows(0)

        If dr.Item("REP1") = True Then
            strDTR = "OK"
        Else
            strDTR = "NOK"
            boComplete = False
        End If

        If dr.Item("REP2") = True Then
            strDSR = "OK"
        Else
            strDSR = "NOK"
            boComplete = False
        End If

        If dr.Item("REP3") = True Then
            strRLR = "OK"
        Else
            strRLR = "NOK"
            boComplete = False
        End If

        If dr.Item("REP4") = True Then
            strFPLR = "OK"
        Else
            strFPLR = "NOK"
            boComplete = False
        End If

        If dr.Item("REP5") = True Then
            strRTR = "OK"
        Else
            strRTR = "NOK"
            boComplete = False
        End If

        If dr.Item("TD_REP") = True Then
            strTTR = "OK"
        Else
            strTTR = "NOK"
            'boComplete = False
        End If

        Common.PopupInformation( _
            "Daily Transaction Register" & vbTab & vbTab & " - " & strDTR & vbNewLine & _
            "Daily Transaction Summary" & vbTab & vbTab & " - " & strDSR & vbNewLine & _
            "Released Loans Register" & vbTab & vbTab & " - " & strRLR & vbNewLine & _
            "Fully Paid Loans Register" & vbTab & vbTab & " - " & strFPLR & vbNewLine & _
            "Reversed Transaction Register" & vbTab & " - " & strRTR & vbNewLine & _
            "TD Transaction Register" & vbTab & vbTab & " - " & strTTR _
        )

        'for testing only
        'boComplete = True

        If boComplete Then
            If Common.PopupQuestion("End-of-Day will be executed. Proceed?") = Windows.Forms.DialogResult.Yes Then
                SetMenuMethod(False)
                Common.PopupInformation("End-of-Day starting. Press any key to continue.")
                Common.RunAsyncProcess(Me, Infrastructure.Enumerations.System.AsyncProcess.EndOfDay)
            End If
        Else
            Common.PopupExclamation("Please complete printing of reports !!!")
        End If
    End Sub

    Private Sub mnuAC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAA.Click
        Dim sb As New System.Text.StringBuilder

        If MsgBox("Archive Loan Records?", MsgBoxStyle.YesNo) = vbYes Then
            Dim popup As New Business.Popups.InputDate()
            If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub

            sb.AppendFormat("Today is {0}{1}", Now.ToString("MM/dd/yyyy"), vbNewLine)
            sb.AppendLine("** Warning **")
            sb.AppendLine("This routine will archive loan records and its corresponding")
            sb.AppendLine("data in other files up to the date specified")
            sb.AppendLine("")
            sb.AppendFormat("Archive fully paid loans up to {0}", Format(popup.Date, "MM/dd/yyyy"))

            If MsgBox(sb.ToString(), MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                If Business.Rules.Administration.IsArchived(popup.Date) Then
                    MsgBox("Archiving complete!", MsgBoxStyle.Information)
                Else
                    MsgBox("Archiving failed!", MsgBoxStyle.Critical)
                End If
            End If
        End If
    End Sub

    Private Sub mnuAIE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAIE.Click
        Dim popup As New Business.Popups.DateRange
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub

        If Business.Rules.Administration.IsInterestExtracted(popup.DateFrom, popup.DateTo) Then
            MsgBox("Records extracted.", MsgBoxStyle.Information)
        Else
            MsgBox("Records not extracted.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub mnuAII_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAII.Click
        Select Case Common.PopupOptions("Print Monthly", "Print Summary", "Cancel")
            Case 1
                Common.OpenReport(Of Admin.InterestDetails)()
            Case 2
                Common.OpenReport(Of Admin.InterestSummary)()
        End Select
    End Sub

    Private Sub mnuAIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAIP.Click
        Const source As String = "SQL"
        Dim popup As New Business.Popups.InputInt
        If Common.OpenPopup(popup, "Enter Year:") <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Admin.StaffPtlInterestPaid)(source, popup.Number)
    End Sub

    Private Sub mnuAIB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAIB.Click
        Dim popup As New Business.Popups.InputInt
        If Common.OpenPopup(popup, "Enter Year:") <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Admin.StaffPtlBalance)(popup.Number)
    End Sub

    Private Sub mnuAIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAIT.Click
        Dim popupYear As Business.Popups.InputInt
        Dim popupDateRange As Business.Popups.DateRange
        Dim tempDate As Date
        Const source As String = "SQL"
        Select Case Common.PopupOptions("By Year", "By Date Range", "Cancel", "Reporting Method")
            Case 1
                popupYear = New Business.Popups.InputInt
                If Common.OpenPopup(popupYear, "Enter Year:") <> Popups.PopupResponses.Ok Then Exit Sub
                Common.OpenReport(Of Admin.TotalInterestPaid)(source, popupYear.Number)
            Case 2
                popupDateRange = New Business.Popups.DateRange
                tempDate = New Date(sysdate.Year, sysdate.Month, 1).AddDays(-1)
                popupDateRange.DateFrom = New Date(tempDate.Year, tempDate.Month, 1)
                popupDateRange.DateTo = New Date(tempDate.Year, tempDate.Month, tempDate.Day)
                If Common.OpenPopup(popupDateRange, "Enter Date Range:") <> Popups.PopupResponses.Ok Then Exit Sub
                Common.OpenReport(Of Admin.TotalInterestPaidByRange)(popupDateRange.DateFrom, popupDateRange.DateTo)
        End Select
    End Sub

    Private Sub mnuAFP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAFP.Click
        Const source As String = "FOX"
        Dim popup As New Business.Popups.InputInt
        If Common.OpenPopup(popup, "Enter year") <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Admin.StaffPtlInterestPaid)(source, popup.Number)
    End Sub

    Private Sub mnuAFT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAFT.Click
        Const source As String = "FOX"
        Dim popup As New Business.Popups.InputInt
        If Common.OpenPopup(popup, "Enter year") <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Admin.TotalInterestPaid)(source, popup.Number)
    End Sub
#End Region

#Region "Close"

    Private Sub mnuC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuC.Click
        Dim dr As DataRow = Common.GetDetailsOld("CTRL", "").Rows(0)
        Dim sb As New System.Text.StringBuilder()

        If CBool(dr.Item("CLOSE")) = True Then
            If Common.PopupQuestion("Transactions already closed, do you wish to open?") = Windows.Forms.DialogResult.Yes Then
                If Business.Rules.Administration.IsSystemOpened() Then
                    Common.PopupInformation("Opened successfully.")
                    SetMenuAfterOpeningMethod()
                Else
                    Common.PopupExclamation("Open failed.")
                End If
            End If
            Exit Sub
        End If

        sb.AppendLine("The purpose of this module is to finalize all transactions for today and prepare the system for end of day processing.")
        sb.AppendLine("")
        sb.AppendLine("When this module is executed, the system can no longer process any transactions.")
        sb.AppendLine("")
        sb.AppendLine("Please proceed with caution...")

        If Common.PopupQuestion(sb.ToString()) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        SetMenuMethod(False)
        Common.PopupInformation("Close starting. Press any key to continue.")
        Common.RunAsyncProcess(Me, Infrastructure.Enumerations.System.AsyncProcess.Close)
    End Sub

#End Region

#Region "Adhoc"

    Private Sub mnuARAA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARAA.Click
        Dim popup As New Business.Popups.DateRange()
        popup.DateFrom = New Date(Now.Year, 1, 1)
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Report.Adhoc.Arrears)(popup.DateFrom, popup.DateTo)
    End Sub

    Private Sub mnuARAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARAR.Click
        Dim popup As New Business.Popups.LoanTypeAndDate()
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Report.Adhoc.Philam)(popup.LoanType.ToString().Replace("ALL", "%"), popup.Date.Month, popup.Date.Year)
    End Sub

    Private Sub mnuARAM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARAM.Click
        Dim popup As New Business.Popups.DateRange()
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Adhoc.MiscellaneousLiability)(popup.DateFrom, popup.DateTo)
    End Sub

    Private Sub mnuARAU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARAU.Click
        Dim popup As New Business.Popups.AsOfDateAndDateRange
        Dim sortByName As Boolean
        Dim mode As String
        Select Case Common.PopupOptions("Maturing", "Outstanding", "Released", "Cancel", "")
            Case 1
                mode = "Maturing"
            Case 2
                mode = "Outstanding"
                popup.DateFrom = New Date(1900, 1, 1)
            Case 3
                mode = "Released"
            Case 4
                Exit Sub
        End Select

        popup.AsOfDate = New Date(sysdate.Year - 1, 12, 31)
        popup.DateFrom = New Date(sysdate.Year - 1, 1, 1)
        popup.DateTo = New Date(sysdate.Year - 1, 12, 31)

        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Select Case Common.PopupOptions("Name", "Loan Type", "Sort")
            Case 1
                sortByName = True
            Case 2
                sortByName = False
        End Select
        Common.OpenReport(Of Adhoc.Runup)(popup.DateFrom, popup.DateTo, popup.AsOfDate, sortByName, mode)
    End Sub

    Private Sub mnuARAP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARAP.Click
        Dim popup As New Business.Popups.AsOfDateAndDateRange
        Dim sortByName As Boolean
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Select Case Common.PopupOptions("Name", "Loan Type", "Sort")
            Case 1
                sortByName = True
            Case 2
                sortByName = False
        End Select
        Common.OpenReport(Of Adhoc.MissedPayments)(popup.DateFrom, popup.DateTo, popup.AsOfDate, sortByName)
    End Sub

    Private Sub mnuARAT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuARAT.Click
        Dim popup As New Business.Popups.InputDate
        popup.Date = sysdate
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Adhoc.TotalExposure)(New Date(1900, 1, 1), popup.Date)
    End Sub

#End Region

#Region "Exit"

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Close()
    End Sub

#End Region

#End Region

#Region "Events"

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetOtherInstances()
        GetUser()
        GetControls()
        SetAdminDate()
        SetLoanRates()
        ExecuteAdhocScript()
    End Sub

    Private Sub GetOtherInstances()
        If Process.GetProcessesByName(Process.GetCurrentProcess.ProcessName).Length > 1 Then
            Common.PopupExclamation(String.Format("Another instance of {0} is currently running. Terminating this session...", Process.GetCurrentProcess.ProcessName))
            End
        End If
    End Sub

    Private Sub GetUser()
        Using form As New PopupAuthenticate()
            form.ShowDialog()
        End Using

        If sysuser = String.Empty Then
            End
        Else
            MessageBox.Show("Login success.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub GetControls()
        Common.GetLatestControl()

        If loanControl Is Nothing Then
            Common.PopupExclamation("Control table not setup. Please contact system administrator.")
            End
        Else
            sysdate = loanControl.SYSDATE
            admdate = loanControl.ADMDATE
            tsslSYSDATE.Text = Format(loanControl.SYSDATE, "MM/dd/yyyy").ToUpper()

            If loanControl.CLOSE Then
                SetMenuAfterClosingMethod()
            Else
                SetMenuAfterOpeningMethod()
            End If
        End If
    End Sub

    Private Sub SetAdminDate()
        If DateDiff(DateInterval.Day, sysdate, admdate) <> 0 Then
            If Common.PopupQuestion(String.Format("System date is not equal to current date.{0}{0}Change?", vbCrLf)) = Windows.Forms.DialogResult.No Then
                End
            Else
                Dim popup As New Business.Popups.ControlDate

                If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then End

                If Business.Rules.Administration.IsAdminDateUpdated(popup.AdminDate) Then
                    admdate = popup.AdminDate
                Else
                    End
                End If
            End If

            If DateDiff(DateInterval.Day, sysdate, admdate) <> 0 Then Me.Close()
        End If
    End Sub

    Private Sub SetLoanRates()
        Dim rateFile As String = ConfigurationManager.AppSettings.Get("File.LoanRates")
        loanRates = New System.Data.DataTable("RATES")
        loanRates.Columns.Add(New DataColumn("LOAN_TYPE", System.Type.GetType("System.String")))
        loanRates.Columns.Add(New DataColumn("TERM", System.Type.GetType("System.Int32")))
        loanRates.Columns.Add(New DataColumn("RATE", System.Type.GetType("System.Double")))

        If File.Exists(rateFile) Then
            loanRates.ReadXml(rateFile)
            Common.PopupInformation(String.Format("{0} rate(s) loaded.", loanRates.Rows.Count))
        End If
    End Sub

    Private Sub ExecuteAdhocScript()
        Dim sr As StreamReader
        Dim db As New Data.Database()
        Dim sb As New StringBuilder()
        Dim sLine As String = String.Empty
        Dim iPatch As Integer = 0

        For Each script As String In Directory.GetFiles(Environment.CurrentDirectory, "*.sql")
            sr = New StreamReader(script)

            While Not sr.EndOfStream
                sLine = sr.ReadLine()
                If sLine.Trim() = "GO" Then
                    db.ExecuteNonQuery(sb.ToString(), CommandType.Text)
                    sb = New StringBuilder()
                Else
                    sb.AppendLine(sLine)
                End If
            End While

            sr.Close()
            File.Delete(script)
            iPatch += 1
        Next

        If iPatch > 0 Then
            Common.PopupInformation(String.Format("{0} patch script(s) applied.", iPatch))
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub SetMenuMethod(ByVal enable As Boolean)
        mnuL.Enabled = enable
        mnuO.Enabled = enable
        mnuP.Enabled = enable
        mnuM.Enabled = enable
        mnuA.Enabled = enable
        mnuC.Enabled = enable
        mnuExit.Enabled = enable
    End Sub

    Public Sub SetMenuAfterClosingMethod()
        SetMenuMethod(False)
        mnuA.Enabled = True
        mnuAR.Enabled = True
        mnuARD.Enabled = True
        mnuARM.Enabled = True
        mnuAE.Enabled = True
        mnuAA.Enabled = True
        'mnuAI.Enabled = True
        mnuAIE.Enabled = True
        mnuAII.Enabled = True
        mnuC.Enabled = True

        For Each mnu As MenuItem In mnuM.MenuItems
            mnu.Enabled = False
        Next

        mnuM.Enabled = True
        mnuMB.Enabled = True
    End Sub

    Public Sub SetMenuAfterOpeningMethod()
        SetMenuMethod(True)
        mnuA.Enabled = True
        mnuAR.Enabled = True
        mnuARD.Enabled = False
        mnuARM.Enabled = True
        mnuAE.Enabled = False
        mnuAA.Enabled = False
        'mnuAI.Enabled = False
        mnuAIE.Enabled = False
        mnuAII.Enabled = False
        mnuC.Enabled = True

        For Each mnu As MenuItem In mnuM.MenuItems
            mnu.Enabled = True
        Next
    End Sub

    Private Sub ViewPayrollDeductionVoucher(Optional ByVal offcycle As Boolean = False)
        Dim popup As New Business.Popups.InputDate
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Report.Voucher.PayrollDeduction)(popup.Date, sysuser, sysdate, offcycle)
    End Sub

    Private Sub ViewPayrollDeductionRegister(Optional ByVal offcycle As Boolean = False)
        Dim popup As New Business.Popups.InputDate()
        Dim payrollDate As New Date(1900, 1, 1)
        Dim loanType As String = "%"
        Dim code5 As Integer = 0

        If offcycle Then
            If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
            payrollDate = popup.Date
        Else
            Select Case Common.PopupOptions("Current", "History", "Cancel", "")
                Case 2
                    If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
                    payrollDate = popup.Date
                Case 3
                    Exit Sub
            End Select
        End If

        Select Case Common.PopupOptions("Details", "Summary", "Cancel", "")
            Case 1
                Select Case Common.PopupOptions("All", "Selective", "Cancel", "")
                    Case 1
                        loanType = "%"
                    Case 2
                        Dim dt As New DataTable
                        Dim dr As DataRow
                        Dim formDataGrid As New PopupDataGridOptions
                        Dim drLoanType As DataRow

                        dt.Columns.Add(Common.AddColumn("System.String", "LOAN_TYPE"))

                        For Each drLoanType In Common.GetDetailsOld("LoanTypeDetails", "%").Rows
                            If drLoanType.Item("LOAN_TYPE").ToString <> "STL" Then
                                dr = dt.NewRow
                                dr.Item("LOAN_TYPE") = drLoanType.Item("LOAN_TYPE").ToString()
                                dt.Rows.Add(dr)
                            End If
                        Next

                        dr = dt.NewRow : dr.Item("LOAN_TYPE") = " MA" : dt.Rows.Add(dr)
                        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "AIU" : dt.Rows.Add(dr)
                        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "COC" : dt.Rows.Add(dr)
                        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "FIX" : dt.Rows.Add(dr)
                        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "PAM" : dt.Rows.Add(dr)
                        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "SAV" : dt.Rows.Add(dr)

                        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "DEP" : dt.Rows.Add(dr)
                        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "OTH" : dt.Rows.Add(dr)
                        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "MAB" : dt.Rows.Add(dr)
                        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "A/P" : dt.Rows.Add(dr)
                        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "AR" : dt.Rows.Add(dr)

                        formDataGrid.GetDataGrid().DataSource = dt
                        formDataGrid.ShowDialog()

                        If formDataGrid.IsCanceled Then
                            Exit Sub
                        End If

                        loanType = CType(formDataGrid.GetDataGrid().DataSource, DataTable).Rows(formDataGrid.GetDataGrid().CurrentCell.RowIndex).Item("LOAN_TYPE")
                        formDataGrid = Nothing
                        dt = Nothing
                    Case 3
                        Exit Sub
                End Select

                Common.OpenReport(Of Report.Payroll.PayrollDeductionRegisterDetails)(payrollDate, loanType, code5, offcycle)
            Case 2
                Select Case Common.PopupOptions("All", "Select Code", "Cancel", "")
                    Case 1
                        code5 = 0
                    Case 2
                        Dim dt As New DataTable
                        Dim dr As DataRow
                        Dim formDataGrid As New PopupDataGridOptions

                        dt.Columns.Add(Common.AddColumn("System.String", "DESC"))
                        dt.Columns.Add(Common.AddColumn("System.Int16", "CODE5"))
                        dr = dt.NewRow

                        dr.Item("DESC") = "MAIN" : dr.Item("CODE5") = 1 : dt.Rows.Add(dr) : dr = dt.NewRow
                        dr.Item("DESC") = "BPSD" : dr.Item("CODE5") = 2 : dt.Rows.Add(dr) : dr = dt.NewRow
                        dr.Item("DESC") = "MROD" : dr.Item("CODE5") = 3 : dt.Rows.Add(dr) : dr = dt.NewRow
                        dr.Item("DESC") = "DGS" : dr.Item("CODE5") = 4 : dt.Rows.Add(dr)

                        formDataGrid.GetDataGrid().DataSource = dt
                        formDataGrid.ShowDialog()

                        If formDataGrid.IsCanceled Then
                            Exit Sub
                        End If

                        code5 = CType(formDataGrid.GetDataGrid().DataSource, DataTable).Rows(formDataGrid.GetDataGrid().CurrentCell.RowIndex).Item("CODE5")
                        formDataGrid = Nothing
                        dt = Nothing
                    Case 3
                        Exit Sub
                End Select

                Common.OpenReport(Of Report.Payroll.PayrollDeductionRegisterSummary)(payrollDate, loanType, code5, offcycle)
            Case 3
                Exit Sub
        End Select

    End Sub

    Private Function selectLoanType() As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim loanType As String
        Dim formDataGrid As New PopupDataGridOptions

        dt.Columns.Add(Common.AddColumn("System.String", "LOAN_TYPE"))

        For Each drLoanTypes As DataRow In Common.GetDetailsOld("LoanTypeDetails", "%").Rows
            dr = dt.NewRow : dr.Item("LOAN_TYPE") = drLoanTypes.Item("LOAN_TYPE") : dt.Rows.Add(dr)
        Next

        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "DEP" : dt.Rows.Add(dr)
        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "OTH" : dt.Rows.Add(dr)
        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "MAB" : dt.Rows.Add(dr)
        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "A/P" : dt.Rows.Add(dr)
        dr = dt.NewRow : dr.Item("LOAN_TYPE") = "AR" : dt.Rows.Add(dr)

        formDataGrid.GetDataGrid().DataSource = dt
        formDataGrid.ShowDialog()

        If formDataGrid.IsCanceled Then
            Return String.Empty
        End If

        loanType = CType(formDataGrid.GetDataGrid().DataSource, DataTable).Rows(formDataGrid.GetDataGrid().CurrentCell.RowIndex).Item("LOAN_TYPE")
        formDataGrid = Nothing
        dt = Nothing

        Return loanType
    End Function

#End Region
    
End Class
