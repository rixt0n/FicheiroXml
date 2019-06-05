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
using System.Xml;


namespace Feed_JN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WebClient wc = new WebClient();
            string noticias = wc.DownloadString("http://feeds.jn.pt/jn-ultimas");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(noticias);

            List<Noticia> noticialist = new List<Noticia>();

            XmlNodeList xnlist = xml.GetElementsByTagName("item");
            
            foreach (XmlNode node in xnlist)
            {
                Noticia news = new Noticia();
                news.title = node.ChildNodes[0].InnerText;
                news.link = node.ChildNodes[1].InnerText;
                news.description = node.ChildNodes[2].InnerText;
                news.category = node.ChildNodes[3].InnerText;
                news.pubDate = node.ChildNodes[4].InnerText;
                noticialist.Add(news);
            }

            dgNoticia.DataSource = noticialist;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://feeds.jn.pt/jn-ultimas");
        }

        private void dgNoticia_DoubleClick(object sender, EventArgs e)
        {
            //OU 
            string abrelink = dgNoticia.SelectedCells[1].Value.ToString();
            System.Diagnostics.Process.Start(abrelink);
        }
    }
}
