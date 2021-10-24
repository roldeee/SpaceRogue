using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public Slider slider;
    public int maxHP = 100;
    public const int minHP = 0;

    public void SetHP(int curHP)
    {
        slider.value = Math.Min(maxHP, Math.Max(minHP, curHP));
    }

    public void setMaxHP(int maxHP)
    {
        if (maxHP < minHP)
        {
            Debug.Log("What are you doing???");
            return;
        }

        this.maxHP = maxHP;
        slider.maxValue = maxHP;
        slider.value = maxHP;
    }
}