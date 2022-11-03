using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
    [System.Serializable]
    public class MainMenu{
        public string nameObject;
        public Transform theObject;
    }

    public static Done_GameController instance;

    public MainMenu[] allMenu;
    public GameObject[] bossBattle;
    public GameObject[] playerFighter;

    public Text[] scoreText;
    // public Text restartText;
    public Text gameOverText;
    public InputField nameHighscore;

    public Image barShield;
    public Image[] barPower;

    public bool isBoss;
    public static bool gameOver;
    public static bool restart;
    public static float healthPlayer = 100;
    private int score;
    public static int indexBar;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        instance = this;
        Instantiate(playerFighter[Done_LevelManager.instance.fighterSelected],Vector3.zero,Quaternion.identity);
    }

    void Start()
    {
        gameOver = false;
        restart = false;
        // restartText.text = "";
        gameOverText.text = Done_GameController.instance.gameOverText.text = "STAGE "+(Done_LevelManager.instance.levelDificulty+1).ToString("D2");
        score += Done_LevelManager.instance.scoreSaved;
        healthPlayer = 100;
        indexBar = 0;
        UpdateScore();        
    }

    public void RestartGame(){
        // if (restart)
        // {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // }
    }    

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText[0].text = score.ToString("D6");
        for (int i = 1; i < scoreText.Length; i++)
        {
            scoreText[i].text = "SCORE "+score.ToString("D6");
        }
    }

    public void GameOver()
    {
        gameOverText.text = "GAME OVER";
        gameOver = true;
        allMenu[3].theObject.localPosition = Vector3.zero;

        // if(isBoss){
        //     StartCoroutine(RestartBoss());
        // }
    }

    public void WinGame(){
        GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>().enabled=false;
        allMenu[0].theObject.localPosition = Vector3.zero;
        gameOverText.text = "YOU WIN";
        gameOver = true;
    }

    public void BackToMenu(){
        Destroy(GameObject.FindGameObjectWithTag("Respawn"));
        Done_LevelManager.instance.scoreSaved = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    // IEnumerator RestartBoss(){
    //     yield return new WaitForSeconds(1);

    //     Done_GameController.instance.restartText.text = "Tap for Restart";
    //     Done_GameController.restart = true;
    // }

    public void ChangeScene(bool isNext){        
        if(isNext && Done_LevelManager.instance.levelDificulty<2){
            if(Time.timeScale==0){
                Time.timeScale=1;
            }

            Done_LevelManager.instance.scoreSaved = score;
            Done_LevelManager.instance.levelDificulty+=1;
            PlayerPrefs.SetInt("level",Done_LevelManager.instance.levelDificulty);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }else{
            for (int i = 0; i < allMenu.Length; i++)
            {
                allMenu[i].theObject.localPosition = new Vector2(1000,0);
            }            
            allMenu[4].theObject.localPosition = Vector3.zero;
        }
    }

    public void PauseGame(bool isPause){
        if(isPause){
            allMenu[1].theObject.localPosition = Vector3.zero;
            Time.timeScale = 0;
        }else{            
            Time.timeScale = 1;
            allMenu[1].theObject.localPosition = new Vector2(1000,0);
        }        
    }

    public void SettingsGame(bool isAcc){
        if(isAcc){
            allMenu[2].theObject.localPosition = new Vector2(1000,0);
            allMenu[1].theObject.localPosition = Vector3.zero;
        }else{
            allMenu[1].theObject.localPosition = new Vector2(1000,0);
            allMenu[2].theObject.localPosition = Vector3.zero;
        }
    }

    public void AccNameHighscore(){
        // var ds = new DataService ("existing.db");
        // ds.CreateHighscore(nameHighscore.text,score);
        if(Time.timeScale==0){
            Time.timeScale=1;
        }
        
        StartCoroutine(Done_LevelManager.instance.InsertData(nameHighscore.text,score));

        Done_LevelManager.instance.scoreSaved = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}