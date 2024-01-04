using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;
    private Transform target;

    public float damage;

    public float HitWaitTime = 1f;
    private float hitCounter;

    public float health = 5f;

    public float KnockBackTime = 0.5f;

    public float KnockBackCounter;

    public int expToGive = 1;

    public int CoinValue = 1;
    public float coinDropRate = 0.5f;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance.transform;
        target = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.activeSelf)
        {
            if (KnockBackCounter > 0)
            {
                KnockBackCounter -= Time.deltaTime;

                if (moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed * 2f;
                }

                if (KnockBackCounter < 0)
                {
                    moveSpeed = Mathf.Abs(moveSpeed * 0.5f);
                }
            }

            if (Vector3.Distance(target.position, transform.position) > 1.0f)
            {
                rb.velocity = (target.position - transform.position).normalized * moveSpeed;
            }

            if (hitCounter > 0f)
            {
                hitCounter -= Time.deltaTime;
            }
        }

        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && hitCounter <= 0f)
        {
            //Todo :: PlayerHealthController.Instance.TakeDamage(damage);
            hitCounter = HitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        health = damageToTake;

        if(health <= 0f)
        {
            Destroy(gameObject);
            //Todo :: ExerienceLevelController 구현
        }
        else
        {
            //Todo :: SFXManager 데미지 받는 소리
        }

        //Todo :: DamageNumberController 구현
    }

    public void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        TakeDamage(damageToTake);

        if(shouldKnockback)
        {
            KnockBackCounter = KnockBackTime;
        }
    }

}
