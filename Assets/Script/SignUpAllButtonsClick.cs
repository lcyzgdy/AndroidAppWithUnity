using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignUpAllButtonsClick : MonoBehaviour
{
	public void ReturnButtonClick()
	{
		SceneManager.LoadScene("Login");
	}

	public struct ResponseMessage
	{
		public int code;
		public string msg;
		public string data;
		public bool status;
	};

	struct RegisterUser
	{
		public int account;
		public int password;
		public string name;
		public int birth;
		public int work_start_time;
	}

	private const string baseUrl = "http://123.207.64.210:7000/api/account/regester";

	public async void SignUpButtonClick()
	{
		HttpClient httpClient = new HttpClient();
		RegisterUser info = new RegisterUser
		{
			account = Convert.ToInt32(GameObject.Find("Account").GetComponent<InputField>().text),
			password = Convert.ToInt32(GameObject.Find("Password").GetComponent<InputField>().text),
			name = GameObject.Find("Name").GetComponent<InputField>().text,
			birth = Convert.ToInt32(GameObject.Find("Age").GetComponent<InputField>().text),
			work_start_time = 1
		};
		var json = JsonUtility.ToJson(info);
		var res = await httpClient.PostAsync(baseUrl, new StringContent(json));
		if (res.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception("Internet error");
		var resJson = JsonUtility.FromJson<ResponseMessage>(await res.Content.ReadAsStringAsync());

	}
}
