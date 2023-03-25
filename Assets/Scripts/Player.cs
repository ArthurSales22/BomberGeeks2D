using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    Rigidbody2D rb;
    float inputX;
    float inputY;
    public float speed = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isLocalPlayer)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector2(inputX, inputY) * speed;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pedindo uma mensagem para o Server!");
                TalkToServer();
            }
        }       
  
    }
    [Command]
    void TalkToServer()
    {
        Debug.Log("Player pediu uma mensagem!");
    }

}
