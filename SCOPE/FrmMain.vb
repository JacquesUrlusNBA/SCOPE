Imports System.Diagnostics.Eventing
Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
Imports System.Security.Policy
Imports System.Text
Imports System.Text.Json
Imports System.Text.Json.Serialization
Imports System.Xml
Imports DevExpress
Imports DevExpress.Xpo
Imports MS.Win32


Public Class FrmMain

    Public Const StrEntryPoint As String = "https://www.nltaxonomie.nl/kvk/2025-12-31/kvk-annual-report-other.xsd"

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '=============================
        ' Fill initial controls
        '=============================

        CmbEntitySize.Items.Add("Micro")
        CmbEntitySize.Items.Add("Small")
        CmbEntitySize.Items.Add("Medium")
        CmbEntitySize.Items.Add("Large")
        CmbEntitySize.SelectedIndex = 0

        CmbConsolidated.Items.Add("No")
        CmbConsolidated.Items.Add("Yes")
        CmbConsolidated.SelectedIndex = 0

        CmbDocumentAdoptionStatus.Items.Add("Not adopted")
        CmbDocumentAdoptionStatus.Items.Add("Adopted")
        CmbDocumentAdoptionStatus.SelectedIndex = 0

        CmbAuditorReportStatementPresent.Items.Add("No")
        CmbAuditorReportStatementPresent.Items.Add("Yes")
        CmbAuditorReportStatementPresent.SelectedIndex = 0

        CmbArticle403.Items.Add("No")
        CmbArticle403.Items.Add("Yes")
        CmbArticle403.SelectedIndex = 0

        CmbArticle408.Items.Add("No")
        CmbArticle408.Items.Add("Yes")
        CmbArticle408.SelectedIndex = 0

    End Sub

    Private Sub CmdSelect_Click(sender As Object, e As EventArgs) Handles CmdSelectAnnualReport.Click

        '=============================
        ' Get the location of the annual report
        '=============================

        Dim OpenFileDialogTmp As New OpenFileDialog

        OpenFileDialogTmp.Filter = "Annual report (*.HTML;*.XHTML)|*.HTML;*.XHTML"
        OpenFileDialogTmp.Title = "Select annual report"
        OpenFileDialogTmp.Multiselect = False

        Dim result As DialogResult = OpenFileDialogTmp.ShowDialog

        If result = DialogResult.OK Then

            TxtAnnualReportLocation.Text = OpenFileDialogTmp.FileName

        End If

    End Sub

    Private Sub CmdSelectSaveLocation_Click(sender As Object, e As EventArgs) Handles CmdSelectSaveLocation.Click

        '=============================
        ' Get the save location of the Report Package
        '=============================

        Dim FolderBrowserDialogTmp As New FolderBrowserDialog

        FolderBrowserDialogTmp.Description = "Select save location Report Package"

        Dim result As DialogResult = FolderBrowserDialogTmp.ShowDialog

        If result = DialogResult.OK Then

            TxtSaveLocationRP.Text = FolderBrowserDialogTmp.SelectedPath

        End If


    End Sub

    Private Sub CmdCreate_Click(sender As Object, e As EventArgs) Handles CmdCreate.Click

        '=============================
        ' Check if all necessary data is being provided
        '=============================

        If String.IsNullOrEmpty(TxtKVKNumber.Text) Or
        String.IsNullOrEmpty(TxtEntityName.Text) Or
        String.IsNullOrEmpty(TxtEntityLegalForm.Text) Or
        String.IsNullOrEmpty(TxtEntityRegisteredOffice.Text) Or
        String.IsNullOrEmpty(TxtAnnualReportLocation.Text) Or
        String.IsNullOrEmpty(TxtSaveLocationRP.Text) Then

            MsgBox("Not all data for creating a Report Package is provided.", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "SCOPE")

            Exit Sub

        End If

        '=============================
        ' Define base directory
        '=============================

        Dim StrBaseDirectory As String = $"{TxtKVKNumber.Text}-{DTPReportingPeriodEndDate.Value:yyyy-MM-dd}-en"

        '=============================
        ' Create folder structure
        '=============================

        SubCreateFolderStructure(StrBaseDirectory)

        '=============================
        ' Create reportPackage.json
        '=============================

        SubCreateReportPackageJSON(StrBaseDirectory)

        '=============================
        ' Create KVK-file
        '=============================

        SubCreateKVKFile(StrBaseDirectory)

        '=============================
        ' Copy annual report
        '=============================

        SubCopyAnnualReport(StrBaseDirectory)

        '=============================
        ' Create xbri-file
        '=============================

        SubCreateXBRIFile(StrBaseDirectory)

        '=============================
        ' Remove files in TEMP folder
        '=============================

        SubDeleteTempFolder()

        '=============================
        ' Inform 
        '=============================

        MsgBox($"Report Package for Other GAAP {TxtSaveLocationRP.Text}\{StrBaseDirectory}.xbri is succesfully created.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SCOPE")

    End Sub

    Private Sub SubCreateXBRIFile(StrBaseDirectory As String)

        Try

            Dim StrSource As String = $"{My.Computer.FileSystem.SpecialDirectories.Temp}/SCOPE/{StrBaseDirectory}/"
            Dim StrXBRI As String = $"{TxtSaveLocationRP.Text}\{StrBaseDirectory}.xbri"

            ZipFile.CreateFromDirectory(StrSource, StrXBRI, CompressionLevel.Optimal, True)

        Catch ex As Exception

            MsgBox("Could not create the Report Package. Please check whether you have sufficient permissions.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SCOPE")

            SubDeleteTempFolder()

            Me.Close()

        End Try


    End Sub

    Private Sub SubDeleteTempFolder()

        '==============================================
        ' Remove files from temp SCOPE directory 
        '==============================================

        My.Computer.FileSystem.DeleteDirectory($"{My.Computer.FileSystem.SpecialDirectories.Temp}/SCOPE", FileIO.DeleteDirectoryOption.DeleteAllContents)

    End Sub


    Private Sub SubCopyAnnualReport(StrBaseDirectory As String)

        My.Computer.FileSystem.CopyFile(TxtAnnualReportLocation.Text, $"{My.Computer.FileSystem.SpecialDirectories.Temp}/SCOPE/{StrBaseDirectory}/{StrBaseDirectory}/reports/annualreport/{Path.GetFileName(TxtAnnualReportLocation.Text)}")

    End Sub

    Private Sub SubCreateKVKFile(StrBaseDirectory As String)

        '==============================================
        ' Create the KVK-file
        '==============================================

        Dim Doc As New XmlDocument()
        Dim Attr As XmlAttribute

        Dim declaration As XmlDeclaration = Doc.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
        Doc.AppendChild(declaration)

        '==============================================
        ' Part 1 - html 
        '==============================================

        Dim html As XmlElement = Doc.CreateElement("html", "http://www.w3.org/1999/xhtml")

        html.SetAttribute("xmlns", "http://www.w3.org/1999/xhtml")
        html.SetAttribute("xml:lang", "en")
        html.SetAttribute("xmlns:ix", "http://www.xbrl.org/2013/inlineXBRL")
        html.SetAttribute("xmlns:xbrli", "http://www.xbrl.org/2003/instance")
        html.SetAttribute("xmlns:xl", "http://www.xbrl.org/2003/XLink")
        html.SetAttribute("xmlns:xbrldi", "http://xbrl.org/2006/xbrldi")
        html.SetAttribute("xmlns:xbrldt", "http://xbrl.org/2005/xbrldt")
        html.SetAttribute("xmlns:link", "http://www.xbrl.org/2003/linkbase")
        html.SetAttribute("xmlns:ixt", "http://www.xbrl.org/inlineXBRL/transformation/2020-02-12")
        html.SetAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink")
        html.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        html.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema")
        html.SetAttribute("xmlns:bw2-titel9", "https://www.nltaxonomie.nl/bw2-titel9/2025-12-31/bw2-titel9-cor")
        html.SetAttribute("xmlns:rj", "https://www.nltaxonomie.nl/rj/2025-12-31/rj-cor")
        html.SetAttribute("xmlns:kvk", "https://www.nltaxonomie.nl/kvk/2025-12-31/kvk-cor")

        Doc.AppendChild(html)

        '==============================================
        ' Part 2 - head
        '==============================================

        Dim head As XmlElement = Doc.CreateElement("head", "http://www.w3.org/1999/xhtml")

        html.AppendChild(head)

        ' meta

        Dim meta As XmlElement = Doc.CreateElement("meta", "http://www.w3.org/1999/xhtml")

        head.AppendChild(meta)

        Attr = Doc.CreateAttribute("http-equiv")
        Attr.Value = "Content-Type"
        meta.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("content")
        Attr.Value = "text/html; charset=UTF-8"
        meta.SetAttributeNode(Attr)

        ' title

        Dim title As XmlElement = Doc.CreateElement("title", "http://www.w3.org/1999/xhtml")
        head.AppendChild(title)
        title.InnerText = "Filing information annual report"

        'style

        Dim style As XmlElement = Doc.CreateElement("style", "http://www.w3.org/1999/xhtml")
        head.AppendChild(style)

        Attr = Doc.CreateAttribute("type")
        Attr.Value = "text/css"
        style.SetAttributeNode(Attr)

        style.InnerText = FunCreateCSS()

        '==============================================
        ' Part 3 - body
        '==============================================

        Dim body As XmlElement = Doc.CreateElement("body", "http://www.w3.org/1999/xhtml")

        html.AppendChild(body)

        Dim divstyle As XmlElement = Doc.CreateElement("div", "http://www.w3.org/1999/xhtml")
        body.AppendChild(divstyle)

        Attr = Doc.CreateAttribute("style")
        Attr.Value = "display:none"
        divstyle.SetAttributeNode(Attr)

        Dim header As XmlElement = Doc.CreateElement("ix:header", "http://www.xbrl.org/2013/inlineXBRL")

        divstyle.AppendChild(header)

        'References

        Dim references As XmlElement = Doc.CreateElement("ix:references", "http://www.xbrl.org/2013/inlineXBRL")

        header.AppendChild(references)

        ' Write entrypoint/schema info

        Dim schemaInfo As XmlElement = Doc.CreateElement("link:schemaRef", "http://www.xbrl.org/2003/linkbase")

        Attr = Doc.CreateAttribute("xlink:type", "http://www.w3.org/1999/xlink")
        Attr.Value = "simple"
        schemaInfo.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("xlink:arcrole", "http://www.w3.org/1999/xlink")
        Attr.Value = "http://www.w3.org/1999/xlink/properties/linkbase"
        schemaInfo.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("xlink:href", "http://www.w3.org/1999/xlink")
        Attr.Value = StrEntryPoint
        schemaInfo.SetAttributeNode(Attr)

        references.AppendChild(schemaInfo)

        ' Write resources

        Dim resources As XmlElement = Doc.CreateElement("ix:resources", "http://www.xbrl.org/2013/inlineXBRL")
        header.AppendChild(resources)

        ' Write context

        Dim context As XmlElement = Doc.CreateElement("xbrli:context", "http://www.xbrl.org/2003/instance")
        Attr = Doc.CreateAttribute("id")
        Attr.Value = "C1"
        context.SetAttributeNode(Attr)

        resources.AppendChild(context)

        ' Write entity

        Dim entity As XmlElement = Doc.CreateElement("xbrli:entity", "http://www.xbrl.org/2003/instance")
        context.AppendChild(entity)

        Dim identifier As XmlElement = Doc.CreateElement("xbrli:identifier", "http://www.xbrl.org/2003/instance")
        Attr = Doc.CreateAttribute("scheme")
        Attr.Value = "http://www.kvk.nl/kvk-id"
        identifier.SetAttributeNode(Attr)
        identifier.InnerText = TxtKVKNumber.Text

        entity.AppendChild(identifier)

        ' Write period

        Dim period As XmlElement = Doc.CreateElement("xbrli:period", "http://www.xbrl.org/2003/instance")
        context.AppendChild(period)

        ' Write dates

        Dim startDate As XmlElement = Doc.CreateElement("xbrli:startDate", "http://www.xbrl.org/2003/instance")
        period.AppendChild(startDate)
        startDate.InnerText = $"{DTPReportingPeriodStartDate.Value:yyyy-MM-dd}"

        Dim endDate As XmlElement = Doc.CreateElement("xbrli:endDate", "http://www.xbrl.org/2003/instance")
        period.AppendChild(endDate)
        endDate.InnerText = $"{DTPReportingPeriodEndDate.Value:yyyy-MM-dd}"

        'Write hidden

        Dim hidden As XmlElement = Doc.CreateElement("ix:hidden", "http://www.xbrl.org/2013/inlineXBRL")
        header.AppendChild(hidden)

        Dim nonNumericHidden As XmlElement = Doc.CreateElement("ix:nonNumeric", "http://www.xbrl.org/2013/inlineXBRL")
        hidden.AppendChild(nonNumericHidden)

        Attr = Doc.CreateAttribute("contextRef")
        Attr.Value = "C1"
        nonNumericHidden.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("name")
        Attr.Value = "kvk:LegalEntitySize"
        nonNumericHidden.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("id")
        Attr.Value = "ID5"
        nonNumericHidden.SetAttributeNode(Attr)

        Select Case CmbEntitySize.SelectedIndex
            Case 0 'Micro
                nonNumericHidden.InnerText = "https://www.nltaxonomie.nl/kvk/2025-12-31/kvk-cor#LegalEntitySizeMicroMember"
            Case 1 'Small
                nonNumericHidden.InnerText = "https://www.nltaxonomie.nl/kvk/2025-12-31/kvk-cor#LegalEntitySizeSmallMember"
            Case 2 'Medium
                nonNumericHidden.InnerText = "https://www.nltaxonomie.nl/kvk/2025-12-31/kvk-cor#LegalEntitySizeMediumMember"
            Case 3 'large
                nonNumericHidden.InnerText = "https://www.nltaxonomie.nl/kvk/2025-12-31/kvk-cor#LegalEntitySizeLargeMember"
        End Select

        ' Start page

        Dim divpage As XmlElement = Doc.CreateElement("div", "http://www.w3.org/1999/xhtml")
        body.AppendChild(divpage)

        Attr = Doc.CreateAttribute("class")
        Attr.Value = "page"
        divpage.SetAttributeNode(Attr)

        ' Write visible header 

        Dim h1 As XmlElement = Doc.CreateElement("h1", "http://www.w3.org/1999/xhtml")
        h1.InnerText = "Filing information annual report Dutch Chamber of Commerce"

        divpage.AppendChild(h1)

        'Dim br As XmlElement = Doc.CreateElement("br", "http://www.w3.org/1999/xhtml")
        'divpage.AppendChild(br)

        Dim h2 As XmlElement = Doc.CreateElement("h2", "http://www.w3.org/1999/xhtml")
        h2.InnerText = "Filing data"

        divpage.AppendChild(h2)

        ' Start with table

        Dim table As XmlElement = Doc.CreateElement("table", "http://www.w3.org/1999/xhtml")
        divpage.AppendChild(table)

        ' Legal entity name - bw2-titel9:LegalEntityName

        Dim row1 As XmlElement = Doc.CreateElement("tr", "http://www.w3.org/1999/xhtml")
        table.AppendChild(row1)

        Dim column1L As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row1.AppendChild(column1L)
        column1L.InnerText = "Legal entity name:"

        Dim column1R As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row1.AppendChild(column1R)

        Dim nonNumeric1 As XmlElement = Doc.CreateElement("ix:nonNumeric", "http://www.xbrl.org/2013/inlineXBRL")
        column1R.AppendChild(nonNumeric1)

        Attr = Doc.CreateAttribute("contextRef")
        Attr.Value = "C1"
        nonNumeric1.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("name")
        Attr.Value = "bw2-titel9:LegalEntityName"
        nonNumeric1.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("id")
        Attr.Value = "ID1"
        nonNumeric1.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("escape")
        Attr.Value = "false"
        nonNumeric1.SetAttributeNode(Attr)

        nonNumeric1.InnerText = TxtEntityName.Text

        ' Legal entity form - bw2-titel9:LegalEntityLegalForm

        Dim row2 As XmlElement = Doc.CreateElement("tr", "http://www.w3.org/1999/xhtml")
        table.AppendChild(row2)

        Dim column2L As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row2.AppendChild(column2L)
        column2L.InnerText = "Legal entity form:"

        Dim column2R As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row2.AppendChild(column2R)

        Dim nonNumeric2 As XmlElement = Doc.CreateElement("ix:nonNumeric", "http://www.xbrl.org/2013/inlineXBRL")
        column2R.AppendChild(nonNumeric2)

        Attr = Doc.CreateAttribute("contextRef")
        Attr.Value = "C1"
        nonNumeric2.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("name")
        Attr.Value = "bw2-titel9:LegalEntityLegalForm"
        nonNumeric2.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("id")
        Attr.Value = "ID2"
        nonNumeric2.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("escape")
        Attr.Value = "false"
        nonNumeric2.SetAttributeNode(Attr)

        nonNumeric2.InnerText = TxtEntityLegalForm.Text

        ' Registered office - bw2-titel9:LegalEntityRegisteredOffice

        Dim row3 As XmlElement = Doc.CreateElement("tr", "http://www.w3.org/1999/xhtml")
        table.AppendChild(row3)

        Dim column3L As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row3.AppendChild(column3L)
        column3L.InnerText = "Registered office:"

        Dim column3R As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row3.AppendChild(column3R)

        Dim nonNumeric3 As XmlElement = Doc.CreateElement("ix:nonNumeric", "http://www.xbrl.org/2013/inlineXBRL")
        column3R.AppendChild(nonNumeric3)

        Attr = Doc.CreateAttribute("contextRef")
        Attr.Value = "C1"
        nonNumeric3.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("name")
        Attr.Value = "bw2-titel9:LegalEntityRegisteredOffice"
        nonNumeric3.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("id")
        Attr.Value = "ID3"
        nonNumeric3.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("escape")
        Attr.Value = "false"
        nonNumeric3.SetAttributeNode(Attr)

        nonNumeric3.InnerText = TxtEntityRegisteredOffice.Text

        ' KVK RegistrationNumber - bw2-titel9:ChamberOfCommerceRegistrationNumber

        Dim row4 As XmlElement = Doc.CreateElement("tr", "http://www.w3.org/1999/xhtml")
        table.AppendChild(row4)

        Dim column4L As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row4.AppendChild(column4L)
        column4L.InnerText = "KVK-number:"

        Dim column4R As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row4.AppendChild(column4R)

        Dim nonNumeric4 As XmlElement = Doc.CreateElement("ix:nonNumeric", "http://www.xbrl.org/2013/inlineXBRL")
        column4R.AppendChild(nonNumeric4)

        Attr = Doc.CreateAttribute("contextRef")
        Attr.Value = "C1"
        nonNumeric4.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("name")
        Attr.Value = "bw2-titel9:ChamberOfCommerceRegistrationNumber"
        nonNumeric4.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("id")
        Attr.Value = "ID4"
        nonNumeric4.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("escape")
        Attr.Value = "false"
        nonNumeric4.SetAttributeNode(Attr)

        nonNumeric4.InnerText = TxtKVKNumber.Text

        ' Legal entity size - kvk:LegalEntitySize

        Dim row5 As XmlElement = Doc.CreateElement("tr", "http://www.w3.org/1999/xhtml")
        table.AppendChild(row5)

        Dim column5L As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row5.AppendChild(column5L)
        column5L.InnerText = "Legal entity size:"

        Dim column5R As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row5.AppendChild(column5R)

        Dim span As XmlElement = Doc.CreateElement("span", "http://www.w3.org/1999/xhtml")
        column5R.AppendChild(span)

        Attr = Doc.CreateAttribute("style")
        Attr.Value = "-ix-hidden:ID5"
        span.SetAttributeNode(Attr)

        span.InnerText = CmbEntitySize.SelectedItem

        ' Financial reporting period end date - bw2-titel9:FinancialReportingPeriodEndDate

        Dim row6 As XmlElement = Doc.CreateElement("tr", "http://www.w3.org/1999/xhtml")
        table.AppendChild(row6)

        Dim column6L As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row6.AppendChild(column6L)
        column6L.InnerText = "Financial reporting period end date:"

        Dim column6R As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row6.AppendChild(column6R)

        Dim nonNumeric6 As XmlElement = Doc.CreateElement("ix:nonNumeric", "http://www.xbrl.org/2013/inlineXBRL")
        column6R.AppendChild(nonNumeric6)

        Attr = Doc.CreateAttribute("contextRef")
        Attr.Value = "C1"
        nonNumeric6.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("name")
        Attr.Value = "bw2-titel9:FinancialReportingPeriodEndDate"
        nonNumeric6.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("id")
        Attr.Value = "ID6"
        nonNumeric6.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("format")
        Attr.Value = "ixt:date-day-monthname-year-en"
        nonNumeric6.SetAttributeNode(Attr)

        nonNumeric6.InnerText = DTPReportingPeriodEndDate.Value.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("en-US"))

        ' Financial reporting period - bw2-titel9:FinancialReportingPeriod

        Dim row7 As XmlElement = Doc.CreateElement("tr", "http://www.w3.org/1999/xhtml")
        table.AppendChild(row7)

        Dim column7L As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row7.AppendChild(column7L)
        column7L.InnerText = "Financial reporting period:"

        Dim column7R As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row7.AppendChild(column7R)

        Dim nonNumeric7 As XmlElement = Doc.CreateElement("ix:nonNumeric", "http://www.xbrl.org/2013/inlineXBRL")
        column7R.AppendChild(nonNumeric7)

        Attr = Doc.CreateAttribute("contextRef")
        Attr.Value = "C1"
        nonNumeric7.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("name")
        Attr.Value = "bw2-titel9:FinancialReportingPeriod"
        nonNumeric7.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("id")
        Attr.Value = "ID7"
        nonNumeric7.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("escape")
        Attr.Value = "false"
        nonNumeric7.SetAttributeNode(Attr)

        nonNumeric7.InnerText = DTPReportingPeriodEndDate.Value.ToString("yyyy")

        ' Financial statements consolidated - rj:FinancialStatementsConsolidated

        Dim row8 As XmlElement = Doc.CreateElement("tr", "http://www.w3.org/1999/xhtml")
        table.AppendChild(row8)

        Dim column8L As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row8.AppendChild(column8L)
        column8L.InnerText = "Financial statements consolidated:"

        Dim column8R As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row8.AppendChild(column8R)

        Dim nonNumeric8 As XmlElement = Doc.CreateElement("ix:nonNumeric", "http://www.xbrl.org/2013/inlineXBRL")
        column8R.AppendChild(nonNumeric8)

        Attr = Doc.CreateAttribute("contextRef")
        Attr.Value = "C1"
        nonNumeric8.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("name")
        Attr.Value = "rj:FinancialStatementsConsolidated"
        nonNumeric8.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("id")
        Attr.Value = "ID8"
        nonNumeric8.SetAttributeNode(Attr)

        Select Case CmbConsolidated.SelectedIndex

            Case 0 'No

                Attr = Doc.CreateAttribute("format")
                Attr.Value = "ixt:fixed-false"
                nonNumeric8.SetAttributeNode(Attr)

                nonNumeric8.InnerText = "No"

            Case 1 'Yes

                Attr = Doc.CreateAttribute("format")
                Attr.Value = "ixt:fixed-true"
                nonNumeric8.SetAttributeNode(Attr)

                nonNumeric8.InnerText = "Yes"

        End Select

        ' Auditors report present - kvk:AuditorsReportFinancialStatementsPresent

        Dim row9 As XmlElement = Doc.CreateElement("tr", "http://www.w3.org/1999/xhtml")
        table.AppendChild(row9)

        Dim column9L As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row9.AppendChild(column9L)
        column9L.InnerText = "Auditors report financial statement present:"

        Dim column9R As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        row9.AppendChild(column9R)

        Dim nonNumeric9 As XmlElement = Doc.CreateElement("ix:nonNumeric", "http://www.xbrl.org/2013/inlineXBRL")
        column9R.AppendChild(nonNumeric9)

        Attr = Doc.CreateAttribute("contextRef")
        Attr.Value = "C1"
        nonNumeric9.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("name")
        Attr.Value = "kvk:AuditorsReportFinancialStatementsPresent"
        nonNumeric9.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("id")
        Attr.Value = "ID9"
        nonNumeric9.SetAttributeNode(Attr)

        Select Case CmbAuditorReportStatementPresent.SelectedIndex

            Case 0 'No

                Attr = Doc.CreateAttribute("format")
                Attr.Value = "ixt:fixed-false"
                nonNumeric9.SetAttributeNode(Attr)

                nonNumeric9.InnerText = "No"

            Case 1 'Yes

                Attr = Doc.CreateAttribute("format")
                Attr.Value = "ixt:fixed-true"
                nonNumeric9.SetAttributeNode(Attr)

                nonNumeric9.InnerText = "Yes"

        End Select

        ' Document adoption status - bw2-titel9:DocumentAdoptionStatus

        Dim rowA As XmlElement = Doc.CreateElement("tr", "http://www.w3.org/1999/xhtml")
        table.AppendChild(rowA)

        Dim columnAL As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        rowA.AppendChild(columnAL)
        columnAL.InnerText = "Document adopted:"

        Dim columnAR As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
        rowA.AppendChild(columnAR)

        Dim nonNumericA As XmlElement = Doc.CreateElement("ix:nonNumeric", "http://www.xbrl.org/2013/inlineXBRL")
        columnAR.AppendChild(nonNumericA)

        Attr = Doc.CreateAttribute("contextRef")
        Attr.Value = "C1"
        nonNumericA.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("name")
        Attr.Value = "bw2-titel9:DocumentAdoptionStatus"
        nonNumericA.SetAttributeNode(Attr)

        Attr = Doc.CreateAttribute("id")
        Attr.Value = "ID10"
        nonNumericA.SetAttributeNode(Attr)

        Select Case CmbDocumentAdoptionStatus.SelectedIndex

            Case 0 'No

                Attr = Doc.CreateAttribute("format")
                Attr.Value = "ixt:fixed-false"
                nonNumericA.SetAttributeNode(Attr)

                nonNumericA.InnerText = "No"

            Case 1 'Yes

                Attr = Doc.CreateAttribute("format")
                Attr.Value = "ixt:fixed-true"
                nonNumericA.SetAttributeNode(Attr)

                nonNumericA.InnerText = "Yes"

        End Select

        ' Document adoption date - bw2-titel9:DocumentAdoptionDate

        If CmbDocumentAdoptionStatus.SelectedIndex = 1 Then

            Dim rowB As XmlElement = Doc.CreateElement("tr", "http://www.w3.org/1999/xhtml")
            table.AppendChild(rowB)

            Dim columnBL As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
            rowB.AppendChild(columnBL)
            columnBL.InnerText = "Document adoption date:"

            Dim columnBR As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
            rowB.AppendChild(columnBR)

            Dim nonNumericB As XmlElement = Doc.CreateElement("ix:nonNumeric", "http://www.xbrl.org/2013/inlineXBRL")
            columnBR.AppendChild(nonNumericB)

            Attr = Doc.CreateAttribute("contextRef")
            Attr.Value = "C1"
            nonNumericB.SetAttributeNode(Attr)

            Attr = Doc.CreateAttribute("name")
            Attr.Value = "bw2-titel9:DocumentAdoptionDate"
            nonNumericB.SetAttributeNode(Attr)

            Attr = Doc.CreateAttribute("id")
            Attr.Value = "ID11"
            nonNumericB.SetAttributeNode(Attr)

            Attr = Doc.CreateAttribute("format")
            Attr.Value = "ixt:date-day-monthname-year-en"
            nonNumericB.SetAttributeNode(Attr)

            nonNumericB.InnerText = DTPDocumentAdoptionDate.Value.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("en-US"))

        End If

        ' Annual report of foreign grouphead for exemption under article 403 -  kvk:AnnualReportOfForeignGroupHeadForExemptionUnderArticle403

        If CmbArticle403.SelectedIndex = 1 Then 'Only add if relevant

            Dim rowC As XmlElement = Doc.CreateElement("tr", "http://www.w3.org/1999/xhtml")
            table.AppendChild(rowC)

            Dim columnCL As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
            rowC.AppendChild(columnCL)
            columnCL.InnerText = "Annual report of foreign grouphead for exemption under article 403:"

            Dim columnCR As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
            rowC.AppendChild(columnCR)

            Dim nonNumericC As XmlElement = Doc.CreateElement("ix:nonNumeric", "http://www.xbrl.org/2013/inlineXBRL")
            columnCR.AppendChild(nonNumericC)

            Attr = Doc.CreateAttribute("contextRef")
            Attr.Value = "C1"
            nonNumericC.SetAttributeNode(Attr)

            Attr = Doc.CreateAttribute("name")
            Attr.Value = "kvk:AnnualReportOfForeignGroupHeadForExemptionUnderArticle403"
            nonNumericC.SetAttributeNode(Attr)

            Attr = Doc.CreateAttribute("id")
            Attr.Value = "ID12"
            nonNumericC.SetAttributeNode(Attr)

            Attr = Doc.CreateAttribute("format")
            Attr.Value = "ixt:fixed-true"
            nonNumericC.SetAttributeNode(Attr)

            nonNumericC.InnerText = "Yes"

        End If

        ' Annual report of foreign grouphead for exemption under article 408 - kvk:AnnualReportOfForeignGroupHeadForExemptionUnderArticle408

        If CmbArticle408.SelectedIndex = 1 Then 'Only add if relevant

            Dim rowD As XmlElement = Doc.CreateElement("tr", "http://www.w3.org/1999/xhtml")
            table.AppendChild(rowD)

            Dim columnDL As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
            rowD.AppendChild(columnDL)
            columnDL.InnerText = "Annual report of foreign grouphead for exemption under article 408:"

            Dim columnDR As XmlElement = Doc.CreateElement("td", "http://www.w3.org/1999/xhtml")
            rowD.AppendChild(columnDR)

            Dim nonNumericD As XmlElement = Doc.CreateElement("ix:nonNumeric", "http://www.xbrl.org/2013/inlineXBRL")
            columnDR.AppendChild(nonNumericD)

            Attr = Doc.CreateAttribute("contextRef")
            Attr.Value = "C1"
            nonNumericD.SetAttributeNode(Attr)

            Attr = Doc.CreateAttribute("name")
            Attr.Value = "kvk:AnnualReportOfForeignGroupHeadForExemptionUnderArticle408"
            nonNumericD.SetAttributeNode(Attr)

            Attr = Doc.CreateAttribute("id")
            Attr.Value = "ID13"
            nonNumericD.SetAttributeNode(Attr)

            Attr = Doc.CreateAttribute("format")
            Attr.Value = "ixt:fixed-true"
            nonNumericD.SetAttributeNode(Attr)

            nonNumericD.InnerText = "Yes"

        End If

        Doc.Save($"{My.Computer.FileSystem.SpecialDirectories.Temp}/SCOPE/{StrBaseDirectory}/{StrBaseDirectory}/reports/annualreport/kvk-{DTPReportingPeriodEndDate.Value:yyyy-MM-dd}-en.xhtml")

    End Sub

    Private Function FunCreateCSS() As String

        '==============================================
        ' Function to create CSS
        '==============================================

        Dim Sb As New StringBuilder

        Sb.AppendLine("html {")
        Sb.AppendLine("Font-family: ""Roboto"", Arial, Helvetica, sans-serif;")
        Sb.AppendLine("Font-Size: 15pt;")
        Sb.Append("}")
        Sb.AppendLine("")
        Sb.AppendLine("h1,")
        Sb.AppendLine("h2 {")
        Sb.AppendLine("color: #007BFF;")
        Sb.AppendLine("}")

        Return Sb.ToString

    End Function


    Private Sub SubCreateFolderStructure(StrBaseDirectory As String)

        '==============================================
        ' Remove files from temp SCOPE directory - to be sure
        '==============================================

        If My.Computer.FileSystem.DirectoryExists($"{My.Computer.FileSystem.SpecialDirectories.Temp}/SCOPE") Then

            My.Computer.FileSystem.DeleteDirectory($"{My.Computer.FileSystem.SpecialDirectories.Temp}/SCOPE", FileIO.DeleteDirectoryOption.DeleteAllContents)

        End If

        '==============================================
        ' Create the directories
        '==============================================

        My.Computer.FileSystem.CreateDirectory($"{My.Computer.FileSystem.SpecialDirectories.Temp}/SCOPE")

        My.Computer.FileSystem.CreateDirectory($"{My.Computer.FileSystem.SpecialDirectories.Temp}/SCOPE/{StrBaseDirectory}/{StrBaseDirectory}")

        My.Computer.FileSystem.CreateDirectory($"{My.Computer.FileSystem.SpecialDirectories.Temp}/SCOPE/{StrBaseDirectory}/{StrBaseDirectory}/reports")

        My.Computer.FileSystem.CreateDirectory($"{My.Computer.FileSystem.SpecialDirectories.Temp}/SCOPE/{StrBaseDirectory}/{StrBaseDirectory}/reports/annualreport")

        My.Computer.FileSystem.CreateDirectory($"{My.Computer.FileSystem.SpecialDirectories.Temp}/SCOPE/{StrBaseDirectory}/{StrBaseDirectory}/META-INF")

    End Sub

    Private Sub SubCreateReportPackageJSON(StrBaseDirectory As String)

        '============================ 
        ' Create reportPackage.json  
        '============================ 

        Dim NewReportPackageJSON As New ReportPackageJSON()

        Dim NewDocumentType As New DocumentType()

        NewDocumentType.DocumentType = "https://xbrl.org/report-package/2023/xbri"

        NewReportPackageJSON.DocumentInfo = NewDocumentType

        Dim StrJSON As String = JsonSerializer.Serialize(NewReportPackageJSON, New JsonSerializerOptions With {.WriteIndented = True})

        File.WriteAllText($"{My.Computer.FileSystem.SpecialDirectories.Temp}/SCOPE/{StrBaseDirectory}/{StrBaseDirectory}/META-INF/reportPackage.json", StrJSON)

    End Sub

    Private Sub CmdClose_Click(sender As Object, e As EventArgs) Handles CmdClose.Click

        '=============================
        ' Exit the application
        '============================

        Me.Close()

    End Sub

    Public Class DocumentType

        '============================ 
        ' Class for the content of reportPackage.json  
        '============================ 

        <JsonPropertyName("documentType")>
        Public Property DocumentType As String

    End Class

    Public Class ReportPackageJSON

        '============================ 
        ' Class for the content of reportPackage.json  
        '============================ 

        <JsonPropertyName("documentInfo")>
        Public Property DocumentInfo As DocumentType

    End Class

    Private Sub CmdInfo_Click(sender As Object, e As EventArgs) Handles CmdInfo.Click

        Dim Sb As New StringBuilder

        Sb.AppendLine("This tool is being created by the Royal Netherlands Institute of Chartered Accountants.")
        Sb.AppendLine("")
        Sb.AppendLine("Its purpose is to create a Report Package for Other GAAP that can be filed with the Dutch Chamber of Commerce.")
        Sb.AppendLine("")
        Sb.AppendLine("The source can be found at https://github.com/JacquesUrlusNBA/SCOPE")
        Sb.AppendLine("")
        Sb.AppendLine("Questions can be emailed to sbr@nba.nl.")
        Sb.AppendLine("")
        Sb.AppendLine("This is free and unencumbered software released into the public domain.")
        Sb.AppendLine("")
        Sb.AppendLine("Anyone is free to copy, modify, publish, use, compile, sell, or distribute this software, either in source code form or as a compiled binary, for any purpose, commercial or non-commercial, and by any means.")
        Sb.AppendLine("")
        Sb.AppendLine("In jurisdictions that recognize copyright laws, the author or authors of this software dedicate any and all copyright interest in the software to the public domain. We make this dedication for the benefit of the public at large and to the detriment of our heirs and successors. We intend this dedication to be an overt act of relinquishment in perpetuity of all present and future rights to this software under copyright law.")
        Sb.AppendLine("")
        Sb.AppendLine("THE SOFTWARE IS PROVIDED ""As Is"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES Or OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE Or OTHER DEALINGS IN THE SOFTWARE.")
        Sb.AppendLine("")
        Sb.Append("For more information, please refer to <https://unlicense.org>")

        MsgBox(Sb.ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SCOPE")

    End Sub


End Class
