using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void LoadByName()
    {
        SceneManager.LoadScene(sceneName);
    }
}
