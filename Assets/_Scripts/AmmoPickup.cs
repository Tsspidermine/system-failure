using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmmoPickup : MonoBehaviour
{
    public GameObject heavyGun;
    public GameObject machineGun;

    private bool isGrabbing = false;

    Scene currentScene;
    string sceneName;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Heavy Ammo" && isGrabbing != true)
        {
            heavyGun.GetComponent<Gun>().maxAmmo++;
            Destroy(other.gameObject);
            isGrabbing = true;
        }

        if (other.gameObject.name == "Light Ammo" && isGrabbing != true)
        {
            machineGun.GetComponent<Gun>().maxAmmo = machineGun.GetComponent<Gun>().maxAmmo + 50;
            Destroy(other.gameObject);
            isGrabbing = true;
        }

        if (other.gameObject.CompareTag("Floor"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Heavy Ammo" != false)
        {
            isGrabbing = false;
        }

        if (other.gameObject.name == "Light Ammo" != false)
        {
            isGrabbing = false;
        }

        if (other.gameObject.CompareTag("Floor"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}
