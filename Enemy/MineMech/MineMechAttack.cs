using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMechAttack : MonoBehaviour
{
    MenuManager menuManager;

    [SerializeField] Transform target;
    [SerializeField] float mechAttackDistance = 3f;

    [SerializeField] GameObject mechAttack;
    [SerializeField] Transform mechAttackSpawn1;
    [SerializeField] Transform mechAttackSpawn2;

    [SerializeField] float attackCooldown = 3f;

    [SerializeField] bool mechCanAttack = true;
    public bool mechIsAttacking = false;
    
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
        if(mechCanAttack)
        {
            if (Vector2.Distance(transform.position, target.position) <= mechAttackDistance)
            {
                mechIsAttacking = true;

                StartCoroutine(AttackCoolDown());
                mechIsAttacking = false;
            }
        }
    }

    IEnumerator AttackCoolDown()
    {
        GameObject mechAttack1 = Instantiate(mechAttack, mechAttackSpawn1.position, transform.rotation);
        GameObject mechAttack2 = Instantiate(mechAttack, mechAttackSpawn2.position, transform.rotation);

        mechAttack1.transform.parent = transform;
        mechAttack2.transform.parent = transform;

        mechCanAttack = false;
        yield return new WaitForSeconds(0.01f);

        mechAttack1.transform.parent = null;
        mechAttack2.transform.parent = null;
        
        yield return new WaitForSeconds(attackCooldown);
        mechCanAttack = true;
    }
}
