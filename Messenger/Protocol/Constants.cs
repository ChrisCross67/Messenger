namespace Messenger.Protocol
{
    public class CommandID
    {
        /*  command  */
        public const int IPMSG_NOOPERATION = 0x00000000;

        public const int IPMSG_BR_ENTRY = 0x00000001;
        public const int IPMSG_BR_EXIT = 0x00000002;
        public const int IPMSG_ANSENTRY = 0x00000003;
        public const int IPMSG_BR_ABSENCE = 0x00000004;

        public const int IPMSG_BR_ISGETLIST = 0x00000010;
        public const int IPMSG_OKGETLIST = 0x00000011;
        public const int IPMSG_GETLIST = 0x00000012;
        public const int IPMSG_ANSLIST = 0x00000013;
        public const int IPMSG_BR_ISGETLIST2 = 0x00000018;

        public const int IPMSG_SENDMSG = 0x00000020;
        public const int IPMSG_RECVMSG = 0x00000021;
        public const int IPMSG_READMSG = 0x00000030;
        public const int IPMSG_DELMSG = 0x00000031;
        public const int IPMSG_ANSREADMSG = 0x00000032;

        public const int IPMSG_GETINFO = 0x00000040;
        public const int IPMSG_SENDINFO = 0x00000041;

        public const int IPMSG_GETABSENCEINFO = 0x00000050;
        public const int IPMSG_SENDABSENCEINFO = 0x00000051;

        public const int IPMSG_GETFILEDATA = 0x00000060;
        public const int IPMSG_RELEASEFILES = 0x00000061;
        public const int IPMSG_GETDIRFILES = 0x00000062;

        public const int IPMSG_GETPUBKEY = 0x00000072;
        public const int IPMSG_ANSPUBKEY = 0x00000073;
    }

    public enum CommandOption
    {
        /*  option for all command  */
        IPMSG_ABSENCEOPT = 0x00000100,
        IPMSG_SERVEROPT = 0x00000200,
        IPMSG_DIALUPOPT = 0x00010000,
        IPMSG_FILEATTACHOPT = 0x00200000,
        IPMSG_ENCRYPTOPT = 0x00400000
    }

    public enum SendCommandOption
    {
        /*  option for send command  */
        IPMSG_SENDCHECKOPT = 0x00000100,
        IPMSG_SECRETOPT = 0x00000200,
        IPMSG_BROADCASTOPT = 0x00000400,
        IPMSG_MULTICASTOPT = 0x00000800,
        IPMSG_NOPOPUPOPT = 0x00001000,
        IPMSG_AUTORETOPT = 0x00002000,
        IPMSG_RETRYOPT = 0x00004000,
        IPMSG_PASSWORDOPT = 0x00008000,
        IPMSG_NOLOGOPT = 0x00020000,
        IPMSG_NEWMUTIOPT = 0x00040000,
        IPMSG_NOADDLISTOPT = 0x00080000,
        IPMSG_READCHECKOPT = 0x00100000,
        IPMSG_SECRETEXOPT = (IPMSG_READCHECKOPT | IPMSG_SECRETOPT)
    }

    public enum EncryptCommandOption
    {
        /* encryption flags for encrypt command */
        IPMSG_RSA_512 = 0x00000001,
        IPMSG_RSA_1024 = 0x00000002,
        IPMSG_RSA_2048 = 0x00000004,
        IPMSG_RC2_40 = 0x00001000,
        IPMSG_RC2_128 = 0x00004000,
        IPMSG_RC2_256 = 0x00008000,
        IPMSG_BLOWFISH_128 = 0x00020000,
        IPMSG_BLOWFISH_256 = 0x00040000,
        IPMSG_SIGN_MD5 = 0x10000000
    }

    public enum FileTypeFlag
    {
        /* file types for fileattach command */
        IPMSG_FILE_REGULAR = 0x00000001,
        IPMSG_FILE_DIR = 0x00000002,
        IPMSG_FILE_RETPARENT = 0x00000003,	// return parent directory
        IPMSG_FILE_SYMLINK = 0x00000004,
        IPMSG_FILE_CDEV = 0x00000005,	// for UNIX
        IPMSG_FILE_BDEV = 0x00000006,	// for UNIX
        IPMSG_FILE_FIFO = 0x00000007,	// for UNIX
        IPMSG_FILE_RESFORK = 0x00000010	// for Mac
    }
}
