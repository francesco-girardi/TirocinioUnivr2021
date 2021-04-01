using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject Ball;

    void ChargeSphere(){

        Instantiate(Ball, transform.position, Quaternion.identity);
        
    }

}
