/***
*		Name: ui脚本模板
*		Des	: 新建ui脚本规范
*		Date:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


#region  ui创建步骤
/**
 * 1、新建一个场景，将Assets/Resources/Prefabs里的UIRoot拖到场景中（重要，一定要在uiroot下新建）
 * 2、在UIRoot下新建ui panel，此panel为新建的界面，改相应的名字，同时在Assets/UI/script/DemoProject中新建同名c#脚本
 * 3、拼ui界面、仿照本模板写脚本
 * 4、拼好了之后将脚本挂到这个界面上
 * 5、将拼好的界面拖到Assets/Resources/Prefabs中保存为prefab
 * 6、在Assets/UI/script/Framework/UIManager中的Awake函数中注册新建界面的路径  
 * 
 * 
 * 注意事项：1如果想修改prefab，不要直接在prefab上修改，应当把这个预制体拖到场景中修改，然后点apply
 *           2提交的时候一定要把.meta文件一并上传
 *           3游戏开始脚本是StartGame，将场景中一个空对象挂上这个脚本直接运行就行
 */
#endregion

public class ReadMe : BaseUIform                            //①所有ui界面一定要继承BaseUIform
{
    //
    private void Awake()
    {

        //定义本窗体的性质(默认数值，可以不写)              //②在Awake中注册ui的以下属性，具体请看定义
        base.CurrentUIType.UIForms_Type = UIFormType.Normal;            //位置类型
        base.CurrentUIType.UIForm_ShowMode = UIFormShowMode.Normal;     //弹出类型
        base.CurrentUIType.UIForm_LucenyTpy = UIFormLucenyTpye.Lucency; //透明度与穿透类型


        /* 给按钮注册事件 */                                //③用委托方式注册按钮的各种事件，也可以用ugui自己的
        //RigisterButtonObjEvent("EnterGameBtn", OnEnterGameBtnClick);

        //
    }
}
