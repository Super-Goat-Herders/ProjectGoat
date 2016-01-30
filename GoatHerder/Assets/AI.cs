using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
    public Transform target;
    public Vector3 targetPosition;
    public float speed;

	// Use this for initialization
	void Start () {
        speed = 1f;
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
	}
}
