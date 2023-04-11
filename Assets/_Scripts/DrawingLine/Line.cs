using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private List<Vector2> _linePoints;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    
    private void SetPoint(Vector2 point)
    {
        _linePoints.Add(point);
        
        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPosition(_linePoints.Count - 1, point);
    }

    public void UpdateLine(Vector2 position) 
    { 
        if(_linePoints == null)
        {
            _linePoints = new List<Vector2>();
            SetPoint(position);
            return;
        }

        if (Vector2.Distance(_linePoints.Last(), position) > .1f)
        {
            SetPoint(position);
        }
    }
    
}
