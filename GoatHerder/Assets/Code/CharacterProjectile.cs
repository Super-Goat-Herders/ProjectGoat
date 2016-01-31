using UnityEngine;
using System.Collections;

public class CharacterProjectile : MonoBehaviour {
    public float speed;
    public float timer;

	// Use this for initialization
	void Start () {
        speed = 3f;
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (timer > 8)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "Wall")
        {
            Debug.Log("We got here");
            speed = 0;
            //If you want arrows to stick to the wall, limit the number of them and disable it's ability to cause damage
            Destroy(gameObject);
        }
        timer += Time.deltaTime;

    }
}
