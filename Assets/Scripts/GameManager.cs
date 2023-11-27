using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text TextoMonedas;
    [SerializeField] private GameObject nivel1Panel;
    [SerializeField] private TMP_Text progresoNivel1Text;

    private int monedas;
    public bool primerNivelActivo = false;
    public int personasConvencidas = 0;

    public static GameManager Instance { get; private set; }

    void Start()
    {
        Instance = this;
        TextoMonedas.text = "x" + string.Format("{0:000}", monedas);
    }

    void Update()
    {
        if (primerNivelActivo) {
            nivel1Interfaz();
        }
    }

    private void nivel1Interfaz() {
        nivel1Panel.SetActive(true);
        progresoNivel1Text.text = personasConvencidas.ToString() + "/5";
    }

    public void ObtenerMoneda()
    {
        monedas += 1;
        TextoMonedas.text = "x" + string.Format("{0:000}", monedas);
    }
}
