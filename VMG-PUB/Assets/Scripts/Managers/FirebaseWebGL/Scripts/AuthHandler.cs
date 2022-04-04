using UnityEngine;
using UnityEngine.UI;
 

    public class AuthHandler : MonoBehaviour
    {
     //   public InputField emailInput;
       // public InputField passwordInput;
 
        public string statusText;
 
        private void Start()
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                DisplayError("Webgl 플랫폼이 아니면 Javascript 기능은 인식되지 않습니다.");
                return;
            }
        }

 
        private void DisplayError(string errortext)
        {
            statusText = errortext;
        }
 
        private void DisPlayInfo(string Infotext)
        {
            statusText = Infotext;
        }
         //public static AuthHandler Instance; // singleton 변수

 
 
        public void SignInWithGoogle() =>
            FirebaseAuth.SignInWithGoogle(gameObject.name, "DisPlayInfo", "DisplayError");
 
 
 
       // public void CreateUserWithEmailAndPassword() =>
           ///d FirebaseAuth.CreateUserWithEmailAndPassword(emailInput.text, passwordInput.text, gameObject.name, "DisPlayInfo", "DisplayError");
 
    }
 

 