using UnityEngine;
using System.Collections;

public class LogoFade : MonoBehaviour {

    float countDown = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        countDown -= Time.deltaTime;

        if (countDown <= 0)
      		AutoFade.LoadLevel(1 ,3,1,Color.black);

	
	}
}
