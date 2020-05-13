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
    int bestHours;
    int bestMinutes;
    float bestSeconds;
    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.PLAYING;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
            LoadGame();

        switch(currentGameState)
        {
            case GameState.PLAYING:
                if(Input.GetKeyDown(KeyCode.Escape))
                    ChangeState(GameState.PAUSE);
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
                Time.timeScale = 1;
                myUIManager.ShowPauseCanvas(false);
                myPanelManager.DisablePanels(false);
                break;
            case GameState.QUIT:
                myUIManager.ShowVictoryCanvas(false);
                Time.timeScale = 1;
                myPanelManager.DisablePanels(false);
                break;       
        }
        switch(nextState)
        {
            case GameState.PLAYING:
                break;
            case GameState.PAUSE:
                Time.timeScale = 0;
                myPanelManager.DisablePanels(true);
                myUIManager.ShowPauseCanvas(true);
                break;
            case GameState.QUIT:
                break;
            case GameState.VICTORY:
                myUIManager.ShowVictoryCanvas(true);
                myUIManager.SetRecords();
                Time.timeScale = 0;
                myPanelManager.DisablePanels(true);                
                break;
        }
        currentGameState = nextState;
    }
    public void SaveGame()
    {
        SaveSystem.SaveGame(myPanelManager,myUIManager);
    }
    public void LoadGame()
    {
        GameSaver gameSaver = SaveSystem.LoadGame();
        myPanelManager.SetPanelsNum(gameSaver);
        myUIManager.LoadTime(gameSaver);
    }
    public void UnPauseGame()
    {
        ChangeState(GameState.PLAYING);    
    }
    public void PlayerWon()
    {
        if(myPanelManager.CheckSudoku())
        {            
            if(!(bestHours == 0 && bestMinutes==0 && bestSeconds == 0))
            {             
                int totalSeconds = myUIManager.GetHours()*3600 + myUIManager.GetMinutes()*60 + (int)myUIManager.GetSeconds();
                int totalsBestSeconds = bestHours * 3600 + bestMinutes*60 + (int)bestSeconds;
                if(totalSeconds<totalsBestSeconds)
                {
                    bestHours = myUIManager.GetHours();
                    bestMinutes = myUIManager.GetMinutes();
                    bestSeconds = myUIManager.GetSeconds();
                }
            }
            else
            {
                bestHours = myUIManager.GetHours();
                bestMinutes = myUIManager.GetMinutes();
                bestSeconds = myUIManager.GetSeconds();
            }        
            myUIManager.SetBestTime("Best time: " + bestHours.ToString("00") + ":" + bestMinutes.ToString("00") + ":" + bestSeconds.ToString("00"));
            ChangeState(GameState.VICTORY);
        }
    }
}
