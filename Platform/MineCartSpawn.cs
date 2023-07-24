using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCartSpawn : MonoBehaviour
{
    [SerializeField] GameObject mineCart;
    [SerializeField] Transform cartSpawn;
    [SerializeField] float spawnDelay = 5f;

    MenuManager menuManager;

    // Start is called before the first frame update
    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        Invoke("OnMineCartSpawn", spawnDelay);
    }

    void OnMineCartSpawn()
    {

        Instantiate(mineCart, cartSpawn.position, transform.rotation);
        Invoke("OnMineCartSpawn", spawnDelay);
    }
}
