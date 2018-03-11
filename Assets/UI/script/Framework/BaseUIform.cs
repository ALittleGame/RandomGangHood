/***
*		Name:BaseUIform
*		Des	:ui窗体的父类
*		     定义四个生命周期
*		     1、Display  显示状态
*		     2、Hiding   隐藏状态
*		     3、ReDisplay再显示状态
*		     4、Freeze   冻结状态
*		Date:20171214
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIform : MonoBehaviour {
    /*字段*/
    private UIType _CurrentUIType = new UIType();
    /*属性*/
    public UIType CurrentUIType
    {
        get
        {
            return _CurrentUIType;
        }

        set
        {
            _CurrentUIType = value;
        }
    }

    #region  窗体的四种状态
    /// <summary>
    /// 显示状态
    /// </summary>
    public virtual void Display()
    {
        this.gameObject.SetActive(true);

        //模态窗体调用
        if(_CurrentUIType.UIForms_Type == UIFormType.PopUp)
        {
            UIMaskMgr.GetInstance().SetMaskPanel(this.gameObject);
        }
    }

    /// <summary>
    /// 隐藏状态
    /// </summary>
    public virtual void Hiding()
    {
        this.gameObject.SetActive(false);

        //模态窗体调用
        if (_CurrentUIType.UIForms_Type == UIFormType.PopUp)
        {
            UIMaskMgr.GetInstance().CancelMaskPanel();
        }
    }

    /// <summary>
    /// 重新显示状态
    /// </summary>
    public virtual void ReDisplay()
    {
        this.gameObject.SetActive(true);

        //模态窗体调用
        if (_CurrentUIType.UIForms_Type == UIFormType.PopUp)
        {
            UIMaskMgr.GetInstance().SetMaskPanel(this.gameObject);
        }
    }

    /// <summary>
    /// 冻结状态
    /// </summary>
    public virtual void Freeze()
    {
        this.gameObject.SetActive(true);
    }
    #endregion

    #region 封装子类常用的方法

    /// <summary>
    /// 注册按钮事件
    /// </summary>
    /// <param name="buttonName">按钮名称</param>
    /// <param name="delHandle">需要注册的方法（委托）</param>
    protected void RigisterButtonObjEvent(string buttonName, SUIFW.EventTriggerListener.VoidDelegate delHandle)
    {
        GameObject button = UnityHelper.FindTheChildNode(this.gameObject, buttonName).gameObject;
        if(button != null)
        {
            SUIFW.EventTriggerListener.Get(button).onClick = delHandle;
        }
    }
    /// <summary>
    /// 打开ui窗体
    /// </summary>
    /// <param name="uiFormName"></param>
    protected void OpenUI(string uiFormName)
    {
        UIManager.GetInstance().ShowUIForms(uiFormName);
    }
    /// <summary>
    /// 关闭ui 注意类名和ui名一致
    /// </summary>
    protected void CloseUI()
    {
        string strUIName = string.Empty;

        //解析字符串，去掉命名空间
        int intPos = -1;
        strUIName = GetType().ToString();
        intPos = strUIName.IndexOf('.');
        if(intPos != -1)
        {
            strUIName = strUIName.Substring(intPos + 1);
        }

        //关闭ui
        UIManager.GetInstance().CloseUIForms(strUIName);
    }
    /// <summary>
    /// 发送方法
    /// </summary>
    /// <param name="msgType">消息类型</param>
    /// <param name="mesName">消息名称</param>
    /// <param name="msgContent">消息内容</param>
    protected void SendMessage(string msgType, string mesName, object msgContent)
    {
        KeyValueUpdate kvs = new KeyValueUpdate(mesName, msgContent);
        MessageCenter.SendMsg(msgType, kvs);
    }
    /// <summary>
    /// 接收消息
    /// </summary>
    /// <param name="messagetype">消息类型</param>
    /// <param name="handler">委托方法</param>
    protected void ReceiveMessage(string messagetype,MessageCenter.DelMessageDeLivery handler)
    {
        MessageCenter.AddMsgListener(messagetype, handler);
    }
    #endregion
}
