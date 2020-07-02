using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    PlayerControls controls;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.WeaponSelectNegative.performed += ctx => SelectNegative();
        controls.Gameplay.WeaponSelectPositive.performed += ctx => SelectPositive();
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused)
            return;
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 1;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 1)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 3;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    void SelectNegative()
    {
        if (PauseMenu.GameIsPaused)
            return;
        if (selectedWeapon <= 1)
            selectedWeapon = transform.childCount - 1;
        else
            selectedWeapon--;
        SelectWeapon();
    }

    void SelectPositive()
    {
        if (PauseMenu.GameIsPaused)
            return;
        if (selectedWeapon >= transform.childCount - 1)
            selectedWeapon = 1;
        else
            selectedWeapon++;
        SelectWeapon();
    }
}
