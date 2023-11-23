using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text TextoMonedas;

    private int monedas;

    public static GameManager Instance { get; private set; }

    void Start()
    {
        Instance = this;
        TextoMonedas.text = "x" + string.Format("{0:000}", monedas);
    }

    void Update()
    {
        
    }

    public void ObtenerMoneda()
    {
        monedas += 1;
        TextoMonedas.text = "x" + string.Format("{0:000}", monedas);
    }
}
