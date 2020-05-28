using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using QuickFix;
using QuickFix.Fields;
using QuickFix.Transport;

namespace FixApi_Tester
{
    class OneZero_Price_FixApi_Connector : MessageCracker, IApplication
    {
        public event FixApiConnectEvent FixApiConnectEventListener;
        public event FixApiDisconnectEvent FixApiDisconnectEventListener;
        public event FixApiQuoteEvent FixApiQuoteEventListener;

        private static OneZero_Price_FixApi_Connector m_staticFixApiManager = null;
        private SessionSettings settings;
        private SocketInitiator initiator;

        private SessionID m_SessionID;
        private Dictionary<string, MTQuote> m_dicSymbolQuote = new Dictionary<string, MTQuote>();
        private bool m_bConnected = false;
        private int m_nQuoteSequenceNumber = 0;


        public static OneZero_Price_FixApi_Connector Instance()
        {
            if (m_staticFixApiManager == null)
                m_staticFixApiManager = new OneZero_Price_FixApi_Connector();
            return m_staticFixApiManager;
        }
        public OneZero_Price_FixApi_Connector()
        {
            string strAppDirPath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            if (!Directory.Exists(strAppDirPath + "/Settings"))
                Directory.CreateDirectory(strAppDirPath + "/Settings");
        }
        public bool StartSession(string strHostName, int nPort, string strSenderCompID, string strTargetCompID, string strPassword)
        {
            try
            {
                if (!IsPortOpen(strHostName, nPort, new TimeSpan(0, 0, 10)))
                    return false;

                string strAppDirPath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);

                QuickFix.Dictionary dicConfig = new QuickFix.Dictionary();
                dicConfig.SetString("ConnectionType", "initiator");
                dicConfig.SetDay("StartDay", DayOfWeek.Sunday);
                dicConfig.SetString("StartTime", "00:00:00");
                dicConfig.SetDay("EndDay", DayOfWeek.Saturday);
                dicConfig.SetString("EndTime", "00:00:00");
                dicConfig.SetDouble("HeartBtInt", 20);
                dicConfig.SetDouble("ReconnectInterval", 10);
                dicConfig.SetBool("ResetOnLogout", true);
                dicConfig.SetBool("ResetOnLogon", true);
                dicConfig.SetBool("ResetOnDisconnect", true);
                dicConfig.SetBool("ResetSeqNumFlag", true);
                dicConfig.SetDouble("EncryptMethod", 0);
                dicConfig.SetBool("CheckLatency", false);
                dicConfig.SetString("FileStorePath", strAppDirPath + "/Store/Ctrader_Price");
                dicConfig.SetString("FileLogPath", strAppDirPath + "/Log");
                dicConfig.SetBool("UseDataDictionary", true);
                dicConfig.SetString("DataDictionary", strAppDirPath + "/Settings/FIX44-OneZero.xml");
                dicConfig.SetBool("ScreenLogShowIncoming", false);
                dicConfig.SetBool("ScreenLogShowOutgoing", false);
                dicConfig.SetString("Password", strPassword);

                SessionID quoteSessionID = new SessionID("FIX.4.4", strSenderCompID, strTargetCompID);
                QuickFix.Dictionary dicQuoteSession = new QuickFix.Dictionary();
                dicQuoteSession.SetString("SocketConnectHost", strHostName);
                dicQuoteSession.SetDouble("SocketConnectPort", nPort);

                settings = new SessionSettings();
                settings.Set(dicConfig);
                settings.Set(quoteSessionID, dicQuoteSession);

                FileStoreFactory storeFactory = new FileStoreFactory(settings);
                ScreenLogFactory logFactory = new ScreenLogFactory(settings);
                initiator = new SocketInitiator(this, storeFactory, settings, logFactory);
                initiator.Start();
                return true;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public void EndSession()
        {
            foreach (SessionID sessionId in initiator.GetSessionIDs())
            {
                Session.LookupSession(sessionId).Logout("User Requested");
            }
            initiator.Stop();
        }
        private bool IsPortOpen(string host, int port, TimeSpan timeout)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(host, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(timeout);
                    if (!success)
                    {
                        return false;
                    }

                    client.EndConnect(result);
                }

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool IsConnected()
        {
            return m_bConnected;
        }

        public void SubscribeMarketData(string symbol, string reqID)
        {
            MDReqID mdReqID = new MDReqID(reqID);
            SubscriptionRequestType subType = new SubscriptionRequestType(SubscriptionRequestType.SNAPSHOT_PLUS_UPDATES);
            MarketDepth marketDepth = new MarketDepth(1);
            MDUpdateType mdUpdateType = new MDUpdateType(0);

            QuickFix.FIX44.MarketDataRequest.NoMDEntryTypesGroup marketDataEntryGroup = new QuickFix.FIX44.MarketDataRequest.NoMDEntryTypesGroup();
            marketDataEntryGroup.SetField(new MDEntryType(MDEntryType.BID));
            marketDataEntryGroup.SetField(new MDEntryType(MDEntryType.OFFER));

            QuickFix.FIX44.MarketDataRequest.NoRelatedSymGroup symbolGroup = new QuickFix.FIX44.MarketDataRequest.NoRelatedSymGroup();
            symbolGroup.Set(new Symbol(symbol));

            QuickFix.FIX44.MarketDataRequest message = new QuickFix.FIX44.MarketDataRequest();
            message.SetField(mdReqID);
            message.SetField(subType);
            message.SetField(marketDepth);
            message.SetField(mdUpdateType);

            message.AddGroup(marketDataEntryGroup);
            message.AddGroup(symbolGroup);

            if (m_SessionID != null)
            {
                Session.SendToTarget(message, m_SessionID);
            }
        }
        public void UpdateDicSymbolQuote(string strSymbol, string strPriceType, string strVal)
        {
            MTQuote quote;
            if (!m_dicSymbolQuote.ContainsKey(strSymbol))
                quote = new MTQuote();
            else
                quote = m_dicSymbolQuote[strSymbol];

            if (strPriceType == "Bid")
                quote.bid = double.Parse(strVal);
            else if (strPriceType == "Ask")
                quote.ask = double.Parse(strVal);

            m_dicSymbolQuote[strSymbol] = quote;
        }

        #region Application interface overrides
        public void OnCreate(SessionID sessionID)
        {
            Console.WriteLine("Session Created - " + sessionID.ToString());
            m_SessionID = sessionID;
        }
        public void OnLogon(SessionID sessionID)
        {
            Console.WriteLine("Logon - " + sessionID.ToString());
            m_bConnected = true;
            //FixApiConnectEventListener();
            Logger.Instance().Log($"Price Connection: Logon - {sessionID.ToString()}");
            for (int i = 0; i < Global_Config.m_SymbolsToSubscribe.Count; i++)
            {
                SubscribeMarketData(Global_Config.m_SymbolsToSubscribe[i], i.ToString());
            }
        }
        public void OnLogout(SessionID sessionID)
        {
            Console.WriteLine("Logout - " + sessionID.ToString());
            m_bConnected = false;
            //FixApiDisconnectEventListener();
            Logger.Instance().Log($"Price Connection: Logout - {sessionID.ToString()}", LogStyle.HighLight);
        }
        public void FromAdmin(Message message, SessionID sessionID) { }
        public void ToAdmin(Message message, SessionID sessionID)
        {
            if (message.Header.GetString(Tags.MsgType) == "A")
            {
                string Password = settings.Get().GetString("Password");
                message.SetField(new Password(Password));
            }
        }
        public void FromApp(Message message, SessionID sessionID)
        {
            try
            {
                Crack(message, sessionID);
            }
            catch
            {
            }
        }
        public void ToApp(Message message, SessionID sessionID) { }
        #endregion

        #region MessageCracker handlers
        public void OnMessage(QuickFix.FIX44.MarketDataRequestReject m, SessionID s)
        {
            Console.WriteLine("Received MarketDataRequestReject");
            Console.WriteLine("MarketDataRequestReject -> ");
            if (m.IsSetField(Tags.Text))
            {
                Console.WriteLine(" Text -> {0}", m.GetString(Tags.Text));
            }
        }
        public void OnMessage(QuickFix.FIX44.MarketDataSnapshotFullRefresh m, SessionID s)
        {
            string symbol = m.GetString(Tags.Symbol).ToString();
            double bid_price = 0;
            double ask_price = 0;
            int entry_count = int.Parse(m.GetString(Tags.NoMDEntries));
            for (int i = 1; i <= entry_count; i++)
            {
                QuickFix.FIX44.MarketDataIncrementalRefresh.NoMDEntriesGroup group = new QuickFix.FIX44.MarketDataIncrementalRefresh.NoMDEntriesGroup();
                m.GetGroup(i, group);
                string entry_type = group.GetString(Tags.MDEntryType);
                if (entry_type == "0")
                {
                    bid_price = double.Parse(group.GetString(Tags.MDEntryPx));
                }
                else if (entry_type == "1")
                {
                    ask_price = double.Parse(group.GetString(Tags.MDEntryPx));
                }
            }
            Console.WriteLine("Symbol: " + symbol + "  Bid: " + bid_price.ToString() + "  Ask: " + ask_price.ToString());
        }

        #endregion
    }
}
