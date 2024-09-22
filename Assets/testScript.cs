using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    bool grounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D)) rb.AddForce(new Vector2(2, 0));
        if(Input.GetKey(KeyCode.A)) rb.AddForce(new Vector2(-2, 0));
        if(Input.GetKeyDown(KeyCode.W) && grounded)
        {
            grounded = false;
            rb.AddForce(new Vector2(0, 300));
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        foreach(var contact in other.contacts)
        {
            if(Vector2.Dot(contact.normal, new Vector2(0, 1)) == 1) grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        grounded = false;
    }
}
