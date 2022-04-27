using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test_Script : MonoBehaviour
{
    [SerializeField] private Tile tileToSet;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Vector3Int position;

    // Update is called once per frame
    void Update()
    {
        //tilemap.SetTile(position, tileToSet);
    }
}
