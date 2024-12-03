using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightFeats : MonoBehaviour
{
    // Start is called before the first frame update
    private Light luz_main;
    public float escala = 0.0f;

    public Music2Color handler;
    void Start()
    {
        luz_main = GetComponent<Light>();

    }

    // Update is called once per frame
    void Update()
    {   
        if (handler.intensidade > 6)
        {
            luz_main.intensity = 6;
        }
        else {
            luz_main.intensity = 1;
        }
        
    }
}
