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

    private float counter = 0.00f;
    private float refreshRate = 0.05f;

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
        
            if (Input.GetKeyDown(KeyCode.X)){
                if(mat.GetFloat("DissolveAmount_") < 1f)
                {
                    if(VFXGraph != null)
                    {
                        VFXGraph.gameObject.SetActive(true);
                        VFXGraph.Play();
                    }
                StartCoroutine (Dissolve());
                }else{
                    mat.SetFloat("DissolveAmount_", 0.0f);
                    counter = 0.00f;
                }
            }
        

      

    }
    
    IEnumerator Dissolve ()
    {

            while(mat.GetFloat("DissolveAmount_") < 1f){
                counter = counter + 0.02f;
                mat.SetFloat("DissolveAmount_", counter);

                yield return new WaitForSeconds (refreshRate);
            }
        
    }


}
