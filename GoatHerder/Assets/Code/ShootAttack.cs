using UnityEngine;
using System.Collections;

public class ShootAttack : MonoBehaviour {
    public float timer;
    public GameObject arrows;
    public float currentCheck;
    public Transform target;

	// Use this for initialization
	void Start () {
        timer = 0;
        currentCheck = ((Random.value * 2) + 3);
        //currentCheck = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        //Debug.Log(timer);
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        //transform.rotation = rotation;
        if (timer > currentCheck)
        {
            Instantiate(arrows, transform.position, rotation);
            timer = 0;
        }
	}
}
