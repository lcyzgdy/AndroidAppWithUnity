using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class History : MonoBehaviour
{
	public void OnClick()
	{
		DontDestroyOnLoad(GameObject.Find("DataSaver"));
		SceneManager.LoadScene("History");
	}
}
