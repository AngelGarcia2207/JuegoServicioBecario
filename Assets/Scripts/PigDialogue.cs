using System.Collections;
using TMPro;
using UnityEngine;

public class PigDialogue : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingTime = 0.05f;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private bool availableToTalk = true;

    private GameObject playerObject;
    private ControladorJugador playerScript;
    private Camera mainCamera;
    private CamaraTerceraPersona cameraScript;

    void Start() {
        animator.SetBool("Sad", true);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetMouseButtonUp(0) && availableToTalk) {
            if (!didDialogueStart) {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex]) {
                NextDialogueLine();
            }
            else {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue() {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;

        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObject.GetComponent<ControladorJugador>();
        mainCamera = Camera.main;
        cameraScript = mainCamera.GetComponent<CamaraTerceraPersona>();
        
        Vector3 playerDirection = playerObject.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));

        playerScript.inmovilizado = true;
        cameraScript.inmovilizado = true;

        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine() {
        lineIndex++;
        if (lineIndex < dialogueLines.Length) {
            StartCoroutine(ShowLine());
        }
        else {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            animator.SetBool("Talking", false);
            availableToTalk = false;
            GameManager.Instance.primerNivelActivo = true;

            playerScript.inmovilizado = false;
            cameraScript.inmovilizado = false;
        }
    }

    private IEnumerator ShowLine() {
        if (lineIndex > 0) {
            animator.SetBool("Sad", false);
            animator.SetBool("Talking", true);
        }

        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex]) {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player") && availableToTalk) {
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
