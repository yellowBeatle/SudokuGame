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

  public GameSaver(PanelManager myPanelManager, UIManager myUIManager, GameManager myGameManager)
  {
    panelsArray = myPanelManager.GetPanelsNum();
    seconds = myUIManager.GetSeconds();
    minutes = myUIManager.GetMinutes();
    hours = myUIManager.GetHours();
    bestSeconds = myGameManager.GetBestSeconds();
    bestMinutes = myGameManager.GetBestMinutes();
    bestHours = myGameManager.GetBestHours();
  }
}
