using UnityEngine;
using System.Collections;

namespace Code
{
	using System.Collections.Generic; 
	using UnityEngine.UI;
	
	public class GameManager : MonoBehaviour
	{
		public float levelStartDelay = 2f;						//Time to wait before starting level, in seconds.
		public float turnDelay = 0.1f;							//Delay between each Player turn.
		public int playerFoodPoints = 100;						//Starting value for Player food points.
		public static GameManager instance = null;				//Static instance of GameManager which allows it to be accessed by any other script.
		[HideInInspector] public bool playersTurn = true;		//Boolean to check if it's players turn, hidden in inspector but public.
		
		
		private Text levelText;									//Text to display current level number.
		private GameObject levelImage;							//Image to block out level as levels are being set up, background for levelText.
		private LevelGenerator boardScript;						//Store a reference to our BoardManager which will set up the level.
		private int level = 1;									//Current level number, expressed in game as "Day 1".
	//	private List<Enemy> enemies;							//List of all Enemy units, used to issue them move commands.
		private bool enemiesMoving;								//Boolean to check if enemies are moving.
		private bool doingSetup = true;							//Boolean to check if we're setting up board, prevent Player from moving during setup.
		
		
		
		//Awake is always called before any Start functions
		void Awake()
		{
			//Check if instance already exists
			if (instance == null) instance = this;
			else if (instance != this) Destroy(gameObject);	
			DontDestroyOnLoad(gameObject);
			
			//Assign enemies to a new List of Enemy objects.
		//	enemies = new List<Enemy>();
			
			//Get a component reference to the attached BoardManager script
			boardScript = GetComponent<LevelGenerator>();
			InitGame();
		}
		void OnLevelWasLoaded(int index)
		{
			level++;
			InitGame();
		}
		
		//Initializes the game for each level.
		void InitGame()
		{
			doingSetup = true;	
			//Get a reference to our image LevelImage by finding it by name.
			//levelImage = GameObject.Find("LevelImage");
			//Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
			//levelText = GameObject.Find("LevelText").GetComponent<Text>();
			//Set the text of levelText to the string "Day" and append the current level number.
			//levelText.text = "Day " + level;
			//Set levelImage to active blocking player's view of the game board during setup.
			//levelImage.SetActive(true);
			//Call the HideLevelImage function with a delay in seconds of levelStartDelay.
			//Invoke("HideLevelImage", levelStartDelay);
			//Clear any Enemy objects in our List to prepare for next level.
		//	enemies.Clear();
			
			//Call the SetupScene function of the BoardManager script, pass it current level number.
			//boardScript.SetupScene(level);
			
		}
		
		
		//Hides black image used between levels
		void HideLevelImage()
		{
			levelImage.SetActive(false);
			doingSetup = false;
		}
        
		void Update()
		{
			//Check that playersTurn or enemiesMoving or doingSetup are not currently true.
			if(playersTurn /*|| enemiesMoving*/ || doingSetup)
				
				//If any of these are true, return and do not start MoveEnemies.
				return;
			
			//Start moving enemies.
		//	StartCoroutine (MoveEnemies ());
		}
		
		//Call this to add the passed in Enemy to the List of Enemy objects.
		/*public void AddEnemyToList(Enemy script)
		{
			Add Enemy to List enemies.
			enemies.Add(script);
		}*/
		
		
		//GameOver is called when the player reaches 0 food points
		public void GameOver()
		{
			//Set levelText to display number of levels passed and game over message
			levelText.text = "After " + level + " days, you starved.";
			
			//Enable black background image gameObject.
			levelImage.SetActive(true);
			
			//Disable this GameManager.
			enabled = false;
		}
		
		//Coroutine to move enemies in sequence.
	/*	IEnumerator MoveEnemies()
		{
			//While enemiesMoving is true player is unable to move.
			enemiesMoving = true;
			
			//Wait for turnDelay seconds, defaults to .1 (100 ms).
			yield return new WaitForSeconds(turnDelay);
			
			//If there are no enemies spawned (IE in first level):
			/*if (enemies.Count == 0) 
			{
				//Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
				yield return new WaitForSeconds(turnDelay);
			}
			for (int i = 0; i < enemies.Count; i++)
			{
				enemies[i].MoveEnemy ();
				yield return new WaitForSeconds(enemies[i].moveTime);
			}
			playersTurn = true;
			enemiesMoving = false;
		}*/
	} 
}

