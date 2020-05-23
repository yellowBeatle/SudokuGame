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
    int currentFilledCells;
    List<Panel> panelsWithNum = new List<Panel>();
    List<Panel> emptyPanels = new List<Panel>();

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
           ShuffleList();
           ItsUniqueSudoku();
        }
        
    }
    public bool SolveSudoku()
    {        
        if(!FindEmptyCells())
        { 
            AddAllNumbers();
            return true;
        } 
        int position = 0;
        for(int i = position; i<panels.Length;++i)
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
    public void GenerateSudoku()
    {
        DisablePanels(true);
        FillSomeSpaces();
        SolveSudoku();
        ShuffleList();
        GetSolvableSudoku();
        LockNumbers();
    }
    void GetSolvableSudoku(int position = 0)
    {
       if(position>=panels.Length)
          return;
       int numOfSolutions = 0;
       List<PanelConsultant> currentPanelsCo = new List<PanelConsultant>();
       currentPanelsCo = FindPanelConsultants(panels[position]);
       for(int i = 1; i <=9;++i)
       {
           panels[position].SetCandidate(i);
           foreach(PanelConsultant consultant in currentPanelsCo)
           {
               numOfSolutions += consultant.NumberOfSolutions(panels[position]);
           }
       }
       if(numOfSolutions <=12)
       {
           panels[position].EraseNumber();
           panelsWithNum.Remove(panels[position]);
           emptyPanels.Add(panels[position]);
       }
       GetSolvableSudoku(++position);           
    }     
    void ItsUniqueSudoku(int index = 0)
    {   
        if(index >= panels.Length)
            return;
        Panel currentPanel = panels[index];
        int currentNum = currentPanel.GetCurrentNum();
        currentPanel.EraseNumber();
        panelsWithNum.Remove(currentPanel);
        emptyPanels.Add(currentPanel);
        foreach(Panel panel in emptyPanels)
        {
            for(int i = 1;i<=9;++i)
            {
                panel.SetCandidate(i);
                if(CanBePlaced(panel))
                    panel.AddCandidate(i);
            }
        }
        List<int> candidatesOfCurrentPanel = currentPanel.GetListOfCandidates();
        if(candidatesOfCurrentPanel.Count!=1)
        {
            int numOfSolutions = 0;
            for(int i = 0; i<candidatesOfCurrentPanel.Count; i++)
            {              
                currentPanel.SetNumber(candidatesOfCurrentPanel[i]);
                if(SolveSudokuUnique())
                {
                    numOfSolutions++;                    
                }
            }
            if(numOfSolutions>1)
            {
                currentPanel.SetNumber(currentNum);
                emptyPanels.Remove(currentPanel);
                panelsWithNum.Add(currentPanel);
            }
            numOfSolutions=0;
        }
           ItsUniqueSudoku(++index);     
    }
    bool SolveSudokuUnique()
    {        
        if(!FindEmptyCells())
        { 
            for(int i = 0; i<emptyPanels.Count;++i)
            {
                emptyPanels[i].EraseNumber();           
            }  
             return true;
        }        
        int position = 0;
        for(int i = position; i<emptyPanels.Count;++i)
        {
            if(!emptyPanels[i].HasNumber())
            {
                position = i;        
                break;
            }                   
        }      
        for(int i=1;i<=9;++i)
        {
            emptyPanels[position].SetCandidate(i);
            if(CanBePlaced(emptyPanels[position]))
            {              
                emptyPanels[position].SetNumber(i);
                if(SolveSudokuUnique())
                    return true;
                else
                    emptyPanels[position].EraseNumber();
            }
        }
        return false;
    }        
    void LockNumbers()
    {
        for(int i = 0; i<panelsWithNum.Count; ++i)
        {
            panelsWithNum[i].Disable(true);
        }
    }
    public void ClearAllNumbers()
    {
        for(int i = 0; i<panels.Length;++i)
        {
            panels[i].EraseNumber();
        }
        panelsWithNum.Clear();
        emptyPanels.Clear();
    }
    public void ClearAllUserNumbers()
    {
        foreach(Panel panel in emptyPanels)
        {
            panel.EraseNumber();
        }
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
