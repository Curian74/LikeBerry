using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;

namespace LikeBerry
{
    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return null;

            string imagePath = value.ToString();
            BitmapImage image = new BitmapImage();

            try
            {
                if (Uri.TryCreate(imagePath, UriKind.Absolute, out Uri uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    // It's a web URL
                    image.BeginInit();
                    image.UriSource = uriResult;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                }
                else
                {
                    // It's a local path
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);

                    if (File.Exists(fullPath))
                    {
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.UriSource = new Uri(fullPath);
                        image.EndInit();
                    }
                    else
                    {
                        // Return a default image or null if the file doesn't exist
                        return null;
                    }
                }

                image.Freeze(); // This can help with performance and thread-safety
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                System.Diagnostics.Debug.WriteLine($"Error loading image: {ex.Message}");
                return null;
            }

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}