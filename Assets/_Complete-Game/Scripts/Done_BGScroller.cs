using UnityEngine;
using System.Collections;

public class Done_BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeZ;
	public Material[] bgImage;
	public bool isParent;

	private Vector3 startPosition;

	void Start ()
	{		
		this.GetComponent<Renderer>().material = bgImage[Done_LevelManager.instance.levelDificulty];

		if(isParent){
			startPosition = transform.position;
		}
	}

	void Update ()
	{
		if(isParent){
			float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
			transform.position = startPosition + Vector3.forward * newPosition;
		}else{
			return;
		}	
	}
}