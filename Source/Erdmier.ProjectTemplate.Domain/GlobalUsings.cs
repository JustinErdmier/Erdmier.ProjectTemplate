global using Erdmier.ProjectTemplate.Domain.Common;
global using Erdmier.ProjectTemplate.Domain.Common.Enums;

global using StronglyTypedIds;

// Global StronglyTypedId Settings

[ assembly: StronglyTypedIdDefaults(converters: StronglyTypedIdConverter.TypeConverter
                                                | StronglyTypedIdConverter.SystemTextJson
                                                | StronglyTypedIdConverter.EfCoreValueConverter) ]
