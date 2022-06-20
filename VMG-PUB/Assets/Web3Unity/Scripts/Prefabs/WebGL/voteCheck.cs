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
        string method = "checkAndTransfer";
            // abi in json format
        string abi = "[{\"inputs\": [] , \"name\" : \"checkAndTransfer\", \"outputs\" : [{\"internalType\":\"bool\", \"name\" : \"\", \"type\" : \"bool\"}] , \"stateMutability\" : \"nonpayable\", \"type\" : \"function\"}, { \"inputs\": [{\"internalType\":\"address\",\"name\" : \"recipient\",\"type\" : \"address\"},{\"internalType\":\"uint256\",\"name\" : \"amount\",\"type\" : \"uint256\"}] ,\"name\" : \"gameWinnerReward\",\"outputs\" : [{\"internalType\":\"bool\",\"name\" : \"\",\"type\" : \"bool\"}] ,\"stateMutability\" : \"nonpayable\",\"type\" : \"function\" }, { \"inputs\": [] ,\"stateMutability\" : \"nonpayable\",\"type\" : \"constructor\" }, { \"inputs\": [] ,\"name\" : \"getBalance\",\"outputs\" : [{\"internalType\":\"uint256\",\"name\" : \"\",\"type\" : \"uint256\"}] ,\"stateMutability\" : \"view\",\"type\" : \"function\" }, { \"inputs\": [] ,\"name\" : \"geterc20address\",\"outputs\" : [{\"internalType\":\"address\",\"name\" : \"\",\"type\" : \"address\"}] ,\"stateMutability\" : \"view\",\"type\" : \"function\" }, { \"inputs\": [] ,\"name\" : \"token\",\"outputs\" : [{\"internalType\":\"contract ERC20\",\"name\" : \"\",\"type\" : \"address\"}] ,\"stateMutability\" : \"view\",\"type\" : \"function\" }]";

        // address of contract
        string contract = "0x0C8a739504D6d827F24B7ED80CB8e32E5229A7e3";
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
