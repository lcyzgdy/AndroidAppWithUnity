using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Evaluate : MonoBehaviour
{
	public void OnClick()
	{
		DontDestroyOnLoad(GameObject.Find("DataSaver"));
		SceneManager.LoadScene("Evaluate");
	}
}
