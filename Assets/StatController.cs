using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteInEditMode]
public class StatController : MonoBehaviour
{
    public RectTransform rectTransformProgress;
    public Image progressBarImage;
    public Text textObject;
    public float progressBarMargin = 3;
    public float currentValue = 0;
    public float maxValue = 1;
    public float maxWidthInner;
    public Color Color;

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        textObject.color = Color;
        progressBarImage.color = Color;
        rectTransformProgress.sizeDelta = new Vector2(maxWidthInner * (currentValue / maxValue), rectTransformProgress.sizeDelta.y);
    }

    public void SetCurrentValue(float value)
    {
        currentValue = value;
    }
}
