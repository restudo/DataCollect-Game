// This script is a Manager that controls the the flow and control of the game. It keeps
// track of player data (orb count, death count, total game time) and interfaces with
// the UI Manager. All game commands are issued through the static methods of this class

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	//This class holds a static reference to itself to ensure that there will only be
	//one in existence. This is often referred to as a "singleton" design pattern. Other
	//scripts access this one through its public static methods
	static GameManager current;

	public float deathSequenceDuration = 0.8f;	//How long player death takes before restarting
	
	SceneFader sceneFader;						//The scene fader
	bool isGameOver;		
				//Is the game currently over?


	void Awake()
	{
		//If a Game Manager exists and this isn't it...
		if (current != null && current != this)
		{
			//...destroy this and exit. There can only be one Game Manager
			Destroy(gameObject);
			return;
		}

		//Set this as the current game manager
		current = this;

		//Persis this object between scene reloads
		DontDestroyOnLoad(gameObject);
	}

	void Update()
	{
		//If the game is over, exit
		if (isGameOver)
			return;
	}

	public static bool IsGameOver()
	{
		//If there is no current Game Manager, return false
		if (current == null)
			return false;

		//Return the state of the game
		return current.isGameOver;
	}

	public static void RegisterSceneFader(SceneFader fader)
	{
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//Record the scene fader reference
		current.sceneFader = fader;
	}

	public static void PlayerDied()
	{
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//If we have a scene fader, tell it to fade the scene out
		if(current.sceneFader != null)
			current.sceneFader.FadeSceneOut();

		//Invoke the RestartScene() method after a delay
		current.Invoke("RestartScene", current.deathSequenceDuration);
	}

	void RestartScene()
	{
		//Reload the current scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
	}
}
