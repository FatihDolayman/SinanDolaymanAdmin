namespace SinanDolayman.Controllers
{
    internal class Account
    {
        private object cLOUD_NAME;
        private object aPI_KEY;
        private object aPI_SECRET;

        public Account(object cLOUD_NAME, object aPI_KEY, object aPI_SECRET)
        {
            this.cLOUD_NAME = cLOUD_NAME;
            this.aPI_KEY = aPI_KEY;
            this.aPI_SECRET = aPI_SECRET;
        }
    }
}