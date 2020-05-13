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

    private void Start()
    {
        bestTime.text = "00:00:00";
    }
    // Update is called once per frame
    void Update()
    {
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
        victoryTime.text = "Time: " + currentTime.text;
        victoryBestTime.text = bestTime.text;
    }
    public void SetBestTime(string currentBestTime)
    {        
        bestTime.text = currentBestTime;
        Debug.Log(bestTime.text);
    }
    public float GetSeconds()
    {
        return seconds;
    }
    public int GetMinutes()
    {
        return minutes;
    }
    public int GetHours()
    {
        return hours;
    }    
    public void LoadTime(GameSaver myGamesaver)
    {
        seconds = myGamesaver.seconds;
        minutes = myGamesaver.minutes;
        hours = myGamesaver.hours;
    }
}
