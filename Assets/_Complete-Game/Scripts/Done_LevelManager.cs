using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Done_LevelManager : MonoBehaviour {

	public static Done_LevelManager instance;
	public int levelDificulty;
    public int fighterSelected;
    public int scoreSaved;
    public bool isServerResponse;
    public HighScoreData datas;

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

    public IEnumerator GetData() {
		WWW www = new WWW(TargetApi("SelectTabel")); //GET data is sent via the URL

		while(!www.isDone && string.IsNullOrEmpty(www.error)) {
            isServerResponse = false;
			yield return null;
		}

		if(string.IsNullOrEmpty(www.error)) {
            datas = JsonUtility.FromJson<HighScoreData>("{\"data\":" + www.text + "}");
            isServerResponse = true;            
		}
		else{ 
			StartCoroutine(GetData());
		}
	}

    public IEnumerator InsertData(string _name, int _score) {
        WWWForm form = new WWWForm();
        form.AddField("valName", _name);
        form.AddField("valScore", _score);
        WWW www = new WWW(TargetApi("InsertTable"), form);

		while(!www.isDone && string.IsNullOrEmpty(www.error)) {        
			yield return null;
		}
	}

    public string TargetApi(string urlApi){        
        string fullUrl = "https://ferdinan32.000webhostapp.com/Database_AirFightersStrike/"+urlApi+".php";
        return fullUrl;
    }
}

[System.Serializable]
public class HighScoreData{
    public HighScoreObject[] data;
}

[System.Serializable]
public class HighScoreObject{
    public int Id;
    public string Name;
    public int Score;
}
