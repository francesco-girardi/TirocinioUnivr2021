using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Slime_aniController : MonoBehaviour
{

    [SerializeField]
    private Animator boss;

    private int atkc = 1;

    // Start is called before the first frame update
    void Start()
    {
      InvokeRepeating("AttackCount", 2.0f, 5.0f);  
    }

    void AttackCount(){


        switch(atkc)
        {
            case 3:
                boss.SetInteger("attackNumber", 3);
                atkc = 1;
                break;

            case 2:
                boss.SetInteger("attackNumber", 2);
                atkc = 3;
                break;

            case 1:
                boss.SetInteger("attackNumber", 1);
                atkc = 2;
                break;

            default:
                boss.SetInteger("attackNumber", 0);
                break;
        }
    }

    private void ResetCounter(){

        boss.SetInteger("attackNumber", 0);
    }
}
