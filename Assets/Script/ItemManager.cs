using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ItemData
{
    public static int itemDamageData = 0;
    public static int itemHpData = 0;
    public static int itemArmorData = 0;
    public static int itemSpeedData = 0;
    public static int itemAttackSpeedData = 0;
    public static float itemCriticalPercentData = 0;
    public static float itemCriticalDamageData = 0;
}

public class ItemManager : MonoBehaviour
{
    public string itemName;

    [Header("������ ���� ����")]
    public int itemDamage;
    public int itemHp;
    public int itemArmor;
    public int itemSpeed;
    public int itemAttackSpeed;
    public float itemCriticalPercent;
    public float itemCriticalDamage;

    [Header("������ ��� ����")]
    public ItemRating itemRating;

    public enum ItemRating
    {
        unCommon,
        Common,
        Rare,
        Uniqe,
        Lengendray
    }

    [Header("������ ����")]
    public string itemStatDescription = 
        "���ݷ� : " + ItemData.itemDamageData +
        "\n ü�� : " + ItemData.itemHpData +
        "\n ���� : " + ItemData.itemArmorData +
        "\n �̵��ӵ� : " + ItemData.itemSpeedData +
        "\n ���ݼӵ� : " + ItemData.itemAttackSpeedData +
        "\n ũ��Ƽ�� Ȯ�� : " + ItemData.itemCriticalPercentData +
        "\n ũ��Ƽ�� ������ : " + ItemData.itemCriticalDamageData;
    public string itemDescription; // ������ ����

    private void Update()
    {
        itemStatDescription = 
        "���ݷ� : " + ItemData.itemDamageData +
        "\n ü�� : " + ItemData.itemHpData +
        "\n ���� : " + ItemData.itemArmorData +
        "\n �̵��ӵ� : " + ItemData.itemSpeedData +
        "\n ���ݼӵ� : " + ItemData.itemAttackSpeedData +
        "\n ũ��Ƽ�� Ȯ�� : " + ItemData.itemCriticalPercentData +
        "\n ũ��Ƽ�� ������ : " + ItemData.itemCriticalDamageData;

        ItemData.itemDamageData = itemDamage;
        ItemData.itemHpData = itemHp;
        ItemData.itemArmorData = itemArmor;
        ItemData.itemSpeedData = itemSpeed;
        ItemData.itemAttackSpeedData = itemAttackSpeed;
        ItemData.itemCriticalPercentData = itemCriticalPercent;
        ItemData.itemCriticalDamageData = itemCriticalDamage;
    }
}
