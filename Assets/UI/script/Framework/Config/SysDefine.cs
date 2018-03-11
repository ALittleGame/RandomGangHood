/***
*		Name:leon
*		Des	:框架核心参数
*		     1 定义系统常量
*		     2 定义全局性方法
*		     3 定义系统枚举类型
*		     4 定义委托定义
*		Date:20171210
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region 系统枚举类型

/// <summary>
/// ui窗体的位置类型
/// </summary>
public enum UIFormType
{
    Normal,     //  普通
    Fixed,      //  固定
    PopUp,      //  弹出
}

/// <summary>
/// ui窗体的显示类型
/// </summary>
public enum UIFormShowMode
{
    Normal,         //普通
    ReverseChange,  //反向切换
    HideOther,      //隐藏其他
}

/// <summary>
/// ui窗体透明度类型
/// </summary>
public enum UIFormLucenyTpye
{
    Lucency,            //完全透明，不能穿透
    Translucency,       //半透明，不能穿透
    ImPenetrable,       // 低透明度，不能穿透
    Pentrate,           //可以穿透
}
#endregion

public class SysDefine : MonoBehaviour {

    /* 路径常量 */
    public const string SYS_PATH_CANVAS = "Prefabs\\Canvas";
    /* 标签常量 */
    public const string SYS_TAG_CANVAS = "_TagCanvas";

    /* 节点常量 */
    public const string SYS_NORMAL_NODE = "Normal";
    public const string SYS_FIXED_NODE = "Fixed";
    public const string SYS_POPUP_NODE = "PopUp";
    public const string SYS_SCRIPTMGR_NODE = "_ScriptMgr";

    /* 摄像机层深的常量 */
    //todo...
    /* 全局性方法 */
    //todo...
    /*  委托的定义 */
    //todo...
}
