using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Color : MonoBehaviour
{
    public ParticleSystem particulas; // Referência ao Particle System
    public Music2Color auxiliar;

    void Start()
    {
        if (particulas == null)
        {
            Debug.LogError("Nenhum Particle System foi atribuído!");
            return;
        }

        // Acessa os parâmetros principais do Particle System
        var mainModule = particulas.main;
        mainModule.startColor = auxiliar.cor_main;

        
    }



void Update()
    {
        var mainModule = particulas.main;
        mainModule.startColor = auxiliar.cor_main;

    }

}
