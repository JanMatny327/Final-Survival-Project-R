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
    [Header("���� ����")]
    public int level = 1; // �÷��̾� ����
    public int[] levelUpExpBox = { 10, 15, 20, 25, 30, 35, 40, 45, 50, 55,
                                   65, 75, 85, 95, 105, 115, 125, 135, 145, 155, 165,
                                   180, 195, 210, 225, 240, 255, 270, 295, 310, 325,
                                   345, 365, 385, 405, 425, 445, 465, 485, 505, 525,
                                   550, 575, 600, 625, 650, 675, 700, 725, 750, 775 }; // �÷��̾ �������� �ϱ� ���� ����ġ �ʿ䷮�� ���� �迭
    public int exp = 0; // �÷��̾� ����ġ
    public int statPoint = 0; // ���� ����Ʈ
    public int maxLevelValue = 50;

    [Header("Stats")]
    public int gold; // �÷��̾� ���
    public int maxHP = 100; // �÷��̾� �ִ� ü��
    public int hp; // �÷��̾� ���� ü��
    public int damage; // �÷��̾� ������
    public float criticalPercent; // ũ��Ƽ�� Ȯ��
    public float criticalDamage; // ũ��Ƽ�� ������
    public float playerSpeed; // �÷��̾� �̵��ӵ�
    public float jumpPower; // ������
    public bool isJump = false;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameData gameData;
    public GameObject Panel;
    public GameObject player;

    [Header("���� �ؽ�Ʈ")]
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
        gameBroadText.text = PlayerController.Instance.gameData.level + "������ �Ǿ����ϴ�!";
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

