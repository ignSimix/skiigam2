using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Image crossfade;
    [SerializeField] private int NextLevelIndex = 1;
    
    public void RestartLevel()
    {
        StartCoroutine(RestartCoroutine());
    }

    private IEnumerator RestartCoroutine()
    {
        crossfade.CrossFadeAlpha(1, 1f, true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private IEnumerator NextLevelCoroutine()
    {
        crossfade.CrossFadeAlpha(1, 1f, true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        StartCoroutine(NextLevelCoroutine());
    }
    
    public void Quit()
    {
        StartCoroutine(QuitCoroutine());
    }
    
    private IEnumerator QuitCoroutine()
    {
        crossfade.CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(1);
        Application.Quit();
    }


    void Start()
    {
        gameOverMenu.SetActive(false);
        crossfade.CrossFadeAlpha(0, 1f, true);
    }

    private void OnEnable()
    {
        GameEvents.raceEnd += EnableGameOver;
    }
    private void OnDisable()
    {
        GameEvents.raceEnd -= EnableGameOver;
    }

    private void EnableGameOver()
    {
        gameOverMenu.SetActive(true);
    }
}
