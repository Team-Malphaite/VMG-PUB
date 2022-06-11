using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_WEBGL
public class voteCheck : MonoBehaviour
{
    // GameObject obj;
    async public void OnSendContract()
    {
        // smart contract method to call
        string method = "voteChecked";
        // abi in json format
        string abi = "[{\"inputs\":[],\"name\":\"voteChecked\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
        // string abi = "[{ \"inputs\": [{ \"internalType\": \"uint8\", \"name\": \"_myArg\", \"type\": \"uint8\" }], \"name\": \"addTotal\", \"outputs\": [], 	\"stateMutability\": \"nonpayable\", \"type\": \"function\" },{ \"inputs\": [], \"name\": \"countTotal\", \"outputs\": [{\"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\"}],\"stateMutability\": \"view\",\"type\": \"function\"}]";
        // address of contract
        string contract = "0xB960D594fa00f3263311C84058849E1E53394e94";
        // array of arguments for contract
        string args = "[]";
        // value in wei
        string value = "0";
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        // connects to user's browser wallet (metamask) to update contract state
        try {
            string response = await Web3GL.SendContract(method, abi, contract, args, value, gasLimit, gasPrice);
            Debug.Log(response);
            UI_Voting.Instance.votingName.gameObject.SetActive(false);
            UI_Voting.Instance.chooseButton1.gameObject.SetActive(false);
            UI_Voting.Instance.chooseButton2.gameObject.SetActive(false);
            UI_Voting.Instance.chooseButton3.gameObject.SetActive(false);
            UI_Voting.Instance.chooseButton4.gameObject.SetActive(false);
            UI_Voting.Instance.chooseButton5.gameObject.SetActive(false);
            UI_Voting.Instance.sendContract.gameObject.SetActive(false);
     
            
        } catch (Exception e) {
            Debug.LogException(e, this);
        }
    }
}
#endif

 
// // SPDX-License-Identifier: MIT
// pragma solidity ^0.8.7;

// contract voteCheck {

//     function voteChecked() public returns (bool){
//         return true;
//     }
// }

// deploy contract: 0x634bBf63A693856Afd0C63B1949dFaC73FaE0DF8
// abi : 
// [
// 	{
// 		"inputs": [],
// 		"name": "voteChecked",
// 		"outputs": [
// 			{
// 				"internalType": "bool",
// 				"name": "",
// 				"type": "bool"
// 			}
// 		],
// 		"stateMutability": "nonpayable",
// 		"type": "function"
// 	}
// ]
