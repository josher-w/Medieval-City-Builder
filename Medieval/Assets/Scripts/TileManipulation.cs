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
    RaycastHit2D hit;

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

        hit = Ray(canPlaceOn);

        if (hit)
        {
            Tile hitTile = hit.collider.GetComponent<Tile>();
            
            Tile.Face faceHit;
            
            faceHit = hitTile.CheckPosition(hit.point);

            switch (faceHit)
            {
                case Tile.Face.LEFT:
                    Instantiate(tileToBecome, grid.transform).GetComponent<Tile>().
                        Place(hitTile, Tile.Face.LEFT, hitTile.transform.position - (grid.cellSize / 2f), hitTile.Sprite.sortingOrder);
                    break;

                case Tile.Face.RIGHT:
                    Instantiate(tileToBecome, grid.transform).GetComponent<Tile>().
                        Place(hitTile, Tile.Face.RIGHT, hitTile.transform.position + (new Vector3(grid.cellSize.x, -grid.cellSize.y) / 2f), hitTile.Sprite.sortingOrder);
                    break;

                case Tile.Face.TOP:
                    Instantiate(tileToBecome, grid.transform).GetComponent<Tile>().
                        Place(hitTile, Tile.Face.TOP, hitTile.transform.position + (Vector3.up * grid.cellSize.y * 0.75f),
                        hitTile.Sprite.sortingOrder + 1);
                    break;

                case Tile.Face.NONE:
                    Debug.Log("Cannot place on uneven face");
                    break;

                default:
                    break;
            }
        }
    }

    public void Replace()
    {
        ChooseTile();

        hit = Ray(canReplace);

        if (hit)
        {
            Instantiate(tileToBecome, grid.transform).GetComponent<Tile>().
                Place(null, Tile.Face.NONE, hit.collider.transform.position,
                hit.collider.GetComponent<SpriteRenderer>().sortingOrder);

            Destroy(hit.collider);
        }
    }

    public void Delete()
    {
        hit = Ray(canDelete);

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(hit.point, 0.1f);
    }
}
