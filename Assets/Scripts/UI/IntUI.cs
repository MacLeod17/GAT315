using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntUI : MonoBehaviour
{
    public Slider slider = null;
    public TMP_Text label = null;
    public TMP_Text valueText = null;
    public int min = 0;
    public int max = 1;

    public IntData data = null;

    private void OnValidate()
    {
        if (data != null)
        {
            name = data.name;
            label.text = name;
        }
        slider.minValue = min;
        slider.maxValue = max;
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(UpdateValue);
    }

    void Update()
    {
        slider.value = data.value;
        valueText.text = data.value.ToString("F2");
    }

    void UpdateValue(float value)
    {
        data.value = (int)value;
    }
}
