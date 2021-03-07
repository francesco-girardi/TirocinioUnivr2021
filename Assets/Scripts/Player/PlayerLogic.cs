using UnityEngine;
using UnityEngine.UI;
using Stat;
using Events;

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
    }

    private void Awake() {
        dataPath = Application.persistentDataPath + "/playerData.json";

        SetCurrentHealth(maxHealth);

        healthBar = new HealthBar(healthBarSlider);
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update() {
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
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
