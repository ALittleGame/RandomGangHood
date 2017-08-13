using System;
using System.Collections.Generic;

/// <summary>
/// 所有玩家、NPC、怪物的基类
/// </summary>
public class RCreature
{
#region // 静态变量及函数
    private static Int64 idCount = 0;  //所有
    private static Int64 GenerateID() { return ++idCount; }
#endregion

    // ---------------------------------------------------
    private Int64 id;       // 生物id
    private String name;    // 生物名称

    private String mapName; // 所在地图名称
    private Int64 pointId;  // 所在的地图点id

    public RCreature(string name, string mapName, long pointId)
    {
        id = RCreature.GenerateID();  // 注意：所有派生类的构造函数都要复制这一行过去
        this.name = name;
        this.mapName = mapName;
        this.pointId = pointId;
    }

    /// <summary>
    /// 移动到相邻的地图点
    /// </summary>
    /// <param name="pointId">目标点id</param>
    /// <returns></returns>
    public Boolean MoveToNeighborPoint(Int64 pointId)
    {
        // TODO: chenyufei 检查是否相邻

        // 移动过去
        MoveToPoint(pointId);

        return true;
    }

    /// <summary>
    /// 直接飞到某个点
    /// </summary>
    /// <param name="pointId">目标点id</param>
    /// <returns></returns>
    public Boolean MoveToPoint(Int64 pointId)
    {
        return true;
    }

    /// <summary>
    /// 直接飞到某个地图，可以指定飞到哪个点
    /// </summary>
    /// <param name="mapName">目标地图名称</param>
    /// <param name="pointId"></param>
    /// <returns></returns>
    public Boolean MoveToMap(String mapName, Int64 pointId = 0)
    {
        // TODO: chenyufei 检查目标地图和目标点是否有效

        // 考虑一下飞如果pointId==0，飞到哪个默认点
        return true;
    }

    /// <summary>
    /// 访问当前所在的地图点（当表现层的人物移动到目标点的时候，由表现层发起调用）
    /// </summary>
    /// <returns></returns>
    public Boolean VisitCurrentPoint()
    {
        // TODO: chenyufei 检查当前所在的地图点是什么类型

        // 如果触发什么事件（遇到怪、有交易、有对话等等），则通知表现层打开对应面板
        return true;
    }
}