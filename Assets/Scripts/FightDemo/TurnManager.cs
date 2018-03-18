using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    bool isFighting = false;  // 是否在战斗状态
    Int64  runningFighter = 0;  // 正在进行回合的角色

    LinkedList<Fighter> fighterList = new LinkedList<Fighter>();


    private static readonly TurnManager instance = new TurnManager();
    public static TurnManager Instance()
    {
        return instance;
    }

    private TurnManager()
    {
        
    }

	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update () {
        // TODO 检查如果isFighting，且runningFighter为0，就寻找列表头部的角色，将回合交给他
        if (isFighting && fighterList.Count > 0)
        {
            if (runningFighter == 0)
                fighterList.First.Value.BeginTurn();
        }
    }

    /// <summary>
    /// 向战斗中加入一个角色
    /// TODO 以后从参数传入一个大世界存在的角色
    /// </summary>
    /// <returns></returns>
    public int AddFighter(Int64 id)
    {
        Fighter fighter = new Fighter();
        LinkedListNode<Fighter> fighterNode = new LinkedListNode<Fighter>(fighter);
        fighterList.AddLast(fighterNode);
        return 0;
    }

    /// <summary>
    /// 当前正在行动的角色调用该函数后，将结束行动
    /// </summary>
    /// <returns></returns>
    public int FinishTurn(Int64 id)
    {
        if(id == runningFighter)
        {
            runningFighter = 0;

            UpdateList();
            return 0;
        }
        return 1;
    }

    /// <summary>
    /// 将 fighterList 的第一个角色，移到下次行动的位置
    /// 目前是移到队尾，以后可以根据行动速度来计算行动槽，然后根据槽的进度插入排序
    /// </summary>
    /// <returns></returns>
    private int UpdateList()
    {
        LinkedListNode<Fighter> fighterNode = fighterList.First;
        fighterList.RemoveFirst();
        fighterList.AddLast(fighterNode);
        return 0;
    }
}
