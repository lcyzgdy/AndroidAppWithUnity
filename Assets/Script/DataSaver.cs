using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
	private Dictionary<string, string> data;

	public void SaveData(string key, string value)
	{
		data[key] = value;
	}

	public string GetData(string key)
	{
		if (!data.ContainsKey(key)) throw new Exception("Key is Null");
		return data[key];
	}
}
