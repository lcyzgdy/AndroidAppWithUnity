using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_Login : MonoBehaviour
{
	private string baseUrl = "http://123.207.64.210:7000/api/account/login";
	[SerializeField] private GameObject errorBox;

	struct LoginJsonStruct
	{
		public string account;
		public string password;
	};

	public async void OnClick()
	{
		var editText = GameObject.Find("InputField").GetComponent<InputField>().text;
		HttpClient httpClient = new HttpClient();
		errorBox.GetComponent<Text>().text = "123456";
		LoginJsonStruct loginJson = new LoginJsonStruct
		{
			account = editText,
			password = "20151234"
		};
		var json = JsonUtility.ToJson(loginJson);
		errorBox.GetComponent<Text>().text += httpClient.ToString();
		StringContent content = new StringContent(json);
		try
		{
			var res = await httpClient.PostAsync(baseUrl, content);
			errorBox.GetComponent<Text>().text += res.ToString();
			if (res.StatusCode != HttpStatusCode.OK) throw new Exception("Http Client Error");
			print(await res.Content.ReadAsStringAsync());
			SceneManager.LoadScene("Evaluate");
			await Task.Delay(3000);
		}
		catch (Exception e)
		{
			print(e.Message);
			//errorBox.SetActive(true);
			errorBox.GetComponent<Text>().text += e.Message;
			//await Task.Delay(3000);
			//errorBox.SetActive(false);
		}
	}
}
