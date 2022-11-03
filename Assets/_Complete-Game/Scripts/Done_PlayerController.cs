using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, 
	xMax, 
	zMin, 
	zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform[] shotSpawn;
	public float fireRate;
	 
	private float nextFire;
	// deltaZ;
	// private Vector3 mOffset;

	// Vector3 GetMouseWorldPos(){
	// 	Vector3 mousePoint = Input.mousePosition;
	// 	mousePoint.z = deltaZ;
	// 	return Camera.main.ScreenToWorldPoint(mousePoint);
	// }

	void FixedUpdate ()
	{
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);

		if(Input.GetMouseButton(0)){
			FireControll();
			// GetComponent<Done_LaserController>().LaserShoot();
		}
	}

	void FireControll(){
		if (Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			for (int i = 0; i < shotSpawn.Length; i++)
			{
				if(shotSpawn[i].gameObject.activeSelf){
					Instantiate(shot, shotSpawn[i].position, shotSpawn[i].rotation);	
				}				
			}			
			GetComponent<AudioSource>().Play ();
		}
	}

	// void OnMouseDown(){
	// 	deltaZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
	// 	// mOffset = gameObject.transform.position - GetMouseWorldPos();		
	// }

	// private void OnMouseDrag() {
	// 	// GetComponent<Rigidbody>().velocity = GetMouseWorldPos() * speed;//+ mOffset * speed;		
	// 	if (Time.time > nextFire) 
	// 	{
	// 		nextFire = Time.time + fireRate;
	// 		for (int i = 0; i < shotSpawn.Length; i++)
	// 		{
	// 			if(shotSpawn[i].gameObject.activeSelf){
	// 				Instantiate(shot, shotSpawn[i].position, shotSpawn[i].rotation);	
	// 			}				
	// 		}			
	// 		GetComponent<AudioSource>().Play ();
	// 	}
	// }

	// private void OnMouseUp() {
	// 	GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
	// }
}
