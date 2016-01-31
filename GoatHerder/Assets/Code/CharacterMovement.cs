using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    public float speed = 2f;
    Animator playerAnimator;
	public AudioClip sound;

    public Object arrows;
    public int orientation;
	// Use this for initialization
	void Start () {
        playerAnimator = GetComponent<Animator>();
        orientation = 2;
	}
	
	// Update is called once per frame
    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        playerAnimator.SetBool("Attack", false);
        if (Input.GetButtonDown("Attack"))
        {
            Quaternion rotation;
            //Quaternion rotation = Quaternion.LookRotation(transform.forward);
            if (orientation == 0)
            {
                rotation = Quaternion.LookRotation(Vector3.up);
            }
            if (orientation == 1)
            {
                rotation = Quaternion.LookRotation(Vector3.right);
            }
            if (orientation == 2)
            {
                rotation = Quaternion.LookRotation(Vector3.down);
            }
            else
            {
                rotation = Quaternion.LookRotation(Vector3.left);
            }


            playerAnimator.SetBool("Attack", true);

			if (sound != null){
				AudioSource.PlayClipAtPoint(sound, transform.position);
			}
			Instantiate (arrows, transform.position, rotation);
        }
        transform.position += move * speed * Time.deltaTime;
        if ((Mathf.Abs(Input.GetAxis("Horizontal")) != 0) || (Mathf.Abs(Input.GetAxis("Vertical")) != 0))
        {
            if (playerAnimator.GetBool("Stopped") == true)
            {
                playerAnimator.SetBool("Stopped", false);
            }
            if ((Mathf.Abs(Input.GetAxis("Horizontal"))) >= (Mathf.Abs(Input.GetAxis("Vertical"))))
            {
                playerAnimator.SetBool("Forward", false);
                playerAnimator.SetBool("Backward", false);
                playerAnimator.SetBool("Sideways", true);
                if (Input.GetAxis("Horizontal") > 0)
                {
                    orientation = 1;
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    // Camera.current.transform.localRotation = Quaternion.identity;
                }
                else
                {
                    orientation = 3;
                    //transform.localRotation = Quaternion.Euler(0, 180, 0);
                    // Camera.current.transform.localRotation = Quaternion.Euler(0, -180, 0);
                }

            }
            else
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    orientation = 0;
                    playerAnimator.SetBool("Forward", false);
                    playerAnimator.SetBool("Backward", true);
                    playerAnimator.SetBool("Sideways", false);
                }
                else
                {
                    orientation = 2;
                    playerAnimator.SetBool("Forward", true);
                    playerAnimator.SetBool("Backward", false);
                    playerAnimator.SetBool("Sideways", false);
                }
            }
        }
        else
        {

            //playerAnimator.SetBool("Forward", false);
            //playerAnimator.SetBool("Backward", false);
            playerAnimator.SetBool("Stopped", true);
            //playerAnimator.SetBool("Sideways", false);
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
