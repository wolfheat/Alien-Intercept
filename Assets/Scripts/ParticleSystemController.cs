using UnityEngine;


public enum ParticleType{enemyBlowUpA}

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField] private ParticleSystem enemyBlowUpPrefab;
    public static ParticleSystemController Instance { get; private set; }
    private void Start()
    {
        if(Instance == null) Instance = this;
        else Destroy(this);
    }

    public void PlayParticleAt(ParticleType type, Transform source)
    {
        ParticleSystem newParticleSystem = Instantiate(enemyBlowUpPrefab,source.position,source.rotation,transform);
    }

}
