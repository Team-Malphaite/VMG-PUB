mergeInto(LibraryManager.library, {

    SignInWithGoogle: function (objectName, callback, fallback) {
 
        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);
 
        try {
            var provider = new firebase.auth.GoogleAuthProvider();
            firebase.auth().signInWithPopup(provider).then(function (unused) {
                unityInstance.Module.SendMessage(parsedObjectName, parsedCallback, "Success: signed in with Google!");
            }).catch(function (error) {
                unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
            });
 
        } catch (error) {
            unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
        }
    },
    GetUserAuthDataEmail: function (objectName, callback, fallback) {
 
        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);

        const user = firebase.auth().currentUser;
        if (user !== null) {
        user.providerData.forEach(function (profile) {
                 unityInstance.Module.SendMessage(parsedObjectName, parsedCallback, profile.email);

        });
        }

         
    },
    /////////////Pointer_stringify 자바스크립트 문자열로 바꿔주는 함수
///////////파이어 스토어
        SetDocument: function (collectionPath, documentId, oneValue, twoValue,threeValue,objectName, callback, fallback) {
        var parsedPath = Pointer_stringify(collectionPath);
        var parsedId = Pointer_stringify(documentId);
        var parsedOneValue = Pointer_stringify(oneValue);
        var parsedTwoValue = Pointer_stringify(twoValue);
        var parsedThreeValue = Pointer_stringify(threeValue);


        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);

        var parseToObject={"Email":parsedOneValue,"character": parsedTwoValue , "name": parsedThreeValue }; // 유니티로 받아온 문자를 자바스크립트 객체로 변환
        var parseToJson=JSON.stringify(parseToObject); //자바스크립트 객체를 json 으로 변환하기 위해 문자열로 변환

        try {
            firebase.firestore().collection(parsedPath).doc(parsedId).set(JSON.parse(parseToJson)).then(function() {
                unityInstance.Module.SendMessage(parsedObjectName, parsedCallback, "Success: signed in with Google!");
                //unityInstance.Module.SendMessage(parsedObjectName, parsedCallback, "Success: document " + parsedId + " was set");
            })
                .catch(function(error) {
                    unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
                });

        } catch (error) {
            unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
        }
    },

    GetDocument: function (collectionPath, documentId, objectName, callback,nameCallback, fallback) {
        var parsedPath = UTF8ToString(collectionPath);
        var parsedId = UTF8ToString(documentId);
        var parsedObjectName = UTF8ToString(objectName);
        var parsedCallback = UTF8ToString(callback);
        var parsedNameCallback = UTF8ToString(nameCallback);
        var parsedFallback = UTF8ToString(fallback);

        try {
            firebase.firestore().collection(parsedPath).doc(parsedId).get().then(function (doc) {

                if (doc.exists) {
                    
                    var userBuffer=doc.data();//doc데이터를 내가 지정한 문서로 분해  - 이렇게하는 이유는 파이어베이스에서 리턴을 자신들의 방법으로 하기때문
                    
                   // userBuffer.character - 지정 문서의 밸류 값 character의 value ex. userBuffer.Email - 이메일주소 값이 리턴됨
         
                    unityInstance.Module.SendMessage(parsedObjectName, parsedCallback,JSON.stringify(userBuffer.character));   
                    unityInstance.Module.SendMessage(parsedObjectName, parsedNameCallback,JSON.stringify(userBuffer.name));                    
                 
                    //unityInstance.Module.SendMessage(parsedObjectName, parsedCallback, JSON.stringify(doc.data()));
                } else {
                    unityInstance.Module.SendMessage(parsedObjectName, parsedCallback, "null");
                }
            }).catch(function(error) {
                unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
            });

        } catch (error) {
            unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
        }
    },
     GetDocumentNameCheck: function ( checkName,objectName, callback, fallback) {   //이름 중복체크하는 함수 
         var parsedCheckName = UTF8ToString(checkName);
        var parsedObjectName = UTF8ToString(objectName);
        var parsedCallback = UTF8ToString(callback);
        var parsedFallback = UTF8ToString(fallback);
        var userBuffer =null;

        try {
            firebase.firestore().collection("user").where("name", "==", parsedCheckName).get().then(function (querySnapshot) {
                querySnapshot.forEach(function (doc){//조건에 맞는 데이터 없으면 실행이안됨
                    userBuffer=doc.data();//doc데이터를 내가 지정한 문서로 분해  - 이렇게하는 이유는 파이어베이스에서 리턴을 자신들의 방법으로 하기때문
                    unityInstance.Module.SendMessage(parsedObjectName, parsedCallback,JSON.stringify(userBuffer.character));                    
                   
                });
                if(userBuffer == null){ // 데이터가 없어서 null 상태 그대로임
                    unityInstance.Module.SendMessage(parsedObjectName, parsedCallback,"null");      
                                    console.log(userBuffer);
              
                }
                
            }).catch(function(error) {
                unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
            });

        } catch (error) {
            unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
        }
    },
    ////vote 저장 디비
     SetVoteDocument: function (collectionPath, documentId, oneValue, twoValue,threeValue,fourValue,fiveValue,owner,objectName, callback, fallback) {
        var parsedPath = Pointer_stringify(collectionPath);
        var parsedId = Pointer_stringify(documentId);

        var parsedOneValue = Pointer_stringify(oneValue);
        var parsedTwoValue = Pointer_stringify(twoValue);
        var parsedThreeValue = Pointer_stringify(threeValue);
        var parsedFourValue = Pointer_stringify(fourValue);
        var parsedFiveValue = Pointer_stringify(fiveValue);
        var parsedOwner = Pointer_stringify(owner)


        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);

        var parseToObject={"Subject":parsedId,"vote1": parsedOneValue , "vote2": parsedTwoValue,"vote3": parsedThreeValue,"vote4": parsedFourValue,"vote5": parsedFiveValue ,"owner" : parsedOwner 
        ,"vote1Cnt":0,"vote2Cnt":0,"vote3Cnt":0,"vote4Cnt":0,"vote5Cnt":0}; // 유니티로 받아온 문자를 자바스크립트 객체로 변환
        var parseToJson=JSON.stringify(parseToObject); //자바스크립트 객체를 json 으로 변환하기 위해 문자열로 변환

        try {
            firebase.firestore().collection(parsedPath).doc(parsedId).set(JSON.parse(parseToJson)).then(function() {
                unityInstance.Module.SendMessage(parsedObjectName, parsedCallback, "Success: document " + parsedId + " was set");
            })
                .catch(function(error) {
                    unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
                });

        } catch (error) {
            unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
        }
    },
     GetAllVoteDocument: function ( objectName, callback, fallback) {   
        var parsedObjectName = UTF8ToString(objectName);
        var parsedCallback = UTF8ToString(callback);
        var parsedFallback = UTF8ToString(fallback);
        var userBuffer =null;
        var i=1;        
        try {
            firebase.firestore().collection("vote").get().then(function (querySnapshot) {
                querySnapshot.forEach(function (doc){
                       

                    unityInstance.Module.SendMessage(parsedObjectName, parsedCallback,JSON.stringify(doc.id));                    
                });
                
            }).catch(function(error) {
                unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
            });

        } catch (error) {
            unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
        }
    },

////////////////////////////////////////////////////////////
GetVoteDocument: function ( WantVote,objectName, returnVote1,returnVoteCnt1,returnVote2,returnVote3,returnVote4,returnVote5, returnVoteCnt2,returnVoteCnt3,returnVoteCnt4,returnVoteCnt5) {  
        var parsedObjectName = UTF8ToString(objectName);
        var parsedWantVote = UTF8ToString(WantVote);
        var voteBuffer =null;
        var parsedreturnVote1 = UTF8ToString(returnVote1);
        var parsedreturnVote2 = UTF8ToString(returnVote2);
        var parsedreturnVote3 = UTF8ToString(returnVote3);
        var parsedreturnVote4 = UTF8ToString(returnVote4);
        var parsedreturnVote5 = UTF8ToString(returnVote5);
        var parsedreturnVoteCnt1 = UTF8ToString(returnVoteCnt1);
        var parsedreturnVoteCnt2 = UTF8ToString(returnVoteCnt2);
        var parsedreturnVoteCnt3 = UTF8ToString(returnVoteCnt3);
        var parsedreturnVoteCnt4 = UTF8ToString(returnVoteCnt4);
        var parsedreturnVoteCnt5 = UTF8ToString(returnVoteCnt5);


        try {
            firebase.firestore().collection("vote").where("Subject", "==", parsedWantVote).get().then(function (querySnapshot) {
                querySnapshot.forEach(function (doc){//조건에 맞는 데이터 없으면 실행이안됨
                    voteBuffer=doc.data();//doc데이터를 내가 지정한 문서로 분해  - 이렇게하는 이유는 파이어베이스에서 리턴을 자신들의 방법으로 하기때문

                    unityInstance.Module.SendMessage(parsedObjectName, parsedreturnVote1,JSON.stringify(voteBuffer.vote1));                    
                    unityInstance.Module.SendMessage(parsedObjectName, parsedreturnVote2,JSON.stringify(voteBuffer.vote2));                    
                    unityInstance.Module.SendMessage(parsedObjectName, parsedreturnVote3,JSON.stringify(voteBuffer.vote3));                    
                    unityInstance.Module.SendMessage(parsedObjectName, parsedreturnVote4,JSON.stringify(voteBuffer.vote4));                    
                    unityInstance.Module.SendMessage(parsedObjectName, parsedreturnVote5,JSON.stringify(voteBuffer.vote5));   

                    unityInstance.Module.SendMessage(parsedObjectName, parsedreturnVoteCnt1,JSON.stringify(voteBuffer.vote1Cnt));   
                    unityInstance.Module.SendMessage(parsedObjectName, parsedreturnVoteCnt2,JSON.stringify(voteBuffer.vote2Cnt));   
                    unityInstance.Module.SendMessage(parsedObjectName, parsedreturnVoteCnt3,JSON.stringify(voteBuffer.vote3Cnt));   
                    unityInstance.Module.SendMessage(parsedObjectName, parsedreturnVoteCnt4,JSON.stringify(voteBuffer.vote4Cnt));   
                    unityInstance.Module.SendMessage(parsedObjectName, parsedreturnVoteCnt5,JSON.stringify(voteBuffer.vote5Cnt));   




                   
                });
                if(userBuffer == null){ // 데이터가 없어서 null 상태 그대로임
                    unityInstance.Module.SendMessage(parsedObjectName, parsedCallback,"null");      
                                    console.log(voteBuffer);
              
                }
                
            }).catch(function(error) {
            });

        } catch (error) {
        }
    },
    // vote 1 증가 모듈
    IncrementFieldValue: function ( documentId, field, objectName, callback, fallback) {
        var parsedId = UTF8ToString(documentId);
        var parsedField = UTF8ToString(field);
        var parsedObjectName = UTF8ToString(objectName);
        var parsedCallback = UTF8ToString(callback);
        var parsedFallback = UTF8ToString(fallback);

        try {

            var value = {};
            value[parsedField] = firebase.firestore.FieldValue.increment(1);
            console.log(parsedField)

            firebase.firestore().collection("vote").doc(parsedId).update(value).then(function() {
                unityInstance.Module.SendMessage(parsedObjectName, parsedCallback, "Success: incremented " + parsedField + " by " + increment);
                            console.log("increment Success")

            })
                .catch(function(error) {
                });

        } catch (error) {
        }
    },


 /////////////////// 



    OnAuthStateChanged: function (objectName, onUserSignedIn, onUserSignedOut) {
        var parsedObjectName = UTF8ToString(objectName);
        var parsedOnUserSignedIn = UTF8ToString(onUserSignedIn);
        var parsedOnUserSignedOut = UTF8ToString(onUserSignedOut);

        firebase.auth().onAuthStateChanged(function(user) {
            if (user) {
                unityInstance.Module.SendMessage(parsedObjectName, parsedOnUserSignedIn, JSON.stringify(user));
            } else {
                unityInstance.Module.SendMessage(parsedObjectName, parsedOnUserSignedOut, "User signed out");
            }
        });

    }
});