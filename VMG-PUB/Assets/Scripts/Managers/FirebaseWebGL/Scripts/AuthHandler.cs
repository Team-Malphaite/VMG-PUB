using UnityEngine;
using UnityEngine.UI;
 

    public class AuthHandler : MonoBehaviour
    {
     //   public InputField emailInput;
       // public InputField passwordInput;
 
        public string statusText;
        public  string emailAddress; // 로그인한 사용자의 이메일 정보를 담고있는 변수
        public  string playerDataBuffer;// 데이터를 읽어오면 여기에 저장된다
 
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
        private void GetEmailData(string Infotext)
        {
                emailAddress=Infotext;
        }
        private void ReadPlayerData(string Infotext)
        {
                playerDataBuffer=Infotext;
        }

         //public static AuthHandler Instance; // singleton 변수

 
 
        public void SignInWithGoogle() =>
            FirebaseAuth.SignInWithGoogle(gameObject.name, "DisPlayInfo", "DisplayError");
 
         public void GetUserAuthDataEmail() =>//사용자의 이메일 정보 받아옴 이를 토대로 데이터 베이스 생성시킨다.
            FirebaseAuth.GetUserAuthDataEmail(gameObject.name, "GetEmailData", "DisplayError");
         public void SetDocument() =>  //사용자가 처음 로그인할때 사용자 정보 저장(문서 작성 - 컬렉션도 없을때 ) 
            FirebaseAuth.SetDocument("user",  emailAddress,  emailAddress,"0", gameObject.name,"DisplayInfo", "DisplayError");
         public void GetDocument() =>   //데베 읽기
            FirebaseAuth.GetDocument("user", emailAddress, gameObject.name, "ReadPlayerData",  "DisplayError");


 
       // public void CreateUserWithEmailAndPassword() =>
           ///d FirebaseAuth.CreateUserWithEmailAndPassword(emailInput.text, passwordInput.text, gameObject.name, "DisPlayInfo", "DisplayError");
 
    }
 

 