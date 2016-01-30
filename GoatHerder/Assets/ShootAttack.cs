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
        currentCheck = ((Random.value * 2) + 5);
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        Debug.Log(timer);
        transform.LookAt(target);
        if (timer > currentCheck)
        {
            Object MyArrow = Instantiate(arrows, transform.position, transform.rotation);
        }
	}
}
