using AutoMapper;
using Istijara.API.Dtos;
using Istijara.Core.Entities;
using Istijara.Core.Interfaces;
using Istijara.Repository.Dtos;
using Istijara.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Istijara.Service.Services
{
 public  class ItemServices : IItemServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemServices> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _environment;

        public ItemServices
            (
              IUnitOfWork unitOfWork,
              IMapper mapper,
              ILogger<ItemServices> logger,
              IHttpContextAccessor httpContextAccessor,
              IWebHostEnvironment environment
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
        }



        public async Task<ItemDto> CreateItemAsync(ItemDto dto)
        {
            // 1. Try to get or create the Category
            var category = await GetOrCreateCategoryAsync(dto.CategoryName, dto.Description);

            // 2. Get or create the current user's profile
            var userProfile = await GetOrCreateUserProfileAsync();

            // 3. Map the DTO to the Item entity
            var item = new Item
            {
                OwnerId = userProfile.Id,
                CategoryId = category.Id,
                Name = dto.Name,
                Description = dto.Description,
                DailyRentalPrice = dto.DailyRentalPrice,
                SecurityDeposit = dto.SecurityDeposit,
                MinRentalPeriod = dto.MinRentalPeriod,
                MaxRentalPeriod = dto.MaxRentalPeriod,
                Condition = dto.Condition,
                Location = dto.Location,
                DeliveryOptions = dto.DeliveryOptions,
                DeliveryRadius = dto.DeliveryRadius,
                DeliveryFee = dto.DeliveryFee
            };

            // 4. Save the item
            _unitOfWork.Repository<Item>().AddAsync(item);
            await _unitOfWork.Complete();

            // 5. Return mapped ItemDto (optional)
            return _mapper.Map<ItemDto>(item);
        }


        // Retrieves the current user profile or creates a default one in Development
        private async Task<UserProfile> GetOrCreateUserProfileAsync()
        {
            try
            {
                // Step 1: Get current user ID from Claims
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Step 2: If no user ID found (user not authenticated)
                if (string.IsNullOrEmpty(userId))
                {
                    // Step 3: If in development, create a fake user profile
                    if (_environment.IsDevelopment())
                    {
                        var devProfile = new UserProfile
                        {
                            UserId = Guid.NewGuid().ToString(),  // generate fake ID
                            FirstName = "Dev",
                            LastName = "User"
                        };

                        await _unitOfWork.Repository<UserProfile>().AddAsync(devProfile);
                        await _unitOfWork.Complete();

                        return devProfile;
                    }

                    // Step 4: Otherwise (in production), throw a proper exception
                    throw new InvalidOperationException("User must be authenticated. Please log in.");
                }

                // Step 5: Try to get the user profile from the database
                var userProfile = await _unitOfWork
                    .Repository<UserProfile>()
                    .FindAsync(p => p.UserId == userId);

                if (userProfile != null)
                    return userProfile;

                // Step 6: User is authenticated but has no profile
                if (_environment.IsDevelopment())
                {
                    var defaultProfile = new UserProfile
                    {
                        UserId = userId,
                        FirstName = "Dev",
                        LastName = "User"
                    };

                    await _unitOfWork.Repository<UserProfile>().AddAsync(defaultProfile);
                    await _unitOfWork.Complete();

                    return defaultProfile;
                }

                throw new InvalidOperationException("User profile not found.");
            }
            catch (Exception ex)
            {
                // Log the error or handle it as you prefer
                throw new Exception($"Failed to retrieve or create user profile: {ex.Message}");
            }
        }

        // Gets a Category by name, or creates it if it doesn't exist (only in Development)
        private async Task<Category> GetOrCreateCategoryAsync(string categoryName, string? description)
        {
            var category = await _unitOfWork
                .Repository<Category>()
                .FindAsync(c => c.Name == categoryName);

            if (category != null)
                return category;

            if (_environment.IsDevelopment())
            {
                var newCategory = new Category
                {
                    Name = categoryName,
                    Description = description,
                    
                };

                await _unitOfWork.Repository<Category>().AddAsync(newCategory);
                await _unitOfWork.Complete();

                return newCategory;
            }

            throw new InvalidOperationException("Category not found.");
        }


        public async Task<ItemDto> UpdateItemAsync(Guid id, ItemDto dto)
        {
            var item = await _unitOfWork.Repository<Item>().FindAsync(i => i.Id == id);

            if (item == null)
                throw new KeyNotFoundException($"Item with ID {id} not found.");

            // Update the properties
            item.Name = dto.Name;
            item.Description = dto.Description;
            item.DailyRentalPrice = dto.DailyRentalPrice;
            item.SecurityDeposit = dto.SecurityDeposit;
            item.MinRentalPeriod = dto.MinRentalPeriod;
            item.MaxRentalPeriod = dto.MaxRentalPeriod;
            item.Condition = dto.Condition;
            item.Location = dto.Location;
            item.DeliveryOptions = dto.DeliveryOptions;
            item.DeliveryRadius = dto.DeliveryRadius;
            item.DeliveryFee = dto.DeliveryFee;

            // Optional: handle category update
            var category = await _unitOfWork.Repository<Category>().FindAsync(c => c.Name == dto.CategoryName);
            if (category != null)
                item.CategoryId = category.Id;

            _unitOfWork.Repository<Item>().Update(item);
            await _unitOfWork.Complete();

            return _mapper.Map<ItemDto>(item);
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var item = await _unitOfWork.Repository<Item>().FindAsync(i => i.Id == id);

            if (item == null)
                return false;

            _unitOfWork.Repository<Item>().Delete(item);
            await _unitOfWork.Complete();
            return true;
        }

        public async Task<IReadOnlyList<ItemDto>> GetAllItemsAsync()
        {
            var items = await _unitOfWork.Repository<Item>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<ItemDto>>(items);
        }

        public async Task<ItemDto> GetItemByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid item ID.", nameof(id));
            var item = await _unitOfWork.Repository<Item>().GetByIdAsync(id);
            return _mapper.Map<ItemDto>(item);
        }
    }
}
