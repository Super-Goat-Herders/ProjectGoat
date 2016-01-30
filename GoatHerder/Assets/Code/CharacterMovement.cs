using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    float speed = 2f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;
        //Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, -10f);
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Door")
        {
            Debug.Log("YOU DID IT!");
        }
    }

    void ApplyDamage(int damage)
    {
        
    }
}
