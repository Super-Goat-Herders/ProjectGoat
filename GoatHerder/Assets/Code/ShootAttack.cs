using UnityEngine;
using System.Collections;

public class ShootAttack : MonoBehaviour {
    public float timer;
    public Object arrows;
    public float currentCheck;
    public Transform target;

	// Use this for initialization
	void Start () {
        timer = 0;
        currentCheck = ((Random.value * 2) + 3);
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
            Object MyArrow = Instantiate(arrows, transform.position, rotation);
            timer = 0;
        }
	}
}
