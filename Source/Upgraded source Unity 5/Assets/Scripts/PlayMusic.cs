using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour {

    public AudioClip sound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {	
	}

    public void PlaySound()
    {
        GetComponent<AudioSource>().PlayOneShot(sound);
        return;
    }
}
