using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightFeats : MonoBehaviour
{
    // Start is called before the first frame update
    private Light luz_main;

    public Music2Color handler;
    void Start()
    {
        luz_main = GetComponent<Light>();

    }

    // Update is called once per frame
    void Update()
    {
        luz_main.intensity = handler.intensidade;
    }
}
