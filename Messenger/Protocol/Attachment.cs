
using Messenger.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Messenger.Protocol
{
    /// <summary>
    /// Attachment
    /// </summary>
    public class Attachment : NotifyPropertyChangedBase, IEquatable<Attachment>
    {
        /// <summary>
        /// Gets or sets the identifier of the attachment.
        /// </summary>
        /// <value>
        /// The identifier of the attachment.
        /// </value>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the name of the attachment.
        /// </summary>
        /// <value>
        /// The name of the attachment.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the size of the attachment.
        /// </summary>
        /// <value>
        /// The size of the attachment.
        /// </value>
        public long Size { get; set; }
        /// <summary>
        /// The date an time of the attachment
        /// </summary>
        public DateTime Time;
        /// <summary>
        /// Gets or sets the attribute of the attachment.
        /// </summary>
        /// <value>
        /// The attribute of the attachment.
        /// </value>
        public int Attribute { get; set; }
        /// <summary>
        /// The extended attributes
        /// </summary>
        public Dictionary<string, string> ExtendedAttributes;
        string fullPath;

        /// <summary>
        /// Gets or sets the full path.
        /// </summary>
        /// <value>
        /// The full path.
        /// </value>
        public string FullPath
        {
            get
            {
                return fullPath;
            }

            set
            {
                fullPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the packet no.
        /// </summary>
        /// <value>
        /// The packet no.
        /// </value>
        public int PacketNo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Attachment"/> class.
        /// </summary>
        public Attachment()
        {
            ExtendedAttributes = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is file.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is file; otherwise, <c>false</c>.
        /// </value>
        public bool IsFile
        {
            get
            {
                return Attribute == (int)FileTypeFlag.IPMSG_FILE_REGULAR;
            }
            set
            {
                Attribute = (int)FileTypeFlag.IPMSG_FILE_REGULAR;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is directory.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is directory; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirectory
        {
            get
            {
                return Attribute == (int)FileTypeFlag.IPMSG_FILE_DIR;
            }
            set
            {
                Attribute = (int)FileTypeFlag.IPMSG_FILE_DIR;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is return parent.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is return parent; otherwise, <c>false</c>.
        /// </value>
        public bool IsReturnParent
        {
            get
            {
                return Attribute == (int)FileTypeFlag.IPMSG_FILE_RETPARENT;
            }
            set
            {
                Attribute = (int)FileTypeFlag.IPMSG_FILE_RETPARENT;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Decodes the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static Attachment Decode(string file)
        {
            Attachment attachment = new Attachment();
            string[] items = file.Split(':');
            attachment.ID = int.Parse(items[0]);
            attachment.Name = items[1];
            attachment.Size = long.Parse(items[2], System.Globalization.NumberStyles.HexNumber);
            attachment.Time = DateTime.FromFileTime(long.Parse(items[3], System.Globalization.NumberStyles.HexNumber));
            attachment.Attribute = int.Parse(items[4], System.Globalization.NumberStyles.HexNumber);
            int index = 5;
            while (index < items.Length)
            {
                if (items[index].Contains('='))
                {
                    string[] pair = items[index].Split('=');
                    attachment.ExtendedAttributes.Add(pair[0], pair[1]);
                }
                index++;
            }
            return attachment;
        }

        /// <summary>
        /// Encodes the specified attachment.
        /// </summary>
        /// <param name="attachment">The attachment.</param>
        /// <returns></returns>
        public static string Encode(Attachment attachment)
        {
            StringBuilder text = new StringBuilder();
            text.Append(attachment.ID);
            text.Append(':');
            text.Append(attachment.Name);
            text.Append(':');
            text.Append(attachment.Size.ToString("x"));
            text.Append(':');
            text.Append(attachment.Time.ToFileTime().ToString("x"));
            text.Append(':');
            text.Append(attachment.Attribute.ToString("x"));
            text.Append(':');
            foreach (KeyValuePair<string, string> attr in attachment.ExtendedAttributes)
            {
                text.Append(string.Format("{0}={1}", attr.Key, attr.Value));
                text.Append(':');
            }
            return text.ToString();
        }

        /// <summary>
        /// Decodes the header.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="headerSize">The header size.</param>
        /// <returns></returns>
        public static Attachment DecodeHeader(byte[] header, int startIndex, int headerSize)
        {
            string file = Messager.TextEncoding.GetString(header, startIndex, headerSize - startIndex);
            Attachment attachment = new Attachment();
            string[] items = file.Split(':');
            attachment.Name = items[0];
            attachment.Size = long.Parse(items[1], System.Globalization.NumberStyles.HexNumber);
            attachment.Attribute = int.Parse(items[2], System.Globalization.NumberStyles.HexNumber);
            int index = 3;
            while (index < items.Length)
            {
                if (items[index].Contains('='))
                {
                    string[] pair = items[index].Split('=');
                    attachment.ExtendedAttributes.Add(pair[0], pair[1]);
                }
                index++;
            }
            return attachment;
        }

        /// <summary>
        /// Encodes the header.
        /// </summary>
        /// <param name="attachment">The attachment.</param>
        /// <returns></returns>
        public static byte[] EncodeHeader(Attachment attachment)
        {
            StringBuilder text = new StringBuilder();
            text.Append(':');
            text.Append(attachment.Name);
            text.Append(':');
            text.Append(attachment.Size.ToString("x"));
            text.Append(':');
            text.Append(attachment.Attribute.ToString("x"));
            text.Append(':');
            foreach (KeyValuePair<string, string> attr in attachment.ExtendedAttributes)
            {
                text.Append(string.Format("{0}={1}", attr.Key, attr.Value));
                text.Append(':');
            }
            return Messager.TextEncoding.GetBytes(text.ToString());
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Encode(this);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public Attachment Clone()
        {
            Attachment attachment = new Attachment();
            attachment.ID = ID;
            attachment.Name = Name;
            attachment.Size = Size;
            attachment.Time = Time;
            attachment.Attribute = Attribute;
            attachment.ExtendedAttributes = ExtendedAttributes;
            attachment.FullPath = FullPath;
            attachment.PacketNo = PacketNo;
            return attachment;
        }

        #region IEquatable<Attachment>

        /// <summary>
        /// Gets whether two attachment are equals.
        /// </summary>
        /// <param name="other">The other attachment.</param>
        /// <returns><c>True</c> if both are equals otherwise <c>False</c></returns>
        public bool Equals(Attachment other)
        {
            if (FullPath != null)
            {
                return FullPath.Equals(other.FullPath)
                    && ID == other.ID;
            }
            return Name.Equals(other.Name);
        }

        #endregion
    }
}
