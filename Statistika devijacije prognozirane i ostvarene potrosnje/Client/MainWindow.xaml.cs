﻿using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //    public ObservableCollection<string> zem { get; set; }
        //     enum zemlje { SRB, BiH, CRO}

        
        public MainWindow()
        {
            DataContext = new cmbItems();
            
            InitializeComponent();

            Regioni.SelectedIndex = 0;
            SatiOd.SelectedIndex = 0;
            SatiDo.SelectedIndex = 24;
        }

        private void Prikazi_Click(object sender, RoutedEventArgs e)
        {

            // List<Trojke> source = new List<Trojke>();

            for(int i = 0; i< Tabela.Items.Count; i++)
            {
                Tabela.Items.RemoveAt(i);
            }
            Tabela.Items.Refresh();
            ChannelFactory<IServer> factory = new ChannelFactory<IServer>(
            new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:81/IServer"));
            IServer proxy = factory.CreateChannel();
            //tu puca
            var source = proxy.vratiTrojku(Regioni.SelectedItem.ToString(), Int32.Parse(SatiOd.SelectedItem.ToString()), Int32.Parse(SatiDo.SelectedItem.ToString()));

            var mStream = new MemoryStream();
            var binFormatter = new BinaryFormatter();
            
            mStream.Write(source, 0, source.Length);
            mStream.Position = 0;

            List<Trojke> trojke = binFormatter.Deserialize(mStream) as List<Trojke>;

            foreach (Trojke x in trojke)
            {
                Tabela.Items.Add(x);
            }

        }
    }
}
