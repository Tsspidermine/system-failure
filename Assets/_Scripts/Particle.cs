using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Particle : MonoBehaviour
{
    public float despawnTime;

    private VisualEffect visualEffect;

    public Vector3 size;

    void Start()
    {
        visualEffect.SetVector3("Size", size);
        StartCoroutine(ParticleDespawn());
    }

    IEnumerator ParticleDespawn()
    {
        yield return new WaitForSecondsRealtime(despawnTime);
        visualEffect.Stop();
    }
}
