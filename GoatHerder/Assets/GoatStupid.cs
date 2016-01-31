using UnityEngine;
using System.Collections;

public class GoatStupid : MonoBehaviour {

	public Rect windowRect = new Rect(20, 20, 120, 50);
	public bool nameGoat = false;
	public string name = "";
	// Use this for initialization
	void Start () {
		transform.localRotation = Quaternion.Euler(0, 180, 0);
	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter2D(Collider2D other)	{
		nameGoat = true;
	}

	void OnGUI() {
		if(nameGoat)
			windowRect = GUI.Window(0, windowRect, DoMyWindow, "Name your goat..");
	}
	void DoMyWindow(int windowID) {
		name = GUI.TextField(new Rect(10, 20, 100, 20), name, 15);
		if(Input.GetKeyDown("return") || Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey ("enter")){
			nameGoat = false;
			//NEW SCENE
		}

	}

}
