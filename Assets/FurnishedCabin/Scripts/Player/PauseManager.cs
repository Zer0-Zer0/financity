using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;

    public Slider Slider;
    public Button resumeButton;
    public Button quitButton;

    public PostProcessVolume postProcessVolume;
    private DepthOfField depthOfFieldLayer;

    public GameObject goalobjetive;

    public CameraController sensidacamera;

    private float sensi;
    void Start()
    {
        pauseMenu.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
        postProcessVolume.profile.TryGetSettings(out depthOfFieldLayer);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            isPaused = !isPaused;

            if (isPaused)
            {
                Cursor.visible = true;
                Time.timeScale = 0.01f;
                pauseMenu.SetActive(true);
                goalobjetive.SetActive(false);
                
                SetFocusDistance(0f);
            }
            else 
            {
                Cursor.visible = false;
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                goalobjetive.SetActive(true);
                
                SetFocusDistance(2.5f);
            }
        }
    }


    void SetFocusDistance(float distance)
    {
        depthOfFieldLayer.focusDistance.value = distance;
    }

    void ResumeGame()
    {
        Cursor.visible = false;
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        goalobjetive.SetActive(true);
        
        SetFocusDistance(2.5f);
    }

    void QuitGame()
    {
        SceneManager.LoadScene("menu");
    }

    public void sensiUpdate()
    {
        sensi = Slider.value;
        sensidacamera.mudarsensi(sensi);
    }
}
