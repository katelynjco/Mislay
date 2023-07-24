using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBossAttack : MonoBehaviour
{
    MenuManager menuManager;

    [SerializeField] Transform[] sideDrills;
    [SerializeField] Transform[] upDrills;

    public float attackCooldown;

    [SerializeField] bool canAttack = false;
    public bool isAttacking = false;

    int drills;
    int randomIndex;
    int randomFactor;

    MineBossHealth mineBossHealth;

    void Start()
    {
        mineBossHealth = GetComponent<MineBossHealth>();
        menuManager = FindObjectOfType<MenuManager>();

        drills = sideDrills.Length + upDrills.Length;
        randomIndex = Random.Range(0, drills);
        randomFactor = Random.Range(1, drills);

        attackCooldown = 5f;
        StartCoroutine(AttackCoolDown());
    }

    void Update()
    {
        if(mineBossHealth.bossAlive)
        {
            Attack();

        }
    }

    void Attack()
    {
        if(canAttack)
        {
            drills = sideDrills.Length + upDrills.Length;
            randomIndex = Random.Range(0, upDrills.Length);
            randomFactor = Random.Range(1, 3);
            
            isAttacking = true;
            for (int i = 0; i < randomIndex ; i++)
            {
                if(randomIndex % randomFactor == 0)
                {
                    int randomSideIndex = Random.Range(0, sideDrills.Length);
                    Transform selectedSideDrill = sideDrills[randomSideIndex];
                    // Call the public move function on the selected drill
                    selectedSideDrill.GetComponent<SideDrill>().Move();
                        
                }
                if(randomIndex % randomFactor == 1)
                {
                    int randomUpIndex = Random.Range(0, upDrills.Length);
                    Transform selectedUpDrill = upDrills[randomUpIndex];
                    // Call the public move function on the selected drill
                    if(randomUpIndex % 2 != 0)
                    {
                        selectedUpDrill.GetComponent<Drill>().Move();
                    }
                }
            }

            StartCoroutine(AttackCoolDown());
            isAttacking = false;
        }
    }

    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
