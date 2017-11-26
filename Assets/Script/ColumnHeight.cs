using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnHeight : MonoBehaviour
{
	float height;
	float current;
	// Use this for initialization
	void Start()
	{
		height = Random.Range(0f, 3f);
		current = 0f;
		StartCoroutine(MyUpdate());
	}

	private IEnumerator MyUpdate()
	{
		while (current < height)
		{
			transform.localScale = new Vector3(1, current, 1);
			yield return null;
			current += Time.deltaTime;
		}
	}
}
