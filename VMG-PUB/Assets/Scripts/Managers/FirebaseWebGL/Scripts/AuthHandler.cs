using UnityEngine;
using UnityEngine.UI;
 

    public class AuthHandler : MonoBehaviour
    {
     //   public InputField emailInput;
       // public InputField passwordInput;
 
        public static AuthHandler Instance =null; // singleton 변수

        public string statusText=null;
        public  string emailAddress=null; // 로그인한 사용자의 이메일 정보를 담고있는 변수
        public string name=null; // 사용자 닉네임
        public string charcter=null;

        private void Start()
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                DisplayError("Webgl 플랫폼이 아니면 Javascript 기능은 인식되지 않습니다.");
                return;
            }
        }
        private void Awake() { 
            if (Instance == null) 
             { 
            Instance = this; 
           DontDestroyOnLoad(gameObject); 
             } else { 
                 if (Instance != this)
                  Destroy(this.gameObject); 
             } 
         }

 
        private void DisplayError(string errortext)
        {
            statusText = errortext;
            Debug.Log("로그인 체크 결과 값 = "+statusText);
        }
 
        private void DisPlayInfo(string Infotext)
        {
            statusText = Infotext;
            Debug.Log("로그인 체크 결과 값 = "+statusText);

        }
        private void GetEmailData(string Infotext)
        {
                emailAddress=Infotext;
        }
        private void ReadPlayerData(string Infotext)//json 데이터는 "key" : "value" 형태여서 split해줌
        {
                charcter= string.Join("" , Infotext.Split('"'));
        }
        private void ReadPlayerName(string Infotext)
        {
                name= string.Join("" , Infotext.Split('"'));
        }

         //public static AuthHandler Instance; // singleton 변수

 
 
        public void SignInWithGoogle() =>
            FirebaseAuth.SignInWithGoogle(gameObject.name, "DisPlayInfo", "DisplayError");
 
         public void GetUserAuthDataEmail() =>//사용자의 이메일 정보 받아옴 이를 토대로 데이터 베이스 생성시킨다.
            FirebaseAuth.GetUserAuthDataEmail(gameObject.name, "GetEmailData", "DisplayError");
          public void SetDocument() =>  //사용자가 처음 로그인할때 사용자 정보 저장(문서 작성 - 컬렉션도 없을때 ) 
            FirebaseAuth.SetDocument("user",  emailAddress,  emailAddress , charcter , name , gameObject.name,"DisplayInfo", "DisplayError");
          public void GetDocument() =>   //데베 읽기 - 캐릭터 정보 읽어옴
            FirebaseAuth.GetDocument("user", emailAddress, gameObject.name, "ReadPlayerData", "ReadPlayerName", "DisplayError");
       public void GetDocumentNameCheck() =>  
            FirebaseAuth.GetDocumentNameCheck(name , gameObject.name , "DisPlayInfo",  "DisplayError");



 
       // public void CreateUserWithEmailAndPassword() =>
           ///d FirebaseAuth.CreateUserWithEmailAndPassword(emailInput.text, passwordInput.text, gameObject.name, "DisPlayInfo", "DisplayError");
 
    }
 

 