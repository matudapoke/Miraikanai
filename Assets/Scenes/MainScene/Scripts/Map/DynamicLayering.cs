using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLayering : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] int PlusSortingOrder;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // オブジェクトのY座標に基づいてsortingOrderを更新します
        spriteRenderer.sortingOrder = (int)(transform.position.y * -100 + PlusSortingOrder);
    }
}
