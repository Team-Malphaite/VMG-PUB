using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_WEBGL
public class WebGLSendContractExample : MonoBehaviour
{
    async public void OnSendContract()
    {
        // smart contract method to call
        string method = "countTotal";
        // abi in json format
        string abi = "[{ \"inputs\": [{ \"internalType\": \"uint8\", \"name\": \"_myArg\", \"type\": \"uint8\" }], \"name\": \"addTotal\", \"outputs\": [], 	\"stateMutability\": \"nonpayable\", \"type\": \"function\" },{ \"inputs\": [], \"name\": \"countTotal\", \"outputs\": [{\"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\"}],\"stateMutability\": \"view\",\"type\": \"function\"}]";
        // address of contract
        string contract = "0x1F62Df9FC6E733Cb4781aB58E5aB15688E0c1261";
        // array of arguments for contract
        string args = "[\"1\"]";
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
}
#endif








