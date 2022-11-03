using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ragemeter : MonoBehaviour
{
    public Slider slider;

    public void SetStartRage(int startRage)
    {
        slider.value = startRage;
    }
    
    public void SetMaxRage(int maxRage)
    {
        slider.maxValue = maxRage;
    }
    
    public void SetRage(int rage)
    {
        slider.value = rage;
    }
}
