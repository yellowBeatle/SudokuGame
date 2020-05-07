using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Text currentTime;
    public Text bestTime;
    [SerializeField]
    GameObject pauseCanvas;
    float seconds;
    int minutes;
    int hours;

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
