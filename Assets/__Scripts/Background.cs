using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Transform _transform;
    float minimum = -7f;
    float maximum = 7f;

    static float t = 0.0f;

    void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        _transform.position = new Vector3(Mathf.Lerp(minimum, maximum, t), 0, 0);

        t += 0.2f*Time.deltaTime;
        
        if(t > 1.0f)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 0.0f;
        }
    }
}
