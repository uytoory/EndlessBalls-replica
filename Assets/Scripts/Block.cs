using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class Block : MonoBehaviour
{
    [SerializeField] int _health;
    [SerializeField] Text _healthText;

    [SerializeField] List<GameObject> _blockPart = new List<GameObject>();
    
    private void Update()
    {
        if(!Application.isPlaying)
        {
            UpdateHealth();
        }
    }
    private void Start()
    {
        if(Application.isPlaying)
        {
            UpdateHealth();
            FindObjectOfType<BlockManager>().AllBlocks.Add(this);
        }

    }
    public void OnHit()
    {
        _health -= 1;
        if (_health == 0)
        {
            FindObjectOfType<BlockManager>().RemoveBlock(this);
        }
        UpdateHealth();
    }


    void UpdateHealth()
    {
        int numberOfPart = 0;
        for (int i = 0; i < _blockPart.Count; i++)
        {
            if(_health > 20 * i)
            {
                _blockPart[i].SetActive(true);
                numberOfPart++;
            }
            else
            {
                _blockPart[i].SetActive(false);
            }
        }
        for (int i = 0; i < numberOfPart; i++)
        {
            _blockPart[i].transform.localPosition = new Vector3(0f, 0.5f * (numberOfPart - 1 - i), 0f);
        }

        _healthText.text = _health.ToString();
    }
}
