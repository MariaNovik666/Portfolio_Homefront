using UnityEngine;

public class GameEvents : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject craftMenu;
    [SerializeField]
    private GameObject plotPanel;
    [SerializeField]
    private GameObject resourcesPanel;
    [SerializeField]
    private GameObject hpPanel;
    [SerializeField]
    private GameObject exitButton;
    [SerializeField]
    private GameObject winButton;

    [SerializeField]
    private SceneChanger sceneChanger;

    public void GameOver()
    {
        sceneChanger.Pause();
        TurnOffPanels();
        gameOverPanel.SetActive(true);
    }

    public void Pause()
    {
        sceneChanger.Pause();
        TurnOffPanels();
        pausePanel.SetActive(true);
    }
    public void Resume()
    {
        sceneChanger.Resume();
        TurnOffPanels();
        resourcesPanel.SetActive(true);
        hpPanel.SetActive(true);
        exitButton.SetActive(true);
        plotPanel.SetActive(true);
    }

    public void TryWin()
    {
        var playerInvontory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<Inventory>();
        if (playerInvontory.HasPlot())
        {
            sceneChanger.Pause();
            TurnOffPanels();
            winPanel.SetActive(true);
            GameProgressController.GameProgress = null;
        }
    }

    public void GoToHome()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player.position = new Vector2(-112.08f, -5.6f);
    }
    public void GoOutFromHome()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player.position = new Vector2(-7.7f, 7.23f);
    }

    private void TurnOffPanels()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        winButton.SetActive(false);
        pausePanel.SetActive(false);
        craftMenu.SetActive(false);
        plotPanel.SetActive(false);
        resourcesPanel.SetActive(false);
        hpPanel.SetActive(false);
        exitButton.SetActive(false);
    }


}