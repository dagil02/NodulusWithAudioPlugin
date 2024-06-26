using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public Text textComponent;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateTextSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator UpdateTextSequence()
    {
        yield return new WaitForSeconds(1);
        textComponent.text = "Welcome to Nodulus!";

        yield return new WaitForSeconds(3);
        textComponent.text = "This game is now accessible";

        yield return new WaitForSeconds(4);
        Destroy(textComponent.gameObject);
        Destroy(gameObject);
    }

}
