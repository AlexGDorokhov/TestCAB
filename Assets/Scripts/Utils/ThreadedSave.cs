using System.IO;
using System.Threading;

namespace Utils
{
    public class ThreadedSave
    {
        
        private string _data; 
        private string _path; 
        
        public ThreadedSave(string data, string path)
        {
            _data = data;
            _path = path;
        }
        
        public void Run() {
            File.WriteAllText(_path, _data);
        }
        
    }
}