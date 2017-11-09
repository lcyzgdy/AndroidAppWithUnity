using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sliding : MonoBehaviour
{
	private float initialPosX;

	private void Start()
	{
		initialPosX = 0;
		Input.simulateMouseWithTouches = true;
	}

	private void OnEnable()
	{
		initialPosX = 0;
		Input.simulateMouseWithTouches = true;
	}

	private void Update()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			print("touched");
			var xAsix = Input.GetTouch(0).deltaPosition.x;
			initialPosX += xAsix;
			if (initialPosX < 0)
			{
				initialPosX = 0;
				return;
			}
			foreach (var item in GetComponentsInChildren<RectTransform>())
			{
				item.localPosition = new Vector3(item.localPosition.x + xAsix, item.localPosition.y, item.localPosition.z);
			}
		}
	}


	private void OnMouseDrag()
	{
		print("drag");
		var xAsix = Input.GetAxis("Mouse X");
		print(xAsix);
		initialPosX += xAsix;
		if (initialPosX < 0)
		{
			initialPosX = 0;
			return;
		}
		foreach (var item in GetComponentsInChildren<RectTransform>())
		{
			item.localPosition = new Vector3(item.localPosition.x + xAsix, item.localPosition.y, item.localPosition.z);
		}
	}
}
