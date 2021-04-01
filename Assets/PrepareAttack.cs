using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareAttack : MonoBehaviour
{

    [SerializeField]
    private GameObject lightning;

    [SerializeField]
    private ParticleSystem discharge;



    void ActivateLightning(){

        lightning.SetActive(true);
        
    }

    void DeactivateLightning() {

        lightning.SetActive(false);
    }

    void ActivateDischarge(){

        discharge.Play();
        
    }
}
