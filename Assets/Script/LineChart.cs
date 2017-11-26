using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineChart : MonoBehaviour
{
	int counter;
	System.Random random;
	// Use this for initialization
	void Start()
	{
		counter = 0;
		random = new System.Random();
		//InvokeRepeating("MyUpdate", 0, 3);
	}

	void Update()
	{
		print("AddPoint");
		var line = GameObject.Find("Main Camera").GetComponent<DrawLine>();
		//line.Move();
		//print(transform.localPosition);
		//print(Input.mousePosition);
		if (counter == 0)
		{
			//var halfSize = GetComponent<RectTransform>().rect.size / 2;
			//var pos = transform.localPosition - new Vector3(halfSize.x, halfSize.y);
			//pos.z = 0;
			//pos += new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
			var pos = new Vector3(500f, random.Next(222, 555));
			line.AddPoint(new Vector3(45f, 166f), pos);
			//print(pos);
		}
		else
		{
			var pos = new Vector3(500f, random.Next(222, 555));
			line.AddPoint(pos);
			//line.AddPoint(transform.localPosition);
		}
		counter++;
	}
}
