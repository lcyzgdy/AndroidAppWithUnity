using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartMove : MonoBehaviour
{
	private void Start()
	{
		//print(transform.localPosition);
	}
	public void StartMove()
	{
		StartCoroutine(MyUpdate());
	}

	public void StopMove()
	{
		//StopCoroutine(MyUpdate());
		StopAllCoroutines();
	}

	IEnumerator MyUpdate()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			//yield return null;
			transform.position += Vector3.left * 0.367355f / 2f;
			if (transform.localPosition.x < -4213.5f)
			{
				transform.localPosition = new Vector3(10481f, -538f, 0f);
			}
		}
	}
}
