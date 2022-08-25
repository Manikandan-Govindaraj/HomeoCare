using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeoCare.Data.Interface
{
    public interface IShoppingCartRepository
    {
        public int AddtoCart(int ProductID);
        public int RemovefromCart(int ProductID);
        public bool UpdateCart(int ProductID, int Quantity);
    }
}
