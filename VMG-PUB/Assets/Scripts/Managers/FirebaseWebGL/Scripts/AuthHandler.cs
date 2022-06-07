using System.Collections.Generic;
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

        public string voteSubject=null;
       public string vote1=null;
        public string vote2=null;
        public string vote3=null;
        public string vote4=null;
        public string vote5=null;
        public string voteCnt1=null;
        public string voteCnt2=null;
        public string voteCnt3=null;
        public string voteCnt4=null;
        public string voteCnt5=null;
 
        public List<string> voteSubjectData = new List<string> (); //보트 제목이 들어가는 리스트
        public string wantvote=null;

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
         private void getVoteData(string Infotext)
        {
             voteSubjectData.Add(Infotext);
                

                
        }


/////////////// 투표 를위한 모듈
        private void returnVoteSubject(string Infotext)
        {
            voteSubject = Infotext;
            Debug.Log(voteSubject);

        }
         private void returnVote1(string Infotext)
        {
            vote1 = Infotext;
            Debug.Log(vote1);

        } private void returnVote2(string Infotext)
        {
            vote2 = Infotext;
            Debug.Log(vote2);

        } private void returnVote3(string Infotext)
        {
            vote3 = Infotext;
            Debug.Log(vote3);

        } private void returnVote4(string Infotext)
        {
            vote4 = Infotext;
            Debug.Log(vote4);

        } private void returnVote5(string Infotext)
        {
            vote5 = Infotext;
            Debug.Log(vote5);

        }
        private void returnVoteCnt1(string Infotext)
        {
            voteCnt1 = Infotext;
            Debug.Log(voteCnt1);

        } private void returnVoteCnt2(string Infotext)
        {
            voteCnt2 = Infotext;
            Debug.Log(voteCnt1);

        } private void returnVoteCnt3(string Infotext)
        {
            voteCnt3 = Infotext;
            Debug.Log(voteCnt3);

        } private void returnVoteCnt4(string Infotext)
        {
            voteCnt4 = Infotext;
            Debug.Log(voteCnt4);

        } private void returnVoteCnt5(string Infotext)
        {
            voteCnt5 = Infotext;
            Debug.Log(voteCnt5);

        }

/////////////

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

        public void SetVoteDocument() =>  //보트를 db에 저장하는 코드 
            FirebaseAuth.SetVoteDocument("vote",  voteSubject,  vote1 , vote2 , vote3,vote4,vote5 ,name, gameObject.name,"DisplayInfo", "DisplayError");

        public void GetAllVoteDocument() =>  //보트 1,2,3을 가져오는 코드
            FirebaseAuth.GetAllVoteDocument(gameObject.name,"getVoteData", "DisplayError");

        public void GetVoteDocument() =>   //보트에대한 모든 데이터 가져오는 코드
            FirebaseAuth.GetVoteDocument(wantvote, gameObject.name,  "returnVote1","returnVoteCnt1","returnVote2","returnVote3","returnVote4","returnVote5", "returnVoteCnt2","returnVoteCnt3","returnVoteCnt4","returnVoteCnt5");

 
 
       // public void CreateUserWithEmailAndPassword() =>
           ///d FirebaseAuth.CreateUserWithEmailAndPassword(emailInput.text, passwordInput.text, gameObject.name, "DisPlayInfo", "DisplayError");
 
    }
 

 