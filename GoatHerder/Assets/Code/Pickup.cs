using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	public GameObject player = GameObject.Find("MainCharacter");
	public CharacterMovement scriptMove;
	public Health scriptHP;
	public UnityEngine.UI.Text textbox;
	public AudioClip sound;


	void Start(){
	}
	void OnTriggerEnter2D(Collider2D other) 
	{
		scriptMove = player.GetComponent<CharacterMovement> ();
		scriptHP = player.GetComponent<Health> ();
		if (other.tag == "Hero"){
			Destroy(gameObject);
			float rekt = Random.value;
			if (rekt <= 0.2) {
				scriptMove.speed = 2.5f;
				textbox.text = "Faster Speed! :)";
			}
			else if (rekt > 0.2 && rekt <= 0.3){
				scriptMove.speed = 1.5f;
				textbox.text = "Slower Speed! :(";
			}
			else if (rekt > 0.3 && rekt <= 0.5){
				scriptHP.Heal(1);
				textbox.text = "+1 HP!! :D";
			}
			else if(rekt > 0.5 && rekt <= 0.6){
				scriptHP.TakeDamage(1);
				textbox.text = "-1 HP!";
			}
			AudioSource.PlayClipAtPoint(sound, transform.position);
		}
	}
}
