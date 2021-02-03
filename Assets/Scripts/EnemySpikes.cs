using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpikes : Enemy {

    [SerializeField]
    float timer;

    bool inZone;

    [SerializeField]
    GameObject enemyPrefab;

    Vector3 startPos;
    Vector3 nextPos;

    [SerializeField]
    float sneakSpeed;

    [SerializeField]
    float distanceToSneak;

    [SerializeField]
    float distanceToNextPos;

    bool allowToAttack = true;

    bool attackIsOver = true;

    [SerializeField]
    GameObject deathCollider;

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
        var nextPoz = enemyPrefab.transform.position + new Vector3(transform.localScale.x * -0.45f, 0, 0);

        var temp = enemyPrefab.transform.localScale;
        deathCollider.SetActive(true);

        while (!over) {

            temp = temp * 1.3f;
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
        nextPos = startPos + new Vector3(transform.localScale.x * distanceToSneak, 0, 0);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(nextPos, 0.1f);
        Gizmos.DrawWireSphere(enemyPrefab.transform.position, 0.3f);
    }

    private void Update() {
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
        nextPos = new Vector3(nextPos.x , nextPos.y, nextPos.z);
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
