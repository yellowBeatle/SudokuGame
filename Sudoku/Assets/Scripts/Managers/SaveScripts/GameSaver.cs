using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaver
{
  public int[] panelsArray;
  public bool[] panelsDisability;
  public float seconds;
  public int minutes;
  public int hours;

  public GameSaver(PanelManager myPanelManager, GameManager myGameManager)
  {
    panelsArray = myPanelManager.GetPanelsNum();
    panelsDisability = myPanelManager.GetPanelsDisability();
    seconds = myGameManager.GetSeconds();
    minutes = myGameManager.GetMinutes();
    hours = myGameManager.GetHours();
  }
}
