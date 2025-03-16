using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeManager : MonoBehaviour
{
    public static bool isClassicMode;

    void Awake()
    {
        isClassicMode = SceneManager.GetActiveScene().name == "ClassicMode";
    }
}
