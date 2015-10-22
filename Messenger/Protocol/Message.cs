using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;


namespace Messenger.Protocol
{
    /// <summary>
    /// Message
    /// </summary>
    public class Message
    {
        /// <summary>
        /// The version
        /// </summary>
        public int Version = 1;
        /// <summary>
        /// The packet no
        /// </summary>
        public int PacketNo;
        /// <summary>
        /// Gets or sets the name of the sender.
        /// </summary>
        /// <value>
        /// The name of the sender.
        /// </value>
        public string SenderName { get; set; }
        string senderHost;

        /// <summary>
        /// Gets or sets the sender host.
        /// </summary>
        /// <value>
        /// The sender host.
        /// </value>
        public string SenderHost
        {
            get
            {
                return senderHost;
            }

            set
            {
                senderHost = value;
            }
        }

        /// <summary>
        /// The command
        /// </summary>
        public uint Command;
        /// <summary>
        /// The option
        /// </summary>
        public uint Option;
        /// <summary>
        /// The additional section
        /// </summary>
        public byte[] AdditionalSection;
        /// <summary>
        /// The attachments
        /// </summary>
        private List<Attachment> attachments;

        /// <summary>
        /// The sender IP
        /// </summary>
        public IPEndPoint SenderIP;
        /// <summary>
        /// The date
        /// </summary>
        DateTime date;

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content
        {
            get
            {
                if (HasIconData)
                {
                    return "<Icon Data>";
                }
                return Messager.TextEncoding.GetString(AdditionalSection).TrimEnd('\0');
            }
            set
            {
                if (value != null)
                {
                    AdditionalSection = Messager.TextEncoding.GetBytes(value);
                }
                else
                {
                    AdditionalSection = new byte[1] { 0 };
                }
            }
        }

        /// <summary>
        /// Gets the sender.
        /// </summary>
        /// <value>
        /// The sender.
        /// </value>
        public string Sender
        {
            get { return string.Format("{0} ({1})", SenderName, SenderHost); }
        }

        /// <summary>
        /// Gets or sets the command no.
        /// </summary>
        /// <value>
        /// The command no.
        /// </value>
        public uint CommandNo
        {
            get { return Option | Command; }
            set
            {
                Option = value & 0xFFFFFF00;
                Command = value & 0xFF;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has attachment.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has attachment; otherwise, <c>false</c>.
        /// </value>
        public bool HasAttachment
        {
            get
            {
                CommandOption option = (CommandOption)Option;
                return (option & CommandOption.IPMSG_FILEATTACHOPT) == CommandOption.IPMSG_FILEATTACHOPT
                    && Command == CommandID.IPMSG_SENDMSG;
            }
            set
            {
                if (value)
                {
                    Option = Option | (uint)CommandOption.IPMSG_FILEATTACHOPT;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [confirm receive].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [confirm receive]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfirmReceive
        {
            get
            {
                SendCommandOption option = (SendCommandOption)Option;
                return (option & SendCommandOption.IPMSG_SENDCHECKOPT) == SendCommandOption.IPMSG_SENDCHECKOPT;
            }
            set
            {
                if (value)
                {
                    Option = Option | (uint)SendCommandOption.IPMSG_SENDCHECKOPT;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has icon data.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has icon data; otherwise, <c>false</c>.
        /// </value>
        public bool HasIconData
        {
            get { return Command == CommandID.IPMSG_SENDINFO && Option == (uint)CommandOption.IPMSG_FILEATTACHOPT; }
        }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public List<Attachment> Attachments
        {
            get
            {
                return attachments;
            }

            set
            {
                attachments = value;
            }
        }

        /// <summary>
        /// Decodes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="remoteEP">The remote end point.</param>
        /// <returns></returns>
        public static Message Decode(byte[] data, IPEndPoint remoteEP)
        {
            List<string> items = new List<string>(5);
            int index = 0;
            int count = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    items.Add(Encoding.UTF8.GetString(data, index, count));
                    count = 0;
                    index = i + 1;
                    if (items.Count == 5) break;
                }
                else
                {
                    count++;
                }
            }
            count = data.Length - index;
            byte[] additional = new byte[count];
            Array.Copy(data, index, additional, 0, count);

            Message message = new Message();
            if (items.Count >= 4)
            {
                int.TryParse(items[0], out message.Version);
                int.TryParse(items[1], out message.PacketNo);
                message.SenderName = items[2];
                message.SenderHost = items[3];
                message.CommandNo = uint.Parse(items[4]);
            }
            message.AdditionalSection = additional;
            message.SenderIP = remoteEP;

            if (message.HasAttachment)
            {
                index = Array.IndexOf<byte>(additional, 0);
                if (index > -1)
                {
                    index++;
                    count = additional.Length - index;
                    string attachment = Messager.TextEncoding.GetString(additional, index, count);
                    string[] files = attachment.Split('\a');
                    message.Attachments = new List<Attachment>(files.Length);
                    foreach (string file in files)
                    {
                        if (file.Contains(':'))
                        {
                            message.Attachments.Add(Attachment.Decode(file));
                        }
                    }
                }
                Array.Resize<byte>(ref message.AdditionalSection, index);
            }

            return message;
        }



        /// <summary>
        /// Encodes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static byte[] Encode(Message message)
        {
            string header = string.Concat(
                message.Version.ToString(),
                ":",
                message.PacketNo.ToString(),
                ":",
                message.SenderName,
                ":",
                message.SenderHost,
                ":",
                message.CommandNo.ToString(),
                ":"
                );
            List<byte> data = new List<byte>(Encoding.UTF8.GetByteCount(header) + message.AdditionalSection.Length);
            data.AddRange(Encoding.UTF8.GetBytes(header));
            data.AddRange(message.AdditionalSection);
            if (message.HasAttachment)
            {
                data.Add((byte)0);
                foreach (Attachment attachment in message.Attachments)
                {
                    data.AddRange(Messager.TextEncoding.GetBytes(Attachment.Encode(attachment)));
                    data.Add((byte)'\a');
                }
            }
            return data.ToArray();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}:{3}:{4}:{5}",
                Version, PacketNo, SenderName, SenderHost, CommandNo, Content);
        }
    }
}
