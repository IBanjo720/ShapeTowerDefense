
using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private EnemySpawner enemySpawner;

    private void Update()
    {
        if (enemySpawner == null || waveText == null)
            return;

        waveText.text = "Wave: " + enemySpawner.CurrentWave;
    }
}
