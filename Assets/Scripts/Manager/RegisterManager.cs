using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class RegisterManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField idField;
    [SerializeField]
    TMP_InputField pwField;
    [SerializeField]
    TMP_InputField confirmField;
    [SerializeField]
    Button submitButton;

    void Start()
    {
        submitButton.onClick.AddListener(OnSubmit);
    }


    async void OnSubmit()
    {
        string id = idField.text;
        string pw = pwField.text;
        string confirm = confirmField.text;

        ReqRegister registerData = new ReqRegister
        {
            Id = id,
            Password = pw,
            ConfirmPassword = confirm
        };

        var request = new Request<ReqRegister, ResRegister>();

        var response = await request.PostRequest(registerData, "27015", "Register");

        if (response.Result != ErrorCode.None)
        {
            Debug.Log($"Regiseter Fail Response: {response.Result}");
        }
        else
        {
            Debug.Log($"Regiseter Success");
        }
    }

}

#region coroutine_ver
//void OnSubmit()
//{
//    string id = idField.text;
//    string pw = pwField.text;
//    string confirm = confirmField.text;

//    RegisterRequest registerData = new RegisterRequest
//    {
//        Id = id,
//        Password = pw,
//        ConfirmPassword = confirm
//    };

//    Debug.Log($"ID: {registerData.Id}, " +
//        $"Password: {registerData.Password}, " +
//        $"ConfirmPassword: {registerData.ConfirmPassword}"); // 필드 값 출력

//    string jsonData = JsonConvert.SerializeObject(registerData);
//    Debug.Log($"json data: {jsonData}");

//    StartCoroutine(PostRegisterRequest(registerData));
//}

//private IEnumerator PostRegisterRequest(RegisterRequest registerData)
//    {
//        //JSON 데이터 생성
//        string jsonData = JsonUtility.ToJson(registerData);
//        string url = "http://localhost:27015/Register";


//        // POST 요청 생성
//        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
//        {
//            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

//              //업로드 핸들러는 HTTP 요청의 본문 데이터를 서버로 전송하는 역할
//            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
//              //다운로드 핸들러는 서버로부터 응답 데이터를 버퍼에 저장하는 역할
//            webRequest.downloadHandler = new DownloadHandlerBuffer();
//            webRequest.SetRequestHeader("Content-Type", "application/json");

//            // 생성한 요청 보내기
//            yield return webRequest.SendWebRequest();

//            Debug.Log("Register Request sent. Status Code: " + webRequest.responseCode);

//            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
//            {
//                Debug.LogError(webRequest.error);
//            }
//            else
//            {
//                // 응답 처리
//                string responseText = webRequest.downloadHandler.text;
//                Debug.Log(responseText);

//                RegisterResponse response = JsonConvert.DeserializeObject<RegisterResponse>(responseText);

//                // 응답 데이터 처리
//                Debug.Log($"Response Result: {response.Result}");

//            }
//        }
//    }
//}
#endregion


