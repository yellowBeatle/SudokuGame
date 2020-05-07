using UnityEngine;
using UnityEditor;

public class BuildSudokuLayout : EditorWindow
{
	public int numOfColoumns;
	public int numOfRows;
	public float dist;	 
	[MenuItem("Window/Multiply")]

	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(BuildSudokuLayout));
	}
	private void OnGUI()
	{
		numOfColoumns = EditorGUILayout.IntField("Number of coloumns",numOfColoumns);
		numOfRows = EditorGUILayout.IntField("Number of rows",numOfRows);
		dist = EditorGUILayout.FloatField("Distance between objects", dist);
		if(GUILayout.Button("Multiply"))
		{
			foreach(GameObject obj in Selection.gameObjects)
			{
				for(int j = 1; j<numOfRows; ++j)
				{
					GameObject nextObj = Instantiate(obj, obj.transform.position, Quaternion.identity);
					nextObj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - dist*j, obj.transform.position.z);
				}
				for(int i = 1; i<numOfColoumns; ++i)
				{
					GameObject nextObj = Instantiate(obj, obj.transform.position, Quaternion.identity);
					nextObj.transform.position = new Vector3(nextObj.transform.position.x + dist*i, nextObj.transform.position.y, nextObj.transform.position.z);
					for(int j = 1; j<numOfRows; ++j)
					{
						GameObject currentObj = Instantiate(nextObj, nextObj.transform.position, Quaternion.identity);
						currentObj.transform.position = new Vector3(nextObj.transform.position.x, nextObj.transform.position.y - dist*j, nextObj.transform.position.z);
					}

				}				
			}
		}
	}
}
