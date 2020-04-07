using Library.Application.Authors.Models;
using Library.Application.Dtos.Models;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _authorPropertyMapping =
          new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
          {
                       { "AuthorId", new PropertyMappingValue(new List<string>() { "AuthorId" } ) },
                       //{ "MainCategory", new PropertyMappingValue(new List<string>() { "MainCategory" } )},
                       { "Age", new PropertyMappingValue(new List<string>() { "DateOfBirth" } , true) },
                       { "Name", new PropertyMappingValue(new List<string>() { "FirstName", "LastName" }) }
          };

        private Dictionary<string, PropertyMappingValue> _bookPropertyMapping =
              new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
              {
                                   { "BookId", new PropertyMappingValue(new List<string>() { "BookId" } ) },
                                   //{ "MainCategory", new PropertyMappingValue(new List<string>() { "MainCategory" } )},
                                   { "Title", new PropertyMappingValue(new List<string>() { "Title" }) },
                                   { "Description", new PropertyMappingValue(new List<string>() { "Description" }) },
                                   { "Publisher", new PropertyMappingValue(new List<string>() { "Publisher" }) },
                                   { "ISBN", new PropertyMappingValue(new List<string>() { "ISBN" }) }
              };

        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<AuthorDto, Author>(_authorPropertyMapping));
            _propertyMappings.Add(new PropertyMapping<BookDto, Book>(_bookPropertyMapping));
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            // the string is separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            // run through the fields clauses
            foreach (var field in fieldsAfterSplit)
            {
                // trim
                var trimmedField = field.Trim();

                // remove everything after the first " " - if the fields 
                // are coming from an orderBy string, this part must be 
                // ignored
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                // find the matching property
                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;
        }


        public Dictionary<string, PropertyMappingValue> GetPropertyMapping
           <TSource, TDestination>()
        {
            // get matching mapping
            var matchingMapping = _propertyMappings
                .OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance " +
                $"for <{typeof(TSource)},{typeof(TDestination)}");
        }
    }
}
