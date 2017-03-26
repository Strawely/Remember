using System;

namespace Remember
{
    public class FNameString
    {
        public string Path { get; set; }
        public int ExistingFlag { get; set; }

        public FNameString()
        {
            
        }

        public FNameString(String path)
        {
            Path = path;
        }

        public new String ToString()
        {
            return Path;
        }
    }
}