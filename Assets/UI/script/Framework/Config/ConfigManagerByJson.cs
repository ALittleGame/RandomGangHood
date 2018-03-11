/***
*		Name:
*		Des	:
*		Date:
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManagerByJson : IConfigManger
{
    //保存键值对集合
    private static Dictionary<string, string> _AppSetting;

    /// <summary>
    /// 得到配置
    /// </summary>
    public Dictionary<string, string> AppSetting
    {
        get
        {
            return _AppSetting;
        }
    }
    
    public ConfigManagerByJson(string jsonpath)
    {
        _AppSetting = new Dictionary<string, string>();
        InitAndAnalysisJson(jsonpath);
    }

    public int GetAppSettingMaxNumber()
    {
        if (_AppSetting != null && _AppSetting.Count >= 1)
            return _AppSetting.Count;
        else
            return 0;
    }

    private void InitAndAnalysisJson(string path)
    {
        TextAsset ConfigInfo = null;
        KeyValueInfo kviobj = null;

        if (string.IsNullOrEmpty(path))
            return;

        try
        {
            ConfigInfo = Resources.Load<TextAsset>(path);
            kviobj = JsonUtility.FromJson<KeyValueInfo>(ConfigInfo.text);
        }
        catch
        {
            throw new JsonAnlysisException(GetType() + "json解析错误 path = " + path);
        }

        foreach(KeyValueNode nodeinfo in kviobj.ConfigInfo)
        {
            _AppSetting.Add(nodeinfo.Key, nodeinfo.Value);
        }
    }
}

