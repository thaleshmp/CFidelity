namespace CFidelity.API.Core.UploadManagement
{
    public class UploadFile
    {
        private const string PREFIX = "Live";

        public string Name { get; set; }

        public string Container { get; set; }

        public string PrefixFolderName { get; set; }

        public string File { get; set; }

        public string PrefixFolderNameFullName
        {
            get
            {
                return string.IsNullOrEmpty(PrefixFolderName) ? Name : string.Format("{0}/{1}", PrefixFolderName, Name);
            }
        }
    }
}
