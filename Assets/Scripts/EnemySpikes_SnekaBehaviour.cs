using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpikes_SnekaBehaviour : StateMachineBehaviour
{

    Vector3 startPos;
    Vector3 nextPos;

    float timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startPos = Vector3.zero;
        nextPos = startPos + new Vector3(2, 0, 0);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        while (true) {
            timer += Time.deltaTime / Time.timeScale;

            if(timer > 0.1) {
                timer = 0;
                return;
            }

            var distance = Vector3.Distance(nextPos,animator.transform.position);

            if(distance > 0.01) {
                animator.transform.position += new Vector3(distance, 0, 0);
            }
            else {
                animator.transform.position -= new Vector3(distance, 0, 0);
            }

        }

    }
  
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

   
}
