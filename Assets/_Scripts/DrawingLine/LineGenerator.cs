using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private Line _activeLine;
    [SerializeField] private Camera _camera;
    [SerializeField] private InputManager inputManager;

    private void Start()
    {
        _camera = Camera.main;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) //inputManager.touchHeld
        {
            //Debug.Log("Held");
            GameObject newLine = Instantiate(_linePrefab, parentTransform); //generates around 200 units away from the parent, which is roughly equal to the mousePosition as converted from screen to world point
            _activeLine = newLine.GetComponent<Line>();
        }

        if (Input.GetMouseButtonUp(0)) //!inputManager.touchHeld
        {
            _activeLine = null;
        }

        if (_activeLine != null)
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition); //inputManager.mousePosition does get correct position, but touchHeld bool currently not working
            //Debug.Log("Old system mousePosition: " + mousePosition);
            _activeLine.UpdateLine(mousePosition);
        }
    }
}
