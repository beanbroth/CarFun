using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputActionMap gameActionMap;
    private bool isPaused = false;

    public GameObject deathScreen;
    public TextMeshProUGUI deathText;
    public GameObject pauseScreen;

    private void Awake()
    {

        gameActionMap = inputActions.FindActionMap("Game");
        gameActionMap["Pause"].performed += ctx => TogglePause();
        gameActionMap["Reload"].performed += ctx => ReloadScene();
        gameActionMap["MainMenu"].performed += ctx => ReturnToMainMenu();
    }

    private void OnEnable()
    {
        gameActionMap.Enable();
    }

    private void OnDisable()
    {
        gameActionMap.Disable();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pauseScreen.SetActive(true);
        }
        else
        {
            pauseScreen.SetActive(false);
        }

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayerDeath(string deadplayer)
    {
        if (deathScreen == null)
        {
            deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
        }

        deathScreen.gameObject.SetActive(true);

        if (deathText == null)
        {
            deathText = GameObject.FindGameObjectWithTag("DeathText").GetComponent<TextMeshProUGUI>();
        }

        deathText.text = deadplayer + " Died";



        //ReloadScene();
    }
}