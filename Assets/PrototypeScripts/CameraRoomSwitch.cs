using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoomSwitch : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject[] rooms;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchRoom(int i)
    {
        transform.position = new Vector3(rooms[i].transform.position.x,rooms[i].transform.position.y,transform.position.z);
    }
}
