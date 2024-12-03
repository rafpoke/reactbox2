using UnityEngine;

public class FlowerParticleController : MonoBehaviour
{
    public AudioAnalysis audioAnalysis; // Referência ao script de análise de áudio
    public int[] frequencyBands; // Bandas de frequência associadas a esta flor (ex.: [0,1] para vermelho)
    public float amplitudeThreshold = 0.1f; // Limite para ativar as partículas
    public ParticleSystem flowerParticleSystem; // Sistema de partículas da flor

    void Update()
    {
        float amplitudeSum = 0f;

        // Soma as amplitudes das bandas de frequência associadas
        foreach (int band in frequencyBands)
        {
            amplitudeSum += audioAnalysis.GetBandAmplitude(band);
        }

        // Verifica se a amplitude média das bandas supera o limite
        if (amplitudeSum / frequencyBands.Length > amplitudeThreshold)
        {
            // Ativa o sistema de partículas se ainda não estiver ativo
            if (!flowerParticleSystem.isPlaying)
                flowerParticleSystem.Play();
        }
        else
        {
            // Desativa o sistema de partículas se estiver ativo
            if (flowerParticleSystem.isPlaying)
                flowerParticleSystem.Stop();
        }
    }
}
