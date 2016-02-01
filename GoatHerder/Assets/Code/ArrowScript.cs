using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
    public float speed;
    public float timer;
    Vector3 myDirection;

	// Use this for initialization
	void Start () {
        speed = 5f;
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        transform.position += myDirection * speed * Time.deltaTime;
        if (timer > 8)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("We got here to collision");
        if (coll.gameObject.tag == "Hero")
        {
            coll.gameObject.SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        } 
        //else {
         //   speed = 0; 
            //Destroy(gameObject);
        //}
        
        if (coll.gameObject.tag == "Wall")
        {
            Debug.Log("We got here");
            speed = 0;
            //If you want arrows to stick to the wall, limit the number of them and disable it's ability to cause damage
            Destroy(gameObject);
        }
         
        //timer += Time.deltaTime;
    }

    public void begin(Vector3 direction, int orientation)
    {
        myDirection = direction.normalized;
        float thisrotation = Mathf.Atan2(direction.y, direction.x) - 90;
        thisrotation = thisrotation * 180 / Mathf.PI;
        Debug.Log(thisrotation);
        transform.rotation *= Quaternion.Euler(0f, 0f, thisrotation);
        //transform.Rotate(new Vector3(0,0,1), thisrotation);
    }
}

