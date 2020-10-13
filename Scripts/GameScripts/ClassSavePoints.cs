using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ClassSavePoints 
{
    public int points1;
    public int points2;
    public int points3;
    public int id = -1;
    public ClassSavePoints()
    {
        points1 = 11;
        points2 = 11;
        points3 = 11;
    }
    public ClassSavePoints(int p1, int p2, int p3)
    {
        points1 = p1;
        points2 = p2;
        points3 = p3;
    }
}
