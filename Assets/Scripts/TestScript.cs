using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这是个编写demo的地方
/// </summary>
public class TestScript : MonoBehaviour {
    private GameObject logicObj;  // 长期保留这个对象，启动的时候GameObject.Find一次，以后直接使用，提高效率

    // Use this for initialization
    void Start () {
        // ------------------以下代码演示表现层的脚本如何操作逻辑层
        // 获取LogicObject对象
        logicObj = GameObject.Find("LogicObject");
        if (logicObj == null)
            return;

        // 获取逻辑对象上的LogicScript
        LogicScript logicScript = logicObj.GetComponent<LogicScript>();
        if (logicScript == null)
            return;

        // 添加地图
        if(!logicScript.GetMapManager().AddMap("testmap", new RMap(10, 10)))
        {
            // 如果地图添加失败，打个提示，或者做点什么处理
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
