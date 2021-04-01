using UnityEngine;

public class CallLightning : MonoBehaviour {

    [SerializeField]
    private GameObject visualEffect;

    private void Start() {
        InvokeRepeating("Lightning", 2.0f, 1.5f);
    }

    private void Lightning() {
        if (visualEffect.activeSelf)
            visualEffect.SetActive(false);
        else
            visualEffect.SetActive(true);
    }

}
