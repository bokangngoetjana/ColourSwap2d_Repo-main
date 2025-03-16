using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnStartSidebars()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnStartClick()
    {
        SceneManager.LoadScene("ClassicMode");
    }
    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
