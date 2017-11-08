using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Button_Login : MonoBehaviour
{
	[SerializeField] private string baseUrl;
	[SerializeField] private GameObject errorBox;

	public async void OnClick()
	{
		var editText = GameObject.Find("InputField").GetComponent<InputField>().text;
		HttpClient httpClient = new HttpClient();
		StringContent content = new StringContent(editText);
		try
		{
			httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "bdf0fd11d2f2444dbd2db68f0f43aef4");
			var res = await httpClient.PostAsync(baseUrl, content);
			if (res.StatusCode != HttpStatusCode.OK) throw new Exception("Http Client Error");
			print(await res.Content.ReadAsStringAsync());
		}
		catch (Exception e)
		{
			print(e.Message);
			errorBox.SetActive(true);
			errorBox.GetComponent<Text>().text = e.Message;
			await Task.Delay(3000);
			errorBox.SetActive(false);
		}
	}
}
