using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text energyText;

    public void SetMaxEnergy(int energy)
    {
        slider.maxValue = energy;
        slider.value = energy;
    }

    public void SetEnergy(int energy)
    {
        slider.value = energy;
        energyText.text = energy + " / 100";
    }
}
