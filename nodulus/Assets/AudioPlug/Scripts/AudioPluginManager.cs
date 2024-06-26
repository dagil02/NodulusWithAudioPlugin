using UnityEngine;

public class AudioPluginManager : MonoBehaviour
{
    public bool pluginEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Speak(string text, bool interrupt) {
        if (pluginEnabled && text != "")
            Speaker.speak(text, interrupt);
    }
}
