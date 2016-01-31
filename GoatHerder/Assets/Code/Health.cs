using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Health : MonoBehaviour {
	public int startHP = 3;
	public int currentHP = 3;
	public static int maxHealth = 6;
	public GameObject heartFull;
	public ParticleSystem explo;
	public ParticleSystem exploDeath;
	public AudioClip sound;
	public AudioClip soundDead1;
	public AudioClip soundDead2;
    private bool isDead = false;

	private List<Transform> hearts = new List<Transform>();

	private float Sx = 0.35f;
	private float y = 2;
	private float x = -1.7f;

	// Use this for initialization
	void Start () {
		currentHP = startHP;
	}
	void  Reset(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("OpeningScene");
	}
	void Update(){
		//if (Input.GetKeyDown ("n")) {
		//	Heal (1);
		//}
		y = gameObject.transform.position.y + 1;
		x = gameObject.transform.position.x - 1;
	    //if (Input.GetKeyDown ("space")) {
		//	TakeDamage (1);
	    //}
		if (currentHP <= 0 && !isDead) {
            isDead = true;
			Vector3 loc1 = gameObject.transform.position;
			ParticleSystem newExplo = (ParticleSystem)Instantiate(exploDeath, loc1, transform.rotation);
			if (soundDead1 != null || soundDead2 != null) {
				AudioSource.PlayClipAtPoint (soundDead1, transform.position, 0.7f);
				AudioSource.PlayClipAtPoint (soundDead2, transform.position, 0.6f);
			}
			Invoke ("Reset", 2);
		}
		while(hearts.Count != currentHP){
			Vector3 loc2 = new Vector3(x+(hearts.Count*Sx), y);
			Transform newHeart =((GameObject)Instantiate(heartFull, loc2, transform.rotation)).transform;
			newHeart.parent = gameObject.transform;
			hearts.Add (newHeart);

		}
	}

	// Use this to take damage
	public void TakeDamage(int damage){
		AudioSource.PlayClipAtPoint(sound, transform.position);
		if (currentHP > 0) {
			Transform dead = hearts [hearts.Count - 1];
			Vector3 loc3 = new Vector3(x+((hearts.Count-1)*Sx), y);
			ParticleSystem newExplo = (ParticleSystem)Instantiate(explo, loc3, transform.rotation);
			hearts.Remove (dead);
			Destroy (dead.gameObject);
			currentHP -= damage;
		}

	}
	public void Heal(int hp){
		if (currentHP < maxHealth) {
			currentHP += hp;
		}
	}
}
 
