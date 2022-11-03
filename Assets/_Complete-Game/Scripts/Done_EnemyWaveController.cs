using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_EnemyWaveController : MonoBehaviour {
    [System.Serializable]
    public class EnemySpawn{
        public string levelName;
        public GameObject[] enemies;
    }
	
    public EnemySpawn[] hazards;
    public Vector3 spawnValues;
    public int waveCount;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    int waveStage;

	// Use this for initialization
	void Start () {
        waveCount += (Done_LevelManager.instance.levelDificulty*2);
        hazardCount += (Done_LevelManager.instance.levelDificulty*2);
		StartCoroutine(SpawnWaves());
	}

	IEnumerator SpawnWaves()
    {             
        yield return new WaitForSeconds(startWait);
        Done_GameController.instance.gameOverText.text = "";

        while (true)
        {       
            if(waveStage==waveCount){
                Done_GameController.instance.isBoss = true;
                Instantiate(Done_GameController.instance.bossBattle[Done_LevelManager.instance.levelDificulty], Done_GameController.instance.bossBattle[Done_LevelManager.instance.levelDificulty].transform.position, Quaternion.identity);
                break;
            }       

            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Done_LevelManager.instance.levelDificulty].enemies[Random.Range(0, hazards[Done_LevelManager.instance.levelDificulty].enemies.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            waveStage+=1;

            if (Done_GameController.gameOver)
            {
                // Done_GameController.instance.restartText.text = "Tap for Restart";
                // Done_GameController.restart = true;
                break;
            }
        }
    }
}
