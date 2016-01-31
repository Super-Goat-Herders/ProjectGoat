using UnityEngine;
using System.Collections;

public class SortingLayer : MonoBehaviour {
	void Start ()
	{
		// Set the sorting layer of the particle system.
		gameObject.GetComponent<Renderer>().sortingLayerName = "Units";
	}
}
