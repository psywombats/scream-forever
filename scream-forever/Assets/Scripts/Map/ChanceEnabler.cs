using System;
using UnityEngine;

public class ChanceEnabler : MonoBehaviour
{
    [SerializeField] private float chance;
    [SerializeField] private GameObject enableChild;

    public void OnEnable()
    {
        enableChild.SetActive(UnityEngine.Random.Range(0, 1f) < chance);
    }
}