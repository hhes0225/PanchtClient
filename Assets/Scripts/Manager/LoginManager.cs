using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField idField;
    [SerializeField]
    TMP_InputField pwField;
    [SerializeField]
    TMP_Text nicknameText;
    [SerializeField]
    TMP_InputField nicknameField;
    [SerializeField]
    Button submitButton;
    [SerializeField]
    Button nicknameSubmitButton;

    void Start()
    {
        nicknameText.gameObject.SetActive(false);
        nicknameField.gameObject.SetActive(false);
        nicknameSubmitButton.gameObject.SetActive(false);
        submitButton.onClick.AddListener(OnSubmitAsync);
    }

    async void OnSubmitAsync()
    {
        string id = idField.text;
        string pw = pwField.text;

        ReqLoginAccountServer loginAccountData = new ReqLoginAccountServer
        {
            Id = id,
            Password = pw
        };

        //계정 서버 로그인
        var loginAccountServerRequest = new Request<ReqLoginAccountServer, ResLoginAccountServer>();
        var LoginAccountServerResponse = await loginAccountServerRequest.PostRequest(loginAccountData, "27015", "Login");

        if (LoginAccountServerResponse.Result != ErrorCode.None)
        {
            Debug.LogWarning("계정 서버 로그인 실패!");
            //에러 코드 예외 처리
            //ex. id나 비번이 올바르지 않습니다. 다시 로그인하세요. 팝업
            return;
        }

        Debug.Log("계정 서버 로그인 성공!");

        ReqLoginGameServer loginGameData = new ReqLoginGameServer
        {
            Id = LoginAccountServerResponse.Id,
            AuthToken = LoginAccountServerResponse.AuthToken
        };

        AuthData.Instance.SetAuthData(LoginAccountServerResponse.Id, LoginAccountServerResponse.AuthToken);
        

        //게임 서버 로그인
        var loginGameServerRequest = new Request<ReqLoginGameServer, ResLoginGameServer>();
        var loginGameServerResponse = await loginGameServerRequest.PostRequest(loginGameData, "27030", "Login");

        if (loginGameServerResponse.Result == ErrorCode.GameDataNotExist)
        {
            Debug.LogWarning("데이터가 없습니다. 새로 생성해 주세요.");
            ShowNicknameUI();
            return;
        }
        else if (loginGameServerResponse.Result != ErrorCode.None)
        {
            Debug.LogWarning("게임 서버 로그인 실패!");
            return;
        }

        Debug.Log("게임 서버 로그인 성공!");
        LogUserData(loginGameServerResponse.UserGameData);

    }

    void ShowNicknameUI()
    {
        nicknameText.gameObject.SetActive(true);
        nicknameField.gameObject.SetActive(true);
        nicknameSubmitButton.gameObject.SetActive(true);
    }

    public void LogUserData(UserData userData)
    {
        Debug.Log($"User Data: " +
            $"UID: {userData.uid}, " +
            $"ID: {userData.id}, " +
            $"Nickname: {userData.nickname}, " +
            $"Create Date: {userData.create_date?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A"}, " +
            $"Total Games: {userData.total_games}, " +
            $"Win Count: {userData.win_count}, " +
            $"Draw Count: {userData.draw_count}, " +
            $"Lose Count: {userData.lose_count}, " +
            $"Tier Score: {userData.tier_score}");
    }

}
