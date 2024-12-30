using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

public class UserProfileManager : MonoBehaviour
{
    [SerializeField] GameObject profileUI;
    [SerializeField] Button profileButton;
    [SerializeField] Button closeButton;
    [SerializeField] TMP_Text nicknameData;
    [SerializeField] TMP_Text emailData;
    [SerializeField] TMP_Text tierData;
    [SerializeField] TMP_Text totalPlayData;
    [SerializeField] TMP_Text winData;
    [SerializeField] TMP_Text drawData;
    [SerializeField] TMP_Text loseData;
    [SerializeField] TMP_Text expData;

    static readonly HttpClient client = new HttpClient();

    void Start()
    {
        profileUI.SetActive(false);

        profileButton.onClick.AddListener(OnClickProfileButton);
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    async void OnClickProfileButton()
    {
        if (AuthData.Instance.Id==string.Empty && AuthData.Instance.AuthToken == string.Empty)
        {
            Debug.LogWarning("로그인 상태가 아닙니다.");
            return;
        }

        //http 메시지 전송
        Debug.Log($"AuthData: {AuthData.Instance.Id}, {AuthData.Instance.AuthToken}");
        ReqUserProfile userProfile = new ReqUserProfile
        {
            Id = AuthData.Instance.Id
        };

        var request = new Request<ReqUserProfile, ResUserProfile>();
        var userProfileResponse = await request.PostRequest(userProfile, "27030", "Profile");

        if (userProfileResponse.Result != ErrorCode.None)
        {
            Debug.LogWarning("유저 프로필 요청 실패!");
            return;
        }

        LogUserData(userProfileResponse.UserGameData);

        //텍스트에 유저 데이터 반영
        nicknameData.text = userProfileResponse.UserGameData.nickname;
        emailData.text = userProfileResponse.UserGameData.id;
        totalPlayData.text = userProfileResponse.UserGameData.total_games.ToString();
        winData.text = userProfileResponse.UserGameData.win_count.ToString();
        drawData.text = userProfileResponse.UserGameData.draw_count.ToString();
        loseData.text = userProfileResponse.UserGameData.lose_count.ToString();
        expData.text = userProfileResponse.UserGameData.tier_score.ToString();

        profileUI.SetActive(true);


    }

    void OnClickCloseButton()
    {
        profileUI.SetActive(false);
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
