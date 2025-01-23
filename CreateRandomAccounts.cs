using System;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CrmSdkInegration
{
    public class CreateRandomAccounts
    {
        public readonly CrmServiceClient svc;
        public CreateRandomAccounts()
        {
            svc = Helper.CreateServiceClient(true);
        }

        public void CreateAccountsInBatches(int totalAccounts, int batchSize)
        {
            if (!svc.IsReady) return;

            int createdCount = 0;

            while (createdCount < totalAccounts)
            {
                int accountsToCreate = Math.Min(batchSize, totalAccounts - createdCount);

                ExecuteMultipleRequest executeMultipleRequest = new ExecuteMultipleRequest
                {
                    Requests = new OrganizationRequestCollection(),
                    Settings = new ExecuteMultipleSettings
                    {
                        ContinueOnError = true,
                        ReturnResponses = true
                    }
                };

                for (int i = 0; i < accountsToCreate; i++)
                {
                    Entity account = new Entity("account")
                    {
                        ["name"] = $"Account_{i+1}"
                    };

                    CreateRequest createRequest = new CreateRequest { Target = account };
                    executeMultipleRequest.Requests.Add(createRequest);
                }

                try
                {
                    ExecuteMultipleResponse response = (ExecuteMultipleResponse)svc.Execute(executeMultipleRequest);
                    createdCount += accountsToCreate;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing batch: {ex.Message}");
                }
            }
        }

        public void CreateAccountsInBatchesWithOneTransaction(int totalAccounts, int batchSize)
        {
            if (!svc.IsReady) return;

            int createdCount = 0;

            while (createdCount < totalAccounts)
            {
                int accountsToCreate = Math.Min(batchSize, totalAccounts - createdCount);

                ExecuteTransactionRequest transactionRequest = new ExecuteTransactionRequest()
                {
                    Requests = new OrganizationRequestCollection()
                };

                for (int i = 0; i < accountsToCreate; i++)
                {
                    Entity account = new Entity("account")
                    {
                        ["name"] = $"Account_{i + 1}"
                    };

                    CreateRequest createRequest = new CreateRequest { Target = account };
                    transactionRequest.Requests.Add(createRequest);
                }

                try
                {
                    ExecuteTransactionResponse response = (ExecuteTransactionResponse)svc.Execute(transactionRequest);
                    createdCount += accountsToCreate;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing batch: {ex.Message}");
                }
            }
        }

        public void CreateAccountsOnSingleThread(int totalAccounts)
        {
            if(!svc.IsReady) return;

            try
            {

                for (int i = 0; i < totalAccounts; i++)
                {
                    var account = new Entity("account");
                    account["name"] = $"Account_{i + 1}";
                    this.svc.Create(account);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing batch: {ex.Message}");
            }
        }

        public void CreateAccountsInParallel(int totalAccounts)
        {
            if(!svc.IsReady) return;

            try
            {
                var accounts = new List<Entity>();
                for (int i = 1; i <= totalAccounts; i++)
                {
                    Entity account = new Entity("account");
                    account["name"] = $"Account {i+ 1}";
                    accounts.Add(account);
                }

                Parallel.For(0, accounts.Count, new ParallelOptions { MaxDegreeOfParallelism = 10 }, i =>
                {
                    svc.Create(accounts[i]);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
