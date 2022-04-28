using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileManipulation : MonoBehaviour
{
    [SerializeField] private LayerMask canPlaceOn;
    [SerializeField] private LayerMask canReplace;
    [SerializeField] private LayerMask canDelete;
    [SerializeField] private GameObject tileToBecome;
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private Grid grid;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();

        controls.Player.Place.performed += _ => Place();
        controls.Player.Replace.performed += _ => Replace();
    }

    private void OnEnable()
    {
        controls.Player.Place.Enable();
        controls.Player.Replace.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Place.Disable();
        controls.Player.Replace.Disable();
    }

    public void Place()
    {
        ChooseTile();

        RaycastHit2D hit = Ray(canPlaceOn);

        if (hit)
        {
            Tile hitTile = hit.collider.GetComponent<Tile>();
            
            if (hitTile.CheckPosition(hit.point))
            {
                Instantiate(tileToBecome, grid.transform).GetComponent<Tile>().
                        Place(hitTile, hitTile.transform.position + (0.75f * grid.cellSize.y * Vector3.up), hitTile.Sprite.sortingOrder + 1);
            }
        }
    }

    public void Replace()
    {
        ChooseTile();

        RaycastHit2D hit = Ray(canReplace);

        if (hit)
        {
            Instantiate(tileToBecome, grid.transform).GetComponent<Tile>().
                Place(null, hit.collider.transform.position, hit.collider.GetComponent<SpriteRenderer>().sortingOrder);

            Destroy(hit.collider);
        }
    }

    public void Delete()
    {
        RaycastHit2D hit = Ray(canDelete);

        if (hit)
        {
            Destroy(hit.collider);
        }
    }

    private RaycastHit2D Ray(LayerMask layer)
    {
        Vector2 startPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        return Physics2D.Raycast(startPos, Vector2.zero, 100f, layer);
    }

    private void ChooseTile()
    {
        tileToBecome = tiles[Random.Range(0, tiles.Length - 1)];
    }
}
