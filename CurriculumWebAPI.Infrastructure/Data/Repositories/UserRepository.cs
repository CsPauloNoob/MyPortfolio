﻿using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Infrastructure.Data.Context;
using CurriculumWebAPI.Infrastructure.IdentityConfiguration;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Infrastructure.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly MyContext _context;

        public UserRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<int> AddNew(User entity)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var hashedPassword = passwordHasher.HashPassword(null, entity.Password);

            await _context.Users.AddAsync(new ApplicationUser()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                NormalizedUserName = entity.UserName.ToUpper().Normalize(),
                Email = entity.Email,
                NormalizedEmail = entity.Email.ToUpper().Normalize(),
                PasswordHash = hashedPassword
            });

            return await _context.SaveChangesAsync();
        }


        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(string id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user is null)
                    return null;

                else
                    return new User
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.UserName
                    };
            }

            catch(Exception)
            {
                return null;
            }
        }


        // ----> Melhorar
        public async Task<User> Update(User entity)
        {
            var newUser = _context.Users.Find(entity.Id);

            if (entity.Curriculum is not null)
            {
                newUser.Curriculum = entity.Curriculum;

                _context.Users.Update(newUser);
                await _context.SaveChangesAsync();

                return entity;
            }

            else throw new NotFoundInDatabase("");
        }
    }
}
