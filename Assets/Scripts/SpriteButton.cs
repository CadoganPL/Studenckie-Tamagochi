using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class Click : UnityEvent { }
/// <summary>
/// Class that handles any sprite similarly to ui button, but without need of ui system. It will create polygon collider arround sprite and then use it to determine tap/click on it.
/// </summary>
public class SpriteButton : MonoBehaviour
{
   
    public Click OnClick;
	// Use this for initialization
	void Start ()
	{
	    var polCol = GetComponent<PolygonCollider2D>();

        if (polCol == null)
	    {
	       polCol =  gameObject.AddComponent<PolygonCollider2D>();
	    }
	    polCol.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        OnClick.Invoke();
    }

    public void TestFunction()
    {
     Debug.Log("click");
    }
}
