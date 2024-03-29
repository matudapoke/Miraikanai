using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLayering : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // �I�u�W�F�N�g��Y���W�Ɋ�Â���sortingOrder���X�V���܂�
        spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
    }
}
