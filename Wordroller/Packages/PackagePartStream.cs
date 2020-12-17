using System.IO;
using System.Threading;

namespace Wordroller.Packages
{
	/// <summary>
	/// See <a href="https://support.microsoft.com/en-gb/kb/951731" /> for explanation (thanks Novacode.DocX)
	/// </summary>
	public class PackagePartStream : Stream
	{
		private static readonly Mutex mutex = new Mutex(false);

		private readonly Stream stream;

		public PackagePartStream(Stream stream)
		{
			this.stream = stream;
		}

		public override bool CanRead => stream.CanRead;

		public override bool CanSeek => stream.CanSeek;

		public override bool CanWrite => stream.CanWrite;

		public override long Length => stream.Length;

		public override long Position
		{
			get => stream.Position;
			set => stream.Position = value;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return stream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			stream.SetLength(value);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return stream.Read(buffer, offset, count);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			mutex.WaitOne(Timeout.Infinite, false);
			stream.Write(buffer, offset, count);
			mutex.ReleaseMutex();
		}

		public override void Flush()
		{
			mutex.WaitOne(Timeout.Infinite, false);
			stream.Flush();
			mutex.ReleaseMutex();
		}

		public override void Close()
		{
			stream.Close();
		}

		protected override void Dispose(bool disposing)
		{
			stream.Dispose();
		}
	}
}
