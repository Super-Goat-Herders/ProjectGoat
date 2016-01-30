using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    float speed = 2f;
    //bool isMoving = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;
      //  Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, -10f);
	}
}