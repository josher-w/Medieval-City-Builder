using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    public SpriteRenderer Sprite
    {
        get { return sprite; }
    }

    [SerializeField] private Transform centerVertex;
    [SerializeField] private Transform topRightVertex;

    public bool placedOn;

    private Tile parentTile;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 
            (transform.position.y / 100f) - (sprite.sortingOrder * 5));
    }

    public void Place(Tile _parentTile, Vector3 _pos, int _sortOrder)
    {
        parentTile = _parentTile;
        parentTile.placedOn = true;

        transform.position = _pos;
        sprite.sortingOrder = _sortOrder;
        
        transform.position = new Vector3(transform.position.x, transform.position.y,
            (transform.position.y) - (sprite.sortingOrder));
    }

    public bool CheckPosition(Vector2 posHit)
    {
        if (posHit.y > centerVertex.position.y && !placedOn)
        {
            placedOn = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDestroy()
    {
        if (parentTile != null)
        {
            parentTile.placedOn = false;
        }
    }
}
