using UnityEngine;
using System.Collections;

public class SugeFeedback : MonoBehaviour {

    public bool playerTouching = false;
    
    public AudioClip dieSound;
    //public AudioClip fanfareSound;
	
	static public bool dead = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
//print("levelcount:" + Application.levelCount);
//print("loadedlevel:" + Application.loadedLevel);

	}
    // NOT WORKING WITH LOAD LEVEL/MENU/LOSE/WIN
    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Wall")
            playerTouching = true;
        else if (collider.transform.tag == "Die")
        {
            audio.PlayOneShot(dieSound);
            Application.LoadLevel(Application.loadedLevel);	
        }
        else if (collider.transform.tag == "Win")
        {
            //audio.PlayOneShot(fanfareSound);
            
			if (Application.loadedLevel + 1 > Application.levelCount -2)
				Application.LoadLevel("win");
			else
				Application.LoadLevel(Application.loadedLevel + 1);
		}
    }

    // 4 levels
    // 7 scenes total
    void OnTriggerExit(Collider collider)
    {
        playerTouching = false;
    }
	

}

/* 
level count = 5, ... shows 5-1, 4 levels in TOTAL
start level = 1*/
