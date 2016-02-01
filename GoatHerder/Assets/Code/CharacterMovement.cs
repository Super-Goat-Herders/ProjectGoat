using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    public float speed = 2f;
    Animator playerAnimator;
	public AudioClip sound;

    public CharacterProjectile arrows;
    public int orientation;
	// Use this for initialization
	void Start () {
        playerAnimator = GetComponent<Animator>();
        orientation = 2;
	}
	
	// Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, -10f);
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        playerAnimator.SetBool("Attack", false);
        if (Input.GetButtonDown("Attack"))
        {
            if (orientation == 0)
            {
                CharacterProjectile myObject = (CharacterProjectile)Instantiate(arrows, transform.position + new Vector3(0,.5f,0), Quaternion.identity);
                myObject.begin(0);
            }
            else if (orientation == 1)
            {
                CharacterProjectile myObject = (CharacterProjectile)Instantiate(arrows, transform.position + new Vector3(.5f, 0, 0), Quaternion.identity);
                myObject.begin(1);
            }
            else if (orientation == 2)
            {
                CharacterProjectile myObject = (CharacterProjectile)Instantiate(arrows, transform.position + new Vector3(0, -.5f, 0), Quaternion.identity);
                myObject.begin(2);
            }
            else
            {
                CharacterProjectile myObject = (CharacterProjectile)Instantiate(arrows, transform.position + new Vector3(-.5f, 0, 0), Quaternion.identity);
                myObject.begin(3);
            }


            playerAnimator.SetBool("Attack", true);

			if (sound != null){
				AudioSource.PlayClipAtPoint(sound, transform.position);
			}
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
                }
                else
                {
                    orientation = 3;
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
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
            playerAnimator.SetBool("Stopped", true);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Door")
        {
            Debug.Log("YOU DID IT!");
        }
    }
}
