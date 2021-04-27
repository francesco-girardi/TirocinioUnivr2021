using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ehi : MonoBehaviour
{

    [SerializeField]
    private AudioSource commento;

    void OnTriggerEnter (Collider other)
    {
    
    commento.Play();

    }
}
