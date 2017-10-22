using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    Vector2 targetPosition = new Vector2(0,0);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // 获取鼠标位置
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mousePositionOnScreen = Input.mousePosition;
        //mousePositionOnScreen.z = screenPosition.z;
        Vector2 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

        // 如果有鼠标左键(0)点击，则将player移动到点击的位置
        // TODO: chenyufei 以后改成不获取鼠标位置，而是获取鼠标点击的（地点）对象位置，然后把player移动过去
        if(Input.GetMouseButtonDown(0))
        {
            targetPosition = mousePositionInWorld;

            // TODO: chenyufei 这里要通知逻辑层进行移动
        }

        // 移动到目标位置
        if((Vector2)transform.position != targetPosition)
        {
            transform.position = targetPosition;  // FIXME: chenyufei 这里以后可以改成慢慢挪过去
        }

    }  // Update
}
