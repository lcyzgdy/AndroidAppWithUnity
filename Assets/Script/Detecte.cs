using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Detecte : MonoBehaviour
{
	[SerializeField] private AnimationCurve curve;
	[SerializeField] private GameObject statusText;

	private void OnEnable()
	{
		statusText.GetComponent<Text>().text = string.Empty;
	}

	public void OnDetecteBegin()
	{
		StartCoroutine(OrbRotate());
	}

	private IEnumerator OrbRotate()
	{
		float i = 0f;
		while (true)
		{
			transform.Rotate(0, 0, curve.Evaluate(i + Time.deltaTime) * 5);
			i += Time.deltaTime;
			yield return null;
		}
	}

	public async void OnDetecteEnd()
	{
		//StopCoroutine("OrbRotate");
		await Task.Delay(1000);
		StopAllCoroutines();
	}
}
