using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music2Features : MonoBehaviour
{
    public AudioClipSwitcher2 audioClipSwitcher;
    public AudioSource audioSource; // Fonte de áudio a ser analisada
    public int sampleSize = 1024;   // Número de amostras no espectro (padrão 1024)
    public float bassCutoff = 200f; // Limite superior para graves (Hz)
    public float midCutoff = 2000f; // Limite superior para médios (Hz)
    
    private float[] spectrum;       // Array para armazenar o espectro de áudio
    private float sampleRate;       // Taxa de amostragem do áudio

    void Start()
    {
        audioSource = audioClipSwitcher.audioSource;
        if (audioSource == null)
        {
            Debug.LogError("Nenhum AudioSource atribuído!");
            return;
        }

        spectrum = new float[sampleSize];
        sampleRate = AudioSettings.outputSampleRate;
    }

    void Update()
    {
        
        AnalyzeAudio();
       
    }

    void AnalyzeAudio()
    {
        // Obtém o espectro de áudio
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        float totalIntensity = 0f;
        float bassIntensity = 0f;
        float midIntensity = 0f;
        float hiIntensity = 0f;
        float maxMagnitude = 0f;
        int maxIndex = 0;

        for (int i = 0; i < spectrum.Length; i++)
        {
            float frequency = i * sampleRate / (2 * spectrum.Length);
            float magnitude = spectrum[i];
            
            totalIntensity += magnitude;

            // Classifica as intensidades por faixa
            if (frequency <= bassCutoff)
                bassIntensity += magnitude;
            else if (frequency <= midCutoff)
                midIntensity += magnitude;
            else
                hiIntensity += magnitude;

            // Identifica a frequência principal
            if (magnitude > maxMagnitude)
            {
                maxMagnitude = magnitude;
                maxIndex = i;
            }
        }

        float dominantFrequency = maxIndex * sampleRate / (2 * spectrum.Length);

        // Exibe os valores no console
        Debug.Log($"Frequência Principal: {dominantFrequency} Hz");
        Debug.Log($"Intensidade Total: {totalIntensity}");
        Debug.Log($"Grave: {bassIntensity}, Médio: {midIntensity}, Agudo: {hiIntensity}");
    }
}