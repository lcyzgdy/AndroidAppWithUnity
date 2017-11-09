using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineChart : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		InvokeRepeating("MyUpdate", 0, 3);
	}

	void MyUpdate()
	{
	}
}
