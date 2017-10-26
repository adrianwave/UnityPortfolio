using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {
	public static DialogueSystem Instance{get; set;}
	public GameObject dialoguePanel;
	public string npcName;
	public List<string> dialogueLines = new List<string>();

	Button continueButton;
	Text dialogueText, nameText;
	int dialogueIndex;

	// Use this for initialization
	void Awake () {
		continueButton = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
		dialogueText = dialoguePanel.transform.Find("Text").GetComponent<Text>();
		nameText = dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<Text>();
		continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
		dialoguePanel.SetActive(false);

		if(Instance != null && Instance != this) {
			Destroy(gameObject);
		} else {
			Instance = this;
		}
	}

	public void AddNewDialogue(string[] lines, string npcName) {
		dialogueIndex = 0;
		dialogueLines = new List<string>();
		dialogueLines.AddRange(lines);
		this.npcName = npcName;
		Debug.Log(dialogueLines.Count);
		createDialogue();
	}

	public void createDialogue()
	{
		dialogueText.text = dialogueLines[dialogueIndex];
		nameText.text = npcName;
		dialoguePanel.SetActive(true);
	}

	public void ContinueDialogue()
	{
		if(dialogueIndex < dialogueLines.Count - 1) {

			dialogueIndex++;
			dialogueText.text = dialogueLines[dialogueIndex];

		} else {

			dialoguePanel.SetActive(false);

		}
	}
}
