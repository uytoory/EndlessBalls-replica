using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public Block Block;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Block.OnHit();
    }
}


