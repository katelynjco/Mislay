using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHumanAttack : MonoBehaviour
{
    MenuManager menuManager;

    [SerializeField] Transform target;
    [SerializeField] float humanAttackDistance = 1.5f;

    [SerializeField] GameObject humanAttack;
    [SerializeField] Transform humanAttackSpawn;

    [SerializeField] float attackCooldown = 3f;

    bool humanCanAttack = true;
    public bool humanIsAttacking = false;
    
    void Start() 
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(humanCanAttack)
        {
            if (Vector2.Distance(transform.position, target.position) <= humanAttackDistance)
            {
            humanIsAttacking = true;

            StartCoroutine(AttackCoolDown());
            humanIsAttacking = false;
            }
        }
    }

    IEnumerator AttackCoolDown()
    {
        GameObject minerAttack = Instantiate(humanAttack, humanAttackSpawn.position, transform.rotation);

        minerAttack.transform.parent = transform;

        humanCanAttack = false;
        yield return new WaitForSeconds(0.01f);

        minerAttack.transform.parent = null;

        yield return new WaitForSeconds(attackCooldown);
        humanCanAttack = true;
    }
}
