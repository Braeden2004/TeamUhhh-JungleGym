using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sensitivitySlider : MonoBehaviour
{
    //reference https://www.youtube.com/watch?v=nTLgzvklgU8&t=197s

    [SerializeField] public Slider _slider;
    [SerializeField] public TextMeshProUGUI _sliderText;

    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            _sliderText.text = v.ToString("0");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
