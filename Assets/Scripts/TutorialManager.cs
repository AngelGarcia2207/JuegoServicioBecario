using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject[] pantallasTutorial;
    [SerializeField] private int pantallaIndex = 0;
    
    public void AvanzarTutorial()
    {
        if (pantallaIndex == 0)
        {
            tutorialPanel.SetActive(true);
        }

        foreach (GameObject pantalla in pantallasTutorial)
        {
            pantalla.SetActive(false);
        }

        if (pantallaIndex < pantallasTutorial.Length)
        {
            pantallasTutorial[pantallaIndex].SetActive(true);
            pantallaIndex++;
        }
        else
        {
            tutorialPanel.SetActive(false);
            pantallaIndex = 0;
        }
    }
}
