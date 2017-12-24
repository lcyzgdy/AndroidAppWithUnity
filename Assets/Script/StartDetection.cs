using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartDetection : MonoBehaviour
{
	private WebCamTexture webCameraTexture;
	private int width = 720;
	private int height = 1280;
	private int cameraFps = 30;
	private HttpClient httpClient;

	private string baseUrl = "http://123.207.64.210:7000/api/user/analyze";
	private string cookie;

	[SerializeField] GameObject errorBox;
	[SerializeField] GameObject orb;

	private void Start()
	{
		cookie = GameObject.Find("DataSaver").GetComponent<DataSaver>().GetData("Cookie");
	}

	public void OnClickAsync()
	{
		if (GetComponentInChildren<Text>().text == "开始检测")
		{
			DetectionBegin();
			GetComponentInChildren<Text>().text = "停止检测";
		}
		else
		{
			DetectionEnd();
		}
	}

	private async void DetectionEnd()
	{
		string url = "http://123.207.64.210:7000/api/user/end_drive";
		var res = await httpClient.PostAsync(url, new StringContent(string.Empty), cookie);
		print(await res.Content.ReadAsStringAsync());
		GetComponentInChildren<Text>().text = "开始检测";
		webCameraTexture.Stop();
		orb.GetComponent<Detecte>().OnDetecteEnd();
		GameObject.Find("LineChart").GetComponent<ChartMove>().StopMove();
		GameObject.Find("LineChart2").GetComponent<ChartMove>().StopMove();
		CancelInvoke();
		GC.Collect();
	}

	private async void DetectionBegin()
	{
		httpClient = new HttpClient();
		try
		{
			string url = "http://123.207.64.210:7000/api/user/start_drive";
			StringContent @string = new StringContent(string.Empty);
			//print(cookie);
			var res = await httpClient.PostAsync(url, @string, cookie);
			print(await res.Content.ReadAsStringAsync());
		}
		catch (Exception e)
		{
			print(e.Message);
			return;
		}

		if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
			Application.RequestUserAuthorization(UserAuthorization.WebCam);
		if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
		{
			print("No Camera");
			errorBox.SetActive(true);
			errorBox.GetComponent<Text>().text = "No Camera";
			await Task.Delay(3000);
			errorBox.SetActive(false);
			return;
		}
		WebCamDevice[] devices = WebCamTexture.devices;

		var frontCamera = (from i in devices
						   where i.isFrontFacing
						   select i).ToArray();
		if (frontCamera.Length == 0)
		{
			webCameraTexture = new WebCamTexture(devices[0].name, width, height, cameraFps);
		}
		else
		{
			webCameraTexture = new WebCamTexture(frontCamera[0].name, width, height, cameraFps);
		}
		orb.SetActive(true);
		orb.GetComponent<Detecte>().OnDetecteBegin();
		webCameraTexture.Play();
		GameObject.Find("LineChart").GetComponent<ChartMove>().StartMove();
		GameObject.Find("LineChart2").GetComponent<ChartMove>().StartMove();
		tex = new Texture2D(webCameraTexture.width, webCameraTexture.height);
		InvokeRepeating("CutAndUpload", 0, 3);
	}
	private Texture2D tex;

	private async void CutAndUpload()
	{
		//print(webCameraTexture.GetPixels().Length);
		//print(tex.GetPixels().Length);
		tex.SetPixels(webCameraTexture.GetPixels());
		tex.Apply();
		//Graphics.Blit(webCameraTexture, renderTexture);
		//var img = GameObject.Find("DebugImage").GetComponent<Image>();
		//img.sprite = Sprite.Create(tex, new Rect(0, 0, 348.5f, 348.5f), Vector2.zero);
		using (MemoryStream ms = new MemoryStream(tex.EncodeToPNG()))
		{
			StreamContent content = new StreamContent(ms);
			try
			{
				//print(content.ReadAsByteArray());
				FileStream fs = new FileStream("temp.png", FileMode.OpenOrCreate);
				BinaryWriter writer = new BinaryWriter(fs);
				var byteArr = content.ReadAsByteArray();
				writer.Write(byteArr);
				//print(byteArr.Length);
				//fs.Write(byteArr, 0, byteArr.Length);
				fs.Close();
				var res = await httpClient.PostAsync(baseUrl, content, cookie);
				if (res.StatusCode != System.Net.HttpStatusCode.OK) throw new MyHttpException();
				var json = await res.Content.ReadAsStringAsync();

				print(json);
			}
			catch (MyHttpException e)
			{
				errorBox.SetActive(true);
				errorBox.GetComponent<Text>().text = "网络错误，请检查后重试";
				DetectionEnd();
				await Task.Delay(5000);
				errorBox.SetActive(false);
			}
			catch (Exception e)
			{
				errorBox.SetActive(true);
				errorBox.GetComponent<Text>().text = "未知错误，请重试";
				print(e.Message);
				DetectionEnd();
				await Task.Delay(5000);
				errorBox.SetActive(false);
			}
		}
	}
}
