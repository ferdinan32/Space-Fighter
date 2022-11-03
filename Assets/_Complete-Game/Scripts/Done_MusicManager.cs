using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_MusicManager : MonoBehaviour {
	public static Done_MusicManager instance;
	public Transform bgm,sfx;

	// Use this for initialization
	void Awake () {
		if (instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
            return;
        }        

        DontDestroyOnLoad(this.gameObject);
	}

	public void MainMusic(int onMusic, int offMusic){
		if(bgm.gameObject.activeInHierarchy){
			bgm.GetChild(offMusic).GetComponent<AudioSource>().Pause();
			bgm.GetChild(onMusic).GetComponent<AudioSource>().UnPause();	
		}
	}

	public void SFXPlay(int indexPlay){
		if(sfx.gameObject.activeInHierarchy){
			sfx.GetChild(indexPlay).GetComponent<AudioSource>().Play();
		}
	}
}
