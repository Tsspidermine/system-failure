using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    private bool inOptions = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public Animator transition;

    PlayerControls controls;

    public float transitionTime = 1f;

    void Start()
    {
        transition.ResetTrigger("Start");

        Resume();
    }

    void Awake()
    {
        controls = new PlayerControls();

        controls.UI.Pause.performed += ctx => GamepadPause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void OnEnable()
    {
        controls.UI.Enable();
    }

    void OnDisable()
    {
        controls.UI.Disable();
    }

    void GamepadPause()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPaused = false;
        inOptions = false;
        Time.timeScale = 1f;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void Options()
    {
        if (!inOptions)
        {
            pauseMenuUI.SetActive(false);
            optionsMenuUI.SetActive(true);
            inOptions = true;
        }
        else
        {
            pauseMenuUI.SetActive(true);
            optionsMenuUI.SetActive(false);
            inOptions = false;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        StartCoroutine(LoadMenu(SceneManager.GetActiveScene().buildIndex - 1));
    }

    IEnumerator LoadMenu(int levelIndex)
    {
        Time.timeScale = 1f;
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
