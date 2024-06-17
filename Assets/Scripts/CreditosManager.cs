using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditosManager : MonoBehaviour
{
    [SerializeField] private GameObject creditosPanel;
    [SerializeField] private GameObject[] pantallasCreditos;
    [SerializeField] private GameObject normalButtons;
    [SerializeField] private GameObject titleText;
    [SerializeField] private int pantallaIndex = 0;
    
    public void AvanzarCreditos()
    {
        if (pantallaIndex == 0)
        {
            creditosPanel.SetActive(true);
            normalButtons.SetActive(false);
            titleText.SetActive(false);
        }

        foreach (GameObject pantalla in pantallasCreditos)
        {
            pantalla.SetActive(false);
        }

        if (pantallaIndex < pantallasCreditos.Length)
        {
            pantallasCreditos[pantallaIndex].SetActive(true);
            pantallaIndex++;
        }
        else
        {
            creditosPanel.SetActive(false);
            normalButtons.SetActive(true);
            titleText.SetActive(true);
            pantallaIndex = 0;
        }
    }
}
