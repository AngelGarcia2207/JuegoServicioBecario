using System.Collections;
using TMPro;
using UnityEngine;

public class HotelierDialogue : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject eventMark;
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
    private SFXManager playerSFX;
    private Camera mainCamera;
    private CamaraTerceraPersona cameraScript;

    void Start() {

    }

    void Update()
    {
        if (isPlayerInRange && availableToTalk) {
            if (Input.GetMouseButtonUp(0) && Time.time - lastInputTime > waitTime) {
                if (!didDialogueStart) {
                    StartDialogue();
                }
                else {
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[lineIndex];
                    playerSFX.sfxSource.Stop();
                }
                lastInputTime = Time.time;
            }

            if (dialogueText.text == dialogueLines[lineIndex]) {
                if (lineIndex == 0) {
                    lineIndex++;
                    NextDialogueLine();
                }
                
                if (lineIndex == 1) {
                    dialogueTextPlayer.text = dialogueLines[3] + '\n' + '\n' + dialogueLines[2];
                    dialoguePanelPlayer.SetActive(true);

                    if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1)) {
                        lineIndex = 5;
                        NextDialogueLine();
                        dialogueTextPlayer.text = string.Empty;
                    }

                    if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2)) {
                        lineIndex = 4;
                        NextDialogueLine();
                        dialoguePanelPlayer.SetActive(false);
                        dialogueText.text = string.Empty;
                        dialogueEnds = true;
                    }
                }

                if (lineIndex == 5) {
                    dialogueTextPlayer.text = dialogueLines[6];

                    if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
                        dialoguePanelPlayer.SetActive(false);
                        lineIndex = 7;
                        NextDialogueLine();
                        dialogueTextPlayer.text = string.Empty;
                        dialogueEnds = true;
                        GameManager.Instance.segundoNivelActivo = true;
                    }
                }

                if (Input.GetMouseButtonDown(0) && dialogueEnds == true && Time.time - lastInputTime > waitTime) {
                    NextDialogueLine();
                    lastInputTime = Time.time;

                    if (lineIndex == 7) {
                        availableToTalk = false;
                        dialogueMark.SetActive(false);
                    }
                }
            }
        }
        else if(!GameManager.Instance.segundoNivelActivo){
            eventMark.SetActive(true);
        }
        else{
            eventMark.SetActive(false);
        }
    }

    private void StartDialogue() {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;

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
            dialoguePanelPlayer.SetActive(false);
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
        if (collision.gameObject.CompareTag("Player") && availableToTalk && !GameManager.Instance.segundoNivelActivo) {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            eventMark.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
            eventMark.SetActive(true);
        }
    }
}
