/***
*		Name: ui遮罩管理器
*		Des	:负责弹出窗体模态显示
*		Date:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMaskMgr : MonoBehaviour {

    /* 字段 */
    //私有单例
    private static UIMaskMgr _Instance = null;
    //ui根节点对象
    private GameObject _GoCanvasRoot = null;
    //ui脚本节点对象
    private Transform _TraUIScriptsNode = null;
    //顶层面板
    private GameObject _GoTopPanel;
    //遮罩面板
    private GameObject _GoMaskPanel;
    //ui摄像机
    private Camera _UICamera;
    //ui摄像机原始层深
    private float _OriginalUICameraDepth;


    //得到实例
    public static UIMaskMgr GetInstance()
    {
        if(_Instance == null)
        {
            _Instance = new GameObject("_UIMaskMgr").AddComponent<UIMaskMgr>();
        }

        return _Instance;
    }

    private void Awake()
    {
        //得到ui根节点对象、脚本节点对象
        _GoCanvasRoot = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS);
        _TraUIScriptsNode = UnityHelper.FindTheChildNode(_GoCanvasRoot, SysDefine.SYS_SCRIPTMGR_NODE);
        //把本脚本实例加载到脚本节点子节点下
        UnityHelper.AddChildNodeToParentNode(_TraUIScriptsNode, this.gameObject.transform);
        //得到顶层面板、以及遮罩面板
        _GoTopPanel = _GoCanvasRoot;
        _GoMaskPanel = UnityHelper.FindTheChildNode(_GoCanvasRoot, "_UIMaskPanel").gameObject;
        //得到ui摄像机原始层深，保证操作的ui摄像机层深最大，防止被遮挡
        _UICamera = GameObject.FindGameObjectWithTag("_TagUICamera").GetComponent<Camera>();
        if(_UICamera != null)
        {
            //原始层深
            _OriginalUICameraDepth = _UICamera.depth;
        }
        else
        {
            Debug.Log(GetType() + "ui_camera is null!");
        }
    }

    /// <summary>
    /// 设置遮罩状态
    /// </summary>
    /// <param name="goDisplayUIForms">需要显示的ui窗体</param>
    /// <param name="lucenytype">显示的透明度</param>
    public void SetMaskPanel(GameObject goDisplayUIForms, UIFormLucenyTpye lucenytype = UIFormLucenyTpye.Lucency)
    {
        //顶层窗体下移
        _GoTopPanel.transform.SetAsLastSibling();
        //启用遮罩并设置透明度
        switch(lucenytype)
        {
            //完全透明，不能穿透
            case UIFormLucenyTpye.Lucency:
                _GoMaskPanel.SetActive(true);
                Color newColor1 = new Color(255 / 255F, 255 / 255F, 255 / 255F, 0F / 255F);
                _GoMaskPanel.GetComponent<Image>().color = newColor1;
                break;

            //半透明，不能穿透
            case UIFormLucenyTpye.Translucency:
                _GoMaskPanel.SetActive(true);
                Color newColor2 = new Color(220 / 255F, 220 / 255F, 220 / 255F, 50F / 255F);
                _GoMaskPanel.GetComponent<Image>().color = newColor2;
                break;

            //低透明，不能穿透
            case UIFormLucenyTpye.ImPenetrable:
                _GoMaskPanel.SetActive(true);
                Color newColor3 = new Color(50 / 255F, 50 / 255F, 50 / 255F, 200F / 255F);
                _GoMaskPanel.GetComponent<Image>().color = newColor3;
                break;
            
            //可以穿透
            case UIFormLucenyTpye.Pentrate:
                if(_GoMaskPanel.activeInHierarchy)
                {
                    _GoMaskPanel.SetActive(false);
                }
                break;
            default:
                break;
        }

        //遮罩窗体下移
        _GoMaskPanel.transform.SetAsLastSibling();
        //显示窗体下移
        goDisplayUIForms.transform.SetAsLastSibling();
        //增加摄像机层深
        if(_UICamera != null)
        {
            _UICamera.depth = _UICamera.depth + 100;
        }
    }

    /// <summary>
    /// 取消遮罩状态
    /// </summary>
    public void CancelMaskPanel()
    {
        //顶层窗体上移
        _GoTopPanel.transform.SetAsFirstSibling();
        //禁用遮罩并设置透明度
        if(_GoMaskPanel.activeInHierarchy)
        {
            _GoMaskPanel.SetActive(false);
        }
        //遮罩窗体下移
        _GoMaskPanel.transform.SetAsFirstSibling();
        //恢复摄像机层深
        if (_UICamera != null)
        {
            _UICamera.depth = _OriginalUICameraDepth;
        }
    }
}
