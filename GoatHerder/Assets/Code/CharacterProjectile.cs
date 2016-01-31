using UnityEngine;
using System.Collections;

public class CharacterProjectile : MonoBehaviour {
    public float speed;
    public float timer;
    public int myDirection;
	public int arrowDamage;

	// Use this for initialization
	void Start () {
        speed = 3f;
        timer = 0;
		arrowDamage = 1;
        //Vector3 temp = new Vector3(0, 0, 0-1f);
        //GameObject.transform.position.z = -1;
        //gameObject.transform.position.z = -1;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("we are doing stuff here");
        //transform.position += transform.forward * speed * Time.deltaTime;
        if (myDirection == 0)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (myDirection == 1)
        {
            transform.position += Vector3.right  * speed * Time.deltaTime;
        }
        if (myDirection == 2)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (myDirection == 3)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (timer > 8)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            speed = 0;
        }
        timer += Time.deltaTime;

        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            coll.gameObject.SendMessage("TakeDamage", arrowDamage, SendMessageOptions.DontRequireReceiver);
        }

    }

    public void begin(int direction)
    {
        myDirection = direction;
        if (myDirection == 1)
        {
            transform.Rotate(new Vector3(0, 0, 1), 270);
        }
        if (myDirection == 2)
        {
            transform.Rotate(new Vector3(0, 0, 1), 180);
        }
        if (myDirection == 3)
        {
            transform.Rotate(new Vector3(0, 0, 1), 90);
        }
    }
}
