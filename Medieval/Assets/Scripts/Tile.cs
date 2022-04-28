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

    public enum Face
    {
        LEFT,
        RIGHT,
        TOP,
        NONE
    }

    public List<Face> unevenFaces = new List<Face>();
    Vector2 poshit;

    private Tile parentTile;
    private Face parentFace;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 
            (transform.position.y / 100f) - (sprite.sortingOrder * 5));
    }

    public void Place(Tile _parentTile, Face _parentFace, Vector3 _pos, int _sortOrder)
    {
        parentTile = _parentTile;
        parentFace = _parentFace;

        transform.position = _pos;
        sprite.sortingOrder = _sortOrder;
        
        transform.position = new Vector3(transform.position.x, transform.position.y,
            (transform.position.y) - (sprite.sortingOrder));
    }

    public Face CheckPosition(Vector2 posHit)
    {
        if (posHit.y < centerVertex.position.y) //definite hit on sides
        {
            //choose btw left and right

            if (posHit.x > centerVertex.position.x) //right hit
            {
                return CheckUneven(Face.RIGHT);
            }
            else //left hit
            {
                return CheckUneven(Face.LEFT);
            }
        }
        else if (posHit.y > topRightVertex.position.y) //definite hit on top
        {
            return CheckUneven(Face.TOP);
        }
        else //in mid ground where y value could determine either top or sides
        {
            //float theta = Mathf.Atan2(topRightVertex.position.y - centerVertex.position.y, topRightVertex.position.x - centerVertex.position.x);

            //float alpha = Mathf.Atan2(posHit.y - centerVertex.position.y, Mathf.Abs(posHit.x - centerVertex.position.x));

            //if (alpha < theta) //left or right
            //{
            //    if (posHit.x > centerVertex.position.x) //right hit
            //    {
            //        return CheckUneven(Face.RIGHT);
            //    }
            //    else //left hit
            //    {
            //        return CheckUneven(Face.LEFT);
            //    }
            //}
            //else
            //{
            //    return CheckUneven(Face.TOP);
            //}

            return CheckUneven(Face.NONE);
        }
    }

    private Face CheckUneven(Face value)
    {
        if (!unevenFaces.Contains(value)) //left hit
        {
            unevenFaces.Add(value);
            return value;
        }
        else
        {
            return Face.NONE;
        }
    }

    private void OnDestroy()
    {
        if (parentTile != null)
        {
            parentTile.unevenFaces.Remove(parentFace);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(poshit, 0.1f);
    }
}
