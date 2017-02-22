using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour {
//	public delegate void SerialDataReceivedEventHandler(string message);
//	public event SerialDataReceivedEventHandler OnDataReceived = delegate{};

	public string portName = "/dev/tty.usbmodem1421";
	public int baudRate    = 9600;

	private SerialPort serialPort_;
	private Thread thread_;
	private bool isRunning_ = false;

	private string message_;
	private bool isNewMessageReceived_ = false;

	public bool switch1 = false;
	public bool switch2 = false;
	public bool switch3 = false;
	public float horizontal = 0.0f;
	public float vertical = 0.0f;

	void Awake(){
		Open();
	}

	void Update(){
		if (isNewMessageReceived_) {
			OnDataReceived(message_);
		}
	}

	public void CloseSerialIfOpen(){
		if (isRunning_ == true) {
			Close ();
		}
	}
	public void OpenSerialIfClose(){
		if (isRunning_ == false) {
			Open ();
		}
	}

	void OnDestroy(){
		Close();
	}

	private void Open(){
		serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
		serialPort_.Open();

		isRunning_ = true;

		thread_ = new Thread(Read);
		thread_.Start();
	}

	private void Close(){
		isRunning_ = false;

		if (thread_ != null && thread_.IsAlive) {
			thread_.Join();
		}

		if (serialPort_ != null && serialPort_.IsOpen) {
			serialPort_.Close();
			serialPort_.Dispose();
		}
	}

	private void Read(){
		while (isRunning_ && serialPort_ != null && serialPort_.IsOpen) {
			try {
				if (serialPort_.BytesToRead > 0) {
					message_ = serialPort_.ReadLine();
					isNewMessageReceived_ = true;
				}
			} catch (System.Exception e) {
				Debug.LogWarning(e.Message);
			}
		}
	}

	void OnDataReceived(string message){
		string[] data = message.Split(
			new string[]{"\t"}, System.StringSplitOptions.None);
		if (data.Length < 5) return;

		try {
			switch1 = data[0] == "1" ? true : false;
			switch2 = data[1] == "1" ? true : false;
			switch3 = data[2] == "1" ? true : false;
			horizontal = float.Parse(data[3]);
			if(horizontal > -100 && horizontal < 100){
				horizontal = 0;
			}
			vertical = float.Parse(data[4]);
			if(vertical > -100 && vertical < 100){
				vertical = 0;
			}
		} catch (System.Exception e) {
			Debug.LogWarning(e.Message);
		}
	}
}
