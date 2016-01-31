using UnityEngine;
using System.Collections;

public class Goat : MonoBehaviour {
	public Rect windowRect = new Rect(20, 20, 120, 50);
	public bool nameGoat = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other)	{
		nameGoat = true;
	}

	void OnGUI() {
		if(nameGoat)
			windowRect = GUI.Window(0, windowRect, DoMyWindow, "My Window");
	}
	void DoMyWindow(int windowID) {
		if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
			print("Got a click");

	}
}
