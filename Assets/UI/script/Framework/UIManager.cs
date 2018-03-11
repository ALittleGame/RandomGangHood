/***
*		Name:UIManager
*		Des	:管理整个ui框架
*		Date:20171214
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    /*字段*/
    private static UIManager _Instance = null;
    //ui窗体预设路径(窗体预设名称，窗体预设路径)
    private Dictionary<string, string> _DicFormsPaths;
    //缓存所有ui窗体
    private Dictionary<string, BaseUIform> _DicALLUIForms;
    //当前显示的ui窗体
    private Dictionary<string, BaseUIform> _DicCurrentShowUIForms;
    //定义栈集合（具备反向切换属性窗体的管理）
    private Stack<BaseUIform> _StaCurrentShowUIForms;

    //ui根节点
    private Transform _TraCanvasTransform = null;
    //全屏幕显示节点
    private Transform _TraNormal = null;
    //固定显示节点
    private Transform _TraFixed = null;
    //弹出节点
    private Transform _TraPopUp = null;
    //ui管理脚本的节点
    private Transform _TraUIScripts = null;

    /// <summary>
    /// 得到manager实例
    /// </summary>
    /// <returns></returns>
    public static UIManager GetInstance()
    {
        if(_Instance == null)
        {
            _Instance = new GameObject("_UIManager").AddComponent<UIManager>();
        }
        Debug.Log("uimanager instance called");
        return _Instance;
    }
    //初始化核心数据，加载“ui窗体路径”到集合中
    public void Awake()
    {
        //字段初始化
        _DicALLUIForms = new Dictionary<string, BaseUIform>();
        _DicCurrentShowUIForms = new Dictionary<string, BaseUIform>();
        _DicFormsPaths = new Dictionary<string, string>();
        _StaCurrentShowUIForms = new Stack<BaseUIform>();
        //初始化加载canvas（根ui窗体）预设
        InitRootCanvasLoading();
        //得到ui根节点、全屏节点、固定节点、弹出节点
        _TraCanvasTransform = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS).transform;
        _TraNormal = UnityHelper.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_NORMAL_NODE);
        _TraFixed = UnityHelper.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_FIXED_NODE);
        _TraPopUp = UnityHelper.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_POPUP_NODE);
        _TraUIScripts = UnityHelper.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_SCRIPTMGR_NODE);
        //把本脚本作为“根ui窗体”的子节点
        this.gameObject.transform.SetParent(_TraUIScripts, false);
        //“根ui窗体”在场景转换的时候，不允许销毁
        DontDestroyOnLoad(_TraCanvasTransform);
        //初始化“ui窗体预设”路径数据
        //InitUIFormsPathData();
        _DicFormsPaths.Add("LogonUIForm", "Prefabs\\LogonUIForm");
        
    }
    /// <summary>
    /// 显示ui窗体
    /// 功能
    /// 1、加载与判断指定的ui窗体名称，加载到“所有ui窗体”缓存集合重
    /// 2、根据不同的ui窗体显示模式，分别做不同的加载处理
    /// </summary>
    /// <param name="uiFormName"></param>

    public void ShowUIForms(string uiFormName)
    {
        BaseUIform baseUIForms = null;             // ui窗体基类

        //参数检查
        if (string.IsNullOrEmpty(uiFormName))
            return;

        // 加载与判断指定的ui窗体名称，加载到“所有ui窗体”缓存集合中
        baseUIForms = LoadFormsToUIFormsCatch(uiFormName);
        if (baseUIForms == null)
            return;

        //是否清空栈集合
        if(baseUIForms.CurrentUIType.IsClearStack)
        {
            ClearStackArray();
        }

        // 根据不同的ui窗体显示模式，分别做不同的加载处理
        switch (baseUIForms.CurrentUIType.UIForm_ShowMode)
        {
            case UIFormShowMode.Normal:                         //普通显示
                //把当前窗体加载到当前窗体集合中
                LoadUIToCurrentCache(uiFormName);             
                break;
            case UIFormShowMode.ReverseChange:                  //反向切换显示
                PushUIFormToStack(uiFormName);
                break;
            case UIFormShowMode.HideOther:                      //隐藏其他
                EnterUIformAndHideOther(uiFormName);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 关闭ui，返回上一个ui
    /// </summary>
    /// <param name="uiFormName"></param>
    public void CloseUIForms(string uiFormName)
    {
        BaseUIform baseUIForm;
        //参数检查
        if (string.IsNullOrEmpty(uiFormName)) return;
        //所有ui窗体如果没有记录则直接返回
        _DicALLUIForms.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm == null) return;
        //根据窗体不同显示类型，分别作不同的关闭处理
        switch(baseUIForm.CurrentUIType.UIForm_ShowMode)
        {
            case UIFormShowMode.Normal:                         //普通关闭
                ExitUIForm(uiFormName);
                break;
            case UIFormShowMode.ReverseChange:                  //反向切换关闭
                PopUIForm();
                break;
            case UIFormShowMode.HideOther:                      //隐藏其他关闭
                ExitUIFormAndRePlayOther(uiFormName);
                break;
            default:
                break;
        }
    }

    #region    显示ui管理器内部核心数据，测试用
    //显示所有ui窗体的数量
    public int ShowUIFormCount()
    {
        if (_DicALLUIForms != null)
        {
            return _DicALLUIForms.Count;
        }

        else
            return 0;
    }
    //显示当前显示的ui窗体数量
    public int ShowCurrentUIFormsCount()
    {
        if (_DicCurrentShowUIForms != null)
        {
            return _DicCurrentShowUIForms.Count;
        }

        else
            return 0;
    }
    //显示栈集合中的ui窗体数量
    public int ShowCurrentStackUIFormCount()
    {
        if (_StaCurrentShowUIForms != null)
        {
            return _StaCurrentShowUIForms.Count;
        }

        else
            return 0;
    }
    #endregion



    #region  私有方法
    /// <summary>
    /// 加载与判断指定的ui窗体名称，加载到“所有ui窗体”缓存集合中
    /// 功能：检查所有ui窗体集合中是否已经加载过了，否则才加载
    /// </summary>
    /// <param name="uiFormsName"></param>
    /// <returns></returns>
    private BaseUIform LoadFormsToUIFormsCatch(string uiFormsName)
    {
        BaseUIform baseUIFormResult = null;                 //加载的反悔ui窗体基类

        _DicALLUIForms.TryGetValue(uiFormsName, out baseUIFormResult);
        if(baseUIFormResult == null)
        {
            baseUIFormResult = LoadUIForm(uiFormsName);
        }

        return baseUIFormResult;
    }
    /// <summary>
    /// 加载指定名称的ui窗体
    /// 功能：1、根据ui窗体名称，加载预设克隆体；
    ///       2、根据不同预设克隆体中带脚本中不同的位置信息，加载到根窗体下不同的节点
    ///       3、隐藏刚创建的ui克隆体
    ///       4、把克隆体加入到所有ui窗体缓存集合中
    /// </summary>
    /// <param name="uiFormName"></param>
    private BaseUIform LoadUIForm(string uiFormName)
    {
        string strUIFormPaths = null;           //ui窗体路径
        GameObject goCloneUIPrefabs = null;     // 创建的ui克隆体预设
        BaseUIform baseUIForm = null;           //  窗体基类

        //根据ui窗体名称得到对应的加载路径
        _DicFormsPaths.TryGetValue(uiFormName, out strUIFormPaths);
        //根据ui窗体名称加载预设克隆体
        if(!string.IsNullOrEmpty(strUIFormPaths))
        {
            goCloneUIPrefabs = ResourcesMgr.GetInstance().LoadAsset(strUIFormPaths, false);
        }
        //设置ui克隆体的父节点
        if(_TraCanvasTransform != null &&  goCloneUIPrefabs != null)
        {
            baseUIForm = goCloneUIPrefabs.GetComponent<BaseUIform>();
            if(baseUIForm ==  null)
            {
                Debug.Log("baseUIForm ==  null");
                return null;
            }
            switch (baseUIForm.CurrentUIType.UIForms_Type)
            {
                case UIFormType.Normal:                     //普通窗体节点
                    goCloneUIPrefabs.transform.SetParent(_TraNormal, false);
                    break;
                case UIFormType.Fixed:                      //固定窗体节点
                    goCloneUIPrefabs.transform.SetParent(_TraFixed, false);
                    break;
                case UIFormType.PopUp:                      //弹出窗体节点
                    goCloneUIPrefabs.transform.SetParent(_TraPopUp, false);
                    break;
                default:
                    break;
            }

            //设置隐藏
            goCloneUIPrefabs.SetActive(false);
            //把克隆体加入到所有ui窗体缓存集合中
            _DicALLUIForms.Add(uiFormName, baseUIForm);
            return baseUIForm;
        }
        else
        {
            Debug.Log("_TraCanvasTransform == null &&  goCloneUIPrefabs == null");
        }

        Debug.Log("error!");
        return null;
    }

    private void InitRootCanvasLoading()
    {
        ResourcesMgr.GetInstance().LoadAsset(SysDefine.SYS_PATH_CANVAS, false);
    }
    /// <summary>
    /// 加载当前窗体到缓存中
    /// </summary>
    /// <param name="uiFormName">预设窗体名称</param>
    private void LoadUIToCurrentCache(string uiFormName)
    {

        BaseUIform baseUIForm;
        BaseUIform baseUIFormFromAllCache;

        //如果正在显示的集合中存在这个ui窗体，则直接返回
        _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm != null)
            return;
        //把当前窗体加载到正在显示的集合中
        _DicALLUIForms.TryGetValue(uiFormName, out baseUIFormFromAllCache);
        if(baseUIFormFromAllCache != null)
        {
            _DicCurrentShowUIForms.Add(uiFormName, baseUIFormFromAllCache);
            baseUIFormFromAllCache.Display();//显示
        }
    }
    /// <summary>
    /// ui窗体入栈
    /// </summary>
    /// <param name="uiFormName"></param>
    private void PushUIFormToStack(string uiFormName)
    {
        BaseUIform baseUIForm;

        //判断栈集合中是否有其他窗体，如果有则冻结处理
        if(_StaCurrentShowUIForms.Count > 0)
        {
            BaseUIform topUIForm = _StaCurrentShowUIForms.Peek();
            topUIForm.Freeze();
        }
        //判断ui所有窗体集合是否有指定的ui窗体
        _DicALLUIForms.TryGetValue(uiFormName, out baseUIForm);
        if(baseUIForm != null)
        {
            baseUIForm.Display();
            //把指定的ui窗体入栈操作
            _StaCurrentShowUIForms.Push(baseUIForm);
        }
        else
        {
            Debug.Log("baseUIForm == null   参数 = " + uiFormName);
        }
    }

    /// <summary>
    /// 退出指定ui窗体
    /// </summary>
    /// <param name="uiFormName"></param>
    private void ExitUIForm(string uiFormName)
    {
        BaseUIform baseUIForm;
        //正在显示集合中没有记录则直接反悔
        _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm == null) return;
        //指定窗体，标记为“隐藏状态”，且从正在显示集合中移除
        baseUIForm.Hiding();
        _DicCurrentShowUIForms.Remove(uiFormName);
    }
    /// <summary>
    /// 反向切换关闭
    /// </summary>
    private void PopUIForm()
    {
        if(_StaCurrentShowUIForms.Count >= 2)
        {
            //出栈
            BaseUIform topUIForm = _StaCurrentShowUIForms.Pop();
            //隐藏
            topUIForm.Hiding();
            //下一个栈顶窗体重新显示
            BaseUIform nextUIForm = _StaCurrentShowUIForms.Peek();
            nextUIForm.ReDisplay();
        }
        else if(_StaCurrentShowUIForms.Count == 1)
        {
            //出栈
            BaseUIform topUIForm = _StaCurrentShowUIForms.Pop();
            //隐藏
            topUIForm.Hiding();
        }
    }
    /// <summary>
    /// 打开窗体且隐藏其他
    /// </summary>
    /// <param name="uiFormName"></param>
    private void EnterUIformAndHideOther(string uiFormName)
    {
        BaseUIform baseUIForm;
        BaseUIform baseUIFormFromALL;
        //参数检查
        if (string.IsNullOrEmpty(uiFormName)) return;
        _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm != null) return;
        //把正在显示集合与栈集合中所有窗体隐藏
        foreach(KeyValuePair<string, BaseUIform> item in _DicCurrentShowUIForms)
        {
            item.Value.Hiding();
        }
        foreach(BaseUIform staUI in _StaCurrentShowUIForms)
        {
            staUI.Hiding();
        }
        //显示当前窗体
        _DicALLUIForms.TryGetValue(uiFormName, out baseUIFormFromALL);
        if(baseUIFormFromALL != null)
        {
            _DicCurrentShowUIForms.Add(uiFormName, baseUIFormFromALL);
            //显示
            baseUIFormFromALL.Display();
        }
    }
    /// <summary>
    /// 关闭窗口并且重新显示其他
    /// </summary>
    /// <param name="uiFormName"></param>
    private void ExitUIFormAndRePlayOther(string uiFormName)
    {
        BaseUIform baseUIForm;

        //参数检查
        if (string.IsNullOrEmpty(uiFormName)) return;
        _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm == null) return;

        //将窗体从当前显示窗体中移除
        baseUIForm.Hiding();
        _DicCurrentShowUIForms.Remove(uiFormName);

        //将当前窗体集合、栈集合重新显示
        foreach(KeyValuePair<string, BaseUIform> item in _DicCurrentShowUIForms)
        {
            item.Value.ReDisplay();
        }
        foreach(BaseUIform staUI in _StaCurrentShowUIForms)
        {
            staUI.ReDisplay();
        }
    }
    /// <summary>
    /// 是否清空栈集合
    /// </summary>
    /// <returns></returns>
    private bool ClearStackArray()
    {
        if(_StaCurrentShowUIForms != null && _StaCurrentShowUIForms.Count >= 1)
        {
            _StaCurrentShowUIForms.Clear();
            return true;
        }

        return false;
    }

    /// <summary>
    /// 初始化ui路径
    /// </summary>
    private void InitUIFormsPathData()
    {
        IConfigManger configMgr = new ConfigManagerByJson("UIPathConfig");
        if(configMgr != null)
        {
            _DicFormsPaths = configMgr.AppSetting;
        }
    }

#endregion
}
