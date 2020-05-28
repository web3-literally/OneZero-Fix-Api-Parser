using System;
using System.Collections.Generic;

namespace FixApi_Tester
{
    public class AccountSetting
    {
        public string Price_SenderCompID { get; set; }
        public string Price_HostName { get; set; }
        public int Price_Port { get; set; }
        public string Price_TargetCompID { get; set; }
        public string Price_Password { get; set; }

        public string Trade_HostName { get; set; }
        public int Trade_Port { get; set; }
        public string Trade_SenderCompID { get; set; }
        public string Trade_TargetCompID { get; set; }
        public string Trade_Password { get; set; }
    }
    /*-----------------------------------TOTAL EVENT-------------------------------*/
    delegate void LogEvent(string strLog, LogStyle logStyle);
    delegate void PingFailedEvent(string strStatus);
    delegate void ServerTradingStatusEvent(string strEvent);
    delegate void AccountInfoEvent(int nAccountNumber, double dBalance, double dEquity);
    delegate void FixApiConnectEvent();
    delegate void FixApiDisconnectEvent();
    delegate void FixApiQuoteEvent(string symbol, string strBid, string strAsk, int nSequence);
    delegate void OpenApiConnectEvent();
    delegate void OpenApiDisconnectEvent();
    delegate void TradeLossEvent();

    /*-----------------------------------About CalcBuffer-------------------------------*/
    public class MTQuote
    {
        public double bid { get; set; }
        public double ask { get; set; }
        public int digits { get; set; }
    }
    public class QuoteDiff
    {
        public double Counter { get; set; }//1
        public double CurrentDiffBid { get; set; }//2
        public double AverageDiffBid { get; set; }//3
        public double SumOfDiffBid { get; set; }//4
        public double RawDiffBid { get; set; }
        public double CurrentDiffAsk { get; set; }
        public double AverageDiffAsk { get; set; }
        public double SumOfDiffAsk { get; set; }
        public double RawDiffAsk { get; set; }
        public List<double> BufferFSDiffBid { get; set; } = new List<double>();//Buffer for 2000 FSDiffBid
        public List<double> BufferFSDiffAsk { get; set; } = new List<double>();//Buffer for 2000 FSDiffAsk
        public int BuyOrderNumber { get; set; }//5
        public int BuyLimitOrderNumber { get; set; }
        public double BuyOrderRate { get; set; }//8
        public double BuySubmitRate { get; set; }
        public int SellOrderNumber { get; set; }//9
        public int SellLimitOrderNumber { get; set; }//9
        public double SellOrderRate { get; set; }//12
        public double SellSubmitRate { get; set; }
        public double MAXBUYRATE { get; set; }//13
        public double MINSELLRATE { get; set; }//14
        public DateTime LongCloseSentTime { get; set; }//23
        public DateTime LongModifyTime { get; set; }
        public DateTime ShortCloseSentTime { get; set; }//24
        public DateTime ShortModifyTime { get; set; }
        public double LastLongCoverFlag { get; set; }//30
        public double LastShortCoverFlag { get; set; }//31
        public double LastLongOrderCloseFlag { get; set; }//33
        public double LastShortOrderCloseFlag { get; set; }//34
        public int FlagBigBidPriceDiff { get; set; }// 0: normal, 1: positive, 2: negative;
        public int FlagBigAskPriceDiff { get; set; }// 0: normal, 1: positive, 2: negative;
        public double OutputDiffCounter { get; set; }
    }
    /*-----------------------------------About Logger-------------------------------*/
    enum LogStyle
    {
        Normal,
        HighLight,
    }

    class Global_Config
    {
        public static List<string> m_SymbolsToSubscribe = new List<string> { "EURUSD", "GBPUSD", "USDCHF", "AUDUSD", "USDJPY", "USDCAD", "NZDUSD" };
        public static Dictionary<string, int> m_dicSymbolDigits = new Dictionary<string, int>()
        {
            {"EURUSD", 5},
            {"GBPUSD", 5},
            {"EURJPY", 3},
            {"USDJPY", 3},
            {"AUDUSD", 5},
            {"USDCHF", 5},
            {"GBPJPY", 3},
            {"USDCAD", 5},
            {"EURGBP", 5},
            {"EURCHF", 5},
            {"AUDJPY", 3},
            {"NZDUSD", 5},
            {"CHFJPY", 3},
            {"EURAUD", 5},
            {"CADJPY", 3},
            {"GBPAUD", 5},
            {"EURCAD", 5},
            {"AUDCAD", 5},
            {"GBPCAD", 5},
            {"AUDNZD", 5},
            {"NZDJPY", 3},
            {"USDNOK", 5},
            {"AUDCHF", 5},
            {"USDMXN", 5},
            {"GBPNZD", 5},
            {"EURNZD", 5},
            {"CADCHF", 5},
            {"USDSGD", 5},
            {"USDSEK", 5},
            {"NZDCAD", 5},
            {"EURSEK", 5},
            {"GBPSGD", 5},
            {"EURNOK", 5},
            {"EURHUF", 3},
            {"USDPLN", 5},
            {"NZDCHF", 5},
            {"GBPCHF", 5},
            {"AUDSGD", 5},
            {"AUDZAR", 5},
            {"EURCZK", 4},
            {"EURMXN", 5},
            {"EURPLN", 5},
            {"EURRUB", 5},
            {"EURSGD", 5},
            {"EURTRY", 5},
            {"EURZAR", 5},
            {"GBPZAR", 5},
            {"CHFHUF", 3},
            {"CHFZAR", 5},
            {"NOKSEK", 5},
            {"NZDSEK", 5},
            {"NZDSGD", 5},
            {"SGDJPY", 3},
            {"TRYJPY", 3},
            {"USDCZK", 4},
            {"USDHUF", 3},
            {"USDILS", 5},
            {"USDRUB", 5},
            {"USDTRY", 5},
            {"USDZAR", 5},
            {"ZARJPY", 3},
            {"XAUUSD", 2},
            {"XAGUSD", 3},
            {"XPTUSD", 2},
            {"XPDUSD", 2},
            {"CL", 3},
            {"NGAS", 3},
            {"NIKKEI", 0},
            {"HK50", 1},
            {"FTSE", 1},
            {"STOXX50", 1},
            {"CAC", 1},
            {"DAX", 1},
            {"ASX", 1},
            {"DOW", 1},
            {"SP", 1},
            {"NSDQ", 1}
        };
    }
}