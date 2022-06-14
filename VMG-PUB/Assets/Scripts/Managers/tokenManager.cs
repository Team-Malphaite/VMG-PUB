using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Numerics;
using System.Threading.Tasks;

public class tokenManager : MonoBehaviour
{
    
    public static tokenManager Instance;
    
          
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    async public void voteCheckReward()
        {
            // smart contract method to call
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
    async public void gameReward(string recipient, string amount)
        {
            // smart contract method to call
            string method = "gameWinnerReward";
            // abi in json format
            string abi = "[{\"inputs\": [] , \"name\" : \"checkAndTransfer\", \"outputs\" : [{\"internalType\":\"bool\", \"name\" : \"\", \"type\" : \"bool\"}] , \"stateMutability\" : \"nonpayable\", \"type\" : \"function\"}, { \"inputs\": [{\"internalType\":\"address\",\"name\" : \"recipient\",\"type\" : \"address\"},{\"internalType\":\"uint256\",\"name\" : \"amount\",\"type\" : \"uint256\"}] ,\"name\" : \"gameWinnerReward\",\"outputs\" : [{\"internalType\":\"bool\",\"name\" : \"\",\"type\" : \"bool\"}] ,\"stateMutability\" : \"nonpayable\",\"type\" : \"function\" }, { \"inputs\": [] ,\"stateMutability\" : \"nonpayable\",\"type\" : \"constructor\" }, { \"inputs\": [] ,\"name\" : \"getBalance\",\"outputs\" : [{\"internalType\":\"uint256\",\"name\" : \"\",\"type\" : \"uint256\"}] ,\"stateMutability\" : \"view\",\"type\" : \"function\" }, { \"inputs\": [] ,\"name\" : \"geterc20address\",\"outputs\" : [{\"internalType\":\"address\",\"name\" : \"\",\"type\" : \"address\"}] ,\"stateMutability\" : \"view\",\"type\" : \"function\" }, { \"inputs\": [] ,\"name\" : \"token\",\"outputs\" : [{\"internalType\":\"contract ERC20\",\"name\" : \"\",\"type\" : \"address\"}] ,\"stateMutability\" : \"view\",\"type\" : \"function\" }]";

            // address of contract
            string contract = "0x0C8a739504D6d827F24B7ED80CB8e32E5229A7e3";
            // array of arguments for contract

            string args = "[\"" + recipient + "\" ,\"" + amount + "\"]";
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
            } catch (Exception e) {
                Debug.LogException(e, this);
            }
        }

    async public Task<BigInteger> getBalance(string getAccount)
    {
        string chain = "ethereum";
        string network = "rinkeby";
        string contract = "0x346913c9ba9bAA963B9EcF6D3b7624ad6ccaCfFf";
        string account = getAccount;

        BigInteger balanceOf = await ERC20.BalanceOf(chain, network, contract, account);
        return balanceOf;
    }

    
    async public Task transfer(string getAccount, string amountToken)
    {
        string contract = "0x346913c9ba9bAA963B9EcF6D3b7624ad6ccaCfFf";
        string toAccount = getAccount;
        string amount = amountToken;
        string abi = "[ { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"name_\", \"type\": \"string\" }, { \"internalType\": \"string\", \"name\": \"symbol_\", \"type\": \"string\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"constructor\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"Approval\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"Transfer\", \"type\": \"event\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" } ], \"name\": \"allowance\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"approve\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"balanceOf\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"decimals\", \"outputs\": [ { \"internalType\": \"uint8\", \"name\": \"\", \"type\": \"uint8\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"subtractedValue\", \"type\": \"uint256\" } ], \"name\": \"decreaseAllowance\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"addedValue\", \"type\": \"uint256\" } ], \"name\": \"increaseAllowance\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"name\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"symbol\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"totalSupply\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"recipient\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"transfer\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"recipient\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"transferFrom\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" } ]";
  
        // smart contract method to call
        string method = "transfer";
        // array of arguments for contract
        string[] obj = {toAccount, amount};
        string args = JsonConvert.SerializeObject(obj);
        // value in wei
        string value = "0";
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        // connects to user's browser wallet (metamask) to send a transaction
        try {
            string response = await Web3GL.SendContract(method, abi, contract, args, value, gasLimit, gasPrice);
            Debug.Log(response);
            PopupWindowController.Instance.setNum = 1;
        } catch (Exception e) {
            Debug.LogException(e, this);
            PopupWindowController.Instance.setNum = 2;
        }
    }
}
