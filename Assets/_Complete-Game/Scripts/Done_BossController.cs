using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_BossController : MonoBehaviour {

	[System.Serializable]
	public class BossShootMangaer{
		public GameObject shot;
		public Transform[] shotSpawn;
		public float fireRate;
		public float delay;
	}

	public float rotationWeapon;
	public int shotLength;
	public BossShootMangaer[] shotManagers;

	void Start ()
	{
		for (int i = 0; i < shotLength; i++)
		{
			InvokeRepeating ("Fire", shotManagers[i].delay, shotManagers[i].fireRate);	
		}

		shotManagers[0].shotSpawn[0].GetComponentInParent<Rigidbody>().angularVelocity = Random.insideUnitSphere * rotationWeapon;		
	}

	void Fire ()
	{
		for (int j = 0; j < shotLength; j++)
		{		
			for (int i = 0; i < shotManagers[j].shotSpawn.Length; i++)
			{
				Instantiate(shotManagers[j].shot, shotManagers[j].shotSpawn[i].position, shotManagers[j].shotSpawn[i].rotation);	
			}		
			GetComponent<AudioSource>().Play();
		}
	}
}
