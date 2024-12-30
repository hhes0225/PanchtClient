using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListManager : MonoBehaviour
{
    [SerializeField] Button charListButton;
    static readonly HttpClient client = new HttpClient();

    // Start is called before the first frame update
    void Start()
    {
        charListButton.onClick.AddListener(OnButtonClick);
    }

    async void OnButtonClick()
    {
        ReqCharacterList reqCharacterList = new ReqCharacterList
        {
            Id = AuthData.Instance.Id
        };

        var request = new Request<ReqCharacterList, ResCharacterList>();

        var resCharacterList = await request.PostRequest(reqCharacterList, "27030", "CharacterList");

        if(resCharacterList.Result != ErrorCode.None||resCharacterList.UserCharacterList==null)
        {
            Debug.LogWarning($"캐릭터 리스트 요청 실패! {resCharacterList.Result}");
            return;
        }

        foreach (var data in resCharacterList.UserCharacterList)
        {
            LogUserCharacaterData(data);
        }
    }


    public void LogUserCharacaterData(UserCharacterData data)
    {
        Debug.Log($"User Collected Character Data: " +
            $"UID: {data.user_id}, " +
            $"ID: {data.character_id}, " +
            $"Nickname: {data.collected_date}" );
    }
}
