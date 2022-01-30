namespace CommerceApiSDK.Services.Interfaces
{
    using System.Threading.Tasks;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;

    public interface ITranslationService
    {
        Task<TranslationResults> GetTranslations(TranslationQueryParameters parameters = null);

        int GetMaxLengthOfTranslationText();
    }
}
