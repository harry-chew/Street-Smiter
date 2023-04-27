using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator= GetComponentInChildren<Animator>();
    }

    public void PlayTargetAnimation(string animName)
    {
        _animator.Play(animName);
    }

    public void StopAnimation()
    {
        
    }
}
