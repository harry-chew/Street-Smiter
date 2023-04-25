using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDrawing : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject drawing;

    [SerializeField] private LayerMask drawingLayer;
    [SerializeField] private LayerMask everythingLayer;


    public void TurnOnDrawing()
    {
        cam.orthographic = true;
        cam.cullingMask = drawingLayer;
        drawing.SetActive(true);
    }

    public void TurnOffDrawing()
    {
        cam.orthographic = false;
        cam.cullingMask = everythingLayer;
        drawing.SetActive(false);
    }
}
