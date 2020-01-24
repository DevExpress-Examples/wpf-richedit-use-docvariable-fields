using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Globalization;
using DevExpress.Utils;


namespace DocumentVariablesExample
{
    public class GeoLocation
    {
        private double _Latitude;
        public double Latitude
        {
            get { return _Latitude; }
            set
            {
                _Latitude = value;
            }
        }
        private double _Longitude;
        public double Longitude
        {
            get { return _Longitude; }
            set
            {
                _Longitude = value;
            }
        }
        private string _Address;
        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
            }
        }

        // Review the Google Maps/Google Earth APIs Terms of Service document
        // before using the code that access geocoding services

        public static GeoLocation[] GeocodeAddress(string address)
        {
            List<GeoLocation> coordinates = new List<GeoLocation>();
            coordinates.Add(new GeoLocation());

            //XmlDocument xmlDoc = new XmlDocument();

            //try {
            //    WebClient wbc = new WebClient();
            //    byte[] bytes = wbc.DownloadData(string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", HttpUtility.UrlEncode(address)));
            //    EncodingDetector detector = new EncodingDetector();
            //    Encoding encoding = detector.Detect(bytes);
            //    if (encoding == null)
            //        encoding = Encoding.UTF8;
            //    string response = encoding.GetString(bytes);
            //    xmlDoc.LoadXml(response);
            //}
            //catch (Exception ex) {
            //    if (ex.Message != null)
            //        return coordinates.ToArray();
            //}
            
            //string status = xmlDoc.DocumentElement.SelectSingleNode("status").InnerText;
            //switch (status.ToLowerInvariant())
            //{
            //case "ok":
            //    // Everything went just fine
            //    break;
            //case "zero_results":
            //    return coordinates.ToArray();
            //case "over_query_limit":
            //case "invalid_request":
            //case "request_denied":
            //    throw new Exception("An error occured when contacting the Google Maps API."); 
            //}

            //XmlNodeList nodeCol = xmlDoc.DocumentElement.SelectNodes("result");
            //foreach (XmlNode node in nodeCol)
            //{
            //    string exact_address = node.SelectSingleNode("formatted_address").InnerText;
            //    double lat = Convert.ToDouble(node.SelectSingleNode("geometry/location/lat").InnerText, CultureInfo.InvariantCulture);
            //    double lng = Convert.ToDouble(node.SelectSingleNode("geometry/location/lng").InnerText, CultureInfo.InvariantCulture);

            //    GeoLocation coord = new GeoLocation();
            //    coord.Address = exact_address;
            //    coord.Latitude = lat;
            //    coord.Longitude = lng;
            //    coordinates.Add(coord);
            //}            
            return coordinates.ToArray();
        }
    }
}
