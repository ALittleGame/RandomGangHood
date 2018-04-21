/***
*		Name:登陆界面
*		Des	:
*		Date:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogonUIForm : BaseUIform {

    private void Awake()
    {

        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.Normal;            //位置类型
        base.CurrentUIType.UIForm_ShowMode = UIFormShowMode.Normal;     //弹出类型
        base.CurrentUIType.UIForm_LucenyTpy = UIFormLucenyTpye.Lucency; //透明度与穿透类型
        /* 给按钮注册事件 */
        RigisterButtonObjEvent("EnterGameBtn", OnEnterGameBtnClick);     
    }

    private void OnEnterGameBtnClick(GameObject go)
    {
        GameObject accountObj = UnityHelper.FindTheChildNodeObj(this.gameObject, "Account");
        GameObject passwordObj = UnityHelper.FindTheChildNodeObj(this.gameObject, "Password");
        if (accountObj != null && passwordObj != null)
        {
            InputField accountTest = accountObj.GetComponent<InputField>();
            InputField passwordTest = passwordObj.GetComponent<InputField>();
            if (accountTest.text == "liang" && passwordTest.text == "shuaige")
            {
                OpenUI("SelectActor");
            }
            else
            {
                Debug.Log("用户名或密码错误，请尝试以下用户名密码：liang/shuaige");
            }
        }
        else
        {
            Debug.Log("error:找不到用户名密码的gameobj");
        }
    }

}
