using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetAndSetUserInfo : MonoBehaviour
{
	struct UserInfo
	{
		public string account;
		public int age;
		public int bad_record;
		public string name;
		public int work_age;
	}
	// Use this for initialization
	void Start()
	{
		var dataSaver = GameObject.Find("DataSaver");
		var str = dataSaver.GetComponent<DataSaver>().GetStringData();
		Debug.Log(str);
		var info = JsonUtility.FromJson<UserInfo>(str);
		GameObject.Find("Name").GetComponent<Text>().text = "姓名：" + info.name;
		GameObject.Find("ID").GetComponent<Text>().text = "工号：" + info.account;
		GameObject.Find("Age").GetComponent<Text>().text = "年龄：" + info.age;
		GameObject.Find("Car").GetComponent<Text>().text = "驾龄：" + info.work_age;
		GameObject.Find("CarID").GetComponent<Text>().text = "不良次数：" + info.bad_record;
	}
}
