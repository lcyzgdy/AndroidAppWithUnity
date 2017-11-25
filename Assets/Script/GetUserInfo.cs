using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUserInfo : MonoBehaviour
{
	struct UserInfo
	{
		public string name;
		public string id;
		public string carType;
		public string carId;
		public int dirveAge;
	};

	UserInfo userInfo;

	void Start()
	{
		var dataSaver = GameObject.Find("DataSaver").GetComponent<DataSaver>();
		userInfo.carId = dataSaver.GetData("CarId");
		userInfo.name = dataSaver.GetData("Name");
		userInfo.dirveAge = Convert.ToInt32(dataSaver.GetData("DriveAge"));
		userInfo.carType = dataSaver.GetData("CarType");
		userInfo.id = dataSaver.GetData("Id");
	}
}
