using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Stat;
using Events;
using Interactions;
using Data;

public class PlayerLogic : CharacterStats {

    private static GameObject __Killer;
    public static GameObject Killer {
        get {
            return __Killer;
        }
        set {
            __Killer = value;
        }
    }

    [Tooltip("Used to define the collision with interactable objects")]
    [Header("Player Camera")]
    public Transform standbyCamera;

    [Tooltip("Interactable objects into we are looking for")]
    [Header("View objects")]
    public Interactable focus;

    [HideInInspector]
    public Wallet playerWallet { get; private set; }

    // private Slider healthBarSlider;

    // private HealthBar healthBar;

    private string dataPath;

    public override void Die() {
        base.Die();

        PlayerDeathInfo playerDeathInfo = new PlayerDeathInfo();
        playerDeathInfo.EventDescription = "Player death";
        playerDeathInfo.killer = __Killer;

        EventSystem.Current.FireEvent(playerDeathInfo);

        Destroy(gameObject);

        PlayerManager.GameOver();
    }

    /// <summary>
    /// Set player money
    /// </summary>
    /// <param name="value"></param>
    public void SetCurrentMoney(int value) {
        playerWallet.SetMoney(value);
    }

    /// <summary>
    /// Add money to player
    /// </summary>
    /// <param name="value"></param>
    public void AddMoney(int value) {
        playerWallet.SetMoney(playerWallet.GetMoney() + value);
    }

    /// <summary>
    /// Remove money to player
    /// </summary>
    /// <param name="value"></param>
    /// <returns>true if money are removed</returns>
    public bool RemoveMoney(int value) {
        if (playerWallet.GetMoney() > value)
            playerWallet.SetMoney(playerWallet.GetMoney() - value);
        else
            return false;

        return true;
    }

    private void Awake() {
        dataPath = Application.persistentDataPath + "/playerData.json";

        SetCurrentHealth(maxHealth);

        // healthBarSlider = GameObject.FindGameObjectWithTag("HealthBarUI").GetComponent<Slider>();

        // healthBar = new HealthBar(healthBarSlider);
        // healthBar.SetMaxHealth(maxHealth);

        playerWallet = new Wallet(0);
    }

    protected override void Start() {
        PlayerDatas playerDatas = SavingSystem.PlayerFromJSON(dataPath);
        Debug.Log(playerDatas);
        if (playerDatas != null) {
            SetCurrentHealth(playerDatas.playerHealth);
            SetCurrentMoney(playerDatas.playerMoney);
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    protected override void Update() {

        // healthBar.SetHealth(currentHealth);

        if (Input.GetMouseButtonDown(1))
            RemoveFocus();

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(standbyCamera.position, standbyCamera.forward, out hit, 100)) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                    SetFocus(interactable);
            }
        }

        if (Physics.Raycast(standbyCamera.position, standbyCamera.forward, out hit, 100)) {
            Portal portal = hit.collider.GetComponent<Portal>();
            if (portal != null)
                portal.Teleport();
        }

        if (currentHealth <= 0)
            Die();

        if (transform.position.y == -100)
            transform.position = new Vector3(transform.position.x, 50, transform.position.z);

    }

    private void SetFocus(Interactable interactable) {

        if (interactable != focus) {
            if (focus != null)
                focus.OnDefocused();

            focus = interactable;
        }

        interactable.OnFocused(transform);
    }

    private void RemoveFocus() {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
    }

}

// /// <summary>
// /// HealthBar code 
// /// </summary>
// public class HealthBar {

//     private Slider slider;

//     /// <summary>
//     /// Create a generic healthBar
//     /// </summary>
//     /// <param name="slider"></param>
//     public HealthBar(Slider slider) {
//         this.slider = slider;
//     }

//     /// <summary>
//     /// Set maximun health value
//     /// </summary>
//     /// <param name="value"></param>
//     public void SetMaxHealth(int value) {
//         if (slider != null)
//             slider.maxValue = value;
//     }

//     /// <summary>
//     /// Set current health value
//     /// </summary>
//     /// <param name="value"></param>
//     public void SetHealth(int value) {
//         if (slider != null)
//             slider.value = value;
//     }

// }

/// <summary>
/// Defines player wallet
/// </summary>
[System.Serializable]
public class Wallet {

    int value;

    /// <summary>
    /// Player wallet
    /// </summary>
    /// <param name="value"></param>
    public Wallet(int value) {
        this.value = value;
    }

    /// <summary>
    /// Set player money
    /// </summary>
    /// <param name="value"></param>
    public void SetMoney(int value) {
        this.value = value;
    }

    /// <summary>
    /// Get player money
    /// </summary>
    /// <returns></returns>
    public int GetMoney() {
        return value;
    }
}
