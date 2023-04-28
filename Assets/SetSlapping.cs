using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSlapping : MonoBehaviour
{
    [SerializeField] private GameObject playerArm;
    [SerializeField] private SmashBar smashBar;
    
    public void TurnOnSlapping()
    {
        playerArm.SetActive(true);
        smashBar.EnableSmashBar();
    }

    public void TurnOffSlapping()
    {
        playerArm.SetActive(false);
        smashBar.DisableSmashBar();
    }
}
