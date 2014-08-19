using ImageResizer;
using System.Collections.Generic;
using System.Web;

namespace ArquivoSilvaMagalhaes.Common
{
    public class FileUploadHelper
    {
        public static void SaveImage(HttpPostedFileBase source, int width, int height, string path, FitMode mode, bool dispose = true, bool resetSource = false)
        {
            var instructions = new Instructions
            {
                Width = width,
                Height = height,
                Mode = mode,
                Encoder = "freeimage",
                OutputFormat = OutputFormat.Jpeg
            };

            var job = new ImageJob
            {
                Instructions = instructions,
                Source = source.InputStream,
                Dest = path,
                CreateParentDirectory = true,
                ResetSourceStream = resetSource,
                DisposeSourceObject = dispose
            };

            ImageBuilder.Current.Build(job);
        }

        public static void GenerateVersions(string original)
        {
            Dictionary<string, string> versions = new Dictionary<string, string>();
            //Define the versions to generate and their filename suffixes.
            versions.Add("_thumb", "width=400&height=300&crop=auto&format=jpg");
            versions.Add("_large", "maxwidth=1024&maxheight=768&format=jpg");

            string basePath = ImageResizer.Util.PathUtils.RemoveExtension(original);

            //Generate each version
            foreach (string suffix in versions.Keys)
            {
                var job = new ImageJob
                {
                    Source = original,
                    Dest = basePath + suffix,
                    Instructions = new Instructions(versions[suffix]),
                    DisposeSourceObject = false,
                    AddFileExtension = true,
                    CreateParentDirectory = true
                };

                ImageBuilder.Current.Build(job);

                //Let the image builder add the correct extension based on the output file type
                //ImageBuilder.Current.Build(original, basePath + suffix,
                //  new ResizeSettings(versions[suffix]), false, true);
            }
        }
    }
}