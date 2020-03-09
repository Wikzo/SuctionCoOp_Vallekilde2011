using UnityEngine;
using System.Collections;

public class LogoScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
        transform.localScale = renderer.material.mainTextureScale;
	
	}
}
