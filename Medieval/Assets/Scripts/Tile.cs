using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform tile in transform.GetComponentInChildren<Transform>())
        {
            tile.position = new Vector3(tile.position.x, tile.position.y,
            (tile.position.y / 100f) - (tile.GetComponent<SpriteRenderer>().sortingOrder*5));
        }
    }
}