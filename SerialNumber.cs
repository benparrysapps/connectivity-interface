
using RFID_Sample;

using System;
namespace BackRoomProject
{
    public class SerialNumber
    {
        public String serialNumber { get; set; }
        internal static string GetSerialNumber()
        {
            String debug_line = "#################################################";
            Console.WriteLine(debug_line);
            Console.WriteLine("GetSerialNumber Running...");
            Console.WriteLine(debug_line);

            //Create a new ReaderDLL object
            ReaderDLL reader = new ReaderDLL();

            //Define Variables for parameters of following methods
            Int32 ReaderHandle = -1;
            byte CardType = 0;
            Int32 Status = 0;
            UInt16 CardTypeMask = 0xFFFF;
            int ret_val = -1;
            UInt32 productID = 0;
            byte[] uidArray = new byte[64];
            byte length = 0;
            string uid = "";
            UInt16 off = 0x000;
            UInt16 green = 0x001;
            UInt16 red = 0x002;
            UInt16 orange = 0x003;
            UInt16 buzzer = 0x004;

            Console.WriteLine("Opening USB Session...");
            Console.WriteLine(debug_line);

            //Check and open serial connection 
            reader.brp_open_usb_session(ref ReaderHandle, productID);

            Console.WriteLine("Reader Handle: " + ReaderHandle);
            Console.WriteLine(debug_line);
                
            Console.WriteLine("Selecting card...");
            Console.WriteLine(debug_line);

            //Select next card in field
            reader.vhl_select(ReaderHandle, CardTypeMask, true, false, ref CardType, ref Status);

            Console.WriteLine("Reader Handle: " + ReaderHandle);
            Console.WriteLine(debug_line);


            Console.WriteLine("Getting the serial number...");
            Console.WriteLine(debug_line);

            //Get the serial number of the selected card
            ret_val = reader.vhl_get_snr(ReaderHandle, ref uidArray, ref length, ref Status);

            Console.WriteLine("Reader Handle: " + ReaderHandle);
            Console.WriteLine(debug_line);

            //Check to see if an error occurred and set the responce as a string
            String error = reader.getStringFromErrorCode(ret_val);

            Console.WriteLine("Error: " + error);
            Console.WriteLine(debug_line);

            if (error == "No error occured")
            {
                for (int x = length; x > 0; x--)
                {
                    //Format uid from 
                    uid += string.Format("{0:X2}", uidArray[x - 1]);
                }
                //Change the LED colour to Red to show succsessful scan
                reader.syscmd_set_port(ReaderHandle, green, ref Status);


                //Play buzz sound to indicate a scan has taken place
                reader.syscmd_set_port(ReaderHandle, buzzer, ref Status);
                Thread.Sleep(500);
            }
            else if (error == "The reader returned a statuscode != 0")
            {
                uid = "No Card Detected";
                //Change the LED colour to Red to show error
                reader.syscmd_set_port(ReaderHandle, red, ref Status);

                //Play buzz sound to indicate a scan has taken place
                reader.syscmd_set_port(ReaderHandle, buzzer, ref Status);
                Thread.Sleep(500);
            }
            else
            {
                uid = error;
                //Change the LED colour to Red to show error
                reader.syscmd_set_port(ReaderHandle, red, ref Status);

                //Play buzz sound to indicate a scan has taken place
                reader.syscmd_set_port(ReaderHandle, buzzer, ref Status);
                Thread.Sleep(500);
            }

            Console.WriteLine("Return value: " + uid);
            Console.WriteLine(debug_line);

            //Turn the LED on the Reader off
            reader.syscmd_set_port(ReaderHandle, off, ref Status);

            //Close USB session
            reader.brp_close_session(ReaderHandle);

            return uid;
        }
    }


}
