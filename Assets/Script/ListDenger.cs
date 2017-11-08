using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class ListDenger : MonoBehaviour
{
	[SerializeField] private GameObject textBox;
	private string baseUrl;

	private void OnEnable()
	{
		HttpClient client = new HttpClient();
		using (StringContent content = new StringContent(""))
		{
			try
			{
				//var res = client.PostAsync(baseUrl, content);
				var txt = Instantiate(textBox);
				float wid = GetComponent<RectTransform>().rect.width;
				for (int i = 0; i < 10; i++)
				{
					float posX = wid / 10f * i - wid / 2f + wid / 20f;
					var item = Instantiate(textBox);
					item.transform.parent = gameObject.transform;
					item.transform.localScale = Vector3.one;
					item.transform.localPosition = new Vector3(posX, 0, 0);
				}
			}
			catch (Exception e)
			{
				print(e.Message);
				return;
			}
		}
	}
}
