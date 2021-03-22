using UnityEngine;
using UnityEngine.UI;
using Stat;
using Events;
using Interactions;

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

    [Header("Healthbar Info")]
    public Slider healthBarSlider;

    private HealthBar healthBar;

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

    private void Awake() {
        dataPath = Application.persistentDataPath + "/playerData.json";

        SetCurrentHealth(maxHealth);

        healthBar = new HealthBar(healthBarSlider);
        healthBar.SetMaxHealth(maxHealth);
    }

    protected override void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    protected override void Update() {
        healthBar.SetHealth(currentHealth);

        if (Input.GetMouseButtonDown(1))
            RemoveFocus();

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;

            if (Physics.Raycast(standbyCamera.position, standbyCamera.forward, out hit, 100)) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                    SetFocus(interactable);
            }
        }

        if (currentHealth <= 0)
            Die();
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

/// <summary>
/// HealthBar code 
/// </summary>
public class HealthBar {

    private Slider slider;

    /// <summary>
    /// Create a generic healthBar
    /// </summary>
    /// <param name="slider"></param>
    public HealthBar(Slider slider) {
        this.slider = slider;
    }

    /// <summary>
    /// Set maximun health value
    /// </summary>
    /// <param name="value"></param>
    public void SetMaxHealth(int value) {
        if (slider != null)
            slider.maxValue = value;
    }

    /// <summary>
    /// Set current health value
    /// </summary>
    /// <param name="value"></param>
    public void SetHealth(int value) {
        if (slider != null)
            slider.value = value;
    }

}
