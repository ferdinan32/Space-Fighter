using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_DropItemController : MonoBehaviour {

	public bool isHeal;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy")
		{
			return;
		}

		if (other.tag == "Player")
		{			
			if(isHeal){
				if(Done_GameController.healthPlayer<100){
					Done_GameController.healthPlayer += 15;
					Done_GameController.instance.barShield.fillAmount = Done_GameController.healthPlayer/100;
				}
			}else{
				Done_PlayerController fireRateControl = other.GetComponent<Done_PlayerController>();

				if(fireRateControl.fireRate>0.2f){
					fireRateControl.fireRate-=0.06f;
					Done_GameController.instance.barPower[Done_GameController.indexBar].fillAmount += 0.2f;
				}
				else{
					if(!fireRateControl.shotSpawn[1].gameObject.activeSelf){
						fireRateControl.fireRate = 0.5f;
						Done_GameController.indexBar+=1;
						for (int i = 0; i < fireRateControl.shotSpawn.Length; i++)
						{
							fireRateControl.shotSpawn[i].gameObject.SetActive(true);
						}				
						fireRateControl.shotSpawn[0].gameObject.SetActive(false);
					}else if(!fireRateControl.shotSpawn[0].gameObject.activeSelf)
					{
						fireRateControl.fireRate = 0.5f;
						Done_GameController.indexBar+=1;
						for (int i = 0; i < fireRateControl.shotSpawn.Length; i++)
						{
							fireRateControl.shotSpawn[i].gameObject.SetActive(true);
						}
					}
				}
			}
			
			Destroy (gameObject);
		}
	}
}
