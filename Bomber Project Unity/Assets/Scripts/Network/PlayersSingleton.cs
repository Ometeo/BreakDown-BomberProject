using UnityEngine;
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
        public bool IsReady { get; set; }


        public PlayerInformation(string playerName, NetworkPlayer networkPlayer, int playerNumber)
        {
            PlayerName = playerName;
            NwPlayer = networkPlayer;
            PlayerNumber = playerNumber;
            ChampNumber = 0;
            IsReady = false;
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

    /*******************************************************************
     *                  GetPlayerInformation Methods
     *******************************************************************/

    public PlayerInformation GetPlayerInformation(String playerName, NetworkPlayer player, ref bool isNewPlayer)
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
