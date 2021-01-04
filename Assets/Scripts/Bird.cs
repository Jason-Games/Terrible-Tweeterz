using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    Vector2 _startPosition;
    Rigidbody2D body;
    SpriteRenderer spriteRenderer;

    [SerializeField] public int LaunchForce;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        _startPosition = body.position;

        body.isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        spriteRenderer.color = Color.red;
    }
    void OnMouseUp()
    {
        var currentPosition = body.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        body.isKinematic = false;
        body.AddForce(direction * LaunchForce);

        spriteRenderer.color = Color.white;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(DoReset());
        
    }

    IEnumerator DoReset()
    {
        yield return new WaitForSeconds(3);
        body.isKinematic = true;
        body.velocity = Vector2.zero;
        body.position = _startPosition;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }
}
