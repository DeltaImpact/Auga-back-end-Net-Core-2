﻿using BackSide2.BL.Entity.PinDto;
using BackSide2.BL.Models.AuthorizeDto;
using BackSide2.BL.Models.ProfileDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extentions
{
    public static class EditProfileDtoExtentions
    {
        public static Person ToPerson(this EditProfileDto model, Person person)
        {
            person.Email = model.Email;
            person.UserName = model.Username;
            person.FirstName = model.FirstName;
            person.Surname = model.Surname;
            person.Gender = model.Gender;
            person.UpdatedBy = person.Id;

            return person;
        }
    }
}