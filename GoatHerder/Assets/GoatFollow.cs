using UnityEngine;
using System.Collections;

public class GoatFollow : MonoBehaviour
{
    public Transform target;
    Animator playerAnimator;
    Vector3 lastPostion;
    public Component[] hitboxes;
    public int myOrientation;

    // Use this for initialization
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        lastPostion = transform.position;
        hitboxes = GetComponentsInChildren<BoxCollider2D>();
        target = GameObject.FindWithTag("Hero").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //playerAnimator.SetBool("Attack", false);
        if (lastPostion == transform.position)
        {
            playerAnimator.SetBool("Moving", false);
        }
        else
        {
            playerAnimator.SetBool("Moving", true);
        }
        Vector3 targetRel = target.position - transform.position;
        if (Mathf.Abs(targetRel.x) > Mathf.Abs(targetRel.y))
        {
            if (targetRel.x >= 0)
            {
                myOrientation = 1;
                playerAnimator.SetInteger("Orientation", 1);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                myOrientation = 3;
                playerAnimator.SetInteger("Orientation", 3);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else
        {
            if (targetRel.y >= 0)
            {
                myOrientation = 0;
                playerAnimator.SetInteger("Orientation", 0);
            }
            else
            {
                myOrientation = 2;
                playerAnimator.SetInteger("Orientation", 2);
            }
        }


    }
}