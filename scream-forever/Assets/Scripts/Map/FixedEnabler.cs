using System;
using System.Collections.Generic;
using UnityEngine;

public class FixedEnabled : MonoBehaviour
{
    [SerializeField][Range(0, 1f)] private float rate;
    [SerializeField] private GameObject enableChild;
    [SerializeField] private string chanceTag;
    
    public void OnEnable()
    {
        enableChild.SetActive(GetAccum() >= 1f);
        if (GetAccum() >= 1f)
        {
            Global.Instance.tagAccum[chanceTag] -= 1f;
        }
        Global.Instance.tagAccum[chanceTag] += rate;
    }

    private float GetAccum()
    {
        Global.Instance.tagAccum.TryAdd(chanceTag, 0f);

        return Global.Instance.tagAccum[chanceTag];
    }
}