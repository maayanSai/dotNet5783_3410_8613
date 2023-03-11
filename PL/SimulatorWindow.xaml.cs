using Microsoft.VisualBasic;
using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        void accepting(object? repAccept)
        {
            updateStatus="";
            order = ((ReportAcceptingObject)repAccept).Order;
            orderStatus=((ReportAcceptingObject)repAccept).Order.Status+1;
            acceptTime=((ReportAcceptingObject)repAccept).AcceptTime.ToString();
            endTreetmentTime=((ReportAcceptingObject)repAccept).TimeEndTreetment.ToString();
        }
        void endTreetment(object? reportEndTreetment)
        {
            order=((ReportEndTreetment)reportEndTreetment).Order;
            updateStatus=((ReportEndTreetment)reportEndTreetment).TreemenMassage;
        }
        void EndSimulator(object? str)
        {
            updateStatus=(string)str;
            Simulator.Simulator.ReportAccept1-=reportProgress;
            Simulator.Simulator.ReportEndTreetment2-=reportProgress;
            Simulator.Simulator.End3-=reportProgress;
            Thread.Sleep(3000);
            backroundWorker.CancelAsync();
        }
        void UpdateClock(object? stopw)
        {
            time=((Stopwatch)stopw).Elapsed.ToString(@"hh\:mm\:ss");
        }


        readonly static BlApi.IBl? bl = BlApi.Factory.Get();
        private Stopwatch stopwatch;
        BackgroundWorker backroundWorker;
        private bool isTimerRun;
        bool isSimulatorThreadEndWork=false;

        private Dictionary<int, Action<object?>> actions;

        static readonly DependencyProperty updateStatusDep = DependencyProperty.Register(nameof(updateStatus), typeof(string), typeof(Window));//לשנות שם גם במאחורה
        string updateStatus {get => (string)GetValue(updateStatusDep); set => SetValue(updateStatusDep, value); }

        static readonly DependencyProperty acceptTimeDep = DependencyProperty.Register(nameof(acceptTime), typeof(string), typeof(Window));
        string acceptTime { get => (string)GetValue(acceptTimeDep); set => SetValue(acceptTimeDep, value); }

        static readonly DependencyProperty endTreetmentTimeDep = DependencyProperty.Register(nameof(endTreetmentTime), typeof(string), typeof(Window));
        string endTreetmentTime { get => (string)GetValue(endTreetmentTimeDep); set => SetValue(endTreetmentTimeDep, value); }

        static readonly DependencyProperty orderDep = DependencyProperty.Register(nameof(order), typeof(BO.Order), typeof(Window));
        BO.Order? order { get => (BO.Order?)GetValue(orderDep); set => SetValue(orderDep, value); }

        static readonly DependencyProperty orderStatusDep = DependencyProperty.Register(nameof(orderStatus), typeof(BO.OrderStatus), typeof(Window));
        BO.OrderStatus? orderStatus { get => (BO.OrderStatus?)GetValue(orderStatusDep); set => SetValue(orderStatusDep, value); }

        static readonly DependencyProperty timeDep = DependencyProperty.Register(nameof(time), typeof(string), typeof(Window));
        string time { get => (string)GetValue(timeDep); set => SetValue(timeDep, value); }
        public SimulatorWindow()
        {
            actions=new();
            InitializeComponent();
            backroundWorker = new BackgroundWorker();
            backroundWorker.DoWork+=BackroundWorker_DoWork;
            backroundWorker.ProgressChanged += BackroundWorker_ProgressChanged;
            backroundWorker.RunWorkerCompleted+=BackroundWorker_WorkerCompleted;
            backroundWorker.WorkerReportsProgress = true;
            backroundWorker.WorkerSupportsCancellation = true;
            stopwatch=new();
            stopwatch.Start();
            backroundWorker.RunWorkerAsync();
        }
        public void AddAction(int key, Action<object?> value) => actions.Add(key, value);
        private void BackroundWorker_WorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            stopwatch.Stop();
           Thread.Sleep(3000);
            this.Close();
        }
        private void BackroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            if (actions.ContainsKey(e.ProgressPercentage))
                actions[e.ProgressPercentage]?.Invoke(e.UserState);
        }
        private void BackroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        { 
            if(backroundWorker.CancellationPending==false)
            {

               
                Simulator.Simulator.ReportAccept1 += reportProgress;
                Simulator.Simulator.ReportEndTreetment2 += reportProgress;
                Simulator.Simulator.End3 += reportProgress;
                
                //List of functions for the dictionary
                AddAction(1,  accepting);
                AddAction(2, (Action<object?>) endTreetment);
                AddAction(3, (Action<object?>)EndSimulator);
                AddAction(4, (Action<object?>)UpdateClock);
                Simulator.Simulator.activate();

                while (backroundWorker.CancellationPending==false)
                {
                    backroundWorker.ReportProgress(4,stopwatch);
                    Thread.Sleep(1000);
                }
            }
        }
        private void reportProgress(int progressPercentage, object? userState) => backroundWorker.ReportProgress(progressPercentage, userState);
        private void StopSimulator_Click(object? sender, RoutedEventArgs e)
        {
            Simulator.Simulator.stopSimulator();
        }
    }
}
