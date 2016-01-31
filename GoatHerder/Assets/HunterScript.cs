using UnityEngine;
using System.Collections;

public class HunterScript : MonoBehaviour {
    public Transform target;
    Animator playerAnimator;

	// Use this for initialization
	void Start () {
        playerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //if ((target.position.x > transform.position.x) && (target.position.y > transform.position.y))
        //{
        //    playerAnimator.SetBool("Up", false);
        //    playerAnimator.SetBool("Down", false);
        //    playerAnimator.SetBool("Left", false);
        //    playerAnimator.SetBool("Right", false);
        //}
        //else if ((target.position.x > transform.position.x) && (target.position.y < transform.position.y))
        //{
        //    playerAnimator.SetBool("Up", false);
        //    playerAnimator.SetBool("Down", false);
        //    playerAnimator.SetBool("Left", false);
        //    playerAnimator.SetBool("Right", false);
        //}
        //else if ((target.position.x < transform.position.x) && (target.position.y > transform.position.y))
        //{
        //    playerAnimator.SetBool("Up", false);
        //    playerAnimator.SetBool("Down", false);
        //    playerAnimator.SetBool("Left", false);
        //    playerAnimator.SetBool("Right", false);
        //}
        //else
        //{
        //    playerAnimator.SetBool("Up", false);
        //    playerAnimator.SetBool("Down", false);
        //    playerAnimator.SetBool("Left", false);
        //    playerAnimator.SetBool("Right", false);
        //}
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
        Debug.Log(playerAnimator.GetInteger("Orientation"));
	}
}
