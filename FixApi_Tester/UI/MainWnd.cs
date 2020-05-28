using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using QuickFix.Fields;

namespace FixApi_Tester
{
    public partial class MainWnd : Form
    {
        public MainWnd()
        {
            InitializeComponent();

            string strAppDirPath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            if (!Directory.Exists(strAppDirPath + "/Settings"))
                Directory.CreateDirectory(strAppDirPath + "/Settings");

            Logger.Instance().LogEventListener += new LogEvent(this.OnLogEvent);

            LoadAccountSetting();

            m_txtPrice_Password.Text = m_AccountSetting.Price_Password;
            m_txtPrice_Host.Text = m_AccountSetting.Price_HostName;
            m_txtPrice_Port.Text = m_AccountSetting.Price_Port.ToString();
            m_txtPrice_SenderCompID.Text = m_AccountSetting.Price_SenderCompID;
            m_txtPrice_TargetCompID.Text = m_AccountSetting.Price_TargetCompID;

            m_txtTrade_Password.Text = m_AccountSetting.Trade_Password;
            m_txtTrade_Host.Text = m_AccountSetting.Trade_HostName;
            m_txtTrade_Port.Text = m_AccountSetting.Trade_Port.ToString();
            m_txtTrade_SenderCompID.Text = m_AccountSetting.Trade_SenderCompID;
            m_txtTrade_TargetCompID.Text = m_AccountSetting.Trade_TargetCompID;   
        }
        private void OnLogEvent(string strLog, LogStyle Style)
        {
            BeginInvoke((Action)(() =>
            {
                if (m_txtLogs.TextLength >= 5000)
                    m_txtLogs.ResetText();

                if (Style == LogStyle.HighLight)
                {
                    m_txtLogs.SelectionStart = m_txtLogs.TextLength;
                    m_txtLogs.SelectionLength = 0;
                    m_txtLogs.SelectionColor = Color.Red;
                    m_txtLogs.AppendText(strLog + '\n');
                    m_txtLogs.SelectionColor = m_txtLogs.ForeColor;
                }
                else
                {
                    m_txtLogs.AppendText(strLog + '\n');
                }
            }));
        }
        private void m_btnConnect_Click(object sender, EventArgs e)
        {
            m_AccountSetting.Price_Password = m_txtPrice_Password.Text;
            m_AccountSetting.Price_HostName = m_txtPrice_Host.Text;
            m_AccountSetting.Price_Port = int.Parse(m_txtPrice_Port.Text);
            m_AccountSetting.Price_SenderCompID = m_txtPrice_SenderCompID.Text;
            m_AccountSetting.Price_TargetCompID = m_txtPrice_TargetCompID.Text;

            m_AccountSetting.Trade_Password = m_txtTrade_Password.Text;
            m_AccountSetting.Trade_HostName = m_txtTrade_Host.Text;
            m_AccountSetting.Trade_Port = int.Parse(m_txtTrade_Port.Text);
            m_AccountSetting.Trade_SenderCompID = m_txtTrade_SenderCompID.Text;
            m_AccountSetting.Trade_TargetCompID = m_txtTrade_TargetCompID.Text;

            SaveAccountSetting();

            OneZero_Price_FixApi_Connector.Instance().StartSession(
                m_AccountSetting.Price_HostName,
                m_AccountSetting.Price_Port,
                m_AccountSetting.Price_SenderCompID,
                m_AccountSetting.Price_TargetCompID,
                m_AccountSetting.Price_Password);

            OneZero_Trade_FixApi_Connector.Instance().StartSession(
                m_AccountSetting.Trade_HostName,
                m_AccountSetting.Trade_Port,
                m_AccountSetting.Trade_SenderCompID,
                m_AccountSetting.Trade_TargetCompID,
                m_AccountSetting.Trade_Password);
        }
        private void LoadAccountSetting()
        {
            string strAppDirPath = Path.GetDirectoryName(Application.ExecutablePath);
            m_AccountSetting = ReadFromXmlFile<AccountSetting>(strAppDirPath + "/Settings/AccountSetting.xml");
        }
        private void SaveAccountSetting()
        {
            string strAppDirPath = Path.GetDirectoryName(Application.ExecutablePath);
            WriteToXmlFile(strAppDirPath + "/Settings/AccountSetting.xml", m_AccountSetting);
        }
        private void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        private T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            catch
            {
                return new T();
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        private void m_btnSendMarketOrder_Click(object sender, EventArgs e)
        {
            OneZero_Trade_FixApi_Connector.Instance().SendMarketOrder("USDJPY", 0.01, Side.BUY);
        }

        private void m_btnSendLimitOrder_Click(object sender, EventArgs e)
        {
        }

        private void m_btnSendPrevQuotedOrder_Click(object sender, EventArgs e)
        {

        }

        private void m_btnPositionForRequest_Click(object sender, EventArgs e)
        {
            OneZero_Trade_FixApi_Connector.Instance().PositionsRequest();
        }



        private AccountSetting m_AccountSetting = new AccountSetting();
    }
}
