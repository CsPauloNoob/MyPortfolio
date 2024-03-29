﻿using CurriculumWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Interfaces
{
    public interface IPdfGenerator
    {
        public Task<string> Generate(Curriculum curriculum);
    }
}
