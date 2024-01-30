using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZomAttack : MonoBehaviour
{
    Animator zomAnimator;

    public int minAttackDamage = 2;
    public int maxAttackDamage = 5;

    private bool isAttacking = false; 

    private void OnTriggerStay2D(Collider2D collider)
    {
        characterManager characterManager = collider.GetComponent<characterManager>();

        if (characterManager != null && !isAttacking)
        {
            StartCoroutine(AttackWithInterval(characterManager));
        }
    }

    private IEnumerator AttackWithInterval(characterManager characterManager)
    {
        isAttacking = true;

        zomAnimator.SetBool("IsAttacking", true);
        int attackDamage = Random.Range(minAttackDamage, maxAttackDamage + 1);
        characterManager.TakeDamage(attackDamage);
        yield return new WaitForSeconds(0.5f);
        zomAnimator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(4f);
        

       
        isAttacking = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        zomAnimator = GetComponentInParent<Animator>();
    }
}
