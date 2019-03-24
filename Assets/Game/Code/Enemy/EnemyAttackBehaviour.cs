﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(AnimatorHash.attack);
    }
}
