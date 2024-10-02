using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BossTurretSystem : MonoBehaviour
{
    [SerializeField] private int turretHp;
    public Transform[] bulletTransform; // �Ѿ� �߻� ��ġ
    public GameObject bulletPrefab; // �Ѿ� ������
    GameObject target; // Ÿ��
    public float attackCoolTime;
    float attackCurTime;

    int DIRECTION;

    void Start()
    {
        target = GameObject.Find("Player");
    }

    void Update()
    {
        LookPlayer();
        Attack();
        DestroyTurret();
    }
    void LookPlayer()
    {
        DIRECTION = (target.transform.position.x < transform.position.x ? -1 : 1); //player�� �ڽ�(����)�� x��ǥ�� ���ؼ� ������ ����� DIRECTION�� �����Ѵ�.
        float scale = transform.localScale.z;
        transform.localScale = new Vector3(DIRECTION * -1 * scale, scale, scale); //DIRECTION������ �̿��ؼ� player���� �ٶ󺸵����Ѵ�.
    }
    void Attack()
    {
        foreach (Transform bullet in bulletTransform)
        {
            bullet.eulerAngles = new Vector3(0f, 0f, bullet.eulerAngles.z + 1f);
            TurretBullet();
        }
    }
    void TurretBullet()
    {
        if (attackCurTime <= 0)
        {
            foreach (Transform bullet in bulletTransform)
            {
                Instantiate(bulletPrefab, bullet.position, bullet.rotation);
            }
            attackCurTime = attackCoolTime;

        }
        attackCurTime -= Time.deltaTime;
    }
    void DestroyTurret()
    {
        if (turretHp <= 0)
        {
            Destroy(this.gameObject);
            BossSystem.Instance.turretCount--;
            if (BossSystem.Instance.turretCount <= 0) BossSystem.Instance.isTurret = false;
        }
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            turretHp -= PlayerController.Instance.gameData.damage;
        }
    }
}
