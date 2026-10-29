using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public GameObject PAUSEMENU_PANEL;

    public void ResumeGame()
    {
        // Unfreeze the game
        Time.timeScale = 1f;

        // Hide the pause menu
        PAUSEMENU_PANEL.SetActive(false);
    }
}