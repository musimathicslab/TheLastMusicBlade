using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private DialogueSequence Dialogue;

    public GameObject image;
    public TextMeshProUGUI name;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    private int lineIndex;
    private float textspeed;
    private Coroutine textAnimationCoroutine;




    public Sprite characterSprite;
    public Sprite guideSprite;
    public Sprite enemySprite;

    public string characterName;
    public string guideName;
    public string enemyName;


    private Dictionary<string, Sprite> nameToSprite = new Dictionary<string, Sprite>();


    private void Awake()
    {
        nameToSprite.Add(characterName, characterSprite);
        nameToSprite.Add(enemyName, enemySprite);
        nameToSprite.Add(guideName, guideSprite);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        dialogueBox.SetActive(false);
        textspeed = 0.1f;
        StartDialogue();
        Debug.Log("STARTING DIALOGUE");


        
    }

    private void Update()
    {

       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }

    public void StartDialogue()
    {
        lineIndex = 0;
        DisplayLine();
        dialogueBox.SetActive(true);
    }

    public void DisplayLine()
    {
        if (lineIndex >= Dialogue.lines.Length)
        {
            EndDialogue();
            return;
        }
        //retrieve Dialogue Line
        DialogueLine line = Dialogue.lines[lineIndex];
        name.text = line.characterName;
        image.GetComponent<Image>().sprite= nameToSprite[line.characterName];

        //check if the coroutine is in progress
        if (textAnimationCoroutine != null)
            StopCoroutine(textAnimationCoroutine);


        
        
        textAnimationCoroutine = StartCoroutine(TypeSentence(line.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
      
        dialogueText.text = "";
        Debug.Log(sentence);
        foreach (char letter in sentence)
        {
            Debug.Log(letter);
            dialogueText.text += letter;
            yield return new WaitForSeconds(textspeed);
        }
        textAnimationCoroutine = null;
    }

    public void NextLine()
    {
        if (textAnimationCoroutine != null)
        {
            StopCoroutine(textAnimationCoroutine);
            dialogueText.text = Dialogue.lines[lineIndex].sentence;
            textAnimationCoroutine = null;
            return;
        }

        lineIndex++;
        DisplayLine();
    }

    private void EndDialogue()
    {
        Debug.Log("Dialogo terminato.");
       
    }







}
