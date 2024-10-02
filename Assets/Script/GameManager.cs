using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class GameData
{
    [Header("성장 관리")]
    public int level = 1; // 플레이어 레벨
    public int[] levelUpExpBox = { 10, 15, 20, 25, 30, 35, 40, 45, 50, 55,
                                   65, 75, 85, 95, 105, 115, 125, 135, 145, 155, 165,
                                   180, 195, 210, 225, 240, 255, 270, 295, 310, 325,
                                   345, 365, 385, 405, 425, 445, 465, 485, 505, 525,
                                   550, 575, 600, 625, 650, 675, 700, 725, 750, 775 }; // 플레이어가 레벨업을 하기 위한 경험치 필요량을 담은 배열
    public int exp = 0; // 플레이어 경험치
    public int statPoint = 0; // 스텟 포인트
    public int maxLevelValue = 50;

    [Header("Stats")]
    public int gold; // 플레이어 골드
    public int maxHP = 100; // 플레이어 최대 체력
    public int hp; // 플레이어 현재 체력
    public int damage; // 플레이어 데미지
    public float criticalPercent; // 크리티컬 확률
    public float criticalDamage; // 크리티컬 데미지
    public float playerSpeed; // 플레이어 이동속도
    public float jumpPower; // 점프력
    public bool isJump = false;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameData gameData;
    public GameObject Panel;
    public GameObject player;

    [Header("공지 텍스트")]
    public TMP_Text gameBroadText;
    private bool isMessage = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        SettingUI();
        StartCoroutine(BroadCastManager());
    }

    [ContextMenu("Save Data")]
    void SaveData()
    {
        string data = JsonUtility.ToJson(gameData);
        string path = Path.Combine(Application.dataPath, "GameData.json");
        File.WriteAllText(path, data);
    }

    [ContextMenu("Load Data")]
    void LoadData()
    {
        SceneManager.LoadScene("GameScenes");

        string path = Path.Combine(Application.dataPath, "GameData.json");
        if (!File.Exists(path))
        {
            SaveData();
        }
        string data = File.ReadAllText(path);

        gameData = JsonUtility.FromJson<GameData>(data);
    }

    public void SettingUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerController.Instance.dieState)
        {
            if (!Panel.activeSelf)
            {
                Panel.SetActive(true);
                player.SetActive(false);
            }
            else
            {
                Panel.SetActive(false);
                player.SetActive(true);
            }
        }
    }

    public void LevelUpBroad()
    {
        gameBroadText.text = PlayerController.Instance.gameData.level + "레벨이 되었습니다!";
        gameBroadText.color = Color.green;
        isMessage = true;
    }

    private IEnumerator BroadCastManager()
    {
        while (isMessage)
        {
            yield return new WaitForSeconds(0.55f);
            gameBroadText.text = "";
            isMessage = false;
        }
    }
}

