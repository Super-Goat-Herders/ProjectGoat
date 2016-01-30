using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
    public Vector3 myTarget;
    public Vector3 myRotation;
    public Vector3 myStart;
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
        Debug.Log("We are getting here");
        if (coll.gameObject.tag == "Hero")
        {
            //coll.gameObject.SendMessage("ApplyDamage", 10);
            Destroy(gameObject);
        }
        if (coll.gameObject.tag == "Wall")
        {
            speed = 0;
        }


    }
}
