﻿using FrogExebitionAPI.DTO.FrogDTOs;
using FrogExebitionAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrogExebitionAPI.Interfaces
{
    public interface IFrogService
    {
        public Task<IEnumerable<FrogDtoGeneral>> GetAllFrogs();
        public Task<FrogDtoDetail> GetFrog(Guid id);
        public Task<FrogDtoDetail> CreateFrog(FrogDtoForCreate frog);
        public Task DeleteFrog(Guid id);
        public Task UpdateFrog(Guid id, FrogDtoForUpdate frog);
        //Task<IEnumerable<FrogDtoGeneral>> GetFrogsRating();
    }
}
