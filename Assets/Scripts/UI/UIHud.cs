using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemiesRemainingText;
    [SerializeField] TextMeshProUGUI pointsRemainingText;
    
    public static UIHud Instance;
    // Update is called once per frame

    private void Start()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    public void SetEnemiesRemaining(int points,int enemies)
    {
        pointsRemainingText.text = "Stored Points: " + points;
        enemiesRemainingText.text = "Active Enemies: " + enemies;
    }


}
