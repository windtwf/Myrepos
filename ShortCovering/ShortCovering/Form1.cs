using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortCovering
{

    public partial class Form_ShortCovering : Form
    {              
        public Form_ShortCovering()
        {
            InitializeComponent();
            Load();
            //webBrowser1.ObjectForScripting = this;
            //webBrowser1.DocumentText =
            //"<html><head><script>" +
            //"function test(message) { alert(message); }" +
            //"</script></head><body><button " +
            //"onclick=\"window.external.Test('called from script code')\">" +
            //"call client code from script code</button>" +
            //"</body></html>";
            
        }

        private void Load()
        {
            webBrowser1.DocumentText = @"<html><head>
                                        <script type= 'text/javascript' src = 'http://hq.sinajs.cn/list=sh601668' charset='gb2312'></script>
                                        <script type= 'text/javascript'>
                                       function getStock() {                                                                     
                                       var elements = hq_str_sh601668.split(',');
                                       document.write(elements[0] + '\n' + 'current price:' + elements[3]);}
                                        </script>                                       
                                       </head></html>";
        }

        public List<double> GetSCValues
        {
            get
            {
                List<double> listValue = new List<double>();
                // default price and number.
                listValue.Add(Convert.ToDouble(txtDefaultPrice.Text));
                listValue.Add(Convert.ToDouble(txtDefaultNumber.Text));
                // SC price and number.
                listValue.Add(Convert.ToDouble(txtSCPrice.Text));
                listValue.Add(Convert.ToDouble(txtSCNumber.Text));
                return listValue;
            }
        }

        public List<double> GetTValues
        {
            get
            {
                List<double> listValue = new List<double>();
                // T+0: Sell price and number
                listValue.Add(Convert.ToDouble(txtSellPrice.Text));
                listValue.Add(Convert.ToDouble(txtSellNumber.Text));
                // T+0 : Current average price
                listValue.Add(Convert.ToDouble(txtSellNumber.Text));
                return listValue;
            }
        }

        private void btnAverage_Click(object sender, EventArgs e)
        {
            txtAverageMoney.Text = Convert.ToString((GetSCValues[0] * GetSCValues[1] + GetSCValues[2] * GetSCValues[3]) / (GetSCValues[1] + GetSCValues[3]));
            var source = new AutoCompleteStringCollection();
            source.Add(txtDefaultPrice.Text);
            txtDefaultPrice.AutoCompleteCustomSource = source;
            txtDefaultPrice.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtDefaultPrice.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }          

        private void btnSellAccount_Click(object sender, EventArgs e)
        {
            double currentAveragePrice = (GetSCValues[0] * GetSCValues[1] + GetSCValues[2] * GetSCValues[3]) / (GetSCValues[1] + GetSCValues[3]);

            txtTAvePrice.Text = Convert.ToString(((GetSCValues[1] + GetSCValues[3]) * currentAveragePrice - GetTValues[1] * GetTValues[0]) / ((GetSCValues[1] + GetSCValues[3]) - GetTValues[1]));
        }


        // http://www.cnblogs.com/blodfox777/archive/2009/02/10/1387229.html   stock url
        private void btnPrice_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("getStock");
        }

        private void tsmi_volumn_Click(object sender, EventArgs e)
        {
            FrmGetVolumn volumn = new FrmGetVolumn();
            volumn.Show();
        }       
    }
}
