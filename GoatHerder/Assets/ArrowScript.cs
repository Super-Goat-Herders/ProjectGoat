using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
    public Vector3 myTarget;
    public Vector3 myRotation;
    public Vector3 myStart;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void setup(Vector3 target, Vector3 rotation, Vector3 start)
    {
        myTarget = target;
    }
}
