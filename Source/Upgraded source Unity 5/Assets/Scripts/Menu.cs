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
        p1KnapNede.GetComponent<Renderer>().enabled = false;
        p2KnapNede.GetComponent<Renderer>().enabled = false;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.S))
            GetComponent<AudioSource>().PlayOneShot(p1Sug);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            GetComponent<AudioSource>().PlayOneShot(p2Sug);

        if (Input.GetKey(KeyCode.S))
            p1KnapNede.GetComponent<Renderer>().enabled = true;
        else
            p1KnapNede.GetComponent<Renderer>().enabled = false;

        if (Input.GetKey(KeyCode.DownArrow))
            p2KnapNede.GetComponent<Renderer>().enabled = true;
        else
            p2KnapNede.GetComponent<Renderer>().enabled = false;

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.DownArrow))
            Application.LoadLevel(2);
	
	}
}
