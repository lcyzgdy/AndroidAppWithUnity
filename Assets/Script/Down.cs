using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down : MonoBehaviour
{
	[SerializeField] private GameObject lastOne;

	// Update is called once per frame
	void Update()
	{
		if (lastOne == null) return;
		var area = lastOne.transform.GetChild(2);
		if (area.gameObject.activeSelf)
		{
			float y = area.localPosition.y - area.GetComponent<RectTransform>().rect.height;
			//var xyz = new Vector3(area.localPosition.x, y, area.localPosition.z);
			//var i = lastOne.transform.worldToLocalMatrix * area.transform.TransformPoint(xyz);
			//print(xyz);
			//print(area.transform.TransformPoint(xyz));
			print(y);
			//GetComponent<RectTransform>().localPosition = new Vector3(0, y, 0);
			transform.localPosition = new Vector3(0, y, 0);
		}
		else
		{
			float y = lastOne.transform.localPosition.y - lastOne.GetComponent<RectTransform>().rect.height;
			//var i = transform.worldToLocalMatrix * area.transform.position;
			transform.localPosition = new Vector3(0, y, 0);
		}
	}
}
