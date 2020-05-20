using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Text currentTime;
    public Text bestTime;
    public Text victoryTime;
    public Text victoryBestTime;
    [SerializeField]
    GameObject pauseCanvas;
    [SerializeField]
    GameObject victoryCanvas;
    float seconds;
    int minutes;
    int hours;

    // Update is called once per frame
    void Update()
    {        
        currentTime.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    public void ShowPauseCanvas(bool enable)
    {
        pauseCanvas.SetActive(enable);
    }
    public void ShowVictoryCanvas(bool enable)
    {
        victoryCanvas.SetActive(enable);
    }
    public void SetRecords()
    {
        victoryTime.text = currentTime.text;
        victoryBestTime.text = bestTime.text;
    }
    public void SetCurrentTime(float gameSeconds, int gameMinutes, int gameHours)
    {
        seconds = gameSeconds;
        minutes = gameMinutes;
        hours = gameHours;
    }
    public void SetBestTime(string currentBestTime)
    {        
        bestTime.text = currentBestTime;
    }
    public void LoadTime(GameSaver myGamesaver)
    {
        seconds = myGamesaver.seconds;
        minutes = myGamesaver.minutes;
        hours = myGamesaver.hours;
    }
}
