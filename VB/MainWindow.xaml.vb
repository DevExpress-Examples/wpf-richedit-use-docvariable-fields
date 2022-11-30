Imports System
Imports System.Windows
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraRichEdit
Imports DevExpress.Xpf.RichEdit
#Region "#usings"
Imports DevExpress.Services
Imports System.Drawing

#End Region  ' #usings
Namespace DocumentVariablesExample

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Private richEdit As RichEditControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub richEditControl1_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.richEditControl1.ApplyTemplate()
            Me.richEditControl1.LoadDocument("Docs\invitation.docx")
            Me.richEditControl1.Options.MailMerge.DataSource = New SampleData()
            AddHandler Me.richEditControl2.Document.CalculateDocumentVariable, New CalculateDocumentVariableEventHandler(AddressOf eventHandler_CalculateDocumentVariable)
            richEdit = Me.richEditControl1
        End Sub

        Private Sub btnMailMerge_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim myMergeOptions As MailMergeOptions = Me.richEditControl1.Document.CreateMailMergeOptions()
            myMergeOptions.MergeMode = MergeMode.NewSection
            Me.richEditControl1.Document.MailMerge(myMergeOptions, Me.richEditControl2.Document)
            Me.tabControl.SelectedIndex = 1
        End Sub

#Region "#servicesubst"
        Private Sub richEditControl1_MailMergeStarted(ByVal sender As Object, ByVal e As MailMergeStartedEventArgs)
            Me.richEditControl1.RemoveService(GetType(IProgressIndicationService))
            Me.richEditControl1.AddService(GetType(IProgressIndicationService), New MyProgressIndicatorService(Me.richEditControl1, Me.progressBarControl1))
        End Sub

#End Region  ' #servicesubst
        Private Sub richEditControl1_MailMergeFinished(ByVal sender As Object, ByVal e As MailMergeFinishedEventArgs)
            Me.richEditControl1.RemoveService(GetType(IProgressIndicationService))
        End Sub

#Region "#mailmergerecordstarted"
        Private Sub richEditControl1_MailMergeRecordStarted(ByVal sender As Object, ByVal e As MailMergeRecordStartedEventArgs)
            Dim _range As DocumentRange = e.RecordDocument.InsertText(e.RecordDocument.Range.Start, String.Format("Created on {0:G}" & Microsoft.VisualBasic.Constants.vbLf & Microsoft.VisualBasic.Constants.vbLf, Date.Now))
            Dim cp As CharacterProperties = e.RecordDocument.BeginUpdateCharacters(_range)
            cp.FontSize = 8
            cp.ForeColor = Color.Red
            cp.Hidden = True
            e.RecordDocument.EndUpdateCharacters(cp)
        End Sub

#End Region  ' #mailmergerecordstarted
#Region "#mailmergerecordfinished"
        Private Sub richEditControl1_MailMergeRecordFinished(ByVal sender As Object, ByVal e As MailMergeRecordFinishedEventArgs)
            e.RecordDocument.AppendDocumentContent("Docs\bungalow.docx", DocumentFormat.OpenXml)
        End Sub

#End Region  ' #mailmergerecordfinished
#Region "#calculatedocumentvariable"
        Private Sub eventHandler_CalculateDocumentVariable(ByVal sender As Object, ByVal e As CalculateDocumentVariableEventArgs)
            Dim location As String = e.Arguments(0).Value.ToString()
            Console.WriteLine(e.VariableName & " " & location)
            If(Equals(location.Trim(), String.Empty)) OrElse location.Contains("<") Then
                e.Value = " "
                e.Handled = True
                Return
            End If

            Select Case e.VariableName
                'case "Weather":
                '    Conditions conditions = new Conditions();
                '    conditions = Weather.GetCurrentConditions(location);
                '    e.Value = String.Format("Forecast for {0}: \nConditions: {1}\nTemperature (C) :{2}\nHumidity: {3}\nWind: {4}\n",
                '        conditions.City, conditions.Condition, conditions.TempC, conditions.Humidity, conditions.Wind);
                '    break;
                Case "Location"
                    Dim loc As GeoLocation() = GeoLocation.GeocodeAddress(location)
                    e.Value = String.Format(" {0}" & Microsoft.VisualBasic.Constants.vbLf & "Latitude: {1}" & Microsoft.VisualBasic.Constants.vbLf & "Longitude: {2}" & Microsoft.VisualBasic.Constants.vbLf, loc(CInt(0)).Address, loc(CInt(0)).Latitude.ToString(), loc(CInt(0)).Longitude.ToString())
            End Select

            e.Handled = True
        End Sub

#End Region  ' #calculatedocumentvariable
        Private Sub tabControl_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Core.TabControlSelectionChangedEventArgs)
            Select Case Me.tabControl.SelectedIndex
                Case 0
                    richEdit = Me.richEditControl1
                    Me.btnMailMerge.IsEnabled = True
                Case 1
                    richEdit = Me.richEditControl2
                    Me.btnMailMerge.IsEnabled = False
            End Select
        End Sub

        Private Sub chkShowCodes_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ShowFieldCodes(True)
        End Sub

        Private Sub chkShowCodes_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ShowFieldCodes(False)
        End Sub

        Private Sub ShowFieldCodes(ByVal showCodes As Boolean)
            Dim doc As Document = richEdit.Document
            doc.BeginUpdate()
            For Each f As Field In doc.Fields
                f.ShowCodes = showCodes
            Next

            doc.EndUpdate()
        End Sub
    End Class
End Namespace
