using UnityEngine;
using UnityEditor;
public class DevDebug : MonoBehaviour
{
    private string framesPerSecond;

    private int lastFrameIndex;
    private float[] frameDeltaTimeArray;

    [SerializeField] private TMPro.TextMeshProUGUI fpsCounter;

    private void Awake()
    {
        frameDeltaTimeArray = new float[50];
    }
    
    private void Update()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.unscaledDeltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
        framesPerSecond = "FPS: " + Mathf.RoundToInt(CalculateAverageFPS()).ToString();
        fpsCounter.text = framesPerSecond;
    }

    private float CalculateAverageFPS()
    {
        float total = 0f;
        foreach(float deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }

        return frameDeltaTimeArray.Length / total;
    }
}
