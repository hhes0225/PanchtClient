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
        //��Ī ��û ������
        ReqMatching matchingData = new ReqMatching
        {
            Id = AuthData.Instance.Id
        };

        //���� ���� ��Ī ��û
        var matchingRequest = new Request<ReqMatching, ResMatching>();
        var matchingResponse = await matchingRequest.PostRequest(matchingData, "27030", "Matching");

        

        if(matchingResponse.Result != ErrorCode.None)
        {
            Debug.Log("��Ī ��û ����!");

            //��Ī ���� Ȯ�� ����
            StartCoroutine(CheckMatchingCoroutine());
        }
        else if (matchingResponse.Result == ErrorCode.GameMatchingFailException)
        {
            Debug.LogWarning("��Ī ��û ����!");
            return;
        }
        else
        {
            Debug.LogWarning("��Ī ��û ����!");
            return;
        }

    }

    IEnumerator CheckMatchingCoroutine()
    {
        while (true)
        {
            //�񵿱� �۾� ���
            var checkTask = CheckMatching();

            while (!checkTask.IsCompleted)
            {
                yield return null;
            }

            var response = checkTask.Result;

            if (response.Result == ErrorCode.None)
            {
                Debug.Log($"��Ī ����! ���� ���� �ּ�: {response.SocketServerAddress}, �� ��ȣ: {response.RoomNumber}");
                ConnectSocketServer(response.SocketServerAddress, response.RoomNumber);
                yield break;//��Ī ���� �� �� �̻� ��û ������ ����
            }
            else if (response.Result == ErrorCode.GameMatchingFailException)
            {
                Debug.LogWarning("��Ī ����!");
            }
            else if (response.Result == ErrorCode.GameMatchingWaiting)
            {
                Debug.Log("��Ī ��� ��...");
            }
            else
            {
                Debug.LogWarning("��Ī ���� Ȯ�� ����!");
            }

            yield return new WaitForSeconds(1.0f);//1�ʸ��� �ݺ�
        }
    }

    async Task<ResCheckMatching> CheckMatching()
    {
        //��Ī ���� Ȯ��
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
        //���� ���� ����
    }

}
