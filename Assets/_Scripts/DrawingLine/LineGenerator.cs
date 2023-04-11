using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private Line _activeLine;
    [SerializeField] private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            GameObject newLine = Instantiate(_linePrefab);
            _activeLine = newLine.GetComponent<Line>();
        }

        if(Input.GetMouseButtonUp(0))
        {
            _activeLine = null;
        }

        if (_activeLine != null)
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _activeLine.UpdateLine(mousePosition);
        }
    }
}
