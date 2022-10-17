using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using RestSharp;
using ClientLib;
using Newtonsoft.Json;

namespace ClientDesktopApp {
    public delegate List<Client> GetClients();

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly static string WEBSERVER_URL = "http://localhost:58988/";

        private static int jobsDone = 0;
        private static string pythonCode;

        public MainWindow() {
            InitializeComponent();
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) {
                pythonCode = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private async void LookForNewClients() {

            Task<List<Client>> task = new Task<List<Client>>(GetClients);
        }

        private List<Client> GetClients() {
            List<Client> clientList = new List<Client>();

            RestClient restClient = new RestClient(WEBSERVER_URL);
            RestRequest restRequest = new RestRequest("api/Clients/", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);

            return JsonConvert.DeserializeObject<List<Client>>(restResponse.Content);
        }
    }
}
