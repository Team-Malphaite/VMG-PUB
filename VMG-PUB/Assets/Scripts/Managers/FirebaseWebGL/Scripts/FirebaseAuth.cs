using System.Runtime.InteropServices;
 

    public static class FirebaseAuth
    {
        //////////////파이어 Auth///////////////////////

        [DllImport("__Internal")]
        public static extern void SignInWithGoogle(string objectName, string callback, string fallback);

        [DllImport("__Internal")]
        public static extern void GetUserAuthDataEmail(string objectName, string callback, string fallback);
         //////////////파이어 스토어 ///////////////////////
        [DllImport("__Internal")]
        public static extern void SetDocument(string collectionPath, string documentId, string oneValue,string twoValue,string threeValue, string objectName,string callback,string fallback);

        [DllImport("__Internal")]
        public static extern void GetDocument(string collectionPath, string documentId, string objectName, string callback, string nameCallback,string fallback);
       [DllImport("__Internal")]
        public static extern void GetDocumentNameCheck(string parsedCheckName,string objectName, string callback, string fallback);
        [DllImport("__Internal")]
        public static extern void SetVoteDocument(string collectionPath, string documentId, string oneValue,string twoValue,string threeValue,string fourValue,string fiveValue,string owner ,string objectName,string callback,string fallback);
        [DllImport("__Internal")]
        public static extern void GetAllVoteDocument(string objectName,string callback,string fallback);
       [DllImport("__Internal")]
        public static extern void GetVoteDocument(string parsedWantVote, string objectName, string returnVote1,string returnVoteCnt1,string returnVote2,string returnVote3,string returnVote4,string returnVote5,string returnVoteCnt2,
        string returnVoteCnt3,string returnVoteCnt4,string returnVoteCnt5);

       [DllImport("__Internal")]
        public static extern void IncrementFieldValue(string documentId, string field,string votingPerson,string objectName,string callback,string fallback);
      [DllImport("__Internal")]
        public static extern void GetVoteCheckDocument(string WantVote, string name,string objectName,string returnVoteCheck);


        
    }

