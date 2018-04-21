/***
*		Name:
*		Des	:
*		Date:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectActor : BaseUIform {

    private Text _actorName = null;
    private void Awake()
    {
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.Normal;            //位置类型
        base.CurrentUIType.UIForm_ShowMode = UIFormShowMode.Normal;     //弹出类型
        base.CurrentUIType.UIForm_LucenyTpy = UIFormLucenyTpye.Lucency; //透明度与穿透类型

        /* 给按钮注册事件 */
        RigisterButtonObjEvent("OkBtn", OnOKBtnClick);
        RigisterButtonObjEvent("LastPanelBtn", OnLastPanelBtnClick);
        RigisterButtonObjEvent("Actor1Btn", OnActor1BtnClick);
        RigisterButtonObjEvent("Actor2Btn", OnActor2BtnClick);

        //初始化变量
        GameObject obj = UnityHelper.FindTheChildNodeObj(this.gameObject, "ActorNameText");
        if(obj != null)
        {
            _actorName = obj.GetComponent<Text>();
        }
        else
        {
            Debug.Log("找不到角色名 " + GetType());
        }
    }
    private void Start()
    {
        
    }

    private void OnOKBtnClick(GameObject go)
    {
        Debug.Log("选好角色，进入游戏");
    }

    private void OnLastPanelBtnClick(GameObject go)
    {
        CloseUI();
    }

    private void OnActor1BtnClick(GameObject go)
    {
        _actorName.text = "陈平安";
    }

    private void OnActor2BtnClick(GameObject go)
    {
        _actorName.text = "宁姚";
    }
}
