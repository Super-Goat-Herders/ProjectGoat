using UnityEngine;

public class LoadLevel : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other) 
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+1);
	}    
    
}