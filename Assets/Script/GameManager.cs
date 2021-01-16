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
    public Discussion discuss;

    public void Start()
    {
        StartCoroutine(falseMessage());
    }

    public IEnumerator falseMessage()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            discuss.AddAMessage("Bonjour, bienvenue sur Strenght and Magic in Synergy ! La nouvelle application d'homme à tout faire !", 0, true);
            yield return new WaitForSeconds(3f);
            discuss.AddAMessage("En quoi puis-je vous aider ?", 0, true);
            yield return new WaitForSeconds(3f);
            discuss.AddAMessage("Oui, vous pouvez me donner votre avis sur ce dessin ?", 1, false);
            yield return new WaitForSeconds(1f);
            discuss.AddAnImage(0, 1, false);
            yield return new WaitForSeconds(3f);
            discuss.AddAMessage("Alors ?", 1, false);
            yield return new WaitForSeconds(3f);
            discuss.AddAMessage("Il est beau hein ? Je l'ai fait un soir d'automne sous la luminère d'une brume douce et lunaire, lors d'un voyage romanesque en outre-terre, par delà les nuages. C'était en hiver.", 1, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
