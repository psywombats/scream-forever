using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultibumpComponent : MonoBehaviour
{
    [SerializeField] public List<BumpComponent> bumps;
    [SerializeField] public float interval = 32f;
    [SerializeField] public PlayerController avatar;

    public bool ContinuousMode { get; set; }

    private float lastBump = 0f;
    
    public void Start()
    {
        StartCoroutine(MultibumpRoutine());
    }

    public void Update()
    {
        if (ContinuousMode)
        {
            foreach (var bump in bumps)
            {
                if (!bump.IsBumping)
                {
                    StartCoroutine(bump.SpeedBumpRoutine(bump.continuousMult, useDelay: false));
                }
            }
        }
    }

    public IEnumerator MultibumpRoutine()
    {
        while (Application.isPlaying)
        {
            if (avatar.TotalTraversed > lastBump + interval)
            {
                Bump();
                lastBump = Global.Instance.Avatar.TotalTraversed;
            }

            yield return null;
        }
    }
    
    public void Bump(float mult = 1f)
    {
        if (isActiveAndEnabled)
        {
            foreach (var bump in bumps)
            {
                StartCoroutine(bump.SpeedBumpRoutine(mult));
            }
        }
    }
}