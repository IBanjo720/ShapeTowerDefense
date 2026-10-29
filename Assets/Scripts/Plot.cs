
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        // Don't build if a tower already exists
        if (tower != null)
            return;

        // Get the selected tower
        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild == null || towerToBuild.prefab == null)
        {
            Debug.LogError("No tower or tower prefab selected!");
            return;
        }

        // Check and spend currency
        if (LevelManager.main.SpendCurrency(towerToBuild.cost))
        {
            // Build the tower only if payment succeeds
            tower = Instantiate(
                towerToBuild.prefab,
                transform.position,
                Quaternion.identity
            );
        }
    }
}
