using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public List<Block> AllBlocks = new List<Block>();
    public GameManager GameManager;

    public void MoveDown()
    {
        for (int i = 0; i < AllBlocks.Count; i++)
        {
            AllBlocks[i].transform.position +=  new Vector3(0,-1f,0f);
            if(AllBlocks[i].transform.position.y <= 0)
            {
                GameManager.ShowLoseWindow();
            }
            
        }
    }

    public void RemoveBlock(Block block)
    {
        AllBlocks.Remove(block);
        Destroy(block.gameObject);
        if(AllBlocks.Count == 0)
        {
            GameManager.ShowWinWindow();
        }
    }
}

