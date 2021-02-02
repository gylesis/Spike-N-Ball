using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpikes : Enemy
{
    [SerializeField]
    float timer;

    [SerializeField]
    GameObject mark;

    bool inZone;

    public void SetColor() {

    }

    public override void Attack() {
        base.Attack();
    }

    private void Update() {
        if (!inZone) {
            timer -= Time.deltaTime;
        }
    }


    private void OnTriggerEnter(Collider other) {
        inZone = true;
    }

    private void OnTriggerStay(Collider other) {
        timer += Time.deltaTime;
    }

    private void OnTriggerExit(Collider other) {
        inZone = false;
    }


}
