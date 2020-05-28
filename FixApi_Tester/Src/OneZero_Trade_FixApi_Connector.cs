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
    class OneZero_Trade_FixApi_Connector : MessageCracker, IApplication
    {
        public event FixApiConnectEvent FixApiConnectEventListener;
        public event FixApiDisconnectEvent FixApiDisconnectEventListener;

        private static OneZero_Trade_FixApi_Connector m_staticFixApiManager = null;
        private SessionSettings settings;
        private SocketInitiator initiator;

        private SessionID m_SessionID;
        private bool m_bConnected = false;


        public static OneZero_Trade_FixApi_Connector Instance()
        {
            if (m_staticFixApiManager == null)
                m_staticFixApiManager = new OneZero_Trade_FixApi_Connector();
            return m_staticFixApiManager;
        }
        public OneZero_Trade_FixApi_Connector()
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
            Logger.Instance().Log($"Trade Connection: Logon - {sessionID.ToString()}");
        }
        public void OnLogout(SessionID sessionID)
        {
            Console.WriteLine("Logout - " + sessionID.ToString());
            m_bConnected = false;
            //FixApiDisconnectEventListener();
            Logger.Instance().Log($"Trade Connection: Logout - {sessionID.ToString()}", LogStyle.HighLight);
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
        public void OnMessage(QuickFix.FIX44.ExecutionReport m, SessionID s)
        {
            Logger.Instance().Log($"REPLY (ExecutionReport) => {m.ToString()}");
        }
        public void OnMessage(QuickFix.FIX44.OrderCancelReject m, SessionID s)
        {
            Logger.Instance().Log($"REPLY (OrderCancelReject) => {m.ToString()}");
        }
        public void OnMessage(QuickFix.FIX44.RequestForPositionsAck m, SessionID s)
        {
            Logger.Instance().Log($"REPLY (RequestForPositionsAck) => {m.ToString()}");
        }
        public void OnMessage(QuickFix.FIX44.PositionReport m, SessionID s)
        {
            Logger.Instance().Log($"REPLY (PositionReport) => {m.ToString()}");
        }
        #endregion

        #region Trading Commands
        public void SendMarketOrder(string strSymbol, double dLots, char chSide)
        {
            try
            {
                if (m_SessionID == null) return;

                string strCIOrdID = "KYO" + DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");

                QuickFix.FIX44.NewOrderSingle newOrderSingle = new QuickFix.FIX44.NewOrderSingle(
                    new ClOrdID(strCIOrdID),
                    new Symbol(strSymbol),
                    new Side(chSide),
                    new TransactTime(DateTime.UtcNow),
                    new OrdType(OrdType.MARKET));
                newOrderSingle.Set(new HandlInst(HandlInst.AUTOMATED_EXECUTION_ORDER_PRIVATE_NO_BROKER_INTERVENTION));
                newOrderSingle.Set(new OrderQty(Convert.ToDecimal(dLots * Math.Pow(10, 5))));
                newOrderSingle.Set(new TimeInForce(TimeInForce.IMMEDIATE_OR_CANCEL));
                Session.SendToTarget(newOrderSingle, m_SessionID);
                Logger.Instance().Log($"REQUEST (SendMarketOrder) => Symbol: {strSymbol}, Lots: {dLots}, Type: {new Side(chSide)}");
            }
            catch (Exception ex)
            {
                Logger.Instance().LogToFile(ex.ToString());
            }
        }
        public void SendLimitOrder(string strSymbol, double dLots, double dPrice, char chSide)
        {
            if (m_SessionID == null) return;

            string strCIOrdID = "KYO" + DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");

            QuickFix.FIX44.NewOrderSingle newOrderSingle = new QuickFix.FIX44.NewOrderSingle(
                new ClOrdID(strCIOrdID),
                new Symbol(strSymbol),
                new Side(chSide),
                new TransactTime(DateTime.UtcNow),
                new OrdType(OrdType.LIMIT));
            newOrderSingle.Set(new HandlInst(HandlInst.AUTOMATED_EXECUTION_ORDER_PRIVATE_NO_BROKER_INTERVENTION));
            newOrderSingle.Set(new Price(Convert.ToDecimal(dPrice)));
            newOrderSingle.Set(new OrderQty(Convert.ToDecimal(dLots * Math.Pow(10, 5))));
            newOrderSingle.Set(new TimeInForce(TimeInForce.GOOD_TILL_CANCEL));
            Session.SendToTarget(newOrderSingle, m_SessionID);
            Logger.Instance().Log($"REQUEST (SendLimitOrder) => Symbol: {strSymbol}, Lots: {dLots}, Price: {dPrice}, Type: {new Side(chSide)}");
        }
        public void SendPrevQuotedOrder(string strSymbol, double dLots, char chSide, string strQuoteID)
        {
            try
            {
                if (m_SessionID == null) return;

                string strCIOrdID = "KYO" + DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");

                QuickFix.FIX44.NewOrderSingle newOrderSingle = new QuickFix.FIX44.NewOrderSingle(
                    new ClOrdID(strCIOrdID),
                    new Symbol(strSymbol),
                    new Side(chSide),
                    new TransactTime(DateTime.UtcNow),
                    new OrdType(OrdType.PREVIOUSLY_QUOTED));
                newOrderSingle.Set(new HandlInst(HandlInst.AUTOMATED_EXECUTION_ORDER_PRIVATE_NO_BROKER_INTERVENTION));
                newOrderSingle.Set(new OrderQty(Convert.ToDecimal(dLots * Math.Pow(10, 5))));
                newOrderSingle.Set(new TimeInForce(TimeInForce.IMMEDIATE_OR_CANCEL));
                newOrderSingle.Set(new QuoteID(strQuoteID));
                Session.SendToTarget(newOrderSingle, m_SessionID);
                Logger.Instance().Log($"REQUEST (SendPreviouslyQuotedOrder) => Symbol: {strSymbol}, Lots: {dLots}, Type: {new Side(chSide)}, QuoteID: {strQuoteID}");
            }
            catch (Exception ex)
            {
                Logger.Instance().LogToFile(ex.ToString());
            }
        }

        public void OrderCancelRequest(string strOrigClOrdID, char chSide)
        {
            try
            {
                if (m_SessionID == null) return;
                string strCIOrdID = "KYO" + DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");
                QuickFix.FIX44.OrderCancelRequest orderCancelRequest = new QuickFix.FIX44.OrderCancelRequest();
                orderCancelRequest.Set(new OrigClOrdID(strOrigClOrdID));
                orderCancelRequest.Set(new ClOrdID(strCIOrdID));
                orderCancelRequest.Set(new Side(chSide));
                orderCancelRequest.Set(new TransactTime(DateTime.UtcNow));
                Session.SendToTarget(orderCancelRequest, m_SessionID);
                Logger.Instance().Log($"REQUEST (OrderCancelRequest) => {orderCancelRequest.ToString()}");
            }
            catch (Exception ex)
            {
                Logger.Instance().LogToFile(ex.ToString());
            }
        }

        public void PositionsRequest()
        {
            try
            {
                if (m_SessionID == null) return;

                string strPosReqID = "KYO" + DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");
                QuickFix.FIX44.RequestForPositions positionsRequest = new QuickFix.FIX44.RequestForPositions();

                QuickFix.FIX44.RequestForPositions.NoPartyIDsGroup NoPartyIDGroup = new QuickFix.FIX44.RequestForPositions.NoPartyIDsGroup();
                NoPartyIDGroup.Set(new PartyID("*"));

                positionsRequest.Set(new PosReqID(strPosReqID));
                positionsRequest.Set(new PosReqType(PosReqType.POSITIONS));
                positionsRequest.Set(new TransactTime(DateTime.UtcNow));
                positionsRequest.AddGroup(NoPartyIDGroup);

                Session.SendToTarget(positionsRequest, m_SessionID);
                Logger.Instance().Log($"REQUEST (PositionsRequest) => {positionsRequest.ToString()}");
            }
            catch (Exception ex)
            {
                Logger.Instance().LogToFile(ex.ToString());
            }
        }
        #endregion
    }
}
