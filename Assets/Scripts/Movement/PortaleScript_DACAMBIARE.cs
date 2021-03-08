using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaleScript_DACAMBIARE : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter (Collider other){

        SceneManager.LoadScene(1);

    }
    
    public void Return_to_0(){
        
        if(SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(2);
    }
}
