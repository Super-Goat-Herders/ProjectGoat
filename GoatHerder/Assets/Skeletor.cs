using UnityEngine;
using System.Collections;

public class Skeletor : MonoBehaviour {
    public Transform target;
    Animator playerAnimator;
    Vector3 lastPostion;
    public BoxCollider2D[] hitboxes;
    public Transform[] smackBoxes;
    public int myOrientation;
    public float timer;

    public int HP;

	// Use this for initialization
	void Start () {
        playerAnimator = GetComponent<Animator>();
        lastPostion = transform.position;
        hitboxes = GetComponentsInChildren<BoxCollider2D>();
        smackBoxes = GetComponentsInChildren<Transform>();
        timer = 0;
        HP = 50;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        playerAnimator.SetBool("Attack", false);
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
        timer += Time.deltaTime;
        if (timer > 1)
        {
            if ((Mathf.Sqrt(Mathf.Pow(transform.position.x - target.position.x, 2) + Mathf.Pow(transform.position.y - target.position.y, 2))) < 2)
            {
                playerAnimator.SetBool("Attack", true);
                
                //hitboxes[myOrientation].enabled = true;
                if ((Mathf.Sqrt(Mathf.Pow(smackBoxes[myOrientation].position.x - target.position.x, 2) + Mathf.Pow(smackBoxes[myOrientation].position.y - target.position.y, 2))) < .6)
                {
                        target.SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);
                        timer = 0;
                        
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (HP > 0)
        {
            HP -= damage;
        }

    }
}
