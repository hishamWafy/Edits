using AutoMapper;
using Istijara.API.Dtos;
using Istijara.Core.Entities;
using Istijara.Core.Interfaces;
using Istijara.Core.Interfaces.Repositories;
using Istijara.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istijara.Service.Services
{
    public class AdminService : IAdminServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task CreateCategoryAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.Complete();
        }

    }
}
