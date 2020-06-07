using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelConsultant : MonoBehaviour
{
	public Panel[] panels;	

	public bool IsNumberRepeated(Panel panel)
	{
		int currentNum = panel.GetCurrentNum();
		for(int i = 0; i<panels.Length; ++i)
		{
			if(panels[i] == panel)
				continue;
			if(currentNum==panels[i].GetCurrentNum())
				return true;
		}
		return false;
	}
	public bool CanBePlaced(Panel currentPanel)
	{
		for(int i = 0; i<panels.Length; ++i)
		{
			if(panels[i] == currentPanel)
				continue;
			if(currentPanel.GetCandidate()==panels[i].GetCurrentNum())
				return false;
		}
		return true;
	}
	public bool IsWellPlaced(Panel currentPanel)
	{
		for(int i = 0; i<panels.Length; ++i)
		{
			if(panels[i] == currentPanel)
				continue;
			if(currentPanel.GetCurrentNum()==panels[i].GetCurrentNum())
				return false;
		}
		return true;
	}
	public PanelConsultant HasMyPanel(Panel panel)
	{
		for(int i = 0; i<panels.Length;++i)
		{
			if(panel==panels[i])
				return this;
		}
		return null;
	}
	public int NumberOfSolutions(Panel panel)
	{
		int numberOfSolutions = 0;
		if(CanBePlaced(panel))
			numberOfSolutions++;
		return numberOfSolutions;
	}
	public void EraseAllCandidates()
	{
		for(int i = 0; i<panels.Length;++i)
		{
			panels[i].EraseCandidates();
		}
	}
}
