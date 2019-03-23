using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : Unit
{
    const float WALK_SPEED = 0.5f;
    const float RUN_SPEED = 1f;

    public Transform currentTarget;

    NavMeshAgent _navMeshAgent;

    Coroutine _moveCoroutine;

    protected override void Awake()
    {
        base.Awake();

        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            MoveToTargetPosition(currentTarget, 1);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            StopMovement();
        }
    }

    public void MoveToTargetPosition(Transform target, float animatorSpeed)
    {
        if (target == null) return;

        currentTarget = target;
        _navMeshAgent.SetDestination(target.position);

        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }

        _moveCoroutine = StartCoroutine(SetAnimatorSpeed(animatorSpeed));
    }

    IEnumerator SetAnimatorSpeed(float animatorSpeed)
    {
        var currentSpeed = GetAnimator.GetFloat(AnimatorHash.vertial);

        while (animatorSpeed != currentSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, animatorSpeed, Time.deltaTime);
            GetAnimator.SetFloat(AnimatorHash.vertial, currentSpeed);
            yield return null;
        }
    }

    public void StopMovement()
    {
        _navMeshAgent.isStopped = true;

        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }

        _moveCoroutine = StartCoroutine(SetAnimatorSpeed(0));
    }
}
