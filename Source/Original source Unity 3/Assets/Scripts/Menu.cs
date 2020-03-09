using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    public AudioClip p1Sug;
    public AudioClip p2Sug;

    public GameObject p1KnapNede;
    public GameObject p2KnapNede;

	// Use this for initialization
	void Start ()
    {
        p1KnapNede.renderer.enabled = false;
        p2KnapNede.renderer.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.S))
            audio.PlayOneShot(p1Sug);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            audio.PlayOneShot(p2Sug);

        if (Input.GetKey(KeyCode.S))
            p1KnapNede.renderer.enabled = true;
        else
            p1KnapNede.renderer.enabled = false;

        if (Input.GetKey(KeyCode.DownArrow))
            p2KnapNede.renderer.enabled = true;
        else
            p2KnapNede.renderer.enabled = false;

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.DownArrow))
            Application.LoadLevel(2);
	
	}
}
