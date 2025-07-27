using Istijara.API.Dtos;
using Istijara.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istijara.Service.Services.Interfaces
{
    public interface IAdminServices
    {


        Task CreateCategoryAsync(CategoryDto category);



    }
}
