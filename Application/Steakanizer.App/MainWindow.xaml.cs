
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using VideoFrameAnalyzer;
using Common = Microsoft.ProjectOxford.Common;
using VisionAPI = Microsoft.ProjectOxford.Vision;

namespace Steakanizer
{
    public partial class MainWindow : System.Windows.Window
    {
        private readonly FrameGrabber<LiveCameraResult> _grabber;
        private DateTime _startTime;

        public MainWindow()
        {
            InitializeComponent();

            _grabber = SetupVideoFrameGrabber();

            // Create API client 
            //_visionClient = new VisionAPI.VisionServiceClient(Properties.Settings.Default.CustomVisionAPIKey, Properties.Settings.Default.CustomVisionAPI);

        }

        private FrameGrabber<LiveCameraResult> SetupVideoFrameGrabber()
        {
            // Create grabber. 
            var grabber = new FrameGrabber<LiveCameraResult>();

            // Set up a listener for when the client receives a new frame.
            grabber.NewFrameProvided += async (s, e) => await ShowFrame(s, e);

            // Set up a listener for when the client receives a new result from an API call.
            grabber.NewResultAvailable += ShowPrediction;

            // Set up the function which will call when an analysis is done
            grabber.AnalysisFunction = FrameAnalysis;

            return grabber;
        }

        private async Task ShowFrame(object sender, FrameGrabber<LiveCameraResult>.NewFrameEventArgs args)
        {
            await this.Dispatcher.BeginInvoke((Action)(() => LeftImage.Source = args.Frame.Image.ToBitmapSource()));

            // See if auto-stop should be triggered. 
            if (Properties.Settings.Default.AutoStopEnabled 
                && (DateTime.Now - _startTime) > Properties.Settings.Default.AutoStopTime)
            {
                await _grabber.StopProcessingAsync();
            }
        }

        private void ShowPrediction(object sender, FrameGrabber<LiveCameraResult>.NewResultEventArgs args)
        {
            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                if (args.TimedOut)
                {
                    MessageArea.Text = "API call timed out.";
                }
                else if (args.Exception != null)
                {
                    MessageArea.Text = string.Format("{0} API call failed on frame {1}. Exception: {2}", "Custom Vision", args.Frame.Metadata.Index, args.Exception.Message);
                }
                else
                {
                    /// TODO: Analyse ausgeben
                    /// args.Analysis;
                }
            }));
        }

        private async Task<LiveCameraResult> FrameAnalysis(VideoFrame frame)
        {
            // Encode image. 
            var jpg = frame.Image.ToMemoryStream(".jpg", new ImageEncodingParam(ImwriteFlags.JpegQuality, 60));
            
            // Submit image to API. 
            // var result = ...

            // Count the API call. 
            Properties.Settings.Default.CustomVisionAPICallCount++;

            // Output. 
            // var prediction = JsonConvert.DeserializeObject<...>(result.Result.ToString());

            return new LiveCameraResult
            {
                // TODO: Analyseergebnis zurückgeben...
            };
        }

        private void CameraList_Loaded(object sender, RoutedEventArgs e)
        {
            int numCameras = _grabber.GetNumCameras();

            if (numCameras == 0)
            {
                MessageArea.Text = "No cameras found!";
            }

            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = Enumerable.Range(0, numCameras).Select(i => string.Format("Camera {0}", i + 1));
            comboBox.SelectedIndex = 0;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CameraList.HasItems)
            {
                MessageArea.Text = "No cameras found; cannot start processing";
                return;
            }

            // Clean leading/trailing spaces in API keys. 
            Properties.Settings.Default.CustomVisionAPIKey = Properties.Settings.Default.CustomVisionAPIKey.Trim();

            // How often to analyze. 
            _grabber.TriggerAnalysisOnInterval(Properties.Settings.Default.AnalysisInterval);

            // Reset message. 
            MessageArea.Text = "";

            // Record start time, for auto-stop
            _startTime = DateTime.Now;

            await _grabber.StartProcessingCameraAsync(CameraList.SelectedIndex);
        }

        private async void StopButton_Click(object sender, RoutedEventArgs e)
        {
            await _grabber.StopProcessingAsync();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsPanel.Visibility = 1 - SettingsPanel.Visibility;
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsPanel.Visibility = Visibility.Hidden;
            Properties.Settings.Default.Save();
        }
    }
}