using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGlow : MonoBehaviour
{
    // subtitle rgb values
    private float r1 = (102.0f/255.0f);
    private float g1 = (51.0f/255.0f);
    private float b1 = (153.0f/255.0f);

    // background rgb values
    private float r2 = (20.0f/255.0f);
    private float g2 = (20.0f/255.0f);
    private float b2 = (20.0f/255.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Transition between colours specified to give a "breathing" effect for the subtitle
        GetComponent<Text>().color = Color.Lerp(new Color(r1, g1, b1), new Color(r2, g2, b2), Mathf.PingPong(Time.time, 1.25f));
        
    }
}
