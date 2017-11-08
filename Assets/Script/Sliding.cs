using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sliding : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			var xAsix = Input.GetTouch(0).deltaPosition.x;
			foreach (var item in GetComponentsInChildren<RectTransform>())
			{
				item.localPosition = new Vector3(item.localPosition.x + xAsix, item.localPosition.y, item.localPosition.z);
			}
		}
	}
}
