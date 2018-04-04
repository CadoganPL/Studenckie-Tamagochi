using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EventManager))]
public class ProgressBar : MonoBehaviour {


    [SerializeField]
    GameObject bar; // blank gameobject, it is meant to hold reference to actuall bar in the UI.
    // Use this for initialization
    private float progress = 0.0f;
    private float progressIncrease = 10.0f;
    private float progressDecrease = 10.0f;
    const float maxProgress = 100.0f;

    private UnityAction increaseProgressDelegate; // UnityAction is a delegate defined by Unity to use it with event system etc.
    private UnityAction decreseProgressDelegate; // instead of C#'s delegates.

    void Awake () {
        increaseProgressDelegate += IncreaseProgress;
        decreseProgressDelegate += DecreaseProgress;
	}
	
	// Update is called once per frame
	void Update () {
        ModifyProgressBar();
	}

    private void DecreaseProgress()
    {
        progress -= progressDecrease; 
        progress = progress < 0 ? 0 : progress; //ensuring progress is always equal or above 0.
    }
    private void IncreaseProgress()
    {
        progress += progressIncrease;
        progress = progress > maxProgress ? maxProgress : progress; // ensuring prograss is always equal or below maxProgress constant.
    }

    void ModifyProgressBar()
    {
        //This method is supposed to change values of progress bars etc.,
        //however there's still not any UI for minigames, so i leave it blank for later implementation.
    }
}
