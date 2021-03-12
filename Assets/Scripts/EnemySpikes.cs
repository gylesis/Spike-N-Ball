using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpikes : Enemy {

    bool inZone;

    Vector3 startPos;
    Vector3 nextPos;

    [SerializeField]
    float timer;

    [SerializeField]
    float sneakSpeed;

    [SerializeField]
    float distanceToSneak;

    [SerializeField]
    float distanceToNextPos;

    [SerializeField]
    float dashSpeed;

    bool allowToAttack = true;

    bool attackIsOver = true;

    [SerializeField]
    GameObject deathCollider;

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    Vector3 foraw;

    public void SetColor() {

    }

    public override void Attack() {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine() {
        allowToAttack = false;
        attackIsOver = false;

        bool over = false;
        Debug.Log("Attack");
        var nextPoz = enemyPrefab.transform.position + new Vector3(transform.root.localScale.x * enemyPrefab.transform.right.x * -0.45f,
            transform.root.localScale.x * -enemyPrefab.transform.right.y / 5, 0);

        var temp = enemyPrefab.transform.localScale;
        deathCollider.SetActive(true);

        while (!over) {

            temp = temp * dashSpeed;
            enemyPrefab.transform.localScale = temp;
            enemyPrefab.transform.position = Vector3.Lerp(enemyPrefab.transform.position, nextPoz, 0.5f);

            if (Vector3.Distance(enemyPrefab.transform.position, nextPoz) < 0.2f && temp.x > 0.5f) {
                over = true;
                attackIsOver = true;
            }

            yield return new WaitForSeconds(0.2f);
        }


        allowToAttack = true;

        yield return new WaitForSeconds(1f);
        deathCollider.SetActive(false);
    }

    private void Start() {
        startPos = enemyPrefab.transform.position;
        nextPos = startPos + new Vector3(transform.root.localScale.x * enemyPrefab.transform.right.normalized.x * distanceToSneak, transform.root.localScale.x * -enemyPrefab.transform.right.normalized.y / 5, 0);
    }

    private void Update() {
        //  foraw = startPos + new Vector3(enemyPrefab.transform.right.normalized.x * transform.root.localScale.x * distanceToSneak, -enemyPrefab.transform.right.normalized.y, 0);

        foraw = startPos + new Vector3(enemyPrefab.transform.right.normalized.x * distanceToSneak, -enemyPrefab.transform.right.normalized.y / 5, 0);
        distanceToNextPos = Vector3.Distance(enemyPrefab.transform.position, nextPos);

        if (!inZone) {
            timer -= Time.deltaTime / Time.timeScale;
            if (timer < 0) timer = 0;

            if (attackIsOver) {
                SneakOut();
            }
        }

        if (timer < 0.49 && inZone && allowToAttack) {
            SneakIn();
        }

    }

    private void OnDrawGizmosSelected() {
        var tem = enemyPrefab.transform.position + new Vector3(transform.root.localScale.x * enemyPrefab.transform.right.x * -0.45f,
            -(transform.root.localScale.x/ transform.root.localScale.x) * -enemyPrefab.transform.right.y / 5, 0);
        Gizmos.DrawWireSphere(tem, 0.1f);
    }


    private void OnTriggerEnter(Collider other) {
        inZone = true;
    }

    private void OnTriggerStay(Collider other) {
        timer += Time.deltaTime / Time.timeScale;
        if (timer > 0.9999) {
            timer = 1;
        }
        if (timer > 0.7f && distanceToNextPos < 0.25f && allowToAttack) {
            Attack();
        }


    }

    private void OnTriggerExit(Collider other) {
        inZone = false;
    }

    void SneakIn() {
        enemyPrefab.transform.position = Vector3.Lerp(enemyPrefab.transform.position, nextPos, sneakSpeed);
    }
    void SneakOut() {

        var temp = enemyPrefab.transform.localScale;
        if (enemyPrefab.transform.localScale.x > 0.51f) {
            temp *= 0.99f;
            enemyPrefab.transform.localScale = temp;
        }



        enemyPrefab.transform.position = Vector3.Lerp(enemyPrefab.transform.position, startPos, sneakSpeed);
    }





}
