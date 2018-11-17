using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    // references
    [SerializeField] Player playerPrefab;
    public Transform healthBarParent;

    // variables
    public int maxPlayers = 4;
    public float spawnOffset;

    private Player[] players;

    private void Awake()
    {
        Instance = this;
        this.players = new Player[this.maxPlayers];
    }

    private void Start()
    {
        this.SpawnPlayers();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            this.RespawnPlayers();
        }
    }

    private void SpawnPlayers()
    {
        for (int i = 0; i < this.players.Length; ++i)
        {
            this.players[i] = Instantiate(this.playerPrefab, this.transform);
            this.players[i].playerNumber = i;
        }
        this.RespawnPlayers();
    }

    private void RespawnPlayers()
    {
        for (int i = 0; i < this.players.Length; ++i)
        {
            float angle = 2f * Mathf.PI * i / this.players.Length;
            this.players[i].transform.localPosition = new Vector3(Mathf.Cos(angle) * this.spawnOffset, 2f, Mathf.Sin(angle) * this.spawnOffset);
            this.players[i].playerHealth.ResetDamage();
            this.players[i].GetRigidbody().isKinematic = true;
            this.players[i].GetRigidbody().isKinematic = false;
        }
    }
}
