using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Health : MonoBehaviour {
	public int startHP = 3;
	public int currentHP = 3;
	public static int maxHealth = 6;
	public GameObject heartFull;
	public ParticleSystem explo;
	public AudioClip sound;
	public AudioClip soundDead1;
	public AudioClip soundDead2;

	private List<Transform> hearts = new List<Transform>();

	private float Sx = 0.35f;
	private float y = 2;
	private float x = -1.7f;

	// Use this for initialization
	void Start () {
		currentHP = startHP;
	}
	void  Reset(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}
	void Update(){
		//if (Input.GetKeyDown ("n")) {
		//	Heal (1);
		//}
	    if (Input.GetKeyDown ("space")) {
			TakeDamage (1);
	    }
		if (currentHP <= 0) {
			AudioSource.PlayClipAtPoint(soundDead1, transform.position, 0.7f);
			AudioSource.PlayClipAtPoint(soundDead2, transform.position, 0.6f);
			Vector3 loc = gameObject.transform.position;
			ParticleSystem newExplo = (ParticleSystem)Instantiate(explo, loc, transform.rotation);
			Invoke ("Reset", 2);

		}
		while(hearts.Count != currentHP){
			Vector3 loc = new Vector3(x+(hearts.Count*Sx), y);
			Transform newHeart =((GameObject)Instantiate(heartFull, loc, transform.rotation)).transform;
			newHeart.parent = this.transform.parent;
			hearts.Add (newHeart);

		}
	}

	// Use this to take damage
	public void TakeDamage(int damage){
		AudioSource.PlayClipAtPoint(sound, transform.position);
		if (currentHP > 0) {
			Transform dead = hearts [hearts.Count - 1];
			Vector3 loc = new Vector3(x+((hearts.Count-1)*Sx), y);
			ParticleSystem newExplo = (ParticleSystem)Instantiate(explo, loc, transform.rotation);
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
 
