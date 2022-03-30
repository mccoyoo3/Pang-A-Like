using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("LEVEL MANAGER")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public Transform respawnPoint;
    public GameObject player;
    public GameObject Rplayer { get; set; }
    private float respawnTimeStart;
    private bool respawn;
    private float respawnTime = 2f;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Scoring.Instance.IncreaseLives(3);
    }

    void Update()
    {
        scoreText.text = Scoring.Instance.Score.ToString();
        livesText.text = Scoring.Instance.Lives.ToString();
        CheckRespawn();
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    private void CheckRespawn()
    {
        if (Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            StartCoroutine(Spawnplayer());
            respawn = false;
        }
    }

    private IEnumerator Spawnplayer()
    {
        Rplayer = Instantiate(player, respawnPoint.position, respawnPoint.transform.rotation);
        yield return new WaitForEndOfFrame();
        Rplayer.transform.parent = null;
        Rplayer.name = player.name;
        yield return new WaitForSeconds(10);
    }
}
