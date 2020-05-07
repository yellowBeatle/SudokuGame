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

  public GameSaver(PanelManager myPanelManager, UIManager myUIManager)
  {
    panelsArray = myPanelManager.GetPanelsNum();
    seconds = myUIManager.GetSeconds();
    minutes = myUIManager.GetMinutes();
    hours = myUIManager.GetHours();
  }
}
