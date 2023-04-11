using System;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public static event Action OnGameStart;
    public void TriggerEvent()
    {
        OnGameStart?.Invoke();
    }
}
