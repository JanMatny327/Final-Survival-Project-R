using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class BossSystem : MonoBehaviour
{
    public static BossSystem Instance;
    // �⺻ �Ӽ�
    [SerializeField] private int bossHp; // ���� HP
    private int maxBossHp; // �ִ� ���� Hp
    private Rigidbody2D rig2D; // ���� ����
    private Animator bossAnim; // ���� �ִϸ��̼�
    private BoxCollider2D boxCollider;
    [SerializeField] private int bossLevel = 1; // ���� ����
    [SerializeField] private Transform target;
    public int speedX; // rush �ִ�ӵ�
    int DIRECTION;
    public bool isTurret; // �ͷ��� ������ �ִ°�
    public int turretCount = 0; // �ͷ��� ��ΰ�
    [SerializeField] private BossTurretSystem turretSystem;

    [Header("BossProfile")]
    public GameObject bossProfile; // ���� ü�¹�
    public Slider hpBar; //HP��
    public TextMeshProUGUI hpText; // HP�� �ܷ� ǥ��



    [Header("Boss Bullet List")]
    [SerializeField] private GameObject[] bossBullet; // �Ϲ� �Ѿ� Prefab �迭
    [SerializeField] private GameObject[] bossTurret; // ������ ��ȯ�� ���


    [Header("Boss Attack")]
    [SerializeField] Transform[] spinbulletPos; // ȸ�� ���� �Ѿ� ���� ��ġ
    [SerializeField] Transform[] missilebulletPos; // ����ź ���� �Ѿ� ���� ��ġ
    [SerializeField] Transform[] turretPos; // �ͷ� ��ȯ�� ��ġ
    public float attackCoolTime; // ���� ��Ÿ��
    private float attackCurTime; // ���� ���� Ÿ��
    bool isAttack;
    public int bossDamage;

    [Header("BossPattern")]
    [SerializeField] private int patternLevel; // ���� ����
    public float patternCoolTime; // ���� ��Ÿ��
    public float patternWaitTime;
    public float roopPattern;
    public bool isPattern;
    bool isWall = false; // �������Ͽ� ���� ��

    [Header("PatternNote")]
    private static readonly int NONE = 0;
    private static readonly int SPINBULLET = 1;
    private static readonly int MISSILE = 2;
    private static readonly int RUSH = 3;

    private WaitForSeconds endOfPatternWait;
    private WaitForSeconds waitOfPatternWait;



    void Start()
    {
        Instance = this;
        rig2D = GetComponent<Rigidbody2D>();
        bossAnim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        endOfPatternWait = new WaitForSeconds(patternCoolTime); // ���� ���ð� : ���� ������ �����Ҷ��� ���ð�
        waitOfPatternWait = new WaitForSeconds(patternWaitTime); // ���� ���ð� : �ڼ��� ���ϰ� �����ϴ� ���ð�
        maxBossHp = bossHp;
        StartCoroutine(RotateBullet());
        hpBar.value = (float)bossHp / maxBossHp;
    }
    void Update()
    {
        isBossRoom();
        BossLevel();
        HandleHp();
    }

    void HandleHp()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, (float)bossHp / maxBossHp, Time.deltaTime * 10);
        hpText.text = bossHp + "/" + maxBossHp;
    }
    public void getDamage()
    {
        bossHp -= PlayerController.Instance.gameData.damage;
        return;
    }
    void BossBullet()
    {
        if (attackCurTime <= 0)
        {
            switch (patternLevel)
            {
                case 1:
                    for (int i = 0; i < spinbulletPos.Length; i++)
                    {
                        Instantiate(bossBullet[0], spinbulletPos[i].position, spinbulletPos[i].rotation);
                    }
                    break;
                case 2:
                    for (int i = 0; i < missilebulletPos.Length; i++)
                    {
                        Instantiate(bossBullet[1], missilebulletPos[i].position, missilebulletPos[i].rotation);
                    }
                    break;
            }
            attackCurTime = attackCoolTime;
        }
        attackCurTime -= Time.deltaTime;
    }
    void Idle()
    {
        bossAnim.SetInteger("PatternLevel", 0);
    }
    void BossLevel()
    {
        if (bossHp <= maxBossHp / 2 && bossHp > maxBossHp / 3)
        {
            bossLevel = 2;
            bossAnim.SetBool("isCritical", true);
            Invoke("BossPattern", 2);
        }
        else if (bossHp <= maxBossHp / 3)
        {
            bossLevel = 3;
            bossAnim.SetBool("isCritical", true);
            Invoke("BossPattern", 2);
        }

    }
    void lookPlayer()
    {
        DIRECTION = (target.position.x < transform.position.x ? -1 : 1); //player�� �ڽ�(����)�� x��ǥ�� ���ؼ� ������ ����� DIRECTION�� �����Ѵ�.
        float scale = transform.localScale.z;
        transform.localScale = new Vector3(DIRECTION * -1 * scale, scale, scale); //DIRECTION������ �̿��ؼ� player���� �ٶ󺸵����Ѵ�.
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
        else if (collision.gameObject.tag == "Wall")
        {
            isWall = true;
        }
    }

    IEnumerator roopOutPattern()
    {
        yield return new WaitForSeconds(roopPattern);
        isPattern = false;
    }

    void BossPattern()
    {
        if (bossLevel == 1)
        {
            patternLevel = Random.Range(1, 4);
            switch (patternLevel)
            {
                case 1:
                    StartCoroutine(RotateBullet());
                    break;
                case 2:
                    StartCoroutine(MissileBullet());
                    break;
                case 3:
                    StartCoroutine(Rush());
                    break;
            }
        }
        else if (bossLevel == 2)
        {
            patternLevel = Random.Range(1, 2);
            switch (patternLevel)
            {
                case 1:
                    StartCoroutine(Turret());
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
        else if (bossLevel == 3)
        {

        }


    }

    IEnumerator RotateBullet() // �Ѿ��� ȸ���ϸ鼭 ���󰡴� ����
    {
        patternLevel = 1;
        bossAnim.SetInteger("PatternLevel", 1);
        StartCoroutine(roopOutPattern());
        isPattern = true;
        while (isPattern)
        {
            yield return new WaitForSeconds(0.01f);
            foreach (Transform bullet1 in spinbulletPos)
            {
                bullet1.eulerAngles = new Vector3(0f, 0f, bullet1.eulerAngles.z + 1f);
                BossBullet();
            }
        }
        Idle();
        yield return endOfPatternWait;
        BossPattern();
    }
    IEnumerator MissileBullet()
    {
        bossAnim.SetInteger("PatternLevel", 2);
        StartCoroutine(roopOutPattern());
        isPattern = true;
        while (isPattern)
        {
            yield return new WaitForSeconds(0.01f);
            foreach (Transform bullet in missilebulletPos)
            {
                Vector3 direction = (target.position - bullet.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                BossBullet();
            }
        }
        Idle();
        yield return endOfPatternWait;
        BossPattern();
    }
    IEnumerator Rush()
    {
        bossAnim.SetInteger("PatternLevel", 3);
        lookPlayer();
        yield return new WaitForSeconds(1f);
        isWall = false;
        while (!isWall)
        {
            yield return new WaitForSeconds(0.1f);
            if (speedX > rig2D.velocity.x)
            {
                rig2D.AddForce(transform.right * DIRECTION * 1000);
            }
        }
        Idle();
        lookPlayer();
        yield return endOfPatternWait;
        BossPattern();
    }
    IEnumerator Turret()
    {
        yield return new WaitForSeconds(1f);
        foreach (Transform turret in turretPos)
        {
            if (isTurret == false) Instantiate(bossTurret[0], turret.position, turret.rotation);
            turretCount++;
            if (turretCount >= 3)
            {
                isTurret = true;
            }

        }
        yield return endOfPatternWait;
        BossPattern();

    }
    void isBossRoom()
    {
        if (PlayerController.Instance.isBossRoom == true)
        {
            bossProfile.SetActive(true);
        }
    }
}
