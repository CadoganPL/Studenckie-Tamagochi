using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventManager))]
public class Timer : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private float playTimeInSeconds;
    [SerializeField]
    GameObject timeDisplay; //object meant to hold reference for time display

    private float timePassed = 0.0f;

    void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        UpdateUITimer();
        if(timePassed >= playTimeInSeconds)
        {
            EventManager.TriggerEvent("GameOver");
        }
	}

    void UpdateUITimer()
    {
        //blank method meant to update time in the UI
    }
}
