using System;
using System.Collections.Generic;

public class RMap
{
    private int[,]     path;   // 用于描述该地图地形和通路的矩阵
    private RPoint[,]  points; // 用于记录具体每个点，与path对应

    
public RMap(int xSize, int ySize)
    {
        // TODO: path也在这里new

        points = new RPoint[xSize, ySize];
        GenPath(xSize,ySize);
        GenPoints();
    }

    /// <summary>
    /// 生成path数组
    /// </summary>
    private void GenPath(int xSize, int ySize)
    {
        path = new int[xSize,ySize];
        
        //生成随机路径算法

        //test
        for(int x = 0; x < xSize; x++)
        {
            for(int y = 0; y < ySize; y++)
            {
                path[x, y] = 0;
            }
        }
       
        //{
        //    { 0, 1, 0, 0, 0},
        //    { 0, 1, 1, 1, 1},
        //    { 1, 0, 1, 0, 0},
        //    { 1, 1, 1, 0, 0}
        //};
    }

    /// <summary>
    /// 根据path数组，生成points数组
    /// </summary>
    private void GenPoints()
    {
        int xSize = path.GetLength(0);//行数
        int ySize = path.GetLength(1);//列数
        Int64 idcount = (Int64)0;

        for(int x=0;x<xSize;x++)
        {
            for(int y=0;y<ySize;y++)
            {
                if(path[x,y] > 0)
                {
                    // TODO: 这里初始化的时候用一个与y,x有关的随机值传入CreatePoint，得到该点在地图上的坐标

                    //test
                    RPoint temppoint = new RPoint(idcount, x,y);
                    points[x, y] = temppoint;//RPointFactory.Instance().CreatePoint(0, 0);
                    idcount++;
                    //test over

                    // TODO: 随机给这个点加上一些特性，例如门派、商店等
                }
                else
                {
                    RPoint temppoint = new RPoint(idcount, x, y);
                    points[x, y] = temppoint;//RPointFactory.Instance().CreatePoint(0, 0);
                    idcount++;
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