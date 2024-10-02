using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Stat : MonoBehaviour
{
    [Header("���ݺ� ������ ����")]
    [SerializeField] public int hpPlus;
    [SerializeField] public int damagePlus;
    [SerializeField] public float speedPlus;
    

    [Header("�������� ������ �� �ɷ�ġ")] // �������� ���� ������ ��ġ�� ������ �Ҵ�
    [SerializeField] private int hpPlusValue = 0;
    [SerializeField] private int damagePlusValue = 0;
    [SerializeField] private float speedPlusValue = 0;

    [Header("�ؽ�Ʈ ����")]
    [SerializeField] TMP_Text BroadCast;
    [SerializeField] TMP_Text[] plusValueTexts;
    [SerializeField] TMP_Text playerInfo;
    [SerializeField] TMP_Text goldText;
    private bool isMessage = false;

    private void Update()
    {
        StartCoroutine(BroadCastManager());
        PlayerInfoUpdateUI();
    }

    public void HpStatPlus()
    {
        if (PlayerController.Instance.gameData.statPoint > 0)
        {
            PlayerController.Instance.gameData.maxHP += hpPlus;
            PlayerController.Instance.gameData.hp += hpPlus;
            hpPlusValue += hpPlus; 
            PlayerController.Instance.gameData.statPoint -= 1;
            plusValueTexts[0].text = "ü�� ������ : " + hpPlusValue;
            Debug.Log("HP : " + PlayerController.Instance.gameData.hp);
        }
        else { Debug.LogError("���� ����Ʈ�� �����մϴ�. "); BroadCast.text = "���� ����Ʈ�� �����մϴ�."; BroadCast.color = Color.red; isMessage = true; }
    }

    public void DamageStatPlus()
    {
        if (PlayerController.Instance.gameData.statPoint > 0)
        {
            PlayerController.Instance.gameData.damage += damagePlus;
            damagePlusValue += damagePlus;
            PlayerController.Instance.gameData.statPoint -= 1;
            plusValueTexts[1].text = "���ݷ� ������ : " + damagePlusValue;
        }
        else { Debug.LogError("���� ����Ʈ�� �����մϴ�. "); BroadCast.text = "���� ����Ʈ�� �����մϴ�."; BroadCast.color = Color.red; isMessage = true; }
    }

    public void SpeedStatPlus()
    {
        if (PlayerController.Instance.gameData.statPoint > 0)
        {
            PlayerController.Instance.gameData.playerSpeed += speedPlus;
            speedPlusValue += speedPlus;
            PlayerController.Instance.gameData.statPoint -= 1;
            plusValueTexts[2].text = "�̼� ������ : " + speedPlusValue;
        }
        else { Debug.LogError("���� ����Ʈ�� �����մϴ�. "); BroadCast.text = "���� ����Ʈ�� �����մϴ�."; BroadCast.color = Color.red; isMessage = true; }
    } 

    private IEnumerator BroadCastManager()
    {
        while (isMessage)
        {
            yield return new WaitForSeconds(0.55f);
            BroadCast.text = "";
            isMessage = false;
        }
    }

    private void PlayerInfoUpdateUI()
    {
        playerInfo.text = "���ݷ� : " + PlayerController.Instance.gameData.damage +
        "\n" +
        "\n�ִ� ü�� : " + PlayerController.Instance.gameData.maxHP +
        "\n" +
        "\n���� ü�� : " + PlayerController.Instance.gameData.hp +
        "\n" +
        "\n�̵��ӵ� : " + PlayerController.Instance.gameData.playerSpeed +
        "\n" +
        "\n" +
        "\n" +
        "\n���� ����Ʈ : " + PlayerController.Instance.gameData.statPoint;

        goldText.text = "" + PlayerController.Instance.gameData.gold;
    }
}
