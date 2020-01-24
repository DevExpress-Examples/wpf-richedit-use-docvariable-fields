Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Net
Imports System.Web
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Globalization
Imports DevExpress.Utils


Namespace DocumentVariablesExample
	Public Class GeoLocation
		Private _Latitude As Double
		Public Property Latitude() As Double
			Get
				Return _Latitude
			End Get
			Set(ByVal value As Double)
				_Latitude = value
			End Set
		End Property
		Private _Longitude As Double
		Public Property Longitude() As Double
			Get
				Return _Longitude
			End Get
			Set(ByVal value As Double)
				_Longitude = value
			End Set
		End Property
		Private _Address As String
		Public Property Address() As String
			Get
				Return _Address
			End Get
			Set(ByVal value As String)
				_Address = value
			End Set
		End Property

		' Review the Google Maps/Google Earth APIs Terms of Service document
		' before using the code that access geocoding services

		Public Shared Function GeocodeAddress(ByVal address As String) As GeoLocation()
			Dim coordinates As New List(Of GeoLocation)()
			coordinates.Add(New GeoLocation())

			'XmlDocument xmlDoc = new XmlDocument();

			'try {
			'    WebClient wbc = new WebClient();
			'    byte[] bytes = wbc.DownloadData(string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", HttpUtility.UrlEncode(address)));
			'    EncodingDetector detector = new EncodingDetector();
			'    Encoding encoding = detector.Detect(bytes);
			'    if (encoding == null)
			'        encoding = Encoding.UTF8;
			'    string response = encoding.GetString(bytes);
			'    xmlDoc.LoadXml(response);
			'}
			'catch (Exception ex) {
			'    if (ex.Message != null)
			'        return coordinates.ToArray();
			'}

			'string status = xmlDoc.DocumentElement.SelectSingleNode("status").InnerText;
			'switch (status.ToLowerInvariant())
			'{
			'case "ok":
			'    // Everything went just fine
			'    break;
			'case "zero_results":
			'    return coordinates.ToArray();
			'case "over_query_limit":
			'case "invalid_request":
			'case "request_denied":
			'    throw new Exception("An error occured when contacting the Google Maps API."); 
			'}

			'XmlNodeList nodeCol = xmlDoc.DocumentElement.SelectNodes("result");
			'foreach (XmlNode node in nodeCol)
			'{
			'    string exact_address = node.SelectSingleNode("formatted_address").InnerText;
			'    double lat = Convert.ToDouble(node.SelectSingleNode("geometry/location/lat").InnerText, CultureInfo.InvariantCulture);
			'    double lng = Convert.ToDouble(node.SelectSingleNode("geometry/location/lng").InnerText, CultureInfo.InvariantCulture);

			'    GeoLocation coord = new GeoLocation();
			'    coord.Address = exact_address;
			'    coord.Latitude = lat;
			'    coord.Longitude = lng;
			'    coordinates.Add(coord);
			'}            
			Return coordinates.ToArray()
		End Function
	End Class
End Namespace
