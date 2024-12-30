
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Request<TReq, TRes> where TRes:IResponse, new()
{
    static readonly HttpClient client = new HttpClient();

    public async Task<TRes> PostRequest(TReq request, string server, string type)
    {
        string url = "http://localhost:" + server + "/" + type;
        string jsonData = JsonConvert.SerializeObject(request);
        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        try
        {
            //사용자 인증 헤더
            if(type != "Register" && type != "Login")
            {
                content.Headers.Add("Id", AuthData.Instance.Id);
                content.Headers.Add("AuthToken", AuthData.Instance.AuthToken);
            }

            HttpResponseMessage httpResponse = await client.PostAsync(url, content);
            string responseString = await httpResponse.Content.ReadAsStringAsync();

            // 상태 코드 확인
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request failed with status code: {httpResponse.StatusCode}");
            }

            return JsonConvert.DeserializeObject<TRes>(responseString);
        }
        catch (Exception e)
        {
            Debug.LogError("Request exception: " + e.Message);
            return new TRes
            {
                Result = ErrorCode.HttpConnectionFail
            };
        }
    }
}
