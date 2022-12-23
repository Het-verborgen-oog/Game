using ArduinoControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using UnityEngine;

public class ArduinoControls : MonoBehaviour, IArduinoData
{
    [SerializeField]
    private int baudRate = 9600;
    [SerializeField]
    char StartMarker = '@';
    [SerializeField]
    char EndMarker = '#';
    [SerializeField]
    char PayloadMarker = ':';
    private SerialPort serialPort { get; set; }

    private MessageCreator messageCreator;

    [SerializeField]
    private string[] Message;
    /// <summary>
    /// The dictionary where the data is stored.
    /// </summary>
    public Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
    private int HorizontalTilt, VerticalTilt, Rotations;

    private string inboundMessage;

    //const float MinimumTilt = -1f, MaximumTilt = 1f, MinimumSpeed = 1f, MaximumSpeed = 2.5f;
    //const float ExpectedMinimumTilt = 0f, ExpectedMaximumTilt = 1024f, ExpectedMinumumSpeed = 0f, ExpectedMaximumSpeed = 5f;

    public struct MeasureData
    {
        public readonly float InputMinimum;
        public readonly float InputMaximum;
        public readonly float OutputMinimum;
        public readonly float OutputMaximum;

        public MeasureData(float inputMinimum, float inputMaximum, float outputMinimum, float outputMaximum)
        {
            InputMinimum = inputMinimum;
            InputMaximum = inputMaximum;
            OutputMinimum = outputMinimum;
            OutputMaximum = outputMaximum;
        }

        public static string[] GetPropertyNames()
        {
            return new string[] { nameof(InputMinimum), nameof(InputMaximum), nameof(OutputMinimum), nameof(OutputMaximum) };
        }

        public float[] GetPropertyValues()
        {
            return new float[] { InputMinimum, InputMaximum, OutputMinimum, OutputMaximum };
        }
    }

    public MeasureData RollData;
    public MeasureData PitchData;
    public MeasureData SpeedData;

    public enum MeasureDataIndex
    {
        Roll = 0,
        Pitch = 1,
        Speed = 2
    }
    public MeasureData[] DataCollection;

    public float Roll_Raw { get { return keyValuePairs["HRZ"]; } }
    public float Pitch_Raw { get { return keyValuePairs["HRZ"]; } }
    public float Speed_Raw { get { return keyValuePairs["HRZ"]; } }


    public float Roll { get { return (keyValuePairs["HRZ"] - RollData.InputMinimum) / (RollData.InputMaximum - RollData.InputMinimum) * (RollData.OutputMaximum - RollData.OutputMinimum) + RollData.OutputMinimum; } }

    public float Pitch { get { return (keyValuePairs["VER"] - PitchData.InputMinimum) / (PitchData.InputMaximum - PitchData.InputMinimum) * (PitchData.OutputMaximum - PitchData.OutputMinimum) + PitchData.OutputMinimum; } }

    public float Speed { get { return (keyValuePairs["SPD"] - SpeedData.InputMinimum) / (SpeedData.InputMaximum - SpeedData.InputMinimum) * (SpeedData.OutputMaximum - SpeedData.OutputMinimum) + SpeedData.OutputMinimum; } }

    /// <summary>
    /// This one is a test function
    /// </summary>
    //public void Start()
    //{
    //    GrabSettings();
    //    PrepareExternal();
    //}

    public void Start()
    {
        string[] ports = GetPorts();
        if (ports.Length > 0)
        {
            serialPort = new SerialPort(ports[0]);
        }
        messageCreator = new MessageCreator(EndMarker, StartMarker);
        serialPort.BaudRate = baudRate;

        PrepareCommands();
        PrepareExternal();
        GrabSettings();
        Connect();

        if (serialPort.IsOpen == false)
        {
            throw new Exception("Hardware could not connect!");
        }
    }

    public void OnDestroy()
    {
        Disconnect();
    }

    private void Update()
    {
        if (ReadMessage())
        {
            ProcessMessage();
        }
    }

    /// <summary>
    /// Prepares the dictionary.
    /// </summary>
    private void PrepareCommands()
    {
        keyValuePairs.Add("HRZ", HorizontalTilt);
        keyValuePairs.Add("VER", VerticalTilt);
        keyValuePairs.Add("SPD", Rotations);
    }

    private void PrepareExternal()
    {
        DataCollection = new MeasureData[] { RollData, PitchData, SpeedData };
    }

    // =============== //
    //  SAVE SETTINGS  //
    // =============== //

    /// <summary>
    /// Grabs all the required settings upon initialisation
    /// </summary>
    private void GrabSettings()
    {
        if(!PlayerPrefs.HasKey(nameof(RollData)+nameof(RollData.OutputMaximum)))
        {
            CreateSettings();
            return;
        }

        RollData = LoadDataSet(nameof(RollData));
        PitchData = LoadDataSet(nameof(PitchData));
        SpeedData = LoadDataSet(nameof(SpeedData));
    }

    private void CreateSettings()
    {
        RollData = new MeasureData(0f, 1023f, -1f, 1f);
        PitchData = new MeasureData(0f, 1023f, -1f, 1f);
        SpeedData = new MeasureData(0f, 5f, 1f, 2.5f);

        SaveDataSet(nameof(RollData),RollData);
        SaveDataSet(nameof(PitchData), PitchData);
        SaveDataSet(nameof(SpeedData), SpeedData);
    }

    private MeasureData LoadDataSet(string dataSet)
    {
        if (dataSet == null)
        {
            throw new ArgumentNullException(nameof(dataSet));
        }
        if (string.IsNullOrEmpty(dataSet) || string.IsNullOrWhiteSpace(dataSet))
        {
            throw new ArgumentException(nameof(dataSet));
        }

        string[] keys = MeasureData.GetPropertyNames(); // Look, it works FINE.
        List<float> values = new List<float>();

        foreach (string item in keys)
        {
            values.Add(LoadData(dataSet, item));
        }

        //Should be an enum, but at this stage of the project I physically do not care enough.
        return new MeasureData(values[0], values[1], values[2], values[3]);
    }

    public void Save(MeasureDataIndex index)
    {
        string dataset = "";
        switch (index)
        {
            case MeasureDataIndex.Roll:
                dataset = nameof(RollData);
                break;
            case MeasureDataIndex.Pitch:
                dataset = nameof(PitchData);
                break;
            case MeasureDataIndex.Speed:
                dataset = nameof(SpeedData);
                break;
            default:
                break;
        }

        SaveDataSet(dataset, DataCollection[(int)index]);
    }

    private void SaveDataSet(string dataSet, MeasureData data)
    {
        string[] keys = MeasureData.GetPropertyNames();
        float[] values = data.GetPropertyValues();

        if (keys.Length != values.Length) throw new Exception("Data set aspects have changed!");

        for (int i = 0; i < keys.Length; i++)
        {
            SaveData(dataSet, keys[i], values[i]);
        }
    }

    /// <summary>
    /// Saves data for calibration to the PlayerPrefs API
    /// </summary>
    /// <param name="dataSet">The data set to access, e.g. RollData</param>
    /// <param name="key">The key to save to, e.g. InputMinimum</param>
    /// <param name="data">The data to store</param>
    private void SaveData(string dataSet, string key, float data)
    {
        if (dataSet == null)
        {
            throw new ArgumentNullException(nameof(dataSet));
        }
        if (string.IsNullOrEmpty(dataSet) || string.IsNullOrWhiteSpace(dataSet))
        {
            throw new ArgumentException(nameof(dataSet));
        }

        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }
        if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException(nameof(key));
        }

        PlayerPrefs.SetFloat(dataSet + key, data);
    }

    /// <summary>
    /// Loads data for calibration to the PlayerPrefs API
    /// </summary>
    /// <param name="dataSet">The data set to access, e.g. RollData</param>
    /// <param name="key">The key to save to, e.g. InputMinimum</param>
    /// <returns>The loaded data, or 0.</returns>
    private float LoadData(string dataSet, string key)
    {
        return PlayerPrefs.GetFloat(dataSet + key);
    }

    // =========== //
    // WORK AROUND //
    // =========== //

    public float GrabRawProperty(MeasureDataIndex data)
    {
        switch (data)
        {
            case MeasureDataIndex.Roll:
                return Roll_Raw;
            case MeasureDataIndex.Pitch:
                return Pitch_Raw;
            case MeasureDataIndex.Speed:
                return Speed_Raw;
            default:
                return -1;
        }
    }

    // ================== //
    // MESSAGE PROCESSING //
    // ================== //

    enum CommandStructure
    {
        Identifier = 0,
        Payload = 1
    }

    private void ProcessMessage()
    {
        if (Int32.TryParse(Message[(int)CommandStructure.Payload], out int payload))
        {
            keyValuePairs[Message[(int)CommandStructure.Identifier]] = payload;
        }
    }   

    /// <summary>
    /// Connects to the serial port.
    /// </summary>
    public void Connect()
    {
        if (serialPort.IsOpen == false)
        {
            string[] ports = GetPorts();
            serialPort = new SerialPort(ports[0]);

            serialPort.Open();
            if (serialPort.IsOpen)
            {
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();
            }
        }
    }

    /// <summary>
    /// Disconnects from the serial port.
    /// </summary>
    public void Disconnect()
    {
        if (serialPort == null) return;
        else if (string.IsNullOrEmpty(serialPort.PortName) || string.IsNullOrWhiteSpace(serialPort.PortName)) return;
        else if (serialPort.IsOpen == true) serialPort.Close();
    }

    /// <summary>
    /// Sends a message via the serial port to the arduino.
    /// </summary>
    /// <param name="message">The message that will be sent.</param>
    /// <returns>Whether it was successful.</returns>
    public new bool SendMessage(string message)
    {
        if (serialPort.IsOpen == true)
        {
            messageCreator.Add(message);
            serialPort.Write(message);
            return true;
        }
        messageCreator.Reset();
        return false;
    }

    /// <summary>
    /// Reads a message from the serial port.
    /// </summary>
    /// <returns>Whether it was successful.</returns>

    // Development note: Code could be improved with the use of "serialPort.ReadExistingLine()", but I didn't have time to implement this.
    public bool ReadMessage()
    {
        if (serialPort != null && serialPort.IsOpen == true && serialPort.BytesToRead > 0)
        {
            string readMessage = serialPort.ReadLine();

            if (readMessage.Contains(EndMarker))
            {
                inboundMessage += readMessage;
                int endIndex = inboundMessage.IndexOf(EndMarker);
                int startIndex = inboundMessage.IndexOf(StartMarker);

                if (startIndex == -1 || endIndex == -1 || startIndex == inboundMessage.Length - 1)
                {
                    inboundMessage = "";
                    return false;
                }

                inboundMessage = inboundMessage.Remove(endIndex);

                if (1 > inboundMessage.Length - startIndex)
                {
                    return false;
                }

                inboundMessage = inboundMessage.Remove(startIndex, 1);
                inboundMessage = inboundMessage.Trim();
                Message = inboundMessage.Split(PayloadMarker, StringSplitOptions.RemoveEmptyEntries);
                inboundMessage = "";
            }
            else
            {
                inboundMessage += readMessage;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets the list of ports available to the program.
    /// </summary>
    /// <returns>An array of strings, which contains the available ports.</returns>
    public string[] GetPorts()
    {
        return SerialPort.GetPortNames();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="portName">The new port.</param>
    /// <returns>True is successful, false if failed.</returns>
    public bool ChangePort(string portName)
    {
        if (string.IsNullOrEmpty(portName))
        {
            return false;
        }
        serialPort.PortName = portName;
        return true;
    }

    public bool isConnected()
    {
        if (serialPort == null) return false;
        else return serialPort.IsOpen;
    }
}
