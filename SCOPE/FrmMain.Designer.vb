<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.CmdCreate = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CmbArticle408 = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.DTPReportingPeriodStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.CmbArticle403 = New System.Windows.Forms.ComboBox()
        Me.CmbDocumentAdoptionStatus = New System.Windows.Forms.ComboBox()
        Me.CmbAuditorReportStatementPresent = New System.Windows.Forms.ComboBox()
        Me.CmbConsolidated = New System.Windows.Forms.ComboBox()
        Me.DTPDocumentAdoptionDate = New System.Windows.Forms.DateTimePicker()
        Me.DTPReportingPeriod = New System.Windows.Forms.DateTimePicker()
        Me.DTPReportingPeriodEndDate = New System.Windows.Forms.DateTimePicker()
        Me.CmbEntitySize = New System.Windows.Forms.ComboBox()
        Me.TxtEntityLegalForm = New System.Windows.Forms.TextBox()
        Me.TxtEntityRegisteredOffice = New System.Windows.Forms.TextBox()
        Me.TxtEntityName = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtKVKNumber = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TxtAnnualReportLocation = New System.Windows.Forms.TextBox()
        Me.CmdSelectAnnualReport = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TxtSaveLocationRP = New System.Windows.Forms.TextBox()
        Me.CmdSelectSaveLocation = New System.Windows.Forms.Button()
        Me.CmdInfo = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'CmdCreate
        '
        Me.CmdCreate.Location = New System.Drawing.Point(632, 512)
        Me.CmdCreate.Name = "CmdCreate"
        Me.CmdCreate.Size = New System.Drawing.Size(75, 23)
        Me.CmdCreate.TabIndex = 18
        Me.CmdCreate.Text = "Create"
        Me.CmdCreate.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CmbArticle408)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.DTPReportingPeriodStartDate)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.CmbArticle403)
        Me.GroupBox1.Controls.Add(Me.CmbDocumentAdoptionStatus)
        Me.GroupBox1.Controls.Add(Me.CmbAuditorReportStatementPresent)
        Me.GroupBox1.Controls.Add(Me.CmbConsolidated)
        Me.GroupBox1.Controls.Add(Me.DTPDocumentAdoptionDate)
        Me.GroupBox1.Controls.Add(Me.DTPReportingPeriod)
        Me.GroupBox1.Controls.Add(Me.DTPReportingPeriodEndDate)
        Me.GroupBox1.Controls.Add(Me.CmbEntitySize)
        Me.GroupBox1.Controls.Add(Me.TxtEntityLegalForm)
        Me.GroupBox1.Controls.Add(Me.TxtEntityRegisteredOffice)
        Me.GroupBox1.Controls.Add(Me.TxtEntityName)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TxtKVKNumber)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(776, 346)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Mandatory elements (RTS - Annex II - 3)"
        '
        'CmbArticle408
        '
        Me.CmbArticle408.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbArticle408.FormattingEnabled = True
        Me.CmbArticle408.Location = New System.Drawing.Point(224, 315)
        Me.CmbArticle408.Name = "CmbArticle408"
        Me.CmbArticle408.Size = New System.Drawing.Size(121, 21)
        Me.CmbArticle408.TabIndex = 14
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(7, 318)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(198, 13)
        Me.Label15.TabIndex = 28
        Me.Label15.Text = "Annual Report Foreign Group Article 408"
        '
        'DTPReportingPeriodStartDate
        '
        Me.DTPReportingPeriodStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPReportingPeriodStartDate.Location = New System.Drawing.Point(224, 155)
        Me.DTPReportingPeriodStartDate.Name = "DTPReportingPeriodStartDate"
        Me.DTPReportingPeriodStartDate.Size = New System.Drawing.Size(121, 20)
        Me.DTPReportingPeriodStartDate.TabIndex = 6
        Me.DTPReportingPeriodStartDate.Value = New Date(2025, 1, 1, 0, 0, 0, 0)
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(7, 187)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(179, 13)
        Me.Label13.TabIndex = 24
        Me.Label13.Text = "Financial Reporting Period End Date"
        '
        'CmbArticle403
        '
        Me.CmbArticle403.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbArticle403.FormattingEnabled = True
        Me.CmbArticle403.Location = New System.Drawing.Point(224, 288)
        Me.CmbArticle403.Name = "CmbArticle403"
        Me.CmbArticle403.Size = New System.Drawing.Size(121, 21)
        Me.CmbArticle403.TabIndex = 13
        '
        'CmbDocumentAdoptionStatus
        '
        Me.CmbDocumentAdoptionStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbDocumentAdoptionStatus.FormattingEnabled = True
        Me.CmbDocumentAdoptionStatus.Location = New System.Drawing.Point(224, 261)
        Me.CmbDocumentAdoptionStatus.Name = "CmbDocumentAdoptionStatus"
        Me.CmbDocumentAdoptionStatus.Size = New System.Drawing.Size(121, 21)
        Me.CmbDocumentAdoptionStatus.TabIndex = 11
        '
        'CmbAuditorReportStatementPresent
        '
        Me.CmbAuditorReportStatementPresent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbAuditorReportStatementPresent.FormattingEnabled = True
        Me.CmbAuditorReportStatementPresent.Location = New System.Drawing.Point(224, 234)
        Me.CmbAuditorReportStatementPresent.Name = "CmbAuditorReportStatementPresent"
        Me.CmbAuditorReportStatementPresent.Size = New System.Drawing.Size(121, 21)
        Me.CmbAuditorReportStatementPresent.TabIndex = 10
        '
        'CmbConsolidated
        '
        Me.CmbConsolidated.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbConsolidated.FormattingEnabled = True
        Me.CmbConsolidated.Location = New System.Drawing.Point(224, 207)
        Me.CmbConsolidated.Name = "CmbConsolidated"
        Me.CmbConsolidated.Size = New System.Drawing.Size(121, 21)
        Me.CmbConsolidated.TabIndex = 9
        '
        'DTPDocumentAdoptionDate
        '
        Me.DTPDocumentAdoptionDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPDocumentAdoptionDate.Location = New System.Drawing.Point(521, 261)
        Me.DTPDocumentAdoptionDate.Name = "DTPDocumentAdoptionDate"
        Me.DTPDocumentAdoptionDate.Size = New System.Drawing.Size(121, 20)
        Me.DTPDocumentAdoptionDate.TabIndex = 12
        Me.DTPDocumentAdoptionDate.Value = New Date(2025, 12, 31, 0, 0, 0, 0)
        '
        'DTPReportingPeriod
        '
        Me.DTPReportingPeriod.CustomFormat = "yyyy"
        Me.DTPReportingPeriod.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPReportingPeriod.Location = New System.Drawing.Point(521, 181)
        Me.DTPReportingPeriod.Name = "DTPReportingPeriod"
        Me.DTPReportingPeriod.Size = New System.Drawing.Size(73, 20)
        Me.DTPReportingPeriod.TabIndex = 8
        Me.DTPReportingPeriod.Value = New Date(2025, 12, 31, 0, 0, 0, 0)
        '
        'DTPReportingPeriodEndDate
        '
        Me.DTPReportingPeriodEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPReportingPeriodEndDate.Location = New System.Drawing.Point(224, 181)
        Me.DTPReportingPeriodEndDate.Name = "DTPReportingPeriodEndDate"
        Me.DTPReportingPeriodEndDate.Size = New System.Drawing.Size(121, 20)
        Me.DTPReportingPeriodEndDate.TabIndex = 7
        Me.DTPReportingPeriodEndDate.Value = New Date(2025, 12, 31, 0, 0, 0, 0)
        '
        'CmbEntitySize
        '
        Me.CmbEntitySize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbEntitySize.FormattingEnabled = True
        Me.CmbEntitySize.Location = New System.Drawing.Point(224, 128)
        Me.CmbEntitySize.Name = "CmbEntitySize"
        Me.CmbEntitySize.Size = New System.Drawing.Size(121, 21)
        Me.CmbEntitySize.TabIndex = 5
        '
        'TxtEntityLegalForm
        '
        Me.TxtEntityLegalForm.Location = New System.Drawing.Point(224, 76)
        Me.TxtEntityLegalForm.Name = "TxtEntityLegalForm"
        Me.TxtEntityLegalForm.Size = New System.Drawing.Size(536, 20)
        Me.TxtEntityLegalForm.TabIndex = 3
        '
        'TxtEntityRegisteredOffice
        '
        Me.TxtEntityRegisteredOffice.Location = New System.Drawing.Point(224, 102)
        Me.TxtEntityRegisteredOffice.Name = "TxtEntityRegisteredOffice"
        Me.TxtEntityRegisteredOffice.Size = New System.Drawing.Size(536, 20)
        Me.TxtEntityRegisteredOffice.TabIndex = 4
        '
        'TxtEntityName
        '
        Me.TxtEntityName.Location = New System.Drawing.Point(224, 50)
        Me.TxtEntityName.Name = "TxtEntityName"
        Me.TxtEntityName.Size = New System.Drawing.Size(536, 20)
        Me.TxtEntityName.TabIndex = 2
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(7, 291)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(198, 13)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "Annual Report Foreign Group Article 403"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(369, 267)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(127, 13)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "Document Adoption Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 267)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(134, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Document Adoption Status"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(369, 187)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(131, 13)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "Financial Reporting Period"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 210)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(169, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Financial Statements Consolidated"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(7, 237)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(119, 13)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Auditors Report Present"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Legal Entity Name"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 79)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(117, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Legal Entity Legal Form"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 105)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(147, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Legal Entity Registered Office"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 131)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Legal Entity Size"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 161)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(182, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Financial Reporting Period Start Date"
        '
        'TxtKVKNumber
        '
        Me.TxtKVKNumber.Location = New System.Drawing.Point(224, 24)
        Me.TxtKVKNumber.Name = "TxtKVKNumber"
        Me.TxtKVKNumber.Size = New System.Drawing.Size(109, 20)
        Me.TxtKVKNumber.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "KVK Registration Number"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TxtAnnualReportLocation)
        Me.GroupBox2.Controls.Add(Me.CmdSelectAnnualReport)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 364)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(776, 68)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Annual report"
        '
        'TxtAnnualReportLocation
        '
        Me.TxtAnnualReportLocation.Location = New System.Drawing.Point(6, 28)
        Me.TxtAnnualReportLocation.Name = "TxtAnnualReportLocation"
        Me.TxtAnnualReportLocation.ReadOnly = True
        Me.TxtAnnualReportLocation.Size = New System.Drawing.Size(674, 20)
        Me.TxtAnnualReportLocation.TabIndex = 14
        Me.TxtAnnualReportLocation.TabStop = False
        '
        'CmdSelectAnnualReport
        '
        Me.CmdSelectAnnualReport.Location = New System.Drawing.Point(686, 26)
        Me.CmdSelectAnnualReport.Name = "CmdSelectAnnualReport"
        Me.CmdSelectAnnualReport.Size = New System.Drawing.Size(75, 23)
        Me.CmdSelectAnnualReport.TabIndex = 15
        Me.CmdSelectAnnualReport.Text = "Select"
        Me.CmdSelectAnnualReport.UseVisualStyleBackColor = True
        '
        'CmdClose
        '
        Me.CmdClose.Location = New System.Drawing.Point(713, 512)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.Size = New System.Drawing.Size(75, 23)
        Me.CmdClose.TabIndex = 19
        Me.CmdClose.Text = "Close"
        Me.CmdClose.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TxtSaveLocationRP)
        Me.GroupBox3.Controls.Add(Me.CmdSelectSaveLocation)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 438)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(776, 68)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Save location Report Package"
        '
        'TxtSaveLocationRP
        '
        Me.TxtSaveLocationRP.Location = New System.Drawing.Point(6, 28)
        Me.TxtSaveLocationRP.Name = "TxtSaveLocationRP"
        Me.TxtSaveLocationRP.ReadOnly = True
        Me.TxtSaveLocationRP.Size = New System.Drawing.Size(674, 20)
        Me.TxtSaveLocationRP.TabIndex = 14
        Me.TxtSaveLocationRP.TabStop = False
        '
        'CmdSelectSaveLocation
        '
        Me.CmdSelectSaveLocation.Location = New System.Drawing.Point(686, 26)
        Me.CmdSelectSaveLocation.Name = "CmdSelectSaveLocation"
        Me.CmdSelectSaveLocation.Size = New System.Drawing.Size(75, 23)
        Me.CmdSelectSaveLocation.TabIndex = 16
        Me.CmdSelectSaveLocation.Text = "Select"
        Me.CmdSelectSaveLocation.UseVisualStyleBackColor = True
        '
        'CmdInfo
        '
        Me.CmdInfo.Location = New System.Drawing.Point(12, 512)
        Me.CmdInfo.Name = "CmdInfo"
        Me.CmdInfo.Size = New System.Drawing.Size(75, 23)
        Me.CmdInfo.TabIndex = 17
        Me.CmdInfo.Text = "Info"
        Me.CmdInfo.UseVisualStyleBackColor = True
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 540)
        Me.Controls.Add(Me.CmdInfo)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.CmdClose)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CmdCreate)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmMain"
        Me.Text = "SBR Creation for Other GAAP Package"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CmdCreate As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents CmdClose As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtKVKNumber As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents CmbEntitySize As ComboBox
    Friend WithEvents TxtEntityLegalForm As TextBox
    Friend WithEvents TxtEntityRegisteredOffice As TextBox
    Friend WithEvents TxtEntityName As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents DTPReportingPeriodEndDate As DateTimePicker
    Friend WithEvents CmbDocumentAdoptionStatus As ComboBox
    Friend WithEvents CmbAuditorReportStatementPresent As ComboBox
    Friend WithEvents CmbConsolidated As ComboBox
    Friend WithEvents DTPDocumentAdoptionDate As DateTimePicker
    Friend WithEvents DTPReportingPeriod As DateTimePicker
    Friend WithEvents CmbArticle403 As ComboBox
    Friend WithEvents TxtAnnualReportLocation As TextBox
    Friend WithEvents CmdSelectAnnualReport As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents TxtSaveLocationRP As TextBox
    Friend WithEvents CmdSelectSaveLocation As Button
    Friend WithEvents DTPReportingPeriodStartDate As DateTimePicker
    Friend WithEvents Label13 As Label
    Friend WithEvents CmdInfo As Button
    Friend WithEvents CmbArticle408 As ComboBox
    Friend WithEvents Label15 As Label
End Class
