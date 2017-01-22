using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileWatcher
{
    class FileWatchers
    {
        public event Action ChangeFile;
        public string Path;
        public DateTime dt = DateTime.Now;
        public object lockObject = new object();

        public FileWatchers(string path)
        {
            Path = path;
        }

        public void Start()
        {
            Task t = new Task(ControlForChange);
            t.Start();
        }

        public void ControlForChange()
        {
            lock (lockObject)
            {
                while (true)
                {
                    var writeTime = File.GetLastWriteTime(Path);
                    if (writeTime > dt)
                    {
                        dt = writeTime;
                        FileChange();
                    }
                }
            }
        }

        protected void FileChange()
        {
            lock (lockObject)
            {
                Action handler = ChangeFile;
                handler?.Invoke();
            }
        }
    }
}