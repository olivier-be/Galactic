using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Nethereum.Unity.Rpc;
using System;
using Nethereum.Web3;
using Nethereum.Contracts;


public class GetLatestBlockCoroutine : 
{
    public string static Url = "https://node.exaion.com/api/v1/${process.env.PROJECT_ID}/rpc";

    // Use this for initialization
    void Start()
    {
        Url = "https://node.exaion.com/api/v1/${process.env.PROJECT_ID}/rpc";
        StartCoroutine(GetBlockNumber());
    }

    public void GetBlockNumberRequest()
    {
        StartCoroutine(GetBlockNumber());
    }

    public IEnumerator GetBlockNumber()
    {

       var blockNumberRequest = new EthBlockNumberUnityRequest(InputUrl.text);
 
       yield return blockNumberRequest.SendRequest();

        if (blockNumberRequest.Exception != null)
        {
            UnityEngine.Debug.Log(blockNumberRequest.Exception.Message);
        }
        else
        {
            ResultBlockNumber.text = blockNumberRequest.Result.Value.ToString();
            UnityEngine.Debug.Log(ResultBlockNumber.text.ToString());

        }
        
        
    }
    
    public IEnumerator Setblock()
    {

        var blockNumberRequest = new EthBlockNumberUnityRequest(InputUrl.text);
 
        yield return blockNumberRequest.SendRequest();

        if (blockNumberRequest.Exception != null)
        {
            UnityEngine.Debug.Log(blockNumberRequest.Exception.Message);
        }
        else
        {
            ResultBlockNumber.text = blockNumberRequest.Result.Value.ToString();
            UnityEngine.Debug.Log(ResultBlockNumber.text.ToString());

        }
        
        
    }
    


    public class HelloWorldContractInteraction
    {
        private const string ContractAddress = "0x..."; // Replace with the actual contract address
        private const string RpcUrl = "https://mainnet.infura.io/v3/your-infura-project-id"; // Replace with your Infura project ID

        public async void SendEchoMessage(string message)
        {
            try
            {
                var web3 = new Web3(RpcUrl);
                var contract = web3.Eth.GetContract(GetContractABI(), ContractAddress);

                var echoFunction = contract.GetFunction("echo");

                // Send the transaction to the contract function 'echo' with the input parameter 'message'
                var transactionInput = echoFunction.CreateTransactionInput(message);

                // Sign and send the transaction
                var transactionHash = await web3.Eth.TransactionManager.SendTransactionAsync(transactionInput);

                // Wait for the transaction to be mined
                var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

                if (receipt != null && receipt.Status == "0x1")
                {
                    Console.WriteLine("Transaction successful. Message sent!");
                }
                else
                {
                    Console.WriteLine("Transaction failed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private string GetContractABI()
        {
            // Replace this with the contract's ABI generated from the Solidity compiler
            return @"[{'constant':false,'inputs':[{'name':'message','type':'string'}],'name':'echo','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'anonymous':false,'inputs':[{'indexed':false,'name':'message','type':'string'}],'name':'Echo','type':'event'}]";
        }
    }

