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
 

        
    }

