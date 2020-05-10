using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Panel : MonoBehaviour, IPointerClickHandler
{
	public UnityEvent OnPlayerClicked;
	InputField myField;	
	int myNum = 0;
	int candidate;
	
	private void Start()
	{
		myField = GetComponent<InputField>();		
	}
	public void WriteOnMe()
	{		
		int.TryParse(myField.text, out myNum);
		if(myNum>9||myNum<1)
			EraseNumber();
		SetNumber(myNum);
	}
	public void SetNumber(int num) 
	{		
		myNum = num;
		myField.text = myNum.ToString();
		SetCandidate(num);
	}
	public void SetCandidate(int cand)
	{
		candidate = cand;
	}
	public int GetCandidate()
	{
		return candidate;
	}
	public void EraseNumber()
	{
		myNum=0;
		myField.text = " ";
	}
	public int GetCurrentNum()
	{
		return myNum;
	}
	public bool HasNumber()
	{
		return myNum!=0;
	}
	public void OnPointerClick (PointerEventData eventData)
    {
		OnPlayerClicked.Invoke();
    }
	public void Disable(bool enable)
	{
		myField.interactable = enable;
	}
}
