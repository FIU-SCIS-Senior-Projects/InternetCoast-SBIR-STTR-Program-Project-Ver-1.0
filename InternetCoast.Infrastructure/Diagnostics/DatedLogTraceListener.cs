using System;
using System.Diagnostics;
using System.IO;

namespace InternetCoast.Infrastructure.Diagnostics
{
    public class DatedLogTraceListener : TextWriterTraceListener
    {

        private readonly string _logFileLocation;
        private DateTime _currentDate;
        StreamWriter _traceWriter;
        private bool _disposed = false;

        public DatedLogTraceListener(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            _logFileLocation = fileName;
            _traceWriter = new StreamWriter(GenerateFileName(), true);
        }

        public override void Write(string message)
        {
            CheckRollover();
            _traceWriter.Write(message);
        }


        public override void WriteLine(string message)
        {
            CheckRollover();
            _traceWriter.WriteLine("{0} - {1}", DateTime.Now.ToLongTimeString(), message);
        }

        private string GenerateFileName()
        {
            _currentDate = DateTime.Today;
            return Path.Combine(Path.GetDirectoryName(_logFileLocation),
                Path.GetFileNameWithoutExtension(_logFileLocation) + "_" + _currentDate.ToString("yyyyMMdd") +
                Path.GetExtension(_logFileLocation));
        }

        private void CheckRollover()
        {
            if (_currentDate.CompareTo(DateTime.Today) == 0) return;
            _traceWriter.Close();
            _traceWriter = new StreamWriter(GenerateFileName(), true);
        }

        public override void Flush()
        {
            lock (this)
            {
                _traceWriter?.Flush();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _traceWriter.Close();
            }

            _disposed = true;
            base.Dispose(disposing);
        }
    }

}
