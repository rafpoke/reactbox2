using UnityEngine;

public class BeatDetection : MonoBehaviour
{
    public AudioSource audioSource;  
    public float[] spectrum = new float[512];  
    public float threshold = 0.1f;  
    public float beatCooldown = 0.5f;  
    private float lastBeatTime = 0f;  

    public float beatIntensity;  

    void Update()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);

        DetectBeats();

        Debug.Log("Intensidade da Batida: " + beatIntensity);
    }

    void DetectBeats()
    {
        float maxIntensity = 0f;

        for (int i = 3; i < 50; i++)
        {
            if (spectrum[i] > maxIntensity)
            {
                maxIntensity = spectrum[i];
            }
        }

        if (maxIntensity > threshold)
        {
                beatIntensity = maxIntensity;
                lastBeatTime = Time.time;  
                Debug.Log("Batida Detectada! Intensidade: " + beatIntensity);
        }
        
        else
        {
            beatIntensity = 0f;
        }
    }
}
