using ABP.TPLMS.Debugging;

namespace ABP.TPLMS
{
    public class TPLMSConsts
    {
        public const string LocalizationSourceName = "TPLMS";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "87b13895bd134d59b10511b655f808c0";
    }
}
