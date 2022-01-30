namespace CommerceApiSDK.Models.Parameters
{
    using System;
    using System.Collections.Generic;
    using CommerceApiSDK.Attributes;

    public class BaseVmiLocationQueryParameters : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public Guid VmiLocationId { get; set; }
    }

    public class VmiLocationQueryParameters : BaseQueryParameters
    {
        public string UserId { get; set; }

        public string Filter { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }

    public class VmiBinQueryParameters : BaseVmiLocationQueryParameters
    {
        public string Filter { get; set; }

        public string SearchCriteria { get; set; }

        public string BinNumberFrom { get; set; }

        public string BinNumberTo { get; set; }

        public DateTime? PreviousCountFromDate { get; set; }

        public DateTime? PreviousCountToDate { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string Expand { get; set; } = null;
    }

    public class VmiCountQueryParameters : BaseVmiLocationQueryParameters
    {
        public Guid VmiBinId { get; set; }
    }

    public class VmiNoteQueryParameters : VmiCountQueryParameters
    {
        public Guid? VmiNoteId { get; set; }
    }

    public class VmiLocationDetailParameters : BaseVmiLocationQueryParameters
    {
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }

    public class VmiBinDetailParameters : BaseVmiLocationQueryParameters
    {
        public Guid VmiBinId { get; set; }
    }

    public class VmiCountDetailParameters : VmiBinDetailParameters
    {
        public Guid VmiCountId { get; set; }
    }

    public class VmiNoteDetailParameters : VmiBinDetailParameters
    {
        public Guid VmiNoteId { get; set; }
    }

    public class VmiLocationProductParameters : BaseVmiLocationQueryParameters
    {
        public string SearchCriteria { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;

        public string Filter { get; set; }
    }
}