using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TestXamarin.Services
{
    public interface IFilePickerService
    {
        Task<Stream> PickImageFromGallery();
    }
}
