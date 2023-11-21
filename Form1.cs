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
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace QUXZ_s_Discord_Account_ToolKit
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }
        class Friends
        {
            public string id { get; set; }
          //  public _recipients recipients { get; set; }
            public _user user { get; set; }
        }

        class OpenChannels
        {
            public string id { get; set; }
        }

        class MeInfo
        {
            public string id { get; set; }
            public string username { get; set; }
            public string verified { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string mfa_enabled { get; set; }
      //      public Array linked_users { get; set; }
            public string locale { get; set; }
            public string premium_type { get; set; }
        }

        class _user
        {
            public string username { get; set; }
            public string global_name { get; set; }
        }

        /*
        class _recipients
        {
            public string id { get; set; }
            public string username { get; set; }
            public string global_name { get; set; }
        }
        */

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            using (var client = new WebClient())
            {
                //https://discord.com/api/v9/users/@me/relationships //
                // https://discord.com/api/v9/users/@me/channels
                client.Headers.Add("authorization", kryptonTextBox1.Text);
                string eh = client.DownloadString("https://discord.com/api/v9/users/@me/channels");
                List<OpenChannels> openchannels = JsonConvert.DeserializeObject<List<OpenChannels>>(eh);
                kryptonRichTextBox1.Clear();
                foreach (OpenChannels friend in openchannels)
                {
                    Console.WriteLine(friend.id);

                    
                          HttpClient httpClient = new HttpClient();
                          httpClient.DefaultRequestHeaders.Add("authorization", kryptonTextBox1.Text);
                          MultipartFormDataContent from = new MultipartFormDataContent();
                          from.Add(new StringContent(kryptonTextBox2.Text), "content");
                          HttpResponseMessage response = httpClient.PostAsync("https://discord.com/api/v9/channels/" + friend.id + "/messages", from).Result;
                          response.EnsureSuccessStatusCode();
                          httpClient.Dispose();

                    kryptonRichTextBox1.Text += "Sent Message To " + friend.id + Environment.NewLine;


                }
                client.Dispose();
            }
        }
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("authorization", kryptonTextBox1.Text);
                string info = client.DownloadString("https://discord.com/api/v9/users/@me/relationships");
                List<Friends> allfriends = JsonConvert.DeserializeObject<List<Friends>>(info);
                int counter = 1;
                kryptonRichTextBox1.Clear();
                foreach (Friends friend in allfriends)
                {
                    kryptonRichTextBox1.Text += friend.user.username + Environment.NewLine;
                    counter++;
                }
            }
        }//https://discordapp.com/api/v6/users/@me
        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("authorization", kryptonTextBox1.Text);
                string info = client.DownloadString("https://discord.com/api/v9/users/@me/channels");
                List<OpenChannels> openchannels = JsonConvert.DeserializeObject<List<OpenChannels>>(info);
                int counter = 1;
                kryptonRichTextBox1.Clear();
                foreach (OpenChannels channels in openchannels)
                {
                    kryptonRichTextBox1.Text += channels.id + Environment.NewLine;
                    counter++;
                }
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("authorization", kryptonTextBox1.Text);
                string info = client.DownloadString("https://discord.com/api/v9/users/@me/billing/subscriptions");
                List<OpenChannels> openchannels = JsonConvert.DeserializeObject<List<OpenChannels>>(info);
                kryptonRichTextBox1.Text += info;
                int counter = 1;
           /*     kryptonRichTextBox1.Clear();
                foreach (OpenChannels channels in openchannels)
                {
                    kryptonRichTextBox1.Text += channels.id + Environment.NewLine;
                    counter++;
                }
           */
            }
        }

        private void kryptonPanel1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", "https://github.com/okinum/Discord-Account-ToolKit");
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("authorization", kryptonTextBox1.Text);
                string info = client.DownloadString("https://discord.com/api/v9/users/@me");
                MeInfo meInfo = JsonConvert.DeserializeObject<MeInfo>(info);
                kryptonRichTextBox1.Clear();
                kryptonRichTextBox1.Text += "id: " + meInfo.id + Environment.NewLine;
                kryptonRichTextBox1.Text += "username: " + meInfo.username + Environment.NewLine;
                kryptonRichTextBox1.Text += "email: " + meInfo.email + Environment.NewLine;
                kryptonRichTextBox1.Text += "phone: " + meInfo.phone + Environment.NewLine;
                kryptonRichTextBox1.Text += "mfa_enabled: " + meInfo.mfa_enabled + Environment.NewLine;
                kryptonRichTextBox1.Text += "locale: " + meInfo.locale + Environment.NewLine;
                kryptonRichTextBox1.Text += "premium_type: " + meInfo.premium_type + Environment.NewLine;
            }
        }
    }
}