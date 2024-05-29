using System.Collections;
using TMPro;
using UnityEngine;

public class PrimerPista : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanelPlayer;
    [SerializeField] private TMP_Text dialogueTextPlayer;
    [SerializeField] private float typingTime = 0.05f;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private bool availableToTalk = true;

    private GameObject playerObject;
    private ControladorJugador playerScript;

    void Update()
    {
        if (isPlayerInRange && availableToTalk) {
            if (!didDialogueStart) {
                StartDialogue();
            }
            else if (dialogueTextPlayer.text == dialogueLines[lineIndex] && Input.GetMouseButtonUp(0)) {
                NextDialogueLine();
            }
            else if (Input.GetMouseButtonUp(0)) {
                StopAllCoroutines();
                dialogueTextPlayer.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue() {
        didDialogueStart = true;
        dialoguePanelPlayer.SetActive(true);

        lineIndex = 0;

        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObject.GetComponent<ControladorJugador>();

        playerScript.hablando = true;

        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine() {
        lineIndex++;

        if (lineIndex < dialogueLines.Length) {
            StartCoroutine(ShowLine());
        }
        else {
            GameManager.Instance.pistas.SetActive(true);
            GameManager.Instance.cuervoFase1.SetActive(false);
            GameManager.Instance.cuervoFase2.SetActive(true);
            didDialogueStart = false;
            availableToTalk = false;
            dialoguePanelPlayer.SetActive(false);

            ControladorJugador playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorJugador>();
            playerScript.hablando = false;
        }
    }

    private IEnumerator ShowLine() {
        dialogueTextPlayer.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex]) {
            dialogueTextPlayer.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player") && availableToTalk) {
            isPlayerInRange = true;
        }
    }
}
