/***
*		Name:ResourcesMgr
*		Des	:资源加载管理器
*		    本功能是在unity的resources类的基础上，增加的缓存处理
*		Date:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ResourcesMgr : MonoBehaviour {

    /*字段*/
    private static ResourcesMgr _Instance;      //单利实例
    private Hashtable ht = null;                //容器键值对集合


    /// <summary>
    /// 得到实例
    /// </summary>
    /// <returns></returns>
    public static ResourcesMgr GetInstance()
    {
        if(_Instance == null)
        {
            _Instance = new GameObject("_ResourceMgr").AddComponent<ResourcesMgr>();
        }
        return _Instance;
    }

    private void Awake()
    {
        ht = new Hashtable();
    }
    /// <summary>
    /// 调用资源（带资源缓存技术）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="isCatch"></param>
    /// <returns></returns>
    public T LoadResource<T>(string path, bool isCatch) where T: UnityEngine.Object
    {
        if(ht.Contains(path))
        {
            return ht[path] as T;
        }

        T TReseource = Resources.Load<T>(path);
        if(TReseource == null)
        {
            Debug.LogError(GetType() + "/GetInstance()/TResource 提取的资源找不到 path = " + path );
        }
        else if(isCatch)
        {
            ht[path] = TReseource;
        }
        return TReseource;
    }
    /// <summary>
    /// 调用资源（带对象缓存技术）
    /// </summary>
    /// <param name="path"></param>
    /// <param name="isCatch"></param>
    /// <returns></returns>
    public GameObject LoadAsset(string path, bool isCatch)
    {
        GameObject goObj = LoadResource<GameObject>(path, isCatch);
        GameObject goObjClone = GameObject.Instantiate<GameObject>(goObj);
        if(goObjClone == null)
        {
            Debug.LogError(GetType() + "/LoadAaaet()/克隆资源不成功，请检查 path=" + path);
        }

        return goObjClone;
    }
}
