using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;

namespace MagicCardGame.UI
{
    public class ConnectBtn : MonoBehaviour
    {
        public static void Connect(TMP_InputField inputField)
        {
            var ip = inputField.text;
            if (new Regex(@"^(?!0)(?!.*\.$)((1?\d?\d|25[0-5]|2[0-4]\d)(\.|$)){4}$").IsMatch(ip))
                Debug.Log("Valid IP");
            else
                Debug.Log("Invalid IP");
        }
    }
}