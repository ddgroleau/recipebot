﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public interface IRecipeRepository
     {
        public void InsertOne(Object record);
        public IEnumerable<IRecipeServiceDTO> FindMany(string text);
    }
}