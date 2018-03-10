Public Class KbciForms
    Inherits System.Windows.Forms.Form

    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(KbciForms))
        Me.SuspendLayout()
        '
        'KbciForms
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "KBCIForms"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.ResumeLayout(False)

    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim rc As Rectangle

        If ClientSize.Width > 0 AndAlso ClientSize.Height > 0 Then
            rc = New Rectangle(0, 0, Me.ClientSize.Width, Me.ClientSize.Height)

            Using brush As New System.Drawing.Drawing2D.LinearGradientBrush(rc, Color.LightBlue, Color.RoyalBlue, Drawing2D.LinearGradientMode.Vertical)
                e.Graphics.FillRectangle(brush, rc)
            End Using
        End If
    End Sub

    Private Sub ApplyTheme(ByVal container As Control)
        Dim cboFontSize As Single = System.Configuration.ConfigurationManager.AppSettings.Get("")
        Dim txtFontSize As Single = System.Configuration.ConfigurationManager.AppSettings.Get("")
        Dim lblFontSize As Single = System.Configuration.ConfigurationManager.AppSettings.Get("")
        Dim chkFontSize As Single = System.Configuration.ConfigurationManager.AppSettings.Get("")

        For Each control As Control In container.Controls
            Select Case True

                Case TypeOf control Is ComboBox
                    control.Font = ApplyThemeFont(control.Font, cboFontSize)

                Case TypeOf control Is TextBox
                    control.Font = ApplyThemeFont(control.Font, txtFontSize)

                Case TypeOf control Is Label
                    control.BackColor = Color.Transparent
                    control.Font = ApplyThemeFont(control.Font, lblFontSize)

                Case TypeOf control Is CheckBox
                    control.BackColor = Color.Transparent
                    control.Font = ApplyThemeFont(control.Font, chkFontSize)

                Case TypeOf control Is GroupBox
                    control.BackColor = Color.Transparent

                Case TypeOf control Is DataGridView
                    CType(control, DataGridView).DefaultCellStyle.Font = ApplyThemeFont(control.Font, txtFontSize)
                    CType(control, DataGridView).ColumnHeadersDefaultCellStyle.Font = ApplyThemeFont(control.Font, txtFontSize)

                Case TypeOf control Is PropertyGrid
                    CType(control, PropertyGrid).Font = ApplyThemeFont(control.Font, lblFontSize)

            End Select

            If control.Controls.Count > 0 Then
                ApplyTheme(control)
            End If
        Next
    End Sub

    Private Function ApplyThemeFont(ByVal font As System.Drawing.Font, Optional ByVal size As Single = 0) As System.Drawing.Font
        Dim fontName As String = System.Configuration.ConfigurationManager.AppSettings.Get("Font.Name.Theme")
        Dim fontBoldOverride As Boolean = CBool(System.Configuration.ConfigurationManager.AppSettings.Get("Font.Bold.Override"))
        Dim fontStyle As System.Drawing.FontStyle

        If Not fontBoldOverride Then
            fontStyle = Drawing.FontStyle.Regular
        End If

        Return New System.Drawing.Font(fontName, font.Size + size, font.Style, font.Unit, font.GdiCharSet)
    End Function

    Private Sub KBCIForms_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.Name <> "Master" Then
            Me.WindowState = FormWindowState.Normal
        End If

        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        ApplyTheme(Me)
    End Sub

End Class
