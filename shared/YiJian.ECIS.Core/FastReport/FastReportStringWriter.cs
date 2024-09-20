using System.IO;
using System.Text;

namespace YiJian.ECIS.Core.FastReport
{
    public class FastReportStringWriter : StringWriter
    {
        private readonly Encoding stringWriterEncoding;
        public FastReportStringWriter()
            : base()
        {
            this.stringWriterEncoding = Encoding.UTF8;
        }

        public override Encoding Encoding
        {
            get
            {
                return this.stringWriterEncoding;
            }
        }
    }
}
