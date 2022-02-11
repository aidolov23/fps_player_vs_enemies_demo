using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Text winGameText;
    // Start is called before the first frame update

    private void Awake()
    {
        //enemy bullets ignore enemies
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullets"), LayerMask.NameToLayer("Enemies"));
    }

    void Start()
    {
        FileLogger.WriteString("Game Start");
        instance = this;

    #if !UNITY_EDITOR
            Cursor.lockState = CursorLockMode.Locked;
    #endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 1f;
        }
    }

    public void GameOver()
    {
        if (!winGameText.gameObject.activeSelf)
        {
            FileLogger.WriteString("Game Over");
            gameOverText.gameObject.SetActive(true);
            RestartGame(1.0f);
        }
        
    }

    public void WinGame()
    {
        FileLogger.WriteString("Win Game");
        winGameText.gameObject.SetActive(true);
        RestartGame(3.0f);
    }

    void RestartGame(float seconds)
    {
        StartCoroutine(RestartGameIEnumerator(seconds));
    }

    IEnumerator RestartGameIEnumerator(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
