﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.Web;
using DevExpress.Utils;
using System.Net.Http;

namespace DocumentVariablesExample
{
    class Weather
    {
        // Review the Google Maps/Google Earth APIs Terms of Service document
        // before using the code that access weather services

        public static Conditions GetCurrentConditions(string location)
        {
            Conditions conditions = new Conditions();

            //XmlDocument xmlConditions = new XmlDocument();
            //try {
            //    WebClient wbc = new WebClient();
            //    byte[] bytes = wbc.DownloadData(string.Format("http://www.google.com/ig/api?weather={0}", HttpUtility.UrlEncode(location)));
            //    EncodingDetector detector = new EncodingDetector();
            //    Encoding encoding = detector.Detect(bytes);
            //    if (encoding == null)
            //        encoding = Encoding.UTF8;
            //    string response = encoding.GetString(bytes);
            //    xmlConditions.LoadXml(response);
            //}
            //catch (Exception ex) {
            //    if (ex.Message != null)
            //    return conditions;
            //}

            //if (xmlConditions.SelectSingleNode("xml_api_reply/weather/problem_cause") == null) {
            //    conditions.City = xmlConditions.SelectSingleNode("/xml_api_reply/weather/forecast_information/city").Attributes["data"].InnerText;
            //    conditions.Condition = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/condition").Attributes["data"].InnerText;
            //    conditions.TempC = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/temp_c").Attributes["data"].InnerText;
            //    conditions.TempF = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/temp_f").Attributes["data"].InnerText;
            //    conditions.Humidity = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/humidity").Attributes["data"].InnerText;
            //    conditions.Wind = xmlConditions.SelectSingleNode("/xml_api_reply/weather/current_conditions/wind_condition").Attributes["data"].InnerText;
            //}

            return conditions;
        }

        /// <summary>
        /// The function that gets the forecast for the next four days.
        /// </summary>
        /// <param name="location">City or ZIP code</param>
        /// <returns></returns>
        public static List<Conditions> GetForecast(string location)
        {
            List<Conditions> conditions = new List<Conditions>();

            XmlDocument xmlConditions = new XmlDocument();
            
            try {
                HttpClient wbc = new HttpClient();
                byte[] bytes = wbc.GetByteArrayAsync(string.Format("http://www.google.com/ig/api?weather={0}", HttpUtility.UrlEncode(location))).Result;
                Encoding encoding = encoding = Encoding.UTF8;
                
                string response = encoding.GetString(bytes);
                xmlConditions.LoadXml(response);
            }
            catch (Exception ex) {
                if (ex.Message != null)
                    return conditions;
            }

            if (xmlConditions.SelectSingleNode("xml_api_reply/weather/problem_cause") != null) {
                conditions = null;
            }
            else {
                foreach (XmlNode node in xmlConditions.SelectNodes("/xml_api_reply/weather/forecast_conditions")) {
                    Conditions condition = new Conditions();
                    condition.City = xmlConditions.SelectSingleNode("/xml_api_reply/weather/forecast_information/city").Attributes["data"].InnerText;
                    condition.Condition = node.SelectSingleNode("condition").Attributes["data"].InnerText;
                    condition.High = node.SelectSingleNode("high").Attributes["data"].InnerText;
                    condition.Low = node.SelectSingleNode("low").Attributes["data"].InnerText;
                    condition.DayOfWeek = node.SelectSingleNode("day_of_week").Attributes["data"].InnerText;
                    conditions.Add(condition);
                }
            }

            return conditions;
        }
    }
    public class Conditions
    {
        string city = "No Data";
        string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
        string condition = "No Data";
        string tempF = "No Data";
        string tempC = "No Data";
        string humidity = "No Data";
        string wind = "No Data";
        string high = "No Data";
        string low = "No Data";

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }

        public string TempF
        {
            get { return tempF; }
            set { tempF = value; }
        }

        public string TempC
        {
            get { return tempC; }
            set { tempC = value; }
        }

        public string Humidity
        {
            get { return humidity; }
            set { humidity = value; }
        }

        public string Wind
        {
            get { return wind; }
            set { wind = value; }
        }

        public string DayOfWeek
        {
            get { return dayOfWeek; }
            set { dayOfWeek = value; }
        }

        public string High
        {
            get { return high; }
            set { high = value; }
        }

        public string Low
        {
            get { return low; }
            set { low = value; }
        }
    }
}
