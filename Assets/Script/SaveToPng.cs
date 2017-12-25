using UnityEngine;
using System.Collections;
using System.IO;

public class SaveToPng : MonoBehaviour
{
	public Shader outShader;
	public Texture inputTex;

	public bool SaveRenderTextureToPNG(Texture inputTex, Shader outputShader, string contents, string pngName, out byte[] bin)
	{
		RenderTexture temp = RenderTexture.GetTemporary(inputTex.width, inputTex.height, 0, RenderTextureFormat.ARGB32);
		Material mat = new Material(outputShader);
		Graphics.Blit(inputTex, temp, mat);
		bool ret = SaveRenderTextureToPNG(temp, contents, pngName, out bin);
		RenderTexture.ReleaseTemporary(temp);
		return ret;
	}

	//将RenderTexture保存成一张png图片
	public bool SaveRenderTextureToPNG(RenderTexture rt, string contents, string pngName, out byte[] bin)
	{
		RenderTexture prev = RenderTexture.active;
		RenderTexture.active = rt;
		Texture2D png = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
		png.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
		byte[] bytes = png.EncodeToPNG();
		bin = bytes;
		if (!Directory.Exists(contents))
			Directory.CreateDirectory(contents);
		//FileStream file = File.Open(contents + "/" + pngName + ".png", FileMode.Create);
		//BinaryWriter writer = new BinaryWriter(file);
		//writer.Write(bytes);
		//file.Close();
		Texture2D.DestroyImmediate(png);
		png = null;
		RenderTexture.active = prev;
		return true;
	}
}
