using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaver
{
  public int[] panelsArray;
  public float seconds;
  public int minutes;
  public int hours;
  public float bestSeconds;
  public int bestMinutes;
  public int bestHours;

  public GameSaver(PanelManager myPanelManager, GameManager myGameManager)
  {
    panelsArray = myPanelManager.GetPanelsNum();
    seconds = myGameManager.GetSeconds();
    minutes = myGameManager.GetMinutes();
    hours = myGameManager.GetHours();
    bestSeconds = myGameManager.GetBestSeconds();
    bestMinutes = myGameManager.GetBestMinutes();
    bestHours = myGameManager.GetBestHours();
  }
}
