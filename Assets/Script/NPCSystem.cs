using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSystem : MonoBehaviour
{
    // NPC Number�� 1000���� ����
    // ���ǰ��� ��ȣ�ۿ��� 100���� ����
    public Dictionary<int, string[]> talkData;
    public Dictionary<int, Sprite> profileData;

    public  Sprite[] profileArr;

    private void Awake()
    {
       talkData = new Dictionary<int, string[]>();
        profileData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    private void GenerateData()
    {
        talkData.Add(1000, new string[] { "�ȳ� ���� �������°��� �Ⱦ���:0", "������ �������Ŷ�...:0" });
        talkData.Add(2000, new string[] { "���� �Ƹ��ٿ� ���̱�..:1", "�״��� �����ڿ� �ǹ�~:1", "�׷��� ���ε�:1","���� ���� ����Ʈ ���� ������?:1" });

        profileData.Add(1000, profileArr[0]);
        profileData.Add(2000 + 1, profileArr[1]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }
    public Sprite GetProfile(int id, int profileIndex)
    {
        return profileData[id + profileIndex];
    }
}
