using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public PanelConsultant[] horizontalPanels;
    public PanelConsultant[] verticalPanels;
    public PanelConsultant[] squarePanels;
    public Panel[] panels;
    Panel currentSelectedPanel;
    public int numOfPanelsToRemove;
    int currentFilledCells;
    List<Panel> panelsWithNum = new List<Panel>();

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
           SolveSudoku();
        }
        if(Input.GetKeyDown(KeyCode.E)) 
        {
           FillSomeSpaces();           
        }
        if(Input.GetKeyDown(KeyCode.R)) 
        {
           Debug.Log(CheckSudoku());
        }
        
    }
    public bool SolveSudoku()
    {        
        if(!FindEmptyCells())
        { 
            AddAllNumbers();
            return true;
        } 
        int position = -1;
        for(int i = 0; i<panels.Length;++i)
        {
            if(!panels[i].HasNumber())
            {
                position = i;        
                break;
            }                   
        }      
        for(int i=1;i<=9;++i)
        {
            panels[position].SetCandidate(i);
            if(CanBePlaced(panels[position]))
            {
                panels[position].SetNumber(i);                
                if(SolveSudoku())
                    return true;
                else
                    panels[position].EraseNumber();
            }
        }
        return false;
    }    
    void RemoveNumber()
    {
        int rnd = Random.Range(0,panelsWithNum.Count);
        Panel panelToErase = panels[rnd];
        while(!panelToErase.HasNumber())
        {
            rnd = rnd + 1 % panelsWithNum.Count;
            panelToErase = panels[rnd];
        }
        panels[rnd].EraseNumber();
        panelsWithNum.Remove(panels[rnd]);
    }
    void DeletePanelNumber(Panel currentPanel)
    {
        currentPanel.EraseNumber();
    }
    public void GenerateSudoku()
    {
        FillSomeSpaces();
        SolveSudoku();
        ShuffleList();
        GetSolvableSudoku();
    }
    void GetSolvableSudoku(int position = 0)
    {
       if(position>80)
          return;
       int numOfSolutions = 0;
       List<PanelConsultant> currentPanelsCo = new List<PanelConsultant>();
       currentPanelsCo = FindPanelConsultants(panelsWithNum[position]);
       for(int i = 1; i <=9;++i)
       {
           panelsWithNum[position].SetCandidate(i);
           foreach(PanelConsultant consultant in currentPanelsCo)
           {
               numOfSolutions += consultant.NumberOfSolutions(panelsWithNum[position]);
           }
       }
       if(numOfSolutions <=12)
       {
           panelsWithNum[position].EraseNumber();
           GetSolvableSudoku(++position);
       }
       GetSolvableSudoku(++position);
           
    } 
    public void ShuffleList()
    {        
        panelsWithNum = panelsWithNum.OrderBy( x => Random.value ).ToList( );
    }
    void AddAllNumbers()
    {
        for(int i = 0; i<panels.Length;++i)
        {
            panelsWithNum.Add(panels[i]);
        }
    }
    bool FindEmptyCells()
    {
        for(int i = 0; i<panels.Length; ++i)
        {
            if(!panels[i].HasNumber())
               return true;
        }        
        return false;
    }
    bool CanBePlaced(Panel panel)
    {
        List<PanelConsultant> currentPanelsCo = new List<PanelConsultant>();
        currentPanelsCo = FindPanelConsultants(panel);
        for(int i = 0;i<currentPanelsCo.Count;++i)
        {
            if(!currentPanelsCo[i].CanBePlaced(panel))
                return false;
        }
        return true;
    }
    bool IsWellPlaced(Panel panel)
    {
        List<PanelConsultant> currentPanelsCo = new List<PanelConsultant>();
        currentPanelsCo = FindPanelConsultants(panel);
        for(int i = 0;i<currentPanelsCo.Count;++i)
        {
            if(!currentPanelsCo[i].IsWellPlaced(panel))
                return false;
        }
        return true;
    }

    List<PanelConsultant> FindPanelConsultants(Panel panel)
    {
        List<PanelConsultant> currentPanelsCo = new List<PanelConsultant>();
        for(int i = 0; i<horizontalPanels.Length; ++i)
        {
            if(horizontalPanels[i].HasMyPanel(panel))
                currentPanelsCo.Add(horizontalPanels[i]);
            if(verticalPanels[i].HasMyPanel(panel))
                currentPanelsCo.Add(verticalPanels[i]);
            if(squarePanels[i].HasMyPanel(panel))
                currentPanelsCo.Add(squarePanels[i]);
            if(currentPanelsCo.Count ==3)
                break;
        }
        return currentPanelsCo;
    }
    void FillSomeSpaces()
    {       
        FillRandomPanelsDiagonal(squarePanels[0]);
        FillRandomPanelsDiagonal(squarePanels[4]);
        FillRandomPanelsDiagonal(squarePanels[8]);
    }
    void FillRandomPanelsDiagonal(PanelConsultant panelCon)
    {
        List<int> numbersToPut = new List<int>(){1,2,3,4,5,6,7,8,9};
        for(int i = 0; i<panelCon.panels.Length;++i)
        {
            int rnd = Random.Range(0,numbersToPut.Count);
            Panel currentPanel = panelCon.panels[i];
            currentPanel.SetNumber(numbersToPut[rnd]);
            numbersToPut.RemoveAt(rnd);
        }
    }   
    public void SetCurrentSelectedPanel(Panel currentPanel)
    {
        currentSelectedPanel = currentPanel;
    }
    public void DisablePanels(bool enable)
    {
        if(enable)
        {
            for(int i = 0; i<panels.Length; ++i)
            {
                panels[i].Disable(!enable);
            }   
        }
        else
        {
            for(int i = 0; i<panels.Length; ++i)
            {
                panels[i].Disable(!enable);
            }  
        }

    }
    public void WriteNumber(int number)
    {
        currentSelectedPanel.SetNumber(number);
    }
    public void EraseNumber()
    {
        currentSelectedPanel.EraseNumber();
    }
    public bool CheckSudoku()
    {
        foreach(Panel panel in panels)
        {
            if(!panel.HasNumber())
                return false;

            if(!IsWellPlaced(panel))
                return false;
        } 
        return true;
    }
    public int[] GetPanelsNum()
    {
        int[] numbers = new int[panels.Length];
        int index = 0;
        foreach(Panel currentPanel in panels)
        {
            numbers[index] = currentPanel.GetCurrentNum();
            index++;
        }
        return numbers;
    }
    public void SetPanelsNum(GameSaver gameSaver)
    {
        for(int i=0;i<panels.Length;++i)
            panels[i].SetNumber(gameSaver.panelsArray[i]);
    }
}
