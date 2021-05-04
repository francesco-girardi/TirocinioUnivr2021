using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;

public class OssAttack : MonoBehaviour
{
        [SerializeField]
        private VisualEffect Fiamma;


        private void ActivateFlame() {
        
            Fiamma.Play();

        }

        private void DeactivateFlame() {
        
            Fiamma.Stop();

        }


}
