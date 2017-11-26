using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Joint
{
	public Vector3 org;
	public Vector3 end;
}

public class DrawLine : MonoBehaviour
{
	Event e;
	private Vector3 orgPos;
	private Vector3 endPos;
	private bool canDrawLines = true;
	//ArrayList posAL;
	List<Joint> posAL;
	//ArrayList temppos;
	[SerializeField] private Material lineMaterial;

	void Start()
	{
		//temppos = new ArrayList();
		//posAL = new ArrayList();
		posAL = new List<Joint>();
	}

	public void AddPoint(Vector3 beg, Vector3 end)
	{
		Joint joint = new Joint
		{
			org = beg,
			end = end
		};
		posAL.Add(joint);
	}

	public void AddPoint(Vector3 end)
	{
		if (posAL.Count == 0)
		{
			AddPoint(new Vector3(), end);
			return;
		}
		Joint joint = new Joint
		{
			org = posAL.LastOrDefault().end,
			end = end
		};
		posAL.Add(joint);
	}

	void GLDrawLine()
	{
		//print(posAL.Count);
		if (!canDrawLines)
		{
			return;
		}

		GL.PushMatrix();
		GL.LoadOrtho();

		//beg.x = beg.x / Screen.width;
		//end.x = end.x / Screen.width;
		//beg.y = beg.y / Screen.height;
		//end.y = end.y / Screen.height;
		//Joint tmpJoint = new Joint
		//{
		//	org = beg,
		//	end = end
		//};

		//posAL.Add(tmpJoint);

		lineMaterial.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(new Color(1, 1, 1, 0.5f));
		for (int i = 0; i < posAL.Count; i++)
		{
			Joint tj = (Joint)posAL[i];
			Vector3 tmpBeg = tj.org;
			Vector3 tmpEnd = tj.end;
			GL.Vertex3(tmpBeg.x, tmpBeg.y, tmpBeg.z);
			GL.Vertex3(tmpEnd.x, tmpEnd.y, tmpEnd.z);
		}
		GL.End();
		GL.PopMatrix();
	}

	public void Move()
	{
		foreach (Joint item in posAL)
		{
			item.org += Vector3.left;
			item.end += Vector3.left;
		}
	}

	public void ClearLines()
	{
		canDrawLines = false;
		posAL.Clear();
	}

	void OnPostRender()
	{
		GLDrawLine();
	}
}