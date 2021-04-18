
using UnityEngine;
using TMPro;
public class Arduino : MonoBehaviour
{
    public SerialController serialController;
    public Material colorLed;
    public Light onLed;
    public Light txLed;
    public Light lLed;
    public Collider coll;
    public Camera camera_;
    public TextMeshPro millis;
    public TextMeshPro connections;
    void Start()
    {  
        onLed.enabled = false;
        txLed.enabled = false;
        lLed.enabled = false;
        //  if (coll == null)
        //     Debug.Log(212);
        // coll = coll.GetComponent<Collider>();
        // serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        // colorLed = GameObject.Find("Material").GetComponent<Material>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera_.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (coll.Raycast(ray, out hit, 100))
            {
                serialController.SendSerialMessage("R");
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            serialController.SendSerialMessage("H");
            lLed.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            serialController.SendSerialMessage("L");
            lLed.enabled = false;

        }
        string message = serialController.ReadSerialMessage();
        if (message == null)
            return;


        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
        {
            onLed.enabled = true;
            txLed.enabled = true;
            connections.SetText("Connect");
            }
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
        { 
            onLed.enabled = false;
            txLed.enabled = false;
            millis.SetText("Millis: 0");
            connections.SetText("Disconnect");
        }
        else if (message == "H")
            lLed.enabled = true;
        else if (message == "L")
            lLed.enabled = false;
        else
            millis.SetText("Millis: " + message);
    }

}
