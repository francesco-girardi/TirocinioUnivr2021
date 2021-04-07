using UnityEngine;

public class MagicExplosion : MonoBehaviour {

    [SerializeField]
    private GameObject Egg;

    void OnTriggerEnter(Collider other) {
        Instantiate(Egg, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
