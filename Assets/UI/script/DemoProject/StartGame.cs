﻿/***
*		Name:
*		Des	:
*		Date:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    public void Start()
    {
        UIManager.GetInstance().ShowUIForms("LogonUIForm");
    }

}
