using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_Login : MonoBehaviour
{
	//private string baseUrl = "http://123.207.64.210:7000/api/account/login";
	private string baseUrl = "http://120.79.91.188:7000/api/account/login";
	//private string baseUrl = "http://172.24.12.41:8080/login";
	[SerializeField] private GameObject errorBox;

	struct LoginJsonStruct
	{
		public string account;
		public string password;
	};

	public struct ResponseMessage
	{
		public int code;
		public string msg;
		public UserData data;
		public bool status;
	};

	public struct UserData
	{
		public string account;
		public int age;
		public int bad_record;
		public string name;
		public int work_age;
	};

	public void SignUpButtonClick_LoginScene()
	{
		SceneManager.LoadScene("SignUp");
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
			account = editText,
			password = editText
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
			var resStr = await res.Content.ReadAsStringAsync();
			//print(resStr);
			var jsonMsg = JsonUtility.FromJson<ResponseMessage>(resStr);
			//print(jsonMsg.msg);
			//print(jsonMsg.data);
			if (!jsonMsg.status) throw new Exception("帐户名或密码错误");

			string dataS = resStr.Substring(resStr.LastIndexOf('{'), resStr.IndexOf('}') - resStr.LastIndexOf('{') + 1);
			//print(dataS);
			var dataSaver = GameObject.Find("DataSaver");
			//var dataJson = JsonUtility.FromJson<UserData>(jsonMsg.data);
			//dataSaver.GetComponent<DataSaver>().SaveStringData(jsonMsg.data);
			dataSaver.GetComponent<DataSaver>().SaveStringData(dataS);
			dataSaver.GetComponent<DataSaver>().SaveData("Cookie", res.Cookie);
			print(res.Cookie);
			//print(dataSaver.GetComponent<DataSaver>().GetStringData());
			//DontDestroyOnLoad(dataSaver);
			SceneManager.LoadScene("Evaluate");
			await Task.Delay(3000);
		}
		catch (MyHttpException e)
		{
			//print(e.Message);
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
