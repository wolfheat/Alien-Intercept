using System;
using System.Collections.Generic;
using UnityEngine;


public enum ParticleType{BulletSplatter,EnemyBlowUpA}

public class ParticleSystemController : MonoBehaviour
{

    [SerializeField] private ParticleSystem[] particleSystemPrefabs;
    public static ParticleSystemController Instance { get; private set; }

    private List<ParticleSystem>[] particleSystems = new List<ParticleSystem>[2];

    private void Start()
    {
        InitiateArray();

        if(Instance == null) Instance = this;
        else Destroy(this);
    }

    private void InitiateArray()
    {
        for (int i = 0; i < particleSystems.Length; i++) particleSystems[i] = new List<ParticleSystem>();
    }

    public void PlayParticleAt(ParticleType type, Transform source)
    {
        // Add Pooling
        ParticleSystem freeParticleSystem = GetFreeParticleSystemOrCreateNew(type);
        freeParticleSystem.transform.position = source.position;
        freeParticleSystem.Play();
    }

    private ParticleSystem GetFreeParticleSystemOrCreateNew(ParticleType type)
    {
        foreach (ParticleSystem p in particleSystems[(int)type])
        {
            if (!p.gameObject.activeSelf) {  
                p.gameObject.SetActive(true);
                return p; 
            }
        }
        return CreateNewParticleSystem(type);
    }

    private ParticleSystem CreateNewParticleSystem(ParticleType type)
    {
        ParticleSystem newParticleSystem = Instantiate(particleSystemPrefabs[(int)type], transform);
        particleSystems[(int)type].Add(newParticleSystem);
        return newParticleSystem;
	}
}
