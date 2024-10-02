using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("BGM ���� ����")]
    public AudioClip[] BgmClips;
    public AudioSource BgmPlayer;

    [Header("SFX ���� ����")]
    public AudioClip[] SfxClips;
    public AudioSource SfxPlayer;

    private void Awake()
    {
        if (Instance != null)
        { Destroy(this.gameObject); }
        else { DontDestroyOnLoad(this.gameObject); Instance = this; }
    }

    public void BgmPlay(string type)
    {
        switch (type)
        {
            case "ù �ε� ȭ�� ����":
                BgmPlayer.clip = BgmClips[0];
                BgmPlayer.Play(); break;
            case "Ÿ��Ʋ ����":
                BgmPlayer.clip = BgmClips[1];
                BgmPlayer.Play(); break;
            case "��1 �����":
                BgmPlayer.clip = BgmClips[2];
                BgmPlayer.Play(); break;
            case "������ �����":
                BgmPlayer.clip = BgmClips[3];
                BgmPlayer.Play(); break;
            default:
                Debug.LogError("ã�� �� ���� �����"); break;
        }
    }

    public void SfxPlay(string type)
    {
        switch (type)
        {
            case "�÷��̾� �� ����":
                SfxPlayer.PlayOneShot(SfxClips[0]); break;
            case "�߰��� �Ҹ�":
                SfxPlayer.clip = SfxClips[1];
                SfxPlayer.Play(); break;
            default:
                Debug.LogError("ã�� �� ���� ȿ����"); break;
        }
    }
}

