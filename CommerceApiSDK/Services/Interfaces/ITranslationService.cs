using System.Threading.Tasks;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface ITranslationService
    {
        Task<TranslationResults> GetTranslations(TranslationQueryParameters parameters = null);

        int GetMaxLengthOfTranslationText();
    }
}
