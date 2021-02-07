﻿using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace NHShareBack
{
    public static class HttpListenerExtension
    {
        public static async Task<string> GetResponseTextAsync(this HttpListenerRequest Request)
        {
            using var reader = new StreamReader(Request.InputStream, Request.ContentEncoding);
            return await reader.ReadToEndAsync();
        }

        public static async Task<JObject> GetResponseJObjectAsync(this HttpListenerRequest Request)
        {
            using var reader = new StreamReader(Request.InputStream, Request.ContentEncoding);
            return JsonConvert.DeserializeObject<JObject>(await reader.ReadToEndAsync());
        }

        public static Task SendHttpResponseAsync(this HttpListenerResponse Response, string Content)
        {
            byte[] Buffer = Encoding.UTF8.GetBytes(Content);

            Response.ContentLength64 = Buffer.Length;
            Stream Output = Response.OutputStream;
            Output.Write(Buffer, 0, Buffer.Length);

            Output.Close();

            return Task.CompletedTask;
        }
    }
}
