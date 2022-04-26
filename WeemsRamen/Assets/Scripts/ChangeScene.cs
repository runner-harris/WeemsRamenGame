using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public GameObject directionsPanel;
    public GameObject[] directionsUI;


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
        SceneManager.LoadScene(level);
    }


    public void OpenDirections()
    {
        directionsUI[0].SetActive(true);
    }

    public void CloseDirections()
    {
        directionsUI[0].SetActive(false);
    }

}
