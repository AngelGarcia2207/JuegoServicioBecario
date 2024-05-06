using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text TextoMonedas;
    [SerializeField] private GameObject nivel1Panel;
    [SerializeField] private TMP_Text progresoNivel1Text;
    [SerializeField] private GameObject nivel2Panel;
    [SerializeField] private TMP_Text progresoNivel2Text;
    [SerializeField] public GameObject cronometroPanel;
    [SerializeField] private TMP_Text tiempoRestanteText;
    [SerializeField] public GameObject monedasRojasPanel;
    [SerializeField] private TMP_Text TextoMonedasRojas;
    [SerializeField] public GameObject monedasObject;
    [SerializeField] public GameObject gallinaObject;
    [SerializeField] private GameObject hotelChico;
    [SerializeField] private GameObject hotelGrande;

    private int monedas;
    public bool primerNivelActivo = false;
    public int personasConvencidas = 0;
    public bool segundoNivelActivo = false;
    public int segundoNivelpersonasConvencidas = 0;
    private int tiempoParaEntregarPan = 35;
    public int tiempoRestante;
    public bool panEntregado = false;
    public int monedasRojas = 0;
    public bool gallinaCapturada = false;

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

        if (segundoNivelActivo) {
            nivel2Interfaz();
        }

        if (segundoNivelpersonasConvencidas == 3) {
            // Alguna transición que diga "El pavo obtuvo las inversiones necesarias y expandió su hotel... Parece que hay algo en la cima del edificio"

            hotelGrande.SetActive(true);
            hotelChico.SetActive(false);
        }

        if (panEntregado) {
            cronometroPanel.SetActive(false);
        }
    }

    private void nivel1Interfaz() {
        nivel1Panel.SetActive(true);
        progresoNivel1Text.text = personasConvencidas.ToString() + "/5";
    }

    private void nivel2Interfaz() {
        nivel2Panel.SetActive(true);
        progresoNivel2Text.text = segundoNivelpersonasConvencidas.ToString() + "/3";
    }

    public void ObtenerMoneda() {
        monedas += 1;
        TextoMonedas.text = "x" + string.Format("{0:000}", monedas);
    }

    public void ObtenerMonedaRoja() {
        monedasRojas += 1;
        TextoMonedasRojas.text = monedasRojas.ToString() + "/8";
    }

    public IEnumerator tareaEntregarPan() {
        cronometroPanel.SetActive(true);

        tiempoRestante = tiempoParaEntregarPan;
        tiempoRestanteText.text = tiempoRestante.ToString();

        while (tiempoRestante > 0)
        {
            yield return new WaitForSeconds(1f);
            tiempoRestante--;
            tiempoRestanteText.text = tiempoRestante.ToString();
        }

        cronometroPanel.SetActive(false);
    }
}