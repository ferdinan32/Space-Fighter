using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_ScrollFighter : MonoBehaviour {
	bool isPlayAudio = false;

	private void Update() {
		if(Done_MusicManager.instance.sfx.gameObject.activeInHierarchy){
			if(transform.localPosition.y<10 && transform.localPosition.y>-10){
				Done_LevelManager.instance.fighterSelected = int.Parse(gameObject.name);
				if(!isPlayAudio){
					Done_MusicManager.instance.sfx.GetChild(5).GetComponent<AudioSource>().PlayOneShot(Done_MusicManager.instance.sfx.GetChild(5).GetComponent<AudioSource>().clip);
					isPlayAudio=true;
				}			
				return;
			}

			if(!Done_MusicManager.instance.sfx.GetChild(5).GetComponent<AudioSource>().isPlaying){
				isPlayAudio = false;
			}
		}		
	}
}
