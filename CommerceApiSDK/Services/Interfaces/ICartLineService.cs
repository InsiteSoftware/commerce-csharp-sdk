using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;

namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// A Service which manages cart lines
    /// </summary>
    public interface ICartLineService
    {
        event EventHandler OnIsAddingToCartSlowChange;
        bool IsAddingToCartSlow { get; }

        event EventHandler OnAddToCartRequestsCountChange;
        int AddToCartRequestsCount { get; }

        Task<CartLine> AddCartLine(AddCartLine cartLine);

        void CancelAllAddCartLineTasks();

        Task<CartLine> UpdateCartLine(CartLine cartLine);

        Task<bool> DeleteCartLine(CartLine cartLine);

        Task<List<CartLine>> AddCartLineCollection(List<AddCartLine> cartLineCollection);
    }
}
