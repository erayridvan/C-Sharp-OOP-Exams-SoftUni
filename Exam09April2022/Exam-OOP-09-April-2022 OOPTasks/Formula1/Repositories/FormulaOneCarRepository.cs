﻿using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Formula1.Repositories
{
    public class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {
        private List<IFormulaOneCar> models;

        public FormulaOneCarRepository()
        {
            models = new List<IFormulaOneCar>();
        }

        public IReadOnlyCollection<IFormulaOneCar> Models => this.models.AsReadOnly();

        public void Add(IFormulaOneCar model)
        {
            models.Add(model);
        }

        public IFormulaOneCar FindByName(string name)
        {
            return models.FirstOrDefault(c => c.Model == name);
        }

        public bool Remove(IFormulaOneCar model)
        {
            if (models.Contains(model))
            {
                models.Remove(model);
                return true;
            }

            return false;
        }
    }
}
