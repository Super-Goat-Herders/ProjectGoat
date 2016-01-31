using UnityEngine;
using System.Collections;

public class ShootAttack : MonoBehaviour {
    Animator playerAnimator;
    public float timer;
    public ArrowScript arrows;
    public float currentCheck;
    public Transform target;

	// Use this for initialization
	void Start () {
        timer = 0;
        currentCheck = ((Random.value * 2) + 3);
        playerAnimator = GetComponent<Animator>();
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
            ArrowScript myArrows = (ArrowScript)Instantiate(arrows, transform.position, Quaternion.identity);
            myArrows.begin(relativePos, playerAnimator.GetInteger("Orientation"));
            timer = 0;
        }
	}
}
