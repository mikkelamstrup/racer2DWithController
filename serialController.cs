using System;                    // Importerer grundlæggende C#-funktioner
using System.IO.Ports;           // Importerer funktionalitet til seriel kommunikation
using UnityEngine;               // Importerer Unity-specifikke funktioner

// Denne klasse håndterer seriel kommunikation mellem Unity og en ekstern enhed som en ESP32.
public class SerialController : MonoBehaviour
{
    public string portName = "/dev/cu.usbserial-0001"; // Skift den til den port som du bruger eks. "COM3"

    public int baudRate = 9600; // Skal matche baudraten på den esp32 firmware vi har uploadet

    // Variabel til at håndtere den serielle port.
    private SerialPort serialPort; // Opretter en variable til håndtering af den serielle port 

    public CarController car; // Henviser til scriptet i unity som styre bilens bevægelser

    void Start()
    {
        // Initialiserer og åbner den serielle port med det specificerede portnavn og baudrate.
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open(); 
    }

    // Metoden kaldes én gang per frame. Den bruger vi til at læse serielle data.
    void Update()
    {
        if (serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            try
            {
                // Læser en linje med data fra den serielle port.
                string data = serialPort.ReadLine(); // Læser en linje med data
                ProcessData(data); //Behandler den læste data
            }
            catch (Exception e)
            {
                Debug.LogError("Fejl ved læsning af seriel data: " + e.Message);
            }
        }
    }

    // Metoden behandler data, der modtages fra ESP32.
    void ProcessData(string data)
    {
        // Forventet format: "potValue,forward,backward"
        string[] values = data.Split(',');

        // Kontrollerer, om dataen indeholder præcis 3 værdier.
        if (values.Length == 3)
        {
            // Konverterer den første værdi (potentiometer-værdi) til en float.
            float potValue = float.Parse(values[0]);

            // Normaliserer potentiometerets værdi til et interval mellem -1 og 1.
            float rotation = (potValue - 2048) / 2048;

            // Bestemmer bilens bevægelse baseret på de to næste værdier (1 for fremad eller bagud).
            float move = (values[1] == "1" ? 1 : (values[2] == "1" ? -1 : 0));

            // Sender de beregnede inputs til bilens kontrolscript.
            car.SetRotationInput(rotation);
            car.SetMoveInput(move);
        }
    }

    // Metoden kaldes, når objektet ødelægges (f.eks. når spillet lukkes eller genindlæses).
    void OnDestroy()
    {
        // Lukker den serielle port, hvis den er åben, for at frigive ressourcer.
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}