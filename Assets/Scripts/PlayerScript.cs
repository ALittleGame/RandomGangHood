using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    Vector2 targetPosition = new Vector2(0,0);
    public float speed = 0.1f;

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
            bool canMove = true;
            // TODO: 先检查界面上的目标点附近有没有“地点”，如果没有，则canMove为false
            targetPosition = mousePositionInWorld;

            // TODO: chenyufei 这里要通知逻辑层进行移动
        }

        // 移动到目标位置
        if(Vector2.Distance(transform.position, targetPosition) > 1)
        {
            float deltaX = targetPosition.x - transform.position.x;
            deltaX = deltaX > 0 ? 1 : -1;
            deltaX = transform.position.x + deltaX * speed;
            float deltaY = targetPosition.y - transform.position.y;
            deltaY = deltaY > 0 ? 1 : -1;
            deltaY = transform.position.y + deltaY * speed;

            Vector2 tempPos = new Vector2(deltaX, deltaY);
            transform.position = tempPos;
        }

    }  // Update

    public bool SetTargetPosition(float x, float y)
    {
        
        targetPosition = new Vector2(x, y);

        return true;
    }
}
