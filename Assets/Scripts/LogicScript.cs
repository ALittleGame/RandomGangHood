using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

#region
    public MapManager GetMapManager()
    {
        return MapManager.Instance();
    }
#endregion
}
