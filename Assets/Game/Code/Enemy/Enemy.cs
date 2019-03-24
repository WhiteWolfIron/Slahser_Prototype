using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Enemy : Unit
{
    private enum SpeedValues
    {
        stop,
        walk,
        run
    }

    readonly Dictionary<SpeedValues, float> SpeedValue = new Dictionary<SpeedValues, float>()
    {
        {SpeedValues.stop, 0 },
        {SpeedValues.walk, 0.5f },
        {SpeedValues.run, 1 },
    };

    public float damage = 10;
    public float knockBackForce = 10;
    public Transform test_target;

    Transform _currentTarget;
    NavMeshAgent _navMeshAgent;
    Coroutine _pathCoroutyne;

    protected override void Awake()
    {
        base.Awake();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.enabled = false;
        GetAnimator.SetFloat(AnimatorHash.speedMultiplier, Random.Range(0.95f, 1.05f));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) GetAnimator.SetTrigger(AnimatorHash.attack);

        if (Input.GetKeyDown(KeyCode.T))
        {
            MoveToTargetPosition(test_target, 1);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            StopMovement();
        }

        if (_currentTarget)
        {
            var distance = (_currentTarget.position - transform.position).sqrMagnitude;

            if (_navMeshAgent.enabled && distance < 4)
            {
                GetAnimator.SetTrigger(AnimatorHash.attack);
            }
        }
    }

    public void MoveToTargetPosition(Transform target, float animatorSpeed)
    {
        if (target == null) return;

        _currentTarget = target;

        if (_pathCoroutyne != null)
        {
            StopCoroutine(_pathCoroutyne);
        }

        _pathCoroutyne = StartCoroutine(CheckPath(target));
        GetAnimator.SetFloat(AnimatorHash.vertial, SpeedValue[SpeedValues.walk]);
    }

    IEnumerator CheckPath(Transform target)
    {
        _navMeshAgent.enabled = true;
        var yieldInstruction = new WaitForSeconds(0.5f);

        while (true)
        {
            _navMeshAgent.SetDestination(target.position);
            yield return yieldInstruction;
        }
    }

    public void StopMovement()
    {
        if (_pathCoroutyne != null)
        {
            StopCoroutine(_pathCoroutyne);
        }

        GetAnimator.SetFloat(AnimatorHash.vertial, SpeedValue[SpeedValues.stop]);
        _navMeshAgent.enabled = false;
    }

    public void Hit()
    {
        var playerMask = 1 << LayerMask.NameToLayer("Player");
        var overlaped = Physics.OverlapBox(transform.TransformPoint(hitVectorPosition), hitVectorSize,
                        Quaternion.identity, playerMask);

        for (int i = 0; i < overlaped.Length; i++)
        {
            var unit = overlaped[i].GetComponent<Unit>();
            if (unit)
            {
                var pushVector = transform.forward;
                Debug.Log(pushVector);
                unit.ApplyDamage(damage, pushVector * knockBackForce);
            }
        }
    }

    [Header("Hitbox")]
    public Vector3 hitVectorPosition = new Vector3(0, 1, 1);
    public Vector3 hitVectorSize = new Vector3(2, 4, 2);
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.TransformPoint(hitVectorPosition), hitVectorSize);
    }
}
