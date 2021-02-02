using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpikes : Enemy {
    [SerializeField]
    float timer;

    bool inZone;

    [SerializeField]
    Animator animator;

  // [SerializeField]
    AnimationCurve curve;

    [SerializeField]
    GameObject enemyPrefab;

    float xBefore;
    float xAfter;

    public void SetColor() {

    }

    public override void Attack() {
        animator.SetTrigger("Attack");
    }

    private void Start() {
        xBefore = enemyPrefab.transform.localPosition.x;
        xAfter = xBefore + 1;

    }

    private void Update() {
        if (!inZone) {
            timer -= Time.deltaTime / Time.timeScale;
            if (timer < 0) timer = 0;
        }

        if (animator.GetBool("Sneak")) {
            animator.enabled = false;

            if (Input.GetKeyDown(KeyCode.G)) {
            //    curve.RemoveKey(1);
            //    curve.RemoveKey(0);

            }

          //  curve.AddKey(0, enemyPrefab.transform.position.x);
          //  curve.AddKey(1, enemyPrefab.transform.position.x + 2);

            curve = new AnimationCurve(new Keyframe { value = xBefore, time = 0 },
                new Keyframe { value = xAfter , time = 1 });

           
            
            Debug.Log(curve.keys[0].value);
            enemyPrefab.transform.position = new Vector3(-curve.Evaluate(timer), enemyPrefab.transform.position.y, enemyPrefab.transform.position.z);
        }

        if (!animator.GetBool("Sneak")) {
            animator.enabled = true;
        }

    }


    private void OnTriggerEnter(Collider other) {
        inZone = true;
        animator.SetBool("Sneak", true);
    }

    private void OnTriggerStay(Collider other) {
        timer += Time.deltaTime / Time.timeScale;
        if (timer > 0.9999) {
            timer = 1;
        }



        if (timer > 0.5f) {
            Attack();
        }
    }

    private void OnTriggerExit(Collider other) {
        inZone = false;
        animator.SetBool("Sneak", false);
    }




}
