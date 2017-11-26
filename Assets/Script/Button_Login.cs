using System;
using System.Net;
using System.Text;
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

	struct ResponseMessage
	{
		public int code;
		public string msg;
		public bool status;
	}

	public async void OnClick()
	{
		var editText = GameObject.Find("InputField").GetComponent<InputField>().text;
		HttpClient httpClient = new HttpClient();
		//var httpClient = WebRequest.CreateHttp(baseUrl);
		//httpClient.Method = "POST";
		//errorBox.GetComponent<Text>().text = "123456";
		LoginJsonStruct loginJson = new LoginJsonStruct
		{
			account = "20144236",
			password = "20144236"
		};
		var json = JsonUtility.ToJson(loginJson);
		var jsonByte = Encoding.UTF8.GetBytes(json.ToCharArray());
		//errorBox.GetComponent<Text>().text += httpClient.ToString();
		//httpClient.GetRequestStream().Write(jsonByte, 0, jsonByte.Length);
		StringContent content = new StringContent(json);
		try
		{
			var res = await httpClient.PostAsync(baseUrl, content);
			//var res = (await httpClient.GetResponseAsync()) as HttpWebResponse;
			if (res.StatusCode != HttpStatusCode.OK) throw new Exception("Http Client Error");
			var jsonMsg = JsonUtility.FromJson<ResponseMessage>(await res.Content.ReadAsStringAsync());
			if (!jsonMsg.status) throw new Exception("帐户名或密码错误");
			var dataSaver = GameObject.Find("DataSaver");
			DontDestroyOnLoad(dataSaver);
			dataSaver.GetComponent<DataSaver>().SaveData("account", loginJson.account);
			SceneManager.LoadScene("Evaluate");
			await Task.Delay(3000);
		}
		catch (MyHttpException e)
		{
			print(e.Message);
			errorBox.SetActive(true);
			errorBox.GetComponent<Text>().text = "网络错误，请检查后重试";
			await Task.Delay(5000);
			errorBox.SetActive(false);
		}
		catch (Exception e)
		{
			print(e.Message);
			errorBox.SetActive(true);
			errorBox.GetComponent<Text>().text = e.Message;
			await Task.Delay(5000);
			errorBox.SetActive(false);
		}
	}
}
