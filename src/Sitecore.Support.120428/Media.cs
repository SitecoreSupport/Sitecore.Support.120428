using Sitecore.Diagnostics;
using Sitecore.Resources.Media;

namespace Sitecore.Support.Resources.Media
{
    public class Media : Sitecore.Resources.Media.Media
    {
        private MediaData mediaData;

        public override MediaStream GetStream(MediaOptions options)
        {
            MediaStream stream = base.GetStream(options);

            if (stream != null)
            {
                string fileName = stream.FileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    stream.Headers.Headers["Content-Disposition"] = string.Format("{0}; filename=\"{1}\"", this.GetContentDispositionType(stream), fileName);
                }
                return stream;
            }
            return null;
        }


        private string GetContentDispositionType(MediaStream stream)
        {
            if (!stream.ForceDownload)
            {
                return "inline";
            }
            return "attachment";
        }

        public override Sitecore.Resources.Media.Media Clone()
        {
            Assert.IsTrue(base.GetType() == typeof(Sitecore.Support.Resources.Media.Media), "The Clone() method must be overridden to support prototyping.");
            return new Sitecore.Support.Resources.Media.Media { mediaData = this.mediaData };
        }

    }
}