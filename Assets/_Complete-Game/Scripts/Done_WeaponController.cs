using UnityEngine;
using System.Collections;

public class Done_WeaponController : MonoBehaviour
{
	public GameObject shot;
	public Transform[] shotSpawn;
	public float fireRate;
	public float delay;
	public bool isParentBoss;

	void Start ()
	{
		InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire ()
	{
		for (int i = 0; i < shotSpawn.Length; i++)
		{
			if(isParentBoss){
				GameObject laser = Instantiate(shot, shotSpawn[i].position, shotSpawn[i].rotation);
				laser.transform.SetParent(transform);
			}else{
				Instantiate(shot, shotSpawn[i].position, shotSpawn[i].rotation);
			}			
		}		
		GetComponent<AudioSource>().Play();
	}
}
