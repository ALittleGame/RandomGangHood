/***
*		Name:json解析抛异常脚本
*		Des	:
*		Date:
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonAnlysisException : Exception {

    public JsonAnlysisException() : base() { }
    public JsonAnlysisException(string exceptiontext) : base(exceptiontext) { }
}
