using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace WpfApplication
{
    public delegate void Notify(Files f);

    public class FileModel : ObservableCollection<Files>
    {
        private static bool _btn = false;
        private int _i = 0;
        private int k = 0;
        public string Path;
        private string[] exc;

        private readonly DriveInfo[] _drives = DriveInfo.GetDrives();

        public readonly Queue<Files> _files = new Queue<Files>();

        public FileModel()
        {
            Path = _drives[_i].ToString();
            Thread thread = new Thread(Run);
            thread.Start();
            Thread threadRun = new Thread(RunThread);
            threadRun.Start();
            Thread threadHash = new Thread(CalculateHash);
            threadHash.Start();
        }
        
        private void RunThread()
        {
            while (true)
            {
                try
                {
                    exc = Directory.GetDirectories(Path);
                }
                catch (Exception e)
                {
                    WorkCompleted();
                }
                if (exc.Length == 0)
                    exc = new string[1] {Path};
                foreach (var dir in exc)
                {
                    if (_files.Count > 500)
                        Thread.Sleep(1000);
                    try
                    {
                        foreach (string file in Directory.GetFiles(dir, ".", SearchOption.AllDirectories))
                        {
                            _files.Enqueue(new Files()
                            {
                                FilePath = System.IO.Path.GetFullPath(file),
                                //Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.IO.Path.GetFullPath(file)),
                                Name = System.IO.Path.GetFileNameWithoutExtension(file),
                            });
                            if (_btn)
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                    if (_btn)
                        break;
                }
                WorkCompleted();

                if (_i == _drives.Length - 1)
                    break;
                if (_btn)
                    break;
            }
        }

        private void WorkCompleted()
        {
            if (_i == _drives.Length)
                _btn = true;
            _i++;
            Path = _drives[_i].ToString();
            //Thread.Sleep(5000);
        }

        private void Run()
        {
            while (true)
            {
                if (_btn)
                    break;
                if (_files.Count == 0)
                    continue;
                if (_files.Peek().Hash == null)
                    continue;
                AddNew(_files.Dequeue());
            }
        }

        public void CalculateHash()
        {
            Files f = new Files();
            while (true)
            {
                if (_btn)
                    break;
                if (_files.Count == 0)
                    continue;
                using (MD5 md5Hash = MD5.Create())
                {
                    f.Hash = Methods.GetMd5Hash(md5Hash, _files.Peek().Name);
                }
                _files.Peek().Hash = f.Hash;
                Thread.Sleep(100);
            }
        }

        private void AddNew(Files fakeFiles)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                this.Add(fakeFiles);
                //Thread.Sleep(500);
            }));
        }

        public static void ButtonPressed()
        {
            _btn = true;
        }
    }
}