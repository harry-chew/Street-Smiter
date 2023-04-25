using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using SimpleJSON;

namespace APICall{
    public class MakeApiCall : MonoBehaviour
    {
        //Powered by <a href="https://www.weatherapi.com/" title="Weather API">WeatherAPI.com</a>
        private static string _url = "http://api.weatherapi.com/v1/current.json";
        private static string _apiKey = "?key=607673592df841cdb14133226221305";
        private static string _query = "&q=auto:ip&aqi=no"; 

        public static Weather GetWeather()
        {
            string parse = _url + _apiKey + _query;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(parse);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            JSONNode result = JSON.Parse(jsonResponse);
            Debug.Log(result["current"]["condition"]["text"]);
            Weather weather = new(result["current"]["wind_dir"], result["current"]["wind_kph"], result["current"]["condition"]["code"]);
            Debug.Log(jsonResponse);
            return weather;
        }

    }

    
}

