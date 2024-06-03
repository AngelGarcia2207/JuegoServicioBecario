using System.Collections;
using TMPro;
using UnityEngine;

public class CaballoRecibeRemesa : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject eventMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingTime = 0.05f;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private bool dialogueEnds = false;
    private bool availableToTalk = true;
    private bool taskStarted = false;

    private GameObject playerObject;
    private ControladorJugador playerScript;
    private SFXManager playerSFX;
    private Camera mainCamera;
    private CamaraTerceraPersona cameraScript;

    void Update()
    {
        if (isPlayerInRange && availableToTalk && GameManager.Instance.cuartoNivelActivo) {
            if (!didDialogueStart && Input.GetMouseButtonUp(0)) {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex]) {
                if (lineIndex == 2 && Input.GetMouseButtonUp(0)) {
                    dialogueEnds = true;
                    NextDialogueLine();
                    GameManager.Instance.remesaEntregada = true;
                    availableToTalk = false;
                }
                else if (Input.GetMouseButtonUp(0)) {
                    lineIndex++;
                    NextDialogueLine();
                }
            }
            else if (Input.GetMouseButtonUp(0)) {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue() {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);

        if (!taskStarted) {
            lineIndex = 0;
        }

        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObject.GetComponent<ControladorJugador>();
        playerSFX = playerObject.GetComponent<SFXManager>();
        mainCamera = Camera.main;
        cameraScript = mainCamera.GetComponent<CamaraTerceraPersona>();
        
        Vector3 playerDirection = playerObject.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));

        playerScript.hablando = true;
        cameraScript.inmovilizado = true;

        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine() {
        if (!dialogueEnds) {
            StartCoroutine(ShowLine());
        }
        else {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            animator.SetBool("Talking", false);
            dialogueEnds = false;

            ControladorJugador playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorJugador>();
            playerScript.hablando = false;
            cameraScript.inmovilizado = false;
        }
    }

    private IEnumerator ShowLine() {
        playerSFX.PlayTalk();

        if (lineIndex > 0) {
            animator.SetBool("Talking", true);
        }

        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex]) {
            if(!playerSFX.sfxSource.isPlaying)
            {
                playerSFX.PlayTalk();
            }
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }

        playerSFX.sfxSource.Stop();
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player") && availableToTalk && GameManager.Instance.cuartoNivelActivo) {
            GameManager.Instance.respawnpoint = 2;
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            eventMark.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider collision) {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.cuartoNivelActivo) {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
            eventMark.SetActive(false);
        }
    }
}
