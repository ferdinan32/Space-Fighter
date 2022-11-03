using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public GameObject[] dropItem;
	public int scoreValue;
	public float damageBullet;
	public bool isLaser;
	public bool isPlayer;
	public bool isLongLaser;

	float bossHealt = 100;
	
	void Start()
	{
		bossHealt = 100;
	}

	void OnTriggerEnter (Collider other)
	{
		LaserExplode(other);
		
		if(isPlayer){
			if (other.tag == "Enemy")
			{				
				DestroyObject();
			}			
		}
		else{
			if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Finish" || other.tag == "Laser" && isLaser)
			{
				return;
			}			

			if (other.tag == "Player")
			{
				if(Done_GameController.healthPlayer>0){
					Done_GameController.healthPlayer -= damageBullet;
					Done_GameController.instance.barShield.fillAmount = Done_GameController.healthPlayer/100;
				}else{
					GameOverEffect(other);					
				}				
			}
			
			if(Done_GameController.instance.isBoss){				
				// Destroy (other.gameObject);
				if(bossHealt<0){
					ExplodeEffect(false);

					Done_GameController.instance.WinGame();
					DestroyObject();
				}else{
					if(other.tag != "Laser" && !isLaser){
						GameOverEffect(other);
					}else{
						Done_GameController.instance.AddScore(scoreValue);
						bossHealt-=damageBullet;
					}
				}

				if(isLaser){
					DestroyObject();
				}
			}
			else{
				ExplodeEffect(Random.Range(0,2)==0);

				Done_GameController.instance.AddScore(scoreValue);
				// Destroy (other.gameObject);
				DestroyObject();
			}
		}		
	}

	void ExplodeEffect(bool isDrop){
		if (explosion != null && !isLaser)
		{
			Instantiate(explosion, transform.position, transform.rotation);
			if(isDrop){
				if(Random.value<=0.3){
					Instantiate(dropItem[1], transform.position, transform.rotation);
				}
				else if(Random.value<=0.7){
					Instantiate(dropItem[0], transform.position, transform.rotation);
				}				
			}			
		}
	}

	void LaserExplode(Collider other){
		if(isLaser){			
			if(!isPlayer && other.tag == "Player" || isPlayer && other.tag == "Enemy"){
				Instantiate(explosion, transform.position, Quaternion.identity);
			}
		}
	}

	void GameOverEffect(Collider targetPrefab){
		Instantiate(playerExplosion, targetPrefab.transform.position, targetPrefab.transform.rotation);
		Done_GameController.instance.GameOver();
		Destroy (targetPrefab.gameObject);
	}

	public void DestroyObject(){
		if(!isLongLaser){
			Destroy(gameObject);
		}
	}
}