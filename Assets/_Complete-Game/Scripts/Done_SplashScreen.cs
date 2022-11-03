using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Done_SplashScreen : MonoBehaviour {
	
	public void GoToMainMenu(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}
}
