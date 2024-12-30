using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour
{
    [SerializeField]
    Button StartButton;


    void Start()
    {
        StartButton.onClick.AddListener(OnClickStartButton);
    }

    async void OnClickStartButton()
    {
        //매칭 요청 데이터
        ReqMatching matchingData = new ReqMatching
        {
            Id = AuthData.Instance.Id
        };

        //게임 서버 매칭 요청
        var matchingRequest = new Request<ReqMatching, ResMatching>();
        var matchingResponse = await matchingRequest.PostRequest(matchingData, "27030", "Matching");

        

        if(matchingResponse.Result != ErrorCode.None)
        {
            Debug.Log("매칭 요청 성공!");

            //매칭 상태 확인 시작
            StartCoroutine(CheckMatchingCoroutine());
        }
        else if (matchingResponse.Result == ErrorCode.GameMatchingFailException)
        {
            Debug.LogWarning("매칭 요청 실패!");
            return;
        }
        else
        {
            Debug.LogWarning("매칭 요청 실패!");
            return;
        }

    }

    IEnumerator CheckMatchingCoroutine()
    {
        while (true)
        {
            //비동기 작업 대기
            var checkTask = CheckMatching();

            while (!checkTask.IsCompleted)
            {
                yield return null;
            }

            var response = checkTask.Result;

            if (response.Result == ErrorCode.None)
            {
                Debug.Log($"매칭 성공! 소켓 서버 주소: {response.SocketServerAddress}, 방 번호: {response.RoomNumber}");
                ConnectSocketServer(response.SocketServerAddress, response.RoomNumber);
                yield break;//매칭 성공 시 더 이상 요청 보내지 않음
            }
            else if (response.Result == ErrorCode.GameMatchingFailException)
            {
                Debug.LogWarning("매칭 실패!");
            }
            else if (response.Result == ErrorCode.GameMatchingWaiting)
            {
                Debug.Log("매칭 대기 중...");
            }
            else
            {
                Debug.LogWarning("매칭 상태 확인 실패!");
            }

            yield return new WaitForSeconds(1.0f);//1초마다 반복
        }
    }

    async Task<ResCheckMatching> CheckMatching()
    {
        //매칭 상태 확인
        ReqCheckMatching matchingData = new ReqCheckMatching
        {
            Id = AuthData.Instance.Id
        };
        var matchingRequest = new Request<ReqCheckMatching, ResCheckMatching>();
        var matchingResponse = await matchingRequest.PostRequest(matchingData, "27030", "CheckMatching");
        
        return matchingResponse;
    }

    void ConnectSocketServer(string address, int roomNumber)
    {
        //소켓 서버 연결
    }

}
