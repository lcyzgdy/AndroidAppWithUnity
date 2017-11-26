using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDenger : MonoBehaviour
{
	private void Update()
	{
		//print(GetComponent<Text>().text);
		if (Input.GetMouseButtonDown(0) && In(Input.mousePosition))
		{
			GameObject.Find("Detail").GetComponent<Text>().text = GetComponent<Text>().text.Replace("\n", "");//.Replace('\r', '\0');
		}
		else if (Input.touchCount > 0 && In(Input.touches[0].position))
		{
			GameObject.Find("Detail").GetComponent<Text>().text = GetComponent<Text>().text.Replace("\n", "");//.Replace('\r', '\0');
		}
	}

	private bool In(Vector2 pos)
	{
		pos -= new Vector2(Screen.width / 2, Screen.height / 2);
		//print(pos);
		//print(transform.localPosition);
		if (pos.x > transform.localPosition.x + 28.65f) return false;
		if (pos.x < transform.localPosition.x - 28.65f) return false;
		if (pos.y > transform.localPosition.y + 105f) return false;
		if (pos.y < transform.localPosition.y - 105f) return false;
		return true;
	}
}
