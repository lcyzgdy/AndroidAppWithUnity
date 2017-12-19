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

	public async Task<HttpResponse> PostAsync<T>(string url, Content<T> content, string cookie = "")
	{
		request = HttpWebRequest.CreateHttp(url);
		if (cookie != "")
		{
			request.Headers.Add("Cookie", "session=.eJwljjkOwzAMwP6iOYNlHbbzmUKWJbRr0kxF_94A3QiQAz_wyCPOJ-zv44oNHq8FOxCRzswpi5IFVynoVli6mFjkqBY2bzLJGN5KGlPhpWiunbw5hsZgac6I7h1v0b2EV2qj6DCqIXenqZN9RDPGKs7ujTpZwAbXGcd_BuH7A-XCL7A.DRlmRw.VFiiHq-NIaJ8_dBcXaY5F4F2LOo");
		}
		//Console.WriteLine(request.Host);
		//request. = url;
		request.Method = "POST";
		byte[] temp = content.ReadAsByteArray();
		await request.GetRequestStream().WriteAsync(temp, 0, temp.Length);
		var req = await request.GetResponseAsync();
		return new HttpResponse(req);
	}
}

public class HttpResponse
{
	private HttpWebResponse webResponse;
	private HttpStatusCode statusCode;
	private StreamContent content;
	private string cookie;

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

	public string Cookie
	{
		get
		{
			return cookie;
		}
	}

	public HttpResponse(WebResponse response)
	{
		webResponse = response as HttpWebResponse;
		statusCode = webResponse.StatusCode;
		content = new StreamContent(webResponse.GetResponseStream());
		int i = 0;
		foreach (var item in webResponse.Headers.AllKeys)
		{
			if (item.Contains("Cookie")) break;
			i++;
		}
		cookie = webResponse.Headers.GetValues(i).FirstOrDefault();
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

public class MyHttpException : Exception
{
	public MyHttpException()
	{
	}

	public MyHttpException(string message) : base(message)
	{
	}
}