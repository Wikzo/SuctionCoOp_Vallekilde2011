using UnityEngine;
using System.Collections;

public class StartMusicMenu : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        DontDestroyOnLoad(transform.gameObject);
        Movement.suckTimes = 0;
	}
	
	void Update()
	{
        if (Application.loadedLevelName == "win")
            Destroy(transform.gameObject);
	}
}
