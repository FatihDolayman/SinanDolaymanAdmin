using System;

namespace SinanDolayman.Controllers
{
    internal class Cloudinary
    {
        private Account account;

        public Cloudinary(Account account)
        {
            this.account = account;
        }

        internal object Upload(ImageUploadParams uploadParams, string v)
        {
            throw new NotImplementedException();
        }
    }
}