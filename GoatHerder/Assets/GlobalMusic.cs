using UnityEngine;
using System.Collections;

public class GlobalMusic : MonoBehaviour {
	public AudioClip openingmusic;
	public AudioClip mainmusic;
	AudioSource audio;

	// Update is called once per frame
	void Update () {
		audio = GetComponent<AudioSource>();
		DontDestroyOnLoad(this.gameObject);
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0) {
			audio.clip = mainmusic;
		} else {
			audio.clip = openingmusic;
		}
	}

	void Start()
	{
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0) {
			audio.clip = mainmusic;
		} else {
			audio.clip = openingmusic;
		}
		audio.Play(); 
	}
}
