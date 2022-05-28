using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    Collider2D boxCollider;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"enter {collision.name}");
        spriteRenderer.color = Color.blue;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"exit {collision.name}");

        IEnumerator test() {
            yield return new WaitForSeconds(1);
            spriteRenderer.color = Color.red;
        }
        StartCoroutine(test());
    }

    // Update is called once per frame
    void Update()
    {
            
    }
}
