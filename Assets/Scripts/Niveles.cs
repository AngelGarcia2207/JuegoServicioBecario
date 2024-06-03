using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niveles : MonoBehaviour
{
    [SerializeField] private GameObject levelButtons;
    [SerializeField] private GameObject normalButtons;

    public void LevelsOn()
    {
        levelButtons.SetActive(true);
        normalButtons.SetActive(false);
    }

    public void LevelsOff()
    {
        levelButtons.SetActive(false);
        normalButtons.SetActive(true);
    }
}
