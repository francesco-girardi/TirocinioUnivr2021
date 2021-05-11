using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DissolvingScript : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private VisualEffect VFXGraph;

    [SerializeField]
    private float dissolveRate = 0.02f;

    [SerializeField]
    private float refreshRate = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
