﻿using FrogExebitionAPI.DTO.ExebitionDTOs;
using FrogExebitionAPI.DTO.FrogDTOs;
using FrogExebitionAPI.Models;

namespace FrogExebitionAPI.Interfaces
{
    public interface IExebitionService
    {
        Task<ExebitionDtoDetail> CreateExebition(ExebitionDtoForCreate exebition);
        Task DeleteExebition(Guid id);
        Task<IEnumerable<ExebitionDtoDetail>> GetAllExebitions();
        Task<ExebitionDtoDetail> GetExebition(Guid id);
        Task UpdateExebition(Guid id, ExebitionDtoForCreate exebition);
        Task<IEnumerable<FrogDtoRating>> GetRating(Guid id);
    }
}