using UnityEngine;
using System.Text;
using System.Runtime.InteropServices;


    public class Speaker : MonoBehaviour
    {
        [DllImport("WindowsTTS")]
        public static extern void initSpeech();

        [DllImport("WindowsTTS")]
        public static extern void destroySpeech();

        [DllImport("WindowsTTS")]
        public static extern void addToSpeechQueue(byte[] s);

        [DllImport("WindowsTTS")]
        public static extern void clearSpeechQueue();

        [DllImport("WindowsTTS")]
        public static extern void statusMessage(StringBuilder str, int length);

        [DllImport("WindowsTTS")]
        public static extern void changeVoice(int vIdx);

        [DllImport("WindowsTTS")]
        public static extern bool isSpeaking();

        public static Speaker theVoice = null;

        void OnEnable()
        {
            if (theVoice == null)
            {
                theVoice = this;
                initSpeech();
            }
        }

        void OnDestroy()
        {
            if (theVoice == this)
            {
                destroySpeech();
                theVoice = null;
            }
        }

        public static void speak(string msg, bool interruptable = false)
        {
            Encoding encoding = System.Text.Encoding.GetEncoding(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ANSICodePage);
            var data = encoding.GetBytes(msg);
            if (interruptable)
                clearSpeechQueue();
            addToSpeechQueue(data);
        }

        private void Awake()
        {

        }

        private void Update()
        {
        }

        private void OnApplicationQuit()
        {
            Speaker.destroySpeech();
        }
    }
