using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSlapping : MonoBehaviour
{
    [SerializeField] private GameObject playerArm;
    
    public void TurnOnSlapping()
    {
        playerArm.SetActive(true);
    }

    public void TurnOffSlapping()
    {
        playerArm.SetActive(false);
    }
}
