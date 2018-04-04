using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EventManager))]
public class Timer : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private float _playTimeInSeconds;
    [SerializeField]
    GameObject _timeDisplay; //object meant to hold reference for time display

    private Text _timerText;
    private float _timePassed = 0.0f;

    void Awake ()
    {
        _timerText = gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        _timePassed += Time.deltaTime;
        
        if(_timePassed >= _playTimeInSeconds)
        {
            EventManager.TriggerEvent("GameOver");
        }
        else UpdateUITimer();
	}

    void UpdateUITimer()
    {
        string secMil = string.Format("{0}:{1:00}", (int)(_playTimeInSeconds - _timePassed) % 60, (int)(((_playTimeInSeconds - _timePassed)-(int)(_playTimeInSeconds - _timePassed) % 60) *1000));
        _timerText.text = "" + secMil+"s";
    }
}
