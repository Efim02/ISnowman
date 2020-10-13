using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ClassSaveEnergies 
{
    public int energies;
    public System.DateTime time;
    public ClassSaveEnergies(int e)
    {
        energies = e;
        time = System.DateTime.Now;
    }
}
