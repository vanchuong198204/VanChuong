﻿using BTLAB_API.Data;
using BTLAB_API.Models.Domain;
using BTLAB_API.Models.DTO;
using System.Security.Policy;

namespace BTLAB_API.Repositories
{
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLPublisherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<PublisherDTO> GetAllPublishers()
        {
            //Get Data From Database -Domain Model 
            var allPublishersDomain = _dbContext.Publishers.ToList();

            //Map domain models to DTOs 
            var allPublisherDTO = new List<PublisherDTO>();
            foreach (var publisherDomain in allPublishersDomain)
            {
                allPublisherDTO.Add(new PublisherDTO()
                {
                    Id = publisherDomain.Id,
                    Name = publisherDomain.Name

                });
            }
            return allPublisherDTO;
        }

        public PublisherNoIdDTO GetPublisherById(int id)
        {
            // get book Domain model from Db 
            var publisherWithIdDomain = _dbContext.Publishers.FirstOrDefault(x => x.Id == id);
            if (publisherWithIdDomain != null)
            { //Map Domain Model to DTOs 
                var publisherNoIdDTO = new PublisherNoIdDTO
                {
                    Name = publisherWithIdDomain.Name,
                };

                return publisherNoIdDTO;
            }

            return null;

        }
       

        public PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (publisherDomain != null)
            {
                publisherDomain.Name = publisherNoIdDTO.Name;

                _dbContext.SaveChanges();
            }
            return null;
        }
        public Publisher? DeletePublisherById(int id)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (publisherDomain != null)
            {
                _dbContext.Publishers.Remove(publisherDomain);
                _dbContext.SaveChanges();
            }
            return null;
        }

        public AddPublisherDTO AddPublisher(AddPublisherDTO addPublisherRequestDTO)
        {
            var publisherDomainModel = new Publishers
            {
                Name = addPublisherRequestDTO.Name,

            };
            //Use Domain Model to create Book 
            _dbContext.Publishers.Add(publisherDomainModel);
            _dbContext.SaveChanges();
            return addPublisherRequestDTO;
        }
    }
}
