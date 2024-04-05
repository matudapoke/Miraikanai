using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deceleration : MonoBehaviour
{
    [SerializeField] float deceleration;
    [SerializeField] bool Abusolute;
    Rigidbody2D Rb;
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Abusolute)
        {
            Rb.velocity = new Vector2(0,0);
        }
        else
        {
            // ï®ëÃÇÃë¨ìxÇèôÅXÇ…å∏è≠Ç≥ÇπÇÈ
            Vector3 velocity = Rb.velocity;
            velocity -= velocity.normalized * deceleration * Time.deltaTime;
            Rb.velocity = velocity;
        }
    }
}
