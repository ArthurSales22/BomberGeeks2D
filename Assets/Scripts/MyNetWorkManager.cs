using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class MyNetWorkManager : NetworkManager
{
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;
    public List<Transform> coinSpawnPoints;
    public int maxCoinsInGame = 2;
    public static int spawnedCoins = 0;



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
    public UnityEvent OnplayerConnect;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        OnplayerConnect.Invoke();

        Transform startPoint;
        Color color;

        if (numPlayers == 0)
        {
            startPoint = player1SpawnPoint;
            color = Color.green;
            InvokeRepeating("SpawnCoin", 2, 2);
        }
        else
        {
            startPoint = player2SpawnPoint;
            //InvokeRepeating("SpawnCoin", 2, 2);
            color = Color.red;
        }

        GameObject new_player =
                Instantiate(playerPrefab, startPoint.position, startPoint.rotation);

        new_player.GetComponent<Player>().playerColor = color;

        NetworkServer.AddPlayerForConnection(conn, new_player);
    }

    public void SpawnCoin()
    {
        if (spawnedCoins < maxCoinsInGame)
        {
            Vector3 local =
                    coinSpawnPoints[Random.Range(0, coinSpawnPoints.Count)].position;

            GameObject new_coin = Instantiate(
                    spawnPrefabs.Find(prefab => prefab.name == "Coin"),
                    local, transform.rotation);

            NetworkServer.Spawn(new_coin);
            spawnedCoins++;
        }
    }




}


