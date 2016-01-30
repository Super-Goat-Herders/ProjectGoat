using UnityEngine;
using System.Collections.Generic;

public class Health : MonoBehaviour {
	public int startHP = 3;
	public int currentHP;
	public static int maxHealth = 6;
	public GameObject heartFull;
	public ParticleSystem explo;

	private List<Transform> hearts = new List<Transform>();

	private float Sx = 0.35f;
	private float y = 2;
	private float x = -1.7f;

	// Use this for initialization
	void Start () {
		currentHP = startHP;
	}

	void Update(){
		if (Input.GetKeyDown ("n")) {
			Heal (1);
		}
		if (Input.GetKeyDown ("space")) {
			TakeDamage (1);
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
 
