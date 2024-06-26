﻿using ORT.Vet.IDataAccess; 
using ORT.Vet.IBusinessLogic;
using ORT.Vet.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ORT.Vet.BusinessLogic
{
    public class PetLogic : IBusinessLogic<Pet>
    {
        private readonly IGenericRepository<Pet> _repository;
        // Si quiero el current user simplemente lo pido por iny de dependencia
        // El session logic ya lo tiene cargado cuando lo pedi en el filter
        private readonly ISessionLogic _sessionLogic;

        public PetLogic(IGenericRepository<Pet> repository, ISessionLogic sessionLogic)
        {
            _repository = repository;
            _sessionLogic = sessionLogic;
        }

        public List<Pet> GetAll()
        {
            return _repository.GetAll<Pet>().ToList();
        }

        public Pet GetById(int id)
        {
            return _repository.Get(p => p.Id == id);
        }

        public Pet Create(Pet pet)
        {
            if (pet.Owner.Name != _sessionLogic.GetCurrentUser().Name)
            {
                throw new ArgumentException("The owner of the pet is not the current user.");
            }
            _repository.Insert(pet);
            _repository.Save();
            return pet;
        }

        public Pet Update(int id, Pet updatedPet)
        {
            var pet = _repository.Get(p => p.Id == id);
            if (pet != null)
            {
                pet.Name = updatedPet.Name;
                pet.Age = updatedPet.Age;
                _repository.Update(pet);
                _repository.Save();
            }
            return pet;
        }

        public bool Delete(int id)
        {
            var pet = _repository.Get(p => p.Id == id);
            if (pet != null)
            {
                _repository.Delete(pet);
                _repository.Save();
                return true;
            }
            return false;
        }
    }
}
