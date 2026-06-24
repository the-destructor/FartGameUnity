using System;
using UnityEngine;

public class SelectPickupSprite : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SpriteRenderer sr;
    public Sprite[] sprites;
    void Start()
    {
        int index = UnityEngine.Random.Range(0, sprites.Length);
        sr.sprite = sprites[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
