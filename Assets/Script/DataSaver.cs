using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
	private Dictionary<string, string> data;
	private string stringData;

	public void SaveData(string key, string value)
	{
		data[key] = value;
	}

	public void SaveStringData(string data)
	{
		stringData = data;
	}

	public string GetData(string key)
	{
		if (!data.ContainsKey(key)) throw new Exception("Key is Null");
		return data[key];
	}

	public string GetStringData()
	{
		return stringData;
	}

	private void Start()
	{
		stringData = string.Empty;
		data = new Dictionary<string, string>();
		GameObject.DontDestroyOnLoad(gameObject);
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
