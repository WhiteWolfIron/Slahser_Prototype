using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class Unit : MonoBehaviour
{
    public float maxHealth = 100;

    [SerializeField]
    Fraction _fraction;

    protected Health _unitHealth;
    protected Rigidbody _rigidbody;
    protected Animator _animator;

    public Fraction GetFraction => _fraction;
    public Rigidbody GetRigidbody => _rigidbody;
    public Animator GetAnimator => _animator;
    public bool IsDead => _unitHealth.IsDead;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _unitHealth = new Health(maxHealth, OnDie);
    }

    public void ApplyDamage(float damage, Vector3 force)
    {
        _rigidbody.AddForce(force, ForceMode.VelocityChange);
        ApplyDamage(damage);
    }

    public void ApplyDamage(float damage)
    {
        _unitHealth.ApplyDamage(damage);
    }

    protected virtual void OnDie()
    {
        _animator.SetTrigger(AnimatorHash.die);
    }

    public void ApplyHeal(float amount)
    {
        _unitHealth.ApplyHeal(amount);
    }
}

public enum Fraction
{
    team_1 = 0,
    team_2 = 1,
    team_3 = 2
}