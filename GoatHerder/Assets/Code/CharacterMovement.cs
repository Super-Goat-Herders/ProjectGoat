using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    public float speed = 2f;
    Animator playerAnimator;
	public AudioClip sound;
	public ParticleSystem explo;
	// Use this for initialization
	void Start () {
        playerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        playerAnimator.SetBool("Attack", false);
        if (Input.GetButtonDown("Attack"))
        {
            playerAnimator.SetBool("Attack", true);
			AudioSource.PlayClipAtPoint(sound, transform.position);
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
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    // Camera.current.transform.localRotation = Quaternion.identity;
                }
                else
                {
                    //transform.localRotation = Quaternion.Euler(0, 180, 0);
                    // Camera.current.transform.localRotation = Quaternion.Euler(0, -180, 0);
                }

            }
            else
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    playerAnimator.SetBool("Forward", false);
                    playerAnimator.SetBool("Backward", true);
                    playerAnimator.SetBool("Sideways", false);
                }
                else
                {
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
