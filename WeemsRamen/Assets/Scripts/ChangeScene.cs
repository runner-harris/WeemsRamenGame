using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public GameObject panel;
    public GameObject[] panelUI;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TransitionScene(int level)

    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }


    public void OpenDirections()
    {
        panelUI[0].SetActive(true);
    }

    public void CloseDirections()
    {
        panelUI[0].SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        panelUI[0].SetActive(true);
        panelUI[1].SetActive(false);
    }
    public void Play()
    {
        Time.timeScale = 1;
        panelUI[0].SetActive(false);
        panelUI[1].SetActive(true);
    }

}
