using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardianBuildManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] Tiles;

    [HideInInspector]
    public GameObject CurrentFocusTile;

    public GameObject GuardianPrefab;

    public Image BuildIconImage;

    void Start()
    {
        Tiles = GameObject.FindGameObjectsWithTag("Tile");
        BuildIconImage.gameObject.SetActive(false);
    }

    private void UpdateBuildImage()
    {
        bool bFocusTile = false;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        foreach (var tile in Tiles)
        {
            if (Vector3.Distance(mousePosition, tile.transform.position) <= 0.5f 
                && !tile.GetComponent<Tile>().CheckIsOwned())
            {
                BuildIconImage.transform.position = tile.transform.position;
                CurrentFocusTile = tile;
                bFocusTile = true;
                break;
            }
        }

        if (bFocusTile)
        {
            BuildIconImage.gameObject.SetActive(true);
        }
        else
        {
            CurrentFocusTile = null;
        }
    }

    private void DeActivateBuildImage()
    {
        BuildIconImage.gameObject.SetActive(false);
        CurrentFocusTile = null;
    }


    void UpdateBuildGuardian()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if (CurrentFocusTile != null)
            {
                Tile tile = CurrentFocusTile.GetComponent<Tile>();
                if (tile?.CheckIsOwned() == false)
                {
                    GameObject guardianInst = Instantiate(GuardianPrefab, CurrentFocusTile.transform);
                    tile.OwnGuardian = guardianInst.GetComponent<Guardian>();
                    DeActivateBuildImage();
                }
            }
        }
    }

    void Update()
    {
        UpdateBuildImage();
        UpdateBuildGuardian();
    }
}