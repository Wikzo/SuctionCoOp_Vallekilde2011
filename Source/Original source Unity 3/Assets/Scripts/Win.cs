using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {

    public GUIStyle style;
    public GUIStyle suckTimesStyle;

    string endingText = "Congratulations! You win!\nBut you also sucked ...";
    string suckTimesText;

    public int text1X= 100;
    public int text1Y = 100;
    public int text2X = 100;
    public int text2Y = 100;

    public Rect GUIRectPosition1;
    public Rect GUIRectPosition2;

    public AudioClip win_fanfare;

    float time = 5f;

	// Use this for initialization
	void Start ()
    {
        //GUIRectPosition1 = new Rect(Screen.width / 2 - text1X, Screen.height / 2 - text1Y, text1X, text1Y);
        GUIRectPosition2 = new Rect(Screen.width / 2 - text2X, Screen.height / 2 - text2Y, text2X, text2Y);
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        GUIRectPosition1 = new Rect(Screen.width / 2 - text1X, Screen.height / 2 - text1Y, 800, 100);
        GUIRectPosition2 = new Rect(Screen.width / 2 - text2X, Screen.height / 2 - text2Y, text2X, text2Y);

        suckTimesText = Movement.suckTimes + " times";

        time -= Time.deltaTime;

        if (time < 0 && Input.anyKey)
        {
            Application.LoadLevel(0);
        }

    }

    void OnGUI()
    {
        // You winn
        GUI.Label(GUIRectPosition1, endingText, style);
        
        // ... but you also sucked
        GUI.Label(GUIRectPosition2, suckTimesText, suckTimesStyle);

    }
}
