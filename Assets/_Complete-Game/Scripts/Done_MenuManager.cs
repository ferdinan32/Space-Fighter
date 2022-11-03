using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Done_MenuManager : MonoBehaviour {
	public Transform lockStage;
	public Transform prefabHighscore;
	public Transform contentHighscore;
	public Transform[] menuList;
	public GameObject[] tutorialList;
	public GameObject swipeContoller;
	public GameObject loadingObject;
	public GameObject loadingToGame;
	public Animator animasiSwipeFighter;
	public Image fighterImage,stageImage,stageText;
	public Sprite[] fightersImage,stagesImage,stagesText;
	public Button selectButton;
	public Toggle bgmSelected, sfxSelected;
	bool isStage;
	bool isTutorial;
	int charValue;
	int stageValue;	
	int nextList;

	void Start()
	{
		StartCoroutine(Done_LevelManager.instance.GetData());

		Done_LevelManager.instance.levelDificulty = 0;
        Done_LevelManager.instance.fighterSelected = 0;
		isTutorial = false;

		loadingToGame.SetActive(false);

		if(PlayerPrefs.GetInt("frist")!=0){
			bgmSelected.isOn = PlayerPrefs.GetInt("bgm") != 0;
			sfxSelected.isOn = PlayerPrefs.GetInt("sfx") != 0;

			Done_MusicManager.instance.bgm.gameObject.SetActive(bgmSelected.isOn);
			Done_MusicManager.instance.sfx.gameObject.SetActive(sfxSelected.isOn);
		}		

		if(bgmSelected.isOn){
			Done_MusicManager.instance.MainMusic(0,1);
		}

		StartCoroutine(SpawnHighscore());
	}
	
	public void BeginGame(){
		isTutorial = true;		
	}

	public void EnterTheGame(){
		loadingToGame.SetActive(true);

		if(Done_MusicManager.instance.bgm.gameObject.activeInHierarchy){
            Done_MusicManager.instance.MainMusic(1,0);
        }
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}

	public void ExitGame(){
		Application.Quit();
	}

	private void Update() {
		if(Input.GetKeyUp(KeyCode.Escape)){
			MenuSelect(menuList[9]);
		}
	}

	public void MenuSelect(Transform selectedMenu){
		PlayerPrefs.SetInt("frist",1);

		for (int i = 0; i < menuList.Length; i++)
		{
			menuList[i].localPosition = new Vector2(1000,1000);
		}

		selectedMenu.localPosition = Vector2.zero;		

		if(selectedMenu.name=="SelectStage" || selectedMenu.name=="FighterSelect"){
			swipeContoller.SetActive(true);

			if(selectedMenu.name=="SelectStage"){
				isStage=true;
			}else{
				isStage=false;
			}
		}else{
			swipeContoller.SetActive(false);
			Done_LevelManager.instance.levelDificulty = stageValue = 0;
        	Done_LevelManager.instance.fighterSelected = charValue = 0;

			fighterImage.sprite = fightersImage[charValue];
			stageImage.sprite = stagesImage[stageValue];
			stageText.sprite = stagesText[stageValue];
		}
	}

	public void SelectMethodSwipe(bool isSwipeUp){
		if(isSwipeUp){
			if(isStage){
				stageValue+=1;
				if(stageValue>2){
					stageValue=0;
				}
			}else{
				charValue+=1;
				if(charValue>2){
					charValue=0;
				}
				animasiSwipeFighter.SetTrigger("swipe");
				animasiSwipeFighter.SetInteger("charValue",charValue);
				animasiSwipeFighter.SetBool("isSwipeUp",isSwipeUp);				
			}
			
		}else{
			if(isStage){
				stageValue-=1;
				if(stageValue<0){
					stageValue=2;
				}
			}else{
				charValue-=1;
				if(charValue<0){
					charValue=2;
				}
				animasiSwipeFighter.SetTrigger("swipe");
				animasiSwipeFighter.SetInteger("charValue",charValue);
				animasiSwipeFighter.SetBool("isSwipeUp",isSwipeUp);				
			}
		}

		if(stageValue>PlayerPrefs.GetInt("level")){
			lockStage.localPosition = Vector2.zero;
			selectButton.interactable = false;
		}else{			
			lockStage.localPosition = new Vector2(1000,1000);
			selectButton.interactable = true;
		}

		// fighterImage.sprite = fightersImage[charValue];
		stageImage.sprite = stagesImage[stageValue];
		stageText.sprite = stagesText[stageValue];

		Done_LevelManager.instance.fighterSelected = charValue;
		Done_LevelManager.instance.levelDificulty = stageValue;
	}	

	public void MusicAccepter(){
		Done_MusicManager.instance.bgm.gameObject.SetActive(bgmSelected.isOn);
		Done_MusicManager.instance.sfx.gameObject.SetActive(sfxSelected.isOn);

		if(bgmSelected.isOn){
			Done_MusicManager.instance.MainMusic(0,1);
		}

		PlayerPrefs.SetInt("bgm", bgmSelected.isOn ? 1:0);
		PlayerPrefs.SetInt("sfx", sfxSelected.isOn ? 1:0);
	}

	public void SfxButton(bool isBack){
		if(isBack){
			Done_MusicManager.instance.SFXPlay(1);
		}else{
			Done_MusicManager.instance.SFXPlay(0);
		}
	}

	IEnumerator SpawnHighscore(){
		loadingObject.SetActive(true);

		yield return new WaitUntil(() =>Done_LevelManager.instance.isServerResponse);

		loadingObject.SetActive(false);

		int tableLength;

		if(Done_LevelManager.instance.datas.data.Length<10){
			tableLength = Done_LevelManager.instance.datas.data.Length;
		}else{
			tableLength = 10;
		}

		for (int i = 0; i < tableLength; i++)
		{
			Transform goHighscore = Instantiate(prefabHighscore);
			goHighscore.SetParent(contentHighscore, false);				
			goHighscore.GetComponent<Text>().text = Done_LevelManager.instance.datas.data[i].Name+"   "+Done_LevelManager.instance.datas.data[i].Score.ToString("D6");
		}
	}

	public void NextTutorial(bool isNext){
		if(isNext){			
			if(nextList<tutorialList.Length-1){
				nextList++;
			}else{
				SkipTutorial();
			}
		}else{			
			if(nextList>0){
				nextList--;
			}else{
				isTutorial = false;
				MenuSelect(menuList[1]);
			}
		}

		for (int i = 0; i < tutorialList.Length; i++)
		{
			tutorialList[i].SetActive(false);
		}

		tutorialList[nextList].SetActive(true);
	}

	public void SkipTutorial(){		
		if(isTutorial){
			EnterTheGame();
		}else{
			nextList=-1;
			NextTutorial(true);
			MenuSelect(menuList[1]);
		}
	}
}
