using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions{

public class AlchemistShop : MonoBehaviour
{
    public GameObject Camera;

    private bool isShopping = false;
    public bool IsShopping{get=>isShopping;}

    private PlayerLogic player;

    void Start(){
        StartCoroutine(TargetPlayer());
    }

    void Update()
    {
        
    }

    void OnTriggerStay(Collider other){
        Vector3 posizione=new Vector3 (18.19f, 2.79f, 17.28f);

        if (Input.GetKey(KeyCode.E) && isShopping==false){
            Camera.transform.position=posizione;
            isShopping = true;
        }

        if(Input.GetKey(KeyCode.Escape) && isShopping==true){
            isShopping = false;
        }
    }

    private IEnumerator TargetPlayer() {
        yield return new WaitForSeconds(1);

        player = PlayerManager.Instance.playerObject.GetComponent<PlayerLogic>();
    }
}
}
