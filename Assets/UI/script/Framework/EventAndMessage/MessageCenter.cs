/***
*		Name:
*		Des	:
*		Date:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCenter {
    //委托：消息传递
    public delegate void DelMessageDeLivery(KeyValueUpdate kv);

    /// <summary>
    /// 消息中心缓存集合
    /// string表示所要监听的类型
    /// DelMessageDeLivery委托方法
    /// </summary>
    public static Dictionary<string, DelMessageDeLivery> _dicMessages = new Dictionary<string, DelMessageDeLivery>();

    /// <summary>
    /// 添加消息的监听
    /// </summary>
    /// <param name="messageType">消息分类</param>
    /// <param name="handler">委托方法</param>
    public static void AddMsgListener(string messageType, DelMessageDeLivery handler)
    {
        if(!_dicMessages.ContainsKey(messageType))
        {
            _dicMessages.Add(messageType, null);
        }
        _dicMessages[messageType] += handler;
    }

    /// <summary>
    /// 取消消息的监听
    /// </summary>
    /// <param name="messageType">消息分类</param>
    /// <param name="handler">消息委托</param>
    public static void RemoveMsgListener(string messageType, DelMessageDeLivery handler)
    {
        if(_dicMessages.ContainsKey(messageType))
        {
            _dicMessages[messageType] -= handler;
        }
    }

    public static void CleanMsgAllListrner()
    {
        if(_dicMessages != null)
        {
            _dicMessages.Clear();
        }
    }

    public static void SendMsg(string messageType, KeyValueUpdate kv)
    {
        DelMessageDeLivery del;
        if(_dicMessages.TryGetValue(messageType, out del))
        {
            if(del != null)
            {
                del(kv);
            }
        }
    }
}

public class KeyValueUpdate
{   
    //键值对，只读属性
    private string Key;
    private object value;

    public string Key1
    {
        get
        {
            return Key;
        }
    }

    public object Value
    {
        get
        {
            return value;
        }
    }

    public KeyValueUpdate(string key, object valueObj)
    {
        Key = key;
        value = valueObj;
    }
}
