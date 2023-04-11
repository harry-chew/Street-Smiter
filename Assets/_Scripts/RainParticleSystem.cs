using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RainParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem _rainParticleSystem;

    private void Start()
    {
        _rainParticleSystem = gameObject.GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        WindScript.OnWindChange += HandleOnWindChange;
    }

    private void HandleOnWindChange(float windDirection, float windSpeed)
    {
        Debug.Log("handle it");
    }
}
