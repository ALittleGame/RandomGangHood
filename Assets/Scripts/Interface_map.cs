using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Interface_map : MeshDrawBase {

    public int xSize = 10;         //横向总长度
    public int ySize = 10;         //纵向总长度

    public float segXLength = 1;   //横向单位长度
    public float segYLength = 1;   //纵向单位长度

    private int vtXCount;          //横向顶点数量
    private int vtYCount;          //纵向顶点数量

    private List<Vector3> vts = new List<Vector3>();

    //地图表现层，接收逻辑层传来的数据，生成地图
    // Use this for initialization
    void Start () {

        //test
        RMap map = new RMap(100,100);
        SetMap(map, 1, 1, true);
	}
	
	// Update is called once per frame
	void Update () {
        if(_dirty)
        {
            DrawMesh();
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
    public void SetMap(RMap map, float segX = 1, float segY = 1, bool dirty = false)
    {
        int rows = map.Path.GetLength(0);
        int columns = map.Path.GetLength(1);

        xSize = rows;
        ySize = columns;
        segXLength = segX;
        segYLength = segY;
        vtXCount = (int)(xSize / segXLength + 1);
        vtYCount = (int)(ySize / segYLength + 1);

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
    protected override void DrawMesh()
    {

        mh = new Mesh();


        //顶点
        for (int i = 0; i < vtYCount; ++i)
        {
            for (int j = 0; j < vtXCount; ++j)
            {
                Vector3 point = new Vector3(j * segXLength, i * segYLength, 0);
                vts.Add(point);
            }
        }
        mh.vertices = vts.ToArray();

        //三角形
        tris = new int[(vtXCount - 1) * (vtYCount - 1) * 6];
        for (int i = 0, vi = 0, ti = 0; i < vtYCount - 1; ++i, ++vi)
        {
            for (int j = 0; j < vtXCount - 1; ++j, ti += 6, ++vi)
            {
                tris[ti] = vi;
                tris[ti + 1] = tris[ti + 4] = vtXCount - 1 + vi + 1;
                tris[ti + 2] = tris[ti + 3] = vi + 1;
                tris[ti + 5] = vtXCount - 1 + vi + 2;
            }
        }
        mh.triangles = tris;

        //uv
        uvs = new Vector2[vts.Count];

        float uvOffsetX = 1f / vtXCount;
        float uvOffsetY = 1f / vtYCount;

        for (int i = 0; i < vtXCount; ++i)
        {
            for (int j = 0; j < vtYCount; ++j)
            {
                uvs[i * vtXCount + j] = new Vector2(j * uvOffsetX, i * uvOffsetY);
            }
        }
        mh.uv = uvs;

        mh.RecalculateNormals();
        mh.RecalculateTangents();

        targetFilter.mesh = mh;
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

   
    //顶点添加纹理（后续可以做成顶点加载图片）
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < vts.Count; ++i)
        {
            Vector3 worldPoint = transform.TransformPoint(vts[i]);
            Gizmos.DrawSphere(worldPoint, .2f);
        }
    }
    #endregion
}
