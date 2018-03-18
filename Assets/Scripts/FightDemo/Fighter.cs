using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {

    private bool isMyTurn = false;
    private Int64 id = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isMyTurn)
        {
            // TODO 用某种方式找到敌人并攻击，还没想好怎么获取敌人列表
        }
	}

    public int BeginTurn()
    {
        isMyTurn = true;
        return 0;
    }

    public void FinishTurn()
    {
        isMyTurn = false;
        TurnManager.Instance().FinishTurn(id);
    }
}
