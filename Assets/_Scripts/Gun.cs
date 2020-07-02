using UnityEngine;
using UnityEngine.VFX;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public float clipSize = 10f;

    public float maxAmmo = 10f;
    private float currentAmmo;
    public float reloadTime = 1f;
    public float particleLife = 0.25f;
    private bool isReloading = false;
    public TextMeshProUGUI ammoText;

    public Camera fpsCam;
    public GameObject impactEffect;
    public GameObject weaponManager;
    public GameObject player;

    private float nextTimeToFire = 0f;

    public Animator animator;

    PlayerControls controls;

    void Start()
    {
        Time.timeScale = 1f;
        currentAmmo = maxAmmo;
        ammoText.GetComponent<TextMeshProUGUI>();
        ammoText.SetText(currentAmmo + "/" + maxAmmo);
    }

    float firing;
    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Reload.performed += ctx => GamepadReload();

        controls.Gameplay.Fire.performed += ctx => firing = ctx.ReadValue<float>();
        controls.Gameplay.Fire.canceled += ctx => firing = 0f;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void GamepadReload()
    {
        if (PauseMenu.GameIsPaused)
            return;
        StartCoroutine(Reload());
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused)
            return;
        ammoText.SetText(currentAmmo + "/" + maxAmmo);

        if (maxAmmo <= 0)
        {
            Time.timeScale = 0f;
        }

        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if (firing > 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        currentAmmo--;

        FindObjectOfType<AudioManager>().Play("Gunshot");
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                Debug.Log("Hit");
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 3f);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading...");

        FindObjectOfType<AudioManager>().Play("Reload");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        FindObjectOfType<AudioManager>().Stop("Gunshot");
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        isReloading = false;

        currentAmmo = maxAmmo;
    }

}
