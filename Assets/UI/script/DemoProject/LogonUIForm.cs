/***
*		Name:
*		Des	:
*		Date:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogonUIForm : BaseUIform {

    private void Awake()
    {
        CurrentUIType.UIForms_Type = UIFormType.Normal;
        CurrentUIType.UIForm_ShowMode = UIFormShowMode.HideOther;
        this.Display();
    }

}
