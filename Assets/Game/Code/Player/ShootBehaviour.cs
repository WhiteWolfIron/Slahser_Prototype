using UnityEngine;

namespace Game.Player
{
    public class ShootBehaviour : GenericBehaviour
    {
        [SerializeField] string shootButton = "Fire1";
        [SerializeField] string triggerName = "Shoot";
        [SerializeField] string indexName = "ShootIndex";
        [SerializeField] ParticleSystem muzzleEffect;

        int shootTrigger;
        int shootIndex;
        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
            shootTrigger = Animator.StringToHash(triggerName);
            shootIndex = Animator.StringToHash(indexName);
        }

        void Update()
        {
            CheckShooting(shootTrigger, shootButton);
        }

        void CheckShooting(int trigger, string buttonName)
        {
            if (!Input.GetButtonDown(buttonName))
            {
                return;
            }

            animator.SetTrigger(trigger);
            animator.SetInteger(shootIndex, 0);
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