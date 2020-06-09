using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        PLAYING,PAUSE, QUIT, VICTORY
    }
    GameState currentGameState;
    public PanelManager myPanelManager;
    public UIManager myUIManager;
    public SceneController mySceneController;
    float seconds;
    int minutes;
    int hours;
    int bestHours;
    int bestMinutes;
    float bestSeconds;
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }
    // Update is called once per frame
    void Update()
    {
        switch(currentGameState)
        {
            case GameState.PLAYING:
                if(Input.GetKeyDown(KeyCode.Escape))
                    ChangeState(GameState.PAUSE);
                seconds+=Time.deltaTime;
                if(seconds>=60)
                {
                    seconds = 0;
                    minutes++;
                    if(minutes>=60)
                    {
                        minutes = 0;
                        hours++;
                    }
                }
                myUIManager.SetCurrentTime(seconds,minutes,hours);
                break;
            case GameState.PAUSE:
                if(Input.GetKeyDown(KeyCode.Escape))
                    ChangeState(GameState.PLAYING);
                break;
            case GameState.QUIT:
                break;       
        }       
    }
    void ChangeState(GameState nextState)
    {
        switch(currentGameState)
        {
            case GameState.PLAYING:
                break;
            case GameState.PAUSE:
                myUIManager.ShowPauseCanvas(false);
                break;
            case GameState.QUIT:                
                break;  
            case GameState.VICTORY:
                myUIManager.ShowVictoryCanvas(false);
                myPanelManager.ClearAllNumbers();
                break;
        }
        switch(nextState)
        {
            case GameState.PLAYING:
                break;
            case GameState.PAUSE:
                myUIManager.ShowPauseCanvas(true);
                break;
            case GameState.QUIT:
                break;
            case GameState.VICTORY:
                myUIManager.ShowVictoryCanvas(true);
                myUIManager.SetRecords();                
                break;
        }
        currentGameState = nextState;
    }
    public void SaveGame()
    {
        SaveSystem.SaveGame(myPanelManager,this);
    }
    void SaveRecord()
    {
        PlayerPrefs.SetInt("BestHours", bestHours);
        PlayerPrefs.SetInt("BestMinutes", bestMinutes);
        PlayerPrefs.SetFloat("BestSeconds", bestSeconds);
    }
    public void LoadGame()
    {
        GameSaver gameSaver = SaveSystem.LoadGame();
        myPanelManager.SetPanels(gameSaver);
        myUIManager.LoadTime(gameSaver);        
        hours = gameSaver.hours;
        minutes = gameSaver.minutes;
        seconds = gameSaver.seconds;
    }
    public void PlayerWon()
    {
        if(myPanelManager.CheckSudoku())
        {           
            if(!(bestHours == 0 && bestMinutes==0 && bestSeconds == 0))
            {             
                int totalSeconds = hours*3600 + minutes*60 + (int)seconds;
                int totalsBestSeconds = bestHours * 3600 + bestMinutes*60 + (int)bestSeconds;
                if(totalSeconds<totalsBestSeconds)
                {
                    bestHours = hours;
                    bestMinutes = minutes;
                    bestSeconds = seconds;
                }
            }
            else
            {
                bestHours = hours;
                bestMinutes = minutes;
                bestSeconds = seconds;
            }        
            myUIManager.SetBestTime(bestHours.ToString("00") + ":" + bestMinutes.ToString("00") + ":" + bestSeconds.ToString("00"));
            SaveRecord();
            ChangeState(GameState.VICTORY);
        }
    }
    public void StartGame()
    {
        GameSaver gameSaver = SaveSystem.LoadGame();
        if(gameSaver!=null)
        {
            bestHours = PlayerPrefs.GetInt("BestHours",0);
            bestMinutes = PlayerPrefs.GetInt("BestMinutes",0);
            bestSeconds = PlayerPrefs.GetFloat("BestSeconds",0);
        }
        myUIManager.SetBestTime(bestHours.ToString("00") + ":" + bestMinutes.ToString("00") + ":" + bestSeconds.ToString("00"));
        ChangeState(GameState.PLAYING);        
        myPanelManager.GenerateSudoku();
        seconds = 0;
        minutes = 0;
        hours = 0;
    }
    public void ChangeScene(string name)
    {
        SaveGame();
        mySceneController.ChangeScene(name);
    }

	//Getters	
	#region
	public int GetBestHours()
    {
        return bestHours;
    }
    public int GetBestMinutes()
    {
        return bestMinutes;
    }
    public float GetBestSeconds()
    {
        return bestSeconds;
    }
    public int GetHours()
    {
        return hours;
    }
    public int GetMinutes()
    {
        return minutes;
    }
    public float GetSeconds()
    {
        return seconds;
    }
	#endregion
}
