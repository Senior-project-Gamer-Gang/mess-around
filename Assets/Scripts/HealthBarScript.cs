using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    private Slider healthbar;
    private float currentHP;
    private GameObject gamemanager;
    void Awake()
    {
        healthbar = GetComponent<Slider>();
        currentHP = healthbar.maxValue;
    }
    // Update is called once per frame
    void Update()
    {
        healthbar.value = currentHP;
    }
    public void HPChange(float hp, float maxHP)
    {
        healthbar.maxValue = maxHP;
        currentHP = hp;
    }
}
