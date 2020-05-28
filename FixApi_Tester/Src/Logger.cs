using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FixApi_Tester
{
    class Logger
    {
        public event LogEvent LogEventListener;
        private static Logger m_Logger = null;
        private string m_strAppDirPath = "";
        private Dictionary<string, string> m_dicLog = new Dictionary<string, string>();
        private System.Windows.Forms.Timer m_Timer = new System.Windows.Forms.Timer();

        public static Logger Instance()
        {
            if (m_Logger == null)
            {
                m_Logger = new Logger();
            }
            return m_Logger;
        }
        public Logger()
        {
            m_strAppDirPath = Path.GetDirectoryName(Application.ExecutablePath);
            if (!Directory.Exists(m_strAppDirPath + "/Logs"))
                Directory.CreateDirectory(m_strAppDirPath + "/Logs");
            if (!Directory.Exists(m_strAppDirPath + "/Logs/Test"))
                Directory.CreateDirectory(m_strAppDirPath + "/Logs/Test");
            /*
            m_Timer.Interval = 10000;
            m_Timer.Tick += OnTimer;
            m_Timer.Start();
            */
        }
        /*
        private void OnTimer(object sender, EventArgs e)
        {
            DumpToFile();
        }
        */
        private void Save(string filename, string outStr)
        {
            if (!m_dicLog.ContainsKey(filename))
            {
                m_dicLog.Add(filename, outStr);
            }
            else
            {
                string curLog = m_dicLog[filename];
                curLog += outStr;
                m_dicLog[filename] = curLog;
                
                if (curLog.Length >= 10000)
                {
                    using (var fs = File.Open(filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    {
                        byte[] toBytes = Encoding.ASCII.GetBytes(curLog);
                        fs.Write(toBytes, 0, toBytes.Length);
                    }
                    m_dicLog[filename] = "";
                }
                else
                {
                    m_dicLog[filename] = curLog;
                }
                
            }
        }
        public void DumpToFile()
        {
            List<string> keyList = new List<string>(this.m_dicLog.Keys);
            for (int i = 0; i < keyList.Count; i++)
            {
                string filename = keyList[i];
                string curLog = m_dicLog[filename];
                using (var fs = File.Open(filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    byte[] toBytes = Encoding.ASCII.GetBytes(curLog);
                    fs.Write(toBytes, 0, toBytes.Length);
                }
            }
            m_dicLog.Clear();
        }
        public void Log(string strLog, LogStyle logStyle = LogStyle.Normal)
        {
            LogEventListener(strLog, logStyle);
            LogToFile(strLog);
        }
        public void LogTest(string symbol, string strLog)
        {
            string filename = $"{m_strAppDirPath}/Logs/Test/{symbol}-Log.txt";
            string outStr = DateTime.Now.ToString("yyyy/MM/dd/HH:mm:ss:fffffffK") + " : " + strLog + "\r\n";
            Save(filename, outStr);
        }
        public void LogToFile(string strLog)
        {
            string filename = $"{m_strAppDirPath}/Logs/Log.txt";
            string outStr = DateTime.Now.ToString("yyyy/MM/dd/HH:mm:ss:fffffffK") + " : " + strLog + "\r\n";
            Save(filename, outStr);
        }
        public void OutputCsv(string symbol, double FastBid, double FastAsk, double SlowBid, double SlowAsk, double PriceDiffBid, double PriceDiffAsk)
        {
            string filename = m_strAppDirPath + "/Logs/" + symbol + "_Pricediff.csv";
            string outStr = DateTime.Now.ToString("yyyy/MM/dd/HH:mm:ss:fffffffK") + "," + symbol + "," + FastBid + "," + FastAsk + "," + SlowBid + "," + SlowAsk + "," + PriceDiffBid + "," + PriceDiffAsk + "\r\n";
            Save(filename, outStr);
        }

        public void OutputCsvOrder(string symbol, double SlowBid, double SlowAsk, string OrderDirection)
        {
            string filename = m_strAppDirPath + "/Logs/" + symbol + "_OrderLog.txt";
            string outStr = DateTime.Now.ToString("yyyy/MM/dd/HH:mm:ss:fffffffK") + "," + symbol + "," + OrderDirection + " Order Sent" + ",SlowBid," + SlowBid + ",SlowAsk," + SlowAsk + "\r\n";
            Save(filename, outStr);
        }

        public void OutputProcess(string symbol, string txt)
        {
            string filename = m_strAppDirPath + "/Logs/" + symbol + "_ProcessLog.txt";
            string outStr = DateTime.Now.ToString("yyyy/MM/dd/HH:mm:ss:fffffffK") + "," + symbol + ": " + txt + "\r\n";
            Save(filename, outStr);
        }
        public void OutputSlippage(string symbol, string orderType, string slippage)
        {
            string filename = m_strAppDirPath + "/Logs/" + symbol + "_SlippageLog.csv";
            string outStr = DateTime.Now.ToString("yyyy/MM/dd/HH:mm:ss:fffffffK") + "," + symbol + "," + orderType + "," + slippage + "\r\n";
            Save(filename, outStr);
        }
        public void OutputOrderDuration(string symbol, string orderType, string strDuration)
        {
            string filename = m_strAppDirPath + "/Logs/" + symbol + "_OrderDurationLog.csv";
            string outStr = DateTime.Now.ToString("yyyy/MM/dd/HH:mm:ss:fffffffK") + "," + symbol + "," + orderType + "," + strDuration + "\r\n";
            Save(filename, outStr);
        }
        public void OutputTrailPriceDiff(string symbol, string orderType, double FaskAsk, double FaskBid, double SlowAsk, double SlowBid, double PriceDiff)
        {
            string filename = m_strAppDirPath + "/Logs/" + symbol + "_TrailPriceDiffLog.csv";
            string outStr = DateTime.Now.ToString("yyyy/MM/dd/HH:mm:ss:fffffffK") + "," + symbol + "," + orderType + "," + FaskAsk.ToString() + "," + FaskBid.ToString() + "," + SlowBid.ToString() + "," + SlowAsk.ToString() + "," + PriceDiff.ToString() + "\r\n";
            Save(filename, outStr);
        }
    }
}
