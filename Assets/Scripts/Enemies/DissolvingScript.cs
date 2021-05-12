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
    private Material mat;

    private bool death = false;
    private float counter = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(VFXGraph != null)
        {
            VFXGraph.Stop();
            VFXGraph.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            death = true;
                if(VFXGraph != null)
                {
                    VFXGraph.gameObject.SetActive(true);
                    VFXGraph.Play();
                }
        }

        if(death)
        {
            while(mat.GetFloat("DissolveAmount_") < 1){
                counter = counter + 0.1f;
                mat.SetFloat("DissolveAmount", counter);
            }
        }

    }
    


}
