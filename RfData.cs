using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Text;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.ComplexTypes;
using ServiceStack.Script;
using Newtonsoft.Json;
using BackRoomProject.Controllers;
using RfidOpcUa;
using System.ComponentModel;

namespace BackRoomProject
{
    public class RfData
    {
        private string[] scanData { get; set; }
        public string scanDataEPC { get; set; }
        public string timeStamp { get; set; }
        public string antennaAmount { get; set; }
        public string signalStrength { get; set; }
        public string powerLevel { get; set; }


        internal static string[] GetRfData()
        {
            string[] dataArray = Array.Empty<string>();
            Task<string[]> dataTaskArray = CreateAsync();
            try
            {
                dataArray = dataTaskArray.Result;
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e);
            }
            return dataArray;
        }

        public string[] getExtentionObjectArray(string obj)
        {
            obj = obj.Replace("[", "");
            obj = obj.Replace("{RAW:STRING |", "");
            obj = obj.Replace("]", "");
            obj = obj.Replace("{", "");
            obj = obj.Replace("}", "");
            obj = obj.Replace(" ", "");
            obj = obj.Replace("|", "','");
            obj = obj.Insert(0, "'");
            obj = obj += "'";
            Console.WriteLine(obj);
            string[] objArray = new string[] { "" };
            objArray = obj.Split(',');
            return objArray;
        }

        public static async Task<string[]> CreateAsync()
        {
            string ServerAddress = "opc.tcp://192.168.110.58";


            string MyApplicationName = "opc ua";
            var config = new ApplicationConfiguration()
            {
                ApplicationName = MyApplicationName,
                ApplicationUri = Utils.Format(@"urn:{0}:" + MyApplicationName + "", ServerAddress),
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration
                {
                    ApplicationCertificate = new CertificateIdentifier { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\MachineDefault", SubjectName = Utils.Format(@"CN={0}, DC={1}", MyApplicationName, ServerAddress) },
                    TrustedIssuerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\UA Certificate Authorities" },
                    TrustedPeerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\UA Applications" },
                    RejectedCertificateStore = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\RejectedCertificates" },
                    AutoAcceptUntrustedCertificates = true,
                    AddAppCertToTrustedStore = true
                },
                TransportConfigurations = new TransportConfigurationCollection(),
                TransportQuotas = new TransportQuotas { OperationTimeout = 15000 },
                ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 },
                TraceConfiguration = new TraceConfiguration()
            };
            
            config.Validate(ApplicationType.Client).GetAwaiter().GetResult();
            if (config.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            {
                config.CertificateValidator.CertificateValidation += (s, e) => { e.Accept = e.Error.StatusCode == Opc.Ua.StatusCodes.BadCertificateUntrusted; };
            }

            var application = new Opc.Ua.Configuration.ApplicationInstance()
            {
                ApplicationName = MyApplicationName,
                ApplicationType = ApplicationType.Client,
                ApplicationConfiguration = config
            };

            application.CheckApplicationInstanceCertificate(false, 2048).GetAwaiter().GetResult();

            EndpointDescription endpointDescription = CoreClientUtils.SelectEndpoint(config, ServerAddress, true);
            EndpointConfiguration endpointConfiguration = EndpointConfiguration.Create(config);
            ConfiguredEndpoint endpoint = new ConfiguredEndpoint(null, endpointDescription, endpointConfiguration);

            Session session = await Session.Create(
                config,
                endpoint,
                false,
                false,
                config.ApplicationName,
                30 * 60 * 1000,
                new UserIdentity(),
                null
            );

            NodeId scanMethodNode = new NodeId(7010, 4);
            NodeId readPoint1Object = new NodeId(5002, 4);
            
            ExpandedNodeId scanSettings = ExpandedNodeId.Parse("ns=3;i=3010");

            ComplexTypeSystem cts = new ComplexTypeSystem(session);
            Task<Type> scanSettingO = cts.LoadType(scanSettings);
            
            ScanSettings scanSettingsForCall = new ScanSettings
            {
                DataAvailable = true,
                Duration = decimal.ToDouble(1000),
                Cycles = decimal.ToInt32(0)
            };

            RfData rfData = new RfData();

            try
            {
                IList<object> callResult = session.Call(readPoint1Object, scanMethodNode, scanSettingsForCall);

                Console.WriteLine(callResult[0]);

                Console.WriteLine(callResult[1]);


                ExtensionObject[] exObjArr = (ExtensionObject[])callResult[0];
                List<string[]> tagsList = new List<string[]>();
                List<int[]> signalStrengthList = new List<int[]>();

                if ((AutoIdOperationStatusEnumeration)callResult[1] == 0)
                {
                    for (int i = 0; i < exObjArr.Length; i++)
                    {
                        string stringOfExtentionObject = exObjArr[i].ToString();
                        Console.WriteLine("Signal Strength Testing. RFID Tag: " + i + " Data: " + stringOfExtentionObject);
                        string[] resultsArray = rfData.getExtentionObjectArray(stringOfExtentionObject);
                        int signalStrength = Int32.Parse(resultsArray[3].Replace("'", ""));
                        int[] signalStrengthInst = { i, signalStrength };
                        tagsList.Add(resultsArray);
                        signalStrengthList.Add(signalStrengthInst);

                    }

                    int highestSignalStrength = 0;
                    int tagToUse = 0;
                    for (int t = 0; t < signalStrengthList.Count; t++)
                    {
                        
                        int[] tagInst = signalStrengthList[t];
                        int curSignalStrength = tagInst[1];
                        Console.WriteLine("sig strength" + curSignalStrength);

                        if (curSignalStrength > highestSignalStrength)
                        {
                            highestSignalStrength = curSignalStrength;
                            tagToUse = tagInst[0];
                        }

                    }
                    rfData.scanData = tagsList[tagToUse];

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                session.Close();
            }

            Console.WriteLine(rfData.scanData);
            session.Close();
            return rfData.scanData;
        }

    }

}

