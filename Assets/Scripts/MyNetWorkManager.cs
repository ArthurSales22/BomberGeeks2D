using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetWorkManager : NetworkManager
{
    public override void OnStartServer()    
    {
        Debug.Log("Seja muito bem vindo!");
    }
    
    
    public override void OnStopServer()
    {      
        Debug.Log("Encerrando sua conexão....");
    }


    public override void OnClientConnect()
    {
        Debug.Log("Novo player conectado!");
    }

    public override void OnClientDisconnect()
    {
        Debug.Log("Um jogador foi desconectado da partida....");
    }

}

