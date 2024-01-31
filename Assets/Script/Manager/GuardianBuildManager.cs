using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardianBuildManager : MonoBehaviour
{
    public GameObject[] Tiles;

    public GameObject CurrentFocusTile;
    public GameObject GuardianPrefab;
    public GameObject BuildIconPrefab;
    public float BuildDeltaY = 0f;
    public float FocusTileDistance = 0.05f;

    void Start()
    {
        Tiles = GameObject.FindGameObjectsWithTag("Tile");
        BuildIconPrefab = Instantiate(BuildIconPrefab, transform.position, Quaternion.Euler(90, 0, 0));
        BuildIconPrefab.gameObject.SetActive(false);
    }

    private void UpdateBuildImage()
    {
        bool bFocusTile = false;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        mousePosition.y = 0f;

        foreach (var tile in Tiles)
        {
            Vector3 tilePos = tile.transform.position;
            tilePos.y = 0f;

            if (Vector3.Distance(mousePosition, tilePos) <= FocusTileDistance
                && !tile.GetComponent<Tile>().CheckIsOwned())
            {
                Vector3 position = tile.transform.position;
                position.y += BuildDeltaY;
                BuildIconPrefab.transform.position = position;

                CurrentFocusTile = tile;
                bFocusTile = true;
                break;
            }
        }

        if (bFocusTile)
        {
            BuildIconPrefab.gameObject.SetActive(true);
        }
        else
        {
            DeActivateBuildImage();
        }
    }

    private void DeActivateBuildImage()
    {
        BuildIconPrefab.gameObject.SetActive(false);
        CurrentFocusTile = null;
    }

    // TODO : Click Interface? 

    void UpdateBuildGuardian()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if (CurrentFocusTile != null)
            {
                Tile tile = CurrentFocusTile.GetComponent<Tile>();
                if (tile?.CheckIsOwned() == false)
                {
                    Vector3 position = BuildIconPrefab.transform.position;
                    GameObject guardianInst = Instantiate(GuardianPrefab, position, Quaternion.identity);
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