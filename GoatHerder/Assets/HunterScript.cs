﻿using UnityEngine;
using System.Collections;

public class HunterScript : MonoBehaviour {
    private Transform target;
    Animator playerAnimator;
    public int HP;

	// Use this for initialization
	void Start () {
        playerAnimator = GetComponent<Animator>();
        HP = 10;
	    target = GameObject.FindWithTag("Hero").transform;

	}
	
	// Update is called once per frame
	void Update () {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        Vector3 targetRel = target.position - transform.position;
        if (Mathf.Abs(targetRel.x) > Mathf.Abs(targetRel.y))
        {
            if (targetRel.x >= 0)
            {
                playerAnimator.SetInteger("Orientation", 1);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                playerAnimator.SetInteger("Orientation", 3);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else
        {
            if (targetRel.y >= 0)
            {
                playerAnimator.SetInteger("Orientation", 0);
            }
            else
            {
                playerAnimator.SetInteger("Orientation", 2);
            }
        }
        //Debug.Log(playerAnimator.GetInteger("Orientation"));
	}

    public void TakeDamage(int damage)
    {
        if (HP > 0)
        {
            HP -= damage;
        }

    }
}
