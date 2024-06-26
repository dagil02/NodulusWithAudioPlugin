using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Audible : MonoBehaviour, IPointerEnterHandler
{
    public string text = "";
    public GameObject label = null;
    public bool speakOnHover = true;
    public bool speakOnEnable = false;
    public bool speakOnTextChange = false;
    public bool enableSpeakOnFirstFrame = false;
    public bool isAGameobject = false;

    private string textToSpeak = "";
    private GameObject managerGO;
    private AudioPluginManager manager;
    private string lastText = "";
    private bool firstFrameDone;
    private bool alreadySpokeOnHover = false;
    private void Awake()
    {
        textToSpeak = getText();
        managerGO = GameObject.Find("pluginAudio");
        manager = managerGO.GetComponent<AudioPluginManager>();
        if (enableSpeakOnFirstFrame) firstFrameDone = true;
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        if (speakOnEnable && firstFrameDone)
            manager.Speak(textToSpeak, true);
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        textToSpeak = getText();

        if (!firstFrameDone) firstFrameDone = true;

        if (speakOnTextChange) {
           
            if (textToSpeak != lastText)
            {
                manager.Speak(textToSpeak, true);
                lastText = textToSpeak;
            }
        }

        if (speakOnHover && isAGameobject) {
            // Crear un rayo desde la cámara hacia la posición del ratón
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Comprobar si el rayo colisiona con un GameObject
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    if (!alreadySpokeOnHover)
                    {
                        manager.Speak(textToSpeak, true);
                        alreadySpokeOnHover = true;
                    }
                } 
                else alreadySpokeOnHover = false;
            }
            else alreadySpokeOnHover = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (speakOnHover)
            manager.Speak(textToSpeak, true);
    }

    public void OnCursorHover()
    {
        if (speakOnHover)
            manager.Speak(textToSpeak, true);
    }

    private string getText() {

        if (label != null) {

            //Unity UI
            string textFromLabelUI = getTextFromLabelUI(label);

            if (textFromLabelUI != "")
                return textFromLabelUI;

            // TextMesh Pro
            string textFromTMPLabel = getTextFromTMPLabel(label);

            if (textFromTMPLabel != "")
                return textFromTMPLabel;
        }

        return text;
    }

    private string getTextFromLabelUI(GameObject go) {
        Text labelComponent = go.GetComponent<Text>();
        if (labelComponent != null)
            return labelComponent.text;

        return "";
    }

    private string getTextFromTMPLabel(GameObject go) {
        
        Component TMPLabel = go.GetComponent("TMP_Text");
        if (TMPLabel == null)
            return "";

        string fromTMPLabel = "";
        var info = TMPLabel.GetType().GetProperty("text");
        if (info != null)
            fromTMPLabel = info.GetValue(TMPLabel, null) as string;
        return fromTMPLabel;

    }
}
