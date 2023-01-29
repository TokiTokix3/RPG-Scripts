using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public Text nameText;
	public Text dialogueText;

	public Animator animator;

	private Queue<string> names;
	private Queue<string> sentences;

	private UnityEvent endEvent;

	// Use this for initialization
	void Start()
	{
		names = new Queue<string>();
		sentences = new Queue<string>();
	}

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			DisplayNextSentence();
		}
	}
	public void StartDialogue(Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		names.Clear();
		sentences.Clear();

		foreach (string name in dialogue.names)
        {
			names.Enqueue(name);
        }

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		endEvent = dialogue.unityEvent;

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string name = names.Dequeue();
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(name, sentence));
	}

	IEnumerator TypeSentence(string name, string sentence)
	{
		nameText.text = name;
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		GameObject.FindWithTag("Player").GetComponent<MovementController>().enabled = true;
		Debug.Log("Invoking action: " + endEvent.ToString());
		endEvent.Invoke();
	}

}