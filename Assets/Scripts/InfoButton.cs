using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoButton : MonoBehaviour
{
    public void GoToInfo()
    {
        SceneManager.LoadScene("Info");
    }
}