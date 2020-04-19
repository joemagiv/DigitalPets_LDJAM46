using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamecontroller : MonoBehaviour
{
    private DigitalPet[] digitalPets;

    public GameObject GameoverPanel;

    public Text GameoverText;

    private bool GameActive;

    // Start is called before the first frame update
    void Start()
    {
        digitalPets = FindObjectsOfType<DigitalPet>();
        GameActive = true;
    }

    public void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    private int totalDays;

    private void ActivateGameOverPanel()
    {
        GameoverPanel.SetActive(true);

        
        foreach (DigitalPet digitalPet in digitalPets)
        {
            totalDays += digitalPet.totalHeartbeats;
        }
        GameoverText.text = "Game Over\n" + totalDays + " days";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameActive)
        {
            int alivePets = 0;
            foreach (DigitalPet digitalPet in digitalPets)
            {
                if (digitalPet.timerActive)
                {
                    alivePets++;
                }
            }

            if (alivePets == 0)
            {
                //All Pets are dead
                GameActive = false;
                ActivateGameOverPanel();
            }
        }

    }
}
