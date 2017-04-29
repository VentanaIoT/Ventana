#if UNITY_EDITOR
using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BluetoothSerialClass {
    public class BluetoothComponent : Singleton<BluetoothComponent> {

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}

#elif WINDOWS_UWP
using System;
using System.Linq;
using UnityEngine;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth.Rfcomm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using HoloToolkit.Unity;
using Windows.Devices.Bluetooth;

namespace BluetoothSerialClass {
    public class BluetoothComponent {

        private RfcommDeviceService _service;
        ObservableCollection<PairedDeviceInfo> _pairedDevices;
        private StreamSocket _socket;
        private DataWriter dataWriterObject;
        private DataReader dataReaderObject;
        private bool isConnected = false;
        



        async public Task ConnectDevice(string name) {
            
            //Revision: No need to requery for Device Information as we alraedy have it:

            DeviceInformationCollection services = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));
            BluetoothDevice.GetDeviceSelectorFromConnectionStatus(BluetoothConnectionStatus.Connected);
            foreach ( DeviceInformation device in services ) {
                Debug.Log(device.Id);
                if ( device.Name == "Dev B" ) {
                    _service = await RfcommDeviceService.FromIdAsync(device.Id);
                }
            }

            /*string selector = BluetoothDevice.GetDeviceSelectorFromPairingState(true);
            Debug.Log(selector);
            DeviceWatcher deviceWatcher = DeviceInformation.CreateWatcher(selector, null, DeviceInformationKind.AssociationEndpoint);
            deviceWatcher.Added += DeviceWatcher_Added;
            deviceWatcher.Start();
            */


        }

        private async void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args) {
            if (!args.Pairing.IsPaired && args.Pairing.CanPair ) {
                var pairingResult = await args.Pairing.PairAsync(DevicePairingProtectionLevel.None);
                Debug.Log(pairingResult.Status);
            }
            if (args.Pairing.IsPaired && args.Name == "HC-06" ) {
                //setup a way to read from it...
                
                bool success = true;
                try {
                    
                    //var list = device.GetRfcommServicesAsync();
                    //Debug.Log(list);
                    

                    if ( _socket != null ) {
                        // Disposing the socket with close it and release all resources associated with the socket
                        _socket.Dispose();
                    }

                    _socket = new StreamSocket();
                    try {
                        // Note: If either parameter is null or empty, the call will throw an exception
                        await _socket.ConnectAsync(_service.ConnectionHostName, _service.ConnectionServiceName);
                    }
                    catch ( Exception ex ) {
                        success = false;
                        System.Diagnostics.Debug.WriteLine("Connect:" + ex.Message);
                    }
                    // If the connection was successful, the RemoteAddress field will be populated
                    if ( success ) {
                        string msg = String.Format("Connected to {0}!", _socket.Information.RemoteAddress.DisplayName);
                        //MessageDialog md = new MessageDialog(msg, Title);
                        System.Diagnostics.Debug.WriteLine(msg);
                        //await md.ShowAsync();
                    }
                }
                catch ( Exception ex ) {
                    System.Diagnostics.Debug.WriteLine("Overall Connect: " + ex.Message);
                    _socket.Dispose();
                    _socket = null;
                }

            }
        }

        public async Task InitializeRfcommDeviceService() {
            try {
                DeviceInformationCollection DeviceInfoCollection = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));


                var numDevices = DeviceInfoCollection.Count();

                // By clearing the backing data, we are effectively clearing the ListBox
                _pairedDevices = new ObservableCollection<PairedDeviceInfo>();
                _pairedDevices.Clear();
                Debug.Log(numDevices);
                if ( numDevices == 0 ) {
                    //MessageDialog md = new MessageDialog("No paired devices found", "Title");
                    //await md.ShowAsync();
                    System.Diagnostics.Debug.WriteLine("InitializeRfcommDeviceService: No paired devices found.");
                } else {
                    // Found paired devices.
                    foreach ( var deviceInfo in DeviceInfoCollection ) {
                        Debug.Log("DEVICE: " + deviceInfo.Name);
                        _pairedDevices.Add(new PairedDeviceInfo(deviceInfo));
                    }
                }
            }
            catch ( Exception ex ) {
                System.Diagnostics.Debug.WriteLine("InitializeRfcommDeviceService: " + ex.Message);
            }
        }
    }

    public class PairedDeviceInfo {
        internal PairedDeviceInfo(DeviceInformation deviceInfo) {
            this.DeviceInfo = deviceInfo;
            this.ID = this.DeviceInfo.Id;
            this.Name = this.DeviceInfo.Name;
        }

        public string Name { get; private set; }
        public string ID { get; private set; }
        public DeviceInformation DeviceInfo { get; private set; }
    }
}

#endif