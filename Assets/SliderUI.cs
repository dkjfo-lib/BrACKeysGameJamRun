using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    public Pipe_Value Pipe_Value;
    [Space]
    public Slider Slider;

    private float currentValue;
    private float maxValue;

    void Start()
    {
        StartCoroutine(UpdateValue());
    }

    IEnumerator UpdateValue()
    {
        Slider.maxValue = 1;
        Slider.minValue = 0;
        while (true)
        {
            currentValue = Pipe_Value.currentValue;
            maxValue = Pipe_Value.maxValue;

            var valuePercent = currentValue / maxValue;
            Slider.value = valuePercent;

            yield return new WaitUntil(() => currentValue != Pipe_Value.currentValue || maxValue != Pipe_Value.maxValue);
        }
    }
}
