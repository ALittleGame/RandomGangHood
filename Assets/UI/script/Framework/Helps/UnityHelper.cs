/***
*		Name:unity帮助文件
*		Des	:提供程序用户一些常用的功能方法，方便快速开发
*		Date:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityHelper : MonoBehaviour {

    //查找子对象方法(递归)
    public static Transform FindTheChildNode(GameObject Parent, string ChildNameStr)
    {
        Transform searchres = null;

        searchres = Parent.transform.Find(ChildNameStr);
        if (searchres == null)
        {
            foreach (Transform trans in Parent.transform)
            {
                searchres = FindTheChildNode(trans.gameObject, ChildNameStr);
                if (searchres != null)
                {
                    return searchres;
                }
            }
        }

        return searchres;
    }

    public static GameObject FindTheChildNodeObj(GameObject Parent, string ChildNameStr)
    {
        GameObject resObj = null;
        Transform searchres = null;

        searchres = Parent.transform.Find(ChildNameStr);
        if (searchres == null)
        {
            foreach (Transform trans in Parent.transform)
            {
                searchres = FindTheChildNode(trans.gameObject, ChildNameStr);
                if (searchres != null)
                {
                    resObj = searchres.gameObject;
                    return resObj;
                }
            }
        }
        else
        {
            resObj = searchres.gameObject;
        }
        return resObj;
    }

    //获取子节点的脚本
    public static T GetTheChildNodeComponentScript<T>(GameObject Parent, string ChildNameStr) where T : Component
    {
        Transform searchTransNode = null;
        searchTransNode = FindTheChildNode(Parent, ChildNameStr);
        if(searchTransNode !=  null)
        {
            return searchTransNode.gameObject.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }


    //给子节点添加脚本
    public static T AddTheChildNodeComponentScript<T>(GameObject Parent, string ChildNameStr) where T : Component
    {
        Transform searchTransNode = null;
        searchTransNode = FindTheChildNode(Parent, ChildNameStr);
        if (searchTransNode != null)
        {
            //如果有则先删除
            T[] componentArr = searchTransNode.GetComponents<T>();
            for(int i = 0; i < componentArr.Length; ++i)
            {
                if(componentArr[i] != null)
                {
                    Destroy(componentArr[i]);
                }
            }

            return searchTransNode.gameObject.AddComponent<T>();
        }
        else
        {
            return null;
        }
    }

    //给子节点添加父对象
    public static void AddChildNodeToParentNode(Transform parent, Transform child)
    {
        child.SetParent(parent, false);
        child.localPosition = Vector3.zero;
        child.localScale = Vector3.one;
        child.localEulerAngles = Vector3.zero;
    }
}
