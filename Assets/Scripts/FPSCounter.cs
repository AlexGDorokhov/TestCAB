using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{

    [SerializeField] private float updateInterval = 0.2f;
    [SerializeField] private TextMeshProUGUI txt;

    private float time = 0.0f;
    private int frames = 0;

    private void Update()
    {
        time += Time.unscaledDeltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (time >= updateInterval)
        {
            float fps = (int)(frames / time);
            time = 0.0f;
            frames = 0;

            txt.text = fps.ToString();
        }
    }
    
}

