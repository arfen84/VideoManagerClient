using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoManagerClientHttp
{
    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        Movie movie;

        //send PUT request
        private void button1_Click(object sender, EventArgs e)
        {
            Request(null, "PUT", "/Home/AddPut");

            movie = new Movie();
            movie.Link = "www.film.pl";
            movie.Description = "Super filmidlo";
            //movie.PictureLink = "link";
            string json = JsonConvert.SerializeObject(movie);
            Request(json, "PUT", "/Home/AddPut");

            movie.Description = "dlugi opis, za dlugi, dlugi opis za dlugi, dlugi opis...";
            json = JsonConvert.SerializeObject(movie);
            Request(json, "PUT", "/Home/AddPut");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Request(null, "POST", "/Home/AddPost");

            movie = new Movie();
            movie.Link = "InnyLink.pl";
            movie.ID = Int32.Parse(textBox1.Text.ToString());
            string json = JsonConvert.SerializeObject(movie);
            Request(json, "POST", "/Home/AddPost");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = Request(textBox1.Text.ToString(), "GET","/Home/GetDesc");
            //try
            //{
            //    var request = (HttpWebRequest)WebRequest.Create("http://localhost:51907/Home/AddPut");
            //    request.Method = "PUT";
            //    request.ContentType = "application/json";
            //    request.Accept = "application/json";

            //    Movie movie = new Movie();
            //    movie.Link = "www.film.pl";
            //    movie.Description = "Super filmidlo";
            //    movie.PictureLink = "link";
            //    string json = JsonConvert.SerializeObject(movie);

            //    ASCIIEncoding encoding = new ASCIIEncoding();
            //    Byte[] bytes = encoding.GetBytes(json);

            //    using (var requestStream = request.GetRequestStream())
            //    {
            //        requestStream.Write(bytes, 0, bytes.Length);
            //    }

            //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //    string returnString = response.StatusCode.ToString();
            //}
            //catch (Exception ex) { Console.WriteLine(ex); }
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = Request(textBox1.Text.ToString(), "GET", "/Home/GetLink");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            movie = new Movie();
            movie.ID = Int32.Parse(textBox1.Text);
            if (movie.ID == null)
                movie.ID = 1;
           string json = JsonConvert.SerializeObject(movie);

            textBox1.Text = Request(json, "DELETE", "/Home/DelFilm");
        }

        public string Request(string queryString, string method, string action)
        {
            Byte[] data = System.Text.Encoding.UTF8.GetBytes("");

            if (queryString != null)
            {
                data = System.Text.Encoding.UTF8.GetBytes(queryString);
            }

            string result = null;

            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    if (String.Equals(method, "GET"))
                    {
                        result = client.DownloadString("http://localhost:51907"+action+"/"+queryString);
                        //result = client.Encoding.GetString(response);
                    }
                    else
                    {
                        client.UploadData("http://localhost:51907" + action, method, data);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return result;
        }
    }
    public class Movie
    {
        public int ID { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string PictureLink { get; set; }
    }
}
