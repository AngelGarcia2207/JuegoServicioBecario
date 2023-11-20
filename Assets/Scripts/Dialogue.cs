using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingTime = 0.05f;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    
    void Update()
    {
        if (isPlayerInRange && Input.GetMouseButtonDown(0)) {
            if (!didDialogueStart) {
                StartDialogue();
            }
        }
    }

    private void StartDialogue() {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine() {
        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex]) {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
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
