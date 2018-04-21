/***
*		Name: 开始游戏
*		Des	: 开始游戏的逻辑（目前只是打开登陆ui，后续可能会有其他逻辑的加入）
*		Date:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    public void Start()
    {
        UIManager.GetInstance().ShowUIForms("LogonUIForm");
    }

}
