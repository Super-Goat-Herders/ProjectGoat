using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Update () {
		DestroyObject (gameObject,1.5f);
	}
}
