/***
*		Name:
*		Des	:
*		Date:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectActor : BaseUIform {

    private void Awake()
    {
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.Normal;            //位置类型
        base.CurrentUIType.UIForm_ShowMode = UIFormShowMode.Normal;     //弹出类型
        base.CurrentUIType.UIForm_LucenyTpy = UIFormLucenyTpye.Lucency; //透明度与穿透类型
        Debug.Log("select actor panel open");
    }
}
