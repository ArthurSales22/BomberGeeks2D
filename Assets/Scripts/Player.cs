using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;
using System;




public class Player : NetworkBehaviour
{
    Rigidbody2D rb;
    float inputX;
    float inputY;
    public float speed = 3;

    [SyncVar]
    public int coins;
    public Color playerColor;

    [Serializable]
    public class IntEvent : UnityEvent<int> { }

    public IntEvent OnCoinCollect;




    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        GetComponent<SpriteRenderer>().color = playerColor;

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
    [Server]
    public void AddCoins()
    {
        coins += 1;
        OnCoinCollect.Invoke(coins);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            AddCoins();
            MyNetWorkManager.spawnedCoins--;
            Destroy(collision.gameObject);
            Debug.Log("pegou");
            
        }
    }


}
