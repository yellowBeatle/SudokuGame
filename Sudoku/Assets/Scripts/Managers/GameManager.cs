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
    }
}
