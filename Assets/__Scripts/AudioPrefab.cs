using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPrefab : MonoBehaviour
{
    public IEnumerator DestroyPrefab()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
