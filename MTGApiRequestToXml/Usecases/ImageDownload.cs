using MTGApiRequestToXml.Data;
using MTGApiRequestToXml.Domain.Entities;
using MTGApiRequestToXml.Domain;

namespace MTGApiRequestToXml.Usecases
{
    /// <summary>
    /// Handling image download
    /// </summary>
    public class ImageDownload : AttributeBaseClass
    {
        /// <summary>
        /// Path to save image
        /// </summary>
        public string imagePath { get; set; }    

        /// <summary>
        /// Constructor
        /// </summary>
        public ImageDownload(AttributeBaseClass attributeBase) {
            imagePath = Path.Combine(attributeBase.folderPath, "Asset","Images");
        }

        /// <summary>
        /// Download image
        /// </summary>
        /// <param name="imageInfo">ImageInfo</param>
        /// <returns>Task</returns>
        public async Task Run(ImageInfo imageInfo)
        {
            
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            SryfallAPI sryFallApiRequest = new SryfallAPI();
            byte[] imageBytes = await sryFallApiRequest.DownloadImageAsync(imageInfo.Url);
            
            await File.WriteAllBytesAsync(Path.Combine(imagePath, imageInfo.FileName), imageBytes);
           
        }
    }
}
