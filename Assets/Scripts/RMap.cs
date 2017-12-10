using System;
using System.Collections.Generic;

public class RMap
{
    private int[,] path;   // 用于描述该地图地形和通路的矩阵
    private RPoint[,] points; // 用于记录具体每个点，与path对应


    public RMap(int xSize, int ySize)
    {
        //path = new int[ySize, xSize];  // FIXME: 以后先执行这一行初始化，再调用GenPath()去随机路径
        GenPath();  

        points = new RPoint[ySize, xSize];
    }

    /// <summary>
    /// 生成path数组
    /// </summary>
    private void GenPath()
    {
        // TODO: 现在用固定地图，以后改成随机地图
        path = new int[,]
        {
            { 0, 1, 0, 0, 0},
            { 0, 1, 1, 1, 1},
            { 1, 0, 1, 0, 0},
            { 1, 1, 1, 0, 0}
        };
    }

    /// <summary>
    /// 根据path数组，生成points数组
    /// </summary>
    private void GenPoints()
    {
        int ySize = path.GetLength(0);
        int xSize = path.GetLength(1);

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                if (path[y, x] > 0)
                {
                    // TODO: 这里初始化的时候用一个与y,x有关的随机值传入CreatePoint，得到该点在地图上的坐标
                    points[y, x] = RPointFactory.Instance().CreatePoint(0, 0);

                    // TODO: 随机给这个点加上一些特性，例如门派、商店等
                }
            }
        }

    }

    public Boolean Save()
    {
        // TODO:chenyufei 调用所有RPoint的Save
        return true;
    }

    #region  // Get与Set方法
    public int[,] Path
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
    public RPoint[,] Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }
    #endregion
}