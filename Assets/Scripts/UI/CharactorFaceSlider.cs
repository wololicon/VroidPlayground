using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorFaceSlider : MonoBehaviour
{
    public Slider _slider;
    [SerializeField]
    Text text_Value, text_Name;
    public BlendValueInfo _blend;
    public CharactorFacePanel _faceControl;
    private void Start()
    {
        _slider.onValueChanged.AddListener(OnSliderChange);
    }

    public void GetInfo(BlendValueInfo b)
    {
        _blend = b;
        text_Name.text = _blend.name;
        _slider.value = b.value;
    }
    public void OnSliderChange(float value)
    {
        text_Value.text = value.ToString("f2");
        _faceControl.ChangeValue(_blend, value);
    }
}
