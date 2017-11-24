using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public class HttpClient
{
	private HttpWebRequest request;

	public HttpClient()
	{

	}

	public HttpClient(string url)
	{
		request = HttpWebRequest.CreateHttp(url);
	}

	public async Task<HttpResponse> PostAsync<T>(string url, Content<T> content)
	{
		if (request == null)
		{
			request = HttpWebRequest.CreateHttp(url);
		}
		request.Method = "POST";
		await request.GetRequestStream().WriteAsync(content.ReadAsByteArray(), 0, 0);
		var req = await request.GetResponseAsync();
		return new HttpResponse(req);
	}
}

public class HttpResponse
{
	private HttpWebResponse webResponse;
	private HttpStatusCode statusCode;
	private StreamContent content;

	public HttpStatusCode StatusCode
	{
		get
		{
			return statusCode;
		}
	}

	public Content<Stream> Content
	{
		get
		{
			return content;
		}
	}

	public HttpResponse(WebResponse response)
	{
		webResponse = response as HttpWebResponse;
		statusCode = webResponse.StatusCode;
		content = new StreamContent(webResponse.GetResponseStream());
	}
}



public class StringContent : Content<string>
{
	public StringContent(string str) : base(str)
	{

	}

	public override byte[] ReadAsByteArray()
	{
		return Encoding.Default.GetBytes(Con.ToCharArray());
	}

	public override Task<byte[]> ReadAsByteArrayAsync()
	{
		return Task.Run(() =>
		{
			return Encoding.Default.GetBytes(Con.ToCharArray());
		});
	}

	public override Task<string> ReadAsStringAsync()
	{
		return Task.Run(() =>
		{
			return Con;
		});
	}
}

public class StreamContent : Content<Stream>
{
	public StreamContent(Stream stream) : base(stream)
	{
	}

	public override byte[] ReadAsByteArray()
	{
		StreamReader streamReader = new StreamReader(Con);
		return Encoding.Default.GetBytes(streamReader.ReadToEnd());
	}

	public override Task<byte[]> ReadAsByteArrayAsync()
	{
		return Task.Run(() =>
		{
			StreamReader streamReader = new StreamReader(Con);
			return Encoding.Default.GetBytes(streamReader.ReadToEnd());
		});
	}

	public async override Task<string> ReadAsStringAsync()
	{
		StreamReader streamReader = new StreamReader(Con);
		return await streamReader.ReadToEndAsync();
	}
}

public abstract class Content<T> : System.IDisposable
{
	private T con;

	public T Con
	{
		get
		{
			return con;
		}

		set
		{
			con = value;
		}
	}

	public Content()
	{

	}

	public Content(T con)
	{
		this.con = con;
	}

	public void Dispose()
	{

	}

	public abstract Task<string> ReadAsStringAsync();
	public abstract Task<byte[]> ReadAsByteArrayAsync();
	public abstract byte[] ReadAsByteArray();
}
