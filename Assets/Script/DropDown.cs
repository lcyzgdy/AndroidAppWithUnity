using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{
	private bool dropState = false;
	[SerializeField] private GameObject downArea;

	public void OnClick()
	{
		if (dropState)
		{
			downArea.SetActive(false);
			transform.rotation = Quaternion.Euler(0, 0, 90);
			dropState = false;
		}
		else
		{
			downArea.SetActive(true);
			transform.rotation = Quaternion.Euler(0, 0, 0);
			dropState = true;
		}
	}
}
