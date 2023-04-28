using UnityEngine;

public class WritingHelp : MonoBehaviour
{
    public void DisplayPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
