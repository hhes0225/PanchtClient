using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateUserManager : MonoBehaviour
{
    [SerializeField] TMP_Text nicknameText;
    [SerializeField] TMP_InputField nicknameField;
    [SerializeField] Button nicknameSubmitButton;

    void Start()
    {
        nicknameSubmitButton.onClick.AddListener(OnClickSubmitButtonAsync);
    }

    async void OnClickSubmitButtonAsync()
    {
        string nickname = nicknameField.text;

        ReqCreateUser createUser = new ReqCreateUser
        {
            Id = AuthData.Instance.Id,
            Nickname = nickname,
            AuthToken = AuthData.Instance.AuthToken
        };

        var request = new Request<ReqCreateUser, ResCreateUser>();

        var createUserResponse = await request.PostRequest(createUser, "27030", "CreateUser");

        if (createUserResponse.Result == ErrorCode.GameCreateFailNicknameExist)
        {
            Debug.LogWarning("닉네임이 이미 존재합니다. 다른 닉네임을 입력하세요.");
            return;
        }

        if(createUserResponse.Result != ErrorCode.None)
        {
            Debug.LogWarning("유저 데이터 생성 실패!");
            return;
        }

        Debug.Log("유저 데이터 생성 성공!");
        Debug.Log("게임 서버 로그인 성공!");
        LogUserData(createUserResponse.UserGameData);

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
