using System.Collections;
using TMPro;
using UnityEngine;

public class TortugaGallina : MonoBehaviour
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
    private bool dialogueEnds = false;
    private bool availableToTalk = true;
    private bool taskStarted = false;
    private GameObject playerObject;
    private ControladorJugador playerScript;
    private SFXManager playerSFX;
    private Camera mainCamera;
    private CamaraTerceraPersona cameraScript;

    void Start() {

    }

    void Update() {

        if (isPlayerInRange && Input.GetMouseButtonUp(0) && availableToTalk) {
            GameManager.Instance.cronometroPanel.SetActive(false);
            GameManager.Instance.monedasRojasPanel.SetActive(false);
            
            if (GameManager.Instance.gallinaCapturada == true) {
                lineIndex = 6;
            }

            if (!didDialogueStart) {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex]) {
                if (lineIndex == 4) {
                    taskStarted = true;
                    lineIndex++;
                    dialogueEnds = true;
                    NextDialogueLine();
                    GameManager.Instance.gallinaObject.SetActive(true);
                }
                else if (lineIndex == 5) {
                    dialogueEnds = true;
                    NextDialogueLine();
                }
                else if (lineIndex == 6) {
                    dialogueEnds = true;
                    NextDialogueLine();
                    GameManager.Instance.segundoNivelpersonasConvencidas++;
                    availableToTalk = false;
                }
                else {
                    lineIndex++;
                    NextDialogueLine();
                }
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

        animator.SetBool("Talking", true);

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
        if (collision.gameObject.CompareTag("Player") && availableToTalk && GameManager.Instance.segundoNivelActivo) {
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
