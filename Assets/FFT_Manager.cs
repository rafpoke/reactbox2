using UnityEngine;

public class AudioAnalysis : MonoBehaviour
{
    public AudioSource audioSource; // Fonte de áudio para análise
    public int sampleSize = 512; // Tamanho do buffer para FFT (ex.: 512 ou 1024)
    public FFTWindow fftWindow = FFTWindow.Blackman; // Janela FFT para suavização

    private float[] spectrum; // Array de espectro de frequências
    private float[] bands; // Bandas de frequências específicas (ex.: graves, médios, agudos)

    void Start()
    {
        spectrum = new float[sampleSize];
        bands = new float[8]; // Dividimos o espectro em 8 bandas (ou mais, se necessário)
    }

    void Update()
    {
        AnalyzeAudio();
    }

    void AnalyzeAudio()
    {
        // Obtém o espectro de frequências do áudio
        audioSource.GetSpectrumData(spectrum, 0, fftWindow);

        // Calcula bandas específicas (graves, médios, agudos)
        CreateFrequencyBands();
    }

    void CreateFrequencyBands()
    {
        // Exemplo de bandas (ajustáveis para mais ou menos bandas)
        int[] bandRanges = { 2, 4, 8, 16, 32, 64, 128, 256 };
        int currentIndex = 0;

        for (int i = 0; i < bands.Length; i++)
        {
            float bandSum = 0f;

            for (int j = 0; j < bandRanges[i] && currentIndex < spectrum.Length; j++)
            {
                bandSum += spectrum[currentIndex];
                currentIndex++;
            }

            bands[i] = bandSum / bandRanges[i]; // Média da banda
        }
    }

    public float GetBandAmplitude(int bandIndex)
    {
        if (bandIndex >= 0 && bandIndex < bands.Length)
        {
            return bands[bandIndex];
        }
        return 0f;
    }
}
