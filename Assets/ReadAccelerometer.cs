using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadAccelerometer : MonoBehaviour
{
    public Vector2 acceleration;

    public bool shouldSlap = false;
    public Transform cube;

    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Update()
    {
        acceleration = Input.acceleration;
        cube.transform.eulerAngles = new Vector3(acceleration.y * 90,0, 0);

        if (acceleration.y >= 0f && !shouldSlap)
        {
            Slap();
            shouldSlap = true;
        }

        if(acceleration.y <= -0.4f)
        {
            shouldSlap = false;
        }
    
    }

    public void PlayAudioOnce()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void Slap()
    {
        Debug.Log("slap");
        PlayAudioOnce();
    }
}
