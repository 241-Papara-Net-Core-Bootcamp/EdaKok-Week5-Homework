using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.DTOS;
using System;
using System.Collections.Generic;


namespace PaparaThirdWeek.Services.Abstracts
{
   public interface IFakeDataUserService
    {
        void Add(FakeDataUserDTO user);
        List<FakeDataUser> GetAllUsers();
    }
}
