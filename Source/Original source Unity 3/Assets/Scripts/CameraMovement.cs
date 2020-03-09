using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public GameObject cylinder;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //print("Loaded level: " + Application.loadedLevel + " and count: " + Application.levelCount);
		transform.position = new Vector3(cylinder.transform.position.x, cylinder.transform.position.y, transform.position.z);
	
	}
}
