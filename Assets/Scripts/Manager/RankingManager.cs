using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [SerializeField] GameObject rankingUI;
    [SerializeField] Button rankingButton;
    [SerializeField] Button closeButton;
    [SerializeField] List<TMP_Text> rankingData;

    void Start()
    {
        rankingUI.SetActive(false);
        rankingButton.onClick.AddListener(OnClickRankingButton);
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    void OnClickRankingButton()
    {
        if (AuthData.Instance.Id == string.Empty && AuthData.Instance.AuthToken == string.Empty)
        {
            Debug.LogWarning("로그인 상태가 아닙니다.");
            return;
        }

        rankingUI.gameObject.SetActive(true);
    }

    private void OnClickCloseButton()
    {
        rankingUI.gameObject.SetActive(false);
    }

    
}
