using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLightning : MonoBehaviour
{

    [SerializeField]
    private GameObject visualEffect;

    void Start()
    {
        InvokeRepeating("Lightning", 2.0f, 1.5f);
    }



    void Lightning()
    {
        if(visualEffect.activeSelf)
        {
            visualEffect.SetActive(false);
        } else {visualEffect.SetActive(true);}
        
    }
}
