using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class MakeApiCall : MonoBehaviour
{
    private string _url = "http://api.weatherapi.com/v1/current.json";
    private string _apiKey = "?key=607673592df841cdb14133226221305";
    private string _query = "&q=London&aqi=no";

    private void Start()
    {
        GetWeather();
    }

    private WeatherInfo GetWeather()
    {
        string parse = _url + _apiKey + _query;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(parse);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
        Debug.Log(jsonResponse);
        return info;
    }

}

[Serializable]
public class Weather
{
    public int id;
    public string main;
}
[Serializable]
public class WeatherInfo
{
    public int id;
    public string name;
    public List<Weather> weather;
}