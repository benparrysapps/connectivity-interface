using System;
using System.Runtime.InteropServices;

namespace RFID_Sample
{

    public class ReaderDLL
    {
        #region Constants and Definitions

        /// <summary>
        /// DLL function return codes
        /// </summary>
        public const Int32 BRP_OK = 0x00;                      // no error occured
        public const Int32 BRP_ERR_STATUS = 0x01;              // the reader returned a statuscode != 0
        public const Int32 BRP_ERR_BUSY = 0x02;                // the reader is working up a command
        public const Int32 BRP_ERR_IDLE = 0x03;                // the reader is waiting for a command
        public const Int32 BRP_ERR_TIMEOUT = 0x04;             // the response/alivemsg timed out
        public const Int32 BRP_ERR_CORRUPTED_FRAME = 0x05;     // the reader sent a corrupted frame
        public const Int32 BRP_ERR_UNEXPECTED_FRAME = 0x06;    // the reader sent a frame during idle state
        public const Int32 BRP_ERR_GENERAL_IO = 0x07;          // the reader is not found or the underlying USB connection generated an error
        public const Int32 BRP_ERR_BUFFER_OVERFLOW = 0x08;     // the reader sends more than max_resp_len bytes or
                                                               // the application tries to send more than send_bufsize
                                                               // bytes (see brp_setup_session)
        public const Int32 BRP_ERR_NO_MORE_HANDLES = 0x09;     // there are no free brp handles
        public const Int32 BRP_ERR_INSUFFICIENT_MEM = 0x0A;    // there is not enough memory to create a new BRP Handle
        public const Int32 BRP_ERR_WRONG_HANDLE = 0x0B;        // the handle specified is not present
        public const Int32 BRP_ERR_WRONG_PARAMETERS = 0x0C;    // the parameters of a command were not right
        public const Int32 BRP_ERR_RS485_NO_RESPONSE = 0x0D;   // the rs485 device is not connected or has a invalid address
        public const Int32 BRP_ERR_CRYPTO = 0x0E;              // invalid crypto key / encrypted command/response is invalid
        public const Int32 BRP_DLL_NOT_FOUND = 0xFF;           // the BrpDriver DLL is not available


        /// <summary>
        /// DLL function return codes
        /// </summary>
        public const Int32 BRP_CHECKSUM_NONE = 0x00;                 // no checksum  (default)
        public const Int32 BRP_CHECKSUM_BCC8 = 0x01;                 // checksum BCC8
        public const Int32 BRP_CHECKSUM_CRC16 = 0x02;                 // checksum CRC16
        public const Int32 BRP_CHECKSUM_BCC16 = 0x03;                 // checksum BCC16

        /// <summary>
        /// defines stati of reader
        /// </summary> 
        /// 
        public const Int32 STATUS_OK = 0x00;
        public const Int32 VHL_ERR_NOTAG = 0x0101;
        public const Int32 VHL_ERR_CARD_NOT_SELECTED = 0x0102;
        public const Int32 VHL_ERR_HF = 0x0103;
        public const Int32 VHL_ERR_CONFIG_ERR = 0x0104;
        public const Int32 VHL_ERR_AUTH = 0x0105;
        public const Int32 VHL_ERR_READ = 0x0106;
        public const Int32 VHL_ERR_WRITE = 0x0107;
        public const Int32 VHL_ERR_CONFCARD_READ = 0x0108;
        public const Int32 VHL_ERR_INVALID_CARD_FAMILY = 0x0109;
        public const Int32 VHL_ERR_NOT_SUPPORTED = 0x010A;

        public const Int32 AUTOREAD_ERR_NO_MESSAGE = 0x0501;
        public const Int32 AUTOREAD_ERR_SCRIPT_RUNTIME = 0x0502;
        public const Int32 AUTOREAD_ERR_SCRIPT_SYNTAX = 0x0503;
        public const Int32 AUTOREAD_ERR_SCRIPT_NOT_IMPL = 0x0504;
        public const Int32 AUTOREAD_ERR_AR_DISABLED = 0x0510;

        /// <summary>
        /// defines LED colors
        /// </summary>
        public const UInt16 LED_OFF = 0x000;
        public const UInt16 LED_GREEN = 0x001;
        public const UInt16 LED_RED = 0x002;
        public const UInt16 LED_ORANGE = 0x003;
        public const UInt16 BUZZER = 0x004;

        #endregion

        #region members
        bool is64BitProcess = true;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public ReaderDLL()
        {
            is64BitProcess = (IntPtr.Size == 8);
        }

        /// <summary>
        /// Returns the corrosponding error text.
        /// </summary>
        /// <param name="errorCode">Error code value</param>
        /// <returns>Returns error description.</returns>
        public string getStringFromErrorCode(Int32 errorCode)
        {
            string retval = "Unknown error code";

            switch (errorCode)
            {
                case BRP_OK:
                    retval = "No error occured";
                    break;

                case BRP_ERR_STATUS:
                    retval = "The reader returned a statuscode != 0";
                    break;

                case BRP_ERR_BUSY:
                    retval = "The reader is working up a command";
                    break;

                case BRP_ERR_IDLE:
                    retval = "The reader is waiting for a command";
                    break;

                case BRP_ERR_TIMEOUT:
                    retval = "The response/alivemsg timed out";
                    break;

                case BRP_ERR_CORRUPTED_FRAME:
                    retval = "The reader sent a corrupted frame";
                    break;

                case BRP_ERR_UNEXPECTED_FRAME:
                    retval = "The reader sent a frame during idle state";
                    break;

                case BRP_ERR_GENERAL_IO:
                    retval = "The reader is not found or the underlying USB connection generated an error";
                    break;

                case BRP_ERR_BUFFER_OVERFLOW:
                    retval = "The reader sends more than max_resp_len bytes or the application tries to send more than send_bufsize bytes (see brp_setup_session)";
                    break;

                case BRP_ERR_NO_MORE_HANDLES:
                    retval = "There are no free brp handles";
                    break;

                case BRP_ERR_INSUFFICIENT_MEM:
                    retval = "There is not enough memory to create a new BRP Handle";
                    break;

                case BRP_ERR_WRONG_HANDLE:
                    retval = "The handle specified is not present";
                    break;

                case BRP_ERR_WRONG_PARAMETERS:
                    retval = "The parameters of a command were not right";
                    break;

                case BRP_ERR_RS485_NO_RESPONSE:
                    retval = "The rs485 device is not connected or has a invalid address";
                    break;

                case BRP_ERR_CRYPTO:
                    retval = "Invalid crypto key / encrypted command/response is invalid";
                    break;

                case BRP_DLL_NOT_FOUND:
                    retval = "The BrpDriver DLL is not available";
                    break;

                default:
                    retval = "Unknown error code";
                    break;
            }
            return retval;
        }

        /// <summary>
        /// Open USB session and return handle if Reader is connected
        /// </summary>
        /// <param name="Handle">Output parameter. Returns handle of reader</param>
        /// <param name="productID">should always be zero</param>
        /// <returns>Returns 0 on success</returns>
        public Int32 brp_open_usb_session(ref Int32 Handle, UInt32 productID)
        {
            try
            {
                if (is64BitProcess)
                {
                    return ReaderDllx64.brp_open_usb_session(ref Handle, productID);
                }
                else
                {
                    return ReaderDllx86.brp_open_usb_session(ref Handle, productID);
                }
            }
            catch (DllNotFoundException)
            {
                Handle = -1;
                return BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                Handle = -1;
                return BRP_ERR_GENERAL_IO;
            }
        }

        /// <summary>
        /// Open SERIAL session and return handle if Reader is connected
        /// </summary>
        /// <param name="Handle">Output parameter. Returns handle of reader</param>
        /// <param name="com_port">com port number</param>
        /// <param name="ser_baudrate_baudrate">baudrate speed</param>
        /// <param name="ser_parity_parity">parity</param>
        ///    /// <returns>Returns 0 on success</returns>
        public Int32 brp_open_serial_session(ref Int32 Handle, Int32 com_port, int ser_baudrate_baudrate, int ser_parity_parity)
        {
            try
            {
                if (is64BitProcess)
                {
                    return ReaderDllx64.brp_open_serial_session(ref Handle, com_port, ser_baudrate_baudrate, ser_parity_parity);
                }
                else
                {
                    return ReaderDllx86.brp_open_serial_session(ref Handle, com_port, ser_baudrate_baudrate, ser_parity_parity);
                }
            }
            catch (DllNotFoundException)
            {
                Handle = -1;
                return BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                Handle = -1;
                return BRP_ERR_GENERAL_IO;
            }
        }

        /// <summary>
        /// Close USB session and free resources
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <returns></returns>
        public Int32 brp_close_session(Int32 Handle)
        {
            try
            {
                if (is64BitProcess)
                {
                    return ReaderDllx64.brp_close_session(Handle);
                }
                else
                {
                    return ReaderDllx86.brp_close_session(Handle);
                }
            }
            catch (DllNotFoundException)
            {
                return BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                if (Handle != 0)
                {
                    return BRP_ERR_WRONG_HANDLE;
                }
                else
                {
                    return BRP_ERR_GENERAL_IO;
                }
            }
        }


        /// <summary>
        /// Set Checksumm (only necessary for RS232 connection)
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="checksum">checksum algorithm , see defines</param>
        /// <returns></returns>
        public Int32 brp_set_checksum(Int32 Handle, Int32 checksum)
        {
            try
            {
                if (is64BitProcess)
                {
                    return ReaderDllx64.brp_set_checksum(Handle, checksum);
                }
                else
                {
                    return ReaderDllx86.brp_set_checksum(Handle, checksum);
                }
            }
            catch (DllNotFoundException)
            {
                return BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                if (Handle != 0)
                {
                    return BRP_ERR_WRONG_HANDLE;
                }
                else
                {
                    return BRP_ERR_GENERAL_IO;
                }
            }
        }


        /// <summary>
        /// Reboots the reader.
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="Status">Output parameter: Returns status code of reader</param>
        /// <returns>Returns 0 on success</returns>
        public Int32 syscmd_reset(Int32 Handle, ref Int32 Status)
        {

            try
            {
                if (is64BitProcess)
                {
                    return ReaderDllx64.syscmd_reset(Handle, ref Status);
                }
                else
                {
                    return ReaderDllx86.syscmd_reset(Handle, ref Status);
                }
            }
            catch (DllNotFoundException)
            {
                return BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                if (Handle != 0)
                {
                    return BRP_ERR_WRONG_HANDLE;
                }
                else
                {
                    return BRP_ERR_GENERAL_IO;
                }
            }
        }

        /// <summary>
        /// Retrieve the firmware version of the reader.
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="fws">Firmware version string</param>
        /// <param name="Status">Output parameter: Returns status code of reader</param>
        /// <returns>Returns 0 on success</returns>
        public Int32 syscmd_get_info(Int32 Handle, ref byte[] Fws, ref Int32 Status)
        {
            byte maxLength = 80;
            Int32 return_val = BRP_OK;

            IntPtr FwsBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(maxLength) * maxLength);
            try
            {
                if (is64BitProcess)
                {
                    return_val = ReaderDllx64.syscmd_get_info(Handle, FwsBuffer, ref Status);
                }
                else
                {
                    return_val = ReaderDllx86.syscmd_get_info(Handle, FwsBuffer, ref Status);
                }

                Fws = new byte[maxLength];
                Marshal.Copy(FwsBuffer, Fws, 0, maxLength); //SnrByteArray.Length);

            }
            catch (DllNotFoundException)
            {
                return_val = BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                if (Handle != 0)
                {
                    return_val = BRP_ERR_WRONG_HANDLE;
                }
                else
                {
                    return_val = BRP_ERR_GENERAL_IO;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(FwsBuffer);
            }

            return return_val;
        }

        /// <summary>
        /// Check status of reader
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="boot_status">Output parameter: Returns bootstatus word of the reader</param>
        /// <param name="Status">Output parameter: Returns status code of reader</param>
        /// <returns>Returns 0 on success</returns>
        public Int32 syscmd_get_boot_status(Int32 Handle, ref UInt32 boot_status, ref Int32 Status)
        {
            try
            {
                if (is64BitProcess)
                {
                    return ReaderDllx64.syscmd_get_boot_status(Handle, ref boot_status, ref Status);
                }
                else
                {
                    return ReaderDllx86.syscmd_get_boot_status(Handle, ref boot_status, ref Status);
                }
            }
            catch (DllNotFoundException)
            {
                return BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                if (Handle != 0)
                {
                    return BRP_ERR_WRONG_HANDLE;
                }
                else
                {
                    return BRP_ERR_GENERAL_IO;
                }
            }
        }

        /// <summary>
        /// Select card in field, or select the next card in field.
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="cardTypeMask">Mask for selecting only specific cards. 0xFFFF = all supported cards</param>
        /// <param name="reselect">If you want to reselect cards without moving them out of the antenna's field physically, you have to set the Reselect flag to TRUE</param>
        /// <param name="allowConfig">True = accept config cards</param>
        /// <param name="CardType">Output parameter: Returns type of card</param>
        /// <param name="Status">Output parameter: Returns status code of reader</param>
        /// <returns></returns>
        public Int32 vhl_select(Int32 Handle, UInt16 cardTypeMask, bool reselect, bool allowConfig, ref byte CardType, ref Int32 Status)
        {
            try
            {
                if (is64BitProcess)
                {
                    return ReaderDllx64.vhl_select(Handle, cardTypeMask, reselect, allowConfig, ref CardType, ref Status);
                }
                else
                {
                    return ReaderDllx86.vhl_select(Handle, cardTypeMask, reselect, allowConfig, ref CardType, ref Status);
                }
            }
            catch (DllNotFoundException)
            {
                return BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                if (Handle != 0)
                {
                    return BRP_ERR_WRONG_HANDLE;
                }
                else
                {
                    return BRP_ERR_GENERAL_IO;
                }
            }
        }

        /// <summary>
        /// Get serial number of selected card
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="Snr">Byte array containig serial number</param>
        /// <param name="Length">Length of serial number</param>
        /// <param name="Status">Output parameter: Returns status code of reade</param>
        /// <returns></returns>
        public Int32 vhl_get_snr(Int32 Handle, ref byte[] Snr, ref byte Length, ref Int32 Status)
        {
            byte maxLength = 40;
            Int32 return_val = BRP_OK;

            IntPtr SnrBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(maxLength) * maxLength);

            try
            {
                if (is64BitProcess)
                {
                    return_val = ReaderDllx64.vhl_get_snr(Handle, SnrBuffer, ref Length, ref Status);
                }
                else
                {
                    return_val = ReaderDllx86.vhl_get_snr(Handle, SnrBuffer, ref Length, ref Status);
                }

                Snr = new byte[Length];
                Marshal.Copy(SnrBuffer, Snr, 0, Length); //SnrByteArray.Length);
            }
            catch (DllNotFoundException)
            {
                return_val = BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                if (Handle != 0)
                {
                    return_val = BRP_ERR_WRONG_HANDLE;
                }
                else
                {
                    return_val = BRP_ERR_GENERAL_IO;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(SnrBuffer);
            }

            return return_val;
        }

        /// <summary>
        /// Set LED 
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="port_mask">Color of LED</param>
        /// <param name="Status">Output parameter: Returns status code of reade</param>
        /// <returns></returns>
        public Int32 syscmd_set_port(Int32 Handle, UInt16 port_mask, ref Int32 Status)
        {
            try
            {
                if (is64BitProcess)
                {
                    return ReaderDllx64.syscmd_set_port(Handle, port_mask, ref Status);
                }
                else
                {
                    return ReaderDllx86.syscmd_set_port(Handle, port_mask, ref Status);
                }
            }
            catch (DllNotFoundException)
            {
                return BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                if (Handle != 0)
                {
                    return BRP_ERR_WRONG_HANDLE;
                }
                else
                {
                    return BRP_ERR_GENERAL_IO;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="Status">Output parameter: Returns status code of reade</param>
        /// <returns></returns>
        public Int32 vhl_is_selected(Int32 Handle, ref Int32 Status)
        {
            try
            {
                if (is64BitProcess)
                {
                    return ReaderDllx64.vhl_is_selected(Handle, ref Status);
                }
                else
                {
                    return ReaderDllx86.vhl_is_selected(Handle, ref Status);
                }
            }
            catch (DllNotFoundException)
            {
                return BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                if (Handle != 0)
                {
                    return BRP_ERR_WRONG_HANDLE;
                }
                else
                {
                    return BRP_ERR_GENERAL_IO;
                }
            }
        }

        /// <summary>
        /// this command can be used for simple commands without continous mode
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="devcode">DevCode of command-set containing the command that shall be invoked.</param>
        /// <param name="cmdcode">CmdCode of command that shall be invoked</param>
        /// <param name="param"> Buffer that contains all command parameters</param>
        /// <param name="param_len"> Amount of bytes at param</param>
        /// <param name="timeout"> Timeout for receiving the response frame from the reader</param>        
        /// <param name="status"> Command execution status received from the reader. This value is a 16-bit value.The MSB contains the 8-bit DevCode denoting
        ///  the command-set that contains the command executed at last and the LSB
        ///  contains the actual status byte generated by the reader</param>
        /// <param name="resp">Buffer for command's response data</param>
        /// <param name="resp_len">Will contain the actual amount of bytes at resp</param>
        /// <param name="max_resp_len"> Maximum length of resp</param>
        /// <returns>Driver status </returns>
        /// 
        public unsafe Int32 brp_exec_command(Int32 Handle, byte devcode, byte cmdcode, byte* param, Int32 param_len,
            Int32 timeout, ref Int32 status, byte* resp, ref Int32 resp_len, Int32 max_resp_len)
        {
            try
            {
                if (is64BitProcess)
                {
                    return ReaderDllx64.brp_exec_command( Handle, devcode, cmdcode, param, param_len,
                          timeout, ref status, resp, ref resp_len, max_resp_len);
                }
                else
                {
                    return ReaderDllx86.brp_exec_command(Handle, devcode, cmdcode, param, param_len,
                          timeout, ref status, resp, ref resp_len, max_resp_len);
                }
            }
            catch (DllNotFoundException)
            {
                return BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                if (Handle != 0)
                {
                    return BRP_ERR_WRONG_HANDLE;
                }
                else
                {
                    return BRP_ERR_GENERAL_IO;
                }
            }
        }

        /// <summary>
        /// Set reader buffer size (default 128 byte)
        /// </summary>
        /// <param name="Handle">Handle of reader </param>
        /// <param name="total_bufsize">The maximum size of command parameter plus command response.</param>
        /// <param name="send_bufsize">The maximum size of command parameter</param>
        /// <param name="recv_bufsize">The maximum size of command response.</param>
        /// <returns>Driver status </returns>
        /// 
        public unsafe Int32 brp_set_bufsize(Int32 Handle, Int32 total_bufsize, Int32 send_bufsize, Int32 recv_bufsize)
        {
            try
            {
                if (is64BitProcess)
                {
                    return ReaderDllx64.brp_set_bufsize(Handle, total_bufsize, send_bufsize, recv_bufsize);
                }
                else
                {
                    return ReaderDllx86.brp_set_bufsize(Handle, total_bufsize, send_bufsize, recv_bufsize);
                }
            }
            catch (DllNotFoundException)
            {
                return BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                if (Handle != 0)
                {
                    return BRP_ERR_WRONG_HANDLE;
                }
                else
                {
                    return BRP_ERR_GENERAL_IO;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="VHLFile">The number of the VHL File that should be used for access</param>
        /// <param name="Address">The start address of the data that should be received</param>
        /// <param name="Length">The number of bytes that should be received starting at address</param>
        /// <param name="Data">The data you requested will be written here.It can as long as Length (max 65535 Byte)</param>
        /// <param name="Status">Output parameter: Returns status code of reade</param>
        /// <returns></returns>
        public unsafe Int32 vhl_read(Int32 Handle, byte VHLFile, Int16 Address, Int16 Length, byte *Data, ref Int32 Status)
        {
            try
            {
                if (is64BitProcess)
                {
                    return ReaderDllx64.vhl_read(Handle, VHLFile, Address, Length, Data, ref Status);
                }
                else
                {
                    return ReaderDllx86.vhl_read(Handle, VHLFile, Address, Length, Data, ref Status);
                }
            }
            catch (DllNotFoundException)
            {
                return BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                if (Handle != 0)
                {
                    return BRP_ERR_WRONG_HANDLE;
                }
                else
                {
                    return BRP_ERR_GENERAL_IO;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="VHLFile">The number of the VHL File that should be used for access</param>
        /// <param name="Address">e start address the data should be written to</param>
        /// <param name="Length">The number of bytes you want to write. Should be equal to
        //					number of bytes in Data. ATTENTION: The length needs to be
        //					less or equal to the send buffer size of the reader MINUS 5</param>
        /// <param name="Data">The buffer that stores the data you want to transfere to the	reader</param>
        /// <param name="Status">Output parameter: Returns status code of reade</param>
        /// <returns></returns>
        public unsafe Int32 vhl_write(Int32 Handle, byte VHLFile, Int16 Address, Int16 Length, byte *Data, ref Int32 Status)
        {
            try
            {
                if (is64BitProcess)
                {
                    return ReaderDllx64.vhl_write(Handle, VHLFile, Address, Length, Data, ref Status);
                }
                else
                {
                    return ReaderDllx86.vhl_write(Handle, VHLFile, Address, Length, Data, ref Status);
                }
            }
            catch (DllNotFoundException)
            {
                return BRP_DLL_NOT_FOUND;
            }
            catch (Exception)
            {
                if (Handle != 0)
                {
                    return BRP_ERR_WRONG_HANDLE;
                }
                else
                {
                    return BRP_ERR_GENERAL_IO;
                }
            }
        }
    }
    
	/// <summary>
	/// Managed class for accessing the functions of the 64 bit RFID Reader DLL 
	/// </summary>
	public class ReaderDllx64
	{
		/// <summary>
		/// Open USB session and return handle if Reader is connected
		/// </summary>
		/// <param name="Handle">Output parameter. Returns handle of reader</param>
		/// <param name="productID">should always be zero</param>
		/// <returns>Returns 0 on success</returns>
		[DllImport("BrpDriver_x64.dll")]
		public static extern Int32 brp_open_usb_session(ref Int32 Handle, UInt32 productID);


        /// <summary>
        /// Open SERIAL session and return handle if Reader is connected
        /// </summary>
        /// <param name="Handle">Output parameter. Returns handle of reader</param>
        /// <param name="com_port">com port number</param>
        /// <param name="ser_baudrate_baudrate">baudrate speed</param>
        /// <param name="ser_parity_parity">parity</param>
        ///    /// <returns>Returns 0 on success</returns>
        [DllImport("BrpDriver_x64.dll")]
        public static extern Int32 brp_open_serial_session(ref Int32 Handle, Int32 com_port, int ser_baudrate_baudrate, int ser_parity_parity);


        /// <summary>
        /// Close USB session and free resources
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <returns></returns>
        [DllImport("BrpDriver_x64.dll")]
		public static extern Int32 brp_close_session(Int32 Handle);


        /// <summary>
        /// Set Checksumm (only necessary for RS232 connection)
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="checksum">checksum algorithm , see defines</param>
        /// <returns></returns>
        [DllImport("BrpDriver_x64.dll")]
        public static extern Int32 brp_set_checksum(Int32 Handle, Int32 checksum);


        /// <summary>
        /// Reboots the reader.
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="Status">Output parameter: Returns status code of reader</param>
        /// <returns>Returns 0 on success</returns>
        [DllImport("BrpDriver_x64.dll")]
		public static extern Int32 syscmd_reset(Int32 Handle, ref Int32 Status);

		/// <summary>
		/// Retrieve the firmware version of the reader.
		/// </summary>
		/// <param name="Handle">Handle of reader</param>
		/// <param name="fws">Firmware version string</param>
		/// <param name="Status">Output parameter: Returns status code of reader</param>
		/// <returns>Returns 0 on success</returns>
		[DllImport("BrpDriver_x64.dll")]
		public static extern Int32 syscmd_get_info(Int32 Handle, IntPtr fws, ref Int32 Status);

		/// <summary>
		/// Check status of reader
		/// </summary>
		/// <param name="Handle">Handle of reader</param>
		/// <param name="boot_status">Output parameter: Returns bootstatus word of the reader</param>
		/// <param name="Status">Output parameter: Returns status code of reader</param>
		/// <returns>Returns 0 on success</returns>
		[DllImport("BrpDriver_x64.dll")]
		public static extern Int32 syscmd_get_boot_status(Int32 Handle, ref UInt32 boot_status, ref Int32 Status);

		/// <summary>
		/// Select card in field, or select the next card in field.
		/// </summary>
		/// <param name="Handle">Handle of reader</param>
		/// <param name="cardTypeMask">Mask for selecting only specific cards. 0xFFFF = all supported cards</param>
		/// <param name="reselect">If you want to reselect cards without moving them out of the antenna's field physically, you have to set the Reselect flag to TRUE</param>
		/// <param name="allowConfig">True = accept config cards</param>
		/// <param name="CardType">Output parameter: Returns type of card</param>
		/// <param name="Status">Output parameter: Returns status code of reader</param>
		/// <returns></returns>
		[DllImport("BrpDriver_x64.dll")]
		public static extern Int32 vhl_select(Int32 Handle, UInt16 cardTypeMask, bool reselect, bool allowConfig, ref byte CardType, ref Int32 Status);

		/// <summary>
		/// Get serial number of selected card
		/// </summary>
		/// <param name="Handle">Handle of reader</param>
		/// <param name="Snr">Byte array containig serial number</param>
		/// <param name="Length">Length of serial number</param>
		/// <param name="Status">Output parameter: Returns status code of reade</param>
		/// <returns></returns>
		[DllImport("BrpDriver_x64.dll")]
		public static extern Int32 vhl_get_snr(Int32 Handle, IntPtr Snr, ref byte Length, ref Int32 Status);

		/// <summary>
		/// Set LED 
		/// </summary>
		/// <param name="Handle">Handle of reader</param>
		/// <param name="port_mask">Color of LED</param>
		/// <param name="Status">Output parameter: Returns status code of reade</param>
		/// <returns></returns>
		[DllImport("BrpDriver_x64.dll")]
		public static extern Int32 syscmd_set_port(Int32 Handle, UInt16 port_mask, ref Int32 Status);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Handle">Handle of reader</param>
		/// <param name="Status">Output parameter: Returns status code of reade</param>
		/// <returns></returns>
		[DllImport("BrpDriver_x64.dll")]
		public static extern Int32 vhl_is_selected(Int32 Handle, ref Int32 Status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="devcode">DevCode of command-set containing the command that shall be invoked.</param>
        /// <param name="cmdcode">CmdCode of command that shall be invoked</param>
        /// <param name="param"> Buffer that contains all command parameters</param>
        /// <param name="param_len"> Amount of bytes at param</param>
        /// <param name="timeout"> Timeout for receiving the response frame from the reader</param>        
        /// <param name="status"> Command execution status received from the reader. This value is a 16-bit value.The MSB contains the 8-bit DevCode denoting
        ///  the command-set that contains the command executed at last and the LSB
        ///  contains the actual status byte generated by the reader</param>
        /// <param name="resp">Buffer for command's response data</param>
        /// <param name="resp_len">Will contain the actual amount of bytes at resp</param>
        /// <param name="max_resp_len"> Maximum length of resp</param>
        /// <returns>Driver status </returns>
        [DllImport("BrpDriver_x64.dll")]

        public unsafe static extern Int32 brp_exec_command(Int32 Handle, byte devcode, byte cmdcode, byte* param, Int32 param_len,
            Int32 timeout, ref Int32 status, byte* resp, ref Int32 resp_len, Int32 max_resp_len);

        /// <summary>
        /// Set reader buffer size (default 128 byte)
        /// </summary>
        /// <param name="Handle">Handle of reader </param>
        /// <param name="total_bufsize">The maximum size of command parameter plus command response.</param>
        /// <param name="send_bufsize">The maximum size of command parameter</param>
        /// <param name="recv_bufsize">The maximum size of command response.</param>
        /// <returns>Driver status </returns>
        [DllImport("BrpDriver_x64.dll")]

        public unsafe static extern Int32 brp_set_bufsize(Int32 Handle, Int32 total_bufsize, Int32 send_bufsize, Int32 recv_bufsize);

        /// <summary>
        /// Read memeory data
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="VHLFile">The number of the VHL File that should be used for access</param>
        /// <param name="Address">The start address of the data that should be received</param>
        /// <param name="Length">The number of bytes that should be received starting at address</param>
        /// <param name="Data">The data you requested will be written here.It can as long as Length (max 65535 Byte)</param>
        /// <param name="Status">Output parameter: Returns status code of reade</param>
        /// <returns></returns>
        [DllImport("BrpDriver_x64.dll")]
        public unsafe static extern Int32 vhl_read(Int32 Handle, byte VHLFile, Int16 Address, Int16 Length, byte *Data, ref Int32 Status);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="VHLFile">The number of the VHL File that should be used for access</param>
        /// <param name="Address">e start address the data should be written to</param>
        /// <param name="Length">The number of bytes you want to write. Should be equal to
        //					number of bytes in Data. ATTENTION: The length needs to be
        //					less or equal to the send buffer size of the reader MINUS 5</param>
        /// <param name="Data">The buffer that stores the data you want to transfere to the	reader</param>
        /// <param name="Status">Output parameter: Returns status code of reade</param>
        /// <returns></returns>
        [DllImport("BrpDriver_x64.dll")]
        public unsafe static extern Int32 vhl_write(Int32 Handle, byte VHLFile, Int16 Address, Int16 Length, byte *Data, ref Int32 Status);
    }

	/// <summary>
	/// Managed class for accessing the functions of the 64 bit RFID Reader DLL 
	/// </summary>
	public class ReaderDllx86
	{
        /// <summary>
        /// Open USB session and return handle if Reader is connected
        /// </summary>
        /// <param name="Handle">Output parameter. Returns handle of reader</param>
        /// <param name="productID">should always be zero</param>
        /// <returns>Returns 0 on success</returns>
        [DllImport("BrpDriver_x86.dll")]
        public static extern Int32 brp_open_usb_session(ref Int32 Handle, UInt32 productID);


        /// <summary>
        /// Open SERIAL session and return handle if Reader is connected
        /// </summary>
        /// <param name="Handle">Output parameter. Returns handle of reader</param>
        /// <param name="com_port">com port number</param>
        /// <param name="ser_baudrate_baudrate">baudrate speed</param>
        /// <param name="ser_parity_parity">parity</param>
        ///    /// <returns>Returns 0 on success</returns>
        [DllImport("BrpDriver_x86.dll")]
        public static extern Int32 brp_open_serial_session(ref Int32 Handle, Int32 com_port, int ser_baudrate_baudrate, int ser_parity_parity);


        /// <summary>
        /// Close USB session and free resources
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <returns></returns>
        [DllImport("BrpDriver_x86.dll")]
		public static extern Int32 brp_close_session(Int32 Handle);


        /// <summary>
        /// Set Checksumm (only necessary for RS232 connection)
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="checksum">checksum algorithm , see defines</param>
        /// <returns></returns>
        [DllImport("BrpDriver_x64.dll")]
        public static extern Int32 brp_set_checksum(Int32 Handle, Int32 checksum);


        /// <summary>
        /// Check status of reader
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="boot_status">Output parameter: Returns bootstatus word of the reader</param>
        /// <param name="Status">Output parameter: Returns status code of reader</param>
        /// <returns>Returns 0 on success</returns>
        [DllImport("BrpDriver_x86.dll")]
		public static extern Int32 syscmd_get_boot_status(Int32 Handle, ref UInt32 boot_status, ref Int32 Status);

		/// <summary>
		/// Reboots the reader.
		/// </summary>
		/// <param name="Handle">Handle of reader</param>
		/// <param name="Status">Output parameter: Returns status code of reader</param>
		/// <returns>Returns 0 on success</returns>
		[DllImport("BrpDriver_x86.dll")]
		public static extern Int32 syscmd_reset(Int32 Handle, ref Int32 Status);

		/// <summary>
		/// Retrieve the firmware version of the reader.
		/// </summary>
		/// <param name="Handle">Handle of reader</param>
		/// <param name="fws">Firmware version string</param>
		/// <param name="Status">Output parameter: Returns status code of reader</param>
		/// <returns>Returns 0 on success</returns>
		[DllImport("BrpDriver_x86.dll")]
		public static extern Int32 syscmd_get_info(Int32 Handle, IntPtr fws, ref Int32 Status);

		/// <summary>
		/// Select card in field, or select the next card in field.
		/// </summary>
		/// <param name="Handle">Handle of reader</param>
		/// <param name="cardTypeMask">Mask for selecting only specific cards. 0xFFFF = all supported cards</param>
		/// <param name="reselect">If you want to reselect cards without moving them out of the antenna's field physically, you have to set the Reselect flag to TRUE</param>
		/// <param name="allowConfig">True = accept config cards</param>
		/// <param name="CardType">Output parameter: Returns type of card</param>
		/// <param name="Status">Output parameter: Returns status code of reader</param>
		/// <returns></returns>
		[DllImport("BrpDriver_x86.dll")]
		public static extern Int32 vhl_select(Int32 Handle, UInt16 cardTypeMask, bool reselect, bool allowConfig, ref byte CardType, ref Int32 Status);

		/// <summary>
		/// Get serial number of selected card
		/// </summary>
		/// <param name="Handle">Handle of reader</param>
		/// <param name="Snr">Byte array containig serial number</param>
		/// <param name="Length">Length of serial number</param>
		/// <param name="Status">Output parameter: Returns status code of reade</param>
		/// <returns></returns>
		[DllImport("BrpDriver_x86.dll")]
		public static extern Int32 vhl_get_snr(Int32 Handle, IntPtr Snr, ref byte Length, ref Int32 Status);

		/// <summary>
		/// Set LED 
		/// </summary>
		/// <param name="Handle">Handle of reader</param>
		/// <param name="port_mask">Color of LED</param>
		/// <param name="Status">Output parameter: Returns status code of reade</param>
		/// <returns></returns>
		[DllImport("BrpDriver_x86.dll")]
		public static extern Int32 syscmd_set_port(Int32 Handle, UInt16 port_mask, ref Int32 Status);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Handle">Handle of reader</param>
		/// <param name="Status">Output parameter: Returns status code of reade</param>
		/// <returns></returns>
		[DllImport("BrpDriver_x86.dll")]
		public static extern Int32 vhl_is_selected(Int32 Handle, ref Int32 Status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="devcode">DevCode of command-set containing the command that shall be invoked.</param>
        /// <param name="cmdcode">CmdCode of command that shall be invoked</param>
        /// <param name="param"> Buffer that contains all command parameters</param>
        /// <param name="param_len"> Amount of bytes at param</param>
        /// <param name="timeout"> Timeout for receiving the response frame from the reader</param>        
        /// <param name="status"> Command execution status received from the reader. This value is a 16-bit value.The MSB contains the 8-bit DevCode denoting
        ///  the command-set that contains the command executed at last and the LSB
        ///  contains the actual status byte generated by the reader</param>
        /// <param name="resp">Buffer for command's response data</param>
        /// <param name="resp_len">Will contain the actual amount of bytes at resp</param>
        /// <param name="max_resp_len"> Maximum length of resp</param>
        /// <returns>Driver status </returns>
		[DllImport("BrpDriver_x86.dll")]

        public unsafe static extern Int32 brp_exec_command(Int32 Handle, byte devcode, byte cmdcode, byte* param, Int32 param_len,
            Int32 timeout, ref Int32 status, byte * resp, ref Int32 resp_len, Int32 max_resp_len);


        /// <summary>
        /// Set reader buffer size (default 128 byte)
        /// </summary>
        /// <param name="Handle">Handle of reader </param>
        /// <param name="total_bufsize">The maximum size of command parameter plus command response.</param>
        /// <param name="send_bufsize">The maximum size of command parameter</param>
        /// <param name="recv_bufsize">The maximum size of command response.</param>
        /// <returns>Driver status </returns>
		[DllImport("BrpDriver_x86.dll")]

        public unsafe static extern Int32 brp_set_bufsize(Int32 Handle, Int32 total_bufsize, Int32 send_bufsize, Int32 recv_bufsize);

        /// <summary>
        /// Read memeory data
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="VHLFile">The number of the VHL File that should be used for access</param>
        /// <param name="Address">The start address of the data that should be received</param>
        /// <param name="Length">The number of bytes that should be received starting at address</param>
        /// <param name="Data">The data you requested will be written here.It can as long as Length (max 65535 Byte)</param>
        /// <param name="Status">Output parameter: Returns status code of reade</param>
        /// <returns></returns>
		[DllImport("BrpDriver_x86.dll")]
        public unsafe static extern Int32 vhl_read(Int32 Handle, byte VHLFile, Int16 Address, Int16 Length, byte *Data, ref Int32 Status);


        /// <summary>
        /// Write data to the user memory
        /// </summary>
        /// <param name="Handle">Handle of reader</param>
        /// <param name="VHLFile">The number of the VHL File that should be used for access</param>
        /// <param name="Address">e start address the data should be written to</param>
        /// <param name="Length">The number of bytes you want to write. Should be equal to
        //					number of bytes in Data. ATTENTION: The length needs to be
        //					less or equal to the send buffer size of the reader MINUS 5</param>
        /// <param name="Data">The buffer that stores the data you want to transfere to the	reader</param>
        /// <param name="Status">Output parameter: Returns status code of reade</param>
        /// <returns></returns>
		[DllImport("BrpDriver_x86.dll")]
        public unsafe static extern Int32 vhl_write(Int32 Handle, byte VHLFile, Int16 Address, Int16 Length, byte *Data, ref Int32 Status);
    }
}
