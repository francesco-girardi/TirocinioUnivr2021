using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Mina : MonoBehaviour
{

     [SerializeField]
    private GameObject mina;

    [SerializeField]
    private GameObject visualEffect;

   
 
    void OnTriggerEnter (Collider other)
    {

       mina.SetActive(false);
       visualEffect.SetActive(true);

       PlayerLogic target = PlayerManager.Instance.playerObject.GetComponent <PlayerLogic>();
       target.TakeDamage(5);

      Destroy(gameObject, 1f);

       }

    
}
