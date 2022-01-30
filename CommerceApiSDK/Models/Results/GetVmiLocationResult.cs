namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetVmiLocationResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<VmiLocationModel> VmiLocations { get; set; }
    }

    public class GetVmiBinResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<VmiBinModel> VmiBins { get; set; }
    }

    public class GetVmiCountResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<VmiCountModel> VmiCounts { get; set; }
    }

    public class GetVmiNoteResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<VmiNoteModel> VmiNotes { get; set; }
    }
}