using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Put this component on your enemy prefabs / objects
public class TargetController : MonoBehaviour
{
    // 
    private const int MAX_HEALTH = 100;
    public int currentHealth = MAX_HEALTH;
    private bool hasHealth;
    public HealthBar healthBar;
    public GameObject healthBarObject;

    // every instance registers to and removes itself from here
    private static readonly HashSet<TargetController> _instances = new HashSet<TargetController>();

    // Readonly property, I would return a new HashSet so nobody on the outside can alter the content
    public static HashSet<TargetController> Instances => new HashSet<TargetController>(_instances);

    // Add target to instances
    private void Awake()
    {
        _instances.Add(this);
    }

    // Set values for health bar slider
    private void Start()
    {
        healthBar.SetMaxHealth(MAX_HEALTH, currentHealth);
    }

    // Remove target from instances when destroyed
    private void OnDestroy()
    {
        _instances.Remove(this);
    }

    // Damage call and activate health bar when at 75 health
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth == 75)
        {
            healthBarObject.SetActive(true);
        }
    }

    // Check if still health left
    public bool checkHasHealth()
    {
        hasHealth = currentHealth > 0 ? true : false;
        return hasHealth;
    }
}