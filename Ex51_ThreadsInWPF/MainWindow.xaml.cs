using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;


namespace Ex51_ThreadsInWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Global threads and tempLabels
        private Thread[] sensorThreads = new Thread[10];
        public Label[] tempLabels;


        public MainWindow()
        {
            InitializeComponent();
            //Initialise tempLabels
            tempLabels = new Label[10] { Temp1, Temp2, Temp3, Temp4, Temp5, Temp6, Temp7, Temp8, Temp9, Temp10};
        }

        private void Sensor(Object o)
        {
            //Never ending loop
            bool SensorsRunning = true;
            Label l = (Label) o;
            Random r = new Random();
            //Could be while (true)
            while (SensorsRunning)
            {
                //Wait 1 second before updateing temp in UI
                Thread.Sleep(1000);
                //Calculate temperature
                double temp = 10 + r.NextDouble() * 15;

                //Dispatch calculated temperature to the label using the label Array
                l.Dispatcher.BeginInvoke(new Action(() => { l.Content = temp.ToString(); }));
            }
        }

        private void StartThread_Click(object sender, RoutedEventArgs e)
        {
                //Start the Sensors
                CreateSensor();
        }

        private void CreateSensor()
        {
            //Create 10 Threads
            for (int i = 0; i < 10; i++)
            {
                sensorThreads[i] = new Thread(Sensor);
                sensorThreads[i].Start(tempLabels[i]);
            }
        }
    }
}
