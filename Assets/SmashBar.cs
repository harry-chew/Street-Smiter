using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmashBar : MonoBehaviour
{
    public static Action OnSmashBarEmpty;
    public float extendSpeed = 0.1f;
    public Vector3 shrinkAmount = new Vector3(0, -0.5f, 0);
    public Color startColor = Color.green;

    private Vector3 maxSize;

    public Image image;
    private float height;
    public float time;

    private void OnEnable()
    {
        ReadAccelerometer.OnSlap += HandleSlap;
    }

    private void OnDisable()
    {
        ReadAccelerometer.OnSlap -= HandleSlap;
    }

    private void HandleSlap()
    {
        time -= 10f;
        if (time <= 0)
        {
            time = 0;
            HandleOnSmashBarEmpty();
        }
    }

    private void Start()
    {
        image.color = startColor;
        height = image.rectTransform.sizeDelta.y;
        image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    private void Update()
    {
        // Gradually extend the object's scale towards the target scale
        //transform.localScale = Vector3.Lerp(transform.localScale, maxSize, extendSpeed * Time.deltaTime);
        //image.color = Color.Lerp(startColor, maxColor, transform.localScale.y / maxSize.y);

        time += Time.deltaTime;
        UpdateSize(time);
    }

    float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
    
    void UpdateSize(float health)
    {
        float mappedHealth = Map(health, 0, 100, 0, height);
    
        image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, mappedHealth);
        image.color = Color.Lerp(startColor, Color.red, mappedHealth / 100);

        if(image.rectTransform.sizeDelta.y >= height)
        {
            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }
    }

    private void HandleOnSmashBarEmpty()
    {
        Debug.Log("Smash bar empty -> this should load next section of game");
        OnSmashBarEmpty?.Invoke();
    }
}