using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlchemistSHop : MonoBehaviour
{
    private int numberOfObj = 0;
    private TMP_Text testo;

    void Start(){
        testo = GetComponentInChildren<TMP_Text>();
    }

    void Update(){

    }

    public void IncreaseN(){
        if(numberOfObj < 99){
            numberOfObj++;
        }
        testo.SetText(numberOfObj.ToString());
    }

    public void DecreaseN(){
        if(numberOfObj > 0){
            numberOfObj--;
        }
        testo.SetText(numberOfObj.ToString());
    }

    public void BuyItems(){
        //la funzione mette gli item nell'inventario
        numberOfObj = 0;
        testo.SetText(numberOfObj.ToString());
    }


}
