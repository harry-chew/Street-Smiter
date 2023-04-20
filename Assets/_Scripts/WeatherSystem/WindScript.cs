using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WindScript : MonoBehaviour
{
    public static Action<float, float> OnWindChange;

    [SerializeField] private float _windDirection;
    [SerializeField] private float _windSpeed;

    private void Start()
    {
        _windDirection = UnityEngine.Random.Range(0f, 360f);
        _windSpeed = UnityEngine.Random.Range(0f, 5f);
        OnWindChange?.Invoke(_windDirection, _windSpeed);
    }



    public void SetWindDirection(float windDirection)
    {
        _windDirection = windDirection;
        OnWindChange?.Invoke(_windDirection, _windSpeed);
    }

    public void SetWindSpeed(float windSpeed)
    {
        _windSpeed = windSpeed;
        OnWindChange?.Invoke(_windDirection, _windSpeed);
    }

    public void SetWind(float windDirection, float windSpeed)
    {
        _windDirection = windDirection;
        _windSpeed = windSpeed;
        OnWindChange?.Invoke(_windDirection, _windSpeed);
    }

    public float GetWindDirection()
    {
        return _windDirection;
    }

    public float GetWindSpeed()
    {
        return _windSpeed;
    }
}
