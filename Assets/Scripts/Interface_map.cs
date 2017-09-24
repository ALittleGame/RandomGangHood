using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface_map : MonoBehaviour {
    //地图表现层，接收逻辑层传来的数据，生成地图
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private int[,] _MapMatrix;  // TODO: chenyufei 该对象需要初始化，否则会崩溃，可以考虑在构造函数或者Start或者某处获取二维的长度，然后new

    public Interface_map()
    {

    }
    #region //对外接口


    //接收数据-对外接口
    public void SetMap(int[,] arr)
    {
        int rows = arr.GetLength(0);
        for (int rowIndex = 0; rowIndex < rows; ++rowIndex)
        {
            int columns = arr.GetLength(1);
            for (int columnIndex = 0; columnIndex < columns; ++columnIndex)
            {
                _MapMatrix[rowIndex,columnIndex] = arr[rowIndex,columnIndex];
            }
        }
        GenerateMap();
    }
    //清除地图-对外接口
    public void ClearMap()
    {

    }

    //更新地图-对外接口
    public void UpdataMap(int[,] arr)
    {
        int rows = arr.GetLength(0);
        for (int rowIndex = 0; rowIndex < rows; ++rowIndex)
        {
            int columns = arr.GetLength(1);
            for (int columnIndex = 0; columnIndex < columns; ++columnIndex)
            {
                _MapMatrix[rowIndex, columnIndex] = arr[rowIndex, columnIndex];
            }
        }
        GenerateMap();
    }
    #endregion

    #region //私有接口


    //生成地图
    private void GenerateMap()
    {
        int rows = _MapMatrix.GetLength(0);
        int columns = _MapMatrix.GetLength(1);
        //生成地图格子
        GenerateMapGrid(rows, columns);
        //对每个格子加载相应资源
        for (int rowIndex = 0; rowIndex < rows; ++rowIndex)
        {
            for (int columnIndex = 0; columnIndex < columns; ++columnIndex)
            {
                LoadMatOnGrid(_MapMatrix[rowIndex,columnIndex]);
            }
        }
    }

    //生成地图格子-私有方法
    private void GenerateMapGrid(int rows, int columns)
    {

    }
    //加载网格子上加载资源-私有方法
    private bool LoadMatOnGrid(int MatID)
    {
        //从库中Find资源挂在到地图格子上
        return true;

    }
#endregion
}
