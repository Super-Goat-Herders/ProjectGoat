using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
    public float speed;

	// Use this for initialization
	void Start () {
        speed = 3f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * speed * Time.deltaTime;
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("We got here to collision");
        if (coll.gameObject.tag == "Hero")
        {
            coll.gameObject.SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        } else {
            speed = 0; Destroy(gameObject);
        }

        if (coll.gameObject.tag == "Wall")
        {
            Debug.Log("We got here");
            speed = 0;
            //If you want arrows to stick to the wall, limit the number of them and disable it's ability to cause damage
            Destroy(gameObject);
        }
    }
}
