using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using APICall;
using System;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private RainParticleSystem rainSystem;
    [SerializeField] private WindScript windSystem;

    private bool isRaining;

    public static WeatherController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Weather weather = MakeApiCall.GetWeather();

        if (weather.conditionCode >= 1150 && weather.conditionCode <= 1201)
        {
            isRaining = true;
            Debug.Log(isRaining);
            //turn rain particle system on
            rainSystem.StartRain();
            windSystem.SetWindSpeed(weather.windSpeed);
        }


    }

    private void Update()
    {
        if (!isRaining)
        {
            rainSystem.StopRain();
        }
    }
}

[Serializable]
public class Weather
{
    public string windDirection;
    public float windSpeed;
    public int conditionCode; //if code between 1150-1201, isRaining

    public Weather(string windDirection, string windSpeed, string conditionCode)
    {
        this.windDirection = windDirection;
        this.windSpeed = float.Parse(windSpeed);
        this.conditionCode = Convert.ToInt32(conditionCode);
    }
}
