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
        if(_dirty)
        {
            GenerateMap();
            _dirty = false;
        }
	}

    private RMap _MapMatrix;
    private bool _dirty;

    public Interface_map()
    {
        _MapMatrix = null;
        _dirty = false;
    }

    #region //对外接口
    //接收数据-对外接口
    public void SetMap(RMap map, bool dirty = false)
    {
        int rows = map.Path.GetLength(0);
        int columns = map.Path.GetLength(1);
        if (_MapMatrix == null) _MapMatrix = new RMap(rows, columns);
        for (int rowIndex = 0; rowIndex < rows; ++rowIndex)
        {
            
            for (int columnIndex = 0; columnIndex < columns; ++columnIndex)
            {
                _MapMatrix.Points[rowIndex,columnIndex] = map.Points[rowIndex,columnIndex];
            }
        }
        _dirty = dirty;
    }
    //清除地图-对外接口
    public void ClearMap()
    {

    }
    #endregion

    #region //私有接口


    //生成地图
    private void GenerateMap()
    {
        if (_MapMatrix == null) return;

        int rows = _MapMatrix.Path.GetLength(0);
        int columns = _MapMatrix.Path.GetLength(1);
        //生成地图格子
        GenerateMapGrid(rows, columns);
        //对每个格子加载相应资源
        for (int rowIndex = 0; rowIndex < rows; ++rowIndex)
        {
            for (int columnIndex = 0; columnIndex < columns; ++columnIndex)
            {
                LoadMatOnGrid(_MapMatrix.Points[rowIndex,columnIndex]);
            }
        }
    }

    //生成地图格子-私有方法
    private void GenerateMapGrid(int rows, int columns)
    {

    }
    //加载网格子上加载资源-私有方法
    private bool LoadMatOnGrid(RPoint point)
    {
        //从库中Find资源挂在到地图格子上
        return true;

    }
#endregion
}
