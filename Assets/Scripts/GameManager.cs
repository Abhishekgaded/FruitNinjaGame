using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    private int score;

    private BLADE blade;
    private Spawner spawner;

    //public Image GameOverPage;
    public Image fadeImage;


    private void Awake()
    {
        blade = FindObjectOfType<BLADE>();
        spawner = FindAnyObjectByType<Spawner>();
    }


    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        ClearScene();
    }


    private void ClearScene()
    {
        Fruits[] fruits = FindObjectsOfType<Fruits>();

        foreach(Fruits fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }


        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }


    public void IncreaseScore(int amount)
    {
        score+= amount;
        scoreText.text = score.ToString();
    }

    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;
        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white,t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        NewGame();
        elapsed = 0f;
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);


            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }


}
