using Defines.Enums;

namespace Scripts
{
    public class ApplicationSettings
    {

        private static ApplicationSettings _instance = null;

        public static ApplicationSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApplicationSettings();
                }

                return _instance;
            }
        }

        public readonly SaveModes SaveMode = SaveModes.File; 

    }
}