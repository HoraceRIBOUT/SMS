using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }
    [Header("GamePart")]
    public Discussion discuss;
    public Spawner spawner;
    public Timer timer;
    public AudioManager audioManager;
    

    public enum gameState
    {
        start,
        message,
        balance,
        pause,
        gameOver,
    }
    public gameState currentState = gameState.start;

    [Header("Quest data")]
    public List<QuestData> questDataFullDeck = new List<QuestData>();
    [Header("Dont touch, computer will do it")]
    public List<QuestData> currentQuestDeck = new List<QuestData>();

    public QuestData currentQuest;
    public int questNumber = 0;

    public List<string> setNameAlreadyUnlock = new List<string>();

    public Color magicMessageColor;
    public Color forceMessageColor;


    public void Start()
    {
        currentState = gameState.start;
    }

    public void TrueStart()
    {
        ReloadDeck();
        //Read first quest
        currentQuest = currentQuestDeck[0];
        currentQuestDeck.RemoveAt(0);

        //Treat current quest
        TreatCurrentQuest();
    }

    public void TreatCurrentQuest()
    {
        questNumber++;
        StartCoroutine(DiplayQuestRequest(currentQuest));
        //wait for end of discussion

    }


    public IEnumerator DiplayQuestRequest(QuestData quest)
    {
        discuss.AddADescription("Vous avez reçu une nouvelle quête.");
        currentState = gameState.message;
        yield return new WaitForSeconds(2f);
        List<QuestData.Message> messageList = quest.messages;
        foreach (QuestData.Message message in messageList)
        {
            bool hero = message.iconeIndex == QuestData.iconeIndex.chevalier;
            if (message.isAPhoto)
                discuss.AddAnImage(message.image, (int)message.iconeIndex, hero);
            else
                discuss.AddAMessage(message.message, (int)message.iconeIndex, hero);


            yield return new WaitForSeconds(message.timingForReadIt);
        }
        Debug.Log("Introduction finish : " + quest.id);
        spawner.SetOpaque();
        spawner.ResumeAllBloc();
        //Quest number ++  too 

        currentState = gameState.balance;
        if (!spawner.imfree)
            spawner.imfree = true;

        spawner.SpawnBloc(questNumber);
        //wait for end of timer
        timer.SetTimer(quest.timerForQuest);
        yield return new WaitForSeconds(quest.timerForQuest);
        //change color 
        spawner.SetTransparent();
        spawner.StopAllBloc();
        currentState = gameState.message;
        //then change brown ?
        bool magic = spawner.GetBalanceResult();
        if (magic)
        {
            discuss.AddADescription("Vous avez décidez d'utilisez la magie !", magicMessageColor);
            yield return new WaitForSeconds(2f);
            StartCoroutine(DisplayQuestResult(quest.messageIfMagic, quest.setUnlockIfMagic));
        }
        else
        {
            discuss.AddADescription("Vous avez décidez d'utilisez la force !", forceMessageColor);
            yield return new WaitForSeconds(2f);
            StartCoroutine(DisplayQuestResult(quest.messageIfForce, quest.setUnlockIfForce));
        }
    }

    public IEnumerator DisplayQuestResult(List<QuestData.Message> messageList, List<QuestSet> sets)
    {
        Debug.Log("Display Quest Result");
        foreach (QuestData.Message message in messageList)
        {
            bool hero = message.iconeIndex == QuestData.iconeIndex.chevalier;
            if (message.isAPhoto)
                discuss.AddAnImage(message.image, (int)message.iconeIndex, hero);
            else
                discuss.AddAMessage(message.message, (int)message.iconeIndex, hero);


            yield return new WaitForSeconds(message.timingForReadIt);
        }
        Debug.Log("Finish displaying result");
        //Unlock ?
        if (sets.Count != 0)
        {
            //unlock those set of deck
            foreach (QuestSet set in sets)
            {
                if (setNameAlreadyUnlock.Contains(set.id))
                    continue;
                discuss.AddADescription("Vous avez débloqué de nouvelles quêtes !");
                audioManager.PlayNewSet();

                foreach (QuestData card in set.allCardToThatSet)
                {
                    questDataFullDeck.Add(card);
                    currentQuestDeck.Add(card);
                }
                foreach(QuestData card in set.cardToRemove)
                {
                    questDataFullDeck.Remove(card);
                }
                yield return new WaitForSeconds(2f);
            }
        }

        //make brown
        //and destroy previous
        spawner.ClearBloc(questNumber);

        //Reach for the next deck !
        NextQuest();

    }

    public void ReloadDeck()
    {
        currentQuestDeck = new List<QuestData>(questDataFullDeck);
    }

    public void NextQuest()
    {
        StopAllCoroutines(); //just to be sure.

        //go check 
        if(currentQuestDeck.Count == 0)
        {
            ReloadDeck();
        }

        //Read a random quest
        int randomQuest = Random.Range(0, currentQuestDeck.Count);
        currentQuest = currentQuestDeck[randomQuest];
        currentQuestDeck.RemoveAt(randomQuest);

        //Treat current quest
        TreatCurrentQuest();

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(currentState == gameState.start && Input.GetMouseButtonDown(0))
        {
            TrueStart();
        }
    }
}
