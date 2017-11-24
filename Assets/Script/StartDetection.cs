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

	private string baseUrl;

	[SerializeField] GameObject errorBox;

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
			GetComponentInChildren<Text>().text = "开始检测";
		}
	}

	private void DetectionEnd()
	{
		webCameraTexture.Stop();
		GetComponent<Button>().enabled = true;
		CancelInvoke();
		GC.Collect();
	}

	private async void DetectionBegin()
	{
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
						   where i.isFrontFacing == true
						   select i).ToArray();
		if (frontCamera.Length == 0)
		{
			webCameraTexture = new WebCamTexture(devices[0].name, width, height, cameraFps);
		}
		else
		{
			webCameraTexture = new WebCamTexture(frontCamera[0].name, width, height, cameraFps);
		}

		webCameraTexture.Play();
		GetComponent<Button>().enabled = false;
		InvokeRepeating("CutAndUpload", 0, 3);
	}

	private async void CutAndUpload()
	{
		HttpClient httpClient = new HttpClient();
		Texture2D tex = new Texture2D(512, 512);
		tex.SetPixels32(webCameraTexture.GetPixels32());
		tex.Apply();
		using (MemoryStream ms = new MemoryStream(tex.EncodeToPNG()))
		{
			StreamContent content = new StreamContent(ms);
			try
			{
				var res = await httpClient.PostAsync(baseUrl, content);
				if (res.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception("Http Error");
				var json = await res.Content.ReadAsStringAsync();
				JsonUtility.FromJson<int>(json);
			}
			catch (Exception e)
			{
			}
		}
	}
}
