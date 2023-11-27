using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject dialoguePanelPlayer;
    [SerializeField] private TMP_Text dialogueTextPlayer;
    [SerializeField] private float typingTime = 0.05f;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private bool dialogueEnds = false;
    private bool availableToTalk = true;
    private float waitTime = 0.2f;
    private float lastInputTime;

    private GameObject playerObject;
    private ControladorJugador playerScript;

    void Update()
    {
        if (isPlayerInRange && availableToTalk && GameManager.Instance.primerNivelActivo) {
            if (Input.GetMouseButtonUp(0) && Time.time - lastInputTime > waitTime) {
                if (!didDialogueStart) {
                    StartDialogue();
                }
                else {
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[lineIndex];
                }
                lastInputTime = Time.time;
            }

            if (dialogueText.text == dialogueLines[lineIndex]) {
                if (lineIndex == 0) {
                    dialogueTextPlayer.text = dialogueLines[2] + '\n' + '\n' + dialogueLines[1];

                    if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1)) {
                        lineIndex = 4;
                        NextDialogueLine();
                        dialogueTextPlayer.text = string.Empty;
                    }

                    if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2)) {
                        lineIndex = 3;
                        NextDialogueLine();
                        dialoguePanelPlayer.SetActive(false);
                        dialogueText.text = string.Empty;
                        dialogueEnds = true;
                    }
                }

                if (lineIndex == 4) {
                    dialogueTextPlayer.text = dialogueLines[5];

                    if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
                        lineIndex = 6;
                        NextDialogueLine();
                        dialogueTextPlayer.text = string.Empty;
                        dialogueEnds = true;
                        GameManager.Instance.personasConvencidas++;
                    }
                }

                if (Input.GetMouseButtonDown(0) && dialogueEnds == true && Time.time - lastInputTime > waitTime) {
                    NextDialogueLine();
                    lastInputTime = Time.time;

                    if (lineIndex == 6) {
                        availableToTalk = false;
                        dialogueMark.SetActive(false);
                    }
                }
            }
        }
    }

    private void StartDialogue() {
        animator.SetBool("Talking", true);
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialoguePanelPlayer.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;

        GameObject playerObject = GameObject.FindWithTag("Player");
        ControladorJugador playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorJugador>();
        
        Vector3 playerDirection = playerObject.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));

        playerScript.inmovilizado = true;

        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine() {
        if (!dialogueEnds) {
            StartCoroutine(ShowLine());
        }
        else {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialoguePanelPlayer.SetActive(false);
            dialogueMark.SetActive(true);
            animator.SetBool("Talking", false);
            dialogueEnds = false;

            ControladorJugador playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorJugador>();
            playerScript.inmovilizado = false;
        }
    }

    private IEnumerator ShowLine() {
        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex]) {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player") && availableToTalk  && GameManager.Instance.primerNivelActivo) {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        }
    }
}