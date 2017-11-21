using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace ShortCovering
{
    public partial class FrmGetVolumn : Form
    {
         string billUrl = "http://vip.stock.finance.sina.com.cn/quotes_service/view/CN_BillList.php?sort=ticktime&symbol=";
         List<BillInfo> BillInfoList = null;
         List<string> BillDisplayList;
         BillInfo billInfo;
         string text;

        public FrmGetVolumn()
        {
            InitializeComponent();
        }

        private void btnQueryBill_Click(object sender, EventArgs e)
        {
            BillDisplayList = new List<string>();

            foreach (BillInfo info in BIL)
            {
                string billStr = string.Format("{0}       {1}       {2}       {3}", info.Time, info.Volumn, info.Price, info.Direction);
                BillDisplayList.Add(billStr);               
            }
            txtBill.Text = String.Join(Environment.NewLine, BillDisplayList);
        }

        public List<BillInfo> BIL
        {
            get
            {
                BillInfoList = new List<BillInfo>();
            if (txtStockID.Text.StartsWith("600") || txtStockID.Text.StartsWith("601") || txtStockID.Text.StartsWith("603"))
            {
                billUrl = billUrl + "sh" + txtStockID.Text;
            }
            else
            {
                billUrl = billUrl + "sz" + txtStockID.Text;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(billUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();

            using (StreamReader reader = new StreamReader(stream))
            {
                while ((text = reader.ReadLine()) != null)
                {
                    if (text.StartsWith(" bill"))
                    {                                           
                            billInfo = new BillInfo();
                        string[] args = text.Split('\'');
                            billInfo.Time = args[1];
                            billInfo.Volumn = Convert.ToInt32(args[3]);
                            billInfo.Price = (args[5]);
                            billInfo.Direction = (args[7]);
                            BillInfoList.Add(billInfo);
                        }
                    }
                }
                return BillInfoList;
            }
        }

        public void Milliion()
        {
            BillDisplayList = new List<string>();

            foreach (BillInfo info in BIL)
            {
                if (info.Volumn >= 1000000)
                {
                    string billStr = string.Format("{0}       {1}       {2}       {3}", info.Time, info.Volumn, info.Price, info.Direction);
                    BillDisplayList.Add(billStr);
                }
            }
            txtBill.Text = String.Join(Environment.NewLine, BillDisplayList);
        }

        private void comboHand_SelectedIndexChanged(object sender, EventArgs e)
        {
            BillDisplayList = new List<string>();
            BillInfoList = new List<BillInfo>();
            if (!billUrl.Contains(txtStockID.Text))
            {
                if (txtStockID.Text.StartsWith("600") || txtStockID.Text.StartsWith("601") || txtStockID.Text.StartsWith("603"))
                {
                    billUrl = billUrl + "sh" + txtStockID.Text;
                }
                else
                {
                    billUrl = billUrl + "sz" + txtStockID.Text;
                }
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(billUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();

            using (StreamReader reader = new StreamReader(stream))
            {
                while ((text = reader.ReadLine()) != null)
                {
                    if (text.StartsWith(" bill"))
                    {
                        billInfo = new BillInfo();
                        string[] args = text.Split('\'');
                        billInfo.Time = args[1];
                        billInfo.Volumn = Convert.ToInt32(args[3]);
                        billInfo.Price = (args[5]);
                        billInfo.Direction = (args[7]);
                        BillInfoList.Add(billInfo);
                    }
                }
            }

            //foreach (BillInfo info in BillInfoList)
            //{
            //    string billStr = string.Format("{0}       {1}       {2}       {3}", info.Time, info.Volumn, info.Price, info.Direction);
            //    BillDisplayList.Add(billStr);
            //}
            //txtBill.Text = String.Join(Environment.NewLine, BillDisplayList);

            //txtBill.Clear();
            //BillDisplayList.Clear();
            //txtBill.Text = string.Empty;
            int time = comboHand.SelectedIndex;
            string siid = comboHand.SelectedItem.ToString();

            if (comboHand.SelectedItem.ToString() == "10000")
            {
                txtBill.Clear();
                int count = BillInfoList.Count;
                foreach (BillInfo info in BillInfoList)
                {
                    if (info.Volumn >= 1000000)
                    {
                        string billStr = string.Format("{0}       {1}       {2}       {3}", info.Time, string.Format("{0:##-###-###-##}", info.Volumn).TrimStart('-').Replace('-', ','), info.Price, info.Direction);
                        BillDisplayList.Add(billStr);
                    }
                }
                txtBill.Text = String.Join(Environment.NewLine, BillDisplayList);
            }
            if (comboHand.SelectedItem.ToString() == "5000")
            {
                txtBill.Clear();
                foreach (BillInfo info in BillInfoList)
                {
                    if (info.Volumn >= 500000 && info.Volumn <= 1000000)
                    {
                        string billStr = string.Format("{0}       {1}       {2}       {3}", info.Time, string.Format("{0:##-###-###-##}", info.Volumn).TrimStart('-').Replace('-', ','), info.Price, info.Direction);
                        BillDisplayList.Add(billStr);
                    }
                }
                txtBill.Text = String.Join(Environment.NewLine, BillDisplayList);
            }
            if (comboHand.SelectedItem.ToString() == "2000")
            {
                txtBill.Clear();
                foreach (BillInfo info in BillInfoList)
                {
                    if (info.Volumn >= 200000 && info.Volumn <= 500000)
                    {
                        string billStr = string.Format("{0}       {1}       {2}       {3}", info.Time, string.Format("{0:##-###-###-##}", info.Volumn).TrimStart('-').Replace('-', ','), info.Price, info.Direction);
                        BillDisplayList.Add(billStr);
                    }
                }
                txtBill.Text = String.Join(Environment.NewLine, BillDisplayList);
            }
            if (comboHand.SelectedItem.ToString() == "1000")
            {
                txtBill.Clear();
                foreach (BillInfo info in BillInfoList)
                {
                    if (info.Volumn >= 100000 && info.Volumn <= 200000)
                    {
                        string billStr = string.Format("{0}       {1}       {2}       {3}", info.Time, string.Format("{0:##-###-###-##}", info.Volumn).TrimStart('-').Replace('-', ','), info.Price, info.Direction);
                        BillDisplayList.Add(billStr);
                    }
                }
                txtBill.Text = String.Join(Environment.NewLine, BillDisplayList);
            }
        }

        public class BillInfo
        {
            public string Time { get; set; }
            public int Volumn { get; set; }
            public string Price { get; set; }
            public string Direction { get; set; }
        }
    }
}
