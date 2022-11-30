Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Xml
Imports System.Web
Imports System.Net.Http

Namespace DocumentVariablesExample

    Friend Class Weather

        ' Review the Google Maps/Google Earth APIs Terms of Service document
        ' before using the code that access weather services
        Public Shared Function GetCurrentConditions(ByVal location As String) As Conditions
            Dim conditions As Conditions = New Conditions()
            'XmlDocument xmlConditions = new XmlDocument();
            'try {
            '    WebClient wbc = new WebClient();
            '    byte[] bytes = wbc.DownloadData(string.Format("http://www.google.com/ig/api?weather={0}", HttpUtility.UrlEncode(location)));
            '    EncodingDetector detector = new EncodingDetector();
            '    Encoding encoding = detector.Detect(bytes);
            '    if (encoding == null)
            '        encoding = Encoding.UTF8;
            '    string response = encoding.GetString(bytes);
            '    xmlConditions.LoadXml(response);
            '}
            'catch (Exception ex) {
            '    if (ex.Message != null)
            '    return conditions;
            '}
            'if (xmlConditions.SelectSingleNode("xml_api_reply/weather/problem_cause") == null) {
            '    conditions.City = xmlConditions.SelectSingleNode("/xml_api_reply/weather/forecast_information/city").Attributes["data"].InnerText;
            '    conditions.Condition = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/condition").Attributes["data"].InnerText;
            '    conditions.TempC = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/temp_c").Attributes["data"].InnerText;
            '    conditions.TempF = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/temp_f").Attributes["data"].InnerText;
            '    conditions.Humidity = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/humidity").Attributes["data"].InnerText;
            '    conditions.Wind = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/wind_condition").Attributes["data"].InnerText;
            '}
            Return conditions
        End Function

        ''' <summary>
        ''' The function that gets the forecast for the next four days.
        ''' </summary>
        ''' <param name="location">City or ZIP code</param>
        ''' <returns></returns>
        Public Shared Function GetForecast(ByVal location As String) As List(Of Conditions)
            Dim conditions As List(Of Conditions) = New List(Of Conditions)()
            Dim xmlConditions As XmlDocument = New XmlDocument()
            Try
                Dim wbc As HttpClient = New HttpClient()
                Dim bytes As Byte() = wbc.GetByteArrayAsync(String.Format("http://www.google.com/ig/api?weather={0}", HttpUtility.UrlEncode(location))).Result
                Dim encoding As Encoding = CSharpImpl.__Assign(encoding, Encoding.UTF8)
                Dim response As String = encoding.GetString(bytes)
                xmlConditions.LoadXml(response)
            Catch ex As Exception
                If Not Equals(ex.Message, Nothing) Then Return conditions
            End Try

            If xmlConditions.SelectSingleNode("xml_api_reply/weather/problem_cause") IsNot Nothing Then
                conditions = Nothing
            Else
                For Each node As XmlNode In xmlConditions.SelectNodes("/xml_api_reply/weather/forecast_conditions")
                    Dim condition As Conditions = New Conditions()
                    condition.City = xmlConditions.SelectSingleNode("/xml_api_reply/weather/forecast_information/city").Attributes("data").InnerText
                    condition.Condition = node.SelectSingleNode("condition").Attributes("data").InnerText
                    condition.High = node.SelectSingleNode("high").Attributes("data").InnerText
                    condition.Low = node.SelectSingleNode("low").Attributes("data").InnerText
                    condition.DayOfWeek = node.SelectSingleNode("day_of_week").Attributes("data").InnerText
                    conditions.Add(condition)
                Next
            End If

            Return conditions
        End Function

        Private Class CSharpImpl

            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class

    Public Class Conditions

        Private cityField As String = "No Data"

        Private dayOfWeekField As String = Date.Now.DayOfWeek.ToString()

        Private conditionField As String = "No Data"

        Private tempFField As String = "No Data"

        Private tempCField As String = "No Data"

        Private humidityField As String = "No Data"

        Private windField As String = "No Data"

        Private highField As String = "No Data"

        Private lowField As String = "No Data"

        Public Property City As String
            Get
                Return cityField
            End Get

            Set(ByVal value As String)
                cityField = value
            End Set
        End Property

        Public Property Condition As String
            Get
                Return conditionField
            End Get

            Set(ByVal value As String)
                conditionField = value
            End Set
        End Property

        Public Property TempF As String
            Get
                Return tempFField
            End Get

            Set(ByVal value As String)
                tempFField = value
            End Set
        End Property

        Public Property TempC As String
            Get
                Return tempCField
            End Get

            Set(ByVal value As String)
                tempCField = value
            End Set
        End Property

        Public Property Humidity As String
            Get
                Return humidityField
            End Get

            Set(ByVal value As String)
                humidityField = value
            End Set
        End Property

        Public Property Wind As String
            Get
                Return windField
            End Get

            Set(ByVal value As String)
                windField = value
            End Set
        End Property

        Public Property DayOfWeek As String
            Get
                Return dayOfWeekField
            End Get

            Set(ByVal value As String)
                dayOfWeekField = value
            End Set
        End Property

        Public Property High As String
            Get
                Return highField
            End Get

            Set(ByVal value As String)
                highField = value
            End Set
        End Property

        Public Property Low As String
            Get
                Return lowField
            End Get

            Set(ByVal value As String)
                lowField = value
            End Set
        End Property
    End Class
End Namespace
