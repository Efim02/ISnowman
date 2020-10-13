using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnergyBar : MonoBehaviour
{
    [SerializeField]
    Sprite sprite0;
    [SerializeField]
    Sprite sprite1;
    [SerializeField]
    Sprite sprite2;
    [SerializeField]
    Sprite sprite3;
    [SerializeField]
    Sprite sprite4;
    [SerializeField]
    Sprite sprite5;

    Image spRendered;

    public static int Energies=0;
    public const int MinutesRestore = 5;
    public static float nonMinutes=0f;
    public static EnergyBar referenceEnergyBar;
    System.DateTime time;
    bool isNonEnergies=false;
    
    private void Start()
    {
        referenceEnergyBar = this;
        spRendered = GetComponent<Image>();
        CanvasMod.charMov.eventDeath += ChangeValue;
        ChangeSprite();
        if (Energies < 5)
        {
            isNonEnergies = true;
            time = System.DateTime.Now;
        }
        //print("Energies "+Energies);
    }
    void ChangeValue()
    {
        Energies -= 1;
        ChangeSprite();
        SaveLoad.SaveEnergies();
        if (!isNonEnergies)
        {
            isNonEnergies = true;
            time = System.DateTime.Now;
        }
    }
    public void ChangeSprite()
    {
        switch(Energies)
        {
            case 0:
                spRendered.sprite = sprite0;
                break;
            case 1:
                spRendered.sprite = sprite1;
                break;
            case 2:
                spRendered.sprite = sprite2;
                break;
            case 3:
                spRendered.sprite = sprite3;
                break;
            case 4:
                spRendered.sprite = sprite4;
                break;
            case 5:
                spRendered.sprite = sprite5;
                break;
        }
    }
    void Update()
    {
        if (isNonEnergies)
        {
            System.TimeSpan timeSpan = System.DateTime.Now.Subtract(time);
            if( timeSpan.TotalMinutes + nonMinutes > MinutesRestore )
            {
                nonMinutes = 0f;
                Energies += 1;
                ChangeSprite();
                SaveLoad.SaveEnergies();
                time = System.DateTime.Now;
                if (Energies == 5)
                    isNonEnergies = false;
            }
        }
    }
}
