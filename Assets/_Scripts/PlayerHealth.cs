using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;

    public float health = 100f;

    Scene currentScene;
    string sceneName;

    void Start()
    {
        healthText.SetText(health + "%");
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    void Die()
    {
        Debug.Log("Dead");
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        healthText.SetText(health + "%");
        if (health <= 0f)
        {
            Die();
        }
    }
}
