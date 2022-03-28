using System.Runtime.InteropServices;
 

    public static class FirebaseAuth
    {
     


        [DllImport("__Internal")]
        public static extern void SignInWithGoogle(string objectName, string callback, string fallback);




        
    }

