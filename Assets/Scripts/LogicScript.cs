using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LogicScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    #region  // 各种Manager
    /// <summary>
    /// 获取地图管理器
    /// （在表现层从无到有显示一张地图的时候要用到？）
    /// </summary>
    /// <returns></returns>
    public MapManager GetMapManager()
    {
        return MapManager.Instance();
    }

    public CreatureManager GetCreatureManager()
    {
        return CreatureManager.Instance();
    }
    #endregion

    #region  // 操作接口

    /// <summary>
    /// 请求将某个生物的目标设定到某个地图点
    /// （这个接口只是请求移动，交给逻辑层去验证是否能够移动，并在真正允许移动时由逻辑层通知）
    /// </summary>
    /// <param name="creatureId">生物id</param>
    /// <param name="pointId">目标点id</param>
    /// <param name="jump">是否瞬间传送过去</param>
    public void CreatureMove(Int64 creatureId, Int64 pointId, Boolean jump = false)
    {
        RCreature creature = GetCreatureManager().GetCreature(creatureId);
        if (creature == null) return;

        if(jump == false)
        {
            creature.MoveToNeighborPoint(pointId);
        }
        else  // 如果是直接传送
        {
            creature.MoveToPoint(pointId);
        }
    }

    /// <summary>
    /// 请求让某个生物访问当前所在点
    /// </summary>
    /// <param name="creasureId">生物id</param>
    public void CreasureVisit(Int64 creasureId)
    {
        // TODO: chenyufei 从CreasureManager.Instance().GetCreasure(creasureId)
        // creasure.VisitCurrentPoint()
    }
    #endregion
}
