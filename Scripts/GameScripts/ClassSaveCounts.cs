using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClassSaveCounts 
{
    public int counts1;
    public int counts2;
    public int counts3;
    public ClassSaveCounts()
    {
        counts1 = 0;
        counts2 = 0;
        counts3 = 0;
    }
    public ClassSaveCounts(int c1, int c2, int c3)
    {
        counts1 = c1;
        counts2 = c2;
        counts3 = c3;
    }
}
