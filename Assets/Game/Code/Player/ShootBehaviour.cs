using UnityEngine;

namespace Game.Player
{
    public class ShootBehaviour : GenericBehaviour
    {
        [SerializeField] string shootButton = "Fire1";
        [SerializeField] ParticleSystem muzzleEffect;

        readonly int shootTrigger = Animator.StringToHash("Shoot1");

        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            CheckShooting(shootTrigger, shootButton);
        }

        void CheckShooting(int trigger, string buttonName)
        {
            if (Input.GetButtonDown(buttonName))
            {
                animator.SetTrigger(trigger);
            }
        }

        void Shoot()
        {
            if (muzzleEffect)
            {
                muzzleEffect.Play();
            }
        }
    }
}