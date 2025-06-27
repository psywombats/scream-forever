using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GpsComponent : MonoBehaviour
{
    [SerializeField] private RectTransform scroller;
    [SerializeField] private float unitsPerSpeed;
    [SerializeField] private RectTransform rotater;
    [SerializeField] private Image overlay;

    public void Update()
    {
        if (Global.Instance.Avatar != null)
        {
            var off = Global.Instance.Avatar.SpeedRatio * unitsPerSpeed;
            scroller.anchoredPosition = new Vector2(0, scroller.anchoredPosition.y + off);
        
            rotater.localRotation = quaternion.Euler(new Vector3(0, 0, 
                Global.Instance.Avatar.transform.localRotation.eulerAngles.y * .07f));
        
            if (scroller.anchoredPosition.y > scroller.sizeDelta.y / 2)
            {
                scroller.anchoredPosition = new Vector2(0, -scroller.sizeDelta.y / 2f);
            }
            if (scroller.anchoredPosition.y < -scroller.sizeDelta.y / 2)
            {
                scroller.anchoredPosition = new Vector2(0, scroller.sizeDelta.y / 2f);
            }
        }

    }
}