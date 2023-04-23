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
            GameObject newLine = Instantiate(_linePrefab, parentTransform);
            _activeLine = newLine.GetComponent<Line>();
        }

        if (Input.GetMouseButtonUp(0)) //!inputManager.touchHeld
        {
            //Debug.Log("Not held");
            _activeLine = null;
        }

        if (_activeLine != null)
        {
            //now getting the screen position is the issue
            //screen position not working with device simulator (also still not showing up on main game)
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition); //inputManager.mousePosition
            _activeLine.UpdateLine(mousePosition);
        }
    }
}
