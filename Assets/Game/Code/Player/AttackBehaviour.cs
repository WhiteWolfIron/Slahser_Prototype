using UnityEngine;

namespace Game.Player
{
    public class AttackBehaviour : GenericBehaviour
    {
        [SerializeField] string attackButton = "Fire2";
        [SerializeField] string triggerName = "Attack";
        [SerializeField] string indexName = "AttackIndex";
        [SerializeField] int attackNumber = 1;
        

        int attackTrigger;
        int attackIndex;
        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
            attackTrigger = Animator.StringToHash(triggerName);
            attackIndex = Animator.StringToHash(indexName);
        }

        void Update()
        {
            CheckAttacking(attackTrigger, attackButton);
        }

        void CheckAttacking(int trigger, string buttonName)
        {
            if (!Input.GetButtonDown(buttonName))
            {
                return;
            }
            
            animator.SetTrigger(trigger);
            animator.SetInteger(attackIndex, attackNumber);
        }

        void Attack()
        {
        }
    }
}