using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// 表现层入口
/// 用于逻辑层向表现层发出指令，要求表现层的物体执行某个动作
/// </summary>
public class DisplayScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 将某个生物的目标设定到某个地图点
    /// （这个接口被调用时，在逻辑层中生物会瞬间到达目标点，这意味着逻辑层是离散的，不会模拟完整的挪动过程）
    /// （如果逻辑层要完全掌控表现层的每一丝移动，就要保存表现层相关的数据，这样耦合会变大，目前不必要）
    /// </summary>
    /// <param name="creasureId">生物id</param>
    /// <param name="pointId">目标点id</param>
    /// <param name="jump">是否瞬间传送过去</param>
    public void CreasureMove(Int64 creasureId, Int64 pointId, Boolean jump = false)
    {
        // TODO: DisplayScript类设计思路： 界面上需要用unity的GameObject来表示地图上的每个点，每个GameObject里面要存一下它在逻辑层对应的点id
        // 最好在这个类中用一个map来存储每个id对应的GameObject引用

        // TODO: 该函数逻辑：找到指定点GameObject，取出坐标，找到指定生物GameObject，将移动目的地设置为指定坐标，生物对象会自己随着Update移动过去
    }
}
