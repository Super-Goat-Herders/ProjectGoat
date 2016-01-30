using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    float speed = 2f;
    Animator playerAnimator;

	// Use this for initialization
	void Start () {
        playerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (playerAnimator.GetBool("Stopped") == true)
            {
                playerAnimator.SetBool("Stopped", false);
            }
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            if (playerAnimator.GetBool("Stopped") == true)
            {
                playerAnimator.SetBool("Stopped", false);
            }

            if (Input.GetAxis("Vertical") > 0)
            {
                playerAnimator.SetBool("Backward", true);
                playerAnimator.SetBool("Forward", false);
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                playerAnimator.SetBool("Forward", true);
                playerAnimator.SetBool("Backward", false);
            }
        }
        else
        {
            playerAnimator.SetBool("Forward", false);
            playerAnimator.SetBool("Backward", false);
            playerAnimator.SetBool("Stopped", true);
        }


        //Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, -10f);
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Door")
        {
            Debug.Log("YOU DID IT!");
        }
    }
}
