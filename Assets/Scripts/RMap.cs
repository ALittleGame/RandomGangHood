using UnityEngine;
using UnityEditor;

public class RMap : ScriptableObject
{
    private int[][]     path;   // 用于描述该地图地形和通路的矩阵
    private RPoint[][]  points; // 用于记录具体每个点

    #region  // Get与Set方法
    public int[][] Path
    {
        get
        {
            return path;
        }

        set
        {
            path = value;
        }
    }
    #endregion
}