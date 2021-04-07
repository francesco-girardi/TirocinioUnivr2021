using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMines : MonoBehaviour
{

    private float cooldown;
    
    [SerializeField]
    private float startCooldown;

    [SerializeField]
    private GameObject Mina;

    [SerializeField]
    private GameObject SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = startCooldown;
    }

    // Update is called once per frame
    void Update()
    {
       if(cooldown <= 0){

           
           Instantiate(Mina, SpawnPoint.transform.position, Quaternion.identity);
           cooldown = startCooldown;

       } else{
           cooldown -= Time.deltaTime;
       }
    }
}
