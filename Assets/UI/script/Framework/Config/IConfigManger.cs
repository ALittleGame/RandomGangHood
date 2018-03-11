/***
*		Name:通用配置管理器借口
*		Des	:基于json的配置解析
*		Date:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IConfigManger {

    /// <summary>
    /// 应用设置，得到设置键值对集合
    /// </summary>
    Dictionary<string, string> AppSetting { get; }

    /// <summary>
    /// 得到配置文件的最大数量
    /// </summary>
    /// <returns></returns>
    int GetAppSettingMaxNumber();
}

[Serializable]
internal class KeyValueInfo
{
    public List<KeyValueNode> ConfigInfo = null;
}

[Serializable]
internal class KeyValueNode
{
    public string Key = null;

    public string Value = null;
}
