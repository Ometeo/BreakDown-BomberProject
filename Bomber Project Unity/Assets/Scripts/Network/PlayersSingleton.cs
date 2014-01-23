﻿using UnityEngine;
using System.Collections;
using System;

public class PlayersSingleton
{
    // Inner PlayerInformation Class
    public class PlayerInformation : IEquatable<PlayerInformation>
    {
        public string PlayerName { get; set; }
        public NetworkPlayer NwPlayer { get; set; }
        public int PlayerNumber { get; set; }
        public Transform PlayerTransform { get; set; }
        public int ChampNumber { get; set; }
        public int Team { get; set; }
        public bool IsReady { get; set; }
        public bool IsIA { get; set; }


        public PlayerInformation(string playerName, NetworkPlayer networkPlayer, int playerNumber)
        {
            PlayerName = playerName;
            NwPlayer = networkPlayer;
            PlayerNumber = playerNumber;
            ChampNumber = 0;
            Team = 0;
            IsReady = false;
            IsIA = false;
        }

        public PlayerInformation(string playerName, NetworkPlayer networkPlayer, int playerNumber, int champNumber)
        {
            PlayerName = playerName;
            NwPlayer = networkPlayer;
            PlayerNumber = playerNumber;
            ChampNumber = champNumber;
        }

        public bool Equals(PlayerInformation otherPlayer)
        {
            if (PlayerName == otherPlayer.PlayerName && NwPlayer.externalIP == otherPlayer.NwPlayer.externalIP)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is PlayerInformation))
                throw new InvalidCastException("The object isn't of Type PlayerInformation");
            return Equals(obj as PlayerInformation);
        }

        public override int GetHashCode()
        {
            return (PlayerName + NwPlayer.externalIP).GetHashCode();
        }
    }

    private string _myName;
    public string MyName
    {
        get { return _myName; }
        set { _myName = value; }
    }


    private ArrayList _players;
    public ArrayList Players
    {
        get { return _players; }
        set { _players = value; }
    }

    private static PlayersSingleton _Instance;
    public static PlayersSingleton Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new PlayersSingleton();
            return _Instance;
        }
    }

    private PlayersSingleton()
    {
        Players = new ArrayList();
    }


    #region GetPlayerTransform()
    /*******************************************************************
     *                  GetPlayerTransform Methods
     *******************************************************************/

    public Transform GetPlayerTransform(NetworkPlayer player)
    {
        foreach(PlayerInformation pi in Players)
        {
            if (pi.NwPlayer == player)
                return pi.PlayerTransform;
        }
        return null;
    }

    public Transform GetPlayerTransform(int playerNumber)
    {
        foreach (PlayerInformation pi in Players)
        {
            if (pi.PlayerNumber == playerNumber)
                return pi.PlayerTransform;
        }
        return null;
    }
    #endregion

    #region GetPlayer()
    /*******************************************************************
     *                  GetPlayer Methods
     *******************************************************************/

    public NetworkPlayer GetPlayer(int playerNumber)
    {
        foreach (PlayerInformation pi in Players)
        {
            if (pi.PlayerNumber == playerNumber)
                return pi.NwPlayer;
        }
        return new NetworkPlayer();
    }

    public NetworkPlayer GetPlayer(Transform playerTransform)
    {
        foreach (PlayerInformation pi in Players)
        {
            if (playerTransform == pi.PlayerTransform)
                return pi.NwPlayer;
        }
        return new NetworkPlayer();
    }
    #endregion

    #region GetPlayerInformation()
    /*******************************************************************
     *                  GetPlayerInformation Methods
     *******************************************************************/

    public PlayerInformation GetOrCreatePlayerInformation(String playerName, NetworkPlayer player, ref bool isNewPlayer)
    {
        PlayerInformation pI = new PlayerInformation(playerName, player, Players.Count);
        int indexOf = Players.IndexOf(pI);

        if (indexOf != -1)
        {
            pI = (PlayerInformation)Players[indexOf];
            pI.NwPlayer = player;
        }
        else
        {
            Players.Add(pI);
            isNewPlayer = true;
        }
        GameOptionSingleton.Instance.SetHostRights();
        return pI;
    }

    public PlayerInformation GetPlayerInformation(int playerNumber)
    {
        foreach (PlayerInformation pi in Players)
        {
            if (pi.PlayerNumber == playerNumber)
                return pi;
        }
        return null;
    }

    public PlayerInformation GetPlayerInformation(NetworkPlayer player)
    {
        foreach (PlayerInformation pi in Players)
        {
            if (pi.NwPlayer == player)
                return pi;
        }
        return null;
    }
    #endregion


    /*******************************************************************
     *                      Other Methods
     *******************************************************************/

    public void ChangePlayerNumber(NetworkPlayer player, int playerNumber)
    {
        foreach (PlayerInformation pi in Players)
        {
            if (pi.NwPlayer == player)
            {
                pi.PlayerNumber = playerNumber;
                break;
            }
        }
    }

    public void RemovePlayer(NetworkPlayer player)
    {
        int numPlayerToRemove = -1;
        for (int numPlayer = 0; numPlayer < Players.Count; numPlayer++)
        {
            if (((PlayerInformation)Players[numPlayer]).NwPlayer == player)
            {
                numPlayerToRemove = numPlayer;
                break;
            }
        }
        Players.RemoveAt(numPlayerToRemove);
    }

    public void AddBot(int playerNumber)
    {
        // Move the previous player
        PlayerInformation oldPi = GetPlayerInformation(playerNumber);
        if (oldPi != null)
            oldPi.PlayerNumber = -1;

        // Add the bot
        PlayerInformation pI = new PlayerInformation("Bot " + playerNumber, Network.player, playerNumber);
        pI.IsReady = true;
        pI.IsIA = true;
        Players.Add(pI);
    }

    public void RemoveBot(int playerNumber)
    {
        int numPlayerToRemove = -1;
        for (int numPlayer = 0; numPlayer < Players.Count; numPlayer++)
        {
            PlayerInformation pI = (PlayerInformation)Players[numPlayer];
            if (pI.NwPlayer == Network.player && pI.PlayerNumber == playerNumber)
            {
                numPlayerToRemove = numPlayer;
                break;
            }
        }
        Players.RemoveAt(numPlayerToRemove);
    }

    public bool AllPlayerReady(NetworkPlayer player)
    {
        bool allReady = true;
        foreach (PlayerInformation pi in Players)
        {
            if (pi.NwPlayer == player)
                pi.IsReady = true;
            else
            {
                if (!pi.IsReady)
                    allReady = false;
            }
        }
        return allReady;
    }

    public void SetPlayerChamp(NetworkPlayer player, int champNumber)
    {
        foreach (PlayerInformation pi in Players)
        {
            if (pi.NwPlayer == player)
            {
                pi.ChampNumber = champNumber;
                break;
            }
        }
    }

    public int GetNbPlayer()
    {
        return Players.Count;
    }
}