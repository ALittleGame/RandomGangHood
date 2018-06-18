using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleUIForm : BaseUIform
{
    GameObject[] BtnList;
    GameObject[] PanelList;

    private void Awake()
    {
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.Normal;            //位置类型
        base.CurrentUIType.UIForm_ShowMode = UIFormShowMode.Normal;     //弹出类型
        base.CurrentUIType.UIForm_LucenyTpy = UIFormLucenyTpye.Lucency; //透明度与穿透类型
        /* 给按钮注册事件 */
        RigisterButtonObjEvent("BtnProp", OnBtnPropClick);
        RigisterButtonObjEvent("BtnEquip", OnBtnEquipClick);
        RigisterButtonObjEvent("BtnAlly", OnBtnAllyClick);
        RigisterButtonObjEvent("BtnPack", OnBtnPackClick);
    }

    public void Start()
    {
        // 对标签按钮的初始化
        BtnList = new GameObject[4];
        BtnList[0] = UnityHelper.FindTheChildNodeObj(this.gameObject, "BtnProp");
        BtnList[1] = UnityHelper.FindTheChildNodeObj(this.gameObject, "BtnEquip");
        BtnList[2] = UnityHelper.FindTheChildNodeObj(this.gameObject, "BtnAlly");
        BtnList[3] = UnityHelper.FindTheChildNodeObj(this.gameObject, "BtnPack");

        // 对每个标签界面的初始化
        PanelList = new GameObject[4];
        PanelList[0] = UnityHelper.FindTheChildNodeObj(this.gameObject, "PanelProp");
        PanelList[1] = UnityHelper.FindTheChildNodeObj(this.gameObject, "PanelEquip");
        PanelList[2] = UnityHelper.FindTheChildNodeObj(this.gameObject, "PanelAlly");
        PanelList[3] = UnityHelper.FindTheChildNodeObj(this.gameObject, "PanelPack");

        EnableAllBtn();
    }

    private void OnBtnPropClick(GameObject go)
    {
        EnableAllBtn();
        go.GetComponent<Button>().interactable = false;
        PanelList[0].SetActive(true);
    }

    private void OnBtnEquipClick(GameObject go)
    {
        EnableAllBtn();
        go.GetComponent<Button>().interactable = false;
        PanelList[1].SetActive(true);
    }

    private void OnBtnAllyClick(GameObject go)
    {
        EnableAllBtn();
        go.GetComponent<Button>().interactable = false;
        PanelList[2].SetActive(true);
    }

    private void OnBtnPackClick(GameObject go)
    {
        EnableAllBtn();
        go.GetComponent<Button>().interactable = false;
        PanelList[3].SetActive(true);
    }

    private void EnableAllBtn()
    {
        // 把所有标签设置为不可点，隐藏所有对应Panel
        for (int i = 0; i < BtnList.Length; i++)
        {
            BtnList[i].GetComponent<Button>().interactable = true;
            PanelList[i].SetActive(false);
        }
    }
}
