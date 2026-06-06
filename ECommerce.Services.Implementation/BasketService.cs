using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Models.CartModule;
using ECommerce.Services.Abstraction;
using ECommerce.Services.Implementation.Exceptions;
using ECommerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Implementation
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO CreateOrUpdateBasket)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(CreateOrUpdateBasket);
            var basketEntity = await _basketRepository.CreateOrUpdateBasketAsync(customerBasket);
            return _mapper.Map<BasketDTO>(basketEntity);
        }

        public async Task<bool> DeleteBasketAsync(string BasketId) => await _basketRepository.DeleteBasketAsync(BasketId);

        public async Task<BasketDTO> GetBasketAsync(string BasketId)
        {
            var basket = await _basketRepository.GetBasketAsync(BasketId);
            if (basket == null) {
                throw new NotFoundException("Basket", BasketId);
            }
            return _mapper.Map<BasketDTO>(basket);
        }
    }
}
